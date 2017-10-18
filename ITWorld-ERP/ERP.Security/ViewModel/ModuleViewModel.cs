using System.Collections.Generic;
using System.Web.Mvc;
using ERP.Security.Domain;

namespace ERP.Security.ViewModel
{
    public class ModuleViewModel
    {
        public IEnumerable<ModuleModel> Modules { get; set; }
        public ModuleModel Module { get; set; }
        public SelectList ApplicationList { get; set; }
    }
}
