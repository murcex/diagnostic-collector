CREATE PROCEDURE [dbo].[usp_Plycur_App_Insert]
(
      @dt_created [smalldatetime],
      @dt_updated [smalldatetime],
      @i_status [int],
      @nvc_key [nvarchar](50),
      @nvc_value [nvarchar](250)
)

AS

INSERT 
INTO [dbo].[tbl_Plycur_App]
(
      [dt_created]
      ,[dt_updated]
      ,[i_status]
      ,[nvc_key]
      ,[nvc_value]
)
VALUES
(
      @dt_created,
      @dt_updated,
      @i_status,
      @nvc_key,
      @nvc_value
)