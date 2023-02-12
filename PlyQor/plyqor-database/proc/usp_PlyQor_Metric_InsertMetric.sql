CREATE PROCEDURE [dbo].[usp_PlyQor_Metric_InsertMetric]

@nvc_container nvarchar(20)
,@nvc_metrictype nvarchar(20)
,@nvc_metric nvarchar(20)
,@i_value int

AS

BEGIN

DECLARE @dt_timestamp smalldatetime;

SET @dt_timestamp = DATEADD(DAY,-1,CAST(GETUTCDATE() AS DATE))

INSERT INTO [dbo].[tbl_PlyQor_Metric]
(
[dt_timestamp]
,[nvc_container]
,[nvc_metrictype]
,[nvc_metric]
,[i_value]
)
VALUES
(
@dt_timestamp
,@nvc_container
,@nvc_metrictype
,@nvc_metric
,@i_value
)

END