CREATE TABLE [dbo].[Faculties]
(
	[Id] INT NOT NULL IDENTITY (1, 1) PRIMARY KEY, 
    [StartDateEducation] DATE NULL, 
    [CountYearEducation] INT NULL, 
    [StudentId] INT NULL,
    [GroupId] INT NULL, 
    [CuratorId] INT NULL, 
    CONSTRAINT [FK_Faculty_Student] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([Id]),
    CONSTRAINT [FK_Faculty_Group] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Groups] ([Id]),
    CONSTRAINT [FK_Faculty_Curator] FOREIGN KEY ([CuratorId]) REFERENCES [dbo].[Curators] ([Id])
)
