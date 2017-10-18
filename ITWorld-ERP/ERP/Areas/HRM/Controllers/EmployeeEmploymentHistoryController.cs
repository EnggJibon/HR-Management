using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using ERP.BLL.HRM;
using ERP.HRM.Domain;
using ERP.HRM.ViewModel;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ERP.BLL.Security;

namespace ERP.Areas.HRM.Controllers
{
    [SessionExpire]
    public class EmployeeEmploymentHistoryController : Controller
    {
        private readonly IEmployeeEmploymentHistoryService _employeeEmploymentHistoryService;
        private readonly IEmployeeService _employeeService;

        public EmployeeEmploymentHistoryController()
        {
            var karnel = GlobalConfiguration.Configuration.DependencyResolver as NinjectResolver;
            if (karnel != null)
            {
                _employeeEmploymentHistoryService = karnel.GetService(typeof(EmployeeEmploymentHistoryService)) as EmployeeEmploymentHistoryService;
                _employeeService = karnel.GetService(typeof(EmployeeService)) as EmployeeService;
            }
        }

        public ActionResult Index(long? eid)
        {
            var employeeInfo = _employeeService.GetAllEmployees().FirstOrDefault(w => w.Id == eid);
            var employeeEmploymentHistoryView = new EmployeeEmploymentHistoryViewModel();
            if (employeeInfo != null)
            {
                employeeEmploymentHistoryView.EmployeeEmploymentHistory = new EmployeeEmploymentHistoryModel
                {
                    Employee = new EmployeeModel
                    {
                        EmployeeCode = employeeInfo.EmployeeCode,
                        EmployeeName = employeeInfo.EmployeeName,
                        DepartmentName = employeeInfo.DepartmentName,
                        DesignationName = employeeInfo.DesignationName
                    }
                };
            }
            return View(employeeEmploymentHistoryView);
        }

        public ActionResult List([DataSourceRequest]DataSourceRequest request, long employeeId)
        {
            var employeeEmploymentHistoryList = _employeeEmploymentHistoryService.GetAll().Where(e => e.EmployeeId == employeeId).ToList();
            return Json(employeeEmploymentHistoryList.ToDataSourceResult(request));
        }

        public JsonResult Get(long id)
        {
            var employeeEmploymentHistory = _employeeEmploymentHistoryService.GetById(id);
            return new JsonResult { Data = employeeEmploymentHistory };
        }

        [ValidateAntiForgeryToken]
        public JsonResult Save([Bind(Include = "Id,EmployeeId,Organization,OrganizationType,Position,Responsibility,StartDate,EndDate,IsActive")] EmployeeEmploymentHistoryModel employeeEmploymentHistory, bool isInsert)
        {
            if (isInsert)
            {
                employeeEmploymentHistory.SetCreateProperties(LoginInformation.UserInformation.Id);
                employeeEmploymentHistory.Id = _employeeEmploymentHistoryService.Insert(employeeEmploymentHistory);
            }
            else
            {
                employeeEmploymentHistory.SetUpdateProperties(LoginInformation.UserInformation.Id);
                _employeeEmploymentHistoryService.Update(employeeEmploymentHistory);
            }
            return new JsonResult { Data = employeeEmploymentHistory };
        }

        public void Delete(long id)
        {
            _employeeEmploymentHistoryService.DeleteSoftly(id);
        }
    }
}