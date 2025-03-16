USE [InventoryDB]
GO
/****** Object:  StoredProcedure [dbo].[UpdateProduct]    Script Date: 15/03/2025 12:22:25 ******/
DROP PROCEDURE IF EXISTS [dbo].[UpdateProduct]
GO
/****** Object:  StoredProcedure [dbo].[GetProducts]    Script Date: 15/03/2025 12:22:25 ******/
DROP PROCEDURE IF EXISTS [dbo].[GetProducts]
GO
/****** Object:  StoredProcedure [dbo].[GetProductById]    Script Date: 15/03/2025 12:22:25 ******/
DROP PROCEDURE IF EXISTS [dbo].[GetProductById]
GO
/****** Object:  StoredProcedure [dbo].[DeleteProduct]    Script Date: 15/03/2025 12:22:25 ******/
DROP PROCEDURE IF EXISTS [dbo].[DeleteProduct]
GO
/****** Object:  StoredProcedure [dbo].[AddProduct]    Script Date: 15/03/2025 12:22:25 ******/
DROP PROCEDURE IF EXISTS [dbo].[AddProduct]
GO
/****** Object:  StoredProcedure [dbo].[AddProduct]    Script Date: 15/03/2025 12:22:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- Insert a product
CREATE PROCEDURE [dbo].[AddProduct]
    @ProductName NVARCHAR(100),
    @Price DECIMAL(18,2),
    @Quantity INT
AS
BEGIN
    INSERT INTO Products (ProductName, Price, Quantity) 
    VALUES (@ProductName, @Price, @Quantity);
    SELECT SCOPE_IDENTITY() AS ProductID;
END;

GO
/****** Object:  StoredProcedure [dbo].[DeleteProduct]    Script Date: 15/03/2025 12:22:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Delete a product
CREATE PROCEDURE [dbo].[DeleteProduct]
    @ProductID INT
AS
BEGIN
    DELETE FROM Products WHERE ProductID = @ProductID;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetProductById]    Script Date: 15/03/2025 12:22:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetProductById]
    @ProductID INT
AS
BEGIN
    SELECT * FROM Products WHERE ProductID = @ProductID;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetProducts]    Script Date: 15/03/2025 12:22:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Get all products
CREATE PROCEDURE [dbo].[GetProducts]
AS
BEGIN
    SELECT * FROM Products;
END;

GO
/****** Object:  StoredProcedure [dbo].[UpdateProduct]    Script Date: 15/03/2025 12:22:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- Update a product
CREATE PROCEDURE [dbo].[UpdateProduct]
    @ProductID INT,
    @ProductName NVARCHAR(100),
    @Price DECIMAL(18,2),
    @Quantity INT
AS
BEGIN
    UPDATE Products 
    SET ProductName = @ProductName, Price = @Price, Quantity = @Quantity 
    WHERE ProductID = @ProductID;
END;

GO
