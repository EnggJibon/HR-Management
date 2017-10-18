using System.Collections.Generic;
using System.Web.Mvc;
using ERP.Security.Domain;

namespace ERP.Security.ViewModel
{
    public class ScreenOperationViewModel
    {
        public IEnumerable<ScreenOperationModel> ScreenOperations { get; set; }
        public ScreenOperationModel ScreenOperation { get; set; }
        public SelectList ScreenList { get; set; }
    }
}
