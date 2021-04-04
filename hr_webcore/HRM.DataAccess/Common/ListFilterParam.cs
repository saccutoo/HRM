using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Common
{
    public class ListFilterParam
    {
        public string filter1 { set; get; }
        public string filter2 { set; get; }
        public string filter3 { set; get; }
        public string filter4 { set; get; }
        public string filter5 { set; get; }
        public string filter6 { set; get; }
        public string filter7 { set; get; }
        public string filter8 { set; get; }
        public string filter9 { set; get; }
        public string filter10 { set; get; }
        public string filter11 { set; get; }
        public string filter12 { set; get; }
        public string filter13 { set; get; }
        public DateTime? FromDate { set; get; }
        public DateTime? ToDate { set; get; }
        public int viewType { set; get; }
        public string StringFromDate { set; get; }
        public string StringToDate { set; get; }
        public DateTime? Period { get; set; } /*dành cho báo cáo chi tiêu tăng thêm*/
    }
}
