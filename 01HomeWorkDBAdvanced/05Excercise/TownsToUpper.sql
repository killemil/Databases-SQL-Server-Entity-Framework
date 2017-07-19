
UPDATE Towns
SET Name = UPPER(Name)
WHERE Country = @coutryName