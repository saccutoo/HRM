using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hrm.Api.Models
{
    public class WorkingdayCheckInOutBeetModel
    {
        public long person_id { get; set; }
        public long camera_id { get; set; }
        public long TypeHolidayId { get; set; }
        public string DBName { get; set; }
        public DateTime time { get; set; }
        public string CheckType { get; set; }
        public string emotion { get; set; }
        public string image { get; set; }
    }
}