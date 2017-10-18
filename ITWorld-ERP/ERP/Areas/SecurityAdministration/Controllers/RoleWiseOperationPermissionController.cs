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
    public class RoleWiseOperationPermissionController : Controller
    {
        private readonly IRoleWiseOperationPermissionService _roleWiseOperationPermissionService;
        private readonly IScreenOperationService _screenOperationService;
        private readonly IRoleService _roleService;

        public RoleWiseOperationPermissionController()
        {
            var kernel = GlobalConfiguration.Configuration.DependencyResolver as NinjectResolver;
            if (kernel != null)
            {
                _roleWiseOperationPermissionService = kernel.GetService(typeof(RoleWiseOperationPermissionService)) as RoleWiseOperationPermissionService;
                _screenOperationService = kernel.GetService(typeof(ScreenOperationService)) as ScreenOperationService;
                _roleService = kernel.GetService(typeof(RoleService)) as RoleService;
            }
        }

        public ActionResult Index()
        {
            var roleWiseOperationPermissionView = new RoleWiseOperationPermissionViewModel
            {
                ScreenOperationList = new SelectList(_screenOperationService.GetAll(), "Id", "Name"),
                RoleList = new SelectList(_roleService.GetAll(), "Id", "Name")
            };
            return View(roleWiseOperationPermissionView);
        }

        public ActionResult List([DataSourceRequest]DataSourceRequest request)
        {
            var roleWiseOperationPermissionServiceList = _roleWiseOperationPermissionService.GetRoleWiseOperationPermissionList(null, null, null);
            return Json(roleWiseOperationPermissionServiceList.ToDataSourceResult(request));
        }

        public JsonResult Save([Bind(Include = "Id, RoleId, ScreenOperationId, HaveAccess, IsActive")] RoleWiseOperationPermissionModel roleWiseOperationPermission, bool isInsert)
        {
            if (isInsert)
            {
                roleWiseOperationPermission.SetCreateProperties(LoginInformation.UserInformation.Id);
                roleWiseOperationPermission.Id = _roleWiseOperationPermissionService.Insert(roleWiseOperationPermission);
            }
            else
            {
                roleWiseOperationPermission.SetUpdateProperties(LoginInformation.UserInformation.Id);
                _roleWiseOperationPermissionService.Update(roleWiseOperationPermission);
            }
            return new JsonResult { Data = roleWiseOperationPermission };
        }

        public JsonResult Get(long id)
        {
            return new JsonResult { Data = _roleWiseOperationPermissionService.GetById(id) };
        }

        public void Delete(long id)
        {
            _roleWiseOperationPermissionService.DeleteSoftly(id);
        }
    }
}