CREATE TABLE [dbo].[SolicitudesCredito] (
    [Id]                    INT             IDENTITY (11550, 1) NOT NULL,
    [ClienteId]             INT             NOT NULL,
    [IdentificacionCliente] VARCHAR (20)    NOT NULL,
    [Monto]                 DECIMAL (18, 2) NOT NULL,
    [Comentarios]           VARCHAR (MAX)   NULL,
    [Estado]                VARCHAR (50)    NULL,
    [Documentos]            VARCHAR (MAX)   NULL,
    [Fecha]                 DATETIME2 (7)   DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([ClienteId]) REFERENCES [dbo].[Clientes] ([Id])
);

