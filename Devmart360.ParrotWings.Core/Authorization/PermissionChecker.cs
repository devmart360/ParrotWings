using Abp.Authorization;
using Devmart360.ParrotWings.Authorization.Roles;
using Devmart360.ParrotWings.MultiTenancy;
using Devmart360.ParrotWings.Users;

namespace Devmart360.ParrotWings.Authorization
{
    public class PermissionChecker : PermissionChecker<Tenant, Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
