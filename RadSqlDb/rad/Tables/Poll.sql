CREATE TABLE [rad].[Poll] (
    [PollId]    INT           IDENTITY (1, 1) NOT NULL,
    [Question]  VARCHAR (500) NOT NULL,
    [StartDate] DATE          NOT NULL,
    [EndDate]   DATE          NOT NULL,
    CONSTRAINT [PK_Poll] PRIMARY KEY CLUSTERED ([PollId] ASC)
);

