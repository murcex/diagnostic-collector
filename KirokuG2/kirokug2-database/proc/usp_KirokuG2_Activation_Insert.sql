CREATE PROCEDURE [dbo].[usp_KirokuG2_Activation_Insert]

@dt_session smalldatetime
,@nvc_id nvarchar(50)
,@nvc_source nvarchar(50)

AS

BEGIN

INSERT INTO [dbo].[tbl_KirokuG2_Activation]
([dt_session]
,[nvc_id]
,[nvc_source])
VALUES
(@dt_session
,@nvc_id
,@nvc_source)

END
GO