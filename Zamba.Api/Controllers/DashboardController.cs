using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Zamba.Api.Models;
using Zamba.Api.Services;

namespace Zamba.Api.Controllers;

[ApiController]
[Route("api/Dashboard")]
public sealed class DashboardController(IDashboardCatalogService dashboardCatalogService) : ControllerBase
{
    [HttpGet("getDepartment")]
    [HttpPost("getDepartment")]
    public async Task<ActionResult<IReadOnlyList<DashboardDepartment>>> getDepartment(CancellationToken cancellationToken) =>
        Ok(await dashboardCatalogService.GetDepartmentsAsync(cancellationToken));

    [HttpGet("getVacation")]
    [HttpPost("getVacation")]
    public async Task<ActionResult<IReadOnlyList<VacationDocument>>> getVacation(CancellationToken cancellationToken)
    {
        if (ControllerContext?.HttpContext?.User?.Identity?.IsAuthenticated != true)
        {
            return Unauthorized();
        }

        return Ok(await dashboardCatalogService.GetVacationsAsync(cancellationToken));
    }

    [HttpGet("getRol")]
    [HttpPost("getRol")]
    public async Task<ActionResult<IReadOnlyList<DashboardRole>>> getRol(CancellationToken cancellationToken) =>
        Ok(await dashboardCatalogService.GetRolesAsync(cancellationToken));

    public static IReadOnlyList<DashboardMenuItem> ListMenuItem(IEnumerable<DashboardSidebarMenuItem>? rows) =>
        rows?.Select(row => new DashboardMenuItem
        {
            Text = row.Name,
            Icon = row.Icon,
            Link = row.Action
        }).ToArray() ?? Array.Empty<DashboardMenuItem>();
}

public sealed class DashboardMenuItem
{
    [JsonPropertyName("text")]
    public string? Text { get; init; }

    [JsonPropertyName("icon")]
    public string? Icon { get; init; }

    [JsonPropertyName("link")]
    public string? Link { get; init; }
}
