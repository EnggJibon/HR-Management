using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.Utilities.Infrastructure;

namespace ERP.HRM.Domain
{
    public class EmployeeReferenceInformationModel:BaseDomainModel<EmployeeReferenceInformationModel>
    {

        public long EmployeeId { get; set; }
        public string Occupation { get; set; }
        public string Organization { get; set; }
        public string Designation { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public EmployeeModel Employee { get; set; }

    }
}
