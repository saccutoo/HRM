using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public partial class SalaryDetail
    {
        public int SalaryDetailId { get; set; }
        public int SalaryId { get; set; }
        public int StaffID { get; set; }
        public int? Period { get; set; }
        public DateTime? CreateDate { get; set; }
        public int Status { get; set; }
        public int? CreateBy { get; set; }
        public double? WorkdayPrevious { get; set; }
        public double? WorkdayAfter { get; set; }
        public double? Workday { get; set; }
        public double? TimeLate { get; set; }
        public double? Standardworkday { get; set; }
        public double? BasicSalary { get; set; }
        public double? Bonus { get; set; }
        public double? SeniorityPay { get; set; }
        public double? AllowancesLaptop { get; set; }
        public double? AllowancesPhone { get; set; }
        public double? BDOAllowances { get; set; }
        public double? OtherAllowances { get; set; }
        public string AllowancesNote { get; set; }
        public double? IncomePerDay { get; set; }
        public double? SalarylastMonth { get; set; }
        public double? Commission { get; set; }
        public double? OtherBonus { get; set; }
        public string BonusNote { get; set; }
        public double? PersonalIncomeTax { get; set; }
        public double? AmountInsurance { get; set; }
        public double? C_Socialinsurance { get; set; }
        public double? C_Healthinsurance { get; set; }
        public double? C_Unemploymentinsurance { get; set; }
        public double? C_KPCD { get; set; }
        public double? P_Socialinsurance { get; set; }
        public double? P_Healthinsurance { get; set; }
        public double? P_Unemploymentinsurance { get; set; }
        public double? P_KPCD { get; set; }
        public double? Deductionitself { get; set; }
        public int? NumRelatedperson { get; set; }
        public double? Deduction { get; set; }
        public double? Foodexpenses { get; set; }
        public double? Taxableincome { get; set; }
        public double? Nontaxableincome { get; set; }
        public string NontaxableincomeNote { get; set; }
        public double? UnionFund { get; set; }
        public double? TardinessReduction { get; set; }
        public double? QCBonus { get; set; }
        public double? AdvancePayment { get; set; }
        public double? OtherReduction { get; set; }
        public string ReductionNote { get; set; }
        public double? TotalIncome { get; set; }
        public double? TotalReduction { get; set; }
        public double? Netsalary { get; set; }
        public double? Decemberbonus { get; set; }
        public double? Margincompensation { get; set; }
        public string MargincompensationNote { get; set; }
        public double? PaymentPeriod1 { get; set; }
        public double? PaymentPeriod2 { get; set; }
        public double? PaymentPeriod3 { get; set; }
        public double? PaymentPeriod4 { get; set; }
        public double? PaymentPeriod5 { get; set; }
        public double? PaymentNextMonth { get; set; }

    }
}
