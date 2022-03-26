CREATE PROCEDURE [dbo].[usp_Plycur_App_Verify]
(
      @nvc_key [nvarchar](50)
)

AS

SELECT [nvc_key]
FROM [dbo].[tbl_Plycur_App]
WHERE [nvc_key] = @nvc_key