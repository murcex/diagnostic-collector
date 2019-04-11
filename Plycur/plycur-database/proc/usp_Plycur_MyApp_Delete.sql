CREATE PROCEDURE [dbo].[usp_Plycur_MyApp_Delete]
(
      @nvc_key [nvarchar](50)
)

AS

DELETE 
FROM [dbo].[tbl_Plycur_MyApp]
WHERE [nvc_key] = @nvc_key