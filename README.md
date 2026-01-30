# Tamplate

## What This Template Supports

This .NET project template provides a complete, production-ready web API with the following features:

### 🏗️ Architecture & Design Patterns
- **Clean Architecture** with separation of concerns (Domain, Application, Infrastructure, API layers)
- **Generic Repository Pattern** with base repository implementation
- **Generic Service Pattern** with base service implementation and lifecycle hooks
- **Generic Base Controller** for automatic CRUD operations
- **CQRS-ready** structure with DTOs and query filters

### ⚙️ Core Features
- 🔐 **Authentication & Authorization** - JWT-based authentication with role-based access control
- 💾 **Database Management** - Entity Framework Core with SQL Server support
- 🔄 **AutoMapper Integration** - Automatic object-to-object mapping
- 📁 **File Management** - File upload/update/delete with configurable storage and validation
- 💳 **Payment Integration** - Stripe payment processing support
- 📧 **Email Service** - RabbitMQ-based email messaging system
- 📚 **Swagger/OpenAPI** - Automatic API documentation
- ⚠️ **Exception Handling** - Global exception handling middleware

### 🛠️ Development Tools
- 🐳 **Docker Support** - Docker Compose configuration for containerization
- 🗄️ **Database Migrations** - Automatic EF Core migrations
- 🌱 **Seeding** - Predefined roles and users for testing
- 🚀 **Setup Scripts** - Automated setup for Windows and Linux/macOS
- 🧹 **Cleanup Scripts** - Easy environment reset

### 📝 Example Implementation
The template includes a complete **Example** entity demonstrating:
- Domain model with properties
- DTOs (ExampleDto, ExampleCreateDto, ExampleUpdateDto)
- Query filter with custom filtering logic
- Repository pattern implementation
- Service with custom filter logic and lifecycle hooks
- Controller inheriting from BaseController with automatic CRUD endpoints

### 🔌 Extensibility
- **Hook Methods** in base service (BeforeCreate, AfterCreate, BeforeUpdate, AfterUpdate, etc.)
- **Custom Filtering** - Override ApplyFilter method for query customization
- **Pagination Support** - Built-in pagination with PagedResult
- **Virtual Methods** - All base methods are virtual and can be overridden

## Testing credentials

### Administrator
- **Username:** desktop
- **Password:** Test1234!

### User
- **Username:** mobile
- **Password:** Test1234!

> **Note:** There are multiple predefined mobile users in the database (mobile1-mobile9). To fully test the functionality of the tamplate application, it is recommended to create a new user account with an email address to which you have access, so that you can receive email notifications sent by the application.

## Starting the application

### Automatsko pokretanje (preporučeno)

**Windows (Command Prompt):**
```cmd
cd Tamplate
setup.cmd
```

**Linux/macOS:**
```bash
cd Tamplate
chmod +x setup.sh
./setup.sh
```

### Manual start

#### 1. Preparing the environment
First remove `-example` from `.env-example` and insert correct credentials.

#### 2. Creating a database
Navigate to `Tamplate.Domain` and add migration with next command:
```bash
cd Tamplate/Tamplate.Domain
dotnet ef migrations add InitialMigration 
```

#### 3. Running docker container
Return to main `Tamplate/` directory and run next commands:
```bash
cd ..
docker-compose up --build -d
```

> **Note:** Instead of running manually, you can use the automatic scripts `setup.ps1` (Windows) or `setup.sh` (Linux/macOS) which automatically perform all the above steps.

## Stopping and cleaning

To stop a Docker container and delete migrations:

**Windows (Command Prompt):**
```cmd
cd Tamplate
cleanup.cmd
```

**Linux/macOS:**
```bash
cd Tamplate
chmod +x cleanup.sh
./cleanup.sh
```

These scripts will:
- Stop and remove Docker containers (`docker-compose down`)
- Delete folder with migrations (`Tamplate.Domain/Migrations`)

After clean-up run `setup.cmd` / `setup.sh` for fresh install.

# Technology used


![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![Docker](https://img.shields.io/badge/Docker-2496ED?style=for-the-badge&logo=docker&logoColor=white)
![RabbitMQ](https://img.shields.io/badge/RabbitMQ-FF6600?style=for-the-badge&logo=rabbitmq&logoColor=white)
![Stripe](https://img.shields.io/badge/Stripe-008CDD?style=for-the-badge&logo=stripe&logoColor=white)
![Swagger](https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=black)
![JWT](https://img.shields.io/badge/JWT-000000?style=for-the-badge&logo=json-web-tokens&logoColor=white)