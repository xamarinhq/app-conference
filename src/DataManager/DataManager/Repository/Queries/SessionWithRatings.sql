select     s.Id
,		   s.Title
,          CONVERT(DECIMAL(2,1), AVG(f.SessionRating)) as [AverageRating]
,          [OneStar] = dbo.fn_NumberOfRatings(s.Id, 1)
,          [TwoStar] = dbo.fn_NumberOfRatings(s.Id, 2)
,          [ThreeStar] = dbo.fn_NumberOfRatings(s.Id, 3)
,          [FourStar] = dbo.fn_NumberOfRatings(s.Id, 4)
,          [FiveStar] = dbo.fn_NumberOfRatings(s.Id, 5)
from       dbo.Sessions s
inner join dbo.Feedbacks f
        on f.SessionId = s.Id
where      (f.Deleted != 1 or f.Deleted is null)
  and      (s.Deleted != 1 or s.Deleted is null)
group by   s.Id, s.Title
order by   3 desc