CREATE PROCEDURE [dbo].[usp_DNS_Stage_Retention]

AS

BEGIN
  DELETE
  FROM [dbo].[tbl_DNS_Stage]
  WHERE [dt_session] < DATEADD(day,-7,GETDATE())
END
