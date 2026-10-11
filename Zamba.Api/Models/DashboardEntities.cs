namespace Zamba.Api.Models;

public sealed class DashboardUser
{
    public int EnterpriseUserId { get; set; }
    public string? CompanyName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public int DepartmentId { get; set; }
    public int RoleId { get; set; }
    public bool IsActive { get; set; }
    public string? Username { get; set; }
    public int? UserId { get; set; }
}

public sealed class DashboardResetPasswordToken
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string TokenData { get; set; } = string.Empty;
    public DateTime ExpirationDate { get; set; }
    public bool Used { get; set; }
}

public sealed class DashboardPendingVerificationUser
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Token { get; set; } = string.Empty;
}

public sealed class DashboardReportCategory
{
    public int Id { get; set; }
    public string Category { get; set; } = string.Empty;
}

public sealed class DashboardReportDescription
{
    public int Id { get; set; }
    public int ReportId { get; set; }
    public string? Description { get; set; }
}

public sealed class DashboardReport
{
    public decimal Id { get; set; }
    public string? Query { get; set; }
    public string? Name { get; set; }
    public string? Completar { get; set; }
    public string? GroupExpression { get; set; }
}

public sealed class DashboardSidebarMenuItem
{
    public int MenuItemId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Action { get; set; }
    public string? Parameters { get; set; }
    public int Position { get; set; }
    public string? Icon { get; set; }
}

public sealed class DashboardCalendarEvent
{
    public int CalendarEventId { get; set; }
    public string? EventData { get; set; }
    public int? GroupId { get; set; }
    public int? UserId { get; set; }
}

public sealed class DashboardWidgetContainer
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Options { get; set; } = string.Empty;
    public string? WidgetCoordinates { get; set; }
}

public sealed class DashboardCarouselContent
{
    public int Id { get; set; }
    public string ContentPath { get; set; } = string.Empty;
    public string SourceContent { get; set; } = string.Empty;
    public int UserId { get; set; }
}

public sealed class DashboardCarouselConfig
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string? DotPosition { get; set; }
    public int? AutoPlaySpeed { get; set; }
    public int? AutoPlay { get; set; }
    public int? EnableSwipe { get; set; }
    public int? Loop { get; set; }
}

public sealed class DashboardVideoPlayerSource
{
    public int VideoPlayerSourceId { get; set; }
    public int? UserId { get; set; }
    public string? YouTubeVideoId { get; set; }
}

public sealed class DashboardRole
{
    public int RoleId { get; set; }
    public string RoleName { get; set; } = string.Empty;
}

public sealed class DashboardUserConfiguration
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string ValueConfig { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public string? GroupId { get; set; }
}

public sealed class DashboardUserRight
{
    public decimal GroupId { get; set; }
    public decimal ObjectId { get; set; }
    public decimal RightType { get; set; }
    public decimal AdditionalId { get; set; }
}

public sealed class DashboardUserGroup
{
    public decimal UserId { get; set; }
    public decimal GroupId { get; set; }
}

public sealed class DashboardGroupInheritance
{
    public decimal UserGroup { get; set; }
    public decimal InheritedUserGroup { get; set; }
}

public sealed class DashboardWorkflow
{
    public decimal WorkflowId { get; set; }
    public string Name { get; set; } = string.Empty;
}

public sealed class DashboardWorkflowStep
{
    public decimal StepId { get; set; }
    public decimal WorkflowId { get; set; }
    public string Name { get; set; } = string.Empty;
}

public sealed class DashboardDocumentType
{
    public decimal DocumentTypeId { get; set; }
    public string? DocumentTypeName { get; set; }
}

public sealed class DashboardWorkflowDocument
{
    public decimal TaskId { get; set; }
    public decimal DocumentId { get; set; }
    public decimal DocumentTypeId { get; set; }
    public decimal StepId { get; set; }
    public decimal WorkflowId { get; set; }
    public decimal? AssignedUserId { get; set; }
    public string? Name { get; set; }
    public DateTime? CheckIn { get; set; }
    public DateTime? ExpireDate { get; set; }
    public DateTime? DateAssignedBy { get; set; }
    public DateTime? LastUpdateDate { get; set; }
    public decimal? StateId { get; set; }
    public decimal? TaskStateId { get; set; }
    public decimal? AssignedByUserId { get; set; }
    public string? Comments { get; set; }
    public string? Tag { get; set; }
}
