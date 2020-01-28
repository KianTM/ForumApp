USE [Forum]
GO

/****** Object:  StoredProcedure [dbo].[Comments_GetAllCommentsByPostTime]    Script Date: 28-01-2020 10:26:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Comments_GetAllCommentsByPostTime]
	
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * FROM Comments ORDER BY postTime DESC
END
GO

