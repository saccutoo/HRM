using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Framework.Models
{
    public class MonthYearSelectModel
    {
        public string Name { get; set; }
        public DateTime? Value { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string DateFormat { get; set; }
        public string PlaceHolder { get; set; }
        public bool IsAnimationLabel { get; set; }
        public string LabelName { get; set; }
        public int MaxYear { get; set; }
        public bool IsRequired { get; set; }
    }
}
