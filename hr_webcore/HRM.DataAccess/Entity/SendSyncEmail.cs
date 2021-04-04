using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public partial class SendSyncEmail
    {
        public int Id { get; set; }
        public Nullable<int> DeptId { get; set; }
        public int? Status { get; set; }
        public string DeptEmail { get; set; }
        public string DeptName { get; set; }
        public int? DeptIdGG { get; set; }
        public int? DeptIdFB { get; set; }
        public int? DeptIdOTher { get; set; }
        public string[] ListDepartment { get; set; }
        public string SendTo { get; set; }
        public string AddTo { get; set; }
        public string AddCc { get; set; }
        public string Cc { get; set; }
        public int Type { get; set; }
        public int AuthorType { get; set; }
        public string StatusName { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public int? ServiceId { get; set; }
        public string Note { get; set; }
        public Nullable<bool> ServiceGG { get; set; }
        public Nullable<bool> ServiceFB { get; set; }
        public Nullable<bool> ServiceOS { get; set; }
        public int? QuotationserviceId { get; set; }
        public int? COAServiceID { get; set; }
        public int? COAInfoID { get; set; }
        public string ToDept { get; set; }
        public string JsonAttachment { get; set; }
        public int? StatusGG { get; set; }
        public int? StatusFB { get; set; }
        public int? StatusOther { get; set; }
    }
}
