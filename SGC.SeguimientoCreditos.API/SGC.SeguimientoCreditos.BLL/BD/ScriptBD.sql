USE SGCSeguimientoCreditosDB;
GO

CREATE TABLE Cliente (
    ClienteId INT NOT NULL IDENTITY(1,1),
    Identificacion NVARCHAR(50) NULL,
    Nombre NVARCHAR(100) NULL,
    Apellidos NVARCHAR(100) NULL,
    Telefono NVARCHAR(20) NULL,
    CONSTRAINT PK_Cliente PRIMARY KEY (ClienteId)
);

CREATE TABLE Usuario (
    UsuarioId INT NOT NULL IDENTITY(1,1),
    Correo NVARCHAR(255) NULL,
    Contrasena NVARCHAR(255) NULL,
    Rol NVARCHAR(50) NULL,
    CONSTRAINT PK_Usuario PRIMARY KEY (UsuarioId)
);

CREATE TABLE SolicitudCredito (
    SolicitudCreditoId INT NOT NULL IDENTITY(1,1),
    Fecha DATETIME2 NOT NULL,
    Monto DECIMAL(18,2) NOT NULL,
    ClienteId INT NOT NULL,
    CONSTRAINT PK_SolicitudCredito PRIMARY KEY (SolicitudCreditoId),
    CONSTRAINT FK_SolicitudCredito_Cliente
        FOREIGN KEY (ClienteId) REFERENCES Cliente(ClienteId)
        ON DELETE CASCADE
);

CREATE TABLE TrackingGestion (
    TrackingGestionId INT NOT NULL IDENTITY(1,1),
    Fecha DATETIME2 NOT NULL,
    Comentario NVARCHAR(MAX) NULL,
    SolicitudCreditoId INT NOT NULL,
    CONSTRAINT PK_TrackingGestion PRIMARY KEY (TrackingGestionId),
    CONSTRAINT FK_TrackingGestion_SolicitudCredito
        FOREIGN KEY (SolicitudCreditoId) REFERENCES SolicitudCredito(SolicitudCreditoId)
        ON DELETE CASCADE
);

CREATE INDEX IX_SolicitudCredito_ClienteId
    ON SolicitudCredito (ClienteId);

CREATE INDEX IX_TrackingGestion_SolicitudCreditoId
    ON TrackingGestion (SolicitudCreditoId);