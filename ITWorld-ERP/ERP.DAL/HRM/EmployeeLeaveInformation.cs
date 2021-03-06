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
    
    public partial class EmployeeLeaveInformation
    {
        public long Id { get; set; }
        public long EmployeeId { get; set; }
        public long AnnualHolidayCategoryId { get; set; }
        public long WeekendCategoryId { get; set; }
        public long LeavePolicyId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
    
        public virtual AnnualHolidayCategory AnnualHolidayCategory { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual LeavePolicy LeavePolicy { get; set; }
        public virtual WeekendCategory WeekendCategory { get; set; }
    }
}
