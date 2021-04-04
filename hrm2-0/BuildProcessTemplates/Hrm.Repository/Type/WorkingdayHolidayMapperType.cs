using System;
using System.Collections.Generic;
using Hrm.Repository.Entity;

namespace Hrm.Repository.Type
{
    public class WorkingdayHolidayMapperType : IUserDefinedType
    {
        public long Id { get; set; }
        public long HolidayId { get; set; }
        public DateTime? Date { get; set; }
        public long DesistRegimeId { get; set; }
    }
}
