using System.Collections.Generic;
using ERP.Security.Domain;

namespace ERP.Security.ViewModel
{
    public class ApplicationViewModel
    {
        public IEnumerable<ApplicationModel> Applications { get; set; }
        public ApplicationModel Application { get; set; }
    }
}
