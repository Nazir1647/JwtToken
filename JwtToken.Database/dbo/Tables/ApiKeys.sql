CREATE TABLE [dbo].[ApiKeys] (
    [Id]      INT            IDENTITY (1, 1) NOT NULL,
    [ApiKey]  NVARCHAR (100) NOT NULL,
    [IsValid] BIT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

