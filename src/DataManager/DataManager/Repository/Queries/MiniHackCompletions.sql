select c.UserId, c.CompletedAt, m.Name, m.Category, m.Score
from dbo.MiniHackCompletions c
inner join dbo.MiniHacks m
on c.HackId = m.Id
where m.Deleted <> 1
