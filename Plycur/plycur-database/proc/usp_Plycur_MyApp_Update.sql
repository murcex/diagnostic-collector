CREATE PROCEDURE [dbo].[usp_Plycur_MyApp_Update]
    (
    @dt_created [smalldatetime],
    --@dt_updated [smalldatetime],
    @ui_key [uniqueidentifier],
    --@i_status [int],
    @nvc_value [nvarchar](250)
)

AS

UPDATE [dbo].[tbl_Plycur_MyApp]
   SET --[dt_created] = @dt_created,
      [dt_updated] = @dt_updated
      --,[i_status] = @i_status
      ,[nvc_value] = @nvc_value
 WHERE [ui_key] = @ui_key


