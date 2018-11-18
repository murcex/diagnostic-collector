CREATE PROCEDURE [dbo].[usp_Plycur_MyApp_Get]
(
    @ui_key [uniqueidentifier]
)

AS

SELECT [dt_created]
      ,[dt_updated]
      ,[ui_key]
      ,[i_status]
      ,[nvc_value]
  FROM [dbo].[tbl_Plycur_MyApp]
  WHERE [ui_key] = @ui_key 


