CREATE PROCEDURE [dbo].[usp_PlyQor_Tag_SelectTagCount]

@nvc_container nvarchar(20)
,@nvc_data nvarchar(50)

AS

BEGIN

SELECT COUNT([nvc_data]) AS [i_count]
FROM [dbo].[tbl_PlyQor_Tag]
WHERE [nvc_container] = @nvc_container
AND [nvc_data] = @nvc_data

END

