USE [Forum]
GO

/****** Object:  StoredProcedure [dbo].[Comments_Insert]    Script Date: 28-01-2020 10:26:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Comments_Insert]
	@Text nvarchar(MAX),
	@CensoredText nvarchar(MAX),
	@PostTime datetime
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO Comments (text, censoredText, postTime, isEdited)
	VALUES (@Text, @CensoredText, @PostTime, 0)
END
GO

