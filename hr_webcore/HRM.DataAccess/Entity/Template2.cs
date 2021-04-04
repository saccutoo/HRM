using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public class Template2
    {
        public int AutoID { get; set; }
        public string Name { get; set; }
        public string NameEN { get; set; }
        public string Value { get; set; }
        public int Type { get; set; }
        public double? Income { set; get; }
        public double? Deduct { set; get; }
        public int? DisplayType { set; get; }
        public string DisplayValue { set; get; }
        public double? DisplayValueFloat { set; get; }
        public string Align { set; get; }
        public string Css { set; get; }
        public int OrderNo { set; get; }
        public string Hide { set; get; }
        public int? Colspan { set; get; } 
        public int? DataFormat { set; get; }
        public string CustomHTML { set; get; }
        public string HideRow { set; get; }
        public string TypeName { set; get; }
        public string DisplayTypeName { set; get; }

        public string DataFomatName { set; get; }
        public string FormatTypeName { set; get; }
    }
}
