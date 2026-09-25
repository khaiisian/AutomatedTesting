-- Schema script for database [TaskManagementDb]
-- Generated 2026-09-25 14:24 from OTGEXPERTBOOKB5\SQLEXPRESS
IF DB_ID(N'TaskManagementDb') IS NULL CREATE DATABASE [TaskManagementDb];
GO
USE [TaskManagementDb];
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TaskItems]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TaskItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Description] [nvarchar](1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Iscompleted] [bit] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__Tasks__Iscomplet__4AB81AF0]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TaskItems] ADD  DEFAULT ((0)) FOR [Iscompleted]
END

GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__Tasks__CreatedAt__4BAC3F29]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[TaskItems] ADD  DEFAULT (getdate()) FOR [CreatedAt]
END

GO
