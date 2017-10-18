using System;
using ERP.Utilities.Infrastructure;

namespace ERP.HRM.Domain
{
     public class EmployeeNomineeInformationModel:BaseDomainModel<EmployeeNomineeInformationModel>
     {
        public long EmployeeId { get; set; }
        public string Relation { get; set; }
        public string Name { get; set; }
        public string Occupation { get; set; }
        public string Organization { get; set; }
        public string NationalId { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Passport { get; set; }
        public Nullable<long> AddressId { get; set; }
        public string BenefitName { get; set; }
        public Nullable<decimal> Percentage { get; set; }

         public EmployeeModel Employee { get; set; }




     }
}
