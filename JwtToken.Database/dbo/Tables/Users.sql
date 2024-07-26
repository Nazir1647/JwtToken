CREATE TABLE [dbo].[Users] (
    [id]         INT           IDENTITY (1, 1) NOT NULL,
    [name]       NVARCHAR (50) NOT NULL,
    [username]   NVARCHAR (50) NOT NULL,
    [password]   NVARCHAR (50) NOT NULL,
    [isvalid]    BIT           DEFAULT ((0)) NULL,
    [CreateDate] DATETIME      NULL,
    [CreateUser] NVARCHAR (30) NULL,
    [ModifyDate] DATETIME      NULL,
    [ModifyUser] NVARCHAR (30) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

