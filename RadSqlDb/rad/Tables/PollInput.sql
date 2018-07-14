CREATE TABLE [rad].[PollInput] (
    [PollInputId]     INT           IDENTITY (1, 1) NOT NULL,
    [PollInputTypeId] INT           NOT NULL,
    [PollId]          INT           NOT NULL,
    [Answer]          VARCHAR (500) NOT NULL,
    CONSTRAINT [PK_PollInput] PRIMARY KEY CLUSTERED ([PollInputId] ASC),
    CONSTRAINT [FK_PollInput_Poll] FOREIGN KEY ([PollId]) REFERENCES [rad].[Poll] ([PollId]),
    CONSTRAINT [FK_PollInput_PollInputType] FOREIGN KEY ([PollInputId]) REFERENCES [rad].[PollInputType] ([PollInputTypeId])
);

