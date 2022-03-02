# Base

Initial structure for a Net Core Api with Clean Architecture.

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

# Project Structure 🧱

```
├───Appi
│   ├───Controllers
│   └───Responses
├───Core
│   ├───Constants
│   ├───CustomEntities
│   ├───DTOs
│   ├───Entities
│   ├───Enumerations
│   ├───Exceptions
│   ├───Infrastructure
│   ├───QueryFilters
│   └───Services
│       └───Imp
│       └───Interfaces
└───Infraestructure
    └───Data
      └───Configurations
    └───Filters
    ├───Mappings
    ├───Repositories
    └───Validators

```

# Built with 🛠️

    - Visual Studio Professional
