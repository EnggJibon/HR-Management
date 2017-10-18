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
    public class EmployeeEducationalQualificationController : Controller
    {
         private readonly IEmployeeEducationalQualificationService _employeeEducationalQualificationService;
         private readonly IEmployeeService _employeeService;

         public EmployeeEducationalQualificationController()
         {
             var karnel = GlobalConfiguration.Configuration.DependencyResolver as NinjectResolver;
             if (karnel != null)
             {
                 _employeeEducationalQualificationService = karnel.GetService(typeof(EmployeeEducationalQualificationService)) as EmployeeEducationalQualificationService;
                 _employeeService = karnel.GetService(typeof(EmployeeService)) as EmployeeService;
             }
         }

         public ActionResult Index(long? eid)
        {

            var employeeInfo = _employeeService.GetAllEmployees().FirstOrDefault(w => w.Id == eid);
            var employeeEducationalQualificationView = new EmployeeEducationalQualificationViewModel();
            if (employeeInfo != null)
            {
                employeeEducationalQualificationView.EmployeeEducationalQualification = new EmployeeEducationalQualificationModel
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
            return View(employeeEducationalQualificationView);
        }

        public ActionResult List([DataSourceRequest]DataSourceRequest request, long employeeId)
        {
            var employeeEducationalQualificationList = _employeeEducationalQualificationService.GetAll().Where(e=>e.EmployeeId==employeeId).ToList();
            return Json(employeeEducationalQualificationList.ToDataSourceResult(request));
        }

        public JsonResult Get(long id)
        {
            return new JsonResult { Data = _employeeEducationalQualificationService.GetById(id) };
        }


        [ValidateAntiForgeryToken]
        public JsonResult Save([Bind(Include = "Id,EmployeeId,Certification,Institute,Board,Marks,PassingYear,Duration,DivisionClassGPA,Subject,Country,OutOf,IsActive")] EmployeeEducationalQualificationModel employeeEducationalQualification, bool isInsert)
        {
            if (isInsert)
            {
                employeeEducationalQualification.SetCreateProperties(LoginInformation.UserInformation.Id);
                employeeEducationalQualification.Id = _employeeEducationalQualificationService.Insert(employeeEducationalQualification);
            }
            else
            {
                employeeEducationalQualification.SetUpdateProperties(LoginInformation.UserInformation.Id);
                _employeeEducationalQualificationService.Update(employeeEducationalQualification);
            }
            return new JsonResult { Data = employeeEducationalQualification };
        }

        public void Delete(long id)
        {
            _employeeEducationalQualificationService.DeleteSoftly(id);
        }
    }
}