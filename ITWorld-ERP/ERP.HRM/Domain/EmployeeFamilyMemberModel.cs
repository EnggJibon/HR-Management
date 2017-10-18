using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.Utilities.Infrastructure;

namespace ERP.HRM.Domain
{
    public class EmployeeFamilyMemberModel : BaseDomainModel<EmployeeFamilyMemberModel>
    {

        public long EmployeeId { get; set; }
        public string Relation { get; set; }
        public string Name { get; set; }
        public string Qualification { get; set; }
        public string Occupation { get; set; }
        public string Organization { get; set; }
        public string Designation { get; set; }
        public string JobLocation { get; set; }
        public string NationalId { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Passport { get; set; }
        public long AddressId { get; set; }

        public EmployeeModel Employee { get; set; }


    }
}
