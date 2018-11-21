CREATE PROCEDURE [dbo].[usp_Sensor_TCPLatency_Insert]

@dt_session smalldatetime
,@nvc_source nvarchar(50)
,@nvc_dns nvarchar(100)
,@nvc_ip nvarchar(20)
,@i_latency int

AS

BEGIN
  INSERT INTO [dbo].[tbl_Sensor_TCPLatency_Stage]
  ([dt_session]
  ,[nvc_source]
  ,[nvc_dns]
  ,[nvc_ip]
  ,[i_latency])
  VALUES
  (@dt_session
  ,@nvc_source
  ,@nvc_dns
  ,@nvc_ip
  ,@i_latency)
END
