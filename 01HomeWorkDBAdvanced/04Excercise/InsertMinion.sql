USE MinionsDB
DECLARE @id INT = (SELECT COUNT(*) + 1 FROM Minions)

INSERT INTO Minions(Id,Name,Age,TownId)
SELECT @id AS id,
	   @minionName AS Name,
	   @Age AS Age,
	   t.Id AS TownId
 FROM Towns AS t
WHERE t.Name = @townName