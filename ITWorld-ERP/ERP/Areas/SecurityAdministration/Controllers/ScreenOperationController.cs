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
    public class ScreenOperationController : Controller
    {
        private readonly IScreenOperationService _screenOperationService;
        private readonly IScreenService _screenService;

        public ScreenOperationController()
        {
            var kernel = GlobalConfiguration.Configuration.DependencyResolver as NinjectResolver;
            if (kernel != null)
            {
                _screenOperationService = kernel.GetService(typeof(ScreenOperationService)) as ScreenOperationService;
                _screenService = kernel.GetService(typeof(ScreenService)) as ScreenService;
            }
        }

        public ActionResult Index()
        {
            var screenOperationView = new ScreenOperationViewModel
            {
                ScreenList = new SelectList(_screenService.GetAll(), "Id", "Title"),
            };

            return View(screenOperationView);
        }

        public ActionResult List([DataSourceRequest]DataSourceRequest request)
        {
            var screenOperationServiceList = _screenOperationService.GetScreenOperationDetailsList(null, null);
            return Json(screenOperationServiceList.ToDataSourceResult(request));
        }

        public JsonResult Save([Bind(Include = "Id, Name, Description, ScreenId, IsActive")] ScreenOperationModel screenOperation, bool isInsert)
        {
            if (isInsert)
            {
                screenOperation.SetCreateProperties(LoginInformation.UserInformation.Id);
                screenOperation.Id = _screenOperationService.Insert(screenOperation);
            }
            else
            {
                screenOperation.SetUpdateProperties(LoginInformation.UserInformation.Id);
                _screenOperationService.Update(screenOperation);
            }
            return new JsonResult { Data = screenOperation };
        }

        public JsonResult Get(long id)
        {
            return new JsonResult { Data = _screenOperationService.GetById(id) };
        }

        public void Delete(long id)
        {
            _screenOperationService.DeleteSoftly(id);
        }
    }
}