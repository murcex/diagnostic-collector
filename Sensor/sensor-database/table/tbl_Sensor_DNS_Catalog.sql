IF (OBJECT_ID(N'[dbo].[tbl_Sensor_DNS_Catalog]', 'U') IS NULL)
BEGIN
  CREATE TABLE [dbo].[tbl_Sensor_DNS_Catalog]
  (
    [nvc_dns] [nvarchar](100) NOT NULL
  ,[nvc_configuration] [nvarchar](4000) NOT NULL
  )
END
