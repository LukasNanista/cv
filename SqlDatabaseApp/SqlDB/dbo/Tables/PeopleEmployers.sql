CREATE TABLE [dbo].[PeopleEmployers]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PersonId] INT NOT NULL, 
    [EmployerId] INT NOT NULL
)
