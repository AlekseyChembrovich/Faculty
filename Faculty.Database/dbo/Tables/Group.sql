CREATE TABLE [dbo].[Group]
(
	[Id] INT NOT NULL IDENTITY (1, 1) PRIMARY KEY, 
    [Name] NVARCHAR(50) NULL, 
    [SpecialozationId] INT NULL,
    CONSTRAINT [FK_Group_Specialozation] FOREIGN KEY ([SpecialozationId]) REFERENCES [dbo].[Specialozation] ([Id]),
)
