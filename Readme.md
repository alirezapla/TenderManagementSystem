## Overview

### The Tender Management System API is a .NET Core application designed to manage tenders, vendors, bids, and related entities. It follows Clean Architecture principles and implements a CQRS pattern with separate read (Dapper) and write (EF Core) operations.
#### Key Features

    Authentication & Authorization: JWT-based authentication with role-based access control

    Tender Management: Full CRUD operations for tenders with proper status tracking

    Bid Management: Vendors can submit bids which administrators can review and update

    Reporting: Comprehensive views of tenders with related bids and vendor information

## Technology Stack

    Backend: .NET 9

    Database: SQL Server

    ORM:

        EF Core for write operations

        Dapper for read operations

    Authentication: JWT Bearer Tokens

    API Documentation: Swagger

## Entity Model

The system manages the following core entities:
User

    Handles authentication and authorization

    Includes role assignment (Admin, Vendor, etc.)

    Related to Vendor for vendor-specific users

Tender

    Represents projects open for bidding

    Contains title, description, deadline

    Related to Category and Status

    Contains collection of Bids

Category

    Classifies tenders into different types

    Simple entity with Id and Name

Vendor

    Represents companies that submit bids

    Contains company information

    Related to User for authentication

    Contains collection of Bids

Bid

    Vendor proposals on tenders

    Contains amount, submission date, comments

    Related to Tender, Vendor, and Status

Status

    Reusable status values for both Tenders and Bids

    Contains status names like "Open", "Pending", "Approved"

## API Endpoints

### Authentication

| Endpoint               | Method | Description                         | Auth Required |
|------------------------|--------|-------------------------------------|---------------|
| `/api/auth/register`   | POST   | Register new user                   | No            |
| `/api/auth/login`      | POST   | Authenticate and get JWT token      | No            |

### Tenders

| Endpoint               | Method | Description                         | Auth Required |
|------------------------|--------|-------------------------------------|---------------|
| `/api/tenders`         | GET    | List all tenders (basic info)       | Yes           |
| `/api/tenders/{id}`    | GET    | Get tender details with bids        | Yes           |
| `/api/tenders`         | POST   | Create new tender                   | Admin         |
| `/api/tenders/{id}`    | PUT    | Update tender                       | Admin         |
| `/api/tenders/{id}`    | DELETE | Delete tender                       | Admin         |

### Vendors

| Endpoint               | Method | Description                         | Auth Required |
|------------------------|--------|-------------------------------------|---------------|
| `/api/vendors`         | GET    | List all vendors                    | Yes           |
| `/api/vendors/{id}`    | GET    | Get vendor details with bids        | Yes           |
| `/api/vendors`         | POST   | Create new vendor                   | Yes           |

### Bids

| Endpoint               | Method | Description                         | Auth Required |
|------------------------|--------|-------------------------------------|---------------|
| `/api/bids`            | POST   | Submit new bid                      | Vendor        |
| `/api/bids/{id}/status`| PUT    | Update bid status                   | Admin         |

### Lookups

| Endpoint               | Method | Description                         | Auth Required |
|------------------------|--------|-------------------------------------|---------------|
| `/api/categories`      | GET    | List all categories                 | Yes           |
| `/api/statuses`        | GET    | List all status values              | Yes           |

## Architecture
Key Patterns

    Clean Architecture: Separation of concerns with Domain, Application, Infrastructure layers

    CQRS: Separate command (write) and query (read) paths

    Repository Pattern: Abstracted data access

    Mediator Pattern: Using MediatR for in-process messaging

Data Access Strategy

    Write Operations: EF Core with code-first migrations

    Read Operations: Dapper for optimized query performance


Getting Started
Prerequisites

    .NET 9 SDK

    SQL Server 

    Docker (optional for containerized deployment)

Installation

    Clone the repository

    Configure connection strings in appsettings.json

    Run database migrations:
    
```bash

dotnet ef database update
```

Run the application:
bash

    dotnet run

Configuration

Key configuration options in appsettings.json:

    Database connection strings

    JWT settings (secret, issuer, audience)

    Logging configuration


### Development Guidelines
#### Code Structure

```
src/
├── TenderManagement.API/          # Presentation layer
├── TenderManagement.Application/  # Business logic
├── TenderManagement.Domain/       # Core domain models
├── TenderManagement.Infrastructure/ # Data access, external services
tests/
├── UnitTests/
├── IntegrationTests/
```

Testing

    Unit Tests: xUnit with FluentAssertions

    Integration Tests: Test API endpoints

    Mocking: NSubstitute for dependencies

Deployment
Docker
```bash

docker-compose up -d
```
IIS

    Publish the application

    Configure IIS site with proper application pool

    Set up database connection

