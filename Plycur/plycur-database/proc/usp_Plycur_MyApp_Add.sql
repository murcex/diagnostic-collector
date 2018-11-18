CREATE PROCEDURE [dbo].[usp_Plycur_MyApp_Add]
(
      @dt_created [smalldatetime],
      @dt_updated [smalldatetime],
      @ui_key [uniqueidentifier],
      @i_status [int],
      @nvc_value [nvarchar](250)
)

AS

INSERT 
      INTO [dbo].[tbl_Plycur_MyApp]
           ([dt_created]
           ,[dt_updated]
           ,[ui_key]
           ,[i_status]
           ,[nvc_value])
     VALUES
           (@dt_created,
            @dt_updated,
            @ui_key,
            @i_status,
            @nvc_value)