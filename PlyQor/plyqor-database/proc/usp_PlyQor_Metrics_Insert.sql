CREATE PROCEDURE [dbo].[usp_PlyQor_Metrics_Insert]

@dt_timestamp smalldatetime
,@nvc_container nvarchar(20)
,@i_type int
,@nvc_key nvarchar(50)
,@i_value int

AS

BEGIN

INSERT INTO [dbo].[tbl_PlyQor_Metrics]
([dt_timestamp]
,[nvc_container]
,[i_type]
,[nvc_key]
,[i_value])
VALUES
(@dt_timestamp
,@nvc_container
,@i_type
,@nvc_key
,@i_value)

END
