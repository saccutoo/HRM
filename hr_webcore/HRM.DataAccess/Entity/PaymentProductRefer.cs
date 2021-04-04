using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public class PaymentProductRefer
    {
        public int PaymentMarginID { set; get; }
        public string CustomerCode { set; get; }
        public int PaymentID { set; get; }
        public int ContractID { set; get; }
        public int StaffID { set; get; }
        public int SharedRate { set; get; }
        public DateTime ContractDate { set; get; }
        public double MarginRate { set; get; }
        public double Amount { set; get; }
        public int PaymentMarginTypeID { set; get; }
        public DateTime CreatedOn { set; get; }
        public string customername { set; get; }
        public string Website { set; get; }
        public string BD { set; get; }
        public string OrganizationUnitName { set; get; }
        public string ContractCode { set; get; }
        public string ContractValue { set; get; }
        public int PaymentNo { set; get; }
        public DateTime PaymentDate { set; get; }
        public bool Viewrateshare { set; get; }
        public string ExcepcionRequest { set; get; }
        public double AmountPayment { set; get; }
        public double DS_ContractValue_VATIncluded { set; get; }
        public string Email { set; get; }
        public double AccountConversion { set; get; }

    }
}
