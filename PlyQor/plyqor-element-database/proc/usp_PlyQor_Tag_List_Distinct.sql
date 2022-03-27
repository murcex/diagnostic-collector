CREATE PROCEDURE [dbo].[usp_PlyQor_Tag_List_Distinct]

@nvc_collection nvarchar(20)

AS

BEGIN

SELECT DISTINCT([nvc_data])
  FROM [dbo].[tbl_PlyQor_Tag]
  WHERE [nvc_collection] = @nvc_collection

END

