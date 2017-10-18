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
    public class AdditionalOperationPermissionController : Controller
    {
        private readonly IAdditionalOperationPermissionService _additionalOperationPermissionService;
        private readonly IScreenOperationService _screenOperationService;
        private readonly IUserInformationService _userInformationService;

        public AdditionalOperationPermissionController()
        {
            var kernel = GlobalConfiguration.Configuration.DependencyResolver as NinjectResolver;
            if (kernel != null)
            {
                _additionalOperationPermissionService = kernel.GetService(typeof(AdditionalOperationPermissionService)) as AdditionalOperationPermissionService;
                _screenOperationService = kernel.GetService(typeof(ScreenOperationService)) as ScreenOperationService;
                _userInformationService = kernel.GetService(typeof(UserInformationService)) as UserInformationService;
            }
        }

        public ActionResult Index()
        {
            var additionalOperationPermissionView = new AdditionalOperationPermissionViewModel
            {
                ScreenOperationList = new SelectList(_screenOperationService.GetAll(), "Id", "Name"),
                UserList = new SelectList(_userInformationService.GetAll(), "Id", "Username")
            };
            return View(additionalOperationPermissionView);
        }

        public ActionResult List([DataSourceRequest]DataSourceRequest request)
        {
            var additionalOperationPermissionServiceList = _additionalOperationPermissionService.GetAdditionalOperationPermissionList(null, null, null);
            return Json(additionalOperationPermissionServiceList.ToDataSourceResult(request));
        }

        public JsonResult Save([Bind(Include = "Id, UserId, ScreenOperationId, StartDate, EndDate, IsActive")] AdditionalOperationPermissionModel additionalOperationPermission, bool isInsert)
        {
            if (isInsert)
            {
                additionalOperationPermission.SetCreateProperties(LoginInformation.UserInformation.Id);
                additionalOperationPermission.Id = _additionalOperationPermissionService.Insert(additionalOperationPermission);
            }
            else
            {
                additionalOperationPermission.SetUpdateProperties(LoginInformation.UserInformation.Id);
                _additionalOperationPermissionService.Update(additionalOperationPermission);
            }
            return new JsonResult { Data = additionalOperationPermission };
        }

        public JsonResult Get(long id)
        {
            return new JsonResult { Data = _additionalOperationPermissionService.GetById(id) };
        }

        public void Delete(long id)
        {
            _additionalOperationPermissionService.DeleteSoftly(id);
        }
    }
}