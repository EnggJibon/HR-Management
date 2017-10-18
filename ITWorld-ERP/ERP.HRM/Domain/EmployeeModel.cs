using System;
using ERP.Utilities.Infrastructure;

namespace ERP.HRM.Domain
{
    public class EmployeeModel : BaseDomainModel<EmployeeModel>
    {
        public string EmployeeCode { get; set; }
        public long CategoryId { get; set; }
        public long DepartmentId { get; set; }
        public long DesignationId { get; set; }
        public long? SupervisorId { get; set; }
        public long? ApproverId { get; set; }
        public long RosterId { get; set; }
        public string JobLocation { get; set; }
        public string WorkLocation { get; set; }
        public string SalaryLocation { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public decimal? AppointmentLetterNo { get; set; }
        public DateTime AppointmentLetterIssueDate { get; set; }
        public DateTime AppointmentLetterJoinDate { get; set; }
        public decimal? JoiningLetterNo { get; set; }
        public DateTime JoiningDate { get; set; }
        public decimal? ProbationaryPeriodInDays { get; set; }
        public DateTime JobConfirmationDate { get; set; }
        public bool IsDualApprovalApplied { get; set; }

        public PersonalInformationModel PersonalInformation { get; set; }

        public string EmployeeName { get; set; }
        public string CategoryName { get; set; }
        public string DepartmentName { get; set; }
        public string DesignationName { get; set; }
        public string RosterName { get; set; }
    }
}
