# Welcome to the StarWarsTrackerApi Repository!

ASP.Net Core Api for tracking Events that happen during the star wars timeline.

This is an Application built using Clean Architecture and the Mediator Pattern. Unit and Integration Tests are set up with xUnit Testing Framework.

---

## Table Of Contents

Coming soon - Table of Contents

---

## Overview
This application is an ASP.Net Core Web API build using Clean Architecture. 
The primary purpose for this application is to track Events that happen in StarWars and for users to track what events they have consumed. 

Events would be:
- Canon and/or Legends
- Referenced in Source Material
- Tied to People/Organizations and/or Planets/Locations
- Happen at a definitive or speculative time/timeframe based on 'Years Since Battle Of Yavin'

---

## Project Goals
The goal of this API is to expose functionality to save/fetch events that have happened in StarWars. Each event would not only have when and what happened, but what locations were visited during the event and any people who were involved. 
Timelines of events will be fetchable by People, Locations, Planets, Organizations, Source Material, Continuity, and/or a specified date range.

### Completed 
- Foundational Infrastructure Set Up
- CRUD Functionality For Events
- Needed Functionality For EventDates

### To Be Completed
- Define Remaining Data Contracts
- CRUD Functionality For Planets
- CRUD Functionality For Locations
- Mapping Events To Locations
- CRUD Functionality For People
- CRUD Functionality For Organizations
- Mapping People To Organizations
- Mapping People To Events
- CRUD Functionality For Source Material
- Mapping Source Material To Events
- Fetching Timelines
- UserEvents For Tracking Consumed Source Material

---

## What Does This Application Allow A User To Do?
The primary functionality for this application is to track events that happen in Star Wars. 
Events will be fetchable by People, Locations, Planets, Organizations, Source Material, Continuity, and/or a specified date range.

### Events
CRUD and basic search functionality is available via the Event endpoints:
- Event/InsertEvent
- Event/DeleteEventByGuid
- Event/GetAllEventsNotHavingDates
- Event/GetEventByNameAndCanonType
- Event/GetEventsByNameLike
- Event/GetEventsByYear

### EventDates
Insert/Delete Functionality For EventDates that define when a specific Event happened.
- EventDate/InsertEventDates
- EventDate/DeleteEventDatesByEventGuid

---

## What Technologies/Dependencies Were Used To Build This Application?
This Application was built using the following frameworks/nuget packages:
- Api Framework: Asp.Net Core Api (.Net 6)
- Microsoft.Extensions.DependencyInjection.Abstraction
  - Used To Encapsulate Dependency Injection 
  - Dependency In Multiple Projects      
      - StarWarsTracker.Application
      - StarWarsTracker.Logging
      - StarWarsTracker.Persistence    
- Dapper (ORM)
  - Object Relational Mapper used to send SQL Transactions to Database
  - Dependency in StarWarsTracker.Persistence
- System.Data.SqlClient  
  - Dependency In StarWarsTracker.Persistence
- Swashbuckle.AspNetCore
  - Swagger
  - Dependency in StarWarsTracker.Api
- xUnit
  - Test Projects for this application are created using xUnit.
  - Dependency in all Test Projects
- Moq
  - Unit Tests are set up by Mocking Dependencies using the popular Moq Framework
  - Dependency in StarWarsTracker.Application.Tests & StarWarsTracker.Tests.Shared 
- Genfu
  - This package is helpful for Generating Fake Data to use in Unit Tests.
  - Dependency in StarWarsTracker.Application.Tests

---

## How Is This Application Structured
This Application is structured following the Clean Architecture (or sometimes referred to as Ports and Adapters) Pattern. 
The Api also utilizes the Mediator pattern to separate the api from any of the logic on how requests are handled. 
This means that for each endpoint there is a Request object, which is tied to a Handler object. Each Handler defines how that request is handled. 

Below is how each project depends on each other:
- StarWarsTracker.Domain
  - This is the center of the application, does not depend on any other applications.
- StarWarsTracker.SqlServerDatabase
  - This project/port is the Database structure (and Post-Deployment Scripts) to build the database
  - This does not depend on any other applications.
- StarWarsTracker.Persistence
  - This project/port encapsulates the interactions with the database.
  - Depends on StarWarsTracker.Domain and StarWarsTracker.Logging 
- StarWarsTracker.Logging
  - This project/port encapsulates the custom logging implementation.
  - Depends on StarWarsTracker.Domain 
- StarWarsTracker.Application
  - This project/port encapsulates the logic for how requests are handled.
  - Depends on StarWarsTracker.Domain, StarWarsTracker.Logging, and StarWarsTracker.Persistence 
- StarWarsTracker.Api
  - This .Net 6 ASP.Net Core Web API project/port exposes functionalty to consumers of this api.
  - Depends on StarWarsTracker.Application

---

## Projects
With this application following Clean Architecture (or sometimes referred to as Ports And Adapters), each project would represent a different Port for this Application. 
The Domain is the core of the Application, so it has no dependencies on any other projects. 
Each of the other Projects/Ports would encapsulate functionality to a different section of the Application. 
For example, all interactions with the Database are encapsulated in the StarWarsTracker.Persistence project. 
In the below sections we will discuss each projects responsibility and any notes to highlight from that project.

### StarWarsTracker.Domain
Info about Domain Layer

### StarWarsTracker.Logging
Info about Logging Layer

### StarWarsTracker.Persistence
Info about DataAccess Layer

### StarWarsTracker.Application
Info about Application Layer

### StarWarsTracker.Api
Info about Api Layer

### StarWarsTracker.SqlServiceDatabase
Info about Sql Server Database

---

## Testing Projects
Testing is an important aspect of every application. This application ensures stability using both Integration and Unit tests with the popular framework xUnit. 
Each test project is responsible for testing a separate Port (Class Library) for the application. In this section we will review the test coverage for this project.

### StarWarsTracker.Api.Tests
Api Tests

### StarWarsTracker.Application.Tests
Application Tests

### StarWarsTracker.Domain.Tests
Domain Tests

### StarWarsTracker.Logging.Tests
Logging Tests

### StarWarsTracker.Persistence.Tests
DataAccess Tests

### StarWarsTracker.Tests.Shared
Shared functionality for tests

---
