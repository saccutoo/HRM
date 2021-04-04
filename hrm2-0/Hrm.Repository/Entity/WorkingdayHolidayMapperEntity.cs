using System;
using System.Collections.Generic;
using Hrm.Repository.Entity;

namespace Hrm.Repository.Entity
{
    public class WorkingdayHolidayMapperEntity :BaseEntity
    {
        public long HolidayId { get; set; }
        public DateTime? Date { get; set; }
        public long DesistRegimeId { get; set; }
    }
}
