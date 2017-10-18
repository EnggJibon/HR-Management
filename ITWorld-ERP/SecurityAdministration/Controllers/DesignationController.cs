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
    public class DesignationController : Controller
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();

        public ActionResult Index()
        {
            if (!Convert.ToBoolean(Session["IsLogged"])) return RedirectToAction("Index", "Home");
            return View();
        }

        public JsonResult Save([Bind(Include = "Name,Description,IsActive,SetOn,SetBy")] DesignationView designationView, bool isInsert)
        {
            var designation = new Designation
            {
                DesignationID = Convert.ToByte(designationView.DesignationID),
                Description = designationView.Description,
                Name = designationView.Name,
                IsActive = designationView.IsActive,
                IsDelete = false,
                SetOn = DateTime.Now,
                SetBy = LoginInformation.UserID
            };

            if (!ModelState.IsValid)
                return new JsonResult
                {
                    Data =_unitOfWork.DesignationRepository.GetByID(Convert.ToByte(designation.DesignationID)).ToDesignationView()
                };

            if (isInsert)
            {
                _unitOfWork.DesignationRepository.Insert(designation);
            }
            else
            {
                _unitOfWork.DesignationRepository.Update(designation);
            }
            _unitOfWork.Save();

            return new JsonResult { Data = _unitOfWork.DesignationRepository.GetByID(Convert.ToByte(designation.DesignationID)).ToDesignationView() };
        }

        public JsonResult GetDesignation(byte? id)
        {
            return new JsonResult { Data = _unitOfWork.DesignationRepository.GetByID(id).ToDesignationView(), };
        }

        public void Delete(byte? id)
        {
            _unitOfWork.DesignationRepository.IsDeleteTrue(id);
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

        public ActionResult DesignationList([DataSourceRequest]DataSourceRequest request)
        {
            var designationList = _unitOfWork.DesignationRepository.Get()
                                    .Where(s => s.IsDelete == false)
                                    .OrderByDescending(s => s.DesignationID)
                                    .ToDesignationView();
            return Json(designationList.ToDataSourceResult(request));
        }
    }
}