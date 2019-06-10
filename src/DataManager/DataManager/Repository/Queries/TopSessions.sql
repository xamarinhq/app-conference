select          s.Id
,				s.Title
,               COUNT(1) as [Favorites]
from            dbo.Sessions s
inner join      dbo.Favorites f
        on      f.SessionId = s.Id
where           (f.Deleted != 1 or f.Deleted is null)
  and           (s.Deleted != 1 or s.Deleted is null)
group by        s.Id, s.Title
union all
select          s.Id
,				s.Title
,               0 as [Favorites]
from            dbo.Sessions s
where not exists (select 1
                  from   dbo.Favorites f
				  where  f.SessionId = s.Id)
order by        3 desc
