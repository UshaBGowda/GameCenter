CREATE TABLE tblUserType
(
	userTypeId INT IDENTITY PRIMARY KEY,
	userType varchar(10) NOT NULL
)

Insert into tblUserType(userType) values('Provider');
Insert into tblUserType(userType) values('Parent');
Insert into tblUserType(userType) values('Patient');
