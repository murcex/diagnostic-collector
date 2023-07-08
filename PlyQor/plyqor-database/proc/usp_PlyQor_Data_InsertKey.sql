CREATE PROCEDURE [dbo].[usp_PlyQor_Data_InsertKey]

@dt_timestamp smalldatetime
,@nvc_container nvarchar(20)
,@nvc_id nvarchar(50)
,@nvc_data nvarchar(max)

AS

BEGIN

INSERT INTO [dbo].[tbl_PlyQor_Data]
([dt_timestamp]
,[nvc_container]
,[nvc_id]
,[nvc_data])
VALUES
(@dt_timestamp
,@nvc_container
,@nvc_id
,@nvc_data)

END
