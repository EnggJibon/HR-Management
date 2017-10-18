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
    public class EmployeeReferenceInformationController : Controller
    {
        private readonly IEmployeeReferenceInformationService _employeeReferenceInformationService;
        private readonly IEmployeeService _employeeService;

        public EmployeeReferenceInformationController()
        {
            var karnel = GlobalConfiguration.Configuration.DependencyResolver as NinjectResolver;

            if (karnel !=null)
            {
                _employeeReferenceInformationService = karnel.GetService(typeof(EmployeeReferenceInformationService)) as EmployeeReferenceInformationService;
                _employeeService = karnel.GetService(typeof(EmployeeService)) as EmployeeService;
            }
        }

        public ActionResult Index(long? eid)
        {
            var employeeInfo = _employeeService.GetAllEmployees().FirstOrDefault(w => w.Id == eid);
            var employeeReferenceInformationView = new EmployeeReferenceInformationViewModel();
            if (employeeInfo != null)
            {
                employeeReferenceInformationView.EmployeeReferenceInformation = new EmployeeReferenceInformationModel()
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
            return View(employeeReferenceInformationView);
        }

        public ActionResult List([DataSourceRequest]DataSourceRequest request, long employeeId)
        {
            var employeeReferenceInformationList = _employeeReferenceInformationService.GetAll().Where(e => e.EmployeeId == employeeId).ToList();
            return Json(employeeReferenceInformationList.ToDataSourceResult(request));
        }

        [ValidateAntiForgeryToken]
        public JsonResult Save([Bind(Include = "Id,EmployeeId,Occupation,Organization,Designation,Mobile,Email,IsActive")] EmployeeReferenceInformationModel employeeReferenceInformation, bool isInsert)
        {
            if (isInsert)
            {
                employeeReferenceInformation.SetCreateProperties(LoginInformation.UserInformation.Id);
                employeeReferenceInformation.Id = _employeeReferenceInformationService.Insert(employeeReferenceInformation);
            }
            else
            {
                employeeReferenceInformation.SetUpdateProperties(LoginInformation.UserInformation.Id);
                _employeeReferenceInformationService.Update(employeeReferenceInformation);
            }
            return new JsonResult { Data = employeeReferenceInformation };
        }

        public JsonResult Get(long id)
        {
            var employeeReferenceInfor = _employeeReferenceInformationService.GetById(id);
            return new JsonResult { Data = employeeReferenceInfor };  
        }

        public void Delete(long id)
        {
            _employeeReferenceInformationService.DeleteSoftly(id);
        }
    }
}