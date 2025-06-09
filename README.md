## Skypoint Social Platform - Backend API

This is the backend service for the Skypoint Social Platform, a social media application for Skypoint employees.

## 🚀 Tech Stack

- .NET 8 Web API
- Clean Architecture
- Entity Framework Core with SQL Server
- JWT Authentication
- Dependency Injection

## 📦 Features

- ✅ User Registration & Login (JWT based)
- ✅ Post creation
- ✅ Voting (upvote/downvote)
- ✅ Follow/Unfollow users
- ✅ Personalized newsfeed
- ✅ Session tracking on logout

## 🗂 Project Structure

- `Skypoint.API`: API Layer (Controllers)
- `Skypoint.Application`: Application Layer (DTOs, IServices)
- `Skypoint.Domain`: Domain Layer (Entities)
- `Skypoint.Infrastructure`: Infrastructure Layer (DbContext, Service, Migrations)

## 🔧 Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/Snehal1001/skypoint-social-backend.git
cd skypoint-social-backend
```

````

### 2. Set up the database

```bash
dotnet ef database update --project ./Skypoint.Infrastructure --startup-project ./Skypoint.API
```

### 3. Run the project

```bash
dotnet run --project Skypoint.API
```

### 4. Test the API

Use Swagger (if enabled) or a tool like Postman to test endpoints:

- `POST Auth/signup`
- `POST Auth/login`
- `POST Auth/logout`
- `POST Post/create`
- `GET PostFeed/`
- `POST Vote/`
- `POST UserFollow/follow`
- `POST UserFollow/unfollow`
````
