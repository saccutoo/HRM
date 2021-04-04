using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace Hrm.Framework.Models
{
    public class DropdownListModel : BaseModel
    {
        public string Name { get; set; }
        public string Style { get; set; }
        public string ValueField { get; set; }
        public string DisplayField { get; set; }
        public bool IsUseIcon { get; set; } = false;
        public bool IsUseImage { get; set; } = false;
        public List<dynamic> Data { get; set; }
        public string Event { get; set; }
        public string NgClick { get; set; }
        public int CountOption { get; set; }
        public string LabelName { get; set; }
        public bool IsAnimationLabel { get; set; }
        public bool IsRequired { get; set; }
        public string OnChange { get; set; }
        public bool SearchField { get; set; }
        public string Value { get; set; }
        public bool IsMultipleLanguage { get; set; } = false;
        public string PropertyName { get; set; }
        public string PropertyId { get; set; }
        public bool Disabled { get; set; } = false;
        public bool IsSelectedEmpty { get; set; } = true;
        public string SelectedValue { get; set; } = string.Empty;
        public bool IsTagsInput { get; set; } = false;
        public List<string> ValueMultiSelect { get; set; } = new List<string>();
        public string Attribute { get; set; }
    }
}