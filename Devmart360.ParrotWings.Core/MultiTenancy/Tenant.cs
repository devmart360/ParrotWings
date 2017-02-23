using Abp.MultiTenancy;
using Devmart360.ParrotWings.Users;

namespace Devmart360.ParrotWings.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public Tenant()
        {
            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}