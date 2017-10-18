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
    public class EmployeeNomineeInformationController : Controller
    {      
        private readonly IEmployeeNomineeInformationService _employeeNomineeInformationService;
        private readonly IEmployeeService _employeeService;

        public EmployeeNomineeInformationController()
        {
            var karnel = GlobalConfiguration.Configuration.DependencyResolver as NinjectResolver;
            if (karnel != null)
            {
                _employeeNomineeInformationService = karnel.GetService(typeof(EmployeeNomineeInformationService)) as EmployeeNomineeInformationService;
                _employeeService = karnel.GetService(typeof(EmployeeService)) as EmployeeService;
            }
        }

        public ActionResult Index(long? eid)
        {
            var employeeInfo = _employeeService.GetAllEmployees().FirstOrDefault(w => w.Id == eid);
            var employeeNomineeInformationView = new EmployeeNomineeInformationViewModel();
            if (employeeInfo != null)
            {
                employeeNomineeInformationView.EmployeeNomineeInformation = new EmployeeNomineeInformationModel()
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
            return View(employeeNomineeInformationView);
        }

        public ActionResult List([DataSourceRequest]DataSourceRequest request, long employeeId)
        {
            var employeeNomineeInformationList = _employeeNomineeInformationService.GetAll().Where(e => e.EmployeeId == employeeId).ToList();
            return Json(employeeNomineeInformationList.ToDataSourceResult(request));
        }


        [ValidateAntiForgeryToken]
        public JsonResult Save([Bind(Include = "Id,EmployeeId,Relation,Name,Occupation,Organization,NationalId,Phone,Mobile,Email,Passport,AddressId,BenefitName,Percentage,IsActive")] EmployeeNomineeInformationModel employeeNomineeInformation, bool isInsert)
        {
            if (isInsert)
            {
                employeeNomineeInformation.SetCreateProperties(LoginInformation.UserInformation.Id);
                employeeNomineeInformation.Id = _employeeNomineeInformationService.Insert(employeeNomineeInformation);
            }
            else
            {
                employeeNomineeInformation.SetUpdateProperties(LoginInformation.UserInformation.Id);
                _employeeNomineeInformationService.Update(employeeNomineeInformation);
            }
            return new JsonResult { Data = employeeNomineeInformation };
        }

        public JsonResult Get(long id)
        {
            var employeeNomineeInfo = _employeeNomineeInformationService.GetById(id);
            return new JsonResult { Data = employeeNomineeInfo };
        }

        public void Delete(long id)
        {
            _employeeNomineeInformationService.DeleteSoftly(id);
        }
    }
}