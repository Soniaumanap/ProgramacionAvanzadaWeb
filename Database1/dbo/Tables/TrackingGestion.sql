CREATE TABLE [dbo].[TrackingGestion] (
    [TrackingGestionId]  INT            IDENTITY (1, 1) NOT NULL,
    [Fecha]              DATETIME2 (7)  NOT NULL,
    [Comentario]         NVARCHAR (MAX) NULL,
    [SolicitudCreditoId] INT            NOT NULL,
    CONSTRAINT [PK_TrackingGestion] PRIMARY KEY CLUSTERED ([TrackingGestionId] ASC),
    CONSTRAINT [FK_TrackingGestion_SolicitudCredito] FOREIGN KEY ([SolicitudCreditoId]) REFERENCES [dbo].[SolicitudCredito] ([SolicitudCreditoId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_TrackingGestion_SolicitudCreditoId]
    ON [dbo].[TrackingGestion]([SolicitudCreditoId] ASC);

