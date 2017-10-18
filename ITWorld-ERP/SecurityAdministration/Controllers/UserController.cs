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
    public class UserController : Controller
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();

        public ActionResult Index()
        {
            if (!Convert.ToBoolean(Session["IsLogged"])) return RedirectToAction("Index", "Home");
            try
            {
                var userViewModel = new UserVM
                {
                    //Users = _unitOfWork.UserRepository.GetUserInfo(0, string.Empty).Where(s => s.IsDelete == false).OrderByDescending(s=>s.UserID),
                    DesignationList = new SelectList(_unitOfWork.DesignationRepository.Get(), "DesignationID", "Name"),
                    SupervisorList = new SelectList(_unitOfWork.UserRepository.GetAllSupervisor(), "UserID", "Supervisor"),
                };

                //ViewBag.UserList = userViewModel.Users.ToList();
                var lastUserCode = _unitOfWork.UserRepository.GetLastUserCodeID();
                ViewBag.UserCode = GetUserCodeID(lastUserCode);

                return View(userViewModel);
            }
            catch (Exception exception)
            {
                throw exception.InnerException;
            }
        }

        public JsonResult Save([Bind(Include = "UserID, SupervisorUserID,UserCode,UserType,Title,FirstName,MiddleName,LastName,DesignationID,Email,Phone,Mobile,IsActive,Description,LoginID,Password,SetOn,SetBy")] UserView userView, bool isInsert, bool isResetPassword)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new User
                    {
                        UserID = Convert.ToInt32(userView.UserID),
                        UserCode = userView.UserCode,
                        UserType = userView.UserType,
                        Title = userView.Title,
                        FirstName = userView.FirstName,
                        MiddleName = userView.MiddleName,
                        LastName = userView.LastName,
                        DesignationID = userView.DesignationID,
                        Email = userView.Email,
                        Phone = userView.Phone,
                        Mobile = userView.Mobile,
                        IsActive = userView.IsActive,
                        SupervisorUserID = Convert.ToInt32(userView.SupervisorUserID),
                        Description = userView.Description,
                        SetBy = LoginInformation.UserID,
                        SetOn = DateTime.Now
                    };

                    var userCredential = new UserCredentialInformation
                    {
                        SetBy = LoginInformation.UserID,
                        SetOn = DateTime.Now,
                        LastPasswordChangedDate = DateTime.Now,
                        IsPasswordAccepted = false,
                        IsLocked = false,
                        Password = Authenticator.GetHashPassword(userView.Password),
                        LoginID = userView.LoginID,
                        UserID = Convert.ToInt32(userView.UserID),
                    };


                    if (isInsert)
                    {
                        _unitOfWork.UserRepository.Insert(user);
                        _unitOfWork.Save();
                        var lastInsertID = _unitOfWork.UserRepository.GetLastInsertID();
                        userCredential.UserID = lastInsertID;
                        _unitOfWork.UserCredentialInformationRepository.Insert(userCredential);
                        _unitOfWork.Save();
                    }
                    else
                    {
                        if (isResetPassword)
                        {
                            _unitOfWork.UserRepository.Update(user);
                            _unitOfWork.UserCredentialInformationRepository.UpdatePassword(userCredential);
                        }
                        else
                        {
                            _unitOfWork.UserRepository.Update(user);
                        }
                        _unitOfWork.Save();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return new JsonResult { Data = _unitOfWork.UserRepository.GetUserInfo(Convert.ToInt32(userView.UserID)).FirstOrDefault() };
        }

        public JsonResult GetUser(int id)
        {
            return new JsonResult { Data = _unitOfWork.UserRepository.GetUserInfo(id).FirstOrDefault() };
        }

        public JsonResult UserCodeGenerate()
        {
            var lastUserCode = _unitOfWork.UserRepository.GetLastUserCodeID();
            return new JsonResult { Data = GetUserCodeID(lastUserCode) };
        }

        public void Delete(int id)
        {
            _unitOfWork.UserRepository.InActiveUserInformation(id);
            _unitOfWork.Save();
        }

        public ActionResult ChangePassword(int userID, string password)
        {

            _unitOfWork.UserRepository.PasswordChange(userID, Authenticator.GetHashPassword(password));
            _unitOfWork.Save();

            return Json(new { result = "password has been changed", url = Url.Action("Index", "Home") });
        }

        public static string GetUserCodeID(string lastUserCode)
        {
            var userCodePrefix = System.Configuration.ConfigurationManager.AppSettings["UserCodePrefix"];

            lastUserCode = lastUserCode ?? userCodePrefix + "-" + "0000";
            int digit = Convert.ToInt16(lastUserCode.Split('-').Last());
            var userCode = "";
            digit++;
            var numberOfUser = System.Configuration.ConfigurationManager.AppSettings["NumberOfUser"];
            if (digit > Convert.ToInt16(numberOfUser))
            {
                userCode = "CROSS_LIMIT";
            }
            else
            {
                userCode += userCodePrefix + "-" + Convert.ToString(digit).PadLeft(4, '0');
            }
            return userCode;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult UserList([DataSourceRequest]DataSourceRequest request)
        {
            var userList = _unitOfWork.UserRepository.GetUserInfo(0, string.Empty)
                        .Where(s => s.IsDelete == false)
                        .OrderByDescending(s => s.UserID);

            return Json(userList.ToDataSourceResult(request));
        }
    }
}
