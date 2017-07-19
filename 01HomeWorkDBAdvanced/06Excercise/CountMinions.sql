SELECT COUNT(mv.MinionId)
  FROM MinionsVillains AS mv
 WHERE mv.VillainsId = @villainId