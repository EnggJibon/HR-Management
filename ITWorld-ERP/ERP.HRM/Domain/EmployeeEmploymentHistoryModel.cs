using System;
using ERP.Utilities.Infrastructure;

namespace ERP.HRM.Domain
{
    public class EmployeeEmploymentHistoryModel : BaseDomainModel<EmployeeEmploymentHistoryModel>
    {
        public int EmployeeId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Organization { get; set; }
        public string OrganizationType { get; set; }
        public string Position { get; set; }
        public string Responsibility { get; set; }
        public EmployeeModel Employee { get; set; }
    }
}
