CREATE TABLE [rad].[PollResponse] (
    [PollResponseId] INT           IDENTITY (1, 1) NOT NULL,
    [PollInputId]    INT           NOT NULL,
    [Response]       VARCHAR (500) NULL,
    [Cookie]         VARCHAR (500) NOT NULL,
    CONSTRAINT [PK_PollResponse] PRIMARY KEY CLUSTERED ([PollResponseId] ASC),
    CONSTRAINT [FK_PollResponse_PollInput] FOREIGN KEY ([PollInputId]) REFERENCES [rad].[PollInput] ([PollInputId])
);

