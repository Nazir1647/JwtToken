CREATE TABLE [dbo].[product] (
    [id]        INT            IDENTITY (1, 1) NOT NULL,
    [name]      NVARCHAR (50)  NOT NULL,
    [category]  INT            NULL,
    [price]     DECIMAL (8, 2) NULL,
    [quantity]  INT            NULL,
    [imageFile] NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

