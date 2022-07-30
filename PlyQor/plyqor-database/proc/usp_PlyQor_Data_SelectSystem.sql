CREATE PROCEDURE [dbo].[usp_PlyQor_Data_SelectSystem]

@nvc_id nvarchar(50)

AS

BEGIN

SELECT [nvc_data]
FROM [dbo].[tbl_PlyQor_Data]
WHERE [nvc_container] = 'SYSTEM'
AND [nvc_id] = @nvc_id

END

GO

