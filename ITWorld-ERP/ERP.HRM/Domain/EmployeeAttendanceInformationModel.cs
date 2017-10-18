using System;
using ERP.Utilities.Infrastructure;

namespace ERP.HRM.Domain
{
    public class EmployeeAttendanceInformationModel : BaseDomainModel<EmployeeAttendanceInformationModel>
    {
        public long EmployeeId { get; set; }
        public DateTime Date { get; set; }
        public DateTime? PunchInTime { get; set; }
        public DateTime? PunchOutTime { get; set; }

        public string EmployeeCode { get; set; }
        public long Month { get; set; }
        public long Year { get; set; }

        public EmployeeModel Employee { get; set; }
    }
}
