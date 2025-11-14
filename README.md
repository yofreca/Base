# Base

API RESTful desarrollada con .NET Core 3.1 siguiendo los principios de **Clean Architecture** con clara separación de responsabilidades en tres capas principales: API (Presentación), Core (Lógica de Negocio) e Infrastructure (Acceso a Datos).

# Start 🚀

    1. Clone this project -> https://github.com/yofreca/Base.git

# Pre-requirements 📋

It is necessary to install -> https://dotnet.microsoft.com/en-us/download/dotnet/3.1

# Dependencies 🤝

The following NuGet packages must be registered

- **[https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer/]** Microsoft SQL Server database provider for Entity Framework Core.
- **[https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Tools/]** Entity Framework Core Tools for the NuGet Package Manager Console in Visual Studio.
- **[https://www.nuget.org/packages/Microsoft.AspNetCore.Mvc.NewtonsoftJson]** ASP.NET Core MVC features that use Newtonsoft.Json. Includes input and output formatters for JSON and JSON PATCH
- **[https://www.nuget.org/packages/AutoMapper.Extensions.Microsoft.DependencyInjection/]** AutoMapper extensions for ASP.NET Core
- **[https://www.nuget.org/packages/FluentValidation.AspNetCore/]** FluentValidation is validation library for .NET that uses a fluent interface and lambda expressions for building strongly-typed validation rules.
- **[https://www.nuget.org/packages/Microsoft.AspNetCore.Authentication.JwtBearer]** ASP.NET Core middleware that enables an application to receive an OpenID Connect bearer token.

# Arquitectura del Proyecto 🏗️

## Patrón Arquitectónico

Este proyecto implementa **Clean Architecture** (Arquitectura Limpia) con separación en tres capas bien definidas, siguiendo el **principio de dependencia inversa** donde las dependencias fluyen hacia el interior (API → Core ← Infrastructure).

### Características principales:
- ✅ **Diseño centrado en el dominio**
- ✅ **Programación basada en interfaces**
- ✅ **Principio de Inversión de Dependencias (DIP)**
- ✅ **Separación de responsabilidades**
- ✅ **Alta testabilidad y mantenibilidad**

## Estructura del Proyecto 🧱

```
Base/
├── Api/                          # 🎯 CAPA DE PRESENTACIÓN
│   ├── Controllers/              # Controladores de la API
│   │   ├── UserController.cs    # CRUD de usuarios
│   │   ├── TokenController.cs   # Autenticación JWT
│   │   └── DefaultController.cs # Endpoints por defecto
│   ├── Responses/                # Wrappers de respuesta
│   │   └── ApiResponse.cs       # Respuesta genérica con metadata
│   ├── Program.cs                # Punto de entrada de la aplicación
│   ├── Startup.cs                # Configuración de servicios y middleware
│   └── appsettings.json          # Configuración de la aplicación
│
├── Core/                         # 💼 CAPA DE LÓGICA DE NEGOCIO
│   ├── CustomEntities/           # Objetos de dominio personalizados
│   │   ├── PagedList.cs          # Lista paginada genérica
│   │   └── PaginationOptions.cs # Opciones de paginación
│   ├── DTOs/                     # Data Transfer Objects
│   │   ├── UserDto.cs            # DTO de usuario
│   │   └── UserLoginDto.cs      # DTO de login
│   ├── Entities/                 # Entidades del dominio
│   │   ├── User.cs               # Usuario
│   │   ├── Login.cs              # Credenciales de acceso
│   │   ├── Rol.cs                # Roles del sistema
│   │   ├── UserRol.cs            # Relación Usuario-Rol
│   │   ├── Module.cs             # Módulos de la aplicación
│   │   ├── Menu.cs               # Menús de navegación
│   │   └── RolPermits.cs         # Permisos por rol
│   ├── Exceptions/               # Excepciones personalizadas
│   │   └── BusinessException.cs # Excepciones de negocio
│   ├── Interfaces/               # Contratos de servicios y repositorios
│   │   ├── IUserService.cs       # Contrato de servicio de usuarios
│   │   ├── ILoginService.cs      # Contrato de servicio de login
│   │   ├── IUserRepository.cs    # Contrato de repositorio de usuarios
│   │   └── ILoginRepository.cs   # Contrato de repositorio de login
│   ├── QueryFilters/             # Filtros de consulta
│   │   └── UserQueryFilter.cs   # Filtros para consultas de usuarios
│   └── Services/                 # Implementación de lógica de negocio
│       ├── UserService.cs        # Lógica de negocio de usuarios
│       └── LoginService.cs       # Lógica de autenticación
│
└── Infrastructure/               # 🔧 CAPA DE INFRAESTRUCTURA
    ├── Data/                     # Contexto de base de datos
    │   ├── Configurations/       # Configuraciones Fluent API
    │   │   ├── UserConfiguration.cs
    │   │   ├── LoginConfiguration.cs
    │   │   ├── RolConfiguration.cs
    │   │   └── ... (otras configuraciones)
    │   └── BaseContext.cs        # DbContext de EF Core
    ├── Filters/                  # Filtros globales
    │   └── GlobalExceptionFilter.cs # Manejo global de excepciones
    ├── Mappings/                 # Perfiles de AutoMapper
    │   └── AutomapperProfile.cs # Mapeos Entity ↔ DTO
    ├── Repositories/             # Implementación de repositorios
    │   ├── UserRepository.cs     # Acceso a datos de usuarios
    │   └── LoginRepository.cs    # Acceso a datos de login
    └── Validators/               # Validadores FluentValidation
        └── UserValidator.cs      # Reglas de validación de usuarios
```

## Responsabilidades por Capa

### 🎯 API Layer (Capa de Presentación)
**Responsabilidades:**
- Manejo de peticiones y respuestas HTTP
- Validación de entrada (FluentValidation)
- Mapeo de DTOs (AutoMapper)
- Autenticación y autorización JWT
- Documentación con Swagger
- Manejo de códigos de estado HTTP

**Tecnologías:**
- ASP.NET Core Web API 3.1
- Swagger/Swashbuckle 5.4.1
- Newtonsoft.Json

### 💼 Core Layer (Capa de Lógica de Negocio)
**Responsabilidades:**
- Reglas de negocio y validaciones
- Modelos de dominio y entidades
- Interfaces de servicios y contratos
- Excepciones de negocio
- Objetos de transferencia de datos (DTOs)
- **Independiente de frameworks externos**

**Principios:**
- Sin dependencias de infraestructura
- Define interfaces que Infrastructure implementa
- Contiene la lógica核心del sistema

### 🔧 Infrastructure Layer (Capa de Infraestructura)
**Responsabilidades:**
- Implementación de acceso a datos
- Configuración de Entity Framework Core
- Validación de datos (FluentValidation)
- Mapeo objeto-objeto (AutoMapper)
- Manejo global de excepciones
- Configuración de base de datos

**Tecnologías:**
- Entity Framework Core 3.1.14
- SQL Server
- AutoMapper 7.0.0
- FluentValidation 8.6.2

## Flujo de Datos 🔄

```
┌─────────────────────────────────────────────────────────────┐
│                       Cliente/Usuario                        │
└──────────────────────────┬──────────────────────────────────┘
                           │ HTTP Request (JSON)
                           ▼
┌─────────────────────────────────────────────────────────────┐
│                  API Layer (Controllers)                     │
│  1. Recibe petición HTTP                                     │
│  2. FluentValidation valida DTOs automáticamente             │
│  3. Mapea DTOs a Entities (AutoMapper)                       │
└──────────────────────────┬──────────────────────────────────┘
                           │ Llama métodos de servicio
                           ▼
┌─────────────────────────────────────────────────────────────┐
│                 Core Layer (Services)                        │
│  4. Ejecuta lógica de negocio                                │
│  5. Aplica reglas de negocio                                 │
│  6. Maneja lógica de paginación                              │
│  7. Lanza BusinessException si hay errores                   │
└──────────────────────────┬──────────────────────────────────┘
                           │ Llama métodos de repositorio
                           ▼
┌─────────────────────────────────────────────────────────────┐
│           Infrastructure (Repositories)                      │
│  8. Accede al DbContext                                      │
│  9. Ejecuta operaciones CRUD                                 │
│  10. Retorna Entities                                        │
└──────────────────────────┬──────────────────────────────────┘
                           │ Consultas EF Core
                           ▼
┌─────────────────────────────────────────────────────────────┐
│                   Base de Datos SQL Server                   │
│  Schema: Authorization                                       │
│  Tablas: User, Login, Rol, UserRol, Module, Menu, etc.      │
└─────────────────────────────────────────────────────────────┘
```

**Ejemplo de flujo de petición:**

1. **Request:** Cliente hace GET `/api/users?pageNumber=1&pageSize=10`
2. **Controller:** `UserController.GetUsers()` recibe el request
3. **Validation:** FluentValidation valida los parámetros automáticamente
4. **Service:** Llama a `IUserService.GetUsers(filter)`
5. **Business Logic:** Servicio aplica lógica de paginación y reglas
6. **Repository:** Llama a `IUserRepository.GetUsers()`
7. **Database:** EF Core ejecuta query contra SQL Server
8. **Mapping:** Entities se mapean a DTOs con AutoMapper
9. **Response:** Retorna `ApiResponse<PagedList<UserDto>>` con metadata
10. **JSON:** Cliente recibe respuesta JSON estructurada

## Patrones de Diseño Implementados 🎨

### 1. **Repository Pattern** (Patrón Repositorio)
Abstrae la lógica de acceso a datos y proporciona una interfaz para operaciones CRUD.
```csharp
// Interfaz en Core
public interface IUserRepository
{
    Task<IEnumerable<User>> GetUsers();
    Task<User> GetUser(int id);
    Task InsertUser(User user);
    Task UpdateUser(User user);
    Task DeleteUser(int id);
}

// Implementación en Infrastructure
public class UserRepository : IUserRepository { ... }
```

### 2. **Service Layer Pattern** (Capa de Servicios)
Encapsula la lógica de negocio y coordina operaciones entre controladores y repositorios.
```csharp
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    // Aplica reglas de negocio, validaciones, etc.
}
```

### 3. **Dependency Injection (DI)** (Inyección de Dependencias)
Configuración en `Startup.cs`:
```csharp
services.AddTransient<IUserService, UserService>();
services.AddTransient<IUserRepository, UserRepository>();
services.AddDbContext<BaseContext>(options =>
    options.UseSqlServer(connectionString));
```

### 4. **DTO Pattern** (Data Transfer Objects)
Objetos separados para transferencia de datos, evitando exponer entidades de dominio directamente.
```csharp
public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    // Solo propiedades necesarias para el cliente
}
```

### 5. **Generic Wrapper Pattern** (Respuesta Genérica)
```csharp
public class ApiResponse<T>
{
    public T Data { get; set; }
    public PagedData Meta { get; set; }
}

public class PagedList<T>
{
    public List<T> Items { get; set; }
    public int TotalCount { get; set; }
    public int PageSize { get; set; }
    // Metadata de paginación
}
```

### 6. **Filter Pattern** (Filtros Globales)
```csharp
// Manejo centralizado de excepciones
public class GlobalExceptionFilter : IExceptionFilter
{
    // Intercepta todas las excepciones y retorna respuestas consistentes
}
```

## Principios SOLID Aplicados ✅

| Principio | Implementación |
|-----------|----------------|
| **S**ingle Responsibility | Cada clase tiene una única responsabilidad: Controladores manejan HTTP, Servicios contienen lógica de negocio, Repositorios acceden a datos |
| **O**pen/Closed | Extensible a través de interfaces y tipos genéricos sin modificar código existente |
| **L**iskov Substitution | Las implementaciones pueden reemplazar sus interfaces sin romper funcionalidad |
| **I**nterface Segregation | Interfaces específicas y focalizadas (`IUserService`, `IUserRepository`) |
| **D**ependency Inversion | Los módulos de alto nivel dependen de abstracciones (interfaces), no de implementaciones concretas |

## Stack Tecnológico 🛠️

### Framework y Runtime
- **.NET Core 3.1**
- **C#** (netstandard2.0 para bibliotecas de clase)

### API y Web
- **ASP.NET Core Web API 3.1**
- **Newtonsoft.Json** - Serialización JSON
- **Swagger/Swashbuckle 5.4.1** - Documentación de API

### Seguridad
- **JWT Bearer Authentication** - Autenticación con tokens JWT
- **Microsoft.AspNetCore.Authentication.JwtBearer 3.1.14**

### Acceso a Datos
- **Entity Framework Core 3.1.14** - ORM
- **SQL Server** - Base de datos relacional
- **Fluent API** - Configuración de entidades

### Herramientas Transversales
- **AutoMapper 7.0.0** - Mapeo objeto a objeto
- **FluentValidation 8.6.2** - Validación de entrada
- **Microsoft.Extensions.Options** - Configuración fuertemente tipada

### Testing
- **NUnit 3.12.0** - Framework de pruebas unitarias

## Configuración y Dependencias 📦

### Configuración de Servicios (Startup.cs)

```csharp
public void ConfigureServices(IServiceCollection services)
{
    // 1. AutoMapper - Mapeo automático
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    // 2. MVC con filtros globales
    services.AddControllers(options =>
    {
        options.Filters.Add<GlobalExceptionFilter>();
    }).AddNewtonsoftJson();

    // 3. Configuración fuertemente tipada
    services.Configure<PaginationOptions>(
        Configuration.GetSection("Pagination"));

    // 4. Entity Framework Core + SQL Server
    services.AddDbContext<BaseContext>(options =>
        options.UseSqlServer(connectionString));

    // 5. Registro de servicios (Transient lifetime)
    services.AddTransient<IUserService, UserService>();
    services.AddTransient<IUserRepository, UserRepository>();
    services.AddTransient<ILoginService, LoginService>();
    services.AddTransient<ILoginRepository, LoginRepository>();

    // 6. Swagger para documentación
    services.AddSwaggerGen();

    // 7. Autenticación JWT
    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options => { /* config */ });

    // 8. FluentValidation
    services.AddMvc().AddFluentValidation(options =>
    {
        options.RegisterValidatorsFromAssemblies(
            AppDomain.CurrentDomain.GetAssemblies());
    });
}
```

### Archivo de Configuración (appsettings.json)

```json
{
  "ConnectionStrings": {
    "DataBaseConnection": "Server=...;Database=...;User Id=...;Password=..."
  },
  "Pagination": {
    "DefaultPageSize": 10,
    "DefaultPageNumber": 1
  },
  "Authentication": {
    "SecretKey": "S3cr3tK3y2022**"
  }
}
```

## Acceso a Datos con Entity Framework Core 💾

### Patrones Implementados

#### 1. **Code-First Approach**
- Entidades definidas en la capa Core
- Migraciones generan el esquema de base de datos

#### 2. **Fluent API Configuration**
Las configuraciones de entidades están en clases separadas que implementan `IEntityTypeConfiguration<T>`:

```csharp
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User", "Authorization");
        builder.HasKey(e => e.UserId);
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        // ... más configuraciones
    }
}
```

#### 3. **DbContext**
```csharp
public class BaseContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Login> Logins { get; set; }
    public DbSet<Rol> Roles { get; set; }
    // ... otros DbSets

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Aplica todas las configuraciones del ensamblado
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
```

#### 4. **Patrón Repository**
Métodos típicos de repositorio:
- `GetAll()` / `GetUsers()` - Obtener colecciones
- `GetById(id)` / `GetUser(id)` - Obtener entidad individual
- `Insert()` / `InsertUser()` - Crear nueva entidad
- `Update()` / `UpdateUser()` - Modificar entidad existente
- `Delete(id)` / `DeleteUser()` - Eliminar entidad

#### 5. **Async/Await Pattern**
Todas las operaciones de datos son asíncronas:
```csharp
public async Task<User> GetUser(int id)
{
    return await _context.Users
        .FirstOrDefaultAsync(u => u.UserId == id);
}
```

### Esquema de Base de Datos

**Schema:** `Authorization`

**Tablas principales:**
- **User** - Información de usuarios
- **Login** - Credenciales de autenticación
- **Rol** - Roles del sistema
- **UserRol** - Asignación de roles a usuarios (relación muchos a muchos)
- **Module** - Módulos de la aplicación
- **Menu** - Estructura de navegación
- **RolPermits** - Permisos basados en roles

**Relaciones:**
- One-to-Many: User → Login, User → UserRol
- Many-to-Many: User ↔ Rol (a través de UserRol)
- One-to-Many: Module → Menu, Rol → RolPermits

## Características Destacadas ⭐

### 1. **Sistema de Paginación**
Implementado en la capa de servicios usando `PagedList<T>`:
```csharp
var pagedUsers = await _userService.GetUsers(filter);
// Retorna: Items, TotalCount, PageSize, PageNumber, TotalPages
```

### 2. **Validación de Entrada Automática**
FluentValidation intercepta requests automáticamente:
```csharp
public class UserValidator : AbstractValidator<UserDto>
{
    public UserValidator()
    {
        RuleFor(user => user.Name)
            .NotEmpty()
            .MaximumLength(100);
    }
}
```

### 3. **Manejo Global de Excepciones**
`GlobalExceptionFilter` captura todas las excepciones y retorna respuestas consistentes.

### 4. **Autenticación JWT**
Sistema completo de autenticación con tokens JWT:
- Login con credenciales
- Generación de token JWT
- Validación de token en cada request
- Autorización basada en roles

### 5. **Documentación Swagger**
API completamente documentada y explorable en `/swagger`.

### 6. **Respuestas Consistentes**
Todas las respuestas API siguen el mismo formato con `ApiResponse<T>`.

## Mejores Prácticas Implementadas 👍

✅ **Separation of Concerns** - Clara separación de responsabilidades
✅ **DRY** (Don't Repeat Yourself) - Código reutilizable y genérico
✅ **SOLID Principles** - Todos los principios SOLID aplicados
✅ **Async/Await** - Operaciones asíncronas para mejor performance
✅ **Strongly Typed Configuration** - Configuración fuertemente tipada
✅ **Interface-based Programming** - Programación basada en contratos
✅ **Global Exception Handling** - Manejo centralizado de errores
✅ **Input Validation** - Validación robusta de entrada
✅ **API Documentation** - Documentación automática con Swagger
✅ **Repository Pattern** - Abstracción de acceso a datos
✅ **Service Layer** - Lógica de negocio encapsulada

---

## Resumen Ejecutivo 📊

Este proyecto es una **API RESTful profesional** construida con **.NET Core 3.1** que implementa **Clean Architecture** con:

- ✅ Separación clara entre presentación, lógica de negocio y acceso a datos
- ✅ Programación basada en interfaces para testabilidad y mantenibilidad
- ✅ Patrones Repository y Service para código organizado
- ✅ Autenticación completa con JWT
- ✅ Validación de entrada con FluentValidation
- ✅ Mapeo de objetos con AutoMapper
- ✅ Manejo global de excepciones
- ✅ Soporte de paginación
- ✅ Sistema de autorización basado en roles
- ✅ Entity Framework Core con configuración Fluent API
- ✅ Documentación Swagger para exploración de API

**Arquitectura escalable, mantenible y siguiendo las mejores prácticas de la industria para aplicaciones empresariales.**

# Built with 🛠️

    - Visual Studio Professional
