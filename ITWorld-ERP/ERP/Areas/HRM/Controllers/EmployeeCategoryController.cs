using System.Web.Http;
using System.Web.Mvc;
using ERP.BLL.HRM;
using ERP.HRM.Domain;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ERP.BLL.Security;

namespace ERP.Areas.HRM.Controllers
{
    [SessionExpire]
    public class EmployeeCategoryController : Controller
    {
        private readonly IEmployeeCategoryService _employeeCategoryService;

        public EmployeeCategoryController()
        {
            var kernel = GlobalConfiguration.Configuration.DependencyResolver as NinjectResolver;
            if (kernel != null)
            {
                _employeeCategoryService = kernel.GetService(typeof(EmployeeCategoryService)) as EmployeeCategoryService;
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List([DataSourceRequest]DataSourceRequest request)
        {
            var employeeCategoryList = _employeeCategoryService.GetAll();
            return Json(employeeCategoryList.ToDataSourceResult(request));
        }


        [ValidateAntiForgeryToken]
        public JsonResult Save([Bind(Include = "Id,Name,Description,IsActive")] EmployeeCategoryModel employeeCategory, bool isInsert)
        {
            if (isInsert)
            {
                employeeCategory.SetCreateProperties(LoginInformation.UserInformation.Id);
                employeeCategory.Id = _employeeCategoryService.Insert(employeeCategory);
            }
            else
            {
                employeeCategory.SetUpdateProperties(LoginInformation.UserInformation.Id);
                _employeeCategoryService.Update(employeeCategory);
            }
            return new JsonResult { Data = employeeCategory };
        }

        public JsonResult Get(long id)
        {
            return new JsonResult { Data = _employeeCategoryService.GetById(id) };
        }

        public void Delete(long id)
        {
            _employeeCategoryService.DeleteSoftly(id);
        }
    }
}