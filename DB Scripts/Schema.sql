

GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [Username]  NVARCHAR (50) NOT NULL,
    [FirstName] NVARCHAR (50) NOT NULL,
    [LastName]  NVARCHAR (50) NOT NULL,
    [Password]  NVARCHAR (50) NOT NULL,
    [Active]    BIT           DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

INSERT INTO Users([Username],[FirstName],[LastName],[Password],[Active]) VALUES ('realm','Jack','Marrone','y1JhwpwTjgt+Qoepb+KPXg==',1)

GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Contacts] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [UserId]    INT           NOT NULL,
    [FirstName] NVARCHAR (50) NOT NULL,
    [LastName]  NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Contacts_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
);


GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ContactMobiles] (
    [Id]        INT          IDENTITY (1, 1) NOT NULL,
    [ContactId] INT          NOT NULL,
    [MobileNo]  VARCHAR (15) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ContactMobiles_Contacts] FOREIGN KEY ([ContactId]) REFERENCES [dbo].[Contacts] ([Id])
);
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE TABLE [dbo].[ContactEmails] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [ContactId]    INT           NOT NULL,
    [EmailAddress] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ContactEmails_Contacts] FOREIGN KEY ([ContactId]) REFERENCES [dbo].[Contacts] ([Id])
);

