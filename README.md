# DotNet8_CQRS_BaseProject

A foundational project implementing CQRS with .NET 8.0.

The primary goal of this repository is to create a base project using CQRS with .NET 8.0 for my personal use.

## Database

The project uses [PostgreSQL](https://www.postgresql.org/) as the primary database.

Also, it uses [Dapper](https://www.learndapper.com/) for the reading operations while keep using [EntityFramework](https://learn.microsoft.com/en-us/aspnet/entity-framework) to the writing ones.

## Repository and UnitOfWork patterns

The project utilizes [reflection](https://learn.microsoft.com/en-us/dotnet/fundamentals/reflection/reflection) in order to inject the repositories from withing the UnitOfWork class.

## CQRS pattern

With CQRS implementation, each operation can not only have its own unique idiosyncrasies but also be easily visualized for potential separation into different microservices.
