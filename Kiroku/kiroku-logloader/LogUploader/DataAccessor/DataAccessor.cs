namespace KLOGLoader
{
    using System;
    using System.Data.SqlClient;
    using System.Data;

    /// <summary>
    /// Azure SQL Server Data Accessor.
    /// </summary>
    class DataAccessor
    {
        /// <summary>
        /// Check if Kiroku logging Instance already exist in database.
        /// </summary>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public static SQLResponseModel CheckInstanceId(Guid instanceId)
        {
            SQLResponseModel response = new SQLResponseModel();
            Guid check = new Guid();

            try
            {
                using (var connection = new SqlConnection(Global.SqlConnetionString))
                {
                    var cmd = new SqlCommand("usp_Kiroku_KInstances_SelectCheck", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("ui_instanceid", instanceId.ToString());

                    connection.Open();

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        check = (Guid)reader["ui_instanceid"];
                    }
                }

                response.Id = check;
                response.Successful();

                return response;
            }
            catch (Exception ex)
            {
                response.Failure(ex.ToString());

                return response;
            }
        }

        /// <summary>
        /// Add the start of the Kiroku logging Instance.
        /// </summary>
        /// <param name="instanceHeader"></param>
        /// <returns></returns>
        public static SQLResponseModel AddInstanceStart(InstanceModel instanceHeader)
        {
            SQLResponseModel response = new SQLResponseModel();

            try
            {
                using (var connection = new SqlConnection(Global.SqlConnetionString))
                {
                    var cmd = new SqlCommand("usp_Kiroku_KInstances_InsertStart", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

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

        /// <summary>
        /// Update existing Kiroku logging Instance with the stop time.
        /// </summary>
        /// <param name="instanceHeader"></param>
        /// <returns></returns>
        public static SQLResponseModel UpdateInstanceStop(InstanceModel instanceHeader)
        {
            SQLResponseModel response = new SQLResponseModel();

            try
            {
                using (var connection = new SqlConnection(Global.SqlConnetionString))
                {
                    var cmd = new SqlCommand("usp_Kiroku_KInstances_UpdateStop", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

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

        /// <summary>
        /// Add Kiroku Metric event.
        /// </summary>
        /// <param name="record"></param>
        /// <param name="metric"></param>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public static SQLResponseModel AddMetric(LogRecordModel record, MetricRecordModel metric, Guid instanceId)
        {
            SQLResponseModel response = new SQLResponseModel();

            try
            {
                using (var connection = new SqlConnection(Global.SqlConnetionString))
                {
                    var cmd = new SqlCommand("usp_Kiroku_KMetrics_InsertAdd", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("dt_session", record.EventTime);
                    cmd.Parameters.AddWithValue("dt_event", record.EventTime);
                    cmd.Parameters.AddWithValue("ui_blockid", record.BlockID);
                    cmd.Parameters.AddWithValue("nvc_metricname", metric.MetricName);
                    cmd.Parameters.AddWithValue("nvc_metrictype", metric.MetricType);
                    cmd.Parameters.AddWithValue("nvc_metricvalue", metric.MetricValue);

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

        /// <summary>
        /// Add Kiroku Result event.
        /// </summary>
        /// <param name="record"></param>
        /// <param name="result"></param>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public static SQLResponseModel AddResult(LogRecordModel record, ResultRecordModel result, Guid instanceId)
        {
            SQLResponseModel response = new SQLResponseModel();

            try
            {
                using (var connection = new SqlConnection(Global.SqlConnetionString))
                {
                    var cmd = new SqlCommand("usp_Kiroku_KResults_InsertAdd", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("dt_session", record.EventTime);
                    cmd.Parameters.AddWithValue("dt_event", record.EventTime);
                    cmd.Parameters.AddWithValue("nvc_resulttype", "Block");
                    cmd.Parameters.AddWithValue("ui_resultid", record.BlockID);
                    cmd.Parameters.AddWithValue("i_result", result.Result);
                    cmd.Parameters.AddWithValue("nvc_resultdata", result.ResultData);

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

        /// <summary>
        /// Add Kiroku log event.
        /// </summary>
        /// <param name="record"></param>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public static SQLResponseModel AddLog(LogRecordModel record, Guid instanceId)
        {
            SQLResponseModel response = new SQLResponseModel();

            try
            {
                using (var connection = new SqlConnection(Global.SqlConnetionString))
                {
                    var cmd = new SqlCommand("usp_Kiroku_KLogs_InsertAdd", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("dt_session", record.EventTime);
                    cmd.Parameters.AddWithValue("dt_event", record.EventTime);
                    cmd.Parameters.AddWithValue("ui_blockid", record.BlockID);
                    cmd.Parameters.AddWithValue("nvc_blockname", record.BlockName);
                    cmd.Parameters.AddWithValue("nvc_logtype", record.LogType);
                    cmd.Parameters.AddWithValue("nvc_logdata", record.LogData);

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

        /// <summary>
        /// Add Kiroku Block pair.
        /// </summary>
        /// <param name="logRecord"></param>
        /// <param name="startRecord"></param>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public static SQLResponseModel AddBlock(LogRecordModel logRecord, LogRecordModel startRecord, Guid instanceId)
        {
            SQLResponseModel response = new SQLResponseModel();

            try
            {
                using (var connection = new SqlConnection(Global.SqlConnetionString))
                {
                    var cmd = new SqlCommand("usp_Kiroku_KBlocks_InsertBlock", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

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

        /// <summary>
        /// Update Kiroku Block, with an empty stop time.
        /// </summary>
        /// <param name="logRecord"></param>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public static SQLResponseModel UpdateBlockEmptyStop(LogRecordModel logRecord, Guid instanceId)
        {
            SQLResponseModel response = new SQLResponseModel();

            try
            {
                using (var connection = new SqlConnection(Global.SqlConnetionString))
                {
                    var cmd = new SqlCommand("usp_Kiroku_KBlocks_UpdateEmptyStop", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

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

        /// <summary>
        /// Kiroku data Retention, all tables.
        /// </summary>
        /// <param name="days"></param>
        /// <returns></returns>
        public static SQLResponseModel Retention(double days)
        {
            SQLResponseModel response = new SQLResponseModel();

            try
            {
                using (var connection = new SqlConnection(Global.SqlConnetionString))
                {
                    var cmd = new SqlCommand("usp_Kiroku_Retention", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("i_days", days);

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