USE MinionsDB
DECLARE @id INT = ( SELECT COUNT(*) + 1 FROM Towns )
INSERT INTO Towns(Id,Name)
VALUES (@id,@townName)