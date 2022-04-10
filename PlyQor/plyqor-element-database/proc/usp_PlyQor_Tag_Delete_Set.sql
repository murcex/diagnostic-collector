CREATE PROCEDURE [dbo].[usp_PlyQor_Tag_Delete_Set]

@nvc_container nvarchar(20)
,@nvc_data nvarchar(50)

AS

BEGIN

DELETE
FROM [dbo].[tbl_PlyQor_Tag]
WHERE [nvc_container] = @nvc_container
AND [nvc_data] = @nvc_data

END

