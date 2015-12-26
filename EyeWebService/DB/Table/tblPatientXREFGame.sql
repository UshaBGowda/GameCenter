CREATE TABLE tblPatientXREFGame
(
	patientId INT NOT NULL,
	gameId INT NOT NULL,
	startDate DATETIME NOT NULL,
	endDate DATETIME NOT NULL,
	maxLevel INT,
	PRIMARY KEY (patientId,gameId,startDate),
	FOREIGN KEY (patientId) REFERENCES tblUser(userId),
	FOREIGN KEY (gameId) REFERENCES tblGame(gameId)
)