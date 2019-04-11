CREATE PROCEDURE [dbo].[usp_Plycur_MyApp_Verify]
(
      @nvc_key [nvarchar](50)
)

AS

SELECT [nvc_key]
FROM [Centrifuge].[dbo].[tbl_Plycur_MyApp]
WHERE [nvc_key] = @nvc_key