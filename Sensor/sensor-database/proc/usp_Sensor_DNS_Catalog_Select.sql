CREATE PROCEDURE [dbo].[usp_Sensor_DNS_Catalog_Select]

AS

BEGIN
  SELECT
  [nvc_dns]
  ,[nvc_configuration]
  FROM [dbo].[tbl_Sensor_DNS_Catalog]
END
