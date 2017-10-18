using System;
using System.Web.Http;
using System.Web.Mvc;
using ERP.BLL.Security;
using ERP.DAL.Security;
using ERP.Security.Domain;
using ERP.Security.ViewModel;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace ERP.Areas.SecurityAdministration.Controllers
{
    [SessionExpire]
    public class RoleWiseScreenPermissionController : Controller
    {
        private readonly IRoleWiseScreenPermissionService _roleWiseScreenPermissionService;
        private readonly IScreenService _screenService;
        private readonly IRoleService _roleService;
        private readonly IModuleService _moduleService;

        public RoleWiseScreenPermissionController()
        {
            var kernel = GlobalConfiguration.Configuration.DependencyResolver as NinjectResolver;
            if (kernel != null)
            {
                _roleWiseScreenPermissionService = kernel.GetService(typeof(RoleWiseScreenPermissionService)) as RoleWiseScreenPermissionService;
                _screenService = kernel.GetService(typeof(ScreenService)) as ScreenService;
                _roleService = kernel.GetService(typeof(RoleService)) as RoleService;
                _moduleService = kernel.GetService(typeof(ModuleService)) as ModuleService;
            }
        }

        public ActionResult Index()
        {
            var roleWiseScreenPermissionView = new RoleWiseScreenPermissionViewModel
            {
                ScreenList = new SelectList(_screenService.GetAll(), "Id", "Title"),
                RoleList = new SelectList(_roleService.GetAll(), "Id", "Name"),
                ModuleList = new SelectList(_moduleService.GetAll(), "Id", "Name"),
            };

            return View(roleWiseScreenPermissionView);
        }

        public ActionResult List([DataSourceRequest]DataSourceRequest request)
        {
            var roleWiseScreenPermissionServiceList = _roleWiseScreenPermissionService.GetRoleWiseScreenPermissionList(null, null, null, null);
            return Json(roleWiseScreenPermissionServiceList.ToDataSourceResult(request));
        }

        public JsonResult Save([Bind(Include = "Id, RoleId, ScreenId, AccessRight, IsActive")] RoleWiseScreenPermissionModel roleWiseScreenPermission, bool isInsert)
        {
            if (isInsert)
            {
                roleWiseScreenPermission.SetCreateProperties(LoginInformation.UserInformation.Id);
                roleWiseScreenPermission.Id = _roleWiseScreenPermissionService.Insert(roleWiseScreenPermission);
            }
            else
            {
                roleWiseScreenPermission.SetUpdateProperties(LoginInformation.UserInformation.Id);
                _roleWiseScreenPermissionService.Update(roleWiseScreenPermission);
            }
            return new JsonResult { Data = roleWiseScreenPermission };
        }

        public JsonResult Get(long id)
        {
            return new JsonResult { Data = _roleWiseScreenPermissionService.GetRoleWiseScreenPermissionDetails(id) };
        }

        public JsonResult GetScreenList(long moduleId)
        {
            if (moduleId > 0)
            {
                var screenList = new SelectList(_screenService.GetScreenList(null, moduleId), "Id", "ScreenTitle");
                return new JsonResult { Data = screenList };
            }
            return new JsonResult { Data = null };
        }

        public void Delete(long id)
        {
            _roleWiseScreenPermissionService.DeleteSoftly(id);
        }
    }
}