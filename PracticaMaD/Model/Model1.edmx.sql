
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/06/2021 17:46:17
-- Generated from EDMX file: E:\ALAN\Projects\4curso\mad\MaDTurtles\PracticaMaD\Model\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [PracticaMaD];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ImageExif]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Exifs] DROP CONSTRAINT [FK_ImageExif];
GO
IF OBJECT_ID(N'[dbo].[FK_ImageCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Images] DROP CONSTRAINT [FK_ImageCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_UserFollows]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Follows] DROP CONSTRAINT [FK_UserFollows];
GO
IF OBJECT_ID(N'[dbo].[FK_UserLike]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Likes] DROP CONSTRAINT [FK_UserLike];
GO
IF OBJECT_ID(N'[dbo].[FK_ImageLike]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Likes] DROP CONSTRAINT [FK_ImageLike];
GO
IF OBJECT_ID(N'[dbo].[FK_UserComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK_UserComment];
GO
IF OBJECT_ID(N'[dbo].[FK_ImageComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK_ImageComment];
GO
IF OBJECT_ID(N'[dbo].[FK_UserImage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Images] DROP CONSTRAINT [FK_UserImage];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Images]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Images];
GO
IF OBJECT_ID(N'[dbo].[Exifs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Exifs];
GO
IF OBJECT_ID(N'[dbo].[Categories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Categories];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Follows]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Follows];
GO
IF OBJECT_ID(N'[dbo].[Likes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Likes];
GO
IF OBJECT_ID(N'[dbo].[Comments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Comments];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Images'
CREATE TABLE [dbo].[Images] (
    [imgId] bigint IDENTITY(1,1) NOT NULL,
    [title] nvarchar(100)  NOT NULL,
    [description] nvarchar(2200)  NULL,
    [uploadDate] datetime  NOT NULL,
    [categoryId] int  NOT NULL,
    [img] varbinary(max)  NULL,
    [path] nvarchar(20)  NULL,
    [userId] bigint  NOT NULL,
    [Category_categoryId] int  NOT NULL
);
GO

-- Creating table 'Exifs'
CREATE TABLE [dbo].[Exifs] (
    [infoType] nvarchar(5)  NOT NULL,
    [value] decimal(18,0)  NOT NULL,
    [imgId] bigint  NOT NULL
);
GO

-- Creating table 'Categories'
CREATE TABLE [dbo].[Categories] (
    [categoryId] int  NOT NULL,
    [category] nvarchar(20)  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [userId] bigint IDENTITY(1,1) NOT NULL,
    [userName] nvarchar(20)  NOT NULL,
    [password] nvarchar(60)  NOT NULL,
    [firstName] nvarchar(60)  NOT NULL,
    [lastName1] nvarchar(60)  NOT NULL,
    [lastName2] nvarchar(60)  NULL,
    [email] nvarchar(100)  NOT NULL,
    [country] nvarchar(30)  NOT NULL,
    [language] nvarchar(30)  NOT NULL
);
GO

-- Creating table 'Follows'
CREATE TABLE [dbo].[Follows] (
    [userId] bigint  NOT NULL,
    [followedId] bigint  NOT NULL
);
GO

-- Creating table 'Likes'
CREATE TABLE [dbo].[Likes] (
    [userId] bigint  NOT NULL,
    [imgId] bigint  NOT NULL
);
GO

-- Creating table 'Comments'
CREATE TABLE [dbo].[Comments] (
    [commentId] bigint IDENTITY(1,1) NOT NULL,
    [userId] bigint  NOT NULL,
    [imgId] bigint  NOT NULL,
    [comment] nvarchar(2200)  NOT NULL,
    [uploadDate] datetime  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [imgId] in table 'Images'
ALTER TABLE [dbo].[Images]
ADD CONSTRAINT [PK_Images]
    PRIMARY KEY CLUSTERED ([imgId] ASC);
GO

-- Creating primary key on [infoType], [imgId] in table 'Exifs'
ALTER TABLE [dbo].[Exifs]
ADD CONSTRAINT [PK_Exifs]
    PRIMARY KEY CLUSTERED ([infoType], [imgId] ASC);
GO

-- Creating primary key on [categoryId] in table 'Categories'
ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [PK_Categories]
    PRIMARY KEY CLUSTERED ([categoryId] ASC);
GO

-- Creating primary key on [userId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([userId] ASC);
GO

-- Creating primary key on [followedId], [userId] in table 'Follows'
ALTER TABLE [dbo].[Follows]
ADD CONSTRAINT [PK_Follows]
    PRIMARY KEY CLUSTERED ([followedId], [userId] ASC);
GO

-- Creating primary key on [userId], [imgId] in table 'Likes'
ALTER TABLE [dbo].[Likes]
ADD CONSTRAINT [PK_Likes]
    PRIMARY KEY CLUSTERED ([userId], [imgId] ASC);
GO

-- Creating primary key on [commentId] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [PK_Comments]
    PRIMARY KEY CLUSTERED ([commentId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [imgId] in table 'Exifs'
ALTER TABLE [dbo].[Exifs]
ADD CONSTRAINT [FK_ImageExif]
    FOREIGN KEY ([imgId])
    REFERENCES [dbo].[Images]
        ([imgId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ImageExif'
CREATE INDEX [IX_FK_ImageExif]
ON [dbo].[Exifs]
    ([imgId]);
GO

-- Creating foreign key on [Category_categoryId] in table 'Images'
ALTER TABLE [dbo].[Images]
ADD CONSTRAINT [FK_ImageCategory]
    FOREIGN KEY ([Category_categoryId])
    REFERENCES [dbo].[Categories]
        ([categoryId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ImageCategory'
CREATE INDEX [IX_FK_ImageCategory]
ON [dbo].[Images]
    ([Category_categoryId]);
GO

-- Creating foreign key on [userId] in table 'Follows'
ALTER TABLE [dbo].[Follows]
ADD CONSTRAINT [FK_UserFollows]
    FOREIGN KEY ([userId])
    REFERENCES [dbo].[Users]
        ([userId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserFollows'
CREATE INDEX [IX_FK_UserFollows]
ON [dbo].[Follows]
    ([userId]);
GO

-- Creating foreign key on [userId] in table 'Likes'
ALTER TABLE [dbo].[Likes]
ADD CONSTRAINT [FK_UserLike]
    FOREIGN KEY ([userId])
    REFERENCES [dbo].[Users]
        ([userId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [imgId] in table 'Likes'
ALTER TABLE [dbo].[Likes]
ADD CONSTRAINT [FK_ImageLike]
    FOREIGN KEY ([imgId])
    REFERENCES [dbo].[Images]
        ([imgId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ImageLike'
CREATE INDEX [IX_FK_ImageLike]
ON [dbo].[Likes]
    ([imgId]);
GO

-- Creating foreign key on [userId] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK_UserComment]
    FOREIGN KEY ([userId])
    REFERENCES [dbo].[Users]
        ([userId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserComment'
CREATE INDEX [IX_FK_UserComment]
ON [dbo].[Comments]
    ([userId]);
GO

-- Creating foreign key on [imgId] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK_ImageComment]
    FOREIGN KEY ([imgId])
    REFERENCES [dbo].[Images]
        ([imgId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ImageComment'
CREATE INDEX [IX_FK_ImageComment]
ON [dbo].[Comments]
    ([imgId]);
GO

-- Creating foreign key on [userId] in table 'Images'
ALTER TABLE [dbo].[Images]
ADD CONSTRAINT [FK_UserImage]
    FOREIGN KEY ([userId])
    REFERENCES [dbo].[Users]
        ([userId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserImage'
CREATE INDEX [IX_FK_UserImage]
ON [dbo].[Images]
    ([userId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------