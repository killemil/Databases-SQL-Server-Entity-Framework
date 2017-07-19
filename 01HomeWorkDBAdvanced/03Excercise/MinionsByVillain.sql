SELECT m.Name,m.Age
  FROM Minions AS m
 INNER JOIN MinionsVillains AS mv
	ON mv.MinionId = m.Id
 WHERE mv.VillainsId = @villainId