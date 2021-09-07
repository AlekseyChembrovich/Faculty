GO
USE [FacultyTest]

GO
INSERT INTO [dbo].[Students] ([dbo].[Students].[Surname], [dbo].[Students].[Name], [dbo].[Students].[Doublename])
	VALUES ('test1', 'test1', 'test1'),
		   ('test2', 'test2', 'test2'),
		   ('test3', 'test3', 'test3')

GO
INSERT INTO [dbo].[Curators] 
	([dbo].[Curators].[Surname], [dbo].[Curators].[Name], [dbo].[Curators].[Doublename], [dbo].[Curators].[Phone]) 
		VALUES ('test1', 'test1', 'test1', '+375-33-111-11-11'), 
			   ('test2', 'test2', 'test2', '+375-33-222-22-22'),
			   ('test3', 'test3', 'test3', '+375-33-333-33-33')

GO
INSERT INTO [dbo].[Specializations] ([dbo].[Specializations].[Name])
	VALUES ('test1'), ('test2'), ('test3')

GO
INSERT INTO [dbo].[Groups] ([dbo].[Groups].[Name], [dbo].[Groups].[SpecializationId]) 
		VALUES ('test1', 1), ('test2', 2), ('test3', 3)

GO
INSERT INTO [dbo].[Faculties] 
	([dbo].[Faculties].[StartDateEducation], [dbo].[Faculties].[CountYearEducation],
	 [dbo].[Faculties].[StudentId], [dbo].[Faculties].[GroupId], [dbo].[Faculties].[CuratorId]) 
		VALUES ('2021-09-01', 5, 1, 1, 1), ('2021-09-01', 4, 2, 2, 2), ('2021-09-01', 5, 3, 3, 3)