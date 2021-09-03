CREATE TABLE [dbo].[Groups]
(
	[Id] INT NOT NULL IDENTITY (1, 1) PRIMARY KEY, 
    [Name] NVARCHAR(50) NULL, 
    [SpecializationId] INT NULL,
    CONSTRAINT [FK_Group_Specialozation] FOREIGN KEY ([SpecializationId]) REFERENCES [dbo].[Specializations] ([Id]),
)
