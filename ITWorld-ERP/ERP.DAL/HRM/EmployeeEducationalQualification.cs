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
    
    public partial class EmployeeEducationalQualification
    {
        public long Id { get; set; }
        public long EmployeeId { get; set; }
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
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
    
        public virtual Employee Employee { get; set; }
    }
}
