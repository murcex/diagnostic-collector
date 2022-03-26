CREATE PROCEDURE [dbo].[usp_Plycur_App_Select]
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
FROM [dbo].[tbl_Plycur_App]
WHERE [nvc_key] = @nvc_key