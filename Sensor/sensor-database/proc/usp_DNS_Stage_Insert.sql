--IF (OBJECT_ID(N'[dbo].[usp_Staging_DNS_Upsert]', 'P') IS NULL)
--BEGIN
  CREATE PROCEDURE [dbo].[usp_DNS_Stage_Insert]

  @dt_session smalldatetime
  ,@nvc_source nvarchar(50)
  ,@nvc_datacenter nvarchar(50)
  ,@nvc_datacentertag nvarchar(10)
  ,@nvc_dns nvarchar(100)
  ,@nvc_ip nvarchar(20)
  ,@nvc_status nvarchar(10)

  AS

  BEGIN
    INSERT INTO [dbo].[tbl_DNS_Stage]
    ([dt_session]
    ,[nvc_source]
    ,[nvc_datacenter]
    ,[nvc_datacentertag]
    ,[nvc_dns]
    ,[nvc_ip]
    ,[nvc_status])
    VALUES
    (@dt_session
    ,@nvc_source
    ,@nvc_datacenter
    ,@nvc_datacentertag
    ,@nvc_dns
    ,@nvc_ip
    ,@nvc_status)
  END
--END
