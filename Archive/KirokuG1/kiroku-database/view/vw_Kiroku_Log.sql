CREATE VIEW [dbo].[vw_Kiroku_Log]

AS

SELECT 
[dt_event] AS [Timestamp]
,a.[nvc_blockname] AS [Block]
,[nvc_logtype] AS [Log Type]
,[nvc_logdata] AS [Log Data]
,c.[ui_applicationid] AS [Application]
FROM [tbl_Kiroku_KLogs] a

LEFT OUTER JOIN [tbl_Kiroku_KBlocks] b
ON a.[ui_blockid] = b.[ui_blockid]

LEFT OUTER JOIN [tbl_Kiroku_KInstances] c
ON b.[ui_instanceid] = c.[ui_instanceid]

WHERE a.[nvc_logtype] != 'Info'