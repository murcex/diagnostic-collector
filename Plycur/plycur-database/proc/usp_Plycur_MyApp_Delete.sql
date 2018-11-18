CREATE PROCEDURE [dbo].[usp_Plycur_MyApp_Delete]
(
      @ui_key [uniqueidentifier]
)

AS

DELETE 
      FROM [dbo].[tbl_Plycur_MyApp]
      WHERE [ui_key] = @ui_key
