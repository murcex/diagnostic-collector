CREATE VIEW [dbo].[vw_Kiroku_Block]

AS

SELECT 
a.[ui_instanceid] AS [Instance]
,a.[ui_blockid] AS [Block]
,a.[nvc_blockname] AS [Block Name]
,[dt_blockstart] AS [Block Session]
,[dt_blockstop] AS [Block End]
,a.[i_duration] AS [Duration]
,b.[ui_applicationid] AS [Application]
,c.[Errors]
,d.[Warnings]
FROM [tbl_Kiroku_KBlocks] a

JOIN [tbl_Kiroku_KInstances] b
ON a.[ui_instanceid] = b.[ui_instanceid]

LEFT OUTER JOIN [vw_Kiroku_Base_BlockError] c
ON a.[ui_blockid] = c.[ui_blockid]

LEFT OUTER JOIN [vw_Kiroku_Base_BlockWarning] d
ON a.[ui_blockid] = d.[ui_blockid]