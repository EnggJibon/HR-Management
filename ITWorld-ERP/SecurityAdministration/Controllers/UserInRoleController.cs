using System;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using SecurityAdministration.BLL;
using SecurityAdministration.BLL.ViewModels;
using SecurityAdministration.DAL;
using SecurityAdministration.DAL.Repositories;

namespace SecurityAdministration.Controllers
{
    public class UserInRoleController : Controller
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();

        public ActionResult Index()
        {
            if (!Convert.ToBoolean(Session["IsLogged"])) return RedirectToAction("Index", "Home");

            var userInRoleVM = new UserInRoleVM
            {
                UserList = new SelectList(_unitOfWork.UserRepository.Get(), "UserID", "LastName"),
                RoleList = new SelectList(_unitOfWork.RoleRepository.Get(), "RoleID", "RoleName"),
            };

            return View(userInRoleVM);
        }

        public JsonResult Save([Bind(Include = "UserID,RoleID,IsActive,SetOn,SetBy")] UserInRoleView userInRoleView, bool isInsert)
        {
            var userInRole = new UserInRole
            {
                UserID = Convert.ToInt16(userInRoleView.UserID),
                RoleID = Convert.ToInt16(userInRoleView.RoleID),
                IsActive = userInRoleView.IsActive,
                SetOn = Convert.ToDateTime(DateTime.Now),
                SetBy = LoginInformation.UserID,
            };

            if (!ModelState.IsValid)
                return new JsonResult { Data = _unitOfWork.UserInRoleRepository.GetByID(userInRole.UserID).ToUserInRoleView() };

            if (isInsert)
            {
                _unitOfWork.UserInRoleRepository.Insert(userInRole);
            }
            else
            {
                _unitOfWork.UserInRoleRepository.Update(userInRole);
            }

            _unitOfWork.Save();

            return new JsonResult
            {
                Data = _unitOfWork.UserInRoleRepository.Get(userInRole.UserID, userInRole.RoleID).FirstOrDefault().ToUserInRoleView()
            };
        }

        public JsonResult GetUserInRole(int userID, int roleID)
        {
            return new JsonResult { Data = _unitOfWork.UserInRoleRepository.Get(userID, Convert.ToByte(roleID)).FirstOrDefault().ToUserInRoleView() };
        }

        //public JsonResult GetUserInRoleList(int userID, int? roleID)
        //{
        //    IEnumerable<UserInRoleView> userInRoles = _unitOfWork.UserInRoleRepository.Get(userID, roleID).ToUserInRoleView();
        //    return new JsonResult { Data = userInRoles };
        //}


        public JsonResult CheckUserInRoleEntry(int roleID)
        {
            bool isExistOrNot = false;
            var objUserView = _unitOfWork.UserInRoleRepository.CheckUser(roleID);
            if (objUserView == null)
            {
                isExistOrNot = true;
            }
            return new JsonResult { Data = isExistOrNot };
        }

        public void Delete(int userId, int roleId)
        {
            _unitOfWork.UserInRoleRepository.DeleteUserInRole(userId, roleId);
            _unitOfWork.Save();

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult UserInRoleList([DataSourceRequest]DataSourceRequest request, int? userID, int? roleID)
        {
            var userinroleList = _unitOfWork.UserInRoleRepository.GetAll(userID, roleID);
            return Json(userinroleList.ToDataSourceResult(request));
        }
    }
}
