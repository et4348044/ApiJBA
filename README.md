# ApiJBA

ApiJBA es una API Web robusta y moderna desarrollada en **ASP.NET Core (C#)** diseñada para la gestión integral de datos de personal y el control de accesos al sistema basado en rangos jerárquicos.

## Características Principales

- **Gestión de Personal**: Operaciones CRUD completas para administrar el registro de personal (cédula, nombre, nivel de rango, cargo, estado, número de cuenta y archivo adjunto).
- **Asignación Automática de Cargos**: El sistema asigna automáticamente el cargo según el nivel jerárquico asignado si no se especifica uno personalizado:
  - `10` -> Sistemas
  - `9` -> Director
  - `8` -> Subdirector
  - `7` -> Secretaria
  - `6` -> Vocero
  - `1` a `5` -> Personal General
- **Control de Accesos Jerárquico**:
  - Implementación de un endpoint de inicio de sesión (`api/personal/login`) que valida la cédula (`ci_p`).
  - **Restricción de Acceso**: Solo los usuarios con un nivel de rango **mayor o igual a 7** (Secretaria, Subdirector, Director, Sistemas) tienen permitido el ingreso al sistema. Los niveles inferiores a 7 (como el nivel 1 de Personal General) tienen el acceso denegado.

## Tecnologías Utilizadas

- **.NET Core / C#**
- **Entity Framework Core** (SQL Server)
- **AutoMapper** para la transferencia de datos limpia a través de DTOs.
- **Swagger / OpenAPI** para pruebas y documentación interactiva de endpoints.

## Configuración y Base de Datos

La API utiliza SQL Server. Puedes configurar la cadena de conexión en el archivo `appsettings.Development.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=BD_JBA;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

## Endpoints Disponibles

### Personal

- `GET /api/personal` - Obtener la lista completa de personal.
- `GET /api/personal/ids` - Obtener la lista únicamente con los IDs (cédulas) del personal.
- `GET /api/personal/{ci}` - Obtener el perfil detallado del personal por su cédula (`ci_p`).
- `POST /api/personal` - Registrar un nuevo personal en el sistema (admite niveles del 1 al 10).
- `PUT /api/personal/{ci}` - Actualizar los datos del personal.

### Autenticación y Acceso

- `POST /api/personal/login` - Iniciar sesión enviando la cédula:
  ```json
  {
    "ci_p": "1234567"
  }
  ```
  *Nota: Retorna éxito si el nivel del usuario es mayor o igual a 7.*
