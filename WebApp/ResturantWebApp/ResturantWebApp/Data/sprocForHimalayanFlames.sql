--//Create Category

CREATE PROCEDURE [dbo].[sproc_CategoryAdd]
@CategoryID int OUTPUT,
@Name nvarchar(60),
@Description nvarchar(200)
AS
     INSERT INTO Category(Name, Description)
               VALUES(@Name, @Description)
     SET @CategoryID = @@IDENTITY
GO

--// Remove Category

CREATE PROCEDURE [dbo].[sproc_CategoryRemove]
@CategoryID int
AS
BEGIN
     DELETE FROM Category
          WHERE CategoryID = @CategoryID

     -- Return -1 if we had an error
     IF @@ERROR > 0
     BEGIN
          RETURN -1
     END
     ELSE
     BEGIN
          RETURN 1
     END
END
GO

--//Category Update

CREATE PROCEDURE [dbo].[sproc_CategoryUpdate]
@CategoryID int,
@Name nvarchar(60),
@Description nvarchar(200)
AS
	UPDATE Category
		SET
			Name=@Name,
			Description=@Description
            WHERE CategoryID= @CategoryID
GO

--// Categokry Get

CREATE PROCEDURE [dbo].[sprocCategoryGet]
@CategoryID int
AS
BEGIN
     SET NOCOUNT ON;

     SELECT * FROM Category
     WHERE CategoryID = @CategoryID
END
GO

--// Category GetAll

CREATE PROCEDURE [dbo].[sprocCategoryGetAll]
AS
BEGIN
     
     SET NOCOUNT ON;		

     SELECT * FROM Category
END

GO


--//Menu

--// Create Menu Get

CREATE PROCEDURE [dbo].[sprocMenuGet]
@MenuID int
AS
BEGIN
     SET NOCOUNT ON;

     SELECT * FROM Menu
     WHERE MenuID = @MenuID
END
GO

--//Menu Remove

CREATE PROCEDURE [dbo].[sproc_MenuRemove]
@MenuID int
AS
BEGIN
     DELETE FROM Menu
          WHERE MenuID = @MenuID

     -- Return -1 if we had an error
     IF @@ERROR > 0
     BEGIN
          RETURN -1
     END
     ELSE
     BEGIN
          RETURN 1
     END
END

GO

--// Create MenuUpdate


CREATE PROCEDURE [dbo].[sproc_MenuUpdate]
@MenuID int,
@Name nvarchar(60),
@Description nvarchar(200),
@Price decimal(12,2),
@CategoryID int
AS
	UPDATE Menu
		SET
			Name=@Name,
			Description=@Description,
		    Price= @Price, 
			CategoryID= @CategoryID
			WHERE MenuID=@MenuID
GO

--// MenuAdd

CREATE PROCEDURE [dbo].[sproc_MenuAdd]
@MenuID int OUTPUT,
@Name nvarchar(60),
@Description nvarchar(200),
@Price decimal(12,2),
@CategoryID int
AS
     INSERT INTO Menu(Name, Description, Price, CategoryID)
               VALUES(@Name, @Description, @Price, @CategoryID)
     SET @MenuID = @@IDENTITY
GO

--// Menu Get

CREATE PROCEDURE [dbo].[sprocMenuGetAll]
AS
BEGIN
     
     SET NOCOUNT ON;		

     SELECT * FROM Menu
END


GO

--// Get Menu for Category

CREATE PROCEDURE [dbo].[sprocMenuGetForCategory]
@CategoryID int
AS
BEGIN
     -- SET NOCOUNT ON added to prevent extra result sets from
     -- interfering with SELECT statements.
     SET NOCOUNT ON;

     SELECT * FROM Menu
		WHERE CategoryID = @CategoryID
END
GO

--// Role Get

CREATE PROCEDUre [dbo].[sprocRoleGet]
@RoleID int
AS 
BEGIN
	SET NOCOUNT ON;
	
	Select * From Role
	Where RoleID = @RoleID
	END
GO

--// GetUserByEmail

CREATE Procedure [dbo].[sprocUserGetByEmail]
@Email nvarchar(60)
AS 
BEGIN
	 SET NOCOUNT ON;
	 
	 SELECT * FROM [User]
	 Where  Email = @Email
	 END
GO

--// User Get

CREATE Procedure [dbo].[sprocUserGet]
@UserID int
AS 
BEGIN
	 SET NOCOUNT ON;
	 
	 SELECT * FROM [User]
	 Where UserID= @UserID
	 END
GO

--//User Get All

CREATE PROCEDURE [dbo].[sprocUserGetAll]
AS
BEGIN
     
     SET NOCOUNT ON;

     SELECT * FROM [User]
END
GO

--// OrderStatusGetAll

CREATE PROCEDURE [dbo].[sprocOrderStatusGetAll]
AS
BEGIN
     
     SET NOCOUNT ON;		

     SELECT * FROM OrderStatus
END
GO

--// OrderStatusGet


CREATE PROCEDURE [dbo].[sprocOrderStatusGet]
@OrderStatusID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM OrderStatus
	WHERE OrderStatusID=@OrderStatusID
END
GO

--User Create

CREATE PROCEDURE [dbo].[sproc_UserAdd]
@UserID int OUTPUT,
@FirstName nvarchar(60),
@LastName nvarchar(60),
@Email nvarchar(60),
@Password nvarchar(max),
@Phone nvarchar(15),
@Salt nvarchar(max),
@RoleID int
AS
     INSERT INTO [User](FirstName,LastName,Email,Password,Salt, Phone, RoleID)
               VALUES(@FirstName,@LastName,@Email,@Password,@Salt, @Phone, @RoleID)
     SET @UserID = @@IDENTITY
GO

--//MenuOrderGetForOrderStatus

CREATE PROCEDURE [dbo].[sprocMenuOrderGetForOrderStatus]
@OrderStatusID int
AS
BEGIN
     -- SET NOCOUNT ON added to prevent extra result sets from
     -- interfering with SELECT statements.
     SET NOCOUNT ON;

     SELECT * FROM MenuOrder
		WHERE OrderStatusID = @OrderStatusID	
END
GO

