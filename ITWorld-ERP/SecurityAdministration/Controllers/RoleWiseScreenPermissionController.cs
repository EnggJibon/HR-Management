using System;
using System.Collections.Generic;
using System.Globalization;
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
    public class RoleWiseScreenPermissionController : Controller
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();

        public ActionResult Index()
        {
            if (!Convert.ToBoolean(Session["IsLogged"])) return RedirectToAction("Index", "Home");

            var roleWiseScreenPermissionVM = new RoleWiseScreenPermissionVM
            {
                RoleList = new SelectList(_unitOfWork.RoleRepository.Get(), "RoleID", "RoleName"),
                ModuleList = new SelectList(_unitOfWork.ModuleRepository.Get(), "ModuleID", "Title"),
            };
            return View(roleWiseScreenPermissionVM);
        }

        public JsonResult GetScreenList(string moduleID)
        {
            var screenList = new SelectList(_unitOfWork.ScreenRepository.Get().Where(w => w.ModuleID == moduleID).ToList(), "ScreenCode", "Title");
            return new JsonResult { Data = screenList };
        }

        public JsonResult GetScreenPermissionList(int? roleID, string moduleID, string screenCode)
        {
            IEnumerable<ScreenPermissionView> screenPermissionList = _unitOfWork.RolewiseScreenPermissionRepository.Get(roleID, moduleID, screenCode);
            return new JsonResult { Data = screenPermissionList };
        }

        public JsonResult GetRoleWiseScreenPermission(int roleID, string screenCode)
        {
            screenCode = screenCode.PadLeft(5, '0');

            var result = _unitOfWork.RolewiseScreenPermissionRepository.Get(roleID, screenCode: screenCode).FirstOrDefault();

            return new JsonResult
            {
                Data = new JData
                {
                    JsonData = result,
                    Message = null
                }
            };
        }

        public JsonResult Save([Bind(Include = "RoleID,ScreenCode,AccessRight")] RoleWiseScreenPermission rolewisescreenpermission, bool isInsert)
        {
            if (ModelState.IsValid)
            {
                rolewisescreenpermission.SetBy = LoginInformation.UserID;
                rolewisescreenpermission.SetOn = DateTime.Now;

                var screenPermission = _unitOfWork.RolewiseScreenPermissionRepository.Get(rolewisescreenpermission.RoleID, screenCode: rolewisescreenpermission.ScreenCode).FirstOrDefault();

                if (isInsert)
                {
                    if (screenPermission != null)
                    {
                        return new JsonResult
                        {
                            Data = new JData
                                {
                                    JsonData = null,
                                    Message = "Information already exists. Please press edit to modify."
                                }
                        };
                    }
                    _unitOfWork.RolewiseScreenPermissionRepository.Insert(rolewisescreenpermission);
                }
                else
                {
                    _unitOfWork.RolewiseScreenPermissionRepository.Update(rolewisescreenpermission);
                }
                _unitOfWork.Save();
            }

            return GetRoleWiseScreenPermission(rolewisescreenpermission.RoleID, rolewisescreenpermission.ScreenCode);
        }

        public void Delete(int roleID, string screenCode)
        {
            screenCode = screenCode.PadLeft(5, '0');
            _unitOfWork.RolewiseScreenPermissionRepository.DeleteRoleWiseScreenPermission(roleID, screenCode);
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

        public ActionResult ScreenPermissionList([DataSourceRequest]DataSourceRequest request, int? roleID, string moduleID, string screenCode)
        {
            var roleWiseScreenPermissionList = _unitOfWork.RolewiseScreenPermissionRepository.Get(roleID, moduleID, screenCode);
            return Json(roleWiseScreenPermissionList.ToDataSourceResult(request));
        }
    }
}
