CREATE PROCEDURE [dbo].[usp_PlyQor_Data_Select_Retention]

AS

BEGIN

SELECT [nvc_id]
  FROM [dbo].[tbl_PlyQor_Data]
  WHERE [dt_timestamp] < DATEADD(day,-1,GETDATE())
  AND [nvc_collection] != 'SYSTEM'

END
