USE MinionsDB
--DECLARE @id INT = (SELECT COUNT(*) + 1 FROM Villains )
INSERT INTO Villains(Name,EvilnessFactor)
VALUES(@villainName,'evil')

