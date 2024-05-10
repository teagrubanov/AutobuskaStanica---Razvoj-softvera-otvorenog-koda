CREATE DATABASE AutobuskaStanica
GO

USE AutobuskaStanica

GO

CREATE TABLE Korisnik
(
idKorisnika int identity(1,1) not null primary key,
Ime nvarchar(30) not null,
Prezime nvarchar(30) not null,
Korisnickoime nvarchar(255) not null,
Lozinka nvarchar (255) not null,
Status nvarchar(50) null
);

GO

CREATE TABLE StatusRezervacije
(
	Status nvarchar (50) not null primary key
) ;

CREATE TABLE Rezervacija
(
	idKorisnika int not null,
	idVoznje int not null,
	Status nvarchar (50) null
) ;

GO

CREATE TABLE Voznja
(
	idVoznje int identity(1,1) not null primary key,
	NazivPrevoznika nvarchar(255) not null,
	Datum date not null,
	Vreme time not null,
	MestoPolaska nvarchar(50) not null,
	MestoDolaska nvarchar(50) not null,
) ;

GO

CREATE TABLE Prevoznik
(
	 NazivPrevoznika nvarchar(255) not null primary key,
	 AdresaPrevoznika nvarchar(255) not null
) ;

GO

ALTER TABLE Rezervacija
ADD CONSTRAINT FK_rezervacija_korisnik FOREIGN KEY (idKorisnika)
REFERENCES Korisnik (idKorisnika)

GO

ALTER TABLE Rezervacija
ADD CONSTRAINT FK_rezervacija_voznja FOREIGN KEY (idVoznje)
REFERENCES Voznja (idVoznje)

GO

ALTER TABLE Rezervacija
ADD CONSTRAINT FK_rezervacija_statusrezervacija FOREIGN KEY (Status)
REFERENCES StatusRezervacije (Status)

GO

ALTER TABLE Voznja
ADD CONSTRAINT FK_voznja_prevoznik FOREIGN KEY (NazivPrevoznika)
REFERENCES Prevoznik (NazivPrevoznika)



SET IDENTITY_INSERT [dbo].[Korisnik] ON 

INSERT [dbo].[Korisnik] ([idKorisnika], [Ime], [Prezime], [Korisnickoime], [Lozinka], [Status]) VALUES (1, N'Tea', N'Grubanov', N'tea', N'123', N'Admin')
INSERT [dbo].[Korisnik] ([idKorisnika], [Ime], [Prezime], [Korisnickoime], [Lozinka], [Status]) VALUES (2, N'1', N'1', N'1', N'1', N'Admin')
INSERT [dbo].[Korisnik] ([idKorisnika], [Ime], [Prezime], [Korisnickoime], [Lozinka], [Status]) VALUES (3, N'2', N'2', N'2', N'2', NULL)
INSERT [dbo].[Korisnik] ([idKorisnika], [Ime], [Prezime], [Korisnickoime], [Lozinka], [Status]) VALUES (4, N'Nikola', N'Nikolic', N'nnikolic', N'123', NULL)
INSERT [dbo].[Korisnik] ([idKorisnika], [Ime], [Prezime], [Korisnickoime], [Lozinka], [Status]) VALUES (5, N'Jovana', N'Mihajlovic', N'jovmihaj', N'123', NULL)
INSERT [dbo].[Korisnik] ([idKorisnika], [Ime], [Prezime], [Korisnickoime], [Lozinka], [Status]) VALUES (6, N'Petar', N'Lecic', N'petarl', N'123', NULL)
SET IDENTITY_INSERT [dbo].[Korisnik] OFF


INSERT [dbo].[Prevoznik] ( [NazivPrevoznika], [AdresaPrevoznika]) VALUES (N'Banat Trans', N'Beogradska 22, Zrenjanin')
INSERT [dbo].[Prevoznik] ( [NazivPrevoznika], [AdresaPrevoznika]) VALUES (N'Kikinda', N'Mileticeva, Zrenjanin')
INSERT [dbo].[Prevoznik] ( [NazivPrevoznika], [AdresaPrevoznika]) VALUES (N'Lasta', N'Autoput za Niš 4, Beograd')
INSERT [dbo].[Prevoznik] ( [NazivPrevoznika], [AdresaPrevoznika]) VALUES (N'Saobracaj Zabalj', N'Svetog Nikole 36-38, Zabalj')

SET IDENTITY_INSERT [dbo].[Voznja] ON 

INSERT [dbo].[Voznja] ([idVoznje], [NazivPrevoznika], [Datum], [Vreme], [MestoPolaska], [MestoDolaska]) VALUES (1, N'Banat Trans', CAST(N'2024-5-08' AS Date), CAST(N'10:00:00' AS time), N'Zrenjanin', N'Beograd')
INSERT [dbo].[Voznja] ([idVoznje], [NazivPrevoznika], [Datum], [Vreme], [MestoPolaska], [MestoDolaska]) VALUES (2, N'Kikinda', CAST(N'2024-8-21' AS Date),CAST(N'12:00:00' AS time), N'Kikinda', N'Zrenjanin')
INSERT [dbo].[Voznja] ([idVoznje], [NazivPrevoznika], [Datum], [Vreme], [MestoPolaska], [MestoDolaska]) VALUES (3, N'Banat Trans', CAST(N'2024-2-18' AS Date), CAST(N'19:00:00' AS time), N'Zrenjanin', N'Novi Sad')
INSERT [dbo].[Voznja] ([idVoznje], [NazivPrevoznika], [Datum], [Vreme], [MestoPolaska], [MestoDolaska]) VALUES (4, N'Lasta', CAST(N'2023-12-18' AS Date),CAST(N'23:15:00' AS time), N'Beograd', N'Zrenjanin')
INSERT [dbo].[Voznja] ([idVoznje], [NazivPrevoznika], [Datum], [Vreme], [MestoPolaska], [MestoDolaska]) VALUES (5, N'Saobracaj Zabalj', CAST(N'2023-12-31' AS Date), CAST(N'12:30:00' AS time), N'Zrenjanin', N'Žabalj')
INSERT [dbo].[Voznja] ([idVoznje], [NazivPrevoznika], [Datum], [Vreme], [MestoPolaska], [MestoDolaska]) VALUES (6, N'Kikinda', CAST(N'2024-3-21' AS Date),CAST(N'15:00:00' AS time), N'Zrenjanin', N'Kikinda')
INSERT [dbo].[Voznja] ([idVoznje], [NazivPrevoznika], [Datum], [Vreme], [MestoPolaska], [MestoDolaska]) VALUES (7, N'Saobracaj Zabalj', CAST(N'2023-11-08' AS Date), CAST(N'08:30:00' AS time), N'Zrenjanin', N'Novi Sad')
INSERT [dbo].[Voznja] ([idVoznje], [NazivPrevoznika], [Datum], [Vreme], [MestoPolaska], [MestoDolaska]) VALUES (8, N'Lasta', CAST(N'2024-01-18' AS Date),CAST(N'18:15:00' AS time), N'Beograd', N'Zrenjanin')
SET IDENTITY_INSERT [dbo].[Voznja] OFF
GO

INSERT [dbo].[StatusRezervacije] ([Status]) VALUES ('Otkazano')

INSERT [dbo].[Rezervacija] ([idKorisnika], [idVoznje]) VALUES (4,1)
INSERT [dbo].[Rezervacija] ([idKorisnika], [idVoznje], [Status]) VALUES (5,2, 'Otkazano')
INSERT [dbo].[Rezervacija] ([idKorisnika], [idVoznje]) VALUES (6,3)
INSERT [dbo].[Rezervacija] ([idKorisnika], [idVoznje]) VALUES (4,4)
INSERT [dbo].[Rezervacija] ([idKorisnika], [idVoznje]) VALUES (6,6)
INSERT [dbo].[Rezervacija] ([idKorisnika], [idVoznje], [Status]) VALUES (5,7, 'Otkazano')
INSERT [dbo].[Rezervacija] ([idKorisnika], [idVoznje]) VALUES (4,7)

