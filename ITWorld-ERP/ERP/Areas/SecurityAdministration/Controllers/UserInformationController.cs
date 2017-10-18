using System.Web.Http;
using System.Web.Mvc;
using ERP.BLL.HRM;
using ERP.BLL.Security;
using ERP.Security.Domain;
using ERP.Security.ViewModel;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace ERP.Areas.SecurityAdministration.Controllers
{
    [SessionExpire]
    public class UserInformationController : Controller
    {
        private readonly IUserInformationService _userInformationService;
        private readonly IUserTypeService _userTypeService;
        private readonly IRoleService _roleService;
        private readonly IEmployeeCategoryService _employeeCategoryService;
        private readonly IEmployeeService _employeeService;

        public UserInformationController()
        {
            var kernel = GlobalConfiguration.Configuration.DependencyResolver as NinjectResolver;
            if (kernel != null)
            {
                _userInformationService = kernel.GetService(typeof(UserInformationService)) as UserInformationService;
                _userTypeService = kernel.GetService(typeof(UserTypeService)) as UserTypeService;
                _roleService = kernel.GetService(typeof(RoleService)) as RoleService;
                _employeeCategoryService = kernel.GetService(typeof(EmployeeCategoryService)) as EmployeeCategoryService;
                _employeeService = kernel.GetService(typeof(EmployeeService)) as EmployeeService;
            }
        }

        public ActionResult List()
        {
            return View();
        }

        public ActionResult Index(long? uid)
        {
            var userInformationView = new UserInformationViewModel
            {
                UserTypeList = new SelectList(_userTypeService.GetAll(), "Id", "Name"),
                RoleList = new SelectList(_roleService.GetAll(), "Id", "Name"),
                EmployeeCategoryList = new SelectList(_employeeCategoryService.GetAll(), "Id", "Name"),
                EmployeeList = new SelectList(string.Empty, "Id", "EmployeeCode"),
            };
            return View(userInformationView);
        }

        public ActionResult PasswordChange()
        {
            return View();
        }

        public JsonResult GetEmployees(long categoryId)
        {
            return Json(_employeeService.GetEmployees(categoryId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetList([DataSourceRequest]DataSourceRequest request)
        {
            var userList = _userInformationService.GetAllUsers();
            return Json(userList.ToDataSourceResult(request));
        }

        public JsonResult Save([Bind(Include = "Id, UserTypeId, EmployeeId, RoleId, Username, Password, PasswordAgeLimit, IsPasswordChanged, IsLocked, WrongPasswordTryLimit, IsSuperAdmin, IsActive")] UserInformationModel userInformation, bool isInsert)
        {
            if (isInsert)
            {
                userInformation.SetCreateProperties(LoginInformation.UserInformation.Id);
                userInformation.Id = _userInformationService.Insert(userInformation);
            }
            else
            {
                userInformation.SetUpdateProperties(LoginInformation.UserInformation.Id);
                _userInformationService.Update(userInformation);
            }
            return new JsonResult { Data = _userInformationService.GetById(userInformation.Id) };
        }

        public JsonResult Get(long id)
        {
            return new JsonResult { Data = _userInformationService.GetUserDetails(id, "") };
        }

        public void Delete(long id)
        {
            _userInformationService.DeleteSoftly(id);
        }

        public JsonResult GetByEmployeeCode(string employeeCode)
        {
            if (string.IsNullOrWhiteSpace(employeeCode))
            {
                employeeCode = LoginInformation.UserInformation.EmployeeCode;
            }
            var employee = _employeeService.GetEmployeeInformation(null, employeeCode);
            if (employee != null)
            {
                var userInfo = _userInformationService.GetUser(employee.Id);

                if (userInfo != null)
                {
                    userInfo.Employee = employee;
                    return new JsonResult { Data = userInfo };
                }
            }
            return new JsonResult { Data = new UserInformationModel { Employee = employee } };
        }

        public JsonResult ChangePassword(UserInformationModel userInformation)
        {
            if (userInformation.Id <= 0)
            {
                userInformation.Id = LoginInformation.UserInformation.Id;
            }

            userInformation.SetUpdateProperties(LoginInformation.UserInformation.Id);
            _userInformationService.ChangePassword(userInformation);

            return new JsonResult { Data = userInformation };
        }
    }
}