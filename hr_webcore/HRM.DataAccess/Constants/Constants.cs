using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Common
{
   public class Constants
    {
        public const int PasswordDerivationIteration = 1000,
                     PasswordBytesLength = 64,
                     MinPasswordLength = 8,
                     PasswordSaltLength = 16,
                     ActivationCodeLength = 32;

        public const string MAIL_HOST = "MAIL_HOST";
        public const string MAIL_PORT = "MAIL_PORT";
        public const string MAIL_SENDER = "MAIL_SENDER";
        public const string MAIL_PASSWORD = "MAIL_PASSWORD";
        public const string MAIL_DISPLAY = "MAIL_DISPLAY";
        public const string MAIL_ADDRESS = "MAIL_ADDRESS";
        public const string MAIL_PASS = "MAIL_PASS";

        // Nova
        public const string CustCode = "KHNOVA";

        /*
         * Date time format
         */
        public const string TIME_SPAN_FORMAT = "HH:mm:ss";
        public const string TIME_SPAN_FORMAT_AM_PM = "hh:mm tt";
        public const string REG_TIMESPAN = "^(0[1-9]|1[0-2]):[0-5][0-9] [ap]m$";

        /*
         * Difinition SEPARATE
         */
        public const string SEPARATE_SEM1 = ";";  // SEPARATE
        public const string SEPARATE_SEM2 = "; "; // SEPARATE
        public const string SEPARATE_COM1 = ",";  // SEPARATE
        public const string SEPARATE_COM2 = ", "; // SEPARATE
        public const string SEPARATE_LATEX = "^"; // SEPARATE
        public const string TAG_REG = "~";
        public const string TAG_BR = "</br>";
        public const string SELLECT_ALL = "Select all";
        public const string ALL = "All";

        /*
         *Status
         */
        public const string STATUS_ACTIVE = "Active";
        public const string STATUS_INACTIVE = "In-Active";

        /*
         * Properties suffix
         */
        public const string SUFFIX_DISP = "Disp";

        /*
         * Sort direction
         */
        public const string ASCENDING = "ASC";
        public const string DESCENDING = "DESC";

        /*
        * Difinication message
        */
        public const string MSG_REQUIRED = "{0} is required.";
        public const string MSG_DUPLICATE = "{0} is duplicated.";
        public const string MSG_NOTENOUGHREQUIREDFIELD = "You are missing some require fields. Please include them.";
        public const string MSG_SAVE_SUCCESSFUL = "Cập nhật thành công";
        public const string MSG_SAVE_UNSUCCESSFUL = "Cập nhật không thành công";
        public const string MSG_INSERT_SUCCESSFUL = "Thêm mới thành công";
        public const string MSG_INSERT_UNSUCCESSFUL = "Thêm mới không thành công";


        /*
         * Contract Status
         */
        public const string CONTRACT_STATUS_DRAFT = "Bản nháp";
        public const string CONTRACT_STATUS_PENDING = "Chờ duyệt";
        public const string CONTRACT_STATUS_NOTAPPROVE = "Không duyệt";
        public const string CONTRACT_STATUS_APPROVE = "Đã duyệt";
        public const string CONTRACT_STATUS_INPROCESS = "Đang triển khai";
        public const string CONTRACT_STATUS_COMPLETE = "Kết thúc";
        public const string CONTRACT_STATUS_DISSOLVE = "Hủy";


        /*
         * Contract Role
         */
        // role quản trị hệ thống
        public const string CONTRACT_ROLE_ADMIN = "1";
        // role quản lý
        public const string CONTRACT_ROLE_MANAGER = "2";
        // role chuyen vien tu van
        public const string CONTRACT_ROLE_ADVISOR = "3";
        // role trien khai dich vu
        public const string CONTRACT_ROLE_TECHNICAL = "4";
        // role Cham soc khach hang
        public const string CONTRACT_ROLE_CUSTSERVICE = "5";
        // role Ke toan
        public const string CONTRACT_ROLE_ACCOUNTING = "6";
        // role Hanh chinh nhan su
        public const string CONTRACT_ROLE_HR = "7";
        public const string CONTRACT_ROLE_MARKETING = "10";
        public const string CONTRACT_ROLE_DIRECTOR_CENTER = "13";
        public const string CONTRACT_ROLE_DESIGN = "14";
        public const string CONTRACT_ROLE_CONTENT = "15";
        public const string CONTRACT_ROLE_SOCIAL = "15";
        /*
         * ApDomain Type
         */
        public const string STATUS_CUSTOMER = "27";
        public const string RESOURCE_CUSTOMER = "23";
        public const string SUBSECTORS_CUSTOMER = "22";
        public const string MARKETING_CUSTOMER = "19";
        public const string TYPEDETAIL_CUSTOMER = "29";
        public const string NEED_CUSTOMER = "24";
        public const string CUSTTYPE_CUSTOMER = "44";
        public const string CONTACTTYPE_CUSTOMER = "18";
        public const string ADDRESSTYPE_CUSTOMER = "17";
        public const string COUNTRY_CUSTOMER = "30";
        public const string SEX_CUSTOMER = "16";
        public const string MARITAL_CUSTOMER = "31";
        public const string CAMPAIGN_STATUS = "31";
        public const string CURRENCY_TYPE = "1";
        public const int REFUND_STATUS_PARENTID = 1719;

        #region Price Google + Deploy Google

        // <!--Nhân viên tư vấn -->
        public const string LIST_ADVISOR = "ListAdvisor";
        public const string LIST_STATUS = "ListStatus";
        // <!--Kỹ thuật triển khai dịch vụ -->
        public const string LIST_TECHNICAL = "ListTechnical";

        // <!-- Khach hang-->
        public const string LIST_CUSTOMER = "ListCustomer";


        // <!--Kiểu tiền tệ-->
        public const string LIST_CURRENCYTYPE = "ListCurrencyType";

        //public const string LIST_PROMOTIONAL = "List_Promotional";

        // <!--Đơn vi thời gian-->
        public const string LIST_TIMEUNIT = "ListTimeUnit";

        // <!---Giá trị/Chủ đề-->
        public const string LIST_THEMS = "ListThems";

        // <!---Giá trị/ Độ tuổi-->
        public const string LIST_OLD = "ListOld";

        // <!---Giá trị/Sở thích-->
        public const string LIST_PREFERENCE = "ListPreference";



        // <!---Website-->
        public const string LIST_WEBSITE = "ListWebsite";

        // <!---Tu khoa-->
        public const string LIST_KEYWORD = "ListKeyword";

        // <!---Hình thức QC-->
        public const string LIST_ADFORMS = "ListAdForms";

        // <!---Định dạng QC-->
        public const string LIST_ADFORMAT = "ListAdFormat";

        // <!---Hình thức quản lý-->
        public const string LIST_MANAGEMENTFORMS = "ListManagementForms";

        // <!---Loai tai khoan-->
        public const string LIST_ACCOUNTTYPE = "ListAccountType";

        // <!---Miền google-->
        public const string LIST_GOOGLEDMAIN = "ListGoogleDmain";

        // <!---Địa ly vung mien-->
        public const string LIST_REGION = "ListRegion";

        // <!---Hình thức tính phí-->
        public const string LIST_FREEFORM = "ListFreeForm";

        // <!---Nhắm chọn-->
        public const string LIST_CLOSECHOOSE = "ListCloseChoose";

        // <!---He dieu hanh-->
        public const string LIST_SYSTEM = "ListSystem";

        // <!---Thiết bị chạy-->
        public const string LIST_DEVICE = "ListDevice";

        // <!---Vị trí hiển thị -->
        public const string LIST_POSITIONDISPLAY = "ListPositionDisplay";

        // <!---List website by customer -->
        public const string LIST_WEBSITE_CUSTOMER = "ListWebsiteCustomer";

        // <!---List Ad_Adword by customer -->
        public const string LIST_AP_ADWORD = "ListAPAdwork";

        public const string GOOGLE_PRICE_AP_DOMAIN = "GooglePriceApDomain";

        // <!---List Department Sendr -->
        public const string LIST_DEPARTMENT = "ListDepartment";


        //--------- Deploy
        // <!--Hợp đồng-->
        public const string LIST_CONTRACT = "ListContract";

        public const string TYPE_STATUS_DEPLOYGOOGLE = "02";
        #endregion


        // <!---Yêu cầu triển khai -->
        public const string LIST_DEPLOY = "ListDeploy";

        // <!--Chăm sóc khách hàng -->
        public const string LIST_CUSTSERVICE = "ListCustService";
        // <!---Yêu cầu triển khai chi tiết-->
        public const string LIST_DEPLOYDETAIL = "ListDeployDetail";
        // <!---Báo giá googlea-->
        public const string LIST_PRICEGOOGLE = "ListPricegoogle";
        // <!---Báo giá googlea-->
        public const string LIST_PROMOTIONAL = "ListPromotional";
        // <!---Báo giá googlea-->
        public const string LIST_PRICEFB = "ListPricefacebook";

        public const long HEADID = 2;
        public const long BRANCHID = 3;
        public const string HEADName = "Hà Nội";
        public const string BRANCHName = "TP Hồ Chí Minh";

        // <!--Hình thức thanh toán -->
        public const string LIST_PAYFORMS = "ListPayForms";
        // <!--VAT -->
        public const string LIST_VAT = "ListVat";

        // <!---List website by customer -->
        public const string LIST_FANPAGE_CUSTOMER = "ListFanPageCustomer";

        // <!---Hình thức QC-->
        public const string LIST_ADFORMSFB = "ListAdFormsFb";

        // <!---Hình thức tính phí-->
        public const string LIST_FREEFORMFB = "ListFreeFormFb";

        public const string CCMAILCONTRACT3 = ",huongptt@novaads.com  ";
        public const string CCMAILCONTRACT = ",huongptt@novaads.com  ";
        public const string CCMAILCONTRACT1 = ",admin.ads@novaon.vn,hrhcm.ads@novaon.vn ,allketoan.ads@novaon.vn,huongptt@novaads.com  ";
        //  public const string CCMAILCONTRACT1 = ",admin.ads@novaon.vn,hrhcm.ads@novaon.vn ,allcskh.ads@novaon.vn,huongptt@novaads.com  ";

        /// <summary>
        /// Mật khẩu mặc định : 123456
        /// </summary>
        public const string PASS_USER_DEFAULT = "e10adc3949ba59abbe56e057f20f883e";

        public static int CouponTypeUsed = 1737;
        public static string ApplicationJson = "Application/Json";
        public static int CouponInActiveStatus = 1237;
        public const int CouponActiveStatus = 1238;
        public const int CustomerReferralPromotionID = 109;
        public static string ClientNeedId = "1647";
        public const int CouponStatusUsed = 1750;
        public static int CouponInStatusUsed = 1751;

        public static int gridPageSize => Convert.ToInt32(Utilities.GetAppConfig("PageSize"));
        public static int PageSizeFive => Convert.ToInt32(Utilities.GetAppConfig("PageSizeFive"));
        public static string DefaultPrefixPromotionCode => Utilities.GetAppConfig("DefaultPrefixPromotionCode");
        public static string AutoAdsApiUrl => Utilities.GetAppConfig("AutoAdsApiUrl");
        public static string DefaultKeySignatureKey => Utilities.GetAppConfig("DefaultKeySignatureKey");
        public static string SignatureKey => Utilities.GetAppConfig("SignatureKey");
        public static string RsaPublicKey => Utilities.GetAppConfig("RsaPublicKey");
        public static string RsaPrivateKey => Utilities.GetAppConfig("RsaPrivateKey");
        public static string AutoAdsApiCreateCustomerUrl => Utilities.GetAppConfig("AutoAdsApiCreateCustomerUrl");
        public static string AutoAdsApiCreateRefundUrl => Utilities.GetAppConfig("AutoAdsApiCreateRefundUrl");
        public static string AutoAdsApiUpdateRefundUrl => Utilities.GetAppConfig("AutoAdsApiUpdateRefundUrl");
        public static string AutoAdsApiCreateDetailRefundUrl => Utilities.GetAppConfig("AutoAdsApiCreateDetailRefundUrl");
        public static string AutoAdsApiUpdateDetailRefundUrl => Utilities.GetAppConfig("AutoAdsApiUpdateDetailRefundUrl");
        public static string AutoAdsApiUptWebsiteUrl => Utilities.GetAppConfig("AutoAdsApiUptWebsiteUrl");
        public static string AutoAdsApiUpdateCustomerUrl => Utilities.GetAppConfig("AutoAdsApiUpdateCustomerUrl");

        public static string IpDomainServer => Utilities.GetAppConfig("IpDomainServer");
        public static string MsgTransactionSuccess => Utilities.GetAppConfig("MsgTransactionSuccess");
        public static string AutoAdsApiChangeCustomerUrl => Utilities.GetAppConfig("AutoAdsApiChangeCustomerUrl");
        public static string DefaultPackage => Utilities.GetAppConfig("DefaultPackage");
        public static string MsgTransactionFail => Utilities.GetAppConfig("MsgTransactionFail");
        public static string DefaultCustomerPassword => Utilities.GetAppConfig("DefaultCustomerPassword");
    }
  
    
}
