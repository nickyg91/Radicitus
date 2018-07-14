CREATE TABLE [rad].[PollInputType] (
    [PollInputTypeId] INT          IDENTITY (1, 1) NOT NULL,
    [InputType]       VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_PollInputType] PRIMARY KEY CLUSTERED ([PollInputTypeId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'', @level0type = N'SCHEMA', @level0name = N'rad', @level1type = N'TABLE', @level1name = N'PollInputType';

