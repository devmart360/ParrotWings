using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;
using Devmart360.ParrotWings.Models;
using Devmart360.ParrotWings.Transactions.Dto;

namespace Devmart360.ParrotWings
{
    [DependsOn(typeof(ParrotWingsCoreModule), typeof(AbpAutoMapperModule))]
    public class ParrotWingsApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.AbpAutoMapper().Configurators.Add(mapper =>
            {
                //Add your custom AutoMapper mappings here...
                //mapper.CreateMap<,>()
                
                mapper.CreateMap<Transaction, TransactionInfoDto>()
                     .ForMember(desct => desct.Amount,
                        opt => opt.MapFrom(src => src.Type * src.Amount));
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
