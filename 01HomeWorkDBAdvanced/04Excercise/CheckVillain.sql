USE MinionsDB
SELECT v.Id
  FROM Villains AS v
WHERE v.Name = @villainName