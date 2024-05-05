CREATE PROCEDURE [dbo].[usp_KirokuG2_Quarantine_Insert]

@dt_session smalldatetime
,@nvc_id nvarchar(50)

AS

BEGIN

INSERT INTO [dbo].[tbl_KirokuG2_Quarantine]
([dt_session]
,[nvc_id])
VALUES
(@dt_session
,@nvc_id)

END