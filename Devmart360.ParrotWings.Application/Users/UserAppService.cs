using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Events.Bus;
using Abp.Extensions;
using Abp.Notifications;
using Devmart360.ParrotWings.Authorization;
using Devmart360.ParrotWings.EventModels;
using Devmart360.ParrotWings.Users.Dto;
using Microsoft.AspNet.Identity;

namespace Devmart360.ParrotWings.Users
{
    /* THIS IS JUST A SAMPLE. */
    [AbpAuthorize(PermissionNames.Pages_Users)]
    //[AbpAuthorize("Users")]
    public class UserAppService : ParrotWingsAppServiceBase, IUserAppService
    {
        private readonly IRepository<User, long> _userRepository;
        private readonly IPermissionManager _permissionManager;
        private readonly INotificationSubscriptionManager _notificationSubscriptionManager;
        private readonly UserManager _userManager;

        public IEventBus EventBus { get; set; }

        public UserAppService(IRepository<User, long> userRepository, IPermissionManager permissionManager, INotificationSubscriptionManager notificationSubscriptionManager, UserManager userManager)
        {
            _userRepository = userRepository;
            _permissionManager = permissionManager;
            _notificationSubscriptionManager = notificationSubscriptionManager;
            _userManager = userManager;
        }

        public async Task ProhibitPermission(ProhibitPermissionInput input)
        {
            var user = await UserManager.GetUserByIdAsync(input.UserId);
            var permission = _permissionManager.GetPermission(input.PermissionName);

            await UserManager.ProhibitPermissionAsync(user, permission);
        }

        //Example for primitive method parameters.
        public async Task RemoveFromRole(long userId, string roleName)
        {
            CheckErrors(await UserManager.RemoveFromRoleAsync(userId, roleName));
        }

        public async Task<ListResultDto<UserListDto>> GetUsers()
        {
            var users = await _userRepository.GetAllListAsync();

            return new ListResultDto<UserListDto>(
                users.MapTo<List<UserListDto>>()
                );
        }

        public async Task CreateUser(CreateUserInput input)
        {
            var user = input.MapTo<User>();

            user.TenantId = AbpSession.TenantId;
            user.Password = new PasswordHasher().HashPassword(input.Password);
            user.IsEmailConfirmed = true;

            // todo: надо добавлять новые разрешения в AuthorizationProvider
            //// по умолчанию всегда добавляется разрешение Users
            //var p = _permissionManager.GetPermission("Users");
            //await _userManager.GrantPermissionAsync(user, p);

            var result = await UserManager.CreateAsync(user);
            CheckErrors(result);

            if (result.Succeeded)
            {
                await CurrentUnitOfWork.SaveChangesAsync();

                // подписка на изменение баланса
                await _notificationSubscriptionManager.SubscribeAsync(new UserIdentifier(null, user.Id), "BalanceChanged");
                // добавление начального баланса
                EventBus.Trigger(new UserCreatedEventData { UserId = user.Id });
            }
        }

        // ---

        public ListResultDto<UserListDto> Find(UserFilterInputDto termUserFilterInputDto)
        {
            // note: добавлен атрибут [AbpAuthorize("Users")] к этому сервису, чтобы пользователи с правами Users имели доступ к этому методу

            var currentUserId = AbpSession.UserId;

            var users = UserManager.Users.WhereIf(!termUserFilterInputDto.SearchTerm.IsNullOrWhiteSpace(),
                u => u.Name.Contains(termUserFilterInputDto.SearchTerm) ||
                     u.Surname.Contains(termUserFilterInputDto.SearchTerm) ||
                     u.UserName.Contains(termUserFilterInputDto.SearchTerm) ||
                     u.EmailAddress.Contains(termUserFilterInputDto.SearchTerm))
                     .Where(u => u.Id != currentUserId);

            return new ListResultDto<UserListDto>(
                users.MapTo<List<UserListDto>>()
                );
        }
    }
}