CREATE TABLE tblGameScore
(
	gameId INT NOT NULL,
    patientId INT NOT NULL,
	startTime DATETIME NOT NULL,
	endTime DATETIME NOT NULL,
	level INT,
	gameScore INT
);