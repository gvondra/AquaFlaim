CREATE TABLE [aut].[Role]
(
	[RoleId] INT IDENTITY(1,1) NOT NULL,
	[Name] VARCHAR(1024) NOT NULL,
	[PolicyName] VARCHAR(256) NOT NULL,
	[CreateTimestamp] DATETIME2(4) CONSTRAINT [DF_Role_CreateTimestamp] DEFAULT(SYSUTCDATETIME()) NOT NULL,
	[UpdateTimestamp] DATETIME2(4) CONSTRAINT [DF_Role_UpdateTimestamp] DEFAULT(SYSUTCDATETIME()) NOT NULL,
	CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED ([RoleId])
)

GO

CREATE UNIQUE INDEX [IX_Role_PolicyName] ON [aut].[Role] ([PolicyName])
