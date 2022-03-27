CREATE PROCEDURE [dbo].[usp_PlyQor_Data_Update_Id]

@nvc_collection nvarchar(20)
,@nvc_old_id nvarchar(50)
,@nvc_new_id nvarchar(50)

AS

BEGIN

UPDATE [dbo].[tbl_PlyQor_Data]
  SET [nvc_id] = @nvc_new_id 
  WHERE [nvc_collection] = @nvc_collection
  AND [nvc_id] = @nvc_old_id

END

