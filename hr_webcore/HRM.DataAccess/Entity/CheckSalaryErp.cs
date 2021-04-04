using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public class CheckSalaryErp
    {
        public int staffid { set; get; }
        public string EmailCompany { set; get; }
        public string StaffName { set; get; }
        public int OrganizationUnitID { set; get; }
        public string StatusName { set; get; }
        public string OrganizationUnit { set; get; }
        public DateTime? qtctStartDate { set; get; }
        public DateTime? qtctEndDate { set; get; }
        public double? qtctBasicPay { set; get; }
        public double? qtctEfficiencyBonus { set; get; }

        public string qtctPolicy { set; get; }
        public string qtctContractType { set; get; }
        public string qtctStaffLevel { set; get; }
        public int qtctWPID { set; get; }
        public DateTime? qtct1StartDate { set; get; }
        public DateTime? qtct1EndDate { set; get; }
        public double? qtct1BasicPay { set; get; }
        public double? qtct1EfficiencyBonus { set; get; }
        public string qtct1Policy { set; get; }
        public string qtct1ContractType { set; get; }
        public string qtct1StaffLevel { set; get; }
        public int qtct1WPID { set; get; }
        public double? bhBasicSalary { set; get; }
        public DateTime? bhFromMonth { set; get; }
        public DateTime? bhToMonth { set; get; }
        public string trangthaibh { set; get; }
        public double? bh1BasicSalary { set; get; }
        public DateTime? bh1FromMonth { set; get; }
        public DateTime? bh1ToMonth { set; get; }
        public string trangthaibh1 { set; get; }
        public string Position { set; get; }

        public double? pclAmount { set; get; }
        public DateTime? pclStartDate { set; get; }
        public  DateTime? pclEndDate { set; get; }
        public double? pcl1Amount { set; get; }
        public DateTime? pcl1StartDate { set; get; }
        public DateTime? pcl1EndDate { set; get; }

        public double? pctnAmount { set; get; }
        public DateTime? pctnStartDate { set; get; }
        public DateTime? pctnEndDate { set; get; }
        public double? pctn1Amount { set; get; }
        public DateTime? pctn1StartDate { set; get; }
        public DateTime? pctn1EndDate { set; get; }

        public double? pcgxAmount { set; get; }
        public DateTime? pcgxStartDate { set; get; }
        public DateTime? pcgxEndDate { set; get; }
        public double? pcgx1Amount { set; get; }
        public DateTime? pcgx1StartDate { set; get; }
        public DateTime? pcgx1EndDate { set; get; }
        
        public double? pcadminAmount { set; get; }
        public DateTime? pcadminStartDate { set; get; }
        public DateTime? pcadminEndDate { set; get; }
        public double? pcadmin1Amount { set; get; }
        public DateTime? pcadmin1StartDate { set; get; }
        public DateTime? pcadmin1EndDate { set; get; }

        public double? pcrrAmount { set; get; }
        public DateTime? pcrrStartDate { set; get; }
        public DateTime? pcrrEndDate { set; get; }
        public double? pcrr1Amount { set; get; }
        public DateTime? pcrr1StartDate { set; get; }
        public DateTime? pcrr1EndDate { set; get; }

        public double? pcnhAmount { set; get; }
        public DateTime? pcnhStartDate { set; get; }
        public DateTime? pcnhEndDate { set; get; }
        public double? pcnh1Amount { set; get; }
        public DateTime? pcnh1StartDate { set; get; }
        public DateTime? pcnh1EndDate { set; get; }

        public double? pccvAmount { set; get; }
        public DateTime? pccvStartDate { set; get; }
        public DateTime? pccvEndDate { set; get; }
        public double? pccv1Amount { set; get; }
        public DateTime? pccv1StartDate { set; get; }
        public DateTime? pccv1EndDate { set; get; }

        public Int32? pcbdAmount { set; get; }
        public DateTime? pcbdStartDate { set; get; }
        public DateTime? pcbdEndDate { set; get; }
        public Int32? pcbd1Amount { set; get; }
        public DateTime? pcbd1StartDate { set; get; }
        public DateTime? pcbd1EndDate { set; get; }

        public string OfficePosition { set; get; }
        public int WorkingDayMachineID { set; get; }
        public string WorkingDayMachinename { set; get; }
        public int WorkingManagerID { set; get; }
        public string WorkingManagerName { set; get; }
        public int WorkingHRID { set; get; }
        public string WorkingHRManagerName { set; get; }
        public string OfficeName { set; get; }
        public string ContractType { set; get; }
        public string BankNumber { get; set; }
        public string HoldSaraly { get; set; }

    }
}
