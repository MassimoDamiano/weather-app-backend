# Weather App Backend

API REST desarrollada con ASP.NET Core 8 para buscar ciudades y consultar su clima actual y pronóstico. Consume OpenWeather y entrega al frontend un contrato propio, evitando exponer la API key del proveedor.

## Funcionalidades

- búsqueda de ciudades por nombre;
- clima actual por latitud y longitud;
- pronóstico por intervalos y resumen de hasta cinco días;
- validación automática de parámetros;
- respuestas de error uniformes con `ProblemDetails`;
- política CORS configurable;
- endpoint de salud para despliegues;
- pruebas unitarias con xUnit y Moq.

## Arquitectura

```text
Petición HTTP
    ↓
Controllers ── validan parámetros y definen las rutas
    ↓
Services ───── aplican la lógica y construyen los DTO públicos
    ↓
Clients ────── se comunican con OpenWeather mediante HttpClient
    ↓
Models
├── OpenWeather ── representan las respuestas del proveedor
└── Responses ──── definen el contrato entregado al frontend

Infrastructure ── manejo global de excepciones
Configuration ─── opciones tipadas y validadas al iniciar
WeatherApi.Tests ─ pruebas unitarias de controllers y errores
```

## Requisitos

- [.NET SDK 8](https://dotnet.microsoft.com/download/dotnet/8.0)
- una API key de [OpenWeather](https://openweathermap.org/api)

## Configuración local

Desde la carpeta del proyecto:

```powershell
dotnet user-secrets init
dotnet user-secrets set "OpenWeather:ApiKey" "TU_API_KEY"
dotnet restore .\WeatherBackend.sln
dotnet run --launch-profile http
```

La documentación Swagger queda disponible en `http://localhost:5270/swagger` cuando el entorno es `Development`. La API key se guarda fuera del repositorio mediante Secret Manager.

## Endpoints

| Método | Ruta | Descripción |
| --- | --- | --- |
| `GET` | `/api/locations?query=Córdoba&limit=5` | Devuelve coincidencias de ciudades. `query` admite entre 2 y 100 caracteres y `limit` entre 1 y 5. |
| `GET` | `/api/weather?latitude=-31.4167&longitude=-64.1833` | Devuelve clima actual, pronóstico por intervalos y resumen diario. |
| `GET` | `/health` | Indica si la aplicación está funcionando. |

Los errores de validación usan `400 Bad Request`. Los errores al consultar el proveedor se traducen a `502 Bad Gateway` o `504 Gateway Timeout`; los inesperados usan `500 Internal Server Error`.

## Pruebas

```powershell
dotnet test .\WeatherBackend.sln
```

Las pruebas actuales cubren los resultados exitosos de ambos controllers, la normalización del texto de búsqueda y la traducción global de excepciones.

## Variables para producción

En Azure App Service se deben configurar como ajustes de la aplicación:

| Variable | Ejemplo | Uso |
| --- | --- | --- |
| `OpenWeather__ApiKey` | valor secreto | Autenticación con OpenWeather. |
| `Cors__AllowedOrigins__0` | `https://mi-frontend.example` | Primer origen web autorizado. Se pueden agregar más aumentando el índice. |

En desarrollo se permiten todos los orígenes para facilitar las pruebas locales. En producción solamente se aceptan los dominios declarados en `Cors:AllowedOrigins`.
