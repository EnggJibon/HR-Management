using ERP.Utilities.Infrastructure;

namespace ERP.HRM.Domain
{
    public class EmployeeEducationalQualificationModel : BaseDomainModel<EmployeeEducationalQualificationModel>
    {
        public int EmployeeId { get; set; }
        public string Certification { get; set; }
        public string Subject { get; set; }
        public string Institute { get; set; }
        public string Board { get; set; }
        public string Country { get; set; }
        public string Duration { get; set; }
        public string PassingYear { get; set; }
        public string DivisionClassGPA { get; set; }
        public string Marks { get; set; }
        public string OutOf { get; set; }

        public EmployeeModel Employee { get; set; }
    }
}
