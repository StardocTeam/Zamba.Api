using Zamba.Api.Models;

namespace Zamba.Api.Services;

public interface IDashboardCatalogService
{
    Task<IReadOnlyList<DashboardDepartment>> GetDepartmentsAsync(CancellationToken cancellationToken);
    Task<IReadOnlyList<VacationDocument>> GetVacationsAsync(CancellationToken cancellationToken);
    Task<IReadOnlyList<DashboardRole>> GetRolesAsync(CancellationToken cancellationToken);
}
