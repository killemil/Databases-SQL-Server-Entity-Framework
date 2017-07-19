USE MinionsDB
INSERT INTO MinionsVillains(MinionId,VillainsId)
SELECT m.Id , v.Id
  FROM Minions AS m, Villains AS v
 WHERE m.Name = @minionName
	AND v.Name = @villainName
