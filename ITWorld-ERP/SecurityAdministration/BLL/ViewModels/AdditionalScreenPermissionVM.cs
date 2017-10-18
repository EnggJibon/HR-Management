using System.Web.Mvc;

namespace SecurityAdministration.BLL.ViewModels
{
    public class AdditionalScreenPermissionVM
    {
        public AdditionalScreenPermissionView AdditionalScreenPermission { get; set; }
        public SelectList UserList { get; set; }
        public SelectList ModuleList { get; set; }
    }

    public class AdditionalScreenPermissionView
    {
        public int UserID { get; set; }
        public string LastName { get; set; }
        public string ModuleID { get; set; }
        public string ModuleTitle { get; set; }
        public string ScreenCode { get; set; }
        public string ScreenTitle { get; set; }
        public string CanRead { get; set; }
        public string CanCreate { get; set; }
        public string CanUpdate { get; set; }
        public string CanDelete { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string SetOn { get; set; }
        public int SetBy { get; set; }
    }
}