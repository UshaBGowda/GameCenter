CREATE TABLE tblGame
(
	gameId INT IDENTITY PRIMARY KEY,
	descript VARCHAR(40) NOT NULL,
	therapyId INT NOT NULL,
	FOREIGN KEY (therapyId) REFERENCES tblTherapy(therapyID),

)