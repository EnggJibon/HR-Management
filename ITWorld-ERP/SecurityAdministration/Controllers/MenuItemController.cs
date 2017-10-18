using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using SecurityAdministration.BLL;
using SecurityAdministration.BLL.ViewModels;
using SecurityAdministration.DAL.Repositories;

namespace SecurityAdministration.Controllers
{
    public class MenuItemController : Controller
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();

        public ActionResult Index()
        {
            if (!Convert.ToBoolean(Session["IsLogged"])) return RedirectToAction("Index", "Home");
            var menuItemViewModel = new MenuItemVM
            {
                MenuItems = _unitOfWork.MenuItemRepository.GetAll().Where(s => s.IsDelete == false).OrderByDescending(s => s.MenuID),
                ModuleList = new SelectList(_unitOfWork.ModuleRepository.Get(), "ModuleID", "Title"),
            };
            return View(menuItemViewModel);
        }

        public JsonResult Save([Bind(Include = "Caption,MenuLevel,ItemOrder,ScreenCode,ParentID,HasChild,Description,IsActive,SetOn,SetBy, IsDelete")] MenuItemView menuItemView, bool isInsert)
        {
            var menuItem = Mapper.Map<DAL.MenuItem>(menuItemView);
            menuItem.MenuID = Convert.ToByte(menuItem.MenuID);
            menuItem.SetBy = LoginInformation.UserID;
            menuItem.SetOn = DateTime.Now;

            //if (!ModelState.IsValid)
            //{
            //    return GetMenuItem(menuItem.MenuID);
            //}

            if (ModelState.IsValid)
            {
                if (isInsert)
                {
                    _unitOfWork.MenuItemRepository.Insert(menuItem);
                }
                else
                {
                    _unitOfWork.MenuItemRepository.Update(menuItem);
                }
                _unitOfWork.Save();
            }
            return GetMenuItem(menuItem.MenuID);
        }

        public JsonResult GetMenuItem(byte? id)
        {
            var data = _unitOfWork.MenuItemRepository.GetByValue(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetScreenList(string moduleid)
        {
            var screenList = new SelectList(_unitOfWork.ScreenRepository.Get().Where(w => w.ModuleID == moduleid).ToList(), "ScreenCode", "Title");
            return new JsonResult { Data = screenList };
        }

        public void Delete(byte? id)
        {
            _unitOfWork.MenuItemRepository.IsDeleteTrue(id);
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

        public ActionResult MenuItemList([DataSourceRequest]DataSourceRequest request)
        {
            var menuItemList = _unitOfWork.MenuItemRepository.Get()
                                    .Where(s => s.IsDelete == false)
                                    .OrderByDescending(s => s.MenuID).ToMenuItemView();

            return Json(menuItemList.ToDataSourceResult(request));
        }
    }
}
