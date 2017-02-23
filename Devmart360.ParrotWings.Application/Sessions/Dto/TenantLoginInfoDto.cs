using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Devmart360.ParrotWings.MultiTenancy;

namespace Devmart360.ParrotWings.Sessions.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}