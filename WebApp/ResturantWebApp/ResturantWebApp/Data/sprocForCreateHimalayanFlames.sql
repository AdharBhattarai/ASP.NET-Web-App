Create Database HimalayanFlames

CREATE TABLE User (
	UserID int IDENTITY(1,1) PRIMARY KEY,
	FirstName nvarchar(60) NOT NULL,
	LastName nvarchar(60) NOT NULL,
	Email nvarchar(60) NOT NULL UNIQUE,
	Password nvarchar(60) NOT NULL,
	Phone nvarchar(15) NOT NULL,
	RoleID int NOT NULL 
		CONSTRAINT User_Role FOREIGN KEY
		REFERENCES Role(RoleID),
	[Salt] [nvarchar](max) NULL,

)

CREATE TABLE [dbo].[Role](
	[RoleID] [int] IDENTITY(1,1) NOT NULL Primary KEY,
	[Name] [nvarchar](42) NOT NULL,
	[CategoryAdd] [bit] NOT NULL,
	[CategoryEdit] [bit] NOT NULL,
	[CategoryDelete] [bit] NULL,
	[CategoryIndex] [bit] NULL,
	[MenuCreate] [bit] NULL,
	[MenuEdit] [bit] NULL,
	[MenuDelete] [bit] NULL,
	[OrderIndex] [bit]
	
	)

CREATE TABLE Category (
	CategoryID int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(60) NOT NULL,
	Description nvarchar(200) NOT NULL,
)

CREATE TABLE Menu(
	MenuID int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(60) NOT NULL,
	Description nvarchar(200) NOT NULL,
	Price decimal(12,2) NOT NULL,
	CategoryID int NOT NULL 
		CONSTRAINT Menu_Category FOREIGN KEY
		REFERENCES Category(CategoryID)
)

CREATE TABLE OrderStatus(
	OrderStatusID int IDENTITY(1,1) PRIMARY KEY,
	OrderTime datetime NOT NULL,
	AccountID int NOT NULL 
		CONSTRAINT OrderStatus_Account FOREIGN KEY
		REFERENCES Account(AccountID)
)

CREATE TABLE MenuOrder(
	MenuOrderID int IDENTITY(1,1) PRIMARY KEY,
	Quantity int NOT NULL,
	ItemPrice decimal(12,2) NOT NULL,
	TotalPrice decimal(12,2) NOT NULL,
	Comment nvarchar(100),
	MenuID int NOT NULL
	CONSTRAINT MenuOrder_Menu FOREIGN KEY
	REFERENCES Menu(MenuID),
	OrderStatus int NOT NULL
	CONSTRAINT MenuOrder_OrderStatus FOREIGN KEY
	REFERENCES OrderStatus(OrderStatusID),
)