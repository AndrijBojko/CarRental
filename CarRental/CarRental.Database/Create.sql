CREATE DATABASE CarRental
GO

USE CarRental
GO

CREATE TABLE Manager
(
	Id INT NOT NULL IDENTITY(1,1),
	FirstName NVARCHAR(25) NOT NULL,
	LastName NVARCHAR(40) NOT NULL,
	[Login] NVARCHAR(50) NOT NULL,
	[Password] NVARCHAR(50) NOT NULL, 

	CONSTRAINT PK_Manager_Id PRIMARY KEY (Id),
	CONSTRAINT UQ_Manager_Login UNIQUE ([Login])
);


CREATE TABLE Car
(
	Id INT NOT NULL IDENTITY(1,1),
	Make NVARCHAR(25) NOT NULL,
	Model NVARCHAR(25) NOT NULL,
	[Type] NVARCHAR(25) NOT NULL,
	Transmission NVARCHAR(25) NOT NULL,
	SeatsNumber INT NOT NULL,
	
	CONSTRAINT PK_Car_Id PRIMARY KEY (Id),
	CONSTRAINT CK_Transmission CHECK (Transmission IN ('Auto', 'Manual')),
	CONSTRAINT CK_BodyType CHECK ([Type] IN ('Sedan', 'Coupe', 'Hatchback', 'Hybrid', 'Minivan', 'SUV'))
);


CREATE TABLE Customer
(
	Id INT NOT NULL IDENTITY(1,1),
	FirstName NVARCHAR(25) NOT NULL,
	LastName NVARCHAR(40) NOT NULL,
	DateOfBirth DATE NOT NULL,
	PhoneNumber NVARCHAR(12),
	Adress NVARCHAR(100) NOT NULL,

	CONSTRAINT PK_Customer_Id PRIMARY KEY (Id),
	CONSTRAINT CK_Customer_DateOfBirth CHECK (DateOfBirth < GETDATE()),
	CONSTRAINT UQ_Customer_CustomerUnique UNIQUE (FirstName, LastName, DateOfBirth, Adress),
	CONSTRAINT UQ_Customer_PhoneNumberUnique UNIQUE (PhoneNumber),
	CONSTRAINT CK_Customer_PhoneNumberLike CHECK (PhoneNumber LIKE '([0-9][0-9][0-9])[0-9][0-9][0-9][0-9][0-9][0-9][0-9]')
);




CREATE TABLE [Order]
(
	Id INT NOT NULL IDENTITY(1,1),
	ManagerId INT NOT NULL,
	CarId INT NOT NULL,
	CustomerId INT NOT NULL,
	StartDateTime DATETIME NOT NULL,
	FinishDateTime DATETIME NOT NULL 

	CONSTRAINT PK_Order_Id PRIMARY KEY (Id),
	CONSTRAINT FK_Order_Manager_Id FOREIGN KEY (ManagerId) REFERENCES Manager (Id) ON DELETE CASCADE ,
	CONSTRAINT FK_Order_Car_Id FOREIGN KEY (CarId) REFERENCES Car (Id) ON DELETE CASCADE ,
	CONSTRAINT FK_Order_Customer_Id FOREIGN KEY (CustomerId) REFERENCES Customer (Id)  ON DELETE CASCADE,

);