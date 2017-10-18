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
    public class EmployeeFamilyMemberController : Controller
    {

        private readonly IEmployeeFamilyMemberService _employeeFamilyMemberService;
        private readonly IEmployeeService _employeeService;

        public ActionResult Index(long? eid)
        {
            {
                var employeeInfo = _employeeService.GetAllEmployees().FirstOrDefault(w => w.Id == eid);
                var employeeFamilyMemberView = new EmployeeFamilyMemberViewModel();
                if (employeeInfo != null)
                {
                    employeeFamilyMemberView.EmployeeFamilyMember = new EmployeeFamilyMemberModel()
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
                return View(employeeFamilyMemberView);
            }
        }

        public EmployeeFamilyMemberController()
        {
            var karnel = GlobalConfiguration.Configuration.DependencyResolver as NinjectResolver;
            if (karnel != null)
            {
                _employeeFamilyMemberService = karnel.GetService(typeof(EmployeeFamilyMemberService)) as EmployeeFamilyMemberService;
                _employeeService = karnel.GetService(typeof(EmployeeService)) as EmployeeService;
            }
        }

        public ActionResult List([DataSourceRequest]DataSourceRequest request, long employeeId)
        {
            var employeeFamilyMemberList = _employeeFamilyMemberService.GetAll().Where(e => e.EmployeeId == employeeId).ToList();
            return Json(employeeFamilyMemberList.ToDataSourceResult(request));
        }

        [ValidateAntiForgeryToken]
        public JsonResult Save([Bind(Include = "Id,EmployeeId,Relation,Name,Qualification,Occupation,Organization,Designation,JobLocation,NationalId,Phone,Mobile,Email,Passport,AddressId,IsActive")] EmployeeFamilyMemberModel employeeFamilyMember, bool isInsert)
        {
            if (isInsert)
            {
                employeeFamilyMember.SetCreateProperties(LoginInformation.UserInformation.Id);
                employeeFamilyMember.Id = _employeeFamilyMemberService.Insert(employeeFamilyMember);
            }
            else
            {
                employeeFamilyMember.SetUpdateProperties(LoginInformation.UserInformation.Id);
                _employeeFamilyMemberService.Update(employeeFamilyMember);
            }
            return new JsonResult { Data = employeeFamilyMember };
        }

        public JsonResult Get(long id)
        {
            return new JsonResult { Data = _employeeFamilyMemberService.GetById(id) };
        }

        public void Delete(long id)
        {
            _employeeFamilyMemberService.DeleteSoftly(id);
        }
    }
}