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
    
    public partial class AnnualHolidayCategory
    {
        public AnnualHolidayCategory()
        {
            this.AnnualHolidays = new HashSet<AnnualHoliday>();
            this.EmployeeLeaveInformations = new HashSet<EmployeeLeaveInformation>();
        }
    
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Year { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
    
        public virtual ICollection<AnnualHoliday> AnnualHolidays { get; set; }
        public virtual ICollection<EmployeeLeaveInformation> EmployeeLeaveInformations { get; set; }
    }
}
