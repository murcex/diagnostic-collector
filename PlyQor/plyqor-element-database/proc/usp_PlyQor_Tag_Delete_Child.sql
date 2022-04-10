CREATE PROCEDURE [dbo].[usp_PlyQor_Tag_Delete_Child]

@nvc_container nvarchar(20)
,@nvc_id nvarchar(50)
,@nvc_data nvarchar(50)

AS

BEGIN

DELETE
FROM [dbo].[tbl_PlyQor_Tag]
WHERE [nvc_container] = @nvc_container
AND [nvc_id] = @nvc_id
AND [nvc_data] = @nvc_data

END
