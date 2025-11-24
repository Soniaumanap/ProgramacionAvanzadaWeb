CREATE DATABASE SGCSeguimientoCreditosDB;
GO
USE SGCSeguimientoCreditosDB;
GO

/* ============================
   TABLA: Usuarios
   ============================ */
CREATE TABLE Usuarios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Identificacion VARCHAR(20) NOT NULL UNIQUE,
    Nombre VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE,
    Password VARCHAR(200) NOT NULL,
    Rol VARCHAR(30) NOT NULL,  -- Admin, Analista, Gestor, ServicioCliente
    Activo BIT NOT NULL DEFAULT 1
);

/* ============================
   TABLA: Clientes
   ============================ */
CREATE TABLE Clientes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Identificacion VARCHAR(20) NOT NULL UNIQUE,
    Nombre VARCHAR(100) NOT NULL,
    Telefono VARCHAR(20),
    Email VARCHAR(100)
);

/* ============================
   TABLA: SolicitudesCredito
   ============================ */
CREATE TABLE SolicitudesCredito (
    Id INT IDENTITY(11550,1) PRIMARY KEY,
    ClienteId INT NOT NULL,
    IdentificacionCliente VARCHAR(20) NOT NULL,
    Monto DECIMAL(18,2) NOT NULL,
    Comentarios VARCHAR(MAX),
    Estado VARCHAR(50),
    Documentos VARCHAR(MAX),
    Fecha DATETIME2 NOT NULL DEFAULT GETDATE(),

    FOREIGN KEY (ClienteId) REFERENCES Clientes(Id)
);

/* ============================
   TABLA: TrackingGestion
   ============================ */
CREATE TABLE TrackingGestion (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    GestionId INT NOT NULL,
    Accion VARCHAR(200) NOT NULL,
    Comentario VARCHAR(MAX),
    UsuarioNombre VARCHAR(100) NOT NULL,
    Fecha DATETIME2 NOT NULL DEFAULT GETDATE(),

    FOREIGN KEY (GestionId) REFERENCES SolicitudesCredito(Id)
);

----------------------------------------------------
-- DATOS DE PRUEBA
----------------------------------------------------

/* Usuarios de prueba */
INSERT INTO Usuarios (Identificacion, Nombre, Email, Password, Rol, Activo) VALUES
('10101010', 'Admin General', 'admin@sgc.com', 'Admin123*', 'Admin', 1),
('20202020', 'Ana Analista', 'analista@sgc.com', 'Analista123*', 'Analista', 1),
('30303030', 'Gerardo Gestor', 'gestor@sgc.com', 'Gestor123*', 'Gestor', 1),
('40404040', 'Sofía Servicio', 'servicio@sgc.com', 'Servicio123*', 'ServicioCliente', 1);

/* Clientes de prueba */
INSERT INTO Clientes (Identificacion, Nombre, Telefono, Email) VALUES
('115500000', 'Juan Pérez',  '8888-8888', 'juan.perez@correo.com'),
('115511111', 'María Gómez', '8777-7777', 'maria.gomez@correo.com'),
('115522222', 'Carlos Rodríguez', '8666-6666', 'carlos.rodriguez@correo.com');

/* Solicitudes de crédito de prueba */
INSERT INTO SolicitudesCredito (ClienteId, IdentificacionCliente, Monto, Comentarios, Estado, Documentos)
VALUES
-- Cliente 1: solicitud en estado Ingresado
(1, '115500000', 5000000, 'Crédito personal para consolidar deudas', 'Ingresado', NULL),
-- Cliente 2: solicitud enviada a aprobación
(2, '115511111', 8000000, 'Crédito para compra de vehículo', 'Enviado aprobación', NULL),
-- Cliente 2: solicitud aprobada
(2, '115511111', 3000000, 'Crédito para gastos médicos', 'Aprobado', NULL);

/* Tracking de las solicitudes
   (los Id de gestión dependen de los IDENTITY; se asume que se insertaron
   en el orden anterior: 11550, 11551, 11552, etc.) */

/* Tracking para la primera solicitud (Id = 11550) */
INSERT INTO TrackingGestion (GestionId, Accion, Comentario, UsuarioNombre)
VALUES
(11550, 'Crear', 'Se crea la gestión para el cliente Juan Pérez', 'Sofía Servicio'),
(11550, 'Enviada aprobación', 'Se analiza y se envía a aprobación', 'Ana Analista');

/* Tracking para la segunda solicitud (Id = 11551) */
INSERT INTO TrackingGestion (GestionId, Accion, Comentario, UsuarioNombre)
VALUES
(11551, 'Crear', 'Gestión para compra de vehículo', 'Sofía Servicio'),
(11551, 'Enviada aprobación', 'Análisis completado', 'Ana Analista'),
(11551, 'Devolución', 'Falta adjuntar VB de negocio', 'Gerardo Gestor'),
(11551, 'Enviada aprobación', 'Se corrige VB de negocio', 'Ana Analista');

/* Tracking para la tercera solicitud (Id = 11552) */
INSERT INTO TrackingGestion (GestionId, Accion, Comentario, UsuarioNombre)
VALUES
(11552, 'Crear', 'Crédito para gastos médicos', 'Sofía Servicio'),
(11552, 'Enviada aprobación', 'Todo correcto, se envía a aprobación', 'Ana Analista'),
(11552, 'Aprobada', 'Se acepta la gestión', 'Gerardo Gestor');
