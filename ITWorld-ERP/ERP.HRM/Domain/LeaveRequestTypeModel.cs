using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.Utilities.Infrastructure;

namespace ERP.HRM.Domain
{
    public class LeaveRequestTypeModel : BaseDomainModel<LeaveRequestTypeModel>
    {

        public string RequestTypeName { get; set; }
        public string Description { get; set; }


    }
}
