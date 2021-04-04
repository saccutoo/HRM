using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public partial class PaymentProduct
    {
        public string StatusName { get; set; }
        public string CustomerName { get; set; }
        public string OrganizationUnitName { get; set; }
        public string ProductName { get; set; }
        public string BDName { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }

        public string UserLoginName { set; get; }
        public double AccountConversion { set; get; }
      
    }
}