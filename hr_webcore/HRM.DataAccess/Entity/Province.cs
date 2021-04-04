using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public partial class Province
    {
        public int ProvinceID { set; get; }
        public int CountryID { set; get; }
        public string Name { set; get; }
        public string NameEN { set; get; }
        public int Status { set; get; }
        public string CountryName { set; get; }
        public string StatusName { set; get; }
    }
}
