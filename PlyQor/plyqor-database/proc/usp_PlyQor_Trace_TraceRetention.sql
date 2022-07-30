CREATE PROCEDURE [dbo].[usp_PlyQor_Trace_TraceRetention]

@nvc_container nvarchar(20)
,@i_top int
,@dt_threshold datetime

AS

BEGIN

DELETE TOP (@i_top)
FROM [dbo].[tbl_PlyQor_Trace]
WHERE [dt_timestamp] < @dt_threshold
AND [nvc_container] = @nvc_container

END
