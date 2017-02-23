using System.Threading.Tasks;
using Abp.Application.Services;
using Devmart360.ParrotWings.Roles.Dto;

namespace Devmart360.ParrotWings.Roles
{
    public interface IRoleAppService : IApplicationService
    {
        Task UpdateRolePermissions(UpdateRolePermissionsInput input);
    }
}
