USE VortexDB
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE InsertUser
@Username VARCHAR(128) = NULL,
@Password VARCHAR(128) = NULL
AS
BEGIN
INSERT INTO [User]([Username], [Password]) VALUES(@Username, @Password);
END