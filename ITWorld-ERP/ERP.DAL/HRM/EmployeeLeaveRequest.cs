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
    
    public partial class EmployeeLeaveRequest
    {
        public long Id { get; set; }
        public long EmployeeId { get; set; }
        public System.DateTime RequestDate { get; set; }
        public long LeaveTypeId { get; set; }
        public System.DateTime LeaveFrom { get; set; }
        public System.DateTime LeaveTo { get; set; }
        public byte DaysCount { get; set; }
        public string Purpose { get; set; }
        public bool IsHalfDay { get; set; }
        public bool IsAdjustment { get; set; }
        public Nullable<System.DateTime> AdjustmentDate { get; set; }
        public long ApprovalStatusId { get; set; }
        public Nullable<System.DateTime> ApprovalDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual LeaveApprovalStatu LeaveApprovalStatu { get; set; }
        public virtual LeaveType LeaveType { get; set; }
    }
}