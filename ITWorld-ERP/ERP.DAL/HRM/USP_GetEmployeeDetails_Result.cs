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
    
    public partial class USP_GetEmployeeDetails_Result
    {
        public long Id { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public long DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public long DesignationId { get; set; }
        public string DesignationName { get; set; }
        public System.DateTime JoiningDate { get; set; }
        public bool IsActive { get; set; }
    }
}
