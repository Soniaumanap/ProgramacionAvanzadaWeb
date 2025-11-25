
CREATE DATABASE SGCSeguimientoCreditosDB;

GO

USE SGCSeguimientoCreditosDB;
GO

/* ==========================================================
   TABLA: Clientes
   ========================================================== */
CREATE TABLE Clientes (
    Id              INT IDENTITY(1,1)     NOT NULL PRIMARY KEY,
    Identificacion  VARCHAR(20)           NOT NULL,
    Nombre          VARCHAR(100)          NOT NULL,
    Telefono        VARCHAR(20)           NULL,
    Email           VARCHAR(100)          NULL
);

CREATE UNIQUE INDEX IX_Clientes_Identificacion
    ON Clientes (Identificacion);
GO

/* ==========================================================
   TABLA: Usuarios
   ========================================================== */
CREATE TABLE Usuarios (
    Id              INT IDENTITY(1,1)     NOT NULL PRIMARY KEY,
    Identificacion  VARCHAR(20)           NOT NULL,
    Nombre          VARCHAR(100)          NOT NULL,
    Email           VARCHAR(100)          NOT NULL,
    [Password]      VARCHAR(200)          NOT NULL,
    Rol             VARCHAR(50)           NOT NULL,    
    Activo          BIT                   NOT NULL DEFAULT(1)
);

CREATE UNIQUE INDEX IX_Usuarios_Identificacion
    ON Usuarios (Identificacion);

CREATE UNIQUE INDEX IX_Usuarios_Email
    ON Usuarios (Email);
GO

/* ==========================================================
   TABLA: SolicitudesCredito
   ========================================================== */
CREATE TABLE SolicitudesCredito (
    Id                     INT IDENTITY(11550,1) NOT NULL PRIMARY KEY,
    ClienteId              INT                    NOT NULL,
    IdentificacionCliente  VARCHAR(20)            NOT NULL,
    Monto                  DECIMAL(18,2)          NOT NULL,
    Comentarios            VARCHAR(MAX)           NOT NULL,
    Estado                 VARCHAR(50)            NOT NULL,            
    Fecha                  DATETIME2              NOT NULL,
    Documentos             VARCHAR(MAX)           NOT NULL,

    CONSTRAINT FK_SolicitudesCredito_Cliente
        FOREIGN KEY (ClienteId) REFERENCES Clientes(Id)
);
GO

/* ==========================================================
   TABLA: TrackingsGestion
   ========================================================== */
CREATE TABLE TrackingsGestion (
    Id             INT IDENTITY(1,1)      NOT NULL PRIMARY KEY,
    GestionId      INT                    NOT NULL,           
    Accion         VARCHAR(100)           NOT NULL,           
    Comentario     VARCHAR(MAX)           NOT NULL,
    UsuarioNombre  VARCHAR(100)           NOT NULL,
    Fecha          DATETIME2              NOT NULL,
    SolicitudId    INT                    NOT NULL,           
    UsuarioId      INT                    NOT NULL,           

    CONSTRAINT FK_TrackingsGestion_Solicitud
        FOREIGN KEY (GestionId) REFERENCES SolicitudesCredito(Id)
);
GO

/* ==========================================================
   DATOS DE PRUEBA (SIN VALORES NULL)
   ========================================================== */

--------------------------------------------------------------
-- CLIENTES (3 registros)
--------------------------------------------------------------
INSERT INTO Clientes (Identificacion, Nombre, Telefono, Email)
VALUES
('115500001', 'Juan Pérez',   '8888-0001', 'juan.perez@demo.com'),
('115500002', 'María Gómez',  '8888-0002', 'maria.gomez@demo.com'),
('115500003', 'Carlos López', '8888-0003', 'carlos.lopez@demo.com');
GO

--------------------------------------------------------------
-- USUARIOS (4 registros: Admin, Analista, Gestor, ServicioCliente)
--------------------------------------------------------------
INSERT INTO Usuarios (Identificacion, Nombre, Email, [Password], Rol, Activo)
VALUES
('101010101', 'Admin SGC',      'admin@sgc.com',   'admin123',   'Admin',           1),
('202020202', 'Ana Analista',   'analista@sgc.com','ana123',     'Analista',        1),
('303030303', 'Gustavo Gestor', 'gestor@sgc.com',  'gestor123',  'Gestor',          1),
('404040404', 'Carlos Servicio','servicio@sgc.com','servicio123','ServicioCliente', 1);
GO

--------------------------------------------------------------
-- SOLICITUDES CREDITO (3 registros)
-- Usamos IDENTITY_INSERT para controlar Id = 11550,11551,11552
--------------------------------------------------------------
SET IDENTITY_INSERT SolicitudesCredito ON;
GO

INSERT INTO SolicitudesCredito
    (Id, ClienteId, IdentificacionCliente, Monto, Comentarios, Estado, Fecha, Documentos)
VALUES
(11550, 1, '115500001', 5000000.00,
 'Crédito personal para consolidar deudas',
 'Ingresado',
 DATEADD(DAY, -5, GETDATE()),
 'Documentos iniciales cliente Juan Pérez'),

(11551, 2, '115500002', 8000000.00,
 'Crédito para compra de vehículo',
 'Enviado aprobación',
 DATEADD(DAY, -3, GETDATE()),
 'Documentos completos cliente María Gómez'),

(11552, 3, '115500003', 3000000.00,
 'Crédito para gastos médicos',
 'Aprobado',
 DATEADD(DAY, -1, GETDATE()),
 'Documentos verificados cliente Carlos López');
GO

SET IDENTITY_INSERT SolicitudesCredito OFF;
GO

--------------------------------------------------------------
-- TRACKINGS GESTION (varios movimientos por gestión)
--------------------------------------------------------------
INSERT INTO TrackingsGestion
    (GestionId, Accion, Comentario, UsuarioNombre, Fecha, SolicitudId, UsuarioId)
VALUES
-- Gestión 11550 (Juan Pérez)
(11550, 'Crear',
 'Se crea la gestión para el cliente Juan Pérez.',
 'Carlos Servicio',
 DATEADD(DAY, -5, GETDATE()),
 11550,
 4),

(11550, 'Enviada aprobación',
 'Se revisa la documentación y se envía a aprobación.',
 'Ana Analista',
 DATEADD(DAY, -4, GETDATE()),
 11550,
 2),

(11550, 'Devolución',
 'Falta constancia salarial actualizada.',
 'Gustavo Gestor',
 DATEADD(DAY, -3, GETDATE()),
 11550,
 3),

-- Gestión 11551 (María Gómez)
(11551, 'Crear',
 'Se crea la gestión para el cliente María Gómez.',
 'Carlos Servicio',
 DATEADD(DAY, -3, GETDATE()),
 11551,
 4),

(11551, 'Enviada aprobación',
 'Análisis satisfactorio, se envía a aprobación.',
 'Ana Analista',
 DATEADD(DAY, -2, GETDATE()),
 11551,
 2),

-- Gestión 11552 (Carlos López)
(11552, 'Crear',
 'Se crea la gestión para el cliente Carlos López.',
 'Carlos Servicio',
 DATEADD(DAY, -2, GETDATE()),
 11552,
 4),

(11552, 'Enviada aprobación',
 'Documentación completa, sin observaciones.',
 'Ana Analista',
 DATEADD(DAY, -1, GETDATE()),
 11552,
 2),

(11552, 'Aprobada',
 'Se aprueba la gestión.',
 'Gustavo Gestor',
 GETDATE(),
 11552,
 3);
GO

