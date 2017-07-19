USE MinionsDB
SELECT m.Id
FROM Minions AS m
WHERE m.Name = @minionName