CREATE TABLE [rad].[Grid] (
    [GridId]        INT           IDENTITY (1, 1) NOT NULL,
    [DateCreated]   DATETIME2 (7) NOT NULL,
    [GridName]      VARCHAR (50)  NULL,
    [CostPerSquare] INT           NOT NULL,
    CONSTRAINT [PK_Grid] PRIMARY KEY CLUSTERED ([GridId] ASC)
);

