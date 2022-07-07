CREATE VIEW [dbo].[vw_Kiroku_Instance]

AS

SELECT 
a.[ui_instanceid] AS [Instance]
,[dt_instancestart] AS [Instance Session]
,[dt_instancestop] AS [Instance End]
,[i_duration] AS [Duration]
,[ui_applicationid] AS [Application]
,[nvc_trackid] AS [Track]
,[nvc_regionid] AS [Region]
,[nvc_clusterid] AS [Cluster]
,[nvc_deviceid] AS [Device]
,[nvc_klogversion] AS [KLog Version]
,(b.[Event Count]) AS [Errors]
,(c.[Event Count]) AS [Warnings]
FROM [tbl_Kiroku_KInstances] a

LEFT OUTER JOIN [vw_Kiroku_Base_InstanceErrors] b
ON a.[ui_instanceid] = b.[Instance]

LEFT OUTER JOIN [vw_Kiroku_Base_InstanceWarning] c
ON a.[ui_instanceid] = c.[Instance]