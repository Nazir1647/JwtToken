CREATE TABLE [dbo].[User_Roles] (
    [id]     INT IDENTITY (1, 1) NOT NULL,
    [userId] INT NOT NULL,
    [roleId] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([roleId]) REFERENCES [dbo].[Roles] ([id]),
    FOREIGN KEY ([userId]) REFERENCES [dbo].[Users] ([id])
);

