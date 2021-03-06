﻿CREATE TABLE [dbo].[Rounds]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Description] NVARCHAR(150) NOT NULL,
	[CreatedBy] NVARCHAR(50) NOT NULL DEFAULT 'system', 
    [CreatedDate] DATETIME NOT NULL DEFAULT GETUTCDATE(),
	[ModifiedBy] NVARCHAR(50) NULL, 
    [ModifiedDate] DATETIME NULL, 
    [Deleted] BIT NOT NULL DEFAULT 0,
)
