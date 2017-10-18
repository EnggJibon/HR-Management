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
    public class EmployeeLeaveInformationController : Controller
    {
        private readonly IEmployeeLeaveInformationService _employeeLeaveInformationService;
        private readonly IAnnualHolidayCategoryService _annualHolidayCategoryService;
        private readonly IWeekendCategoryService _weekendCategoryService;
        private readonly ILeavePolicyService _leavePolicyService;
        private readonly IEmployeeService _employeeService;

        public EmployeeLeaveInformationController()
        {
            var kernel = GlobalConfiguration.Configuration.DependencyResolver as NinjectResolver;

            if (kernel != null)
            {
                _employeeLeaveInformationService = kernel.GetService(typeof(EmployeeLeaveInformationService)) as EmployeeLeaveInformationService;
                _annualHolidayCategoryService = kernel.GetService(typeof(IAnnualHolidayCategoryService)) as AnnualHolidayCategoryService;
                _weekendCategoryService = kernel.GetService(typeof(IWeekendCategoryService)) as WeekendCategoryService;
                _leavePolicyService = kernel.GetService(typeof(ILeavePolicyService)) as LeavePolicyService;
                _employeeService = kernel.GetService(typeof(IEmployeeService)) as EmployeeService;
            }
        }

        public ActionResult Index(long? eid)
        {
            var employeeLeaveInfoView = new EmployeeLeaveInformationViewModel
            {
                AnnualHolidayCategoryList = new SelectList(_annualHolidayCategoryService.GetAll(), "Id", "Title"),
                WeekendCategoryList = new SelectList(_weekendCategoryService.GetAll(), "Id", "Name"),
                LeavePolicyList = new SelectList(_leavePolicyService.GetAll(), "Id", "PolicyName"),
            };
            return View(employeeLeaveInfoView);
        }


        /*Get LIST*/
        public ActionResult List([DataSourceRequest]DataSourceRequest request)
        {
            var employeeLeaveInformationList = _employeeLeaveInformationService.GetAll();
            return Json(employeeLeaveInformationList.ToDataSourceResult(request));
        }

        public JsonResult GetByEmployeeCode(string employeeCode)
        {
            var employeeInfo = _employeeService.GetEmployeeInformation(null, employeeCode);

            if (employeeInfo != null)
            {
                var employeeLeaveInformation = _employeeLeaveInformationService.GetLeaveInformation(employeeInfo.Id);
                if (employeeLeaveInformation != null)
                {
                    employeeLeaveInformation.Employee = employeeInfo;
                    return new JsonResult { Data = employeeLeaveInformation };
                }
                return new JsonResult { Data = new EmployeeLeaveInformationModel { Employee = employeeInfo } };
            }
            return new JsonResult { Data = null };
        }

        public JsonResult GetByEmployeeId(long id)
        {
            if (id <= 0)
            {
                id = LoginInformation.UserInformation.EmployeeId;
            }

            var employeeInfo = _employeeService.GetEmployeeInformation(id, null);

            if (employeeInfo != null)
            {
                var employeeLeaveInformation = _employeeLeaveInformationService.GetLeaveInformation(id);
                if (employeeLeaveInformation != null)
                {
                    employeeLeaveInformation.Employee = employeeInfo;
                    return new JsonResult {Data = employeeLeaveInformation};
                }
                return new JsonResult {Data = new EmployeeLeaveInformationModel {Employee = employeeInfo}};
            }
            return new JsonResult {Data = null};
        }

        public ActionResult LeaveBalance([DataSourceRequest]DataSourceRequest request, long employeeId)
        {
            var leaveBalanceInformation = _employeeLeaveInformationService.GetEmployeeLeaveBalanceInformation(employeeId);

            return Json(leaveBalanceInformation.ToDataSourceResult(request));
        }


        [ValidateAntiForgeryToken]
        public JsonResult Save([Bind(Include = "Id,EmployeeId,AnnualHolidayCategoryId,WeekendCategoryId,LeavePolicyId,IsActive")] EmployeeLeaveInformationModel employeeLeaveInformation, bool isInsert)
        {
            

            if (isInsert)
            {
                employeeLeaveInformation.SetCreateProperties(LoginInformation.UserInformation.Id);
                employeeLeaveInformation.Id = _employeeLeaveInformationService.Insert(employeeLeaveInformation);
            }
            else
            {
                employeeLeaveInformation.SetUpdateProperties(LoginInformation.UserInformation.Id);
                _employeeLeaveInformationService.Update(employeeLeaveInformation);
            }
            return new JsonResult { Data = employeeLeaveInformation };
        }

        public void Delete(long id)
        {
            _employeeLeaveInformationService.DeleteSoftly(id);
        }
    }
}