# ASP.NET Core Hosted Services

- Hosted Services is the same as background services
-- Downsize of using hosted services is that the data may be out of date

- Use Cases
-- Polling for data from an external service
-- Responding to external messages or events
-- Performing data-intensive work, outside of the request lifecycle
-- Uploading and processing large files
--- Save large file on the server and notify user (Hosted Service)
--- Hosted Service will parse the file and process the file (Hosted Service)
-- System.Threading.Channels
--- Thread-safe data transfer
--- Have one or more producer, and one or more consumer

- Architecture Warning
-- If you find yourself with many hosted services than it contains too many responsibilities
--- In this case, use a micro-service architecture

- BackgroundService implements IHostedService, IDisposable

## Coordinating between requests and hosted services

- Using channels to transfer data
- Improve response times

## Building .NET Core Worker Services

- A worker service is simply a console service
- Works with no user interaction
- Similar to a Windows Service
- Scheduled workloads
- Hosting supports for long-running services
- Cloud Native worker Micro-Services

- Common Workloads or Use Cases
-- Processing messages/events from a queue, service bus or event stream
-- Reacting to file changes in a object/file store
-- Aggregating data from a data store
-- Enriching data in data ingestion pipelines
-- Formatting and cleansing of AI/ML datasets

## Create a worker service project using the .NET CLI

```<c#>
    # .NET CLI
    dotnet new worker -n "TennisBookings.ScoreProcessor"
```

## Decoupling services with Queue

- MicroService Concept
-- Web Application interacts with worker services via sending messages through queues
--- Based on volume of work, you can scale up the amount of worker services instances
--- Poll Azure Queues for new messages

- AWS
- Setup LocalStack
- Docker
- complex data processing
-- Advanced Data and Stream Processing with Microsoft TPL Dataflow => .NET
--- Problem TPL Dataflow is trying to solve
---- Writing High parallel execution => large data, execution, and parallel execution
--- TPL DataFlow => Task-Parallel-Library (TPL) => Non-Linear programming
---- Parallel Execution => runs complex and large data processing on separate computer cores (Async)
--- NUGET Package => System.Threading.Tasks.Dataflow => TPL DataFlow
---- .NET Framework 4.5
--- Opensource .NET Repository => dotnet/corefx GITHUB Repository
--- .NET Core own NUGET Package
-- AKKA.Net

## Advanced Hosted Service Concepts

- Handling Exceptions
-- All exceptions within the hosted services are swallowed and never caught or thrown within the hosted service.
--- Best result is to have try/catch/finally in Worker Service and perform exception logic in catch-section as necessary
- Triggering application shutdown
-- How lifetime is managed by the host
--- IHostApplicationLifetime interface
---- ApplicationStarted { get; }
---- ApplicationStopping { get; }
---- ApplicationStopped { get; }
---- void StopApplication();
-- How the shutdown process works
-- Triggering shutdown
-- Configuring shutdown options
- Ordering background services
-- Starting and Stopping Background Services
--- StartAsync() => Each service must start in an order
---- This is based off of services.AddHostedService Registration in the Startup.ConfigureServices method
--- StopAsync() => must end or shutdown in reverse order
---- This is based off of services.AddHostedService Registration in the Startup.ConfigureServices method
- Overriding StartAsync and StopAsync
-- Adding some logging
- Testing worker Services
- Configuring the Host
- AVOIDING BLOCKING CODE IN STARTASYNC METHOD
-- StartAsync blocks application startup
-- StartAsync should complete quickly
-- Avoid long-running work in StartAsync
-- ExecuteAsync also blocks startup until the first await
-- Awaiting an immediately completed Task is effectively synchronous

## Running Worker Services in Production

- Docker Primer
-- Container are good for deploying MicroServices
-- Containers are a popular choice for deploying microservices to production.
-- Worker services can easily be deployed in containers
--- Containers
---- Containers are lightweight Virtual Machines
---- Have Applications, Runtime, DOCKER
---- Docker is a popular container package
----- .NET Core
----- Docker Images
------ Include the dependencies needed to run an executable
------ Built from several immutable layers
------ Immutable provides consistency
------ New images can be based on existing ones
------ A unit of distribution and deployment
----- Docker Containers
------ Runnable instance of an image (does not contain the operating system)
------ Smaller footprint than virtual machines
------ Many containers can be run on a host
------ Run in isolation
----- Orchestrators
------ Kubenatics is an orchestration
------ Manage, schedule and deploy containers
------ Health monitoring
------ Scaling
------ Load balancing
------ Service discovery
- Running Worker Services as Containers
- Running Worker Services as Windows Services
-- Install NUGET Package => Microsoft.Extensions.Hosting and Microsoft.Extensions.Hosting.WindowsServices
-- UseWindowsService() Extensions Method in the MVC Pipeline
--- Configures the host to use a WindowsServiceLifetime
--- Sets the ContentRootPath to Appcontext.BaseDirectory
--- Publish as Windows Service
dotnet publish -r win-x64 -c Release -o c:\publish\win-service /p:PublishSingleFile=true /p:Debugtype=None
/contents
- appsettings.json
- windows-service.exe
--- Create the service (verify in services.msc and right-click and select START from the context menu option)
sc create ServiceName binPath=c:\publish\win-serv-ce\windows.service.exe start=delayed-auto
--- Enables logging to the Windows Event Log
- Running Worker Services as Azure App Services
