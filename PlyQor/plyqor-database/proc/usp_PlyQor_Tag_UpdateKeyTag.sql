CREATE PROCEDURE [dbo].[usp_PlyQor_Tag_UpdateKeyTag]

@nvc_container nvarchar(20)
,@nvc_id nvarchar(50)
,@nvc_old_data nvarchar(50)
,@nvc_new_data nvarchar(50)

AS

BEGIN

UPDATE [dbo].[tbl_PlyQor_Tag]
SET [nvc_data] = @nvc_new_data
WHERE [nvc_container] = @nvc_container
AND [nvc_id] = @nvc_id
AND [nvc_data] = @nvc_old_data

END

