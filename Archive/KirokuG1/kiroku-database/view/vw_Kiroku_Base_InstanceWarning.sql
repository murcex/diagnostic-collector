CREATE VIEW [dbo].[vw_Kiroku_Base_InstanceWarning]

AS

SELECT 
a.[dt_instancestart] AS [Instance Session]
,a.[ui_instanceid] AS [Instance]
,a.[ui_applicationid] AS [Application]
,c.[nvc_logtype] AS [Event Type]
,COUNT(c.[nvc_logtype]) AS [Event Count]
FROM [tbl_Kiroku_KInstances] a
  
-- join Instance to Block
JOIN [tbl_Kiroku_KBlocks] b
ON a.[ui_instanceid] = b.[ui_instanceid]

-- join Block to Log
JOIN [tbl_Kiroku_KLogs] c
ON b.[ui_blockid] = c.[ui_blockid]

WHERE [nvc_logtype] = 'Warning'
GROUP BY c.[nvc_logtype], a.[dt_instancestart], a.[ui_applicationid], a.[ui_instanceid]