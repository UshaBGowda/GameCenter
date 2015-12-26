CREATE TABLE tblAddress
(
	addressId INT IDENTITY PRIMARY KEY,
	addressType VARCHAR(20) NOT NULL,
	loginName VARCHAR(12) NOT NULL,
	streetName VARCHAR(20) NOT NULL,
	city VARCHAR(10) NOT NULL,
	stateName VARCHAR(10) NOT NULL,
	country VARCHAR(15) NOT NULL,
	zipcode INT NOT NULL,
	phoneNo INT NOT NULL,
	FOREIGN KEY (loginName) REFERENCES tblLogin(LoginName) ON DELETE CASCADE ON UPDATE CASCADE
)

select * from tblLogin;
select * from tblAddress;
select * from tblUser;
select * from tblParentXREF;