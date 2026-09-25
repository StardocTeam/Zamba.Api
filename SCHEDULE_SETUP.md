# Zamba.Api - Schedule Management

## Overview

This document describes the Schedule management system that has been integrated into Zamba.API. The system allows creating, configuring, and monitoring recurring task executions.

## Database Schema

### Tables

#### ScheduleEvent
Represents a scheduled event/rule that executes according to a recurrence pattern.

**Columns:**
- `Id` (int, PK): Unique identifier
- `Name` (varchar(255), NOT NULL): Display name
- `Description` (varchar(1000)): Optional description
- `RuleId` (varchar(255), NOT NULL): Identifier of the rule to execute
- `EndpointOverride` (varchar(500)): Optional custom endpoint URL
- `IsActive` (bit, NOT NULL): Whether the event is active
- `CreatedAt` (datetime2, NOT NULL): Creation timestamp
- `UpdatedAt` (datetime2, NOT NULL): Last update timestamp
- `CreatedBy` (varchar(255)): Creator identifier
- `UpdatedBy` (varchar(255)): Last updater identifier

**Indexes:**
- PK: Id
- IX: RuleId, IsActive, CreatedAt

#### ScheduleConfig
Stores the recurrence configuration for a ScheduleEvent (1-to-1 relationship).

**Columns:**
- `Id` (int, PK): Unique identifier
- `ScheduleEventId` (int, FK): Foreign key to ScheduleEvent
- `RecurrenceType` (int, NOT NULL): Type of recurrence (0=Once, 1=Minutely, 2=Hourly, 3=Daily, 4=Weekly, 5=Monthly, 6=CronExpression)
- `IntervalValue` (int): For Minutely/Hourly recurrence
- `DaysOfWeek` (varchar(50)): For Weekly recurrence (e.g., "MON,WED,FRI")
- `DayOfMonth` (int): For Monthly recurrence (1-31)
- `TimeOfDay` (time): Time of day for execution
- `StartDate` (datetime2): When the schedule starts
- `EndDate` (datetime2): When the schedule ends (NULL = indefinite)
- `CronExpression` (varchar(255)): For CronExpression recurrence type
- `MisfireInstruction` (int): What to do if execution is missed (0=FireNow, 1=DoNothing, 2=FireOnceNow)
- `TimeZone` (varchar(100)): Time zone ID for schedule calculations

**Indexes:**
- PK: Id
- FK: ScheduleEventId

#### ScheduleExecutionLog
Audit log of all schedule executions.

**Columns:**
- `Id` (int, PK): Unique identifier
- `ScheduleEventId` (int, FK): Foreign key to ScheduleEvent
- `ScheduledAt` (datetime2, NOT NULL): When execution was scheduled
- `StartedAt` (datetime2, NOT NULL): When execution started
- `FinishedAt` (datetime2): When execution finished
- `Status` (int, NOT NULL): Execution status (0=Running, 1=Success, 2=Failed, 3=Skipped, 4=Timeout)
- `HttpStatusCode` (int): HTTP response status code
- `ResponseBody` (varchar(4000)): Truncated response body
- `ErrorMessage` (varchar(2000)): Error details if failed
- `DurationMs` (bigint): Execution duration in milliseconds
- `RetryCount` (int): Number of retries

**Indexes:**
- PK: Id
- IX: ScheduleEventId, ScheduledAt, Status

## EF Core Configuration

### Connection String

Update `appsettings.json` to use the appropriate connection string:

```json
{
  "ConnectionStrings": {
    "ZambaConnectionDEV": "Server=YOUR_SERVER;Database=YOUR_DATABASE;User Id=YOUR_USER;Password=YOUR_PASSWORD;TrustServerCertificate=True;"
  }
}
```

### Migrations

To apply migrations:

```bash
cd Zamba.Api
dotnet ef database update
```

To create new migrations:

```bash
dotnet ef migrations add MigrationName
```

## Switching Database Providers

### From SQL Server to Oracle

1. **Update Zamba.Api.csproj:** Replace `Microsoft.EntityFrameworkCore.SqlServer` with `Oracle.EntityFrameworkCore`

```xml
<PackageReference Include="Oracle.EntityFrameworkCore" Version="9.0.0" />
```

2. **Update Program.cs:** Replace `UseSqlServer()` with `UseOracle()`

```csharp
builder.Services.AddDbContext<ZambaDbContext>(options =>
    options.UseOracle(connectionString)
);
```

3. **Update ZambaDbContext.cs:** Adjust column types for Oracle compatibility:
   - `datetime2` → `timestamp` or use Oracle-specific types
   - `bigint` for DurationMs is compatible
   - `time` type may need adjustment to `interval day to second` or similar

4. **Regenerate Migrations:** Delete existing migrations and create new ones

```bash
dotnet ef migrations add InitialScheduleSchema
dotnet ef database update
```

## API Endpoints

All endpoints are under `/api/schedule-events` and `/api/schedule-logs`.

### Schedule Events

- `GET /api/schedule-events` - List events (paginated, filterable)
- `GET /api/schedule-events/{id}` - Get event details
- `POST /api/schedule-events` - Create new event
- `PUT /api/schedule-events/{id}` - Update event
- `PATCH /api/schedule-events/{id}/toggle` - Toggle active status
- `DELETE /api/schedule-events/{id}` - Delete event
- `GET /api/schedule-events/{id}/logs` - Get execution logs for event
- `GET /api/schedule-events/{id}/next-runs` - Calculate next scheduled executions

### Execution Logs

- `GET /api/schedule-logs` - Get all execution logs (paginated, filterable)
- `POST /api/schedule-logs` - Record execution (called by Zamba.Schedule worker)

## Authentication & Authorization

**Current Status:** Not implemented. All endpoints are currently anonymous.

**TODO (Future Implementation):**
- Add `[Authorize]` attributes to all endpoints
- Implement JWT or similar authentication
- Add role-based authorization (e.g., ScheduleAdmin, ScheduleViewer)

The infrastructure is prepared for easy addition of auth without major refactoring:
- Middleware placeholder exists in Program.cs (commented out)
- Controllers have TODO markers where [Authorize] should be added
- IScheduleMapper has CreatedBy/UpdatedBy set to "API" as placeholder for actual user

## Logging

Structured logging is configured using Serilog. Log configuration in `appsettings.json`:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

Logs include:
- All CRUD operations on schedule events
- All execution log creations
- Validation errors
- Database operation details

## Validation

All inputs are validated using FluentValidation:

- Schedule event name and rule ID are required and length-restricted
- Recurrence configuration is validated based on recurrence type
- Days of week format is enforced (e.g., "MON,WED,FRI")
- Time zones must be valid system time zone IDs
- Cron expressions are validated for structure

Validation errors return HTTP 400 with detailed error messages.

## Notes for Zamba.Schedule Integration

The `POST /api/schedule-logs` endpoint is specifically designed for the Zamba.Schedule worker to record execution results:

```json
{
  "scheduleEventId": 1,
  "scheduledAt": "2026-09-23T10:00:00Z",
  "startedAt": "2026-09-23T10:00:01Z",
  "finishedAt": "2026-09-23T10:00:05Z",
  "status": 1,
  "httpStatusCode": 200,
  "responseBody": "Execution successful",
  "errorMessage": null,
  "durationMs": 4000,
  "retryCount": 0
}
```

Status values:
- 0 = Running
- 1 = Success
- 2 = Failed
- 3 = Skipped
- 4 = Timeout
