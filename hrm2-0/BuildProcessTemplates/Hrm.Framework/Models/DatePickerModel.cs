using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hrm.Framework.Models
{
    public class DatePickerModel
    {
        public string Name { get; set; }
        public DateTime? Value { get; set; }
        public bool ChangeMonth { get; set; } = true;
        public bool ChangeYear { get; set; } = true;
        public string DateFormat { get; set; }
        public string PlaceHolder { get; set; }
        public bool IsAnimationLabel { get; set; }
        public string LabelName { get; set; }
        public bool IsRequired { get; set; }
        public string Vertical { get; set; } = "bottom";
        public string Attribute { get; set; } = string.Empty;
    }
}