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
    public class AnnualHolidayCategoryController : Controller
    {
        private readonly IAnnualHolidayCategoryService _annualholidayCategoryService;

        public AnnualHolidayCategoryController()
        {
            var kernel = GlobalConfiguration.Configuration.DependencyResolver as NinjectResolver;
            if (kernel != null)
            {
                _annualholidayCategoryService = kernel.GetService(typeof(AnnualHolidayCategoryService)) as AnnualHolidayCategoryService;
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List([DataSourceRequest]DataSourceRequest request)
        {
            var annualHolidayCategoryList = _annualholidayCategoryService.GetAll();
            return Json(annualHolidayCategoryList.ToDataSourceResult(request));
        }


        [ValidateAntiForgeryToken]
        public JsonResult Save([Bind(Include = "Id,Title,Year,Description,IsActive")] AnnualHolidayCategoryModel annualholidayCategory, bool isInsert)
        {
            if (isInsert)
            {
                annualholidayCategory.SetCreateProperties(LoginInformation.UserInformation.Id);
                annualholidayCategory.Id = _annualholidayCategoryService.Insert(annualholidayCategory);
            }
            else
            {
                annualholidayCategory.SetUpdateProperties(LoginInformation.UserInformation.Id);
                _annualholidayCategoryService.Update(annualholidayCategory);
            }
            return new JsonResult { Data = annualholidayCategory };
        }

        public JsonResult Get(long id)
        {
            return new JsonResult { Data = _annualholidayCategoryService.GetById(id) };
        }

        public void Delete(long id)
        {
            _annualholidayCategoryService.DeleteSoftly(id);
        }
    }
}