CREATE TABLE tblParentXREF
(
	parentId INT NOT NULL,
	patientId INT NOT NULL,
	providerId INT NOT NULL,
	PRIMARY KEY (parentId,patientId)
	--FOREIGN KEY (parentId) REFERENCES tblUser(userId),
	--FOREIGN KEY (patientId) REFERENCES tblUser(userId),
	--FOREIGN KEY (providerId) REFERENCES tblUser(userId)
)

--select * from tblLogin;
--select * from tblUser;
--select * from tblAddress;
--select * from tblParentXREF;
--drop table tblParentXREF;