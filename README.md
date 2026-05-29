# FleetEdge 🚛

> **Enterprise Fleet & Logistics Management Platform**  
> Built with .NET 8 Microservices · Angular 21 · Docker · RabbitMQ

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=flat-square&logo=dotnet)
![Angular](https://img.shields.io/badge/Angular-21-DD0031?style=flat-square&logo=angular)
![Docker](https://img.shields.io/badge/Docker-Compose-2496ED?style=flat-square&logo=docker)
![RabbitMQ](https://img.shields.io/badge/RabbitMQ-MassTransit-FF6600?style=flat-square&logo=rabbitmq)
![SQL Server](https://img.shields.io/badge/SQL_Server-2022-CC2927?style=flat-square&logo=microsoftsqlserver)
![Redis](https://img.shields.io/badge/Redis-7-DC382D?style=flat-square&logo=redis)

---

## 📌 Overview

**FleetEdge** is a cloud-ready, event-driven enterprise platform for managing vehicles, drivers, trips, fuel, and maintenance operations. Built following MNC-grade software architecture standards with a full microservices approach, clean architecture per service, CQRS with MediatR, and a reactive Angular frontend.

This project is designed as a **professional portfolio piece** demonstrating real-world enterprise patterns used in top software companies.

---

## ✨ Key Features

- 🔐 **JWT Authentication** with role-based access control (SuperAdmin, FleetManager, Driver, Dispatcher)
- 🚛 **Fleet Management** — Vehicle CRUD (Truck, Van, Car), status tracking, capacity management
- 👨‍✈️ **Driver Management** — Driver profiles, license tracking, assignment management
- 🗺️ **Trip Management** — Trip lifecycle (Plan → Start → Complete), route management, capacity validation
- ⛽ **Fuel Management** — Manual fuel logs, consumption tracking, cost per trip
- 🔧 **Maintenance Management** — Scheduled maintenance + breakdown logging, auto vehicle blocking
- 🔔 **In-App Notifications** — Event-driven notifications via RabbitMQ
- 📊 **Reports** — Excel export, PDF export, dashboard charts
- 🌐 **API Gateway** — Ocelot with JWT validation and rate limiting

---

## 🏗️ Architecture

```
┌─────────────────────────────────────────────────────┐
│                   Angular 17 SPA                     │
│          (NgRx · Material · Standalone Components)   │
└─────────────────────┬───────────────────────────────┘
                      │ HTTP
┌─────────────────────▼───────────────────────────────┐
│              API Gateway (Ocelot) :5000              │
│         JWT Validation · Rate Limiting · Routing     │
└──┬──────┬──────┬──────┬──────┬──────┬──────┬────────┘
   │      │      │      │      │      │      │
:5001  :5002  :5003  :5004  :5005  :5006  :5007  :5008
Identity Fleet Driver  Trip   Fuel  Maint  Notif  Report
   │      │      │      │      │      │      │
└──┴──────┴──────┴──────┴──────┴──────┴──────┴────────┘
                      │ Events
┌─────────────────────▼───────────────────────────────┐
│                   RabbitMQ                           │
└─────────────────────────────────────────────────────┘
```

---

## 🧱 Microservices

| # | Service | Port | Responsibility |
|---|---------|------|----------------|
| 1 | **Identity Service** | 5001 | JWT auth, user management, roles, refresh tokens |
| 2 | **Fleet Service** | 5002 | Vehicle CRUD, types, capacity, status management |
| 3 | **Driver Service** | 5003 | Driver profiles, license tracking, assignments |
| 4 | **Trip Service** | 5004 | Trip lifecycle, route management, capacity validation |
| 5 | **Fuel Service** | 5005 | Manual fuel logs, consumption tracking, cost analysis |
| 6 | **Maintenance Service** | 5006 | Scheduled + breakdown maintenance, vehicle blocking |
| 7 | **Notification Service** | 5007 | In-app notifications via RabbitMQ events |
| 8 | **Report Service** | 5008 | Excel/PDF exports, dashboard analytics |
| 9 | **API Gateway** | 5000 | Ocelot routing, JWT validation, rate limiting |

---

## 🛠️ Tech Stack

### Backend
| Technology | Usage |
|-----------|-------|
| .NET 8 Web API | One API project per microservice |
| Entity Framework Core 8 | Code-First ORM with migrations |
| MediatR | CQRS — Commands & Queries |
| FluentValidation | Request validation |
| MassTransit + RabbitMQ | Event-driven messaging |
| Ocelot | API Gateway |
| Serilog + Seq | Structured centralized logging |
| Redis | Session caching, rate limiting |
| SQL Server 2022 | One database per service |
| Swagger / OpenAPI | API documentation per service |

### Frontend
| Technology | Usage |
|-----------|-------|
| Angular 17 | SPA with Standalone Components |
| NgRx | State management (one store per feature) |
| Angular Material | UI component library |
| RxJS | Reactive streams |
| HttpClient Interceptors | Auth, error, loading handling |
| Angular Guards | Role-based route protection |
| Reactive Forms | FormBuilder-based forms |

### Infrastructure
| Technology | Usage |
|-----------|-------|
| Docker + Docker Compose | Full stack containerization |
| RabbitMQ | Message broker |
| Redis | Caching layer |
| Seq | Centralized log viewer |
| SQL Server | Relational database |

---

## 🏛️ Architecture Patterns

- ✅ **Clean Architecture** — Domain / Application / Infrastructure / API (per service)
- ✅ **CQRS with MediatR** — Commands for writes, Queries for reads
- ✅ **Repository Pattern + Unit of Work**
- ✅ **Outbox Pattern** — Reliable RabbitMQ message publishing
- ✅ **Saga Pattern** — Multi-service distributed transaction management
- ✅ **Domain Events** — Via MediatR notifications
- ✅ **Soft Delete** — IsDeleted flag on all entities
- ✅ **BaseEntity** — Id, CreatedAt, UpdatedAt, IsDeleted on all entities

---

## 🐇 RabbitMQ Event Map

| Publisher | Event | Consumer(s) |
|-----------|-------|-------------|
| Fleet Service | `VehicleAssignedEvent` | Trip Service |
| Fleet Service | `VehicleUnderMaintenanceEvent` | Trip Service |
| Trip Service | `TripStartedEvent` | Fuel Service |
| Trip Service | `TripCompletedEvent` | Report Service, Fuel Service |
| Maintenance Service | `MaintenanceDueEvent` | Notification Service |
| Maintenance Service | `BreakdownLoggedEvent` | Notification Service, Fleet Service |
| Fuel Service | `LowFuelAlertEvent` | Notification Service |
| Identity Service | `UserCreatedEvent` | Notification Service |

---

## 🔐 Role-Permission Matrix

| Feature | SuperAdmin | FleetManager | Driver | Dispatcher |
|---------|:---:|:---:|:---:|:---:|
| Manage Users | ✅ | ❌ | ❌ | ❌ |
| Manage Vehicles | ✅ | ✅ | ❌ | ❌ |
| Manage Drivers | ✅ | ✅ | ❌ | ❌ |
| Assign Trips | ✅ | ✅ | ❌ | ✅ |
| View Own Trip | ✅ | ✅ | ✅ | ✅ |
| Log Fuel | ✅ | ✅ | ✅ | ❌ |
| Manage Maintenance | ✅ | ✅ | ❌ | ❌ |
| View Reports | ✅ | ✅ | ❌ | ✅ |
| Export Reports | ✅ | ✅ | ❌ | ❌ |

---

## 📁 Folder Structure

```
FleetEdge/
├── services/
│   ├── FleetEdge.IdentityService/
│   │   ├── FleetEdge.Identity.Domain/
│   │   ├── FleetEdge.Identity.Application/
│   │   ├── FleetEdge.Identity.Infrastructure/
│   │   └── FleetEdge.Identity.API/
│   ├── FleetEdge.FleetService/
│   ├── FleetEdge.DriverService/
│   ├── FleetEdge.TripService/
│   ├── FleetEdge.FuelService/
│   ├── FleetEdge.MaintenanceService/
│   ├── FleetEdge.NotificationService/
│   ├── FleetEdge.ReportService/
│   └── FleetEdge.Gateway/
├── frontend/
│   └── fleetedge-angular/
│       └── src/app/
│           ├── core/
│           ├── shared/
│           ├── features/
│           └── store/
├── docker-compose.yml
├── docker-compose.override.yml
├── .env
└── FleetEdge.sln
```

---

## 🚀 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Node.js 20+](https://nodejs.org/)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- [SQL Server 2022](https://www.microsoft.com/en-us/sql-server/)

### Run with Docker Compose

```bash
# Clone the repository
git clone https://github.com/alasaneeth/fleet-edge-and-logistics.git
cd fleet-edge-and-logistics

# Copy environment variables
cp .env.example .env

# Start all services
docker-compose up --build
```

### Service URLs

| Service | URL |
|---------|-----|
| API Gateway | http://localhost:5000 |
| Identity Service Swagger | http://localhost:5001/swagger |
| Fleet Service Swagger | http://localhost:5002/swagger |
| Angular Frontend | http://localhost:4200 |
| RabbitMQ Management | http://localhost:15672 |
| Seq Logs | http://localhost:5341 |
| Redis | localhost:6379 |

---

## 🗓️ Sprint Plan

| Sprint | Focus | Duration |
|--------|-------|----------|
| Sprint 1 | Infrastructure + Identity Service | 2 weeks |
| Sprint 2 | Fleet Service + Driver Service | 2 weeks |
| Sprint 3 | Trip Service + API Gateway | 2 weeks |
| Sprint 4 | Fuel Service + Maintenance Service | 2 weeks |
| Sprint 5 | Notification Service | 2 weeks |
| Sprint 6 | Report Service | 2 weeks |
| Sprint 7 | Angular Foundation | 2 weeks |
| Sprint 8 | Angular Features | 2 weeks |
| Sprint 9 | Angular Reports + Notifications | 2 weeks |
| Sprint 10 | Docker + Integration + Polish | 2 weeks |

---

## 🌿 Branch Strategy

```
main                          → production
develop                       → integration
feature/sprintX-service-name  → active development
```

---

## 👨‍💻 Author

Aasif Saneeth  
Full-Stack Developer | .NET + Angular  
[LinkedIn](www.linkedin.com/in/alasaneeth) · [GitHub](https://github.com/alasaneeth)

---

## 📄 License

This project is licensed under the MIT License — see the [LICENSE](LICENSE) file for details.

---
