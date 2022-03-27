CREATE PROCEDURE [dbo].[usp_PlyQor_Tag_Delete_Child]

@nvc_collection nvarchar(20)
,@nvc_id nvarchar(50)
,@nvc_data nvarchar(50)

AS

BEGIN

DELETE
  FROM [dbo].[tbl_PlyQor_Tag]
  WHERE [nvc_collection] = @nvc_collection
  AND [nvc_id] = @nvc_id
  AND [nvc_data] = @nvc_data

END
