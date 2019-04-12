CREATE PROCEDURE [dbo].[usp_Plycur_App_Delete]
(
      @nvc_key [nvarchar](50)
)

AS

DELETE 
FROM [dbo].[tbl_Plycur_App]
WHERE [nvc_key] = @nvc_key