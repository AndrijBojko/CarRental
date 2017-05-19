USE master;
GO

DROP TABLE [CarRental].dbo.[Order];
DROP TABLE [CarRental].dbo.[Manager];
DROP TABLE [CarRental].dbo.[Car];
DROP TABLE [CarRental].dbo.[Customer];


ALTER DATABASE [CarRental] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
DROP DATABASE [CarRental];
