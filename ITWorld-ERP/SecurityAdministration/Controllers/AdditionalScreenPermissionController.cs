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
    public class AdditionalScreenPermissionController : Controller
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();

        public ActionResult Index()
        {
            if (!Convert.ToBoolean(Session["IsLogged"])) return RedirectToAction("Index", "Home");

            var roleWiseAdditionalScreenPermissionVM = new AdditionalScreenPermissionVM
            {
                UserList = new SelectList(_unitOfWork.UserRepository.Get(), "UserID", "LastName"),
                ModuleList = new SelectList(_unitOfWork.ModuleRepository.Get(), "ModuleID", "Title"),
            };

            return View(roleWiseAdditionalScreenPermissionVM);
        }

        public ActionResult AdditionalScreenPermissionList([DataSourceRequest]DataSourceRequest request, int? userID, string moduleID, string screenCode)
        {
            var roleWiseScreenPermissionList = _unitOfWork.AdditionalScreenPermissionRepository.Get(userID, moduleID, screenCode);
            return Json(roleWiseScreenPermissionList.ToDataSourceResult(request));
        }

        public JsonResult Save([Bind(Include = "UserID,ScreenCode,AccessRight,StartDate,EndDate")] AdditionalScreenPermission additionalScreenPermission, bool isInsert)
        {
            if (ModelState.IsValid)
            {
                additionalScreenPermission.SetBy = LoginInformation.UserID;
                additionalScreenPermission.SetOn = DateTime.Now;

                if (isInsert)
                {
                    if (_unitOfWork.AdditionalScreenPermissionRepository.IsExistAdditionalScreenPermission(additionalScreenPermission.UserID, additionalScreenPermission.ScreenCode))
                    {
                        return new JsonResult
                        {
                            Data = new JData
                            {
                                JsonData = null,
                                Message = "Permission already exists. Please press edit to modify."
                            }                                                               
                        };
                    }

                    _unitOfWork.AdditionalScreenPermissionRepository.InsertAdditionalScreenPermission(additionalScreenPermission);
                }
                else
                {
                    _unitOfWork.AdditionalScreenPermissionRepository.UpdateAdditionalScreenPermission(additionalScreenPermission);
                }
                _unitOfWork.Save();
            }

            return new JsonResult
            {
                Data = new JData
                {
                    JsonData = null,
                    Message = null
                }
            };

            //return GetRoleWiseScreenPermission(additionalScreenPermissionView.RoleID, additionalScreenPermissionView.ScreenCode);
        }

        public JsonResult GetAdditionalScreenPermission(int userID, string screenCode)
        {
            screenCode = screenCode.PadLeft(5, '0');
            var result = _unitOfWork.AdditionalScreenPermissionRepository.Get(userID, screenCode: screenCode).FirstOrDefault();
            return new JsonResult { Data = result};
        }
    }
}