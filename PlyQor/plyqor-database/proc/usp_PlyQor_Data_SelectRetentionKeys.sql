CREATE PROCEDURE [dbo].[usp_PlyQor_Data_SelectRetentionKeys]

@nvc_container nvarchar(20)
,@i_days int

AS

BEGIN

SELECT [nvc_id]
FROM [dbo].[tbl_PlyQor_Data]
WHERE [dt_timestamp] < DATEADD(day,@i_days,GETDATE())
AND [nvc_container] = @nvc_container

END

