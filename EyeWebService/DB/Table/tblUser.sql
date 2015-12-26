CREATE TABLE tblUser
(
	userId INT IDENTITY PRIMARY KEY,
	firstName VARCHAR(30) NOT NULL,
	lastName VARCHAR(30) NOT NULL,
	loginName VARCHAR(12) NOT NULL,
	userType VARCHAR(30) NOT NULL,
	dateOfBirth DATETIME,
	gender char(1)
)

--DROP TABLE tblUser
--DROP table tblParentXREF;

