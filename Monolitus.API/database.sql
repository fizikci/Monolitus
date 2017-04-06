DROP TABLE User;
CREATE TABLE User(
	Id varchar(12) NOT NULL PRIMARY KEY,
	Email varchar(100) NOT NULL,
	Password varchar(16) NOT NULL,
	
	UserType varchar(10) NOT NULL,
	Name varchar(50) NOT NULL,
	Surname varchar(50),
	
	PhoneCell varchar(50) NOT NULL,
	EmailValidated bool,
	PhoneCellValidated bool,
	
	Avatar varchar(100),
	City varchar(50),
	Gender int,
	DogumTarihi varchar(45),
	FacebookId varchar(20),
	TwitterId varchar(20),
	
	ProfileInfoPercent int,
	
	Keyword varchar(16) NOT NULL,
	LastLoginDate datetime,
	NewLoginDate datetime,
	NewEmail varchar(100),
	
	InsertDate datetime,
	IsDeleted bool
);

DROP TABLE Sector;
CREATE TABLE Sector(
	Id varchar(12) NOT NULL PRIMARY KEY,
	Name varchar(100) NOT NULL,
	OrderNo int,
	InsertDate datetime,
	IsDeleted bool
);

insert into Sector(Id,Name,OrderNo,InsertDate,IsDeleted) values('Adalet', 'Adalet ve G�venlik',0,'2015-03-26',0);
insert into Sector(Id,Name,OrderNo,InsertDate,IsDeleted) values('Agac', 'A�a� ��leri, Ka��t ve Ka��t �r�nleri',0,'2015-03-26',0);
insert into Sector(Id,Name,OrderNo,InsertDate,IsDeleted) values('Bilisim', 'Bili�im Teknolojileri',0,'2015-03-26',0);
insert into Sector(Id,Name,OrderNo,InsertDate,IsDeleted) values('Cam', 'Cam, �imento ve Toprak',0,'2015-03-26',0);
insert into Sector(Id,Name,OrderNo,InsertDate,IsDeleted) values('Cevre', '�evre',0,'2015-03-26',0);
insert into Sector(Id,Name,OrderNo,InsertDate,IsDeleted) values('Egitim', 'E�itim',0,'2015-03-26',0);
insert into Sector(Id,Name,OrderNo,InsertDate,IsDeleted) values('Elektrik', 'Elektrik, Elektronik',0,'2015-03-26',0);
insert into Sector(Id,Name,OrderNo,InsertDate,IsDeleted) values('Enerji', 'Enerji',0,'2015-03-26',0);
insert into Sector(Id,Name,OrderNo,InsertDate,IsDeleted) values('Finans', 'Finans',0,'2015-03-26',0);
insert into Sector(Id,Name,OrderNo,InsertDate,IsDeleted) values('Gida', 'G�da',0,'2015-03-26',0);
insert into Sector(Id,Name,OrderNo,InsertDate,IsDeleted) values('Insaat', '�n�aat',0,'2015-03-26',0);
insert into Sector(Id,Name,OrderNo,InsertDate,IsDeleted) values('Is', '�� ve Y�netim',0,'2015-03-26',0);
insert into Sector(Id,Name,OrderNo,InsertDate,IsDeleted) values('Kimya', 'Kimya, Petrol, Lastik ve Plastik',0,'2015-03-26',0);
insert into Sector(Id,Name,OrderNo,InsertDate,IsDeleted) values('Kultur', 'K�lt�r, Sanat',0,'2015-03-26',0);
insert into Sector(Id,Name,OrderNo,InsertDate,IsDeleted) values('Maden', 'Maden',0,'2015-03-26',0);
insert into Sector(Id,Name,OrderNo,InsertDate,IsDeleted) values('Medya', 'Medya, �leti�im ve Yay�nc�l�k',0,'2015-03-26',0);
insert into Sector(Id,Name,OrderNo,InsertDate,IsDeleted) values('Metal', 'Metal',0,'2015-03-26',0);
insert into Sector(Id,Name,OrderNo,InsertDate,IsDeleted) values('Otomotiv', 'Otomotiv',0,'2015-03-26',0);
insert into Sector(Id,Name,OrderNo,InsertDate,IsDeleted) values('Saglik', 'Sa�l�k ve Sosyal Hizmetler',0,'2015-03-26',0);
insert into Sector(Id,Name,OrderNo,InsertDate,IsDeleted) values('Tarim', 'Tar�m, Avc�l�k, Bal�k��l�k',0,'2015-03-26',0);
insert into Sector(Id,Name,OrderNo,InsertDate,IsDeleted) values('Tekstil', 'Tekstil, Haz�r Giyim, Deri',0,'2015-03-26',0);
insert into Sector(Id,Name,OrderNo,InsertDate,IsDeleted) values('Ticaret', 'Ticaret (Sat�� ve Pazarlama)',0,'2015-03-26',0);
insert into Sector(Id,Name,OrderNo,InsertDate,IsDeleted) values('Toplumsal', 'Toplumsal ve Ki�isel Hizmetler',0,'2015-03-26',0);
insert into Sector(Id,Name,OrderNo,InsertDate,IsDeleted) values('Turizm', 'Turizm, Konaklama, Yiyecek-��ecek Hizmetleri',0,'2015-03-26',0);
insert into Sector(Id,Name,OrderNo,InsertDate,IsDeleted) values('Ulastirma', 'Ula�t�rma, Lojistik ve Haberle�me',0,'2015-03-26',0);

DROP TABLE Company;
CREATE TABLE Company(
	Id varchar(12) NOT NULL PRIMARY KEY,
	Name varchar(100) NOT NULL,
	LogoPath varchar(100),
	SectorId varchar(12),
	Url varchar(100),
	
	AuthUserId varchar(12),
	
	SmsCount int,
	GsmCount int,
	EmailCount int,
	
	ApiSmsAllowLink varchar(100),
	ApiSmsRejectLink varchar(100),
	ApiEmailAllowLink varchar(100),
	ApiEmailRejectLink varchar(100),
	ApiGsmAllowLink varchar(100),
	ApiGsmRejectLink varchar(100),
	ApiEmailChangeLink varchar(100),
	ApiPhoneChangeLink varchar(100),
	
	InsertDate datetime,
	IsDeleted bool
);

DROP TABLE UserCompany;
CREATE TABLE UserCompany(
	Id varchar(12) NOT NULL PRIMARY KEY,
	UserId varchar(12) NOT NULL,
	CompanyId varchar(12) NOT NULL,
	
	Sms bool,
	Gsm bool,
	Email bool,
	
	SmsUpdateDate datetime,
	GsmUpdateDate datetime,
	EmailUpdateDate datetime,
	
	InsertDate datetime
);

DROP TABLE Session;
CREATE TABLE Session(
	Id varchar(12) NOT NULL PRIMARY KEY,
	UserId varchar(100),
	LoginDate datetime,
	LastAccess datetime,
	InsertDate datetime
);

DROP TABLE ApiUsage;
CREATE TABLE ApiUsage(
	Id varchar(12) NOT NULL PRIMARY KEY,
	UserId varchar(100),
	MethodName varchar(50),
	Successful bool,
	ProcessTime int,
	InsertDate datetime
);

DROP TABLE CompanyApplication;
CREATE TABLE CompanyApplication(
	Id varchar(12) NOT NULL PRIMARY KEY,
	
	Name varchar(100) NOT NULL,
	AuthName varchar(100),
	Email varchar(100),
	PhoneCell varchar(16),
	City varchar(50),
	Url varchar(100),
	
	InsertDate datetime,
	Applied bool
);

DROP TABLE Message;
CREATE TABLE Message(
	Id varchar(12) NOT NULL PRIMARY KEY,
	UserId varchar(12) NOT NULL,
	Subject varchar(100) NOT NULL,
	MessageText text NULL,
	InsertDate datetime NULL,
	Email varchar(100) NULL);

DROP TABLE SmsSend;
CREATE TABLE SmsSend(
	Id varchar(12) NOT NULL PRIMARY KEY,
	CompanyId varchar(12) NOT NULL,
	MessageText text,
	ToUsers int,
	UsersFilter text,
	InsertDate datetime
);

CREATE VIEW ListViewUserCompany AS 
select 
	uc.Id AS Id,
	uc.UserId AS UserId,
	uc.CompanyId AS CompanyId,
	uc.Sms AS Sms,
	uc.Gsm AS Gsm,
	uc.Email AS Email,
	uc.SmsUpdateDate AS SmsUpdateDate,
	uc.GsmUpdateDate AS GsmUpdateDate,
	uc.EmailUpdateDate AS EmailUpdateDate,
	uc.InsertDate AS InsertDate,
	concat(u.Name,' ',u.Surname) AS UserName 
from 
	(usercompany uc join user u) where (uc.UserId = u.Id);
	
CREATE VIEW ListViewMessage AS
SELECT
	m.*,
	concat(u.Name,' ',u.Surname) AS UserName
FROM
	Message m, User u
where 
	m.UserId = u.Id;