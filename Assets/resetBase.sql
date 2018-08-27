CREATE TABLE IF NOT EXISTS `Outil` (
	`IdOutil`	INTEGER PRIMARY KEY,
	`Nom`	TEXT NOT NULL,
	`NbExp`	INTEGER
);

CREATE TABLE IF NOT EXISTS `Niveau` (
	`IdNiveau`	INTEGER PRIMARY KEY,
	`Nom`	TEXT,
	`NbExp`	INTEGER
);

CREATE TABLE IF NOT EXISTS `Profil` (
	`IdProfl`	INTEGER PRIMARY KEY,
	`Nom`	INTEGER,
	`IdNiveau`	INTEGER,
	`Exp`	INTEGER,
	`Date`	TEXT,
	`TempsPasse`	INTEGER,
	FOREIGN KEY(`IdNiveau`) REFERENCES `Niveau`(`IdNiveau`)
);

CREATE TABLE IF NOT EXISTS `Description` (
	`IdDescription`	INTEGER PRIMARY KEY,
	`DebutPeriodePlantage`	TEXT,
	`FinPeriodePlantage`	TEXT,
	`DebutPeriodeRecolte`	TEXT,
	`FinPeriodeRecolte`	TEXT,
	`TempsArrosage`	INTEGER,
	`TexteDescription`	TEXT,
	`TempsPousseMin`	INTEGER,
	`TempsPousseMax`	INTEGER
);

CREATE TABLE IF NOT EXISTS `Legume` (
	`IdLegume`	INTEGER PRIMARY KEY,
	`Nom`	TEXT,
	`NomImage`	TEXT,
	`IdDescription`	INTEGER,
	FOREIGN KEY(`IdDescription`) REFERENCES `Description`(`IdDescription`)
);

CREATE TABLE IF NOT EXISTS `Recolte` (
	`IdRecolte`	INTEGER PRIMARY KEY,
	`IdProfil`	INTEGER,
	`IdLegume`	INTEGER,
	`NbRecoltes`	INTEGER,
	`NbMalRecoltes`	INTEGER,
	FOREIGN KEY(`IdProfil`) REFERENCES `Profil`(`IdProfl`),
	FOREIGN KEY(`IdLegume`) REFERENCES `Legume`(`IdLegume`)
);

CREATE TABLE IF NOT EXISTS `Potager` (
	`IdPotager`	INTEGER PRIMARY KEY,
	`PositionX`	INTEGER,
	`PositionY`	INTEGER,
	`IdLegume`	INTEGER,
	`DatePlantage`	TEXT,
	`IdOutil`	INTEGER,
	`DateDernierArrosage`	TEXT,
	`IdProfil`	INTEGER,
	FOREIGN KEY(`IdOutil`) REFERENCES `Outil`(`IdOutil`),
	FOREIGN KEY(`IdLegume`) REFERENCES `Legume`(`IdLegume`),
	FOREIGN KEY(`IdProfil`) REFERENCES `Profil`(`IdProfl`)
);

INSERT INTO Description ('DebutPeriodePlantage', 'FinPeriodePlantage', 'DebutPeriodeRecolte', 
				'FinPeriodeRecolte', 'TempsArrosage', 'TexteDescription',
				'TempsPousseMin', 'TempsPousseMax') 
VALUES ('2000-02-01', '2000-06-01', '2000-07-01', '2000-10-01', '3', 
				'Description tomate', '120', '130');
				
INSERT INTO Description ('DebutPeriodePlantage', 'FinPeriodePlantage', 'DebutPeriodeRecolte', 
				'FinPeriodeRecolte', 'TempsArrosage', 'TexteDescription',
				'TempsPousseMin', 'TempsPousseMax') 
VALUES ('2000-03-01', '2000-06-01', '2000-06-01', '2000-10-01', '7', 
				'Description pomme de terre', '90', '150');

INSERT INTO Description ('DebutPeriodePlantage', 'FinPeriodePlantage', 'DebutPeriodeRecolte', 
				'FinPeriodeRecolte', 'TempsArrosage', 'TexteDescription',
				'TempsPousseMin', 'TempsPousseMax') 
VALUES ('2000-03-01', '2000-07-15', '2000-04-01', '2000-11-01', '6', 
				'Description carotte', '90', '150');
				
INSERT INTO Description ('DebutPeriodePlantage', 'FinPeriodePlantage', 'DebutPeriodeRecolte', 
				'FinPeriodeRecolte', 'TempsArrosage', 'TexteDescription',
				'TempsPousseMin', 'TempsPousseMax') 
VALUES ('2000-02-01', '2000-04-01', '2000-07-01', '2000-10-01', '3', 
				'Description aubergine', '140', '160');
				
INSERT INTO Description ('DebutPeriodePlantage', 'FinPeriodePlantage', 'DebutPeriodeRecolte', 
				'FinPeriodeRecolte', 'TempsArrosage', 'TexteDescription',
				'TempsPousseMin', 'TempsPousseMax') 
VALUES ('2000-04-01', '2000-06-01', '2000-09-01', '2000-12-01', '7', 
				'Description brocoli', '120', '210');
				
INSERT INTO Description ('DebutPeriodePlantage', 'FinPeriodePlantage', 'DebutPeriodeRecolte', 
				'FinPeriodeRecolte', 'TempsArrosage', 'TexteDescription',
				'TempsPousseMin', 'TempsPousseMax') 
VALUES ('2000-02-01', '2000-07-01', '2000-06-01', '2000-03-01', '3', 
				'Description laitue', '60', '80');
				
INSERT INTO Legume ('Nom', 'nomImage', 'IdDescription') VALUES ('Tomate', 'tomato', 1);
INSERT INTO Legume ('Nom', 'nomImage', 'IdDescription') VALUES ('Pomme de terre', 'potato', 2);
INSERT INTO Legume ('Nom', 'nomImage', 'IdDescription') VALUES ('Carotte', 'carrot', 3);
INSERT INTO Legume ('Nom', 'nomImage', 'IdDescription') VALUES ('Aubergine', 'eggplant', 4);
INSERT INTO Legume ('Nom', 'nomImage', 'IdDescription') VALUES ('Brocoli', 'brocoli', 5);
INSERT INTO Legume ('Nom', 'nomImage', 'IdDescription') VALUES ('Laitue', 'salad', 6);
INSERT INTO Legume ('Nom', 'nomImage') VALUES ('Herbe', 'grass');
INSERT INTO Legume ('Nom', 'nomImage') VALUES ('Terre', 'mud');
INSERT INTO Legume ('Nom', 'nomImage') VALUES ('Terre à retourner', 'mudNotGood');

INSERT INTO Niveau ('Nom', 'NbExp') VALUES ('Débutant', 0);
INSERT INTO Niveau ('Nom', 'NbExp') VALUES ('Intermédiaire', 50000);
INSERT INTO Niveau ('Nom', 'NbExp') VALUES ('Expert', 150000);

INSERT INTO Profil ('Nom', 'IdNiveau', 'Exp', 'Date', 'TempsPasse') VALUES ('NomTest', 1, 4000, '10/12/2018', 0);

INSERT INTO Outil ('Nom', 'NbExp') VALUES ('ArroseurAutomatique', 20000);
INSERT INTO Outil ('Nom', 'NbExp') VALUES ('Scie', 25000);
INSERT INTO Outil ('Nom', 'NbExp') VALUES ('Casse-pierre', 30000);

INSERT INTO Potager ('PositionX', 'PositionY', 'IdLegume', 'IdProfil') VALUES (-6.08, 3.28, 7, 1);
INSERT INTO Potager ('PositionX', 'PositionY', 'IdLegume', 'IdProfil') VALUES (-4.16, 3.28, 7, 1);
INSERT INTO Potager ('PositionX', 'PositionY', 'IdLegume', 'IdProfil') VALUES (-2.24, 3.28, 7, 1);
INSERT INTO Potager ('PositionX', 'PositionY', 'IdLegume', 'IdProfil') VALUES (-0.32, 3.28, 7, 1);
INSERT INTO Potager ('PositionX', 'PositionY', 'IdLegume', 'IdProfil') VALUES (1.6, 3.28, 7, 1);
INSERT INTO Potager ('PositionX', 'PositionY', 'IdLegume', 'IdProfil') VALUES (3.52, 3.28, 7, 1);
INSERT INTO Potager ('PositionX', 'PositionY', 'IdLegume', 'IdProfil') VALUES (5.44, 3.28, 7, 1);
INSERT INTO Potager ('PositionX', 'PositionY', 'IdLegume', 'IdProfil') VALUES (7.36, 3.28, 7, 1);

INSERT INTO Potager ('PositionX', 'PositionY', 'IdLegume', 'IdProfil') VALUES (-6.72, 2, 7, 1);
INSERT INTO Potager ('PositionX', 'PositionY', 'IdLegume', 'IdProfil') VALUES (-4.8, 2, 7, 1);
INSERT INTO Potager ('PositionX', 'PositionY', 'IdLegume', 'IdProfil') VALUES (-2.88, 2, 7, 1);
INSERT INTO Potager ('PositionX', 'PositionY', 'IdLegume', 'IdProfil') VALUES (-0.96, 2, 7, 1);
INSERT INTO Potager ('PositionX', 'PositionY', 'IdLegume', 'IdProfil') VALUES (0.96, 2, 7, 1);
INSERT INTO Potager ('PositionX', 'PositionY', 'IdLegume', 'IdProfil') VALUES (2.88, 2, 7, 1);
INSERT INTO Potager ('PositionX', 'PositionY', 'IdLegume', 'IdProfil') VALUES (4.8, 2, 7, 1);
INSERT INTO Potager ('PositionX', 'PositionY', 'IdLegume', 'IdProfil') VALUES (6.72, 2, 7, 1);

INSERT INTO Potager ('PositionX', 'PositionY', 'IdLegume', 'IdProfil') VALUES (-7.36, 0.72, 7, 1);
INSERT INTO Potager ('PositionX', 'PositionY', 'IdLegume', 'IdProfil') VALUES (-5.44, 0.72, 7, 1);
INSERT INTO Potager ('PositionX', 'PositionY', 'IdLegume', 'IdProfil') VALUES (-3.52, 0.72, 7, 1);
INSERT INTO Potager ('PositionX', 'PositionY', 'IdLegume', 'IdProfil') VALUES (-1.6, 0.72, 8, 1);
INSERT INTO Potager ('PositionX', 'PositionY', 'IdLegume', 'IdProfil') VALUES (0.32, 0.72, 8, 1);
INSERT INTO Potager ('PositionX', 'PositionY', 'IdLegume', 'IdProfil') VALUES (2.24, 0.72, 7, 1);
INSERT INTO Potager ('PositionX', 'PositionY', 'IdLegume', 'IdProfil') VALUES (4.16, 0.72, 7, 1);
INSERT INTO Potager ('PositionX', 'PositionY', 'IdLegume', 'IdProfil') VALUES (6.08, 0.72, 7, 1);

INSERT INTO Potager ('PositionX', 'PositionY', 'IdLegume', 'IdProfil') VALUES (-8, -0.56, 7, 1);
INSERT INTO Potager ('PositionX', 'PositionY', 'IdLegume', 'IdProfil') VALUES (-6.08, -0.56, 7, 1);
INSERT INTO Potager ('PositionX', 'PositionY', 'IdLegume', 'IdProfil') VALUES (-4.16, -0.56, 7, 1);
INSERT INTO Potager ('PositionX', 'PositionY', 'IdLegume', 'IdProfil') VALUES (-2.24, -0.56, 8, 1);
INSERT INTO Potager ('PositionX', 'PositionY', 'IdLegume', 'IdProfil') VALUES (-0.32, -0.56, 8, 1);
INSERT INTO Potager ('PositionX', 'PositionY', 'IdLegume', 'IdProfil') VALUES (1.6, -0.56, 7, 1);
INSERT INTO Potager ('PositionX', 'PositionY', 'IdLegume', 'IdProfil') VALUES (3.52, -0.56, 7, 1);
INSERT INTO Potager ('PositionX', 'PositionY', 'IdLegume', 'IdProfil') VALUES (5.44, -0.56, 7, 1);

INSERT INTO Potager ('PositionX', 'PositionY', 'IdLegume', 'IdProfil') VALUES (-8.64, -1.84, 7, 1);
INSERT INTO Potager ('PositionX', 'PositionY', 'IdLegume', 'IdProfil') VALUES (-6.72, -1.84, 7, 1);
INSERT INTO Potager ('PositionX', 'PositionY', 'IdLegume', 'IdProfil') VALUES (-4.8, -1.84, 7, 1);
INSERT INTO Potager ('PositionX', 'PositionY', 'IdLegume', 'IdProfil') VALUES (-2.88, -1.84, 7, 1);
INSERT INTO Potager ('PositionX', 'PositionY', 'IdLegume', 'IdProfil') VALUES (-0.96, -1.84, 7, 1);
INSERT INTO Potager ('PositionX', 'PositionY', 'IdLegume', 'IdProfil') VALUES (0.96, -1.84, 7, 1);
INSERT INTO Potager ('PositionX', 'PositionY', 'IdLegume', 'IdProfil') VALUES (2.88, -1.84, 7, 1);
INSERT INTO Potager ('PositionX', 'PositionY', 'IdLegume', 'IdProfil') VALUES (4.80, -1.84, 7, 1);

INSERT INTO Potager ('PositionX', 'PositionY', 'IdLegume', 'IdProfil') VALUES (-9.28, -3.12, 7, 1);
INSERT INTO Potager ('PositionX', 'PositionY', 'IdLegume', 'IdProfil') VALUES (-7.36, -3.12, 7, 1);
INSERT INTO Potager ('PositionX', 'PositionY', 'IdLegume', 'IdProfil') VALUES (-5.44, -3.12, 7, 1);
INSERT INTO Potager ('PositionX', 'PositionY', 'IdLegume', 'IdProfil') VALUES (-3.52, -3.12, 7, 1);
INSERT INTO Potager ('PositionX', 'PositionY', 'IdLegume', 'IdProfil') VALUES (-1.6, -3.12, 7, 1);
INSERT INTO Potager ('PositionX', 'PositionY', 'IdLegume', 'IdProfil') VALUES (0.32, -3.12, 7, 1);
INSERT INTO Potager ('PositionX', 'PositionY', 'IdLegume', 'IdProfil') VALUES (2.24, -3.12, 7, 1);
INSERT INTO Potager ('PositionX', 'PositionY', 'IdLegume', 'IdProfil') VALUES (4.16, -3.12, 7, 1);