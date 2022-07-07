CREATE PROCEDURE [dbo].[usp_Kiroku_Retention]

@i_days int

AS

BEGIN
  
  DELETE
  FROM [dbo].[tbl_Kiroku_KBlocks]
  WHERE [dt_blockstart] < DATEADD(day,@i_days,GETDATE())

  DELETE
  FROM [dbo].[tbl_Kiroku_KInstances]
  WHERE [dt_instancestart] < DATEADD(day,@i_days,GETDATE())

  DELETE
  FROM [dbo].[tbl_Kiroku_KLogs]
  WHERE [dt_event] < DATEADD(day,@i_days,GETDATE())

  DELETE
  FROM [dbo].[tbl_Kiroku_KMetrics]
  WHERE [dt_event] < DATEADD(day,@i_days,GETDATE())

  DELETE
  FROM [dbo].[tbl_Kiroku_KResults]
  WHERE [dt_event] < DATEADD(day,@i_days,GETDATE())

END