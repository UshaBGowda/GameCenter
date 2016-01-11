--CREATE MASTER KEY ENCRYPTION BY 
--PASSWORD = 'v1s10n3y3'
--GO

--CREATE CERTIFICATE TestCert
--   WITH SUBJECT = 'Pwd Encryption';
--GO
 
--CREATE SYMMETRIC KEY HRKey
--    WITH ALGORITHM = DES
--    ENCRYPTION BY CERTIFICATE eyeVisionPwdCert;
--GO

CREATE TABLE tblLogin
(
	Id INT IDENTITY PRIMARY KEY,
	LoginName VARCHAR(12) UNIQUE NOT NULL,
	Password VARCHAR(50) NOT NULL ,
	emailID VARCHAR(50) NOT NULL,
    CreatedDate DATETIME DEFAULT GETDATE(),
	LastUpdateDate DATETIME DEFAULT GETDATE()
)

--DROP TABLE tblGameScore;
--DROP TABLE tblPatientXREFGame;
--DROP TABLE tblGame;
--DROP TABLE tblTherapy;
--DROP TABLE tblParentXREFPatient;
--DROP TABLE tblUser;
--DROP TABLE tblAddress;
--DROP TABLE tblLogin;

--select * from tblLogin;
--delete from tblLogin;