CREATE PROCEDURE [dbo].[usp_PlyQor_Data_UpsertSystem]

@dt_timestamp smalldatetime
,@nvc_id nvarchar(50)
,@nvc_data nvarchar(max)

AS

UPDATE [dbo].[tbl_PlyQor_Data] 
WITH (UPDLOCK, SERIALIZABLE) 
SET [nvc_data] = @nvc_data 
WHERE [nvc_container] = 'SYSTEM'
AND [nvc_id] = @nvc_id

IF @@ROWCOUNT = 0

BEGIN

INSERT INTO [dbo].[tbl_PlyQor_Data]
([dt_timestamp]
,[nvc_container]
,[nvc_id]
,[nvc_data])
VALUES
(@dt_timestamp
,'SYSTEM'
,@nvc_id
,@nvc_data)

END
GO

