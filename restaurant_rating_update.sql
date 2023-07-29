update Restaurant
set Restaurant.resRating = t.avg_rating
from 
(
  select Reviews.resName, avg(Reviews.revRating) as avg_rating
  from Reviews 
  group by Reviews.resName
) t
where t.resName = Restaurant.resName;
