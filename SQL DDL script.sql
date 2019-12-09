CREATE TABLE [VortexRegistration] (
  [VortexId] int,
  [Registration] Varchar(8)
);

CREATE INDEX [PK, FK] ON  [VortexRegistration] ([VortexId]);

CREATE TABLE [User] (
  [UserId] int,
  [Username] Varchar(),
  [Password] Varchar(),
  PRIMARY KEY User(UserId)
);

CREATE TABLE [Vortex] (
  [VortexId] int,
  [SerialNumber] Varchar(32),
  [Model] Varchar(64),
  PRIMARY KEY ([VortexId])
);

CREATE TABLE [PowerLog] (
  [VortexId] int,
  [Time] datetime,
  [Power] bit,
  PRIMARY KEY ([Time])
);

CREATE INDEX [PK, FK] ON  [PowerLog] ([VortexId]);

CREATE TABLE [Owner] (
  [Id] int,
  [FirstName] Varchar(64),
  [LastName] Varchar(64),
  PRIMARY KEY ([Id])
);

CREATE TABLE [VortexOwner] (
  [VortexId] int,
  [OwnerId] Int
);

CREATE INDEX [PK, FK] ON  [VortexOwner] ([VortexId]);

CREATE INDEX [FK] ON  [VortexOwner] ([OwnerId]);

CREATE TABLE [StateLog] (
  [VortexId] int,
  [Time] datetime,
  [State] bit,
  PRIMARY KEY ([Time])
);

CREATE INDEX [PK, FK] ON  [StateLog] ([VortexId]);

CREATE TABLE [SessionKeys] (
  [UserId] int,
  [VortexId] Int,
  [SessionKey] int
);

CREATE INDEX [PK, FK] ON  [SessionKeys] ([UserId]);

CREATE INDEX [PF, FK] ON  [SessionKeys] ([VortexId]);

CREATE TABLE [WaterLevel] (
  [VortexId] int,
  [Time] datetime,
  [WaterLevelReading] smallint,
  PRIMARY KEY ([Time])
);

CREATE INDEX [PK, FK] ON  [WaterLevel] ([VortexId]);

CREATE TABLE [Temperature] (
  [VortexId] int,
  [Time] datetime,
  [Temperature] smallint,
  PRIMARY KEY ([Time])
);

CREATE INDEX [PK, FK] ON  [Temperature] ([VortexId]);

CREATE TABLE [Compas] (
  [VortexId] int,
  [Time] datetime,
  [CompasReading] smallint,
  PRIMARY KEY ([Time])
);

CREATE INDEX [PK, FK] ON  [Compas] ([VortexId]);

CREATE TABLE [VortexUser] (
  [UserId] int,
  [VortexId] Int
);

CREATE INDEX [PK, FK] ON  [VortexUser] ([UserId]);

CREATE INDEX [PF, FK] ON  [VortexUser] ([VortexId]);
