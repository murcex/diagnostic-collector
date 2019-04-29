CREATE PROCEDURE [dbo].[usp_Kiroku_KInstances_InsertStart]
	(
		@ui_instanceid [uniqueidentifier],
		@dt_instancestart [datetime],
		@ui_applicationid [nvarchar](20) NULL,
		@nvc_trackid [nvarchar](20) NULL,
		@nvc_regionid [nvarchar](20) NULL,
		@nvc_clusterid [nvarchar](20) NULL,
		@nvc_deviceid [nvarchar](20) NULL,
		@nvc_klogversion [nvarchar](20)
	)
AS
	SET NOCOUNT ON
	SET XACT_ABORT ON
	
	INSERT INTO dbo.tbl_Kiroku_KInstances
	(
		ui_instanceid, 
		dt_instancestart,
		ui_applicationid,
		nvc_trackid,
		nvc_regionid,
		nvc_clusterid,
		nvc_deviceid,
		nvc_klogversion
	)
	VALUES
	(
		@ui_instanceid,
		@dt_instancestart,
		@ui_applicationid,
		@nvc_trackid,
		@nvc_regionid,
		@nvc_clusterid,
		@nvc_deviceid,
		@nvc_klogversion
	)

