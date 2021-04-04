using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository.Entity
{
    public class StaffCertificateEntity : BaseEntity
    {
        public long StaffId { get; set; }
        public long TypeId { get; set; }
        public DateTime? IssuedDate { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public long RankId { get; set; }
        public string IssuedBy { get; set; }
        public decimal Point { get; set; }
        public string Note { get; set; }
    }
}
