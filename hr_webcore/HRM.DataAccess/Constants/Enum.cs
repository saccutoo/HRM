


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Constants
{
    public partial class Constant
    {
        /// <summary>
        /// Loại tiền tệ
        /// </summary>
        public enum numCurrency
        {
            [Description("VND")]
            VND = 194,

            [Description("USD")]
            USD = 195,
        }
        /// <summary>
        /// Loại giá vốn
        /// </summary>
        public enum numCostType
        {
            [Description("Inhouse")]
            Inhouse = 1345,
            [Description("Outsourcing")]
            Outsourcing = 1346,
            [Description("Automatic")]
            Automatic = 1347,
            [Description("SocialSend")]
            SocialSend = 1374
        }
        /// <summary>
        /// trạng thái giá vốn
        /// </summary>
        public enum numCostStatus
        {
            [Description("Confirmed")]
            Confirmed = 1367,
            [Description("Undefined")]
            Undefined = 1365,
            [Description("Automatic")]
            Automatic = 1364,
            [Description("WaitConfirm")]
            WaitConfirm = 1366,
            [Description("NotConfirm")]
            NotConfirm = 1368
        }
        /// <summary>
        /// Action
        /// </summary>
        public enum numMenuAction
        {
            Quotation = 61,
            QuotationEdit = 90,
            QuotationApproval = 92,
            Contract = 66,
            ContractEdit = 94,
            ContractApproval = 96,
            ContractApprovalDeploy = 199,
            ContractRejectDeploy = 200,
            ContractAddService = 207,
            ContractReject = 97,
            ErpCampaignEdit = 99,
            ErpCampaignAddCare = 113,
            ErpCampaignEditCare = 114,
            ErpCampaignAddCost = 110,
            ErpCampaignApprovalCost = 132,
            PaymentView = 122,
            PaymentEdit = 214,
            DiscountEdit = 158,
            ApprovalCommitment = 232,


        }

        /// <summary>
        /// Controller
        /// </summary>
        public enum numController
        {
            Quotation = 10,
            Contract = 13,
            ErpCampaign = 17,
            Customer = 9,
            Payment = 16,
            Discount = 18
        }
        public enum numRefundStatus
        {
            RefundProcessStatus = 1721,
            RefundClosedStatus = 1722
        }
        /// <summary>
        /// Nhắm chọn
        /// </summary>
        public enum numTargeting
        {
            Adword = 542,
            Website = 543,
            Theme = 544,
            Hobby = 545,
            Remarketing = 546,
            Age = 547,
            Gender = 548,
            Other = 549,
            AddressMap = 550,
            PhoneMap = 551,
        }

        /// <summary>
        /// Gói quảng cáo
        /// </summary>
        public enum numServicePackage
        {
            [Description("GDN")]
            GDN = 1,

            [Description("GS")]
            GS = 4,
        }

        /// <summary>
        /// Dịch vụ
        /// </summary>
        public enum numService
        {
            [Description("Google")]
            Google = 1,
            [Description("Facebook")]
            Facebook = 2,
            [Description("Other")]
            Other = 3
        }

        /// <summary>
        /// Hình thức tính phí
        /// </summary>
        public enum numBiddingMethod
        {
            [Description("CPC")]
            CPC = 534,

            [Description("CPI")]
            CPI = 536,
        }

        /// <summary>
        /// Trạng thái hợp đồng
        /// </summary>
        public enum numStatus
        {
            [Description("Bản Nháp")]
            Draft = 119,

            [Description("Chờ duyệt")]
            PendingApproval = 120,

            [Description("Không duyệt")]
            Notapproved = 128,
            [Description("Chờ QL duyệt")]
            PendingApprovalFromManagers = 121,
            [Description("Chờ QL không duyệt")]
            PendingDisapprovalFromManagers = 122,
            [Description("Đã duyệt")]
            Approved = 123,
            [Description("Đã chuyển hợp đồng")]
            ContractsGenerated = 124,
            [Description("Huy")]
            Cancel = 125,
            [Description("Kết thúc")]
            Ended = 126,
            [Description("Đang triển khai")]
            Ongoing = 127,
            [Description("Chờ triển khai")]
            PendingSetupRequest = 129,
            [Description("Đủ điều kiện - chuẩn")]
            ActiveStandard = 134,
            [Description("Đủ điều kiện - chậm KPI")]
            ActiveProlonged = 135,
            [Description("Media chờ triển khai")]
            MediaPending = 131,
            [Description("Media đang triển khai")]
            MediaOngoing = 132,
            [Description("Account viết lời quảng cáo")]
            AccountWringtingAds = 130,
            [Description("Account triển khai")]
            AccountOnGoing = 142,
            [Description("Đã kích hoạt chiến dịch")]
            ActiveCampaign = 133,
            [Description("Tạm dừng")]
            Paused = 136,
            [Description("Đang xem xét")]
            PendingReview = 137,
            [Description("Đã kết thúc - chuẩn")]
            EndedStandard = 139,
            [Description("Đã kết thúc - khác")]
            EndedOther = 140,
            [Description("Có cập nhật")]
            HasUpdated = 1559
        }

        /// <summary>
        /// Quyền người dùng
        /// </summary>
        public enum numRole
        {
            BD = 3,
            Media = 4,
            Account = 5,
            Social = 6,
            Accountant = 11,

            Admin = 1,
            PlanSocial = 7,
            MediaPerformance = 26,
            CSAutoAds = 27,
            Marketing = 28,
            BDAutoAds = 29,
            ConfirmInfo = 30,
            Design = 9

        }

        /// <summary>
        /// Danh sách danh mục
        /// </summary>
        public enum numGlobalListParent
        {
            AP_Genaral = 0,
            AP_StatusShareRate = 80,
            AP_CostType = 1344,
            AP_ScheduleType = 81,
            AP_STAFFSTATUS = 91,
            AP_OFFICE = 20,
            AP_TermType = 72,
            AP_FontStyle = 73,
            AP_StatusPayment = 60,
            AP_StatusCostType = 1363,
            AP_PaymentType = 1308,
            AP_INVOICE = 59,
            //  AP_STAFFSTATUS = 91,
            //  AP_OFFICE = 20,
            AP_LEVEL = 21,
            AP_CURRENCYTYPE = 3,
            AP_STATUS = 2,
            AP_SERVICE = 1265,
            AP_SERVICEPACKAGE = 2,
            AP_LANGUAGE = 1,
            AP_VAT = 35,
            AP_PAYFORMS = 25,
            AP_PROMOTIONAL = 33,
            AP_ADFORMAT = 4,
            AP_MANAGEMENTFORMS = 5,
            AP_ACCOUNTTYPE = 6,
            AP_ADFORMS = 8,
            AP_GOOGLEDMAIN = 23,
            AP_LOCATION = 24,
            AP_FREEFORM = 11,
            AP_CLOSECHOOSE = 12,
            AP_SYSTEM = 27,
            AP_DEVICE = 28,
            AP_POSITIONDISPLAY = 29,
            AP_ADFORMSFB = 39,
            AP_FREEFORMFB = 40,
            AP_WEBSITE = 38,
            AP_STATUSCOMMIT = 48,
            AP_THEMS = 50,
            AP_GENDER = 30,
            AP_TIMEUNIT = 46,
            AP_TYPEKPI = 34,
            AP_AGE = 51,
            AP_DS = 1561,
            AP_ACCEPTANCE = 52,
            AP_STATUSACCEPTANCE = 53,
            AP_PAY = 46,
            AP_CUSTOMERACTIVITYSTATUS = 1526,
            STATUS_CUSTOMER = 41, //changed
            RESOURCE_CUSTOMER = 37, //changed
            SUBSECTORS_CUSTOMER = 36, //changed
            MARKETING_CUSTOMER = 33, //changed
            TYPEDETAIL_CUSTOMER = 29, //not use
            NEED_CUSTOMER = 38, //changed
            CUSTTYPE_CUSTOMER = 58, //changed
            CONTACTTYPE_CUSTOMER = 32, //changed
            ADDRESSTYPE_CUSTOMER = 17,
            COUNTRY_CUSTOMER = 30,
            SEX_CUSTOMER = 30, //changed
            MARITAL_CUSTOMER = 31,
            CURRENCY_TYPE = 1,
            STATUS_COMMITMENT = 34,
            TypeOutsource = 150,
            KPICommit = 151,
            ShareCareType = 154,
            ShareCareStatus = 155,
            Language = 1,
            MANAGERMENT_CUSTOMER = 1269, //changed
            CT_STATUS = 2, //changed Trạng thái hợp đồng
            CT_LANGUAGE = 1, //changed Ngôn ngữ hợp đồng
            CT_VAT = 49, //changed Vat hợp đồng
            CT_PAYFORMS = 39, //changed Hình thức thanh toán hợp đồng
            CT_CURRENCYTYPE = 3, //changed Kiểu tiền tệ hợp đồng

            /// <summary>
            /// Giá trị công thức tính phí ghi kênh
            /// </summary>
            TiLeGhiKenh = 915,
            NETWORK = 1423,
            JOBS = 1466,
            MaritalStatus = 45, // Trạng thái hôn nhân
            CustomerType = 1542,//Phân loại khách hàng
            PercenDayWork = 1589,
            DeptPerformance = 1650,
            CompanyType = 1622,
            StaffStatus = 55,
            AbilityDeal = 1548, // Khả năng chốt deal

            DataTypeFilterGrid = 1760, // Kiểu dữ liệu trong Grid

            AccountType = 1707,
            MccAccountStatus = 1703,
            TypeStatus = 1723,
            RankId = 1726,
            PromotionStatus = 88,
            PromotionTypeUsed = 1736,
            STATUS_QUALITYREQUEST = 1719,
            StatusCustomerAutoads = 1785,
            ContractType = 1949,
            StatusApproveContractDiscountInfo = 1812,//Trạng thái duyệt thông tin chiết khấu
            TypeContractDiscountInfo = 1808,//Kiểu nhận chi chiết khấu
            EmailAccountApprovalCommisionInfo = 1840,
            EmailQC = 1844,
            Statuscontract = 2106,
            Config_Insurance_Status = 2014,
            Config_Insurance_Type = 2006,
        }

        /// <summary>
        /// ngôn ngữ
        /// </summary>
        public enum numLanguage
        {
            EN = 4,
            VN = 5
        }
        public enum numNotifyType
        {
            NotifyType = 1972,
            KPI = 1973
        }
        /// <summary>
        /// Trạng thái khách hàng
        /// </summary>
        public enum numCustomerStatus
        {
            LienHeBaoGia = 691,
            DuyetHopDong = 692,
            TrienKhai = 693,
            DaDung = 694,
            ChoDuyet = 695

        }

        /// <summary>
        /// Loại quyền quản lý khách hàng
        /// </summary>
        public enum numManagerCustomerType
        {
            Master = 1271,
            Member = 1272,
            Report = 1273
        }

        /// <summary>
        /// Loai Lay reference quotation detail
        /// </summary>
        public enum numReferenceData
        {
            AdsPackageID = 1,
            AdsAccountTypeID = 2,
            BiddingMethodID = 3,
            AdsPositionCommitmentIDs = 5,
            GoogleDomainIDs = 4,
            TypeKPIId = 6,
            AdsFormatIDs = 7,
            Targetting = 8,
            TypeKPIIdOther = 9
        }

        public enum numAdsPackageID
        {
            AdsPackageID_CAMKET = 245,
            AdsPackageID_QLTK = 246,
            AdsPackageID_QLTK_NOIBO = 247,
            AdsPackageID_THUE_TK = 248,
            AdsPackageID_THUE_TK_DB = 249,
            AdsPackageID_NOIBO_MARKETING = 1343
        }

        public enum numCOATypeId
        {
            /// <summary>
            /// Hợp đồng
            /// </summary>
            Contract = 1289,

            /// <summary>
            /// Phụ lục
            /// </summary>
            Appendix = 1291,

            /// <summary>
            /// Nghiệm thu thanh lý
            /// </summary>
            Acceptance = 1292,

            /// <summary>
            /// Nghiệm thu theo lần
            /// </summary>
            AcceptanceTimes = 1294,
            /// <summary>
            /// Phụ lục gia hạn
            /// </summary>
            AppendixExtension = 1387
        }

        public enum numNotificationTypeID
        {
            // Báo giá gửi chờ duyệt

            // Báo giá đã duyệt

            // Báo giá gửi đến quản lý - chờ duyệt

            // Báo giá gửi đến quản lý - không duyệt

            // HĐ gửi chờ duyệt

            // HĐ đã duyệt

            // HĐ gửi đến quản lý - chờ duyệt

            // HĐ gửi đến quản lý - không duyệt

            // YCTK chờ duyệt

            // YCTK không duyệt

            //


            // Cảnh báo

            // Chờ duyệt hợp đồng, 

            // chờ duyệt phụ lục

            // 
        }

        public enum numNotificationStatus
        {
            Unread = 1354,
            Read = 1357
        }

        public enum numPaymentMethodId
        {
            /// <summary>
            /// Chuyển khoản
            /// </summary>
            Transfer = 658,
            /// <summary>
            /// Tiền mặt
            /// </summary>
            Cash = 659,
            /// <summary>
            /// Chuyển khoản hoặc tiền mặt
            /// </summary>
            TranferOrCash = 660
        }


        /// <summary>
        /// Loại thanh toán
        /// </summary>
        public enum numContractPaymentType
        {
            Payment = 1309,//TT
            Discount = 1311//CK
        }

        public enum numCompanyType
        {

            Ads = 1624,
            Bookin = 1625,
            Fast = 1626,
            Alibaba = 1627,
            Other = 1628,
            Cloud = 1641,
            AutoAds = 1648
        }

        public enum numCustomerActivityStatus
        {
            Plan = 1527,
            Pitching = 1530,
            Meeting = 1531,
            Quoting = 1532,
            Contract = 1535,
            Close = 1536,
            Orders = 1629,
            UseTest = 1678
        }

        public enum numContractPaymentStatus
        {
            Unpaid = 887,//TT
            Confirm = 888,//CK
            Wait = 889,
            Cancel = 1411,
            PendingConfirInfomation = 1828,// Chờ Account xác nhận thông tin
            PendingConfirInfomationBOD = 1847 // Chờ BOD xác nhận thông tin
        }

        public enum numContractinvoiceStatus
        {
            Pending = 883,//TT
            Approved = 884,//CK
            CancelRequest = 885,
            Cancel = 886
        }


        public enum numManagerForm
        {

            CAMKET = 245,
            QLTK = 246,
            QLTK_NOIBO = 247,
            THUE_TK = 248,
            THUE_TK_DB = 249,
            NOIBO_MARKETING = 1343,
        }

        public enum numAdsAccountTypeID
        {
            INVOICE = 219,
            THE_KH = 220,
            THE_NOVA = 221
        }

        public enum numSendSyncType
        {
            /// <summary>
            /// Sau khi ký Hợp đồng
            /// </summary>
            SAU_KHI_KY_HOP_DONG = 1186,
            THEO_NGAY_DU_KIEN = 1297,
            DA_THANH_TOAN = 888,
            THEO_DOI_CAM_KET = 760,
            DANG_CAM_KET = 758,
            TRUOC_KHI_HET_NGAN_SACH = 1520,
            SAU_KHI_CHAY_DUOC_SO_KPI_LA = 1518,

            SAU_KHI_KICH_HOAT = 1187,
            SAU_KHI_KY_PHU_LUC = 1188,
            SAU_KHI_NGHIEM_THU_THANH_LY = 1189,
            SAU_KHI_CHAY_KPI = 1190,
            THANH_TOAN_SAU_KHI_HET_NGAN_SACH_DOT = 1191,

        }

        public enum numStatusStaff
        {


            /// <summary>
            /// Không hoạt đông
            /// </summary>
            Inactive = 878,
            /// <summary>
            /// Đang hoạt động
            /// </summary>
            Activity = 879,
            /// <summary>
            /// Mới nghỉ
            /// </summary>
            NewLeave = 880
        }
        public enum numStatusCommitment
        {
            /// <summary>
            /// Đang cam kết
            /// </summary>
            Commitment = 758,
            /// <summary>
            ///Chờ duyệt cam kết
            /// </summary>
            PendingApproval = 759,
            /// <summary>
            /// Theo dõi cam kết
            /// </summary>
            FollowCommitment = 760,
            /// <summary>
            /// Không duyệt cam kết
            /// </summary>
            NotApprovalCommitment = 761,
            /// <summary>
            /// Đang cam kết - hết hạn mức
            /// </summary>
            CommitmentOutOffLimit = 762,
            /// <summary>
            /// Đã trừ thưởng
            /// </summary>
            DeductedBonuses = 763,
            /// <summary>
            /// Chờ quản lý duyệt
            /// </summary>
            PendingManager = 1968,
            /// <summary>
            /// Chờ BDD duyệt
            /// </summary>
            PendingBDD = 1969,
            /// <summary>
            /// Chờ BOD duyệt
            /// </summary>
            PendingBOD = 1970

        }
        public enum numVat
        {
            /// <summary>
            /// Không VAT
            /// </summary>
            VAT_0 = 764,
            /// <summary>
            /// 10% Vat
            /// </summary>
            VAT_10 = 765,
            VAT_12 = 1681,
        }


        public enum numTimekeeping
        {
            Loai_Cham_Cong = 84,
            Trang_Thai_Cham_Cong = 85,
            Ly_Do_Bo_Sung = 86,
            Muc_Dich = 1887
        }

        public enum numTypeVacation
        {
            Di_Muon_Ve_Som = 1,
            Nghi_Phep = 2,
            Khong_Luong = 3,
            Bo_Sung_Cong = 4,
            Bo_Sung_Cong_Them = 5
        }
        public enum numReasonTypeVacation
        {
            Gap_Khach_Hang = 1213
        }

        public enum numHRPermistion
        {
            Chot_Con_Cuoi = 1616,
            Cap_Nhat_Phep = 1617,
            Xem_Toan_Bo_Nhan_Su = 1621
        }

        public enum numStatusTimekeeping
        {
            Cho_QL_Duyet = 1,
            Cho_HR_Duyet = 3,
            QL_Khong_Duyet = 2,
            HR_Khong_Duyet = 4,
            HR_Da_Duyet = 5,
            Cho_CCO_Duyet = 6,
            CCO_Khong_Duyet = 7,
            Cho_Admin_Duyet = 8,
            Admin_Khong_Duyet = 9,
            Admin_Da_Duyet = 10,
            Ban_Nhap = 0
        }

        public enum numFilterType
        {
            Text = 1,
            Number = 2,
            DateTime = 3,
            ComboBox = 4,
            ComboBoxLike = 5
        }
        public enum numOfficePosition
        {
            CCO = 262,
            ViceCEO = 253,
            Director = 252,
            Truong_phong = 254,
            Truong_nhom = 256,
            Pho_phong = 255
        }
        public enum AutoAdsApiType
        {
            CreateCustomer = 1,
            CreateRefund = 2,
            UpdateRefund = 4,
            CreateDetailRefund = 5,
            UpdateDetailRefund = 6,
            UptWebsite = 3,
            UpdateCustomer = 7
        }


        public enum ApiMethod
        {
            PutAsJsonAsync,
            PostAsJsonAsync
        }


        public enum EAction
        {
            Index = 1,
            Get = 2,
            Add = 3,
            Edit = 4,
            Delete = 5,
            Excel = 6,
            Approval = 7,
            DisApproval = 8,
            FilterButton = 9,
            Submit = 10,
            Copy = 11,
        }

        public enum ETable
        {
            Demo_table = 1,
            Department_List = 2,
            Sec_Controller = 3,
            Sec_Menu = 4,
            Sec_Role = 5,
            Sec_User = 7,
            Config_Insurance = 10,
            Sec_Role_Menu = 11,
            Sec_User_Menu = 12,
            Insurance_Position = 13,
            GlobalList = 14,
            PersonalIncomeTax = 15, //danh mục thuế thu nhập cá nhân
            OrganizationUnit = 16,
            Staff = 17,
            WorkingProcess = 18,
            EmployeeAllowance = 19,
            SocialInsuranceDetail = 20,
            EmployeeRelationships = 21,
            EmployeeBonus_Discipline=22,
            Salary =24,
            //Sec_Role_Organizationunit=,
            //Thông_tin_khen_thưởng = 22,
            //Thông_tin_kỷ_luật = 23,
            //Bảng_lương = 24,
            //Phiếu_lương = 25,
            Timekeeping = 26, //quản lý chấm công
            Sec_Role_User = 27,
            Sys_Table_Role_Action = 28,
            Timekeeping_TimeSSN = 29, //công trên máy
            Timekeeping_ManagerVacation = 30, //quản lý phép
            HR_WorkingDaySummary = 31, //tổng hợp công
            Sys_table_Column_Role = 32,
            RecommendedList = 33, //danh sách đề nghị  
            HR_WorkingDaySupplement = 34, //duyệt công
            PerformanceReport = 35, //báo cáo chi phí lương
            PerformanceReportONN = 74, //báo cáo chi phí lương
            PaymentProduct = 36, //danh sách Margin thu phí
            ReportAccountByStaff = 38,
            DetailAccountReport = 40, //báo cáo chi tiết
            //Báo_cáo_chi_tiết_phí_thuê = 41,
            //Báo_cáo_chi_tiết_thuê_refer = 42,
            Sec_StaffMarginLevel=45,
            HR_WorkingDayMachineSatList=43,
            ConfigAllowance=44,
            Sec_User_ViewCompany=46,
            Policy = 47,
            PolicyDetail = 93,
            StaffPlanFundRate = 96,
            StaffPlanRenewalRate = 95,
            HR_Holiday =48,
            Config_PersonalIncomeTax=49,
            HR_WorkingDay =50,
            HR_WorkingDayMachine=51,
            HR_WorkingDayConfig =52,
            HR_WorkingDayMachineDetail=58,
            Province = 59,
            TemplateSalary=61,
            StandardSpending= 64,
            CheckSalaryErp=57,
            OrganizationUnitPlan= 66,
            OrganizationUnitPlanImplementation=67,
            StaffPlan=68,
            StaffPlanImplementation=69,
            Log = 70,
            MCC_BMInHouse =71,
            utl_Control_Permission=72,
            utl_Grid_Permission=73,
            ReportSourceLead = 78,
            ReportL_Staff =79,
            ReportL_Department =80,
            Merge = 88,
            //utl_Role_Permission = 74,
            SalaryKPI = 94,
            HR_OpeningAdditionalWork = 97
            //HR_OpeningAdditionalWork = 96
        }
    }

}
