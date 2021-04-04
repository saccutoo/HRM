using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hrm.Framework.Models
{
    public class TextEditorModel :BaseModel
    {
        public string NameId { get; set; } = string.Empty;
        public string Name { get; set; }
        public string Value { get; set; }
        public string PlaceHolder { get; set; }
        public string Style { get; set; }
        public string Type { get; set; }
        public bool IsAnimationLabel { get; set; }
        public string LabelName { get; set; }
        public bool IsRequired { get; set; }
        public string OnChange { get; set; }
        public string OnFocus { get; set; }
        public bool isTimeFormat { get; set; } = true;
        public string OnBlur { get; set; }
        public string Attribute { get; set; } = string.Empty;

    }
}