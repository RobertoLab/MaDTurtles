
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/24/2021 17:42:26
-- Generated from EDMX file: D:\src\4curso\mad\MaDTurtles\PracticaMaD\Model\Photogram.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Photogram];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ImageExif]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Exifs] DROP CONSTRAINT [FK_ImageExif];
GO
IF OBJECT_ID(N'[dbo].[FK_ImageComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK_ImageComment];
GO
IF OBJECT_ID(N'[dbo].[FK_UserImage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Images] DROP CONSTRAINT [FK_UserImage];
GO
IF OBJECT_ID(N'[dbo].[FK_UserComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK_UserComment];
GO
IF OBJECT_ID(N'[dbo].[FK_ImageCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Images] DROP CONSTRAINT [FK_ImageCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_ImageTag_Image]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ImageTag] DROP CONSTRAINT [FK_ImageTag_Image];
GO
IF OBJECT_ID(N'[dbo].[FK_ImageTag_Tag]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ImageTag] DROP CONSTRAINT [FK_ImageTag_Tag];
GO
IF OBJECT_ID(N'[dbo].[FK_Likes_Image]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Likes] DROP CONSTRAINT [FK_Likes_Image];
GO
IF OBJECT_ID(N'[dbo].[FK_Likes_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Likes] DROP CONSTRAINT [FK_Likes_User];
GO
IF OBJECT_ID(N'[dbo].[FK_Follows_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Follows] DROP CONSTRAINT [FK_Follows_User];
GO
IF OBJECT_ID(N'[dbo].[FK_Follows_User1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Follows] DROP CONSTRAINT [FK_Follows_User1];
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
IF OBJECT_ID(N'[dbo].[Comments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Comments];
GO
IF OBJECT_ID(N'[dbo].[Tags]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tags];
GO
IF OBJECT_ID(N'[dbo].[ImageTag]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ImageTag];
GO
IF OBJECT_ID(N'[dbo].[Likes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Likes];
GO
IF OBJECT_ID(N'[dbo].[Follows]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Follows];
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
    [img] varbinary(max)  NULL,
    [path] nvarchar(20)  NULL,
    [userId] bigint  NOT NULL,
    [categoryId] int  NOT NULL
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
    [categoryId] int IDENTITY(1,1) NOT NULL,
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

-- Creating table 'Comments'
CREATE TABLE [dbo].[Comments] (
    [commentId] bigint IDENTITY(1,1) NOT NULL,
    [imgId] bigint  NOT NULL,
    [comment] nvarchar(2200)  NOT NULL,
    [uploadDate] datetime  NOT NULL,
    [userId] bigint  NOT NULL
);
GO

-- Creating table 'Tags'
CREATE TABLE [dbo].[Tags] (
    [tagId] int IDENTITY(1,1) NOT NULL,
    [tag] nvarchar(max)  NOT NULL,
    [imgCount] int  NOT NULL
);
GO

-- Creating table 'ImageTag'
CREATE TABLE [dbo].[ImageTag] (
    [Images_imgId] bigint  NOT NULL,
    [Tags_tagId] int  NOT NULL
);
GO

-- Creating table 'Likes'
CREATE TABLE [dbo].[Likes] (
    [ImagesLiked_imgId] bigint  NOT NULL,
    [UsersLikes_userId] bigint  NOT NULL
);
GO

-- Creating table 'Follows'
CREATE TABLE [dbo].[Follows] (
    [UserFollow_userId] bigint  NOT NULL,
    [Users_userId] bigint  NOT NULL
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

-- Creating primary key on [commentId] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [PK_Comments]
    PRIMARY KEY CLUSTERED ([commentId] ASC);
GO

-- Creating primary key on [tagId] in table 'Tags'
ALTER TABLE [dbo].[Tags]
ADD CONSTRAINT [PK_Tags]
    PRIMARY KEY CLUSTERED ([tagId] ASC);
GO

-- Creating primary key on [Images_imgId], [Tags_tagId] in table 'ImageTag'
ALTER TABLE [dbo].[ImageTag]
ADD CONSTRAINT [PK_ImageTag]
    PRIMARY KEY CLUSTERED ([Images_imgId], [Tags_tagId] ASC);
GO

-- Creating primary key on [ImagesLiked_imgId], [UsersLikes_userId] in table 'Likes'
ALTER TABLE [dbo].[Likes]
ADD CONSTRAINT [PK_Likes]
    PRIMARY KEY CLUSTERED ([ImagesLiked_imgId], [UsersLikes_userId] ASC);
GO

-- Creating primary key on [UserFollow_userId], [Users_userId] in table 'Follows'
ALTER TABLE [dbo].[Follows]
ADD CONSTRAINT [PK_Follows]
    PRIMARY KEY CLUSTERED ([UserFollow_userId], [Users_userId] ASC);
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
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserImage'
CREATE INDEX [IX_FK_UserImage]
ON [dbo].[Images]
    ([userId]);
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

-- Creating foreign key on [categoryId] in table 'Images'
ALTER TABLE [dbo].[Images]
ADD CONSTRAINT [FK_ImageCategory]
    FOREIGN KEY ([categoryId])
    REFERENCES [dbo].[Categories]
        ([categoryId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ImageCategory'
CREATE INDEX [IX_FK_ImageCategory]
ON [dbo].[Images]
    ([categoryId]);
GO

-- Creating foreign key on [Images_imgId] in table 'ImageTag'
ALTER TABLE [dbo].[ImageTag]
ADD CONSTRAINT [FK_ImageTag_Image]
    FOREIGN KEY ([Images_imgId])
    REFERENCES [dbo].[Images]
        ([imgId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Tags_tagId] in table 'ImageTag'
ALTER TABLE [dbo].[ImageTag]
ADD CONSTRAINT [FK_ImageTag_Tag]
    FOREIGN KEY ([Tags_tagId])
    REFERENCES [dbo].[Tags]
        ([tagId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ImageTag_Tag'
CREATE INDEX [IX_FK_ImageTag_Tag]
ON [dbo].[ImageTag]
    ([Tags_tagId]);
GO

-- Creating foreign key on [ImagesLiked_imgId] in table 'Likes'
ALTER TABLE [dbo].[Likes]
ADD CONSTRAINT [FK_Likes_Image]
    FOREIGN KEY ([ImagesLiked_imgId])
    REFERENCES [dbo].[Images]
        ([imgId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [UsersLikes_userId] in table 'Likes'
ALTER TABLE [dbo].[Likes]
ADD CONSTRAINT [FK_Likes_User]
    FOREIGN KEY ([UsersLikes_userId])
    REFERENCES [dbo].[Users]
        ([userId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Likes_User'
CREATE INDEX [IX_FK_Likes_User]
ON [dbo].[Likes]
    ([UsersLikes_userId]);
GO

-- Creating foreign key on [UserFollow_userId] in table 'Follows'
ALTER TABLE [dbo].[Follows]
ADD CONSTRAINT [FK_Follows_User]
    FOREIGN KEY ([UserFollow_userId])
    REFERENCES [dbo].[Users]
        ([userId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Users_userId] in table 'Follows'
ALTER TABLE [dbo].[Follows]
ADD CONSTRAINT [FK_Follows_User1]
    FOREIGN KEY ([Users_userId])
    REFERENCES [dbo].[Users]
        ([userId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Follows_User1'
CREATE INDEX [IX_FK_Follows_User1]
ON [dbo].[Follows]
    ([Users_userId]);
GO

INSERT INTO [dbo].[Users] VALUES(
	"testUN", "test", "testFN", "testLN1", "testLN2", "test@test.ts", "testLand", "ES");
GO

INSERT INTO [dbo].[Categories] VALUES(
	"test");
GO

INSERT INTO [dbo].[Categories] VALUES(
	"test2");
GO

INSERT INTO [dbo].[Images] VALUES(
	"image-title", "image description ninio",  '2022-01-11 23:12:00.937', NULL, "1.jpeg", 1, 1
);
GO

INSERT INTO [dbo].[Tags] VALUES(
	"test_tag_100", 100
);
GO

INSERT INTO [dbo].[Tags] VALUES(
	"test_tag_80", 80
);
GO

INSERT INTO [dbo].[Tags] VALUES(
	"test_tag_60", 60
);
GO

INSERT INTO [dbo].[Tags] VALUES(
	"test_tag_40", 40
);
GO

INSERT INTO [dbo].[Tags] VALUES(
	"test_tag_20", 20
);
GO
-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------