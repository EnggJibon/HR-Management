using System.Web.Http;
using System.Web.Mvc;
using ERP.BLL.Security;
using ERP.Security.Domain;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Linq;

namespace ERP.Areas.SecurityAdministration.Controllers
{
    [SessionExpire]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;

        public RoleController()
        {
            var kernel = GlobalConfiguration.Configuration.DependencyResolver as NinjectResolver;
            if (kernel != null)
            {
                _roleService = kernel.GetService(typeof(RoleService)) as RoleService;
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List([DataSourceRequest]DataSourceRequest request)
        {
            var roleServiceList = _roleService.GetAll().Where(w => !w.IsDeleted).ToList();
            return Json(roleServiceList.ToDataSourceResult(request));
        }

        public JsonResult Save([Bind(Include = "Id, Name, Description, IsActive")] RoleModel role, bool isInsert)
        {
            if (isInsert)
            {
                role.SetCreateProperties(LoginInformation.UserInformation.Id);
                role.Id = _roleService.Insert(role);
            }
            else
            {
                role.SetUpdateProperties(LoginInformation.UserInformation.Id);
                _roleService.Update(role);
            }
            return new JsonResult { Data = role };
        }

        public JsonResult Get(long id)
        {
            return new JsonResult { Data = _roleService.GetById(id) };
        }

        public void Delete(long id)
        {
            _roleService.DeleteSoftly(id);
        }
    }
}