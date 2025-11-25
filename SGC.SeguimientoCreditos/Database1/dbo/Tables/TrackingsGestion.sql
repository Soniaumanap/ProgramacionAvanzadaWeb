CREATE TABLE [dbo].[TrackingsGestion] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [GestionId]     INT           NOT NULL,
    [Accion]        VARCHAR (200) NOT NULL,
    [Comentario]    VARCHAR (MAX) NULL,
    [UsuarioNombre] VARCHAR (100) NOT NULL,
    [Fecha]         DATETIME2 (7) DEFAULT (getdate()) NOT NULL,
    [SolicitudId]   INT           NULL,
    [UsuarioId]     INT           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([GestionId]) REFERENCES [dbo].[SolicitudesCredito] ([Id])
);

