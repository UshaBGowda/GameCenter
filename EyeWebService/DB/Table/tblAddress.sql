CREATE TABLE tblAddress
(
	addressId INT IDENTITY PRIMARY KEY,
	addressType VARCHAR(20) NOT NULL,
	loginId nvarchar(128) NOT NULL,
	streetName VARCHAR(200) NOT NULL,
	city VARCHAR(100) NOT NULL,
	stateName VARCHAR(100) NOT NULL,
	country VARCHAR(50) NOT NULL,
	zipcode VARCHAR(15) NOT NULL,
	phoneNo VARCHAR(20) NOT NULL,
	FOREIGN KEY (loginId) REFERENCES [dbo].[AspNetUsers](Id) ON DELETE CASCADE ON UPDATE CASCADE
)

--select * from tblLogin;
--select * from tblAddress;
--select * from tblUser;
--drop table tblLogin;
--select * from tblParentXREF;

--drop table tblParentXREF;
--drop table tblUser;
--drop table tblAddress;
--drop table tblLogin;