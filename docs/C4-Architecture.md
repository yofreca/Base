# Arquitectura C4 - Base API

## Índice
1. [Introducción a C4](#introducción-a-c4)
2. [Nivel 1: Diagrama de Contexto](#nivel-1-diagrama-de-contexto)
3. [Nivel 2: Diagrama de Contenedores](#nivel-2-diagrama-de-contenedores)
4. [Nivel 3: Diagrama de Componentes](#nivel-3-diagrama-de-componentes)
5. [Flujo de Datos](#flujo-de-datos)
6. [Patrones Arquitectónicos](#patrones-arquitectónicos)
7. [Tecnologías Utilizadas](#tecnologías-utilizadas)

---

## Introducción a C4

El modelo C4 (Context, Containers, Components, Code) es un enfoque para visualizar la arquitectura de software mediante diagramas en cuatro niveles de abstracción:

- **Nivel 1 - Contexto**: Vista general del sistema y sus relaciones con usuarios y sistemas externos
- **Nivel 2 - Contenedores**: Aplicaciones y almacenes de datos que componen el sistema
- **Nivel 3 - Componentes**: Componentes principales dentro de cada contenedor
- **Nivel 4 - Código**: Clases e interfaces (opcional, para detalles específicos)

---

## Nivel 1: Diagrama de Contexto

El diagrama de contexto muestra cómo el sistema **Base API** se relaciona con sus usuarios y sistemas externos.

```mermaid
C4Context
    title Diagrama de Contexto - Base API

    Person(user, "Usuario/Cliente", "Aplicación frontend o cliente HTTP que consume la API")

    System(baseApi, "Base API", "API RESTful que gestiona usuarios, autenticación y autorización basada en roles")

    System_Ext(database, "SQL Server", "Base de datos relacional que almacena usuarios, roles, permisos y menús")

    Rel(user, baseApi, "Realiza peticiones HTTP/HTTPS", "JSON/REST")
    Rel(baseApi, database, "Lee y escribe datos", "SQL/TCP")

    UpdateLayoutConfig($c4ShapeInRow="3", $c4BoundaryInRow="1")
```

### Descripción

**Actores:**
- **Usuario/Cliente**: Aplicación frontend (SPA, móvil) o cualquier cliente HTTP que consume los endpoints de la API

**Sistema Principal:**
- **Base API**: Sistema central que expone endpoints RESTful para:
  - Gestión de usuarios (CRUD)
  - Autenticación mediante JWT
  - Autorización basada en roles
  - Gestión de permisos y menús

**Sistemas Externos:**
- **SQL Server**: Base de datos relacional que persiste toda la información del sistema

---

## Nivel 2: Diagrama de Contenedores

El diagrama de contenedores descompone **Base API** en sus contenedores principales.

```mermaid
C4Container
    title Diagrama de Contenedores - Base API

    Person(user, "Usuario/Cliente", "Aplicación frontend o cliente HTTP")

    Container_Boundary(baseApiSystem, "Base API System") {
        Container(apiLayer, "API Layer", ".NET Core 3.1, ASP.NET Core", "Expone endpoints REST, maneja autenticación JWT y validación de entrada")
        Container(coreLayer, "Core Layer", ".NET Standard 2.0", "Contiene lógica de negocio, entidades de dominio, servicios e interfaces")
        Container(infraLayer, "Infrastructure Layer", ".NET Standard 2.0, Entity Framework Core", "Implementa repositorios, acceso a datos y configuraciones de EF Core")
    }

    ContainerDb(database, "SQL Server Database", "SQL Server Express", "Almacena usuarios, credenciales, roles, permisos, módulos y menús (Schema: Authorization)")

    Rel(user, apiLayer, "Realiza peticiones HTTPS", "JSON/REST, JWT Bearer Token")
    Rel(apiLayer, coreLayer, "Usa servicios", "Interfaces (IUserService, ILoginService)")
    Rel(coreLayer, infraLayer, "Usa repositorios", "Interfaces (IUserRepository, ILoginRepository)")
    Rel(infraLayer, database, "Lee/Escribe", "Entity Framework Core, LINQ")

    UpdateLayoutConfig($c4ShapeInRow="2", $c4BoundaryInRow="1")
```

### Descripción de Contenedores

#### 1. API Layer (Capa de Presentación)
- **Tecnología**: ASP.NET Core 3.1 Web API
- **Ubicación**: `/Api/`
- **Responsabilidades**:
  - Exponer endpoints HTTP REST
  - Autenticación JWT Bearer
  - Autorización basada en roles
  - Validación de entrada (FluentValidation)
  - Serialización/deserialización JSON
  - Documentación Swagger/OpenAPI
  - Manejo global de excepciones

**Componentes principales:**
- Controllers (UserController, TokenController)
- Middleware pipeline
- Global exception filter
- API response wrapper
- Configuración de servicios (Startup.cs)

---

#### 2. Core Layer (Capa de Dominio)
- **Tecnología**: .NET Standard 2.0
- **Ubicación**: `/Core/`
- **Responsabilidades**:
  - Definir entidades de dominio
  - Implementar lógica de negocio
  - Definir contratos (interfaces)
  - Gestionar DTOs
  - Paginación de resultados

**Componentes principales:**
- Entidades: User, Login, Rol, UserRol, Module, Menu, RolPermits
- Servicios: UserService, LoginService
- Interfaces: IUserService, ILoginService, IUserRepository, ILoginRepository
- DTOs: UserDto, UserLoginDto
- Custom Entities: PagedList, PaginationOptions

**Principio de Clean Architecture:**
- Esta capa NO depende de frameworks externos
- Es el núcleo del sistema
- Otras capas dependen de esta

---

#### 3. Infrastructure Layer (Capa de Infraestructura)
- **Tecnología**: .NET Standard 2.0, Entity Framework Core 3.1
- **Ubicación**: `/Infrastructure/`
- **Responsabilidades**:
  - Implementar acceso a datos
  - Configurar Entity Framework Core
  - Mapear entidades a tablas (Fluent API)
  - Implementar repositorios
  - Validación con FluentValidation
  - Mapeo con AutoMapper

**Componentes principales:**
- Repositorios: UserRepository, LoginRepository
- DbContext: BaseContext
- Configuraciones: UserConfiguration, LoginConfiguration, etc.
- Mappings: AutomapperProfile
- Validators: UserValidator

---

#### 4. SQL Server Database
- **Servidor**: SQL Server Express (DESKTOP-9ME0K1V\SQLEXPRESS)
- **Base de datos**: Base
- **Schema**: Authorization
- **Tablas**:
  - Authorization.User
  - Authorization.Login
  - Authorization.Rol
  - Authorization.UserRol (Many-to-Many)
  - Authorization.Module
  - Authorization.Menu
  - Authorization.RolPermits

---

## Nivel 3: Diagrama de Componentes

### Componentes de API Layer

```mermaid
C4Component
    title Diagrama de Componentes - API Layer

    Container_Boundary(apiLayer, "API Layer") {
        Component(userController, "UserController", "ASP.NET Core Controller", "Expone endpoints CRUD para usuarios")
        Component(tokenController, "TokenController", "ASP.NET Core Controller", "Genera tokens JWT para autenticación")
        Component(defaultController, "DefaultController", "ASP.NET Core Controller", "Endpoints por defecto")

        Component(middleware, "Middleware Pipeline", "ASP.NET Core", "Procesa requests: HTTPS redirect, routing, authentication, authorization")

        Component(exceptionFilter, "GlobalExceptionFilter", "IExceptionFilter", "Manejo centralizado de excepciones de negocio")

        Component(apiResponse, "ApiResponse<T>", "Generic Wrapper", "Envuelve respuestas con metadata de paginación")

        Component(startup, "Startup", "Configuration", "Configura DI container y middleware pipeline")
    }

    Container(coreLayer, "Core Layer", ".NET Standard 2.0", "Servicios e interfaces de negocio")

    Rel(middleware, userController, "Enruta requests")
    Rel(middleware, tokenController, "Enruta requests")
    Rel(userController, exceptionFilter, "Captura excepciones")
    Rel(userController, apiResponse, "Crea respuestas")
    Rel(userController, coreLayer, "Llama a IUserService")
    Rel(tokenController, coreLayer, "Llama a ILoginService")
    Rel(startup, middleware, "Configura pipeline")

    UpdateLayoutConfig($c4ShapeInRow="3", $c4BoundaryInRow="1")
```

### Componentes de Core Layer

```mermaid
C4Component
    title Diagrama de Componentes - Core Layer

    Container_Boundary(coreLayer, "Core Layer") {
        Component(userService, "UserService", "Service", "Lógica de negocio de usuarios, paginación y filtros")
        Component(loginService, "LoginService", "Service", "Lógica de autenticación y validación de credenciales")

        Component(iUserService, "IUserService", "Interface", "Contrato de servicio de usuarios")
        Component(iLoginService, "ILoginService", "Interface", "Contrato de servicio de login")
        Component(iUserRepo, "IUserRepository", "Interface", "Contrato de repositorio de usuarios")
        Component(iLoginRepo, "ILoginRepository", "Interface", "Contrato de repositorio de login")

        Component(entities, "Entities", "Domain Models", "User, Login, Rol, UserRol, Module, Menu, RolPermits")
        Component(dtos, "DTOs", "Data Transfer Objects", "UserDto, UserLoginDto")
        Component(pagedList, "PagedList<T>", "Custom Entity", "Lista paginada genérica con metadata")

        Component(businessEx, "BusinessException", "Exception", "Excepciones de lógica de negocio")
    }

    Container(infraLayer, "Infrastructure Layer", ".NET Standard 2.0", "Implementación de repositorios")

    Rel(userService, iUserService, "Implementa")
    Rel(loginService, iLoginService, "Implementa")
    Rel(userService, iUserRepo, "Usa")
    Rel(loginService, iLoginRepo, "Usa")
    Rel(userService, entities, "Trabaja con")
    Rel(userService, pagedList, "Retorna")
    Rel(userService, businessEx, "Lanza si error")
    Rel(iUserRepo, infraLayer, "Implementado en")
    Rel(iLoginRepo, infraLayer, "Implementado en")

    UpdateLayoutConfig($c4ShapeInRow="3", $c4BoundaryInRow="1")
```

### Componentes de Infrastructure Layer

```mermaid
C4Component
    title Diagrama de Componentes - Infrastructure Layer

    Container_Boundary(infraLayer, "Infrastructure Layer") {
        Component(userRepo, "UserRepository", "Repository", "Implementa acceso a datos de usuarios con EF Core")
        Component(loginRepo, "LoginRepository", "Repository", "Implementa acceso a datos de credenciales")

        Component(baseContext, "BaseContext", "DbContext", "Contexto de Entity Framework Core, configura DbSets y relaciones")

        Component(configs, "Entity Configurations", "IEntityTypeConfiguration", "UserConfiguration, LoginConfiguration, RolConfiguration, etc.")

        Component(autoMapper, "AutomapperProfile", "Profile", "Mapeo Entity ↔ DTO (User ↔ UserDto)")

        Component(validators, "UserValidator", "AbstractValidator", "Validación de UserDto con FluentValidation")

        Component(globalFilter, "GlobalExceptionFilter", "IExceptionFilter", "Intercepta BusinessException y retorna BadRequest")
    }

    ContainerDb(database, "SQL Server Database", "SQL Server", "Base de datos Authorization")

    Container(coreLayer, "Core Layer", ".NET Standard 2.0", "Interfaces de repositorio")

    Rel(userRepo, baseContext, "Usa")
    Rel(loginRepo, baseContext, "Usa")
    Rel(baseContext, configs, "Aplica configuraciones")
    Rel(baseContext, database, "Ejecuta queries SQL", "EF Core LINQ")
    Rel(userRepo, coreLayer, "Implementa IUserRepository")
    Rel(loginRepo, coreLayer, "Implementa ILoginRepository")

    UpdateLayoutConfig($c4ShapeInRow="2", $c4BoundaryInRow="1")
```

---

## Flujo de Datos

### Flujo Completo de una Petición GET /api/user

```mermaid
sequenceDiagram
    participant Cliente
    participant Middleware
    participant UserController
    participant UserService
    participant UserRepository
    participant BaseContext
    participant Database

    Cliente->>Middleware: GET /api/user?pageNumber=1&pageSize=10<br/>[Authorization: Bearer JWT]

    Middleware->>Middleware: 1. Validar JWT token
    Middleware->>Middleware: 2. Verificar autorización
    Middleware->>UserController: Enrutar request

    UserController->>UserController: FluentValidation valida parámetros
    UserController->>UserService: GetUsers(UserQueryFilter)

    UserService->>UserService: Aplicar valores default de paginación
    UserService->>UserRepository: GetUsers()

    UserRepository->>BaseContext: _context.User.ToListAsync()
    BaseContext->>Database: SELECT * FROM [Authorization].[User]

    Database-->>BaseContext: Result Set (filas)
    BaseContext-->>UserRepository: List<User> (entidades)
    UserRepository-->>UserService: List<User>

    UserService->>UserService: Crear PagedList<User> con metadata
    UserService-->>UserController: PagedList<User>

    UserController->>UserController: AutoMapper: Map<UserDto>(users)
    UserController->>UserController: Crear ApiResponse<IEnumerable<UserDto>>
    UserController-->>Middleware: IActionResult (200 OK)

    Middleware->>Middleware: Serializar a JSON
    Middleware-->>Cliente: HTTP 200 OK + JSON response
```

### Flujo de Autenticación POST /api/token

```mermaid
sequenceDiagram
    participant Cliente
    participant TokenController
    participant LoginService
    participant LoginRepository
    participant Database

    Cliente->>TokenController: POST /api/token<br/>{username, password}

    TokenController->>TokenController: FluentValidation valida UserLoginDto
    TokenController->>LoginService: Authenticate(userLogin)

    LoginService->>LoginRepository: GetLoginByCredentials(userName, password)
    LoginRepository->>Database: SELECT FROM [Authorization].[Login]<br/>WHERE UserName=@userName AND Password=@password

    Database-->>LoginRepository: Login entity (si existe)
    LoginRepository-->>LoginService: Login entity

    alt Credenciales válidas
        LoginService->>LoginService: Validar IsApproved = true
        LoginService->>LoginService: Validar IsLockedOut = false
        LoginService-->>TokenController: Login entity

        TokenController->>TokenController: Generar JWT con claims (Name, UserId)
        TokenController->>TokenController: Firmar con SecretKey (HMAC-SHA256)
        TokenController->>TokenController: Establecer expiración (10 min)
        TokenController-->>Cliente: HTTP 200 OK + {token: "eyJhbGc..."}
    else Credenciales inválidas
        LoginService-->>TokenController: null
        TokenController-->>Cliente: HTTP 401 Unauthorized
    end
```

### Flujo de Manejo de Excepciones

```mermaid
sequenceDiagram
    participant Cliente
    participant Controller
    participant Service
    participant GlobalExceptionFilter

    Cliente->>Controller: Request
    Controller->>Service: Llamada a método de negocio

    Service->>Service: Validación de reglas de negocio

    alt Error de negocio
        Service-->>Service: throw new BusinessException("Error message")
        Service-->>Controller: BusinessException
        Controller-->>GlobalExceptionFilter: BusinessException

        GlobalExceptionFilter->>GlobalExceptionFilter: OnException(context)
        GlobalExceptionFilter->>GlobalExceptionFilter: Crear JSON error response
        GlobalExceptionFilter-->>Cliente: HTTP 400 Bad Request<br/>{message: "Error message"}
    else Éxito
        Service-->>Controller: Resultado
        Controller-->>Cliente: HTTP 200 OK + Data
    end
```

---

## Patrones Arquitectónicos

### 1. Clean Architecture (Arquitectura Limpia)

```
┌─────────────────────────────────────────┐
│         API (Presentación)              │
│     Depende de: Core + Infrastructure   │
└─────────────────┬───────────────────────┘
                  │
        ┌─────────┴─────────┐
        ▼                   ▼
┌───────────────┐   ┌───────────────────┐
│     Core      │◄──┤ Infrastructure    │
│  (Dominio)    │   │  (Datos/Impl)     │
│ No depende de │   │  Depende de: Core │
│     nadie     │   │                   │
└───────────────┘   └───────────────────┘
```

**Beneficios:**
- Independencia de frameworks
- Testabilidad
- Bajo acoplamiento
- Alta cohesión
- Facilita mantenimiento

---

### 2. Repository Pattern

```mermaid
graph LR
    A[Controller] -->|Usa| B[Service]
    B -->|Usa| C[IRepository Interface]
    C -->|Implementado por| D[Repository]
    D -->|Usa| E[DbContext]
    E -->|Accede| F[(Database)]

    style C fill:#f9f,stroke:#333,stroke-width:2px
    style D fill:#bbf,stroke:#333,stroke-width:2px
```

**Características:**
- Abstracción del acceso a datos
- Facilita testing con mocks
- Centraliza queries
- Permite cambiar ORM sin afectar negocio

---

### 3. Dependency Injection

```mermaid
graph TD
    A[Startup.ConfigureServices] -->|Registra| B[IUserService → UserService]
    A -->|Registra| C[IUserRepository → UserRepository]
    A -->|Registra| D[BaseContext - Scoped]
    A -->|Registra| E[AutoMapper]

    F[UserController] -.->|Inyecta| B
    F -.->|Inyecta| E

    G[UserService] -.->|Inyecta| C

    H[UserRepository] -.->|Inyecta| D

    style A fill:#f96,stroke:#333,stroke-width:3px
```

**Lifetimes:**
- **Transient**: Servicios, Repositorios (nueva instancia por inyección)
- **Scoped**: DbContext (una instancia por request HTTP)
- **Singleton**: Configuraciones (una instancia para toda la app)

---

### 4. DTO Pattern (Data Transfer Objects)

```mermaid
graph LR
    A[Cliente] -->|POST /api/user| B[UserController]
    B -->|UserDto| C[AutoMapper]
    C -->|User Entity| D[UserService]
    D -->|User Entity| E[UserRepository]
    E -->|Persist| F[(Database)]

    F -->|User Entity| E
    E -->|User Entity| D
    D -->|User Entity| C
    C -->|UserDto| B
    B -->|JSON| A

    style C fill:#ff9,stroke:#333,stroke-width:2px
```

**Beneficios:**
- No expone estructura interna
- Control sobre serialización
- Previene over-posting
- Optimiza transferencia

---

### 5. Generic Wrapper Pattern

**Ejemplo de respuesta:**
```json
{
  "data": [
    {
      "userId": 1,
      "firstName": "John",
      "lastName": "Doe",
      "email": "john.doe@example.com"
    }
  ],
  "pagedData": {
    "currentPage": 1,
    "totalPages": 5,
    "pageSize": 10,
    "totalCount": 50,
    "hasNextPage": true,
    "hasPreviousPage": false
  }
}
```

---

### 6. Principios SOLID

| Principio | Implementación |
|-----------|----------------|
| **S**ingle Responsibility | Cada clase tiene una única responsabilidad: Controllers (HTTP), Services (negocio), Repositories (datos) |
| **O**pen/Closed | Extensible a través de interfaces sin modificar código existente |
| **L**iskov Substitution | Implementaciones reemplazan interfaces sin romper funcionalidad |
| **I**nterface Segregation | Interfaces específicas y focalizadas (IUserService, IUserRepository) |
| **D**ependency Inversion | Módulos de alto nivel dependen de abstracciones, no de implementaciones |

---

## Tecnologías Utilizadas

### Backend Framework
- **.NET Core 3.1** (netcoreapp3.1)
- **ASP.NET Core Web API 3.1**

### ORM y Base de Datos
- **Entity Framework Core 3.1.14**
- **SQL Server Express** (DESKTOP-9ME0K1V\SQLEXPRESS)
- **Microsoft.EntityFrameworkCore.SqlServer 3.1.14**

### Seguridad
- **Microsoft.AspNetCore.Authentication.JwtBearer 3.1.14**
- **System.IdentityModel.Tokens.Jwt**
- **HMAC-SHA256** para firma de tokens

### Herramientas
- **AutoMapper 7.0.0** - Mapeo Entity ↔ DTO
- **FluentValidation.AspNetCore 8.6.2** - Validación de entrada
- **Swashbuckle.AspNetCore 5.4.1** - Documentación OpenAPI/Swagger
- **Newtonsoft.Json** - Serialización JSON

### Testing
- **NUnit 3.12.0** - Framework de pruebas unitarias

---

## Modelo de Datos

### Diagrama Entidad-Relación (Simplificado)

```mermaid
erDiagram
    USER ||--o{ LOGIN : "tiene credenciales"
    USER ||--o{ USER_ROL : "tiene roles"
    ROL ||--o{ USER_ROL : "asignado a usuarios"
    ROL ||--o{ ROL_PERMITS : "tiene permisos"
    MODULE ||--o{ ROL_PERMITS : "define permisos"
    MODULE ||--o{ MENU : "contiene menús"
    MENU ||--o{ MENU : "submenús (jerárquico)"

    USER {
        bigint UserId PK
        nvarchar FirstName
        nvarchar LastName
        nvarchar Email
        nvarchar Phone
        nvarchar Charge
    }

    LOGIN {
        bigint LoginId PK
        bigint UserId FK
        nvarchar UserName
        nvarchar Password
        bit IsApproved
        bit IsLockedOut
    }

    ROL {
        bigint RolId PK
        nvarchar Name
        bit IsActive
    }

    USER_ROL {
        bigint UserId FK
        bigint RolId FK
    }

    MODULE {
        int ModuleId PK
        nvarchar Name
        nvarchar Description
        nvarchar Route
    }

    MENU {
        int MenuId PK
        int IdParentMenu FK
        smallint LevelMenu
        int ModuleId FK
        smallint Order
        nvarchar MenuName
        nvarchar Icon
    }

    ROL_PERMITS {
        int ModuleId FK
        bigint RolId FK
        int PermitId
    }
```

**Schema:** `Authorization`

**Relaciones clave:**
- User → Login (1:N)
- User ↔ Rol (Many-to-Many mediante UserRol)
- Rol → RolPermits → Module
- Module → Menu (1:N)
- Menu → Menu (jerárquico, self-referencing)

---

## Endpoints de la API

### Autenticación

| Método | Endpoint | Descripción | Auth |
|--------|----------|-------------|------|
| POST | `/api/token` | Autenticar y obtener JWT | No |

**Request Body:**
```json
{
  "userName": "admin",
  "password": "password123"
}
```

**Response:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

---

### Gestión de Usuarios

| Método | Endpoint | Descripción | Auth |
|--------|----------|-------------|------|
| GET | `/api/user` | Listar usuarios (paginado) | Sí (JWT) |
| GET | `/api/user/{id}` | Obtener usuario por ID | Sí (JWT) |
| POST | `/api/user` | Crear nuevo usuario | Sí (JWT) |
| PUT | `/api/user` | Actualizar usuario | Sí (JWT) |
| DELETE | `/api/user/{id}` | Eliminar usuario | Sí (JWT) |

**Ejemplo GET /api/user:**
```
GET /api/user?pageNumber=1&pageSize=10
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

**Response:**
```json
{
  "data": [
    {
      "userId": 1,
      "firstName": "John",
      "lastName": "Doe",
      "email": "john.doe@example.com",
      "phone": "1234567890",
      "charge": "Developer"
    }
  ],
  "pagedData": {
    "currentPage": 1,
    "totalPages": 5,
    "pageSize": 10,
    "totalCount": 50,
    "hasNextPage": true,
    "hasPreviousPage": false
  }
}
```

---

## Configuración

### appsettings.json

```json
{
  "ConnectionStrings": {
    "DataBaseConnection": "Server=DESKTOP-9ME0K1V\\SQLEXPRESS;initial catalog=Base;integrated security=True;"
  },
  "Pagination": {
    "DefaultPageSize": 10,
    "DefaultPageNumber": 1
  },
  "Authentication": {
    "SecretKey": "S3cr3tK3y2022**"
  },
  "AllowedHosts": "*"
}
```

### Características de Seguridad

1. **Autenticación JWT**:
   - Tokens firmados con HMAC-SHA256
   - Expiración: 10 minutos
   - Claims: Name, UserId

2. **Autorización**:
   - Basada en roles (RBAC)
   - Middleware de autorización de ASP.NET Core

3. **Validación**:
   - FluentValidation para DTOs
   - Reglas de negocio en Services
   - GlobalExceptionFilter para errores

4. **HTTPS**:
   - Redirección automática a HTTPS
   - Comunicación cifrada

---

## Swagger/OpenAPI

La API incluye documentación interactiva mediante Swagger UI:

**URL:** `https://localhost:5001/swagger`

Permite:
- Explorar todos los endpoints
- Ver esquemas de request/response
- Probar endpoints directamente desde el navegador
- Descargar especificación OpenAPI

---

## Conclusión

Esta arquitectura implementa **Clean Architecture** con separación clara de responsabilidades en tres capas:

1. **API Layer**: Presentación y endpoints HTTP
2. **Core Layer**: Lógica de negocio independiente
3. **Infrastructure Layer**: Implementación de acceso a datos

**Ventajas:**
- ✅ Alta testabilidad (cada capa puede probarse independientemente)
- ✅ Bajo acoplamiento (cambios en infraestructura no afectan negocio)
- ✅ Mantenibilidad (código organizado y predecible)
- ✅ Escalabilidad (fácil agregar nuevas features)
- ✅ Principios SOLID aplicados consistentemente
- ✅ Documentación automática con Swagger

**Patrones utilizados:**
- Repository Pattern
- Service Layer Pattern
- Dependency Injection
- DTO Pattern
- Generic Wrapper Pattern
- Options Pattern
- Async/Await Pattern

---

**Fecha de generación:** 2025-11-26
**Versión de la aplicación:** .NET Core 3.1
**Autor:** Base API Team
