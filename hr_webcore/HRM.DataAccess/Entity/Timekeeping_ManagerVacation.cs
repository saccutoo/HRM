using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public class Timekeeping_ManagerVacation
    {
        public int StaffID { set; get; }
        public string Fullname { set; get; }
        public int OrganizationUnitID { set; get; }
        public string OrganizationUnitName { set; get; }
        public double FurloughYear { set; get; }
        public double FurloughSeniority { set; get; }
        public double FurloughMore { set; get; }
        public string FurloughMoreNote { set; get; }
        public double ToTalFurloughYear { set; get; }
        public double FurloughUse { set; get; }
        public double FurloughOtherUse { set; get; }
        public string FurloughOtherUseNote { set; get; }
        public double FurloughLastYear { set; get; }
        public double FurloughLastYearUse { set; get; }
        public double TotalFurloughYearRemaining { set; get; }
        public double T1 { set; get; }
        public double T2 { set; get; }
        public double T3 { set; get; }
        public double T4 { set; get; }
        public double T5 { set; get; }
        public double T6 { set; get; }
        public double T7 { set; get; }
        public double T8 { set; get; }
        public double T9 { set; get; }
        public double T10 { set; get; }
        public double T11 { set; get; }
        public double T12 { set; get; }
    }
}

