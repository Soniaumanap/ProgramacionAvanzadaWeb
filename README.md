# 🏦 Sistema de Gestión de Créditos (SGC)
**Universidad Fidélitas – Ingeniería en Sistemas de Computación**  
**Curso:** SC-701 Programación Avanzada en Web  
**Profesor:** Arce Vargas Richard  
**Periodo:** I Cuatrimestre, 2025  

---

## 👥 Integrantes del grupo
| Nombre completo | Rol en el equipo |
|------------------|------------------|
| **Ronald Joel Angulo Hernández** | Lógica de negocio y base de datos |
| **Marvin Gustavo Marín Lazo** | Diseño de interfaz y vistas Razor |
| **Sonia Sofía Umaña Piñar** | Controladores, validaciones y testing |
| **Argenis David Cerrato Amador** | Modelado de entidades y reportes |

---

## 🌐 Enlace del repositorio
🔗 [https://github.com/Soniaumanap/ProgramacionAvanzadaWeb](https://github.com/Soniaumanap/ProgramacionAvanzadaWeb)

---

## 🧩 Especificación básica del proyecto

### 📌 Descripción general
El **SGC – Seguimiento de Gestiones de Crédito** es una aplicación web desarrollada en **ASP.NET Core MVC** que permite administrar el ciclo completo de las solicitudes de crédito de clientes, desde su registro hasta la aprobación final.  
El sistema implementa diferentes roles (Administrador, Analista, Gestor y Servicio al Cliente) y cuenta con seguimiento histórico de cada gestión para garantizar la trazabilidad del proceso.

---

## 🏗️ Arquitectura del proyecto
El sistema sigue el **patrón de arquitectura en capas** bajo el modelo **MVC (Model–View–Controller)** y la división propuesta en el curso:

| Capa | Descripción | Tecnologías |
|------|--------------|--------------|
| **Presentación (WebApp)** | Interfaz desarrollada con Razor Pages y vistas parciales. Manejo de modales (SweetAlert2) y validaciones dinámicas. | ASP.NET Core MVC, Razor, HTML5, Bootstrap |
| **Lógica de Negocio (Business Layer)** | Contiene las reglas de negocio, validaciones de estados de crédito y control de roles. | C# con principios SOLID |
| **Acceso a Datos (Data Access Layer)** | Interactúa con la base de datos mediante Entity Framework Core. | EF Core, LINQ, SQL Server |
| **Base de Datos** | Modelo relacional con tablas `Usuarios`, `Clientes`, `Solicitudes`, `HistorialGestiones` y `Roles`. | SQL Server 2022 / Azure SQL |

---

## 📦 Librerías y paquetes NuGet utilizados

| Paquete | Uso principal |
|----------|---------------|
| **Microsoft.EntityFrameworkCore** | ORM para mapeo y acceso a datos |
| **Microsoft.EntityFrameworkCore.SqlServer** | Proveedor de base de datos SQL Server |
| **Microsoft.EntityFrameworkCore.Tools** | Soporte de migraciones y scaffolding |
| **Microsoft.AspNetCore.Identity** | Autenticación y roles de usuario |
| **SweetAlert2** (JS) | Ventanas modales interactivas sin recargar la página |
| **Newtonsoft.Json** | Serialización y envío de datos en formato JSON |
| **Bootstrap 5 / jQuery** | Estilo y control dinámico de componentes front-end |

---

## 💡 Principios de SOLID y patrones de diseño aplicados

| Principio / Patrón | Aplicación práctica |
|--------------------|--------------------|
| **S (Single Responsibility)** | Cada clase cumple una única función (p. ej. `SolicitudService` solo maneja operaciones de solicitudes). |
| **O (Open/Closed)** | Las reglas de negocio pueden extenderse sin modificar las clases existentes mediante interfaces. |
| **L (Liskov Substitution)** | Las clases hijas (`UsuarioAdmin`, `UsuarioAnalista`) pueden sustituir a la clase base `Usuario`. |
| **I (Interface Segregation)** | Separación de interfaces pequeñas (`IClienteRepositorio`, `ISolicitudRepositorio`) para evitar dependencias innecesarias. |
| **D (Dependency Inversion)** | Uso de inyección de dependencias (`AddScoped`, `AddTransient`) para desacoplar las capas. |
| **Patrón MVC** | Separación de responsabilidades entre modelo, vista y controlador. |
| **Patrón Repositorio** | Implementado para abstraer el acceso a datos. |
| **Patrón DTO / ViewModel** | Transferencia de datos entre capas de forma segura y eficiente. |

---

## 📊 Funcionalidades principales

- 🔐 **Inicio de sesión** según rol (Administrador, Analista, Gestor, Servicio al Cliente).  
- 👥 **Administración de clientes y usuarios** (CRUD completo).  
- 📝 **Creación de solicitudes de crédito** con validaciones:
  - No permitir duplicar solicitudes activas.
  - No exceder ₡10,000,000.  
- 🔄 **Flujo de gestión de crédito:** registrado → enviado a aprobación → devolución → aprobado.  
- 📁 **Seguimiento histórico completo** de cada gestión.  
- 📈 **Reporte de movimientos** por número de gestión.  
- 💬 **Interacción sin recargar página** usando JavaScript y modales dinámicos (SweetAlert2).  

---

## 🧠 Metodología y buenas prácticas

- Uso de **nombres significativos**, **indentación uniforme** y **convenciones de estilo C#**.  
- Implementación de **validaciones** para IDs únicos, estados y campos requeridos.  
- Integración con **GitHub** y control de versiones colaborativo (pull/push por cada integrante).  
- Revisión de código mediante **SonarQube** para garantizar seguridad y mantenibilidad.

---

## 📚 Recursos académicos y bibliografía
- Sznajdleder, P. (2017). *Programación orientada a objetos y estructura de datos a fondo*. Alfaomega.  
- Joyanes Aguilar, L. (2020). *Fundamentos de programación: algoritmos, estructuras de datos y objetos*. McGraw-Hill.  
- Martin, J. (2016). *Visual Studio 2015 Cookbook*. Packt Publishing.  
- Documentación oficial de [ASP.NET Core](https://learn.microsoft.com/es-es/aspnet/core/).

---

## 📅 Entregas
- **Avance:** Semana 9  
- **Defensa final:** Semana 14-15  
- **Evaluación:** 50 % de la nota total del curso  

---

## 🪪 Licencia
Este proyecto se publica bajo la licencia **MIT License**.  
Puedes usar, modificar y distribuir el código con fines académicos o educativos, siempre citando a los autores originales.

---

© 2025 Universidad Fidélitas – SC-701 Programación Avanzada en Web
