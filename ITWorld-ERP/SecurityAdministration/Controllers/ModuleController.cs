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
    public class ModuleController : Controller
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();

        public ActionResult Index()
        {
            if (!Convert.ToBoolean(Session["IsLogged"])) return RedirectToAction("Index", "Home");

            var moduleViewModel = new ModuleVM
            {
                ApplicationList = new SelectList(_unitOfWork.ApplicationRepository.Get(), "ApplicationID", "Title")
            };
            return View(moduleViewModel);
        }

        [HttpPost]
        public JsonResult Save([Bind(Include = "ModuleID,ApplicationID,Title,IsActive,Description,SetOn,SetBy")] Module module, bool isInsert)
        {
            if (ModelState.IsValid)
            {
                module.SetBy = LoginInformation.UserID;
                module.SetOn = DateTime.Now;
                module.IsDelete = false;

                if (isInsert)
                {
                    module.ModuleID = GetNewModuleID();
                    _unitOfWork.ModuleRepository.Insert(module);
                }
                else
                {
                    _unitOfWork.ModuleRepository.Update(module);
                }
                _unitOfWork.Save();
            }

            ViewBag.ApplicationID = new SelectList(_unitOfWork.ApplicationRepository.Get(), "ApplicationID", "Title", module.ApplicationID);
            return new JsonResult { Data = _unitOfWork.ModuleRepository.GetByValue(module.ModuleID).FirstOrDefault() };
        }

        public void Delete(string id)
        {
            _unitOfWork.ModuleRepository.IsDeleteTrue(id.PadLeft(2, '0'));
            _unitOfWork.Save();
        }

        public JsonResult GetModule(string id)
        {
            return new JsonResult { Data = _unitOfWork.ModuleRepository.GetByValue(id).FirstOrDefault() };
        }

        public JsonResult GetApplication(byte applicationId)
        {
            return new JsonResult { Data = _unitOfWork.ApplicationRepository.GetByValue(applicationId) };
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _unitOfWork.Dispose();
            base.Dispose(disposing);
        }

        private string GetNewModuleID()
        {
            string moduleID = _unitOfWork.ModuleRepository.GetLastModuleID();
            return string.IsNullOrWhiteSpace(moduleID) ? "01" : (Convert.ToInt32(moduleID) + 1).ToString().PadLeft(2, '0');
        }

        public ActionResult ModuleList([DataSourceRequest]DataSourceRequest request)
        {
            var moduleList = _unitOfWork.ModuleRepository.GetAll()
                                    .Where(s => s.IsDelete == false)
                                    .OrderByDescending(s => s.ModuleID);
            return Json(moduleList.ToDataSourceResult(request));
        }
    }
}
