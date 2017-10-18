using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using SecurityAdministration.BLL;
using SecurityAdministration.BLL.ViewModels;
using SecurityAdministration.DAL;
using SecurityAdministration.DAL.Repositories;
using System.Globalization;

namespace SecurityAdministration.Controllers
{
    public class ScreenController : Controller
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();

        public ActionResult Index()
        {
            if (!Convert.ToBoolean(Session["IsLogged"])) return RedirectToAction("Index", "Home");

            var screenViewModel = new ScreenVM
            {
                //Screens = _unitOfWork.ScreenRepository.GetAll().OrderByDescending(s=>s.ScreenCode),
                ModuleList = new SelectList(_unitOfWork.ModuleRepository.Get(), "ModuleID", "Title")
            };
            //ViewBag.ScreenList = screenViewModel.Screens.ToList();
            return View(screenViewModel);
        }

        public JsonResult Save([Bind(Include = "ScreenCode,ModuleID,Title,Type,URL,IsActive,Description")] Screen screen, bool isInsert)
        {
            //var objscreen = new Screen
            //{
            //    ScreenCode = screen.ScreenCode,
            //    ModuleID = screen.ModuleID,
            //    Title = screen.Title,
            //    Type = screen.Type,
            //    URL = screen.URL,
            //    Description = screen.Description,
            //    //ModuleTitle = screen.ModuleTitle,
            //    IsActive = screen.IsActive,
            //    SetOn = Convert.ToDateTime(DateTime.Now),
            //    SetBy = LoginInformation.UserID
            //};

            //if (!ModelState.IsValid)
            //    //return new JsonResult { Data = _unitOfWork.UserInRoleRepository.GetByID(userInRole.UserID).ToUserInRoleView() };
            //    return new JsonResult { Data = _unitOfWork.ScreenRepository.GetByID(screen.ScreenCode).ToScreenView() };

            //if (isInsert)
            //{
            //    _unitOfWork.ScreenRepository.Insert(objscreen);
            //}
            //else
            //{
            //    _unitOfWork.ScreenRepository.Update(objscreen);
            //}

            //_unitOfWork.Save();
            //return new JsonResult { Data = _unitOfWork.ScreenRepository.GetByID(objscreen.ScreenCode).ToScreenView() };

            if (ModelState.IsValid)
            {
                screen.SetBy = LoginInformation.UserID;
                screen.SetOn = DateTime.Now;

                if (isInsert)
                {
                    screen.ScreenCode = GetNewScreenCode();
                    _unitOfWork.ScreenRepository.Insert(screen);
                }
                else
                {
                    _unitOfWork.ScreenRepository.Update(screen);
                }

                _unitOfWork.Save();
            }

            return new JsonResult { Data = _unitOfWork.ScreenRepository.GetByValue(screen.ScreenCode) };
        }

        public JsonResult GetScreen(string id)
        {
            var screen = _unitOfWork.ScreenRepository.GetByValue(id.PadLeft(5, '0'));
            return new JsonResult { Data = screen };
        }

        public JsonResult ScreenCheck(string id)
        {
            bool isExistOrNot = false;
            var objScreen = _unitOfWork.ScreenRepository.GetByValue(id.PadLeft(5, '0'));
            if (objScreen == null)
            {
                isExistOrNot = true;
            }
            return new JsonResult { Data = isExistOrNot };
        }

        public void Delete(string id)
        {
            _unitOfWork.ScreenRepository.IsDeleteTrue(id.PadLeft(5, '0'));
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

        private string GetNewScreenCode()
        {
            string screenCode = _unitOfWork.ScreenRepository.LastScreenCode();

            return string.IsNullOrWhiteSpace(screenCode) ? "00001" : (Convert.ToInt32(screenCode) + 1).ToString().PadLeft(5, '0');
        }

        public ActionResult ScreenList([DataSourceRequest]DataSourceRequest request)
        {
            var screenList = Mapper.Map<List<ScreenView>>(_unitOfWork.ScreenRepository.GetAll().OrderByDescending(s => s.ScreenCode));
            return Json(screenList.ToDataSourceResult(request));
        }
    }
}
