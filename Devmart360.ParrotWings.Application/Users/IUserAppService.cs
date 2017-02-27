using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Devmart360.ParrotWings.Users.Dto;

namespace Devmart360.ParrotWings.Users
{
    public interface IUserAppService : IApplicationService
    {
        Task ProhibitPermission(ProhibitPermissionInput input);

        Task RemoveFromRole(long userId, string roleName);

        Task<ListResultDto<UserListDto>> GetUsers();

        Task CreateUser(CreateUserInput input);

        // ---

        ListResultDto<UserListDto> Find(UserFilterInputDto termUserFilterInputDto);
    }
}