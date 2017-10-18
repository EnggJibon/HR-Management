using System;
using System.Web;
using ERP.Utilities.Infrastructure;

namespace ERP.HRM.Domain
{
    public class PersonalInformationModel : BaseDomainModel<PersonalInformationModel>
    {
        public long EmployeeId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string Religion { get; set; }
        public string Nationality { get; set; }
        public string NationalId { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Passport { get; set; }
        public string Photo { get; set; }
        public string Signature { get; set; }
        public string MotherLanguage { get; set; }

        public AddressModel Address { get; set; }
    }
}
