using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using static KLOGLoader.Program;

namespace KLOGLoader
{
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

            try
            {
                foreach (var record in logCollection)
                {
                    if (record.LogData.Contains("KLOG_BLOCK"))
                    {
                        if (record.LogData.Contains("KLOG_BLOCK_START"))
                        {
                            var emptyResponse = AddBlockStart(record, instanceId);
                        }
                        else if (record.LogData.Contains("KLOG_BLOCK_STOP"))
                        {
                            var emptyResponse = UpdateBlockStop(record);
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