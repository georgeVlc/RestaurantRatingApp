CREATE TABLE [dbo].[Users] (
    [userName] VARCHAR (50)  NOT NULL,
    [userType] VARCHAR (20)  NOT NULL,
    [userPwd]  VARCHAR (50)  NOT NULL,
    [resName]  VARCHAR (100) NULL,
    PRIMARY KEY CLUSTERED ([userName] ASC),
    CHECK ([userType]='ADMIN' OR [userType]='APPLICANT' OR [userType]='SIGNED')
);

CREATE TABLE [dbo].[Restaurant] (
    [resName]        VARCHAR (100)  NOT NULL,
    [resRating]      DECIMAL (3, 1) NULL,
    [resImgName]     VARCHAR (100)  NULL,
    [resType]        VARCHAR (20)   NOT NULL,
    [resDescription] VARCHAR (255)  NULL,
    [resOwner]       VARCHAR (50)   NULL,
    PRIMARY KEY CLUSTERED ([resName] ASC),
    FOREIGN KEY ([resOwner]) REFERENCES [dbo].[Users] ([userName]),
    CHECK ([resType]='MEXICAN' OR [resType]='ITALIAN' OR [resType]='ASIAN' OR [resType]='CONTEMPORARY' OR [resType]='GREEK')
);

CREATE TABLE [dbo].[Reviews] (
    [userName]  VARCHAR (50)   NOT NULL,
    [resName]   VARCHAR (100)  NOT NULL,
    [revRating] DECIMAL (3, 1) NULL,
    PRIMARY KEY CLUSTERED ([userName] ASC, [resName] ASC),
    FOREIGN KEY ([userName]) REFERENCES [dbo].[Users] ([userName]),
    FOREIGN KEY ([resName]) REFERENCES [dbo].[Restaurant] ([resName])
);