CREATE TABLE [dbo].[SolicitudCredito] (
    [SolicitudCreditoId] INT             IDENTITY (1, 1) NOT NULL,
    [Fecha]              DATETIME2 (7)   NOT NULL,
    [Monto]              DECIMAL (18, 2) NOT NULL,
    [ClienteId]          INT             NOT NULL,
    CONSTRAINT [PK_SolicitudCredito] PRIMARY KEY CLUSTERED ([SolicitudCreditoId] ASC),
    CONSTRAINT [FK_SolicitudCredito_Cliente] FOREIGN KEY ([ClienteId]) REFERENCES [dbo].[Cliente] ([ClienteId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_SolicitudCredito_ClienteId]
    ON [dbo].[SolicitudCredito]([ClienteId] ASC);

