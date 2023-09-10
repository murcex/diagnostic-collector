CREATE PROCEDURE [dbo].[usp_KirokuG2_Instance_Insert]

@dt_session smalldatetime
,@nvc_id nvarchar(50)
,@nvc_source nvarchar(50)
,@nvc_function nvarchar(50)
,@i_duration int
,@i_errors int

AS

BEGIN

INSERT INTO [dbo].[tbl_KirokuG2_Instance]
([dt_session]
,[nvc_id]
,[nvc_source]
,[nvc_function]
,[i_duration]
,[i_errors])
VALUES
(@dt_session
,@nvc_id
,@nvc_source
,@nvc_function
,@i_duration
,@i_errors)

END