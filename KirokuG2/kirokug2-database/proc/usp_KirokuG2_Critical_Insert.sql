CREATE PROCEDURE [dbo].[usp_KirokuG2_Critical_Insert]

@dt_session smalldatetime
,@nvc_id nvarchar(50)
,@nvc_source nvarchar(50)
,@nvc_function nvarchar(50)
,@nvc_message nvarchar(250)

AS

BEGIN

INSERT INTO [dbo].[tbl_KirokuG2_Critical]
([dt_session]
,[nvc_id]
,[nvc_source]
,[nvc_function]
,[nvc_message])
VALUES
(@dt_session
,@nvc_id
,@nvc_source
,@nvc_function
,@nvc_message)

END