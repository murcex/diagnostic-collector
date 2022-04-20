CREATE VIEW [dbo].[vw_PlyQor_Trace]

AS

SELECT [dt_timestamp] AS [Session]
,[nvc_container] AS [Container]
,[nvc_id] AS [Identity]
,[nvc_operation] AS [Operation]
,[nvc_code] AS [Code]
,[nvc_status] AS [Status]
,[i_duration] AS [Latency]
FROM [dbo].[tbl_PlyQor_Trace]