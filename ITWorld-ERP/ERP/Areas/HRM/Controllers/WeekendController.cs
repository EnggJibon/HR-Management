using System.Web.Http;
using System.Web.Mvc;
using ERP.BLL.HRM;
using ERP.HRM.Domain;
using ERP.HRM.ViewModel;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Linq;
using ERP.BLL.Security;

namespace ERP.Areas.HRM.Controllers
{
    [SessionExpire]
    public class WeekendController : Controller
    {
        private readonly IWeekendService _weekendService;
        private readonly IWeekendCategoryService _weekendCategoryService;

        public WeekendController()
        {
            var kernel = GlobalConfiguration.Configuration.DependencyResolver as NinjectResolver;
            if (kernel != null)
            {
                _weekendService = kernel.GetService(typeof(WeekendService)) as WeekendService;
                _weekendCategoryService = kernel.GetService(typeof(WeekendCategoryService)) as WeekendCategoryService;
            }
        }

        public ActionResult Index()
        {
            var weekendView = new WeekendViewModel()
            {
                WeekendHolidayDayList = new SelectList(_weekendService.GetDayList(), "Value", "Text"),
                WeekendCategoryList = new SelectList(_weekendCategoryService.GetAll(),"Id","Name")

            };
            return View(weekendView);
        }

        public ActionResult List([DataSourceRequest]DataSourceRequest request)
        {
            var weekendList = _weekendService.GetAll().Where(w => !w.IsDeleted).ToList();
            return Json(weekendList.ToDataSourceResult(request));
        }

        [ValidateAntiForgeryToken]
        public JsonResult Save([Bind(Include = "Id,WeekendCategoryId,Day,IsHalfDay,IsActive")] WeekendModel weekend, bool isInsert)
        {
            if (isInsert)
            {
                weekend.SetCreateProperties(LoginInformation.UserInformation.Id);
                weekend.Id = _weekendService.Insert(weekend);
            }
            else
            {
                weekend.SetUpdateProperties(LoginInformation.UserInformation.Id);
                _weekendService.Update(weekend);
            }
            return new JsonResult { Data = weekend };
        }

        public JsonResult Get(long id)
        {
            return new JsonResult { Data = _weekendService.GetById(id) };
        }

        public void Delete(long id)
        {
            _weekendService.DeleteSoftly(id);
        }
    }
}