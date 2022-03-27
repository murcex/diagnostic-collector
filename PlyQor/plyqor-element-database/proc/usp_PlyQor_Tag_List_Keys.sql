CREATE PROCEDURE [dbo].[usp_PlyQor_Tag_List_Keys]

@top int
,@nvc_collection nvarchar(20)
,@nvc_data nvarchar(50)

AS

BEGIN

SELECT TOP (@top) [nvc_id]
  FROM [dbo].[tbl_PlyQor_Tag]
  WHERE [nvc_collection] = @nvc_collection
  AND [nvc_data] = @nvc_data

END

