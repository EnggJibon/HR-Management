using System.Web.Http;
using System.Web.Mvc;
using ERP.BLL.HRM;
using ERP.BLL.Security;
using ERP.HRM.Domain;
using ERP.HRM.ViewModel;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace ERP.Areas.HRM.Controllers
{
    [SessionExpire]
    public class AnnualHolidayController : Controller
    {

        private readonly IAnnualHolidayService _annualHolidayService;
        private readonly IAnnualHolidayCategoryService _annualHolidayCategoryService;
        public ActionResult Index()
        {
            var annualHolidayView = new AnnualHolidayViewModel()
            { 
            AnnualHolidayCategoryList =  new SelectList(_annualHolidayCategoryService.GetAll(),"Id","Title"),
            AnnualHolidayTypeList = new SelectList(_annualHolidayService.GetAnnualHolidayType(), "Value", "Text"),
            AnnualHolidayDayList = new SelectList(_annualHolidayService.GetDayList(), "Value", "Text")
            };

            return View(annualHolidayView);
        }

        public AnnualHolidayController()
        {
            var kernel = GlobalConfiguration.Configuration.DependencyResolver as NinjectResolver;
            if (kernel != null)
            {
                _annualHolidayService = kernel.GetService(typeof (AnnualHolidayService)) as AnnualHolidayService;
                _annualHolidayCategoryService =kernel.GetService(typeof (AnnualHolidayCategoryService)) as AnnualHolidayCategoryService;
            }
        }

        public ActionResult List([DataSourceRequest]DataSourceRequest request)
        {
            var annualHolidayList = _annualHolidayService.GetAll();
            return Json(annualHolidayList.ToDataSourceResult(request));
        }


        [ValidateAntiForgeryToken]
        public JsonResult Save([Bind(Include = "Id,AnnualHolidayCategoryId,Date,Day,Title,Type,Description,IsActive")] AnnualHolidayModel annualHoliday, bool isInsert)
        {
            if (isInsert)
            {
                annualHoliday.SetCreateProperties(LoginInformation.UserInformation.Id);
                annualHoliday.Id = _annualHolidayService.Insert(annualHoliday);
            }
            else
            {
                annualHoliday.SetUpdateProperties(LoginInformation.UserInformation.Id);
                _annualHolidayService.Update(annualHoliday);
            }
            return new JsonResult { Data = annualHoliday };
        }

        public JsonResult Get(long id)
        {
            return new JsonResult { Data = _annualHolidayService.GetById(id) };
        }

        public void Delete(long id)
        {
            _annualHolidayService.DeleteSoftly(id);
        }
    }
}