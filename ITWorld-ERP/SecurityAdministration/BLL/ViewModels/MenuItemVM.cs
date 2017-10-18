using System.Collections.Generic;
using System.Web.Mvc;

namespace SecurityAdministration.BLL.ViewModels
{
    public class MenuItemVM
    {
        public IEnumerable<MenuItemView> MenuItems { get; set; }
        public MenuItemView MenuItem { get; set; }
        public SelectList ModuleList { get; set; }
        public SelectList ScreenList { get; set; }
    }

    public class MenuItemView
    {
        public byte MenuID { get; set; }
        public string Caption { get; set; }
        public byte MenuLevel { get; set; }
        public byte ItemOrder { get; set; }
        public byte? ParentID { get; set; }
        public string ScreenCode { get; set; }
        public string ScreenTitle { get; set; }
        public string ModuleTitle { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public string ScreenModuleID { get; set; }
        public string Description { get; set; }
        public bool HasChild { get; set; }
        public string SetOn { get; set; }
        public int SetBy { get; set; }
    }
}