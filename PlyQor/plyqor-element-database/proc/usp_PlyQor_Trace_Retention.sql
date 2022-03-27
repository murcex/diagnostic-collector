CREATE PROCEDURE [dbo].[usp_PlyQor_Trace_Retention]

@i_days int

AS

BEGIN

DELETE
  FROM [dbo].[tbl_PlyQor_Trace]
  WHERE [dt_timestamp] < DATEADD(day,@i_days,GETDATE())

END
