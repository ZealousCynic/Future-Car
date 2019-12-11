USE Master;
GO
BEGIN
CREATE DATABASE VortexDB;
END
GO
BEGIN
USE VortexDB;
END
GO
	CREATE TABLE [Vortex] (
		[VortexId] INT NOT NULL,
		[SerialNumber] VARCHAR(32),
		[Model] VARCHAR(64),
		PRIMARY KEY ([VortexId])
	);
	
	CREATE TABLE [User] (
	  [UserId] INT NOT NULL,
	  [Username] VARCHAR(128) NOT NULL,
	  [Password] VARCHAR(128) NOT NULL,
	  PRIMARY KEY ([UserId])
	);

	CREATE TABLE [Owner] (
	  [Id] INT NOT NULL,
	  [FirstName] VARCHAR(64) NOT NULL,
	  [LastName] VARCHAR(64),
	  PRIMARY KEY ([Id])
	);
GO

	CREATE TABLE [VortexRegistration] (
		[VortexId] INT NOT NULL,
		[Registration] VARCHAR(8)
		FOREIGN KEY (VortexId) REFERENCES Vortex(VortexId)
		ON DELETE CASCADE 
		ON UPDATE CASCADE,
		CONSTRAINT PK_VortexId PRIMARY KEY(VortexId)
	);

	CREATE TABLE [PowerLog] (
	  [VortexId] INT NOT NULL,
	  [Time] DATETIME NOT NULL,
	  [Power] BIT NULL,
	  FOREIGN KEY (VortexId) REFERENCES Vortex(VortexId)
	  ON DELETE CASCADE
	  ON UPDATE CASCADE,
	  CONSTRAINT PK_VortexId_Time  PRIMARY KEY ([Time], VortexId)
	);

	CREATE TABLE [VortexOwner] (
	  [VortexId] INT NOT NULL,
	  [OwnerId] INT NOT NULL
	  FOREIGN KEY (VortexId) REFERENCES Vortex(VortexId)
	  ON DELETE CASCADE
	  ON UPDATE CASCADE,
	  FOREIGN KEY (OwnerId) REFERENCES Owner(Id)
	  ON DELETE CASCADE
	  ON UPDATE CASCADE,
	  CONSTRAINT PK_VortexId_OwnerId PRIMARY KEY (VortexId, OwnerId)
	);

	CREATE TABLE [StateLog] (
	  [VortexId] INT NOT NULL,
	  [Time] DATETIME NOT NULL,
	  [State] BIT,
	  FOREIGN KEY (VortexId) REFERENCES Vortex(VortexId)
	  ON DELETE CASCADE
	  ON UPDATE CASCADE,
	  CONSTRAINT PK_VortexId_Time_StateLog PRIMARY KEY (VortexId, [Time])
	);

	CREATE TABLE [SessionKeys] (
	  [UserId] INT NOT NULL,
	  [VortexId] INT NOT NULL,
	  [SessionKey] INT NOT NULL
	  FOREIGN KEY (UserId) REFERENCES [User](UserId)
	  ON DELETE CASCADE
	  ON UPDATE CASCADE,
	  FOREIGN KEY (VortexId) REFERENCES Vortex(VortexId)
	  ON DELETE CASCADE
	  ON UPDATE CASCADE,
	  CONSTRAINT PK_UserId_VortexId_SesKey PRIMARY KEY (UserId, VortexId)
	);

	CREATE TABLE [WaterLevel] (
	  [VortexId] INT NOT NULL,
	  [Time] DATETIME NOT NULL,
	  [WaterLevelReading] SMALLINT NOT NULL,
	  FOREIGN KEY (VortexId) REFERENCES Vortex(VortexId)
	  ON DELETE CASCADE
	  ON UPDATE CASCADE,
	  CONSTRAINT PK_VortexId_Time_WLevel PRIMARY KEY (VortexId, [Time])
	);

	CREATE TABLE [Temperature] (
	  [VortexId] INT NOT NULL,
	  [Time] DATETIME NOT NULL,
	  [Temperature] SMALLINT NOT NULL,
	  FOREIGN KEY (VortexId) REFERENCES Vortex(VortexId)
	  ON DELETE CASCADE
	  ON UPDATE CASCADE,
	  CONSTRAINT PK_VortexId_Time_Temp PRIMARY KEY (VortexId, [Time])	
	  );

	CREATE TABLE [Compas] (
	  [VortexId] INT NOT NULL,
	  [Time] DATETIME NOT NULL,
	  [CompasReading] SMALLINT NOT NULL,
	  FOREIGN KEY (VortexId) REFERENCES Vortex(VortexId)
	  ON DELETE CASCADE
	  ON UPDATE CASCADE,
	  CONSTRAINT PK_VortexId_Time_Compas PRIMARY KEY (VortexId, [Time])
	);
	
	CREATE TABLE [VortexUser] (
	  [UserId] INT NOT NULL,
	  [VortexId] INT NOT NULL
	  FOREIGN KEY (UserId) REFERENCES [User](UserId)
	  ON DELETE CASCADE
	  ON UPDATE CASCADE,
	  FOREIGN KEY (VortexId) REFERENCES Vortex(VortexId)
	  ON DELETE CASCADE
	  ON UPDATE CASCADE,
	  CONSTRAINT PK_UserId_VortexId_VUser PRIMARY KEY (VortexId, UserId)
	);
GO