//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ERP.DAL.Common
{
    using System;
    using System.Collections.Generic;
    
    public partial class Country
    {
        public long Id { get; set; }
        public string CountryName { get; set; }
        public string CountryCodeAlpha2 { get; set; }
        public string CountryCodeAlpha3 { get; set; }
        public string CountryCodeNumeric { get; set; }
        public string ISOCode { get; set; }
        public bool IsActive { get; set; }
    }
}