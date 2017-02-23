using Abp.Authorization.Roles;
using Devmart360.ParrotWings.Users;

namespace Devmart360.ParrotWings.Authorization.Roles
{
    public class Role : AbpRole<User>
    {
        //Can add application specific role properties here

        public Role()
        {

        }

        public Role(int? tenantId, string displayName)
            : base(tenantId, displayName)
        {

        }

        public Role(int? tenantId, string name, string displayName)
            : base(tenantId, name, displayName)
        {

        }
    }
}