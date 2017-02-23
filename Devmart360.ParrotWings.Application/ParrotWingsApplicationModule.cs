using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;

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
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
