CREATE PROCEDURE [dbo].[usp_PlyQor_Trace_Insert]

@dt_timestamp smalldatetime
,@nvc_version nvarchar(10)
,@nvc_id nvarchar(50)
,@nvc_operation nvarchar(20)
,@nvc_code nvarchar(10)
,@nvc_status nvarchar(20)
,@i_duration int

AS

BEGIN
INSERT INTO [dbo].[tbl_PlyQor_Trace]
           ([dt_timestamp]
		   ,[nvc_version]
		   ,[nvc_id]
           ,[nvc_operation]
		   ,[nvc_code]
		   ,[nvc_status]
		   ,[i_duration])
     VALUES
           (@dt_timestamp
		   ,@nvc_version
           ,@nvc_id
           ,@nvc_operation
		   ,@nvc_code
		   ,@nvc_status
		   ,@i_duration)
END
