IF DB_ID('API-SHOP-2') IS NOT NULL
begin
	USE master
	drop database API-SHOP-2
end
GO
CREATE DATABASE [API-SHOP-2]
GO
USE [API-SHOP-2]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Carts](
	[carts_id] [int] NOT NULL,
	[id_user] [int] NOT NULL,
	[item_id] [int] NOT NULL,
	[quantity] [int] NOT NULL,
	[is_deleted] [bit] NOT NULL,
 CONSTRAINT [PK_Carts] PRIMARY KEY CLUSTERED 
(
	[carts_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[category_id] [int] NOT NULL,
	[category_name] [nvarchar](50) NOT NULL,
	[is_deleted] [bit] NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[category_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[id_user] [int] NOT NULL,
	[order_id] [int] NOT NULL,
	[delivery_id] [int] NOT NULL,
	[status_id] [int] NOT NULL,
	[date] [date] NOT NULL,
	[is_deleted] [bit] NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[order_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders_item](
	[order_item_id] [int] NOT NULL,
	[order_id] [int] NOT NULL,
	[item_id] [int] NOT NULL,
	[quantity] [int] NOT NULL,
	[is_deleted] [bit] NOT NULL,
 CONSTRAINT [PK_Orders_item] PRIMARY KEY CLUSTERED 
(
	[order_item_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[item_id] [int] NOT NULL,
	[item_name] [nvarchar](50) NOT NULL,
	[item_description] [nvarchar](100) NULL,
	[warehouse_quantity] [int] NOT NULL,
	[price] [int] NULL,
	[category_id] [int] NULL,
	[is_deleted] [bit] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[item_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[role_id] [int] NOT NULL,
	[role_name] [nvarchar](50) NOT NULL,
	[is_deleted] [bit] NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[role_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Specs](
	[spec_id] [int] NOT NULL,
	[category_id] [int] NOT NULL,
	[spec_name] [nvarchar](50) NOT NULL,
	[is_deleted] [bit] NOT NULL,
 CONSTRAINT [PK_Specs] PRIMARY KEY CLUSTERED 
(
	[spec_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Specs_item](
	[spec_item_id] [int] NOT NULL,
	[item_id] [int] NOT NULL,
	[spec_id] [int] NOT NULL,
	[spec_value] [nvarchar](50) NULL,
	[is_deleted] [bit] NOT NULL,
 CONSTRAINT [PK_Specs_item] PRIMARY KEY CLUSTERED 
(
	[spec_item_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[id_user] [int] NOT NULL,
	[login] [nvarchar](50) NOT NULL,
	[password] [nvarchar](50) NOT NULL,
	[role_id] [int] NOT NULL,
	[address] [nvarchar](50) NOT NULL,
	[is_deleted] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[id_user] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Carts]  WITH CHECK ADD  CONSTRAINT [FK_ItemsCarts] FOREIGN KEY([item_id])
REFERENCES [dbo].[Products] ([item_id])
GO
ALTER TABLE [dbo].[Carts] CHECK CONSTRAINT [FK_ItemsCarts]
GO
ALTER TABLE [dbo].[Carts]  WITH CHECK ADD  CONSTRAINT [FK_UserCarts] FOREIGN KEY([id_user])
REFERENCES [dbo].[Users] ([id_user])
GO
ALTER TABLE [dbo].[Carts] CHECK CONSTRAINT [FK_UserCarts]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_UsersOrders] FOREIGN KEY([id_user])
REFERENCES [dbo].[Users] ([id_user])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_UsersOrders]
GO
ALTER TABLE [dbo].[Orders_item]  WITH CHECK ADD  CONSTRAINT [FK_ItemsOrders_item] FOREIGN KEY([item_id])
REFERENCES [dbo].[Products] ([item_id])
GO
ALTER TABLE [dbo].[Orders_item] CHECK CONSTRAINT [FK_ItemsOrders_item]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Categories] FOREIGN KEY([category_id])
REFERENCES [dbo].[Categories] ([category_id])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Categories]
GO
ALTER TABLE [dbo].[Specs]  WITH CHECK ADD  CONSTRAINT [FK_Specs_Categories] FOREIGN KEY([category_id])
REFERENCES [dbo].[Categories] ([category_id])
GO
ALTER TABLE [dbo].[Specs] CHECK CONSTRAINT [FK_Specs_Categories]
GO
ALTER TABLE [dbo].[Specs_item]  WITH CHECK ADD  CONSTRAINT [FK_ItemsSpecs_item] FOREIGN KEY([item_id])
REFERENCES [dbo].[Products] ([item_id])
GO
ALTER TABLE [dbo].[Specs_item] CHECK CONSTRAINT [FK_ItemsSpecs_item]
GO
ALTER TABLE [dbo].[Specs_item]  WITH CHECK ADD  CONSTRAINT [FK_SpecsSpecs_item] FOREIGN KEY([spec_id])
REFERENCES [dbo].[Specs] ([spec_id])
GO
ALTER TABLE [dbo].[Specs_item] CHECK CONSTRAINT [FK_SpecsSpecs_item]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles] FOREIGN KEY([role_id])
REFERENCES [dbo].[Roles] ([role_id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles]
GO