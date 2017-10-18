using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ERP.BLL.HRM;
using ERP.BLL.Security;
using ERP.Security.Domain;

namespace ERP.Controllers
{
    //[SessionExpire]
    public class GeneralController : Controller
    {
        private readonly IUserInformationService _userInformationService;
        private readonly IMenuService _menuService;
        private readonly IEmployeeService _employeeService;
        private readonly IEmployeeLeaveRequestService _employeeLeaveRequestService;

        public GeneralController()
        {
            var kernel = GlobalConfiguration.Configuration.DependencyResolver as NinjectResolver;
            if (kernel != null)
            {
                _userInformationService = kernel.GetService(typeof(UserInformationService)) as UserInformationService;
                _menuService = kernel.GetService(typeof(MenuService)) as MenuService;
                _employeeService = kernel.GetService(typeof(EmployeeService)) as EmployeeService;
                _employeeLeaveRequestService = kernel.GetService(typeof(EmployeeLeaveRequestService)) as EmployeeLeaveRequestService;
            }
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Login(string username, string password)
        {
            try
            {
                var userInfo = _userInformationService.GetUserDetails(null, username);
                if (userInfo == null)
                {
                    return Json(new { result = "Username or password is incorrect. Please enter your valid credentials.", url = Url.Action("Index", "Home") });
                }

                if (Authenticator.ValidatePassword(password.Trim(), userInfo.Password))
                {
                    if (!userInfo.IsActive || userInfo.IsLocked)
                    {
                        return Json(new { val = 1, result = "User is not active now. Please contact with your system administrator.", url = Url.Action("Index", "Home") });
                    }

                    SetLoginInformation(userInfo);
                    if (userInfo.IsPasswordChanged)
                    {
                        return Json(new
                        {
                            val = 2,
                            result = "Welcome " + userInfo.EmployeeName,
                            url = Url.Action("Index", "Home")
                        });
                    }
                    return Json(new { val = 3, result = "Change your password", url = @Url.Action("PasswordChange", "UserInformation", new { area = "SecurityAdministration" }) }); 
                }
                return Json(new { result = "Username or password is incorrect. Please enter your valid credentials.", url = Url.Action("Index", "Home") });
            }
            catch (Exception exception)
            {
                if (exception.Message.Contains("The underlying provider failed on Open"))
                {
                    return Json(new { result = "Failed to connect database server. Please contact with your system administrator.", url = Url.Action("Index", "Home") });
                }
            }
            return Json(new { result = "Application service is not available now. Please try again later." });
        }

        private void SetLoginInformation(UserInformationModel userInformation)
        {
            Session["IsLogged"] = true;
            LoginInformation.UserInformation = userInformation;

            if (userInformation.RoleId != null)
            {
                LoginInformation.IsApprover = _employeeService.IsApprover(userInformation.EmployeeId);
                if (LoginInformation.IsApprover)
                {
                    LoginInformation.IncomingLeaveRequest = _employeeLeaveRequestService.GetEmployeeLeaveRequests(
                                                            LoginInformation.UserInformation.EmployeeId)
                                                            .Where(s => s.ApprovalStatusId == 1 || s.ApprovalStatusId == 2).ToList().Count;
                }
                LoginInformation.PermittedMenues = _menuService.GetPermittedMenus((long)userInformation.RoleId);
            }
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult LogOut()
        {
            HttpSessionStateBase session = HttpContext.Session; //filterContext.HttpContext.Session;
            if (session != null)
            {
                session.RemoveAll();
                session.Clear();
                session.Abandon();

                Session["IsLogged"] = false;
                LoginInformation.UserInformation = null;
                LoginInformation.PermittedMenues = null;
            }
            return Json(new { result = "Redirect", url = Url.Action("Index", "Home") });
        }
    }
}