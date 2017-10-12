CREATE TABLE [dbo].[PlayerGroups]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Group_Id] INT,
	[Player_Id] INT,
	[CreatedBy] NVARCHAR(50) NOT NULL DEFAULT 'system', 
    [CreatedDate] DATETIME NOT NULL DEFAULT GETUTCDATE(),
	[ModifiedBy] NVARCHAR(50) NULL, 
    [ModifiedDate] DATETIME NULL, 
    [Deleted] BIT NOT NULL DEFAULT 0,
	CONSTRAINT [FK_PlayerGroups_Group] FOREIGN KEY ([Group_Id]) REFERENCES [Groups]([Id]),
	CONSTRAINT [FK_PlayerGroups_Player] FOREIGN KEY ([Player_Id]) REFERENCES [Players]([Id]),
)
