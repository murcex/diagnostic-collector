CREATE PROCEDURE [dbo].[usp_PlyQor_Tag_List_Distinct]

@nvc_container nvarchar(20)

AS

BEGIN

SELECT DISTINCT([nvc_data])
FROM [dbo].[tbl_PlyQor_Tag]
WHERE [nvc_container] = @nvc_container

END

