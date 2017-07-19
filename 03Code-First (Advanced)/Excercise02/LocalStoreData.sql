USE [LocalStore]
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([ProductId], [ProductName], [Distributor], [Description], [Price]) VALUES (1, N'Spoko', N'Pobeda AD', N'Dark Chocolate Waffle', CAST(0.60 AS Decimal(18, 2)))
INSERT [dbo].[Products] ([ProductId], [ProductName], [Distributor], [Description], [Price]) VALUES (2, N'Everest', N'Pobeda Ad', N'Chocolate Biscuits', CAST(1.20 AS Decimal(18, 2)))
INSERT [dbo].[Products] ([ProductId], [ProductName], [Distributor], [Description], [Price]) VALUES (3, N'Rakia', N'Vinprom Karnobat', N'Grozdova', CAST(6.20 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[Products] OFF
