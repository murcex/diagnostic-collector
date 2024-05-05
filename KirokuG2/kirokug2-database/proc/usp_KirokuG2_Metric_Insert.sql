CREATE PROCEDURE [dbo].[usp_KirokuG2_Metric_Insert]

@dt_session smalldatetime
,@nvc_id nvarchar(50)
,@nvc_source nvarchar(50)
,@nvc_function nvarchar(50)
,@i_type int
,@nvc_key nvarchar(20)
,@nvc_value nvarchar(20)


AS

BEGIN

INSERT INTO [dbo].[tbl_KirokuG2_Metric]
([dt_session]
,[nvc_id]
,[nvc_source]
,[nvc_function]
,[i_type]
,[nvc_key]
,[nvc_value])
VALUES
(@dt_session
,@nvc_id
,@nvc_source
,@nvc_function
,@i_type
,@nvc_key
,@nvc_value)

END