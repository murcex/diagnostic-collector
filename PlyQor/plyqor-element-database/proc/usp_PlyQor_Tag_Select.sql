CREATE PROCEDURE [dbo].[usp_PlyQor_Tag_Select]

@nvc_collection nvarchar(20)
,@nvc_id nvarchar(50)

AS

BEGIN

SELECT [nvc_data]
  FROM [dbo].[tbl_PlyQor_Tag]
  WHERE [nvc_collection] = @nvc_collection
  AND [nvc_id] = @nvc_id

END

