using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Framework.Models
{
    public class ExportCellModel
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Value { get; set; }
        public float Width { get; set; }
        public string DataFormat { get; set; }
    }
    public class ExportRowModel
    {
        public List<ExportCellModel> Cells { get; set; }
    }
}
