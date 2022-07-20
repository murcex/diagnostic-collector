CREATE PROCEDURE [dbo].[usp_Kiroku_KInstances_UpdateStop]
	(
		@ui_instanceid [uniqueidentifier],
		@dt_instancestop [datetime]
	)
AS
	SET NOCOUNT ON
	SET XACT_ABORT ON
	
	BEGIN TRANSACTION
		UPDATE dbo.tbl_Kiroku_KInstances
		SET   
		dt_instancestop = @dt_instancestop 
		WHERE ui_instanceid = @ui_instanceid
	COMMIT