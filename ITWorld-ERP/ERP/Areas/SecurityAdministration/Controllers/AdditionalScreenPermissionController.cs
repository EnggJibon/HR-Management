using System;
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
    public class AdditionalScreenPermissionController : Controller
    {
        private readonly IAdditionalScreenPermissionService _additionalScreenPermissionService;
        private readonly IRoleWiseScreenPermissionService _roleWiseScreenPermissionService;
        private readonly IScreenService _screenService;
        private readonly IUserInformationService _userInformationService;
        private readonly IModuleService _moduleService;

        public AdditionalScreenPermissionController()
        {
            var kernel = GlobalConfiguration.Configuration.DependencyResolver as NinjectResolver;
            if (kernel != null)
            {
                _additionalScreenPermissionService = kernel.GetService(typeof(AdditionalScreenPermissionService)) as AdditionalScreenPermissionService;
                _roleWiseScreenPermissionService = kernel.GetService(typeof(RoleWiseScreenPermissionService)) as RoleWiseScreenPermissionService;
                _screenService = kernel.GetService(typeof(ScreenService)) as ScreenService;
                _userInformationService = kernel.GetService(typeof(UserInformationService)) as UserInformationService;
                _moduleService = kernel.GetService(typeof(ModuleService)) as ModuleService;
            }
        }

        public ActionResult Index()
        {
            var additionalScreenPermissionView = new AdditionalScreenPermissionViewModel
            {
                ScreenList = new SelectList(_screenService.GetAll(), "Id", "Title"),
                UserList = new SelectList(_userInformationService.GetAll(), "Id", "Username"),
                ModuleList = new SelectList(_moduleService.GetAll(), "Id", "Name"),
            };
            return View(additionalScreenPermissionView);
        }

        public ActionResult List([DataSourceRequest]DataSourceRequest request)
        {
            var additionalScreenPermissionServiceList = _additionalScreenPermissionService.GetAdditionalScreenPermissionList(null, null, null, null);
            return Json(additionalScreenPermissionServiceList.ToDataSourceResult(request));
        }

        public JsonResult Save([Bind(Include = "Id, UserId, ScreenId, StartDate, EndDate, AccessRight, IsActive")] AdditionalScreenPermissionModel additionalScreenPermission, bool isInsert)
        {
            if (isInsert)
            {
                additionalScreenPermission.SetCreateProperties(LoginInformation.UserInformation.Id);
                additionalScreenPermission.Id = _additionalScreenPermissionService.Insert(additionalScreenPermission);
            }
            else
            {
                additionalScreenPermission.SetUpdateProperties(LoginInformation.UserInformation.Id);
                _additionalScreenPermissionService.Update(additionalScreenPermission);
            }
            return new JsonResult { Data = additionalScreenPermission };
        }

        public JsonResult Get(long id)
        {
            return new JsonResult { Data = _additionalScreenPermissionService.GetAdditionalScreenPermissionDetails(id) };
        }

        public JsonResult GetScreenList(string moduleId)
        {
            var screenList = new SelectList(_roleWiseScreenPermissionService.GetRoleWiseScreenPermissionList(null, null, Convert.ToInt64(moduleId), null), "ScreenId", "ScreenTitle");
            return new JsonResult { Data = screenList };
        }

        public void Delete(long id)
        {
            _additionalScreenPermissionService.DeleteSoftly(id);
        }
    }
}