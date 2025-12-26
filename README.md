🚀 DigiLearn Project
📌 English Version

Overview

Tony is a modular backend solution built with ASP.NET Core and designed based on Clean Architecture, CQRS, and Domain-Driven Design (DDD) principles.

The project is structured to be scalable, maintainable, and event-driven, leveraging MongoDB for persistence and RabbitMQ for asynchronous messaging and inter-service communication.

🏗 Project Structure
src/
 ├── Common/
 │   ├── Common.Application
 │   ├── Common.Domain
 │   ├── Common.EventBus
 │   ├── Common.Infrastructure
 │   └── Common.Query
 │       ├── MongoDb
 │       ├── Filters
 │       ├── BaseDto
 │       ├── IQuery / IQueryHandler
 │
 ├── EndPoints/
 │   └── Tony.Web

📦 Core Modules
🔹 Common.Domain

Domain entities, value objects, and business rules

No dependency on infrastructure or frameworks

🔹 Common.Application

Application services and use cases

Coordinates domain logic and workflows

🔹 Common.Infrastructure

Database and external service implementations

MongoDB context and repositories

RabbitMQ connection and configuration

🔹 Common.Query

Query side implementation of CQRS

Generic query handlers

MongoDB paging, filtering, and query extensions

🔹 Common.EventBus

Event-driven architecture layer

Integration with RabbitMQ

Publishes and consumes domain and integration events

Enables loose coupling between modules

🔹 BTCSalman.Web

ASP.NET Core Web API

Exposes HTTP endpoints

Authentication via Microsoft Identity Platform

Frontend dependencies managed via npm

🛠 Technologies & Tools

ASP.NET Core

.NET

MongoDB

RabbitMQ

CQRS Pattern

Domain-Driven Design (DDD)

Microsoft Identity Platform

npm

📄 License

This project is licensed under the MIT License, allowing free use, modification, and distribution.
