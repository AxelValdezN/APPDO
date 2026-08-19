APPDO - prototipo para organización de pagos

Solución experimental compuesta por una API en ASP.NET Core y una aplicación .NET MAUI. El repositorio implementa la base técnica para registro e inicio de sesión de usuarios y prepara la evolución hacia una aplicación de recordatorios de pagos y deudas.

English summary: Early-stage payment reminder application with an ASP.NET Core Web API, PostgreSQL persistence and a .NET MAUI client. The current implementation covers user registration, login, API communication and the initial database model.

Objetivo

Ayudar a organizar pagos de servicios, tarjetas, préstamos, renta, suscripciones y otras obligaciones mediante recordatorios, categorías, notificaciones e historial.

Estado actual

La versión disponible es un prototipo técnico en desarrollo. Actualmente incluye:

API REST desarrollada con ASP.NET Core.

Registro de usuarios.

Inicio de sesión.

Persistencia con Entity Framework Core y PostgreSQL.

Validación de correos duplicados.

Respuestas HTTP diferenciadas para éxito, conflicto y autenticación fallida.

Documentación y pruebas manuales mediante Swagger y archivos .http.

Cliente .NET MAUI con pantallas de registro e inicio de sesión.

Consumo de la API mediante HttpClient.

Manejo básico de errores de conexión y tiempo de espera.

Las funciones de pagos, recordatorios, categorías, notificaciones e historial forman parte del alcance planeado y todavía no están implementadas en este repositorio.

Arquitectura

flowchart TD
    M[Aplicación .NET MAUI] -->|HTTP/JSON| C[AuthController]
    C --> S[AuthService]
    S --> EF[Entity Framework Core]
    EF --> DB[(PostgreSQL)]
    SW[Swagger / archivos HTTP] --> C

Tecnologías

Backend

C#

.NET 8

ASP.NET Core Web API

Entity Framework Core

PostgreSQL con Npgsql

Swagger / OpenAPI

Aplicación cliente

.NET MAUI

XAML

HttpClient

Android como plataforma principal

Endpoints disponibles

Método

Ruta

Descripción

POST

/api/auth/register

Registra un usuario y valida correos duplicados.

POST

/api/auth/login

Valida las credenciales y devuelve la información básica del usuario.

Ejecución del backend

Requisitos

.NET 8 SDK

PostgreSQL

Herramientas de Entity Framework Core

Clona el repositorio y restaura las dependencias:

git clone https://github.com/AxelValdezN/APPDO.git
cd APPDO
dotnet restore AppDoAPII/AppDoAPII.csproj

Configura una cadena de conexión local mediante secretos de usuario o variables de entorno. Ejemplo de estructura:

{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=appdo;Username=postgres;Password=tu_contraseña"
  }
}

Aplica las migraciones y ejecuta la API:

dotnet ef database update --project AppDoAPII
dotnet run --project AppDoAPII

En desarrollo, Swagger permite consultar y probar los endpoints disponibles.

Ejecución del cliente MAUI

El proyecto AppDoMAUI utiliza .NET MAUI y requiere las cargas de trabajo correspondientes:

dotnet workload install maui
dotnet restore AppDoMAUI/AppDoMAUI.csproj
dotnet build AppDoMAUI/AppDoMAUI.csproj -f net10.0-android

La URL base de la API debe configurarse de acuerdo con el emulador o dispositivo utilizado.

Estructura del repositorio

APPDO/
├── AppDoAPII/              # API principal, autenticación y persistencia
│   ├── Controllers/
│   ├── Data/
│   ├── Migrations/
│   ├── Models/
│   └── Services/
├── AppDoMAUI/              # Cliente multiplataforma
│   ├── Models/
│   ├── Services/
│   └── Views/
└── AppDoAPI/               # Proyecto base conservado como referencia inicial

Limitaciones conocidas

La autenticación utiliza un token temporal y aún no implementa JWT.

El prototipo actual no cifra las contraseñas; antes de producción debe incorporarse un algoritmo seguro de hashing.

El dominio de pagos y recordatorios todavía no está implementado.

No hay pruebas automatizadas.

La configuración local debe moverse completamente a secretos o variables de entorno.

Próximas mejoras

Implementar hashing de contraseñas y autenticación JWT.

Crear entidades y operaciones CRUD para pagos, categorías y recordatorios.

Añadir notificaciones locales en Android.

Incorporar historial y estado de pagos.

Agregar validaciones, pruebas unitarias y pruebas de integración.

Configurar integración continua y despliegue de la API.

Autor

Proyecto académico desarrollado en equipo. Participación de Axel Valdez en la conexión con PostgreSQL, modelos, persistencia con Entity Framework Core, registro, autenticación y pruebas de API.

Axel Nathel Valdez Noriega

GitHub · LinkedIn
