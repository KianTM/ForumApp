USE [Forum]
GO

/****** Object:  StoredProcedure [dbo].[Comments_Update]    Script Date: 28-01-2020 10:26:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Comments_Update]
	@Id int,
	@Text nvarchar(MAX),
	@CensoredText nvarchar(MAX)
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE Comments
	SET text = @Text, censoredText = @CensoredText, isEdited = 1
	WHERE id = @Id
END
GO

