CREATE TABLE [dbo].[Curators]
(
	[Id] INT NOT NULL IDENTITY (1, 1) PRIMARY KEY, 
    [Surname] NVARCHAR(50) NULL, 
    [Name] NVARCHAR(50) NULL, 
    [Doublename] NVARCHAR(50) NULL, 
    [Phone] NVARCHAR(20) NULL
)
