CREATE TABLE IF NOT EXISTS `tableTest` (
	`IdPotager`	INTEGER PRIMARY KEY,
	`PositionX`	INTEGER,
	`PositionY`	INTEGER,
	`IdLegume`	INTEGER,
	`DatePlantage`	TEXT,
	`IdOutil`	INTEGER,
	`DateDernierArrosage`	TEXT,
	`IdProfil`	INTEGER
);

INSERT INTO `tableTest` VALUES (1, 2, 3, 4, "test", 5, "test2", 6);