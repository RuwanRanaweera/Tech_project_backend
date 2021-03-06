USE [gemDB]
GO
/****** Object:  StoredProcedure [dbo].[SelectGem]    Script Date: 05-Apr-21 13:02:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[SelectGems]

AS
BEGIN
    SET NOCOUNT ON;
	SET XACT_ABORT,
	QUOTED_IDENTIFIER,
	ARITHABORT,
	ANSI_NULLS,
	ANSI_PADDING,
	ANSI_WARNINGS,
	CONCAT_NULL_YIELDS_NULL ON;
	SET NUMERIC_ROUNDABORT OFF;
  
    DECLARE @sErrorProcedure	VARCHAR(200),
			@sLog				VARCHAR(500),
			@sErrorMessage		VARCHAR(500)

    BEGIN TRY
		SELECT	*
		FROM	gems
		
	
	END TRY
	
	BEGIN CATCH

		DECLARE @iErrorNumber INT

		SELECT	@sErrorProcedure=ERROR_PROCEDURE()
		SELECT	@sErrorMessage=ERROR_MESSAGE()
		SELECT	@iErrorNumber=ERROR_NUMBER()

		RAISERROR (@sErrorMessage,16,1)

		RETURN 0
  
    END CATCH  
END