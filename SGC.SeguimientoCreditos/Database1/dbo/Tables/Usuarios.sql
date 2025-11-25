CREATE TABLE [dbo].[Usuarios] (
    [Id]             INT           IDENTITY (1, 1) NOT NULL,
    [Identificacion] VARCHAR (20)  NOT NULL,
    [Nombre]         VARCHAR (100) NOT NULL,
    [Email]          VARCHAR (100) NOT NULL,
    [Password]       VARCHAR (200) NOT NULL,
    [Rol]            VARCHAR (30)  NOT NULL,
    [Activo]         BIT           DEFAULT ((1)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Email] ASC),
    UNIQUE NONCLUSTERED ([Identificacion] ASC)
);

