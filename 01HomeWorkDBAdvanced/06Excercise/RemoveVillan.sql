DELETE FROM MinionsVillains
WHERE VillainsId = @villainId

DELETE FROM Villains
WHERE Id = @villainId