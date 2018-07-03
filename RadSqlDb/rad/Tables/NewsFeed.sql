CREATE TABLE [rad].[NewsFeed] (
    [NewsFeedId]  INT             IDENTITY (1, 1) NOT NULL,
    [DateCreated] DATETIME2 (7)   NOT NULL,
    [Content]     VARCHAR (8000)  NOT NULL,
    [Image]       VARBINARY (MAX) NULL,
    CONSTRAINT [PK_NewsFeed] PRIMARY KEY CLUSTERED ([NewsFeedId] ASC)
);

