SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE';
DROP TABLE Product;
DROP TABLE Orders;
DROP TABLE Products;
DROP TABLE Categories;
DROP TABLE Users;

DROP TABLE [User];
DROP TABLE [Order];


CREATE TABLE Category (
    CategoryId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL
);

CREATE TABLE Product (
    ProductId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(255) NOT NULL,
    Description NVARCHAR(1000),
    Price DECIMAL(18,2) NOT NULL,
    ImageUrl NVARCHAR(255),
    CategoryId INT,
    FOREIGN KEY (CategoryId) REFERENCES Category(CategoryId)
);

CREATE TABLE [User] (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(255) NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255)
);

CREATE TABLE [Order] (
    OrderId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT,
    OrderDate DATETIME NOT NULL DEFAULT GETDATE(),
    Total DECIMAL(18,2) NOT NULL,
    FOREIGN KEY (UserId) REFERENCES [User](UserId)
);

CREATE TABLE OrderDetail (
    OrderDetailId INT PRIMARY KEY IDENTITY(1,1),
    OrderId INT,
    ProductId INT,
    Quantity INT NOT NULL,
    Price DECIMAL(18,2) NOT NULL,
    FOREIGN KEY (OrderId) REFERENCES [Order](OrderId),
    FOREIGN KEY (ProductId) REFERENCES Product(ProductId)
);


INSERT INTO Categories (Name) VALUES 
(N'Điện tử'),
(N'Thời trang'),
(N'Sách'),
(N'Mỹ phẩm'),
(N'Nội thất');

INSERT INTO Products (Name, Description, Price, ImageUrl, CategoryId) VALUES 
(N'TV Samsung', N'TV 4K Ultra HD', 15000000, N'samsung_tv.jpg', 1),
(N'Áo thun Nam', N'Áo thun cotton', 200000, N'ao_thun_nam.jpg', 2),
(N'Sách Lập trình', N'Sách học lập trình C#', 300000, N'sach_laptrinh.jpg', 3),
(N'Son môi', N'Son môi đỏ tươi', 250000, N'son_moi.jpg', 4),
(N'Giường gỗ', N'Giường gỗ sồi', 5000000, N'giuong_go.jpg', 5);

INSERT INTO [Users] (Username, Password, Email) VALUES 
(N'admin', N'admin123', N'admin@website.com'),
(N'user1', N'user123', N'user1@website.com'),
(N'user2', N'user123', N'user2@website.com'),
(N'user3', N'user123', N'user3@website.com'),
(N'user4', N'user123', N'user4@website.com');


INSERT INTO [Orders] (UserId, OrderDate, Total) VALUES 
(1, '2025-04-01', 15500000),
(2, '2025-04-02', 450000),
(3, '2025-04-03', 300000),
(4, '2025-04-04', 250000),
(5, '2025-04-05', 5000000);

INSERT INTO OrderDetail (OrderId, ProductId, Quantity, Price) VALUES 
(1, 1, 1, 15000000),   -- Đơn hàng 1, Sản phẩm TV Samsung
(2, 2, 2, 200000),     -- Đơn hàng 2, Sản phẩm Áo thun Nam
(3, 3, 1, 300000),     -- Đơn hàng 3, Sản phẩm Sách Lập trình
(4, 4, 1, 250000),     -- Đơn hàng 4, Sản phẩm Son môi
(5, 5, 1, 5000000);    -- Đơn hàng 5, Sản phẩm Giường gỗ


SELECT * FROM Category;
SELECT * FROM Product;
SELECT * FROM [User];
SELECT * FROM [Order];
SELECT * FROM OrderDetail;
