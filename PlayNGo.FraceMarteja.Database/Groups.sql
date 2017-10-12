CREATE TABLE [dbo].[Groups]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Round_Id] INT,
	[Hand_Id] INT,
	[IsWinner] BIT DEFAULT 0,
	[CreatedBy] NVARCHAR(50) NOT NULL DEFAULT 'system', 
    [CreatedDate] DATETIME NOT NULL DEFAULT GETUTCDATE(),
	[ModifiedBy] NVARCHAR(50) NULL, 
    [ModifiedDate] DATETIME NULL, 
    [Deleted] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_Groups_Round] FOREIGN KEY ([Round_Id]) REFERENCES [Rounds]([Id]),
	CONSTRAINT [FK_Groups_Hand] FOREIGN KEY ([Hand_Id]) REFERENCES [Hands]([Id]),
)
