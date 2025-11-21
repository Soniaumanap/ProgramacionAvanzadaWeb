CREATE TABLE [dbo].[Cliente] (
    [ClienteId]      INT            IDENTITY (1, 1) NOT NULL,
    [Identificacion] NVARCHAR (50)  NULL,
    [Nombre]         NVARCHAR (100) NULL,
    [Apellidos]      NVARCHAR (100) NULL,
    [Telefono]       NVARCHAR (20)  NULL,
    CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED ([ClienteId] ASC)
);

