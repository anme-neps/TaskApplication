# Task Management API

## Overview
The **Task Management API** is a .NET Core 6 application for managing tasks and users. It supports CRUD operations for tasks and users, role-based access control (RBAC), and is protected using Bearer token authentication.

---

## Features
- **Users**:
  - Create, update, retrieve, and delete users.
  - Role-based access control (Admin/User roles).
- **Tasks**:
  - Admin can create, update, delete, and list all tasks.
  - Users can only view and update their assigned tasks.
- **Authentication**:
  - JWT Bearer token authentication for secure API access.
- **Documentation**:
  - Interactive API documentation with Swagger UI.

---

## Technologies Used
- **.NET 8**: Framework for building the API.
- **Entity Framework Core**: ORM for database interactions.
- **In-Memory Database**: Used for development and testing.
- **JWT Authentication**: For secure API authentication.
- **Swagger/OpenAPI**: API documentation.

---

## Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Any API testing tool (e.g., [Postman](https://www.postman.com/) or Swagger UI).

---

## Getting Started

### 1. Clone the Repository
```bash
git clone https://github.com/your-repository-url.git
cd TaskManagementAPI
```
### 2. Build the project
```bash
 dotnet build
```
### 3. Update Migration
```bash
dotnet ef database update
```
### 4. Run Project
```bash
dotnet run
```

## Login Details
- **Admin Login**
  - Email: admin@example.com
  - Password: admin
- **User Login**
  - Email: user@example.com
  - Password: user
