 SELECT v.Name, COUNT(mv.MinionId) AS MinionsCount
  FROM Villains AS v
 INNER JOIN MinionsVillains AS mv 
	ON v.Id = mv.VillainsId
 GROUP BY v.Name
HAVING COUNT(mv.MinionId) > 3
 ORDER BY MinionsCount DESC