IF (OBJECT_ID(N'[dbo].[tbl_DNS_Catalog]', 'U') IS NULL)
BEGIN
  CREATE TABLE [dbo].[tbl_DNS_Catalog]
  ([nvc_dns] [nvarchar](100) NULL
  ,[nvc_configuration] [nvarchar](4000) NULL)
END
