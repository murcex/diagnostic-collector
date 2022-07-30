CREATE PROCEDURE [dbo].[usp_PlyQor_Data_SelectRetentionKeys]

@nvc_container nvarchar(20)
,@i_top int
,@dt_threshold datetime

AS

BEGIN

SELECT TOP (@i_top) [nvc_id]
FROM [dbo].[tbl_PlyQor_Data]
WHERE [dt_timestamp] < @dt_threshold
AND [nvc_container] = @nvc_container

END

