﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Framework.Models
{
    public class WorkingdayShiftDetailModel : BaseModel
    {
        public long ShifId { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public decimal TotalTime { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public long Status { get; set; }
        public decimal OvertimeMinRequire { get; set; }
        public decimal SalaryRatio { get; set; }
        public bool IsLunchBreak { get; set; }
        public bool IsOverTime { get; set; }
        public bool T2 { get; set; }
        public bool T3 { get; set; }
        public bool T4 { get; set; }
        public bool T5 { get; set; }
        public bool T6 { get; set; }
        public bool T7 { get; set; }
        public bool CN { get; set; }
        public int Index { get; set; }

    }
}