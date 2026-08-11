# Human Resource Management System (SaaS MVP)

A multi-tenant Human Resource Management System (HRMS) SaaS MVP built
with modern .NET technologies and a clean, scalable architecture.

The project is designed to provide organizations with a centralized
platform for managing employees, departments, designations, leave
management, and other core HR operations.

------------------------------------------------------------------------

## 🚀 Project Overview

The HRMS SaaS MVP is being developed as a portfolio-grade enterprise
application with a focus on:

-   Clean Architecture
-   Domain-driven design principles
-   CQRS
-   Separation of concerns
-   Repository and Unit of Work patterns
-   Multi-tenant SaaS architecture
-   Secure authentication and authorization
-   Scalable and maintainable code structure

The system is currently under active development.

------------------------------------------------------------------------

## 🏗️ Architecture

The backend follows a simplified **Clean Architecture** approach:

``` text
                    ┌─────────────────────┐
                    │      Front-End      │
                    │      Angular        │
                    └──────────┬──────────┘
                               │
                               ▼
                    ┌─────────────────────┐
                    │       API Layer     │
                    │   ASP.NET Core Web  │
                    │        API          │
                    └──────────┬──────────┘
                               │
                               ▼
                    ┌─────────────────────┐
                    │ Application Layer   │
                    │ CQRS + MediatR      │
                    │ Commands / Queries  │
                    └──────────┬──────────┘
                               │
                               ▼
                    ┌─────────────────────┐
                    │    Domain Layer     │
                    │ Entities / Enums    │
                    │ Business Rules      │
                    └──────────┬──────────┘
                               │
                               ▼
                    ┌─────────────────────┐
                    │ Persistence Layer   │
                    │ EF Core / Repos     │
                    │ Unit of Work        │
                    └──────────┬──────────┘
                               │
                               ▼
                    ┌─────────────────────┐
                    │     SQL Server      │
                    └─────────────────────┘
```

------------------------------------------------------------------------

## 🛠️ Technology Stack

### Backend

-   ASP.NET Core 8 Web API
-   Entity Framework Core
-   SQL Server
-   MediatR
-   CQRS
-   FluentValidation
-   Generic Repository Pattern
-   Unit of Work Pattern
-   Swagger / OpenAPI

### Frontend

-   Angular
-   Angular Material
-   TypeScript
-   Bootstrap

### Development Tools

-   Visual Studio / Visual Studio Code
-   Git & GitHub
-   SQL Server Management Studio
-   Entity Framework Core Migrations

------------------------------------------------------------------------

## 📦 Project Structure

``` text
Human Resource Management System (SaaS)
│
├── Back-End
│   ├── HRMS_MVP.API
│   ├── HRMS_MVP.Application
│   ├── HRMS_MVP.Domain
│   └── HRMS_MVP.Persistence
│
├── Front-End
│
├── .gitignore
└── README.md
```

### Backend Layers

  -----------------------------------------------------------------------
  Layer                               Responsibility
  ----------------------------------- -----------------------------------
  **API**                             HTTP endpoints, controllers,
                                      middleware and API configuration

  **Application**                     Use cases, CQRS commands/queries,
                                      handlers, DTOs and validation

  **Domain**                          Core entities, enums and business
                                      rules

  **Persistence**                     EF Core, DbContext, repositories,
                                      Unit of Work and database
                                      configuration
  -----------------------------------------------------------------------

------------------------------------------------------------------------

## 📋 Current Modules

### Organization Management

-   Organization registration
-   Multi-tenant organization structure
-   Organization-specific data isolation

### Employee Management

-   Employee creation
-   Employee information management
-   Organization-based employee relationships

### Department Management

-   Create departments
-   Organization-specific department uniqueness
-   Department management

### Designation Management

-   Designation management
-   Employee-designation relationships
-   Organization-specific designations

### Leave Management

🚧 Under development

------------------------------------------------------------------------

## 🔐 Security

Security is an important part of the system architecture.

Planned and implemented security capabilities include:

-   Authentication
-   Authorization
-   Role-based access control
-   JWT authentication
-   Secure API endpoints
-   Organization-level data isolation
-   Input validation
-   Soft deletion

------------------------------------------------------------------------

## 🗄️ Database

The application uses **Microsoft SQL Server** with **Entity Framework
Core**.

Database schema changes are managed using EF Core migrations.

``` bash
dotnet ef migrations add InitialCreate
```

Apply migrations:

``` bash
dotnet ef database update
```

> **Important:** Never commit production credentials, database
> passwords, JWT secrets, or other sensitive configuration values to
> GitHub.

------------------------------------------------------------------------

## ⚙️ Getting Started

### Prerequisites

Make sure the following are installed:

-   .NET 8 SDK
-   Node.js
-   Angular CLI
-   SQL Server
-   SQL Server Management Studio
-   Git

### 1. Clone the repository

``` bash
git clone https://github.com/Mehdi851/Human-Resource-Management-System-SaaS-MVP.git
```

### 2. Navigate to the backend

``` bash
cd "Human-Resource-Management-System-SaaS-MVP/Back-End"
```

### 3. Configure the database

Configure the local SQL Server connection using your local development
configuration.

Do not commit production credentials or passwords to GitHub.

### 4. Apply EF Core migrations

``` bash
dotnet ef database update
```

### 5. Run the API

``` bash
dotnet run
```

Swagger will be available in the development environment at the
configured ASP.NET Core URL.

------------------------------------------------------------------------

## 🧪 Development Status

This project is currently under active development.

### Completed

-   [x] Solution architecture
-   [x] Domain layer
-   [x] Entity Framework Core setup
-   [x] SQL Server integration
-   [x] Generic Repository
-   [x] Unit of Work
-   [x] Dependency Injection
-   [x] Employee Management foundation
-   [x] Department Management foundation
-   [x] Designation Management

### In Progress

-   [ ] Leave Management
-   [ ] Authentication & Authorization
-   [ ] Role management
-   [ ] Dashboard
-   [ ] Angular frontend integration
-   [ ] API hardening
-   [ ] Automated testing

### Planned

-   [ ] Attendance Management
-   [ ] Payroll Management
-   [ ] Notifications
-   [ ] Reporting
-   [ ] Audit logging
-   [ ] Advanced SaaS administration
-   [ ] Docker support
-   [ ] CI/CD pipeline
-   [ ] Cloud deployment

------------------------------------------------------------------------

## 🎯 Project Goals

The primary goals of this project are to:

1.  Build a realistic SaaS-based HR management platform.
2.  Apply Clean Architecture principles in a real-world project.
3.  Demonstrate scalable backend development using ASP.NET Core.
4.  Implement CQRS and maintain clear separation between application
    concerns.
5.  Build a reusable multi-tenant architecture.
6.  Develop a modern Angular frontend.
7.  Apply secure software engineering practices.
8.  Prepare the system for future cloud deployment and scalability.

------------------------------------------------------------------------

## 🗺️ Roadmap

``` text
Architecture
     │
     ▼
Domain Layer
     │
     ▼
Infrastructure / Persistence
     │
     ▼
Application Layer
     │
     ▼
API
     │
     ▼
Authentication & Authorization
     │
     ▼
HR Modules
     │
     ▼
Angular Frontend
     │
     ▼
Testing & Hardening
     │
     ▼
Docker / CI-CD
     │
     ▼
Cloud Deployment
```

------------------------------------------------------------------------

## 📚 Learning & Portfolio Project

This project is also being developed as a practical demonstration of
modern software engineering practices, including:

-   Enterprise application architecture
-   SaaS application design
-   Clean Architecture
-   API development
-   Database design
-   Secure application development
-   Design patterns
-   Software scalability
-   Full-stack development

------------------------------------------------------------------------

## 👨‍💻 Author

**Muhammad Jawad Mehdi**

Full-Stack .NET Developer\
ASP.NET Core \| Angular \| SQL Server \| Clean Architecture \| SaaS

------------------------------------------------------------------------

## 📄 License

This project is currently intended for educational, portfolio, and
demonstration purposes.

License details will be added as the project progresses.
