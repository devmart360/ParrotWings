using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using Devmart360.ParrotWings.Editions;
using Devmart360.ParrotWings.Users;

namespace Devmart360.ParrotWings.MultiTenancy
{
    public class TenantManager : AbpTenantManager<Tenant, User>
    {
        public TenantManager(
            IRepository<Tenant> tenantRepository, 
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository, 
            EditionManager editionManager,
            IAbpZeroFeatureValueStore featureValueStore
            ) 
            : base(
                tenantRepository, 
                tenantFeatureRepository, 
                editionManager,
                featureValueStore
            )
        {
        }
    }
}