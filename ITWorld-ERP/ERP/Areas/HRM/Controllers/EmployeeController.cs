using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using ERP.BLL.Common;
using ERP.BLL.HRM;
using ERP.BLL.Security;
using ERP.HRM.Domain;
using ERP.HRM.ViewModel;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace ERP.Areas.HRM.Controllers
{
    [SessionExpire]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _emplyeeService;
        private readonly IEmployeeCategoryService _employeeCategoryService;
        private readonly IDepartmentService _departmentService;
        private readonly IDesignationService _designationService;
        private readonly IPersonalInformationService _personalInformationService;
        private readonly ICountryService _countryService;
        private readonly IRosterInformationService _rosterInformationService;

        public EmployeeController()
        {
            var kernel = GlobalConfiguration.Configuration.DependencyResolver as NinjectResolver;
            if (kernel != null)
            {
                _emplyeeService = kernel.GetService(typeof(EmployeeService)) as EmployeeService;
                _employeeCategoryService = kernel.GetService(typeof(EmployeeCategoryService)) as EmployeeCategoryService;
                _departmentService = kernel.GetService(typeof(DepartmentService)) as DepartmentService;
                _designationService = kernel.GetService(typeof(DesignationService)) as DesignationService;
                _personalInformationService = kernel.GetService(typeof(PersonalInformationService)) as PersonalInformationService;
                _countryService = kernel.GetService(typeof(CountryService)) as CountryService;
                _rosterInformationService = kernel.GetService(typeof(IRosterInformationService)) as RosterInformationService;
            }
        }

        public ActionResult List()
        {
            return View();
        }

        public ActionResult Index(long? eid)
        {
            var countryList = _countryService.GetAll().ToList();
            Session["CountryList"] = countryList;
            var employeeList = _emplyeeService.GetAll().ToList();

            var employeeView = new EmployeeViewModel
            {
                EmployeeCategoryList = new SelectList(_employeeCategoryService.GetAll(), "Id", "Name"),
                DepartmentList = new SelectList(_departmentService.GetAll(), "Id", "Name"),
                DesignationList = new SelectList(_designationService.GetAll(), "Id", "Name"),
                RosterList = new SelectList(_rosterInformationService.GetAll(), "Id", "Name"),
                SupervisorList = new SelectList(employeeList, "Id", "EmployeeCode"),
                ApproverList = new SelectList(employeeList, "Id", "EmployeeCode"),

                TitleList = new SelectList(_personalInformationService.GetTitleList()),
                GenderList = new SelectList(_personalInformationService.GetGenderList(), "Value", "Text"),
                MaritalStatusList = new SelectList(_personalInformationService.GetMaritalStatusList(), "Value", "Text")
            };
            return View(employeeView);
        }

        public JsonResult GetList([DataSourceRequest]DataSourceRequest request)
        {
            var employeeList = _emplyeeService.GetAllEmployees();
            return Json(employeeList.ToDataSourceResult(request));
        }

        [ValidateAntiForgeryToken]
        public JsonResult Save(EmployeeModel employee, bool isInsert)
        {
            if (isInsert)
            {
                employee.SetCreateProperties(LoginInformation.UserInformation.Id);
                employee.Id = _emplyeeService.Insert(employee);
            }
            else
            {
                employee.SetUpdateProperties(LoginInformation.UserInformation.Id);
                _emplyeeService.Update(employee);
            }
            return new JsonResult { Data = employee };
        }

        public JsonResult Get(long id)
        {
            return new JsonResult { Data = _emplyeeService.GetEmployeeDetails(id) };
        }

        public void Delete(long id)
        {
            _emplyeeService.DeleteSoftly(id);
        }        
    }
}