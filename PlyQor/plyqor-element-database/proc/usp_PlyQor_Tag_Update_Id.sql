CREATE PROCEDURE [dbo].[usp_PlyQor_Tag_Update_Id]

@nvc_container nvarchar(20)
,@nvc_old_id nvarchar(50)
,@nvc_new_id nvarchar(50)

AS

BEGIN

UPDATE [dbo].[tbl_PlyQor_Tag]
SET [nvc_id] = @nvc_new_id
WHERE [nvc_container] = @nvc_container
AND [nvc_id] = @nvc_old_id

END

