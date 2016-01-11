CREATE TABLE tblUser
(
	userId INT IDENTITY PRIMARY KEY,
	loginId nvarchar(128)  NULL,
	firstName VARCHAR(30) NOT NULL,
	lastName VARCHAR(30) NOT NULL,
	userTypeId INT NOT NULL,
	dateOfBirth DATETIME,
	gender char(1)--,
	--FOREIGN KEY (loginId) REFERENCES [dbo].[AspNetUsers](Id) ON DELETE CASCADE ON UPDATE CASCADE,
	--FOREIGN KEY (userTypeId) REFERENCES dbo.tblUserType(userTypeId) ON DELETE CASCADE ON UPDATE CASCADE
)

--DROP TABLE tblUser
--DROP table tblParentXREF;

--select * from tblGame;
--select * from tblTherapy;