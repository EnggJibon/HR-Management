//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ERP.DAL.Security
{
    using System;
    using System.Collections.Generic;
    
    public partial class ApplicationPolicy
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long ApplicationId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
    
        public virtual Application Application { get; set; }
    }
}
