GO
USE [Faculty.Database]

GO
INSERT INTO [dbo].[Student] ([dbo].[Student].[Surname], [dbo].[Student].[Name], [dbo].[Student].[Doublename])
	VALUES ('test1', 'test1', 'test1'),
		   ('test2', 'test2', 'test3'),
		   ('test1', 'test2', 'test3')

GO
INSERT INTO [dbo].[Curator] 
	([dbo].[Curator].[Surname], [dbo].[Curator].[Name], [dbo].[Curator].[Doublename], [dbo].[Curator].[Phone]) 
		VALUES ('test1', 'test2', 'test3', '+375-33-111-11-11'), 
			   ('test1', 'test2', 'test3', '+375-33-222-22-22'),
			   ('test1', 'test2', 'test3', '+375-33-333-33-33')

GO
INSERT INTO [dbo].[Specialozation] ([dbo].[Specialozation].[Name])
	VALUES ('test1'), ('test2'), ('test3')

GO
INSERT INTO [dbo].[Group] ([dbo].[Group].[Name], [dbo].[Group].[SpecialozationId]) 
		VALUES ('test1', 1), ('test2', 2), ('test3', 3)

GO
INSERT INTO [dbo].[Faculty] 
	([dbo].[Faculty].[StartDateAducation], [dbo].[Faculty].[CountYearAducation],
	 [dbo].[Faculty].[StudentId], [dbo].[Faculty].[GroupId], [dbo].[Faculty].[CuratorId]) 
		VALUES ('2021-09-01', 5, 1, 1, 1), ('2021-09-01', 4, 2, 2, 2), ('2021-09-01', 5, 3, 3, 3)