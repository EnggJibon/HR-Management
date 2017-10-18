//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ERP.DAL.HRM
{
    using System;
    using System.Collections.Generic;
    
    public partial class PersonalInformation
    {
        public PersonalInformation()
        {
            this.Addresses = new HashSet<Address>();
        }
    
        public long Id { get; set; }
        public long EmployeeId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
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
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
    
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
