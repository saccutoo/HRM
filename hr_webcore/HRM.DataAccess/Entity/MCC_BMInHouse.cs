using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public class MCC_BMInHouse
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public Int64? BM_MCCId { get; set; }
        public int AccountType { get; set; }
        public bool? IsPartner { get; set; }
        public int? Type { get; set; }
        public string AccountName { get; set; }
        public string TypeName { get; set; }
       
    }
}
