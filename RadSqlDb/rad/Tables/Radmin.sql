CREATE TABLE [rad].[Radmin] (
    [Username] NVARCHAR (50)  NOT NULL,
    [Password] VARBINARY (64) NOT NULL,
    [RadminId] INT            IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_Radmin] PRIMARY KEY CLUSTERED ([RadminId] ASC)
);

