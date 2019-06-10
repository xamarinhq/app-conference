if exists (select 1 from dbo.sysobjects where type = 'FN' and name = 'fn_NumberOfRatings')
begin
	drop function dbo.fn_NumberOfRatings
end
go

-- This function returns the number of people who gave a particular rating for a particular session
create function fn_NumberOfRatings
(
	@p_SessionId NVARCHAR(128)
,	@p_Rating INT
)
returns INT
as
begin
	declare @num INT

	select @num = count(1)
	from dbo.Feedbacks
	where SessionId = @p_SessionId
	  and SessionRating = @p_Rating
	  and Deleted = 0 or Deleted is null

	return @num
end
go

grant execute on dbo.fn_NumberOfRatings to public
go