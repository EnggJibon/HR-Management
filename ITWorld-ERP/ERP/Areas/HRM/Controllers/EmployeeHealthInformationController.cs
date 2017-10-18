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
    public class EmployeeHealthInformationController : Controller
    {
        private readonly IEmployeeHealthInformationService _employeeHealthInformationService;
        private readonly IEmployeeService _employeeService;

        public EmployeeHealthInformationController()
        {
            var karnel = GlobalConfiguration.Configuration.DependencyResolver as NinjectResolver;
            if (karnel != null)
            {
                _employeeHealthInformationService = karnel.GetService(typeof(EmployeeHealthInformationService)) as EmployeeHealthInformationService;
                _employeeService = karnel.GetService(typeof(EmployeeService)) as EmployeeService;
            }
        }

        public ActionResult Index(long? eid)
        {
            var employeeInfo = _employeeService.GetAllEmployees().FirstOrDefault(w => w.Id == eid);
            var employeeHealthInformationView = new EmployeeHealthInformationViewModel();
            if (employeeInfo != null)
            {
                employeeHealthInformationView.EmployeeHealthInformation = new EmployeeHealthInformationModel()
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
            return View(employeeHealthInformationView);  
        }

        public ActionResult List([DataSourceRequest]DataSourceRequest request, long employeeId)
        {
            var employeeHealthInformationList = _employeeHealthInformationService.GetAll().Where(e => e.EmployeeId == employeeId).ToList();
            return Json(employeeHealthInformationList.ToDataSourceResult(request));
        }

        [ValidateAntiForgeryToken]
        public JsonResult Save([Bind(Include = "Id,EmployeeId,Age,Height,Weight,BloodGroup,Disease,DiseaseNote,DiseaseStatus,IdentifiedHealthSymbol,IsActive")] EmployeeHealthInformationModel employeeHealthInformation, bool isInsert)
        {
            if (isInsert)
            {
                employeeHealthInformation.SetCreateProperties(LoginInformation.UserInformation.Id);
                employeeHealthInformation.Id = _employeeHealthInformationService.Insert(employeeHealthInformation);
            }
            else
            {
                employeeHealthInformation.SetUpdateProperties(LoginInformation.UserInformation.Id);
                _employeeHealthInformationService.Update(employeeHealthInformation);
            }
            return new JsonResult { Data = employeeHealthInformation };
        }

        public JsonResult Get(long id)
        {
            var employeeHealthInfo = _employeeHealthInformationService.GetById(id);
            return new JsonResult { Data = employeeHealthInfo };
        }
    }
}