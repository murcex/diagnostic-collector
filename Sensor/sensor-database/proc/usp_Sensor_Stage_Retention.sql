CREATE PROCEDURE [dbo].[usp_Sensor_Stage_Retention]

AS

BEGIN
  DELETE TOP (50)
  FROM [dbo].[tbl_Sensor_Stage]
  WHERE [dt_session] < DATEADD(day,-7,GETDATE())
END
