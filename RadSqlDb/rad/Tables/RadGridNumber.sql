CREATE TABLE [rad].[RadGridNumber] (
    [RadNumberId]   INT            IDENTITY (1, 1) NOT NULL,
    [GridId]        INT            NOT NULL,
    [GridNumber]    INT            NOT NULL,
    [RadMemberName] NVARCHAR (250) NOT NULL,
    [HasWon]        BIT            CONSTRAINT [DF_RadGridNumber_HasPaid] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_RadGridNumber] PRIMARY KEY CLUSTERED ([RadNumberId] ASC)
);

