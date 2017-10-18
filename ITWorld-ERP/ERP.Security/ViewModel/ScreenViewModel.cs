using System.Collections.Generic;
using System.Web.Mvc;
using ERP.Security.Domain;

namespace ERP.Security.ViewModel
{
    public class ScreenViewModel
    {
        public IEnumerable<ScreenModel> Screens { get; set; }
        public ScreenModel Screen { get; set; }
        public SelectList ModuleList { get; set; }
        public SelectList ScreenTypeList { get; set; }
    }
}
