using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SecurityAdministration.BLL.ViewModels;
using SecurityAdministration.DAL.Repositories;

namespace SecurityAdministration.Controllers
{
    public class AdditionalOprationPermissionController : Controller
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        public ActionResult Index()
        {
            if (Convert.ToBoolean(Session["IsLogged"]))
            {
                var additionalOperationPermissionVm = new AdditionalOperationPermissionVm
                {
                   
                    UserList = new SelectList(_unitOfWork.RoleRepository.Get(), "UserID", "UserName"),
                    OperationList = new SelectList(_unitOfWork.ScreenOperationRepository.Get(), "OperationID", "OperationTitle"),
                };
                //ViewBag.RoleWiseOperationPermissionList = roleWiseOperationPermissionVm.RoleWiseScreenPermissions.ToList();
                return View(additionalOperationPermissionVm);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}