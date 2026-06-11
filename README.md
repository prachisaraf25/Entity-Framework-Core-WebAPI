# Entity Framework Core Web API

## Overview

This project is a practice-based ASP.NET Core Web API application developed to explore and implement Entity Framework Core features with SQL Server. It demonstrates CRUD operations, relationship management, data loading strategies, raw SQL execution, stored procedures, and bulk operations using modern EF Core capabilities.

The project serves as a hands-on learning resource for understanding how Entity Framework Core is used in real-world .NET applications.

---

## Technologies Used

- C#
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- LINQ
- Swagger / OpenAPI
- RESTful APIs

---

## Features Implemented

### CRUD Operations

- Create new records
- Retrieve records
- Update records
- Delete records

### Entity Framework Core Concepts

#### Data Retrieval
- LINQ Queries
- Projections using `Select`
- Asynchronous Queries

#### Relationship Handling
- Navigation Properties
- One-to-Many Relationships
- Foreign Key Mapping

#### Loading Related Data
- Eager Loading (`Include`)
- Explicit Loading (`Reference` and `Collection`)

#### SQL Integration
- Raw SQL Queries
- Parameterized SQL Queries
- Stored Procedure Execution
- Execute SQL Commands

#### Bulk Operations
- Bulk Insert (`AddRangeAsync`)
- Bulk Update (`ExecuteUpdateAsync`)
- Bulk Delete (`ExecuteDeleteAsync`)

---

## Project Structure

```text
Entity-Framework-Core-WebAPI
│
├── Controllers
│   └── BooksController.cs
│
├── Data
│   ├── AppDbContext.cs
│   ├── Book.cs
│   ├── Language.cs
│   └── Author.cs
│
├── Migrations
│
├── Program.cs
│
├── appsettings.json
│
└── README.md
```

---

## API Endpoints

| Method | Endpoint | Description |
|----------|----------|-------------|
| POST | `/api/books` | Create a new book |
| GET | `/api/books` | Get all books |
| GET | `/api/books/getRelatedData` | Retrieve related data using navigation properties |
| GET | `/api/books/getSQLQueryData` | Execute SQL query / stored procedure |
| POST | `/api/books/bulkInsert` | Insert multiple records |
| PUT | `/api/books/{id}` | Update a specific record |
| PUT | `/api/books` | Update using EF Core Update() |
| PUT | `/api/books/bulkUpdate` | Bulk update records |
| DELETE | `/api/books/{id}` | Delete a specific record |
| DELETE | `/api/books/bulk` | Bulk delete records |

---

## Getting Started

### Clone the Repository

```bash
git clone https://github.com/your-username/Entity-Framework-Core-WebAPI.git
```

### Restore Dependencies

```bash
dotnet restore
```

### Configure Database

Update the database connection string in your local configuration file before running the application.

### Apply Migrations

```bash
dotnet ef database update
```

### Run the Application

```bash
dotnet run
```

### Access Swagger

```text
https://localhost:<port>/swagger
```

---

## Key Learning Areas

This project demonstrates:

- Entity Framework Core Fundamentals
- Code-First Approach
- CRUD Operations
- Navigation Properties
- Relationship Mapping
- Eager Loading
- Explicit Loading
- LINQ Queries
- Raw SQL Queries
- Stored Procedures
- Bulk Operations
- SQL Server Integration
- ASP.NET Core Web API Development
- Asynchronous Programming

---

## Purpose

The purpose of this project is to gain practical experience with Entity Framework Core and ASP.NET Core Web API development by implementing commonly used database operations and patterns found in enterprise applications.
