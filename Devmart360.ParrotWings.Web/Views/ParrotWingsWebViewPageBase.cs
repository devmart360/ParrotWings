using Abp.Web.Mvc.Views;

namespace Devmart360.ParrotWings.Web.Views
{
    public abstract class ParrotWingsWebViewPageBase : ParrotWingsWebViewPageBase<dynamic>
    {

    }

    public abstract class ParrotWingsWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected ParrotWingsWebViewPageBase()
        {
            LocalizationSourceName = ParrotWingsConsts.LocalizationSourceName;
        }
    }
}