CREATE PROCEDURE [dbo].[usp_Sensor_Stage_Insert]

@dt_session smalldatetime
,@nvc_source nvarchar(50)
,@nvc_dns nvarchar(100)
,@nvc_dnsstatus nvarchar(10)
,@nvc_ip nvarchar(20)
,@nvc_ipstatus nvarchar(10)
,@nvc_datacenter nvarchar(50)
,@nvc_datacentertag nvarchar(10)
,@i_port int
,@i_latency int

AS

BEGIN
  INSERT INTO [dbo].[tbl_Sensor_Stage]
  ([dt_session]
  ,[nvc_source]
  ,[nvc_dns]
  ,[nvc_dnsstatus]
  ,[nvc_ip]
  ,[nvc_ipstatus]
  ,[nvc_datacenter]
  ,[nvc_datacentertag]
  ,[i_port]
  ,[i_latency])
  VALUES
  (@dt_session
  ,@nvc_source
  ,@nvc_dns
  ,@nvc_dnsstatus
  ,@nvc_ip
  ,@nvc_ipstatus
  ,@nvc_datacenter
  ,@nvc_datacentertag
  ,@i_port
  ,@i_latency)
END
