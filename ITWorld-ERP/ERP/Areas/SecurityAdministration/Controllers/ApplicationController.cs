using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using ERP.BLL.Security;
using ERP.Security.Domain;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace ERP.Areas.SecurityAdministration.Controllers
{
    [SessionExpire]
    public class ApplicationController : Controller
    {
        private readonly IApplicationService _applicationService;

        public ApplicationController()
        {
            var kernel = GlobalConfiguration.Configuration.DependencyResolver as NinjectResolver;
            if (kernel != null)
            {
                _applicationService = kernel.GetService(typeof(ApplicationService)) as ApplicationService;
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List([DataSourceRequest]DataSourceRequest request)
        {
            var applicationServiceList = _applicationService.GetAll().Where(w => !w.IsDeleted).ToList();
            return Json(applicationServiceList.ToDataSourceResult(request));
        }

        public JsonResult Save([Bind(Include = "Id,Name,Description,IsActive")] ApplicationModel application, bool isInsert)
        {
            if (isInsert)
            {
                application.SetCreateProperties(LoginInformation.UserInformation.Id);
                application.Id = _applicationService.Insert(application);
            }
            else
            {
                application.SetUpdateProperties(LoginInformation.UserInformation.Id);
                _applicationService.Update(application);
            }
            return new JsonResult { Data = application };
        }

        public JsonResult Get(long id)
        {
            return new JsonResult { Data = _applicationService.GetById(id) };
        }

        public void Delete(long id)
        {
            _applicationService.DeleteSoftly(id);
        }
    }
}