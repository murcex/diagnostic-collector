CREATE PROCEDURE [dbo].[usp_DNS_Catalog_Select]

AS

BEGIN
  SELECT
  [nvc_dns]
  ,[nvc_configuration]
  FROM [dbo].[tbl_DNS_Catalog]
END
