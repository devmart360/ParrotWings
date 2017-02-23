using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;

namespace Devmart360.ParrotWings.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : ParrotWingsControllerBase
    {
        public ActionResult Index()
        {
            return View("~/App/Main/views/layout/layout.cshtml"); //Layout of the angular application.
        }
	}
}