CREATE VIEW [dbo].[vw_Kiroku_Base_BlockError]

AS

SELECT 
a.[ui_blockid]
,COUNT(b.[nvc_logtype]) AS [Errors]
FROM [tbl_Kiroku_KBlocks] a

LEFT OUTER JOIN [tbl_Kiroku_KLogs] b
ON a.[ui_blockid] = b.[ui_blockid]

WHERE b.[nvc_logtype] = 'Error'
GROUP BY b.[nvc_logtype], a.[ui_blockid]