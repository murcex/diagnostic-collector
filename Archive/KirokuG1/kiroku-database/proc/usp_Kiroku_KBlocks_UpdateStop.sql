CREATE PROCEDURE [dbo].[usp_Kiroku_KBlocks_UpdateStop]
	(
		@ui_blockid [uniqueidentifier],
		@dt_blockstop [datetime]
	)
AS
	SET NOCOUNT ON
	SET XACT_ABORT ON
	
	BEGIN TRANSACTION
		UPDATE dbo.tbl_Kiroku_KBlocks
		SET dt_blockstop = @dt_blockstop
		WHERE ui_blockid = @ui_blockid
	COMMIT

