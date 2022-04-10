CREATE PROCEDURE [dbo].[usp_PlyQor_Data_Update_Data]

@nvc_container nvarchar(20)
,@nvc_id nvarchar(50)
,@nvc_data nvarchar(max)

AS

BEGIN

UPDATE [dbo].[tbl_PlyQor_Data]
SET [nvc_data] = @nvc_data
WHERE [nvc_container] = @nvc_container
AND [nvc_id] = @nvc_id

END

