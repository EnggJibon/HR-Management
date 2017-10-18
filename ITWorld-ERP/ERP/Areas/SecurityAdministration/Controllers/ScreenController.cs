using System.Web.Http;
using System.Web.Mvc;
using ERP.BLL.Security;
using ERP.Security.Domain;
using ERP.Security.ViewModel;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace ERP.Areas.SecurityAdministration.Controllers
{
    [SessionExpire]
    public class ScreenController : Controller
    {
        private readonly IScreenService _screenService;
        private readonly IModuleService _moduleService;

        public ScreenController()
        {
            var kernel = GlobalConfiguration.Configuration.DependencyResolver as NinjectResolver;
            if (kernel != null)
            {
                _screenService = kernel.GetService(typeof(ScreenService)) as ScreenService;
                _moduleService = kernel.GetService(typeof(ModuleService)) as ModuleService;
            }
        }

        public ActionResult Index()
        {
            var screenView = new ScreenViewModel
            {
               ModuleList = new SelectList(_moduleService.GetAll(), "Id", "Name"),
               ScreenTypeList = new SelectList(_screenService.GetScreenTypeList(), "Value", "Text"),

            };
            return View(screenView);
        }

        public ActionResult List([DataSourceRequest]DataSourceRequest request)
        {
            var screenServiceList = _screenService.GetScreenList(null, null);
            return Json(screenServiceList.ToDataSourceResult(request));
        }

        public JsonResult Save([Bind(Include = "Id, ScreenCode, ModuleId, Title, Type, URL, IsActive")] ScreenModel screen, bool isInsert)
        {
            if (isInsert)
            {
                screen.SetCreateProperties(LoginInformation.UserInformation.Id);
                screen.Id = _screenService.Insert(screen);
            }
            else
            {
                screen.SetUpdateProperties(LoginInformation.UserInformation.Id);
                _screenService.Update(screen);
            }
            return new JsonResult { Data = screen };
        }

        public JsonResult Get(long id)
        {
            return new JsonResult { Data = _screenService.GetById(id) };
        }

        public JsonResult GetScreenList(string moduleId)
        {
           // var screenList = new SelectList(_screenService.GetScreenList(null, null, Convert.ToInt64(moduleId), null), "ScreenId", "ScreenTitle");
            return new JsonResult { Data = "" };
        }

        public void Delete(long id)
        {
            _screenService.DeleteSoftly(id);
        }
	}
}