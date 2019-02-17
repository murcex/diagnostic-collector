namespace KLOGLoader
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Data;

    class DataAccessor
    {
        // TODO: Add try/catch to all methods
        // TODO: Add summary to all methods

        public static Guid CheckInstanceId(Guid instanceId)
        {
            Guid check = new Guid();

            using (var connection = new SqlConnection(Global.SqlConnetionString))
            {
                var cmd = new SqlCommand("usp_Kiroku_KInstances_SelectCheck", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                // Data Capacity Columns -- example: command.Parameters.AddWithValue("Value1", Value1);
                // Example: command.Parameters.AddWithValue("execution_runtime", item.execution_runtime);
                cmd.Parameters.AddWithValue("ui_instanceid", instanceId.ToString());

                connection.Open();

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    check = (Guid)reader["ui_instanceid"];
                }
            }

            return check;
        }

        public static SQLResponse AddInstanceStart(InstanceModel instanceHeader)
        {
            SQLResponse response = new SQLResponse();

            try
            {
                using (var connection = new SqlConnection(Global.SqlConnetionString))
                {
                    var cmd = new SqlCommand("usp_Kiroku_KInstances_InsertStart", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Data Capacity Columns -- example: command.Parameters.AddWithValue("Value1", Value1);
                    // Example: command.Parameters.AddWithValue("execution_runtime", item.execution_runtime);
                    cmd.Parameters.AddWithValue("ui_instanceid", instanceHeader.InstanceID);
                    cmd.Parameters.AddWithValue("dt_instancestart", instanceHeader.EventTime);
                    cmd.Parameters.AddWithValue("ui_applicationid", instanceHeader.ApplicationID);
                    cmd.Parameters.AddWithValue("nvc_trackid", instanceHeader.TrackID);
                    cmd.Parameters.AddWithValue("nvc_regionid", instanceHeader.RegionID);
                    cmd.Parameters.AddWithValue("nvc_clusterid", instanceHeader.ClusterID);
                    cmd.Parameters.AddWithValue("nvc_deviceid", instanceHeader.DeviceID);
                    cmd.Parameters.AddWithValue("nvc_klogversion", instanceHeader.Version);

                    connection.Open();

                    var reader = cmd.ExecuteReader();

                    response.Successful();

                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Failure(ex.ToString());

                return response;
            }
        }

        public static SQLResponse UpdateInstanceStop(InstanceModel instanceHeader)
        {
            SQLResponse response = new SQLResponse();

            try
            {
                using (var connection = new SqlConnection(Global.SqlConnetionString))
                {
                    var cmd = new SqlCommand("usp_Kiroku_KInstances_UpdateStop", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Data Capacity Columns -- example: command.Parameters.AddWithValue("Value1", Value1);
                    // Example: command.Parameters.AddWithValue("execution_runtime", item.execution_runtime);
                    cmd.Parameters.AddWithValue("ui_instanceid", instanceHeader.InstanceID);
                    cmd.Parameters.AddWithValue("dt_instancestop", instanceHeader.EventTime);

                    connection.Open();

                    var reader = cmd.ExecuteReader();

                    response.Successful();

                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Failure(ex.ToString());

                return response;
            }
        }

        public static SQLResponse AddLogs(List<LogRecordModel> logCollection, Guid instanceId)
        {
            SQLResponse response = new SQLResponse();

            // Add blocktracker dictionary
            Dictionary<Guid, LogRecordModel> blockCache = new Dictionary<Guid, LogRecordModel>();

            try
            {
                foreach (var record in logCollection)
                {
                    if (record.LogData.Contains("KLOG_BLOCK"))
                    {
                        if (record.LogData.Contains("KLOG_BLOCK_START"))
                        {
                            //var emptyResponse = AddBlockStart(record, instanceId);

                            blockCache.Add(record.BlockID, record);
                        }
                        else if (record.LogData.Contains("KLOG_BLOCK_STOP"))
                        {
                            //var emptyResponse = UpdateBlockStop(record);

                            var startBlock = blockCache[record.BlockID];

                            if (startBlock != null)
                            {
                                var emptyResponse = AddBlock(record, startBlock, instanceId);
                            }
                            else
                            {
                                // TODO: Add Error
                            }

                            // TODO: Remove dictionary entry
                        }
                        else
                        {
                            // TODO: Add Error, should never reach.
                        }
                    }
                    else
                    {
                        using (var connection = new SqlConnection(Global.SqlConnetionString))
                        {
                            var cmd = new SqlCommand("usp_Kiroku_KLogs_InsertAdd", connection);
                            cmd.CommandType = CommandType.StoredProcedure;

                            // Data Capacity Columns -- example: command.Parameters.AddWithValue("Value1", Value1);
                            // Example: command.Parameters.AddWithValue("execution_runtime", item.execution_runtime);
                            cmd.Parameters.AddWithValue("dt_session", record.EventTime);
                            cmd.Parameters.AddWithValue("dt_event", record.EventTime);
                            cmd.Parameters.AddWithValue("ui_blockid", record.BlockID);
                            cmd.Parameters.AddWithValue("nvc_blockname", record.BlockName);
                            cmd.Parameters.AddWithValue("nvc_logtype", record.LogType);
                            cmd.Parameters.AddWithValue("nvc_logdata", record.LogData);

                            connection.Open();

                            var reader = cmd.ExecuteReader();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.Failure(ex.ToString());

                return response;
            }

            response.Successful();

            return response;
        }

        public static SQLResponse AddBlock(LogRecordModel logRecord, LogRecordModel startRecord, Guid instanceId)
        {
            SQLResponse response = new SQLResponse();

            try
            {
                using (var connection = new SqlConnection(Global.SqlConnetionString))
                {
                    var cmd = new SqlCommand("usp_Kiroku_KBlocks_InsertBlock", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Data Capacity Columns -- example: command.Parameters.AddWithValue("Value1", Value1);
                    // Example: command.Parameters.AddWithValue("execution_runtime", item.execution_runtime);
                    cmd.Parameters.AddWithValue("ui_instanceid", instanceId);
                    cmd.Parameters.AddWithValue("ui_blockid", logRecord.BlockID);
                    cmd.Parameters.AddWithValue("nvc_blockname", logRecord.BlockName);
                    cmd.Parameters.AddWithValue("dt_blockstart", startRecord.EventTime);
                    cmd.Parameters.AddWithValue("dt_blockstop", logRecord.EventTime);

                    connection.Open();

                    var reader = cmd.ExecuteReader();

                    response.Successful();

                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Failure(ex.ToString());

                return response;
            }
        }

        public static SQLResponse AddBlockStart(LogRecordModel logRecord, Guid instanceId)
        {
            SQLResponse response = new SQLResponse();

            try
            {
                using (var connection = new SqlConnection(Global.SqlConnetionString))
                {
                    var cmd = new SqlCommand("usp_Kiroku_KBlocks_InsertStart", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Data Capacity Columns -- example: command.Parameters.AddWithValue("Value1", Value1);
                    // Example: command.Parameters.AddWithValue("execution_runtime", item.execution_runtime);
                    cmd.Parameters.AddWithValue("ui_instanceid", instanceId);
                    cmd.Parameters.AddWithValue("ui_blockid", logRecord.BlockID);
                    cmd.Parameters.AddWithValue("nvc_blockname", logRecord.BlockName);
                    cmd.Parameters.AddWithValue("dt_blockstart", logRecord.EventTime);

                    connection.Open();

                    var reader = cmd.ExecuteReader();

                    response.Successful();

                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Failure(ex.ToString());

                return response;
            }
        }

        public static SQLResponse UpdateBlockStop(LogRecordModel logRecord)
        {
            SQLResponse response = new SQLResponse();

            try
            {
                using (var connection = new SqlConnection(Global.SqlConnetionString))
                {
                    var cmd = new SqlCommand("usp_Kiroku_KBlocks_UpdateStop", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Data Capacity Columns -- example: command.Parameters.AddWithValue("Value1", Value1);
                    // Example: command.Parameters.AddWithValue("execution_runtime", item.execution_runtime);
                    cmd.Parameters.AddWithValue("ui_blockid", logRecord.BlockID);
                    cmd.Parameters.AddWithValue("dt_blockstop", logRecord.EventTime);

                    connection.Open();

                    var reader = cmd.ExecuteReader();

                    response.Successful();

                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Failure(ex.ToString());

                return response;
            }
        }

        public static SQLResponse UpdateBlockEmptyStop(LogRecordModel logRecord, Guid instanceId)
        {
            SQLResponse response = new SQLResponse();

            try
            {
                using (var connection = new SqlConnection(Global.SqlConnetionString))
                {
                    var cmd = new SqlCommand("usp_Kiroku_KBlocks_UpdateEmptyStop", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Data Capacity Columns -- example: command.Parameters.AddWithValue("Value1", Value1);
                    // Example: command.Parameters.AddWithValue("execution_runtime", item.execution_runtime);
                    cmd.Parameters.AddWithValue("ui_instanceid", instanceId);
                    cmd.Parameters.AddWithValue("dt_blockstop", logRecord.EventTime);

                    connection.Open();

                    var reader = cmd.ExecuteReader();

                    response.Successful();

                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Failure(ex.ToString());

                return response;
            }
        }
    }
}