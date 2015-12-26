CREATE TABLE tblGame
(
	gameId INT IDENTITY PRIMARY KEY,
	gameName VARCHAR(30) NOT NULL,
	gameDescription VARCHAR(50),
	therapyId INT NOT NULL,
	FOREIGN KEY (therapyId) REFERENCES tblTherapy(therapyId),

)
