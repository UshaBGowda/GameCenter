CREATE TABLE tblGameScore
(
	gameId INT NOT NULL,
    patientId INT NOT NULL,
	startTime DATETIME NOT NULL,
	endTime DATETIME NOT NULL,
	level INT,
	gameScore INT,
	PRIMARY KEY (gameId,patientId,startTime),
	FOREIGN KEY (patientId) REFERENCES tblUser(userId),
	FOREIGN KEY (gameId) REFERENCES tblGame(gameId)
);