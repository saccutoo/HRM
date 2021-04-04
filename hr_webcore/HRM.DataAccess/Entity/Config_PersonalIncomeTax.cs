using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public class Config_PersonalIncomeTax
    {
        public int ID { get; set; }
        public double? FromIncome { get; set; }
        public double? Tax { get; set; }
        public double? ProgressiveAmount { get; set; }
        public double? FullAmount { get; set; }
        public double? SubtractAmount { get; set; }
    }
}

