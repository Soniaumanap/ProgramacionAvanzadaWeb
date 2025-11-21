CREATE TABLE [dbo].[Usuario] (
    [UsuarioId]  INT            IDENTITY (1, 1) NOT NULL,
    [Correo]     NVARCHAR (255) NULL,
    [Contrasena] NVARCHAR (255) NULL,
    [Rol]        NVARCHAR (50)  NULL,
    CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED ([UsuarioId] ASC)
);

