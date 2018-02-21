CREATE VIEW [dbo].[vw_Sensor_Fabric] AS
SELECT
[nvc_sensor] AS [Sensor]
,[nvc_region] AS [Region]
,[nvc_location] AS [Location]
FROM [dbo].[tbl_Sensor_Fabric]
