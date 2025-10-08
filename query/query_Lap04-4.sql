USE [ProductOrder]
GO
/****** Object: Table [dbo].[Invoice] Script Date: 07/04/2020 23:13:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoice](
    [InvoiceNo] [nvarchar](20) NOT NULL,
    [OrderDate] [datetime] NOT NULL,
    [DeliveryDate] [datetime] NOT NULL,
    [Note] [nvarchar](255) NULL,
    CONSTRAINT [PK_Invoice] PRIMARY KEY CLUSTERED 
(
    [InvoiceNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object: Table [dbo].[Product] Script Date: 07/04/2020 23:13:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
    [ProductID] [nvarchar](20) NOT NULL,
    [ProductName] [nvarchar](100) NOT NULL,
    [Unit] [nvarchar](20) NOT NULL,
    [BuyPrice] [decimal](18, 0) NULL,
    [SellPrice] [decimal](18, 0) NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
    [ProductID] ASC
)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object: Table [dbo].[Order] Script Date: 07/04/2020 23:13:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
    [InvoiceNo] [nvarchar](20) NOT NULL,
    [No] [int] NOT NULL,
    [ProductID] [nvarchar](20) NOT NULL,
    [ProductName] [nvarchar](100) NULL,
    [Unit] [nvarchar](20) NULL,
    [Price] [decimal](18, 0) NOT NULL,
    [Quantity] [int] NOT NULL,
    CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
    [InvoiceNo] ASC,
    [No] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object: ForeignKey [FK_Order_Invoice] Script Date: 07/04/2020 23:13:15 ******/
ALTER TABLE [dbo].[Order]  WITH CHECK ADD CONSTRAINT [FK_Order_Invoice] FOREIGN KEY([InvoiceNo])
REFERENCES [dbo].[Invoice] ([InvoiceNo])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Invoice]
GO

/****** Object: ForeignKey [FK_Order_Product] Script Date: 07/04/2020 23:13:15 ******/
ALTER TABLE [dbo].[Order] WITH CHECK ADD CONSTRAINT [FK_Order_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ProductID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Product]
GO

USE [ProductOrder]
GO

/****** Object: Table [dbo].[Invoice] Script Date: 07/04/2020 23:14:16 ******/
INSERT [dbo].[Invoice] ([InvoiceNo], [OrderDate], [DeliveryDate], [Note]) VALUES (N'HDX001', CAST(0x0000AAB900000000 AS DateTime), CAST(0x0000AADA00000000 AS DateTime), N'Giao hàng trước 9h')
INSERT [dbo].[Invoice] ([InvoiceNo], [OrderDate], [DeliveryDate], [Note]) VALUES (N'HDX002', CAST(0x0000AADA00000000 AS DateTime), CAST(0x0000AADA00000000 AS DateTime), N'Gọi điện trước khi giao')
INSERT [dbo].[Invoice] ([InvoiceNo], [OrderDate], [DeliveryDate], [Note]) VALUES (N'HDX003', CAST(0x0000AADA00000000 AS DateTime), CAST(0x0000AADC00000000 AS DateTime), N'Giao từ 1-3h')
GO

/****** Object: Table [dbo].[Product] Script Date: 07/04/2020 23:14:16 ******/
INSERT [dbo].[Product] ([ProductID], [ProductName], [Unit], [BuyPrice], [SellPrice]) VALUES (N'Product1', N'Sản phẩm 1', N'Cái', CAST(100000 AS Decimal(18, 0)), CAST(120000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ProductID], [ProductName], [Unit], [BuyPrice], [SellPrice]) VALUES (N'Product2', N'Sản phẩm 2', N'Cái', CAST(50000 AS Decimal(18, 0)), CAST(120000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ProductID], [ProductName], [Unit], [BuyPrice], [SellPrice]) VALUES (N'Product3', N'Sản phẩm 3', N'Cái', CAST(40000 AS Decimal(18, 0)), CAST(70000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ProductID], [ProductName], [Unit], [BuyPrice], [SellPrice]) VALUES (N'Product4', N'Sản phẩm 4', N'Hộp', CAST(200000 AS Decimal(18, 0)), CAST(300000 AS Decimal(18, 0)))
GO

/****** Object: Table [dbo].[Order] Script Date: 07/04/2020 23:14:16 ******/
INSERT [dbo].[Order] ([InvoiceNo], [No], [ProductID], [ProductName], [Unit], [Price], [Quantity]) VALUES (N'HDX001', 1, N'Product1', N'Sản phẩm 1', N'Cái', CAST(120000 AS Decimal(18, 0)), 20)
INSERT [dbo].[Order] ([InvoiceNo], [No], [ProductID], [ProductName], [Unit], [Price], [Quantity]) VALUES (N'HDX001', 2, N'Product2', N'Sản phẩm 2', N'Cái', CAST(120000 AS Decimal(18, 0)), 4)
INSERT [dbo].[Order] ([InvoiceNo], [No], [ProductID], [ProductName], [Unit], [Price], [Quantity]) VALUES (N'HDX001', 3, N'Product4', N'Sản phẩm 4', N'Hộp', CAST(300000 AS Decimal(18, 0)), 10)
INSERT [dbo].[Order] ([InvoiceNo], [No], [ProductID], [ProductName], [Unit], [Price], [Quantity]) VALUES (N'HDX002', 1, N'Product4', N'Sản phẩm 1', N'Hộp', CAST(300000 AS Decimal(18, 0)), 10)
INSERT [dbo].[Order] ([InvoiceNo], [No], [ProductID], [ProductName], [Unit], [Price], [Quantity]) VALUES (N'HDX002', 2, N'Product2', N'Sản phẩm 3', N'Cái', CAST(300000 AS Decimal(18, 0)), 12)
INSERT [dbo].[Order] ([InvoiceNo], [No], [ProductID], [ProductName], [Unit], [Price], [Quantity]) VALUES (N'HDX003', 1, N'Product1', N'Sản phẩm 1', N'Cái', CAST(120000 AS Decimal(18, 0)), 40)
INSERT [dbo].[Order] ([InvoiceNo], [No], [ProductID], [ProductName], [Unit], [Price], [Quantity]) VALUES (N'HDX003', 4, N'Product2', N'Sản phẩm 2', N'Cái', CAST(120000 AS Decimal(18, 0)), 60)
GO

/*thêm sau*/
USE [ProductOrder]
GO

-- XÓA DỮ LIỆU CŨ
DELETE FROM [dbo].[Order];
DELETE FROM [dbo].[Invoice];
DELETE FROM [dbo].[Product];
GO

/****** BẢNG PRODUCT (10 sản phẩm mới) ******/
INSERT [dbo].[Product] ([ProductID], [ProductName], [Unit], [BuyPrice], [SellPrice]) VALUES
(N'PRD001', N'Bàn phím cơ RGB', N'Cái', 800000, 1200000),
(N'PRD002', N'Chuột không dây Logitech', N'Cái', 350000, 550000),
(N'PRD003', N'Màn hình 24 inch LG', N'Cái', 2500000, 3300000),
(N'PRD004', N'Tai nghe Bluetooth Sony', N'Cái', 700000, 950000),
(N'PRD005', N'Ổ cứng SSD 1TB Samsung', N'Cái', 1600000, 2100000),
(N'PRD006', N'Loa Bluetooth JBL Go 4', N'Cái', 900000, 1250000),
(N'PRD007', N'Laptop Acer Aspire 5', N'Cái', 13000000, 15500000),
(N'PRD008', N'Webcam Logitech C920', N'Cái', 1100000, 1500000),
(N'PRD009', N'Bàn làm việc gỗ sồi', N'Cái', 1800000, 2300000),
(N'PRD010', N'Ghế công thái học Sihoo', N'Cái', 2800000, 3500000);
GO

/****** BẢNG INVOICE (5 hóa đơn gần nhất) ******/
INSERT [dbo].[Invoice] ([InvoiceNo], [OrderDate], [DeliveryDate], [Note]) VALUES
(N'INV001', '2025-09-03', '2025-09-04', N'Giao trong buổi sáng'),
(N'INV002', '2025-09-15', '2025-09-16', N'Liên hệ trước khi giao'),
(N'INV003', '2025-10-01', '2025-10-02', N'Giao trong ngày'),
(N'INV004', '2025-10-05', '2025-10-06', N'Khách yêu cầu giao sớm'),
(N'INV005', '2025-10-07', '2025-10-08', N'Giao tận nơi, test sản phẩm trước');
GO

/****** BẢNG ORDER (chi tiết hóa đơn) ******/
INSERT [dbo].[Order] ([InvoiceNo], [No], [ProductID], [ProductName], [Unit], [Price], [Quantity]) VALUES
-- HÓA ĐƠN INV001
(N'INV001', 1, N'PRD001', N'Bàn phím cơ RGB', N'Cái', 1200000, 2),
(N'INV001', 2, N'PRD002', N'Chuột không dây Logitech', N'Cái', 550000, 3),
(N'INV001', 3, N'PRD006', N'Loa Bluetooth JBL Go 4', N'Cái', 1250000, 1),

-- HÓA ĐƠN INV002
(N'INV002', 1, N'PRD003', N'Màn hình 24 inch LG', N'Cái', 3300000, 1),
(N'INV002', 2, N'PRD004', N'Tai nghe Bluetooth Sony', N'Cái', 950000, 2),
(N'INV002', 3, N'PRD005', N'Ổ cứng SSD 1TB Samsung', N'Cái', 2100000, 1),
(N'INV002', 4, N'PRD009', N'Bàn làm việc gỗ sồi', N'Cái', 2300000, 1),

-- HÓA ĐƠN INV003
(N'INV003', 1, N'PRD007', N'Laptop Acer Aspire 5', N'Cái', 15500000, 1),
(N'INV003', 2, N'PRD002', N'Chuột không dây Logitech', N'Cái', 550000, 2),
(N'INV003', 3, N'PRD004', N'Tai nghe Bluetooth Sony', N'Cái', 950000, 1),

-- HÓA ĐƠN INV004
(N'INV004', 1, N'PRD010', N'Ghế công thái học Sihoo', N'Cái', 3500000, 2),
(N'INV004', 2, N'PRD001', N'Bàn phím cơ RGB', N'Cái', 1200000, 1),
(N'INV004', 3, N'PRD008', N'Webcam Logitech C920', N'Cái', 1500000, 1),
(N'INV004', 4, N'PRD006', N'Loa Bluetooth JBL Go 4', N'Cái', 1250000, 2),

-- HÓA ĐƠN INV005
(N'INV005', 1, N'PRD003', N'Màn hình 24 inch LG', N'Cái', 3300000, 2),
(N'INV005', 2, N'PRD005', N'Ổ cứng SSD 1TB Samsung', N'Cái', 2100000, 2),
(N'INV005', 3, N'PRD009', N'Bàn làm việc gỗ sồi', N'Cái', 2300000, 1),
(N'INV005', 4, N'PRD010', N'Ghế công thái học Sihoo', N'Cái', 3500000, 1);
GO
