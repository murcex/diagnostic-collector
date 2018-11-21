CREATE PROCEDURE [dbo].[usp_Sensor_DNSDistribution_Insert]

@dt_session smalldatetime
,@nvc_source nvarchar(50)
,@nvc_dns nvarchar(100)
,@nvc_ip nvarchar(20)
,@i_count int

AS

BEGIN
  INSERT INTO [dbo].[tbl_Sensor_DNSDistribution_Stage]
  ([dt_session]
  ,[nvc_source]
  ,[nvc_dns]
  ,[nvc_ip]
  ,[i_count])
  VALUES
  (@dt_session
  ,@nvc_source
  ,@nvc_dns
  ,@nvc_ip
  ,@i_count)
END
