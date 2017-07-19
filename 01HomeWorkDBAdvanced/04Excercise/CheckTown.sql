USE MinionsDB
SELECT t.Id
  FROM Towns AS t
 WHERE t.Name = @townName