CREATE PROCEDURE [dbo].[usp_Plycur_MyApp_Select]
(
      @nvc_key [nvarchar](50)
)

AS

SELECT 
[dt_created]
,[dt_updated]
,[i_status]
,[nvc_key]
,[nvc_value]
FROM [Centrifuge].[dbo].[tbl_Plycur_MyApp]
WHERE [nvc_key] = @nvc_key