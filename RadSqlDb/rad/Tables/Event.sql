CREATE TABLE [rad].[Event] (
    [EventId]     INT            IDENTITY (1, 1) NOT NULL,
    [Title]       NVARCHAR (500) NOT NULL,
    [Description] NVARCHAR (250) NULL,
    [EventDate]   DATETIME2 (7)  NOT NULL,
    CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED ([EventId] ASC)
);

