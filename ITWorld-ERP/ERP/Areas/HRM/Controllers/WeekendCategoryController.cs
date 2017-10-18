using System.Web.Http;
using System.Web.Mvc;
using ERP.BLL.HRM;
using ERP.HRM.Domain;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Linq;
using ERP.BLL.Security;

namespace ERP.Areas.HRM.Controllers
{
    [SessionExpire]
    public class WeekendCategoryController : Controller
    {
        private readonly IWeekendCategoryService _weekendCategoryService;

        public WeekendCategoryController()
        {
            var kernel = GlobalConfiguration.Configuration.DependencyResolver as NinjectResolver;
            if (kernel != null)
            {
                _weekendCategoryService = kernel.GetService(typeof(WeekendCategoryService)) as WeekendCategoryService;
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List([DataSourceRequest]DataSourceRequest request)
        {
            var weekendCategoryList = _weekendCategoryService.GetAll().Where(w => !w.IsDeleted).ToList();
            return Json(weekendCategoryList.ToDataSourceResult(request));
        }


        [ValidateAntiForgeryToken]
        public JsonResult Save([Bind(Include = "Id,Name,Description,IsActive")] WeekendCategoryModel weekendCategory, bool isInsert)
        {
            if (isInsert)
            {
                weekendCategory.SetCreateProperties(LoginInformation.UserInformation.Id);
                weekendCategory.Id = _weekendCategoryService.Insert(weekendCategory);
            }
            else
            {
                weekendCategory.SetUpdateProperties(LoginInformation.UserInformation.Id);
                _weekendCategoryService.Update(weekendCategory);
            }
            return new JsonResult { Data = weekendCategory };
        }

        public JsonResult Get(long id)
        {
            return new JsonResult { Data = _weekendCategoryService.GetById(id) };
        }

        public void Delete(long id)
        {
            _weekendCategoryService.DeleteSoftly(id);
        }
    }
}