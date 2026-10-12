using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Claims;
using Zamba.Api.Controllers;
using Zamba.Api.Data;
using Zamba.Api.Models;
using Zamba.Api.Services;

namespace Zamba.Api.Tests;

[TestClass]
public sealed class DashboardControllerTests
{
    [TestMethod]
    public async Task GetVacation_ReturnsVacationDocumentsFromService()
    {
        var expected = new[] { new VacationDocument { DocumentId = 17, I110 = "V-17" } };
        var controller = new DashboardController(new FakeDashboardCatalogService(expected, [], []));
        controller.ControllerContext = AuthenticatedContext();

        var response = await controller.getVacation(CancellationToken.None);

        Assert.IsNotNull(response.Result);
        var ok = response.Result as OkObjectResult;
        Assert.IsNotNull(ok);
        Assert.AreSame(expected, ok.Value);
    }

    [TestMethod]
    public async Task GetDepartment_ReturnsDepartmentsFromService()
    {
        var expected = new[] { new DashboardDepartment { Id = 3, Name = "Operations" } };
        var controller = new DashboardController(new FakeDashboardCatalogService([], [], expected));

        var response = await controller.getDepartment(CancellationToken.None);

        Assert.IsNotNull(response.Result);
        var ok = response.Result as OkObjectResult;
        Assert.IsNotNull(ok);
        Assert.AreSame(expected, ok.Value);
    }

    [TestMethod]
    public async Task GetVacation_RejectsAnonymousRequest()
    {
        var controller = new DashboardController(new FakeDashboardCatalogService([], [], []));

        var response = await controller.getVacation(CancellationToken.None);

        Assert.IsInstanceOfType(response.Result, typeof(UnauthorizedResult));
    }

    [TestMethod]
    public async Task GetRol_ReturnsRolesFromService()
    {
        var expected = new[] { new DashboardRole { RoleId = 4, RoleName = "Manager" } };
        var controller = new DashboardController(new FakeDashboardCatalogService([], expected, []));

        var response = await controller.getRol(CancellationToken.None);

        Assert.IsNotNull(response.Result);
        var ok = response.Result as OkObjectResult;
        Assert.IsNotNull(ok);
        Assert.AreSame(expected, ok.Value);
    }

    [TestMethod]
    public void ListMenuItem_MapsLegacyMenuProperties()
    {
        var mapped = DashboardController.ListMenuItem(
        [
            new DashboardSidebarMenuItem { Name = "Vacation", Icon = "calendar", Action = "/vacation" }
        ]);

        Assert.AreEqual(1, mapped.Count);
        Assert.AreEqual("Vacation", mapped[0].Text);
        Assert.AreEqual("calendar", mapped[0].Icon);
        Assert.AreEqual("/vacation", mapped[0].Link);
    }

    [TestMethod]
    public void ListMenuItem_NullInputReturnsEmptyList()
    {
        Assert.AreEqual(0, DashboardController.ListMenuItem(null).Count);
    }

    [TestMethod]
    public void DashboardEntities_MapToDiscoveredSqlServerSchemas()
    {
        var options = new DbContextOptionsBuilder<ZambaDbContext>()
            .UseSqlServer("Server=localhost;Database=unused;User Id=unused;Password=unused;TrustServerCertificate=True")
            .Options;
        using var dbContext = new ZambaDbContext(options);

        var vacation = dbContext.Model.FindEntityType(typeof(VacationDocument));
        var role = dbContext.Model.FindEntityType(typeof(DashboardRole));
        var user = dbContext.Model.FindEntityType(typeof(DashboardUser));
        var department = dbContext.Model.FindEntityType(typeof(DashboardDepartment));

        Assert.AreEqual("DOC_I110", vacation?.GetTableName());
        Assert.AreEqual("zambabpm_RRHH", vacation?.GetSchema());
        Assert.AreEqual("DOC_ID", vacation?.FindPrimaryKey()?.Properties.Single().GetColumnName());
        Assert.AreEqual("Rol", role?.GetTableName());
        Assert.AreEqual("zambabpm_RRHH", role?.GetSchema());
        Assert.AreEqual("DashboardUsers", user?.GetTableName());
        Assert.AreEqual("SLST_S152", department?.GetTableName());
        Assert.AreEqual("zambabpm_RRHH", department?.GetSchema());
    }

    [TestMethod]
    public void DashboardEntities_BuildWithOracleProviderAndDefaultSchema()
    {
        var options = new DbContextOptionsBuilder<ZambaDbContext>()
            .UseOracle("User Id=test;Password=test;Data Source=localhost:1521/XEPDB1")
            .Options;
        using var dbContext = new ZambaDbContext(options);

        var role = dbContext.Model.FindEntityType(typeof(DashboardRole));
        var scheduleEvent = dbContext.Model.FindEntityType(typeof(ScheduleEvent));
        var scheduleIndex = scheduleEvent?.GetIndexes()
            .Single(index => index.Properties.Any(property => property.Name == nameof(ScheduleEvent.RagDocumentTypeId)));

        Assert.IsNotNull(role);
        Assert.AreEqual("ROL", role.GetTableName());
        Assert.IsNull(role.GetSchema());
        Assert.IsTrue(scheduleIndex?.IsUnique ?? false);
    }

    private static ControllerContext AuthenticatedContext()
    {
        var context = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity([new Claim(ClaimTypes.NameIdentifier, "42")], "test"))
        };
        return new ControllerContext { HttpContext = context };
    }

    private sealed class FakeDashboardCatalogService(
        IReadOnlyList<VacationDocument> vacations,
        IReadOnlyList<DashboardRole> roles,
        IReadOnlyList<DashboardDepartment> departments) : IDashboardCatalogService
    {
        public Task<IReadOnlyList<DashboardDepartment>> GetDepartmentsAsync(CancellationToken cancellationToken) => Task.FromResult(departments);
        public Task<IReadOnlyList<VacationDocument>> GetVacationsAsync(CancellationToken cancellationToken) => Task.FromResult(vacations);
        public Task<IReadOnlyList<DashboardRole>> GetRolesAsync(CancellationToken cancellationToken) => Task.FromResult(roles);
    }
}
