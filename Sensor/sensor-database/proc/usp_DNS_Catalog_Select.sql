--IF (OBJECT_ID(N'[dbo].[usp_Article_DNS_Select]', 'P') IS NULL)
--BEGIN
  CREATE PROCEDURE [dbo].[usp_DNS_Catalog_Select]

  AS

  BEGIN
    SELECT
    [nvc_dns]
    ,[nvc_configuration]
    FROM [dbo].[tbl_DNS_Catalog]
  END
--END
