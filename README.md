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
The Domain is the core of the application. This project does not depend on any other projects.

Below are some things you will find in the Domain project:
- Constants
  - Contains classes for constants related to different values such as MaxLength used to track the maximum length of values like EventName.
- Enums
  - Contains Enums used throughout the application such as CanonType and EventDateType.
- Exceptions
  - Contains Custom exceptions created for various scenarios like AlreadyExistsException or NotFoundException.
- Extensions
  - Contains Extension Methods for various classes such as EnumExtensions that has a function to GetEnumDescription.
- Models
  - Contains Domain Models that are used throughout the application.
- Validation
  - Contains ValidationRules for how objects are validated, reuseable ValidationFailureMessages, and a Validator to apply Validation Rules.

### StarWarsTracker.Logging
The Logging project is responsible for defining how the Custom Logging is handled/exposed.

Below are some things you will find in the Logging project:
- Abstraction
  - IClassLogger
    - This interface defines the contract of how a Class will be able to manipulate a LogMessage. 
  - IClassLoggerFactory
    - This interface defines the contract for the Factory that will return an IClassLogger for different classes to use for logging.
  - ILogConfig
    - This interface defines the contract for the LogConfig that will be stored/saved at startup. This exposes the Default Configurations and Endpoint Override Configurations.
  - ILogConfigReader
    - This interface defines the contract for how a class is able to obtain LogLevel Configurations from the ILogConfig.
  - ILogMessage
    - This interface defines the contract for how you can manipulate and obtain data from the LogMessage.
  - ILogWriter
    - This interface defines the contract for how a LogMessage will be written/saved.
- AppSettingsConfig
  - LogConfigSection
    - LogConfigSection represents the Dictionary (of String, LogLevel) that contains LogLevel Configurations. Each Key would map to a LogLevel.   
  - LogConfigCategory
    - LogConfigCategory represents the Dictionary (of String, LogConfigSection) that contains ConfigSections where each Section is a Dictionary of string, LogLevel.
  - LogConfigSettings
    - LogConfigSettings represents the Dictionary (of String, LogConfigCategory) that contains the Default or Endpoint Override Configurations.
- Implementation
  - This namespace contains the implementation for the interfaces defined in Abstraction (First Bullet Point).

### StarWarsTracker.Persistence
The Persistence layer encapsulates the SQL transactions sent to the Database. This is where the application has a dependency on the Dapper (ORM).
'DataRequests' (Queries/Commands) sent to Dapper use an IDataRequest interface to define how they will GetSql() and GetParameters().

Below are some things you will find in the Persistence project:
- Abstraction
  - IDbConnectionFactory
    - Defines the contract for Factory that returns an IDbConnection to connect to the database.
  - IDataRequest
    - Defines the contract for every request that goes to Dapper. Each request will GetSql() and GetParameters().
    - IDataExecute implements IDataRequest and is used by SQL Commands (Insert, Update, Delete).
    - IDataFetch < T > implements IDataRequest and uses Generics to define the DTO fetched for SQL Queries.
  - IDataAccess
    - This is the Interface abstracting the calls to Dapper.
    - ExecuteAsync(IDataExecute request) - Used for Insert/Update/Delete and returns an int representing the number of rows affected by the SQL command.
    - FetchAsync < T > (IDataFetch < T > request) - Used for SQL Queries and will return the FirstOrDefault <T> defined by the IDataFetch request.
    - FetchListAsync < T > (IDataFetch < T > request) - Used for SQL Queries and will return a collection of the <T> defined by the IDataFetch request.
- BaseDataRequests
  - Contains reuseable BaseRequests for SQL Queries/Commands that may reuse the same parameters.
    - For Example, 'IdParameter' can be reused for a DataRequest where the parameter is just @Id
- DataRequestObjects
  - Contains folders for each table/feature that Queries/Commands are created for.
    - For Example, EventRequests and EventDateRequests.
  - Each DataRequest implements either IDataFetch (Query) or IDataExecute (Command).
    - Both of these implement IDataRequest, so they will use GetSql() and GetParameters() to define the SQL transaction.
- DataTransferObjects
  - Contains the DTOs (Data Transfer Objects) that we will fetch from the database with IDataFetch requests.
- Implementation
  - SqlConnectionFactory
    - Implements IDbConnectionFactory to create SqlConnections.
  - DataAccess
    - Implements IDataAccess to encapsulate Dapper dependency.
  - DependencyInjection
    - Utilize Microsoft.Extensions.DependencyInjection to inject Dependencies into IServiceCollection.

### StarWarsTracker.Application
The Application Project encapsulates the logic on how requests are handled. This is where the Mediator Pattern is implemented. 
The IOrchestrator will receive an IRequest or IRequestResponse, and use the IHandlerFactory to instantiate the appropriate RequestHandler at runtime. 
There is also RequestValidation that is automatically processed for any request that implements the StarWarsTracker.Domain.IValidatabale interface before the handler is initialized.

Below are some things you will find in the Application project:
- Abstraction
  - IBaseHandler
    - This Interface defines a common method that is used to handle a request. The IBaseHandler is implemented by the IRequestHandler and IRequestResponseHandler
  - IHandlerDictionary
    - This interface defines the contract for the Handler Dictionary which will use RequestTypes to locate and return the appropriate RequestHandler.
  - IHandlerFactory
    - This interface defines the contract for the factory that will instantiate a Handler at runtime using the Type of Request that is being received.
  - IOrchestrator
    - This interface defines the contract for the Orchestrator that will act as a mediator between the request caller and the request handler.
  - IRequest
    - This interface will be implemented by any Request (class) that will be executed by the IOrchestrator and does not return a response.
  - IRequestHandler
    - This Handler is used for any class that will handle an IRequest and not return any response type.
  - IRequestResponse
    - This interface will be implemented by any Request (class) that returns a response which will be fetched by the IOrchestrator.
  - IRequestResponseHandler
    - This Handler is used for any class that will handle an IRequest that returns a Response.
  - ITypeActivator
    - This interface defines the contract for the implementation to instantiate an object at run time.
- BaseObjects
  - BaseHandlers
    - Reuseable base classes for Handlers that will have common dependencies for example using the IDataAccess interface.
  - BaseRequests
    - Reuseable base classes for Requests that will have common requestBody and/or validation rules, for example RequiredEventGuidRequest.
- Implementation
    - This namespace contains the implementation for the interfaces defined in Abstraction (First Bullet Point).
- Requests
  - This namespace contains folders/subFolders where the folders are Feature/Table specific and the subFolders are Request specific.
    - Example Folders are EventRequests and EventDateRequests
    - Example SubFolders are EventRequests/Delete or EventRequests/GetByGuid

### StarWarsTracker.Api
The Api Application is the outermost layer of the application. This is what exposes the functionality of the application to the user, Dependencies are injected, and Middlewares are implemented.

Below are some things you will find in the API project:
- Controllers
  - BaseController - Defines common dependencies used by all Controllers and implements logging on requests being handled.
  - Contains various other controllers that expose endpoints such as EventController and EventDateController.
- Middleware
  - ExceptionHandlingMiddleware
    - This class is responsible for acting as a global exception handler in the middleware.
      - This ensures that custom exceptions are returned with consistent response bodies and expected status codes.
      - Other unhandled exceptions are treated as 500 Internal Server Error.
      - Logging is enabled based on Logging Configurations from appsettings.
  - LoggingMiddleware
    - This class is responsible for acting as a global Logger in the middleware.
      - LogConfigReaders have their EndpointOverrides set as the request comes in the pipeline.
      - Additional Try/Catch is enabled in case of any exceptions making it past Global Exception Handler.
      - LogMessage is saved using ILogWriter when the request is leaving the pipeline.     
  - AppSettings
    - Although it is added to the gitIgnore, the AppSettings is housed in the API project.
    - AppSettings has configurations such as the Database ConnectionString and the Logging Configurations.

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
