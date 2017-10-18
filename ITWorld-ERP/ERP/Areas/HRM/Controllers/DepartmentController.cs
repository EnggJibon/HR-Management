using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using ERP.BLL.HRM;
using ERP.BLL.Security;
using ERP.HRM.Domain;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace ERP.Areas.HRM.Controllers
{
    [SessionExpire]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController()
        {
            var kernel = GlobalConfiguration.Configuration.DependencyResolver as NinjectResolver;
            if (kernel != null)
            {
                _departmentService = kernel.GetService(typeof(DepartmentService)) as DepartmentService;
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List([DataSourceRequest]DataSourceRequest request)
        {
            var departmentList = _departmentService.GetAll().Where(w => !w.IsDeleted).ToList();
            return Json(departmentList.ToDataSourceResult(request));
        }

        [ValidateAntiForgeryToken]
        public JsonResult Save([Bind(Include = "Id,Name,Description,IsActive")] DepartmentModel department, bool isInsert)
        {
            if (isInsert)
            {
                department.SetCreateProperties(LoginInformation.UserInformation.Id);
                department.Id = _departmentService.Insert(department);
            }
            else
            {
                department.SetUpdateProperties(LoginInformation.UserInformation.Id);
                _departmentService.Update(department);
            }
            return new JsonResult { Data = department };
        }

        public JsonResult Get(long id)
        {
            return new JsonResult { Data = _departmentService.GetById(id) };
        }

        public void Delete(long id)
        {
            _departmentService.DeleteSoftly(id);
        }
    }
}