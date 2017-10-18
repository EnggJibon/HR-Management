using System.Collections.Generic;
using System.Web.Mvc;

namespace SecurityAdministration.BLL.ViewModels
{
    public class ModuleVM
    {
        public IEnumerable<ModuleView> Modules { get; set; }
        public ModuleView Module { get; set; }
        public SelectList ApplicationList { get; set; }
    }

    public class ModuleView
    {
        public string ModuleID { get; set; }
        public string Title { get; set; }
        public byte ApplicationID { get; set; }
        public string ApplicationTitle { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public string Description { get; set; }
        public string SetOn { get; set; }
        public int SetBy { get; set; }
    }
}