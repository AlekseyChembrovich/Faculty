CREATE TABLE [dbo].[Faculty]
(
	[Id] INT NOT NULL IDENTITY (1, 1) PRIMARY KEY, 
    [StartDateAducation] DATE NULL, 
    [CountYearAducation] INT NULL, 
    [StudentId] INT NULL,
    CONSTRAINT [FK_Faculty_Student] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Student] ([Id])
)
