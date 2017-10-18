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
    public class ApplicationController : Controller
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();

        public ActionResult Index()
        {
            if (!Convert.ToBoolean(Session["IsLogged"])) return RedirectToAction("Index", "Home");
            return View();
        }

        public JsonResult Save([Bind(Include = "Title,ApplicationID,Description,IsActive,SetOn,SetBy")] ApplicationView applicationView, bool isInsert)
        {
            var application = new Application
            {
                ApplicationID = Convert.ToByte(applicationView.ApplicationID),
                Description = applicationView.Description,
                Title = applicationView.Title,
                IsActive = applicationView.IsActive,
                IsDelete = false,
                SetOn = DateTime.Now,
                SetBy = LoginInformation.UserID
            };

            if (!ModelState.IsValid)
            {
                return new JsonResult
                {
                    Data =_unitOfWork.ApplicationRepository.GetByID(Convert.ToByte(application.ApplicationID)).ToApplicationView()
                };
            }

            if (isInsert)
            {
                _unitOfWork.ApplicationRepository.Insert(application);
            }
            else
            {
                _unitOfWork.ApplicationRepository.Update(application);
            }
            _unitOfWork.Save();

            return new JsonResult { Data = _unitOfWork.ApplicationRepository.GetByID(Convert.ToByte(application.ApplicationID)).ToApplicationView() };
        }


        public JsonResult GetApplication(byte? id)
        {
            return new JsonResult { Data = _unitOfWork.ApplicationRepository.GetByID(id).ToApplicationView() };
        }

        public void Delete(byte? id)
        {
            _unitOfWork.ApplicationRepository.IsDeleteTrue(id);
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

        public ActionResult ApplicationList([DataSourceRequest]DataSourceRequest request)
        {
            var applicationList = _unitOfWork.ApplicationRepository.Get()
                                 .Where(s => s.IsDelete == false)
                                 .OrderByDescending(s => s.ApplicationID)
                                 .ToApplicationView();
            return Json(applicationList.ToDataSourceResult(request));
        }
    }
}
