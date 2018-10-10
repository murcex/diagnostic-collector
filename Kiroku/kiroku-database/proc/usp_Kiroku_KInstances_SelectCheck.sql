CREATE PROCEDURE [dbo].[usp_Kiroku_KInstances_SelectCheck]
		@ui_instanceid [uniqueidentifier]	
AS
	SET NOCOUNT ON
	SET XACT_ABORT ON
	
	BEGIN TRANSACTION

	SELECT TOP 1 ui_instanceid
	FROM dbo.tlb_Kiroku_KInstances
	WHERE ui_instanceid = @ui_instanceid

	COMMIT

