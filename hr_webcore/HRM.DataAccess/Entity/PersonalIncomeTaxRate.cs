//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HRM.DataAccess.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class PersonalIncomeTaxRate
    {
        public int PersonalIncomeTaxRateId { get; set; }
        public string Name { get; set; }
        public int CreateBy { get; set; }
        public int Status { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime StartApply { get; set; }
        public System.DateTime EndApply { get; set; }
        public int RegionId { get; set; }
        public string Description { get; set; }
        public string CurrencyID { get; set; }
    }
}