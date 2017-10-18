using ERP.Utilities.Infrastructure;

namespace ERP.HRM.Domain
{
    public class EmployeeHealthInformationModel : BaseDomainModel<EmployeeHealthInformationModel>
    {
        public long EmployeeId { get; set; }
        public string Age { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string BloodGroup { get; set; }
        public string Disease { get; set; }
        public string DiseaseNote { get; set; }
        public string DiseaseStatus { get; set; }
        public string IdentifiedHealthSymbol { get; set; }

        public EmployeeModel Employee { get; set; }
    }
}
