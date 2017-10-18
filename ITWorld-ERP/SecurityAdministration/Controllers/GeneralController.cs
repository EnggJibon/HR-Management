using System;
using System.Linq;
using System.Web.Mvc;
using SecurityAdministration.BLL;
using SecurityAdministration.BLL.ViewModels;
using SecurityAdministration.DAL.Repositories;

namespace SecurityAdministration.Controllers
{
    public class GeneralController : Controller
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();

        [HttpPost]
        public ActionResult Login(string loginID, string password)
        {
            //var jsonData = new JData();

            try
            {
                var userInfo = _unitOfWork.UserCredentialInformationRepository.Get(null,loginID).FirstOrDefault();

                if (userInfo != null)
                {
                    if (Authenticator.ValidatePassword(password.Trim(), userInfo.Password))
                    {
                        if (!userInfo.IsActive || userInfo.IsLocked != null && (bool) userInfo.IsLocked)
                        {
                            //jsonData.Check = 1;
                            //jsonData.Message = "There is a problem contact your administrator";
                            //return new JsonResult{Data = jsonData};
                            return Json(new {val = 1, result = "User not valid", url = Url.Action("Index", "Home")});
                        }

                        if (userInfo.IsPasswordAccepted != null && (bool) userInfo.IsPasswordAccepted)
                        {
                            //jsonData.Check = 2;
                            //jsonData.Message = "Successfully Logged In";

                            Session["IsLogged"] = true;
                            SetLoginInformation(userInfo);
                            //return new JsonResult {Data = jsonData};
                            return
                                Json(
                                    new
                                    {
                                        val = 2,
                                        result = "Welcome " + userInfo.FullName,
                                        url = Url.Action("Index", "Home")
                                    });

                        }
                        else
                        {
                            //jsonData.Check = 3;
                            //jsonData.Message = "Change";
                            //return new JsonResult {Data = jsonData};
                            Session["IsLogged"] = true;
                            SetLoginInformation(userInfo);
                            return
                                Json(
                                    new
                                    {
                                        val = 3,
                                        result = "Change your password",
                                        url = Url.Action("ChangePassword", "Home")
                                    });
                        }
                    }
                }
                else
                {
                    //jsonData.Check = 0;
                    //jsonData.Message = "Login Failed";
                    //return new JsonResult{Data = jsonData};
                    return Json(new {result = "Login Failed", url = Url.Action("Index", "Home")});
                }
            }
            catch (Exception exception)
            {
                //jsonData.Check = 0;
                //jsonData.Message = exception.Message;
                return Json(new {result = exception.Message, url = Url.Action("Index", "Home")});
            }
            return Json(new {result = "Sorry system error"});
        }

        private void SetLoginInformation(UserView userView)
        {
            LoginInformation.UserID = Convert.ToInt32(userView.UserID);
            LoginInformation.FullName = userView.FirstName + " " + userView.MiddleName + " " + userView.LastName;
            LoginInformation.IsActive = userView.IsActive;
            LoginInformation.IsLocked = userView.IsLocked;
            LoginInformation.IsPasswordAccepted = userView.IsPasswordAccepted;
            LoginInformation.LastPasswordChangedDate = Convert.ToDateTime(userView.LastPasswordChangedDate);
            LoginInformation.LoginID = userView.LoginID;
            LoginInformation.Password = userView.Password;
            LoginInformation.SupervisorUserID = Convert.ToInt32(userView.SupervisorUserID);
            LoginInformation.UserCode = userView.UserCode;
            LoginInformation.UserType = userView.UserType;
            LoginInformation.DesignationID = userView.DesignationID;
        }

        public ActionResult LogOut()
        {
            Session["IsLogged"] = false;
            //return new JsonResult {Data = "Successfully Logout."};
            return Json(new {result = "Redirect", url = Url.Action("Index", "Home")});
        }
    }
}