UPDATE Minions
SET Age += 1
WHERE Id = @minionsId

UPDATE Minions
SET Name = CONCAT(UPPER(LEFT(Name,1)),SUBSTRING(Name,2,LEN(Name)))
WHERE Id = @minionsId