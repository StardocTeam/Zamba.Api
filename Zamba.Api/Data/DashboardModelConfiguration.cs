using Microsoft.EntityFrameworkCore;
using Zamba.Api.Models;

namespace Zamba.Api.Data;

internal static class DashboardModelConfiguration
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DashboardUser>(e =>
        {
            e.ToTable("DashboardUsers", "zambabpm_RRHH"); e.HasKey(x => x.EnterpriseUserId);
            e.Property(x => x.EnterpriseUserId).HasColumnName("Enterpriseuser_id");
            e.Property(x => x.CompanyName).HasColumnName("companyname"); e.Property(x => x.FirstName).HasColumnName("firstname");
            e.Property(x => x.LastName).HasColumnName("lastname"); e.Property(x => x.PhoneNumber).HasColumnName("phonenumber");
            e.Property(x => x.Email).HasColumnName("email"); e.Property(x => x.Password).HasColumnName("password");
            e.Property(x => x.DepartmentId).HasColumnName("department_id"); e.Property(x => x.RoleId).HasColumnName("rol_id");
            e.Property(x => x.IsActive).HasColumnName("isActive"); e.Property(x => x.Username).HasColumnName("username");
            e.Property(x => x.UserId).HasColumnName("userid");
        });
        modelBuilder.Entity<DashboardResetPasswordToken>(e =>
        {
            e.ToTable("resetpasswordtokens", "zambabpm_RRHH"); e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id"); e.Property(x => x.Email).HasColumnName("email").HasMaxLength(100);
            e.Property(x => x.TokenData).HasColumnName("tokendata").HasMaxLength(50);
            e.Property(x => x.ExpirationDate).HasColumnName("expirationdate"); e.Property(x => x.Used).HasColumnName("used");
        });
        modelBuilder.Entity<DashboardPendingVerificationUser>(e =>
        {
            e.ToTable("pendingverificationusers", "zambabpm_RRHH"); e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("id"); e.Property(x => x.UserId).HasColumnName("userid");
            e.Property(x => x.Token).HasColumnName("token");
        });
        modelBuilder.Entity<DashboardReportCategory>(e =>
        {
            e.ToTable("ReportCategories", "zambabpm_RRHH"); e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("Id"); e.Property(x => x.Category).HasColumnName("Category").HasMaxLength(255);
        });
        modelBuilder.Entity<DashboardReportDescription>(e =>
        {
            e.ToTable("reportDescription", "zambabpm_RRHH"); e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("Id"); e.Property(x => x.ReportId).HasColumnName("ReportId");
            e.Property(x => x.Description).HasColumnName("Description").HasMaxLength(255);
        });
        modelBuilder.Entity<DashboardReport>(e =>
        {
            e.ToTable("Reporte_General", "dbo"); e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("ID").HasColumnType("numeric(18,0)");
            e.Property(x => x.Query).HasColumnName("Query").HasMaxLength(4000);
            e.Property(x => x.Name).HasColumnName("Name").HasMaxLength(100);
            e.Property(x => x.Completar).HasColumnName("Completar").HasMaxLength(300);
            e.Property(x => x.GroupExpression).HasColumnName("GroupExpression").HasMaxLength(200);
        });
        modelBuilder.Entity<DashboardSidebarMenuItem>(e =>
        {
            e.ToTable("RH_SidebarMenuItem", "zambabpm_RRHH"); e.HasKey(x => x.MenuItemId);
            e.Property(x => x.MenuItemId).HasColumnName("menuitem_id"); e.Property(x => x.Name).HasColumnName("name");
            e.Property(x => x.Title).HasColumnName("title"); e.Property(x => x.Description).HasColumnName("description");
            e.Property(x => x.Action).HasColumnName("action"); e.Property(x => x.Parameters).HasColumnName("parameters");
            e.Property(x => x.Position).HasColumnName("position"); e.Property(x => x.Icon).HasColumnName("icon");
        });
        modelBuilder.Entity<DashboardCalendarEvent>(e =>
        {
            e.ToTable("CalendarEvents", "zambabpm_RRHH"); e.HasKey(x => x.CalendarEventId);
            e.Property(x => x.CalendarEventId).HasColumnName("calendareventid").ValueGeneratedOnAdd();
            e.Property(x => x.EventData).HasColumnName("eventdata"); e.Property(x => x.GroupId).HasColumnName("groupid");
            e.Property(x => x.UserId).HasColumnName("userid");
        });
        modelBuilder.Entity<DashboardWidgetContainer>(e =>
        {
            e.ToTable("WidgetsContainer", "zambabpm_RRHH"); e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("Id").ValueGeneratedOnAdd(); e.Property(x => x.UserId).HasColumnName("UserId");
            e.Property(x => x.Options).HasColumnName("Options").HasMaxLength(500);
            e.Property(x => x.WidgetCoordinates).HasColumnName("WidgetCoordinates").HasMaxLength(1000);
        });
        modelBuilder.Entity<DashboardCarouselContent>(e =>
        {
            e.ToTable("CarouselContent", "zambabpm_RRHH"); e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("Id"); e.Property(x => x.ContentPath).HasColumnName("ContentPath").HasMaxLength(500);
            e.Property(x => x.SourceContent).HasColumnName("SourceContent").HasMaxLength(50); e.Property(x => x.UserId).HasColumnName("UserId");
        });
        modelBuilder.Entity<DashboardCarouselConfig>(e =>
        {
            e.ToTable("CarouselConfig", "zambabpm_RRHH"); e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("Id"); e.Property(x => x.UserId).HasColumnName("UserId");
            e.Property(x => x.DotPosition).HasColumnName("DotPosition").HasMaxLength(20);
            e.Property(x => x.AutoPlaySpeed).HasColumnName("AutoPlaySpeed"); e.Property(x => x.AutoPlay).HasColumnName("AutoPlay");
            e.Property(x => x.EnableSwipe).HasColumnName("EnableSwipe"); e.Property(x => x.Loop).HasColumnName("Loop");
        });
        modelBuilder.Entity<DashboardVideoPlayerSource>(e =>
        {
            e.ToTable("VideoplayerURLSources", "zambabpm_RRHH"); e.HasKey(x => x.VideoPlayerSourceId);
            e.Property(x => x.VideoPlayerSourceId).HasColumnName("videoplayersourceid");
            e.Property(x => x.UserId).HasColumnName("userid"); e.Property(x => x.YouTubeVideoId).HasColumnName("youtubevideoid");
        });
        modelBuilder.Entity<DashboardRole>(e =>
        {
            e.ToTable("Rol", "zambabpm_RRHH"); e.HasKey(x => x.RoleId);
            e.Property(x => x.RoleId).HasColumnName("rol_id"); e.Property(x => x.RoleName).HasColumnName("rolname");
        });
        modelBuilder.Entity<DashboardUserConfiguration>(e =>
        {
            e.ToTable("DashboardUserConfig", "zambabpm_RRHH"); e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("dashboarduserconfig_id"); e.Property(x => x.UserId).HasColumnName("userid");
            e.Property(x => x.ValueConfig).HasColumnName("valueconfig"); e.Property(x => x.Value).HasColumnName("value");
            e.Property(x => x.GroupId).HasColumnName("group_id").HasMaxLength(50);
        });
        modelBuilder.Entity<DashboardUserRight>(e =>
        {
            e.ToTable("USR_RIGHTS", "dbo"); e.HasKey(x => new { x.GroupId, x.ObjectId, x.RightType, x.AdditionalId });
            e.Property(x => x.GroupId).HasColumnName("GROUPID").HasColumnType("numeric(18,0)");
            e.Property(x => x.ObjectId).HasColumnName("OBJID").HasColumnType("numeric(18,0)");
            e.Property(x => x.RightType).HasColumnName("RTYPE").HasColumnType("numeric(18,0)");
            e.Property(x => x.AdditionalId).HasColumnName("ADITIONAL").HasColumnType("numeric(18,0)");
        });
        modelBuilder.Entity<DashboardUserGroup>(e =>
        {
            e.ToTable("USR_R_GROUP", "dbo"); e.HasKey(x => new { x.UserId, x.GroupId });
            e.Property(x => x.UserId).HasColumnName("USRID").HasColumnType("numeric(18,0)");
            e.Property(x => x.GroupId).HasColumnName("GROUPID").HasColumnType("numeric(18,0)");
        });
        modelBuilder.Entity<DashboardGroupInheritance>(e =>
        {
            e.ToTable("group_r_group", "dbo"); e.HasKey(x => new { x.UserGroup, x.InheritedUserGroup });
            e.Property(x => x.UserGroup).HasColumnName("UserGroup").HasColumnType("numeric(18,0)");
            e.Property(x => x.InheritedUserGroup).HasColumnName("inheritedUserGroup").HasColumnType("numeric(18,0)");
        });
        modelBuilder.Entity<DashboardWorkflow>(e =>
        {
            e.ToTable("WFWorkflow", "dbo"); e.HasKey(x => x.WorkflowId);
            e.Property(x => x.WorkflowId).HasColumnName("work_id").HasColumnType("numeric(18,0)");
            e.Property(x => x.Name).HasColumnName("Name");
        });
        modelBuilder.Entity<DashboardWorkflowStep>(e =>
        {
            e.ToTable("WFStep", "dbo"); e.HasKey(x => x.StepId);
            e.Property(x => x.StepId).HasColumnName("step_Id").HasColumnType("numeric(18,0)");
            e.Property(x => x.WorkflowId).HasColumnName("work_id").HasColumnType("numeric(18,0)");
            e.Property(x => x.Name).HasColumnName("Name").HasMaxLength(50);
        });
        modelBuilder.Entity<DashboardDocumentType>(e =>
        {
            e.ToTable("DOC_TYPE", "dbo"); e.HasKey(x => x.DocumentTypeId);
            e.Property(x => x.DocumentTypeId).HasColumnName("DOC_TYPE_ID").HasColumnType("numeric(18,0)");
            e.Property(x => x.DocumentTypeName).HasColumnName("DOC_TYPE_NAME").HasMaxLength(60).IsFixedLength();
        });
        modelBuilder.Entity<DashboardWorkflowDocument>(e =>
        {
            e.ToTable("WFDocument", "dbo"); e.HasKey(x => x.TaskId);
            e.Property(x => x.TaskId).HasColumnName("Task_ID").HasColumnType("numeric(18,0)");
            e.Property(x => x.DocumentId).HasColumnName("Doc_ID").HasColumnType("numeric(18,0)");
            e.Property(x => x.DocumentTypeId).HasColumnName("DOC_TYPE_ID").HasColumnType("numeric(18,0)");
            e.Property(x => x.StepId).HasColumnName("step_Id").HasColumnType("numeric(18,0)");
            e.Property(x => x.WorkflowId).HasColumnName("work_id").HasColumnType("numeric(18,0)");
            e.Property(x => x.AssignedUserId).HasColumnName("User_Asigned").HasColumnType("numeric(18,0)");
            e.Property(x => x.Name).HasColumnName("Name").HasMaxLength(250); e.Property(x => x.CheckIn).HasColumnName("CheckIn");
            e.Property(x => x.ExpireDate).HasColumnName("ExpireDate"); e.Property(x => x.DateAssignedBy).HasColumnName("Date_Asigned_By");
            e.Property(x => x.LastUpdateDate).HasColumnName("LastUpdateDate");
            e.Property(x => x.StateId).HasColumnName("Do_State_ID").HasColumnType("numeric(18,0)");
            e.Property(x => x.TaskStateId).HasColumnName("Task_State_ID").HasColumnType("numeric(18,0)");
            e.Property(x => x.AssignedByUserId).HasColumnName("User_Asigned_By").HasColumnType("numeric(18,0)");
            e.Property(x => x.Comments).HasColumnName("Comments").HasMaxLength(1000); e.Property(x => x.Tag).HasColumnName("Tag").HasMaxLength(500);
        });
    }
}
