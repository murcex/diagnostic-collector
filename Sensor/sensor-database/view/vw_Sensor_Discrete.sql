CREATE VIEW [dbo].[vw_Sensor_Discrete]

AS

SELECT
[Session]
,[DNS]
,[Data Center Tag]
,[Count]
,NULL AS [Trace Route]
,NULL [Latency]
FROM [dbo].[vw_Sensor_Base_Region]

UNION ALL

SELECT 
[Session]
,[DNS]
,NULL
,NULL
,[Trace Route]
,[Latency]
FROM [dbo].[vw_Sensor_Base_Latency]