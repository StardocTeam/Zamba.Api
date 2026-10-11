using Microsoft.EntityFrameworkCore;
using Zamba.Api.Data;
using Zamba.Api.Models;

namespace Zamba.Api.Services;

public sealed class DashboardCatalogService(ZambaDbContext dbContext) : IDashboardCatalogService
{
    public async Task<IReadOnlyList<DashboardDepartment>> GetDepartmentsAsync(CancellationToken cancellationToken) =>
        await dbContext.DashboardDepartments.AsNoTracking().OrderBy(row => row.Id).ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<VacationDocument>> GetVacationsAsync(CancellationToken cancellationToken) =>
        await dbContext.VacationDocuments.AsNoTracking().OrderByDescending(row => row.Date).ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<DashboardRole>> GetRolesAsync(CancellationToken cancellationToken) =>
        await dbContext.DashboardRoles.AsNoTracking().OrderBy(row => row.RoleId).ToListAsync(cancellationToken);
}
