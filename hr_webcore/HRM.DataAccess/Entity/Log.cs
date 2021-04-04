using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.DAL
{
    public class Log
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserIp { get; set; }
        public string Controller { get; set; }
        public string UserAction { get; set; }
        public DateTime? Date { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
        public string StaffName { get; set; }

    }
}
