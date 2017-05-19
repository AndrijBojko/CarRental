USE CarRental
GO

SET IDENTITY_INSERT Manager ON;

INSERT INTO Manager(Id, FirstName,LastName,[Login],[Password])
	VALUES
	(1,'Andrii','Boiko','boiko','827ccb0eea8a706c4c34a16891f84e7b'),--12345
	(2,'Ivan','Ivanov', 'ivanov','e10adc3949ba59abbe56e057f20f883e');--123456

SET IDENTITY_INSERT Manager OFF;
GO

SET IDENTITY_INSERT Car ON;

INSERT INTO Car(Id,Make,Model,[Type],Transmission,SeatsNumber) 
	VALUES
	(1,'Mazda','CX-7','SUV','Auto',5),
	(2,'Audi','A6','Sedan','Auto',5),
	(3, 'Ford','Fiesta','Hatchback','Manual',5),
	(4,'Mercedes','S65 AMG','Coupe','Manual',2),
	(5,'Toyota','Prius','Hybrid','Auto',4);

SET IDENTITY_INSERT Car OFF;
GO

SET IDENTITY_INSERT Customer ON;

INSERT INTO Customer(Id,FirstName,LastName,DateOfBirth,PhoneNumber,Adress)
	VALUES
	(1,'Andrii','Zyhor','1994-03-10','(050)1234567','Lviv,Bandery 17'),
	(2,'Bohdan','Zaharkiv','1993-03-10','(050)2345678','Lviv,Horodocka 151'),
	(3,'Roman','Frankiv','1994-04-22','(050)3456789','Lviv,Chornovola 55'),
	(4,'Dima','Stets','1994-01-01','(050)4567890','Lviv,Lypynskogo 17'),
	(5,'Oleg','Shyba','1990-10-22','(050)4567891','Lviv,Mazepy 20'),
	(6,'Vasyl','Shysh','1989-11-11','(050)5678901','Lviv,Grinchenka 66'),
	(7,'Ivan','Myhaliuk','1990-05-05','(050)6789012','Lviv,Zelena 100'),
	(8,'Nazar','Kruk','1995-08-09','(050)7890123','Lviv,Sheptyckyh 12')

SET IDENTITY_INSERT Customer OFF;
GO



SET IDENTITY_INSERT [Order] ON;

INSERT INTO [Order](Id, ManagerId, CarId, CustomerId, StartDateTime, FinishDateTime)
	VALUES
	(1, 1, 1, 1, '2017-01-11 10:00', '2017-01-15 15:00' ),
	(2, 2, 2, 2, '2017-01-13 15:00', '2017-01-14 15:00' ),
	(3, 1, 3, 3, '2017-01-16 16:00', '2017-01-18 11:00'),
	(4, 2, 4, 4, '2017-01-22 17:00', '2017-01-23 17:00'),
	(5, 2, 5, 5, '2017-01-22 18:00', '2017-01-23 20:00'),
	(6, 2, 2, 6, '2017-01-23 12:00', '2017-01-23 20:00'),
	(7, 1, 1, 7, '2017-01-24 08:00', '2017-01-25 13:00'),
	(8, 1, 4, 8, '2017-01-26 11:00', '2017-01-27 12:00')

SET IDENTITY_INSERT [Order] OFF;
GO