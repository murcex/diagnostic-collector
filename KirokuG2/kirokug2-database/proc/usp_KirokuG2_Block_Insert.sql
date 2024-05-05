CREATE PROCEDURE [dbo].[usp_KirokuG2_Block_Insert]

@dt_session smalldatetime
,@nvc_id nvarchar(50)
,@nvc_tag nvarchar(50)
,@nvc_source nvarchar(50)
,@nvc_function nvarchar(50)
,@nvc_name nvarchar(50)
,@i_duration int

AS

BEGIN

INSERT INTO [dbo].[tbl_KirokuG2_Block]
([dt_session]
,[nvc_id]
,[nvc_tag]
,[nvc_source]
,[nvc_function]
,[nvc_name]
,[i_duration])
VALUES
(@dt_session
,@nvc_id
,@nvc_tag
,@nvc_source
,@nvc_function
,@nvc_name
,@i_duration)

END