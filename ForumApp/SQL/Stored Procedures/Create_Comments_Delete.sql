USE [Forum]
GO

/****** Object:  StoredProcedure [dbo].[Comments_Delete]    Script Date: 28-01-2020 10:25:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Comments_Delete]
	@Id int
AS
BEGIN
	SET NOCOUNT ON;

    DELETE FROM Comments WHERE id = @Id
END
GO

