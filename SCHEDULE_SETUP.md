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

## Schedule email notifications with Google Workspace SMTP relay

`Zamba.Api` can send schedule notifications through Google Workspace without SMTP username/password authentication. The relay trusts the API server's fixed public outbound IP, and the connection uses STARTTLS over port 587.

### Configure Google Workspace

1. Confirm that the API server has a static public outbound IP address. If it runs behind NAT or in a cloud, use the NAT gateway's outbound IP, not the server's private or inbound address.
2. Sign in to [Google Admin Console](https://admin.google.com/) with an administrator that has Gmail settings privileges.
3. Open **Apps > Google Workspace > Gmail > Routing**, then find **SMTP relay service** and choose **Configure** or **Add another rule**.
4. Give the rule a name, such as `Zamba API`.
5. Under **Allowed senders**, select **Only addresses in my domains**. Set `FromAddress` below to an address in the Workspace domain. If it is a mailbox user, **Only registered Apps users in my domains** is also suitable.
6. Under **Authentication**, select **Only accept mail from the specified IP addresses** and add the API server's static public outbound IP. Do not enable **Require SMTP Authentication** for this IP-based setup.
7. Under **Encryption**, select **Require TLS encryption**, then save the rule. Google notes that changes can take up to 24 hours to propagate.
8. Allow outbound TCP traffic from the API server to `smtp-relay.gmail.com` on port `587`.

### Configure Zamba.Api

Set the `ScheduleEmail` section in `appsettings.json` or override it with environment variables in production:

```json
"ScheduleEmail": {
  "Provider": "GoogleWorkspaceRelay",
  "Username": "",
  "Password": "",
  "FromAddress": "avisos@tu-dominio.com",
  "FromName": "Zamba Schedule",
  "Port": 587
}
```

`Username` and `Password` are intentionally blank for IP-based relay authentication. Use the actual Workspace sender address in place of `avisos@tu-dominio.com`. For environment-based configuration, use `ScheduleEmail__FromAddress`, `ScheduleEmail__Provider`, and the corresponding double-underscore setting names.

After saving the rule and configuration, trigger a schedule event that has email notification enabled and verify delivery. Ensure the domain's SPF, DKIM, and DMARC records are set up for Google Workspace.

## Schedule Slack notifications

Zamba.Api sends Slack notifications through SlackNet, a .NET SDK client for Slack's Web API `chat.postMessage` method. It authenticates with a bot token and posts to the configured channel ID.

1. In Slack, open [Your Apps](https://api.slack.com/apps) and create or select an app for the workspace.
2. Under **OAuth & Permissions > Bot Token Scopes**, add `chat:write` and save the change.
3. Install or reinstall the app in the workspace and copy its **Bot User OAuth Token** (`xoxb-...`). Keep it secret.
4. Invite the bot to the destination channel with `/invite @YourApp`.
5. Copy the channel ID from the channel details (it usually begins with `C`; private channels can begin with `G`).
6. Set `Slack:BotToken` and `Slack:ChannelId` in `appsettings.json`, or use `Slack__BotToken` and `Slack__ChannelId` environment variables in production.
7. Apply the pending EF migration, restart Zamba.Api, and enable Slack success/failure notifications on the desired schedule events.

```json
"Slack": {
  "BotToken": "xoxb-REPLACE-WITH-SECRET",
  "ChannelId": "C0123456789"
}
```

The message includes the event configuration and execution result. Large payloads are truncated to fit Slack's message size limit. Keep bot tokens out of source control and rotate/reinstall the app if a token is exposed. Slack may require `chat:write.public` if the bot must post to public channels it has not joined; inviting it to the target channel is preferable.

## Switching Database Providers

### From SQL Server to Oracle

The API selects its EF Core provider with `Database:Provider`. SQL Server remains the default. Set it to `Oracle` to use ODP.NET EF Core; configure the matching connection string separately:

```json
{
  "Database": {
    "Provider": "Oracle"
  },
  "ConnectionStrings": {
    "ZambaOracleConnectionDEV": "User Id=APP_USER;Password=REPLACE_ME;Data Source=localhost:1521/ORCLPDB1"
  }
}
```

Use environment variables or a secret store for credentials in deployed environments. The equivalent environment-variable keys are `Database__Provider` and `ConnectionStrings__ZambaOracleConnectionDEV`.

The Oracle model maps application-owned tables to the connected user's schema and uppercases unquoted identifiers. `Database:Provider` accepts only `SqlServer` or `Oracle`; the selected provider must have a non-empty matching connection string. Dashboard and entity-RAG SQL use provider-aware quoting, parameters, pagination, metadata queries, and Oracle named binding.

The checked-in EF migrations were generated for SQL Server. They are not run automatically and must not be applied to Oracle. Before enabling Oracle, create the API-owned tables and indexes using an Oracle-specific schema deployment reviewed for the target Oracle version. The legacy dashboard tables are an external dependency and are not created by these migrations.

The schedule execution endpoint accepts read-only `SELECT` statements but does not translate their SQL. Scheduled query text must therefore use syntax supported by the configured provider; keep separate query text per provider where SQL dialects differ. Do not delete the SQL Server migrations. Future Oracle schema changes need a separate Oracle migration set or reviewed Oracle DDL scripts.

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
