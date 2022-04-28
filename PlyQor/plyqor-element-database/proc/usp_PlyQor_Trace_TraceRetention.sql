CREATE PROCEDURE [dbo].[usp_PlyQor_Trace_TraceRetention]

@i_days int

AS

BEGIN

DELETE
FROM [dbo].[tbl_PlyQor_Trace]
WHERE [dt_timestamp] < DATEADD(day,@i_days,GETDATE())

END
