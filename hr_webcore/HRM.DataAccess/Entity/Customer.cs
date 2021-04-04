using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public int UserID { get; set; }
        public bool IsInvidualCustomer { get; set; }
        public string CustomerCode { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public string Website { get; set; }
        public string Fanpage { get; set; }
        public string BankInfo { get; set; }
        public Nullable<bool> Gender { get; set; }
        public string Email { get; set; }
        public string NickName { get; set; }
        public string HomePhone { get; set; }
        public string Mobile { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string Address { get; set; }
        public string TaxCode { get; set; }
        public string InvoiceAddress { get; set; }
        public Nullable<int> ClientSourceID { get; set; }
        public Nullable<int> IndustryID { get; set; }
        public string ClientNeedIDs { get; set; }
        public Nullable<int> FromMarketingCampaignID { get; set; }
        public int CustomerStatus { get; set; }
        public string Note { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<int> ClassifyId { get; set; }
        public Nullable<int> CustomerTypeID { get; set; }
        public Nullable<bool> IsExport { get; set; }
        public Nullable<long> EmployeesAmout { get; set; }
        public string IndustryNote { get; set; }
        public Nullable<int> ReferralCustomerID { get; set; }
        public string UTMSource { get; set; }
        public string UTMMedium { get; set; }
        public string UTMCampaign { get; set; }
        public string UTMTerm { get; set; }
        public string UTMContent { get; set; }
        public Nullable<int> RankID { get; set; }
        public Nullable<int> AutoAdsID { get; set; }
        public Nullable<int> StatusAutoads { get; set; }
        public Nullable<int> CustomerTranferID { get; set; }
        public Nullable<int> StatusUsedAutoads { get; set; }
        public Nullable<int> StatusIsNew { get; set; }

        public virtual Sec_User Sec_User { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Customer_MCCAccount> Customer_MCCAccount { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<CustomerContact> CustomerContacts { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Staff_Customer_CustomerManagementRole> Staff_Customer_CustomerManagementRole { get; set; }
    }
}
