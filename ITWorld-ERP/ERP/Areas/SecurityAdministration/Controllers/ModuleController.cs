using System.Web.Http;
using System.Web.Mvc;
using ERP.BLL.Security;
using ERP.Security.Domain;
using ERP.Security.ViewModel;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Linq;

namespace ERP.Areas.SecurityAdministration.Controllers
{
    [SessionExpire]
    public class ModuleController : Controller
    {
        private readonly IModuleService _moduleService;
        private readonly IApplicationService _applicationService;

        public ModuleController()
        {
            var kernel = GlobalConfiguration.Configuration.DependencyResolver as NinjectResolver;
            if (kernel != null)
            {
                _moduleService = kernel.GetService(typeof(ModuleService)) as ModuleService;
                _applicationService = kernel.GetService(typeof(ApplicationService)) as ApplicationService;
            }
        }

        public ActionResult Index()
        {
            var moduleView = new ModuleViewModel
            {
                ApplicationList = new SelectList(_applicationService.GetAll(), "Id", "Name"),
            };
            return View(moduleView);
        }

        public ActionResult List([DataSourceRequest]DataSourceRequest request)
        {
            var modules = _moduleService.GetAll().Where(w => !w.IsDeleted).ToList();
            return Json(modules.ToDataSourceResult(request));
        }

        public JsonResult Save([Bind(Include = "Id, Name, Description, ApplicationId, IsActive")] ModuleModel module, bool isInsert)
        {
            if (isInsert)
            {
                module.SetCreateProperties(LoginInformation.UserInformation.Id);
                module.Id = _moduleService.Insert(module);
            }
            else
            {
                module.SetUpdateProperties(LoginInformation.UserInformation.Id);
                _moduleService.Update(module);
            }
            return new JsonResult { Data = module };
        }

        public JsonResult Get(long id)
        {
            return new JsonResult { Data = _moduleService.GetById(id) };
        }

        public void Delete(long id)
        {
            _moduleService.DeleteSoftly(id);
        }
    }
}