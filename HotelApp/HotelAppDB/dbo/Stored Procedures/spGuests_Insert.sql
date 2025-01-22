CREATE PROCEDURE [dbo].[spGuests_Insert]
	@firstName nvarchar(50),
	@lastName nvarchar(50)
AS
begin
	set nocount on;

	if not exists (select 1 from dbo.Guests where FirstName = @firstName and LastName = @lastName)
	begin
		insert into dbo.Guests (FirstName, LastName)
		values (@firstName, @lastName);
	end

	/*
		[] here denote (column) name, especially useful if column name is otherwise a reserved word
		seen mainly when sql generates the code, it puts brackets everywhere it can by default (like also on the first line here
		in procedure name)

		in regards to top 1 here - returns the first row in query,
		in this case these should not be more than one because of the filter above, but just in case
	*/
	select top 1 [Id], [FirstName], [LastName]
	from dbo.Guests
	where FirstName = @firstName and LastName = @lastName;
end