## Welcome to the StarWarsTrackerApi

This README is setup to help you get started with running this project locally. 
If you have any questions please reach out to Daniel Aguirre.

Steps to complete:
1. Install Required Applications
	- Skip if you already have SqlServer, SSMS, and your IDE installed.
2. Database Setup
	- Create/Publish Database
	- Add appsettings.json to API Project
	- Add ConnectionString to appsettings.json
3. Logging Configuration Setup
	- Add Default Logging Configs For Development Environment
	- Understanding The Logging Configs
4. Tests Setup
    - Create Hidden.cs file
    - Run all the tests
---

## Required Installations:
Please ensure you have the following installed:
- Visual Studios Community (or preferred IDE)
- SqlServer
- SSMS (Sql Server Management Studios)

---

## Database Setup:

### 1. Create/Publish Your Database:
- Create a SqlServer database locally in SSMS (Sql Server Management Studios)
- Right-Click StarWarsTracker.SqlServerDatabase project
- Click 'Publish...'
- Select your local SqlServer database
	- Click Edit and find your local database.
	- You can click SaveProfileAs to save the publish location in StarWarsTracker.SqlServerDatabase.PublishLocations
		- This helps skip the step of editing/locating your local database when publishing changes in the future.
- Click Publish or Generate Script
	- If you generate the script you can review it first or run it manually.


### 2. Add appsettings.json to API Project:
- Add an appsettings.json to the StarWarsTracker.Api project
	- Right-Click StarWarsTracker.Api
	- Click Add -> New Item
	- Select 'App Settings File'
		- You can search in the top right for 'appsettings'
		- Create 'appsettings.json' file

### 3. Add ConnectionString to appsettings.json
Now that you've set up your database and added an appsetting.json to the API project, you will need to add the connection string to the appSettings.

Your appsettings probably looks like this:
```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

We will add the ConnectionString like this:
```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "Default": "!!! REPLACE THIS STRING WITH YOUR CONNECTION STRING !!!"
  },
  "AllowedHosts": "*"
}
```

You should be able to run the API locally now - however logging will not be enabled and some of the test projects may have a compile error. Continue on to finish setup.

---

## Logging Configuration Setup:

### 1. Add Default Logging Configs For Development Environment

To enable logging, we need to add the configurations to the appsettings. After adding the below json to your appsettings we will review what each section is/means and how to add new configs.

In the appsettings.json, your "Logging" config probably looks like this:
```
"Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
```
Replace that section with the configurations below:
```
 "Logging": {
    "Environment": {
      "Development": {
        "Default": {
          "CustomLogLevels": {
            "DatabaseLogSettings": {
              "LogMessageLevelToWrite": "Trace",
              "LogContentLevelToWrite": "Trace"
            },
            "SqlExecute": {
              "SqlRequestLogLevel": "Information",              
              "SqlRequestLogDetails": "Trace",
              "SqlResponseLogLevel": "Information",              
              "SqlResponseLogDetails": "Trace"
            },
            "SqlFetch": {
              "SqlRequestLogLevel": "Information",
              "SqlRequestLogDetails": "Trace",
              "SqlResponseLogLevel": "Information",
              "SqlResponseLogDetails": "Trace"
            },
            "SqlFetchList": {
              "SqlRequestLogLevel": "Information",
              "SqlRequestLogDetails": "Trace",
              "SqlResponseLogLevel": "Information",
              "SqlResponseLogDetails": "Trace"
            },
            "ExceptionLogging": {
              "DefaultExceptionLogLevel": "Critical",
              "DoesNotExistExceptionLogLevel": "Debug",
              "AlreadyExistsExceptionLogLevel": "Debug",
              "ValidationFailureExceptionLogLevel": "Debug"
            },
            "ControllerLogging": {
              "ControllerRequestBodyLogLevel": "Debug",
              "ControllerResponseBodyLogLevel": "Debug"
            }
          },
          "OverrideLogLevelByNameSpace": {
            "StarWarsTracker.Application.Implementation": {
              "Trace": "Critical",
              "Information": "Critical"
            }
          },
          "OverrideLogLevelByClassName": {
            "HandlerFactory": {
              "Information": "Critical"
            }
          }
        },
        "Event/GetAllEventsNotHavingDates": {
          "OverrideLogLevelByClassName": {
            "HandlerFactory": {
              "Trace": "Critical",
              "Information": "Critical"
            }
          }
        }
      },
      "Staging": {
      
      },
      "Production": {

      }
    }
  },
```
Note: This is only setting the Development configurations, you can leave them empty for now, re-use them for Staging/Production, or set separate configurations for Staging/Production.

### 2. Understanding The Logging Configs

#### Here are some details about different sections of the logging configs:
- Environment
    - The Environment section defines which configs to use when the application is run in different environments (Development, Staging, Production).
- Development
    - The Development section defines the configs that are used when the application is running in Development
    - Staging and Production sections work the same, defining the configs used when the application runs in those environments.
- Default
    - These are the default configurations that will be used for any API Request that is received.
- ConfigCategories
    - The ConfigCategories are used for different categories of logging.
    - Example Categories: 'CustomLogLevels', 'OverrideLogLevelByNameSpace', and 'OverrideLogLevelByClassName'.
    - Each Category has a ConfigSection.
- ConfigSections
    - The ConfigSections are used to store related configuration keys/values.
    - Example Sections: 'DatabaseLogSettings', 'SqlExecute', and 'HandlerFactory'
    - Each Section is basically a Dictionary (of string, string) where the values are names of LogLevels.
- ConfigKeys
    - The ConfigKey is the key that is mapped to a specific ConfigValue.    
    - Example Keys: 'LogMessageLevelToWrite', 'SqlRequestLogLevel', and 'ControllerRequestBodyLogLevel'.
- ConfigValues
    - The ConfigValue is the actual LogLevel that is being configured.
    - Each Value must be a valid LogLevel
        - Trace, Debug, Information, Warning, Error, Critical, None

#### Here are some details about the logging override configs:
- Endpoint Overrides
    - Endpoint Overrides are siblings to the 'Default' section.
    - The Key should be the route, for example: 'Event/GetAllEventsNotHavingDates'
    - Any Configurations not defined will use what is defined in the 'Default' section.
- Namespace Overrides
    - Namespace Overrides exist in the 'OverrideLogLevelByNameSpace' Category
    - Namespace matching is done using .Equals() and is not case sensitive.
    - Example NamespaceOverride is 'StarWarsTracker.Application.Implementation'
    - Each Namespace Override contains a ConfigSection
        - The Keys are the LogLevel to override 
        - The Values are the value you want the LogLevel to be treated as.
        - Example: "Trace": "Critical" <- Overrides Trace messages to be treated as Critical.
- ClassName Overrides
    - ClassName Overrides exist in the 'OverrideLogLevelByClassName' Category
    - ClassName matching is exact match and is case sensitive.
    - Example ClassNameOverride is 'HandlerFactory'
    - Each ClassName Override contains a ConfigSection
        - These work the same as the NameSpace Override ConfigSections.
        - Example: "Information": "None" <- Overrides Informaton messages to be treated as None.

You should be able to run the API locally now and check the Log table in the Database to see the logs saved based on your configuration values. Continue on to finish setup.

---

### Tests Setup:

For now the tests are set up using a class that is added to the GitIgnore to provide the database ConnectionString.

- Right click StarWarsTracker.Tests.Shared
- Click Add -> Class...
- Name the class: Hidden.cs
    - The Hidden.cs should already be added to your GitIgnore.

Update your Hidden.cs class to look like the following:

```
 public static class Hidden
{
    public const string DbServer = "!!! REPLACE WITH YOUR DATABASE SERVER !!!";

    public const string DbName = "!!! REPLACE WITH YOUR DATABASE NAME !!!";
}
```

Note: You may want to consider using a separate database for your tests vs running locally, but that decision is up to you.

You should be able to run your tests now and all should pass.

---

Thank you for reviewing the README-Setup. If you have any questions, or suggestions for improvement, please let me (Daniel Aguirre) know.
