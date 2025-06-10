# Client Software - Backend 

## Tech Stack

- .NET 8
- C# 12
- Entity Framework 8
- SQL
- Azure

## Summary

This backend project is built with C# and .NET 8, designed for performance, maintainability, and scalability in a production-grade logistics environment. The application follows a layered architecture, separating concerns into distinct Application, Infrastructure, Client, and API projects. The API adheres to modern standards (RESTful design, HTTP status conventions), and is secured using ASP.NET Identity for authentication and authorisation. It integrates with external services such as maps, email providers, and Azure resources, and uses NSwag to automatically generate and update client-side API code. The app is deployed to Azure using CI/CD pipelines via GitHub, and makes use of Key Vault for securing secrets. Performance is enhanced through Entity Framework, SQL stored procedures, retry logic for unreliable third-party services, and detailed logging via Azure Application Insights. Middleware handles time tracking, slow request alerts, and complete audit logging of all data changes.

## Features

- Built in C# with .NET 8, structured using a layered architecture:
  - Application Layer for business logic and use cases.
  - Infrastructure Layer for data access, file storage, and external services.
  - Client Layer for generating frontend files.
  - API Layer for exposing endpoints.
- Layered design improves testability, enforces separation of concerns, and supports easier scaling and onboarding.
- Secure authentication & authorisation using ASP.NET Identity with JWT support, scoped access, and role-based permissions.
- Integrated with third-party services.
- Automatically generates TypeScript/JavaScript clients using NSwag, keeping frontend clients in sync with backend changes.
- Deployed to Azure App Service and Azure Static Web Apps, with full CI/CD pipelines using GitHub Actions.
- Azure Key Vault used for secure storage of sensitive configuration, secrets, and connection strings.
- Uses Entity Framework Core and SQL stored procedures for database communications.
- Handles third-party instability using resilience patterns like retries, and fallback logic.
- Implements full logging and monitoring via Azure Application Insights, with structured logs and exception-level triggers.
- Adheres to clean code principles including single-responsibility design, consistent naming, dependency injection, and testable modular services.

## Notes

Some features in this project have been disabled, redacted, or replaced with placeholder data to protect sensitive client information. While the core structure and functionality are representative, certain components may not function as expected in this public version. Please feel free to reach out if you’d like further technical details or clarification.




© Daniel Wood (https://github.com/thedanwood) 2025

This project is publicly visible for portfolio and educational purposes only.  
Unauthorized use, distribution, or reproduction of this code is not permitted.  
Please contact me if you are interested in using any part of this work.
