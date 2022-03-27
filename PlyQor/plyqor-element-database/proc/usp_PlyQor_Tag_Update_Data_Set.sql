CREATE PROCEDURE [dbo].[usp_PlyQor_Tag_Update_Data_Set]

@nvc_collection nvarchar(20)
,@nvc_old_data nvarchar(50)
,@nvc_new_data nvarchar(50)

AS

BEGIN

UPDATE [dbo].[tbl_PlyQor_Tag]
  SET [nvc_data] = @nvc_new_data
  WHERE [nvc_collection] = @nvc_collection
  AND [nvc_data] = @nvc_old_data

END

