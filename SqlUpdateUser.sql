USE [VortexDB]
GO
/****** Object:  StoredProcedure [dbo].[InsertUser]    Script Date: 19-12-2019 14:17:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateUser]
@Username VARCHAR(128) = NULL,
@Password VARCHAR(128) = NULL,
@NewUsername VARCHAR(128) = NULL,
@NewPassword VARCHAR(128) = NULL
AS
BEGIN
UPDATE [User]
SET [Username] = @NewUsername,
	[Password] = @NewPassword
WHERE
	[Username] = @Username 
	AND
	[Password] = @Password;
END