
using System;

namespace HRM.Common
{
    // Model Constants
    public static class SystemMessageConst // Lớp static SystemMessageConst
    {
        public static class systemmessage
        {
            public const string ProductNotExisted = "Sản phẩm không tồn tại hoặc không có liệu trình";

            public const string NameTherapyExisted = "Tên liệu trình đã tồn tại";
            public const string NumberTherapyExisted = "Số lần liệu trình đã tồn tại";

            public const string CancesEquipment = "Loại bỏ thiết bị khỏi liệu trình thành công";
            public const string AddEquipment = "Thêm thiết bị vào liệu trình thành công";

            public const string NameBrandchExisted = "Tên chi nhánh đã tồn tại";
            public const string CodeBrandchExisted = "Mã chi nhánh đã tồn tại";

            public const string NameRoomExisted = "Tên phòng đã tồn tại";
            public const string CodeRoomExisted = "Mã phòng đã tồn tại";

            public const string AmountExisted = "Số tiền không hợp lệ";
            public const string NameCarExisted = "Tên thẻ đã tồn tại";
            public const string NotRoleCreate = "Không có quyền thêm mới";
            public const string NotRoleDelete = "Không có quyền xóa";
            public const string NotRoleUpdate = "Không có quyền cập nhật thông tin";

            public const string IncorrectCodeActive = "Nhập sai code active";
            public const string IncorrectCodeActiveEn = "Incorrect code active";
            public const string CreateAccountSuccess = "Tạo tài khoản thành công";
            public const string CreateAccountSuccessEn = "Create account successfull";
            public const string AddSuccess = "Thêm thành công";
            public const string AddSuccessEn = "Add successfull";
            public const string BuySuccess = "Thanh toán thành công";
            public const string BuySuccessEn = "Check out successfull";
            public const string SaveSuccess = "Cập nhật thành công";
            public const string SaveSuccessEn = "Update successful";
            public const string IsNotExist = "Dữ liệu không tồn tại";
            public const string IsNotExistEn = "This Data is not existed!";
            public const string EmailIsExisted = "Email đã tồn tại";
            public const string EmailIsExistedEn = "Email is existed";
            public const string PhoneIsExisted = "Số điện thoại đã tồn tại";
            public const string PhoneIsExistedEn = "Phone is existed";
            public const string EmailIsNotExist = "Email không tồn tại";
            public const string EmailIsNotExistEn = "Email is not existed";
            public const string EmailAndPhoneIsExisted = "Email hoặc số điện thoại này đã được đăng ký";
            public const string EmailAndPhoneIsExistedEn = "Email or phone is existed";
            public const string EditSuccess = "Cập nhật thành công";
            public const string EditSuccessEn = "Update successfull";
            public const string CheckInSuccess = "Check in thành công";
            public const string CheckoutSuccess = "Check out thành công";
            public const string ConfirmAfterDelete = "Bạn có chắc chắn muốn xóa !";
            public const string DeleteSuccess = "Xóa thành công";
            public const string DataIsEmpty = "Chưa có dữ liệu";
            public const string WereAreSendMailConfirm = "Đăng ký tài khoản thành công,Mời bạn kiểm tra hòm thư để xác nhận mail";
            public const string SendMailActive = "Mail kích hoạt đã được gửi";
            public const string SendMailActiveEn = "This mail confirm has been send to your mail";
            public const string SendMailForgotPassword = "Mail lấy lại mật khẩu đã được gửi";
            public const string SendMailForgotPasswordEn = "Mail forgotpassword has been send";
            public const string MustCheckAgree = "Bạn chưa chọn đồng ý";
            public const string MustCheckAgreeEn = "You must be check agree";
            public const string EmailConfirmSuccessFull = "Xác nhận email thành công";
            public const string EmailConfirmUnSuccessFull = "Xác nhận email thất bại";
            public const string EmailConfirmIsNotCorrect = "Xác nhận email không chính xác";
            public const string EmailConfirmIsNotCorrectEn = "Email confirm is not correct";
            public const string DateTimeIsNotCorrectFormat = "Sai định dạng ngày tháng";
            public const string ConfirmPasswordNotCorrect = "Xác nhận mật khẩu không chính xác";
            public const string ConfirmPasswordNotCorrectEn = "Confirm password is not correct !";
            public const string OldPasswordNotCorrect = "Mật khẩu cũ không chính xác";
            public const string OldPasswordNotCorrectEn = "Old password is not correct";
            public const string SendSuccess = "Gửi thành công";
            public const string SendSuccessEn = "Send Success";
            public const string StatusHide = "Hide";
            public const string StatusShow = "Show";
            public const string AccountNotExist = "Tài khoảng không tồn tại";
            public const string AccountNotExistEn = "This Account is not exist !";
            public const string PasswordNotCorrect = "Mật khẩu không chính xác";
            public const string PasswordNotCorrectEn = "Your password is not correct !";
            public const string City = "thành phố";
            public const string CityEn = "thành phố";
            public const string Phone = "số điện thoại";
            public const string PhoneEn = "phone";
            public const string Address = "địa chỉ";
            public const string AddressEn = "address";
            public const string Address2 = "địa chỉ 2";
            public const string Address2En = "address2";
            public const string Fullname = "họ và tên";
            public const string FullnameEn = "Fullname";
            public const string MessageEn = "Message";
            public const string Message = "Nội dung tin nhắn";
            public const string DefaultImage = "/Content/dist/img/default-image.gif";
            public const string NoData = "Không có dữ liệu";
            public const string NoDataEn = "No Data";
            public const string CartEmpty = "Giỏ hàng trống !";
            public const string CartEmptyEn = "Your cart is empty !";
            public const string CaptchaIncorrect = "Nhập lại captcha";
            public const string CaptchaIncorrectEn = "Incorrect captcha";
            public const string ArticleExisted = "Mã sản phẩm đã tồn tại";
            public const string PhoneConfirmMes = "Một tin nhắn chứa mã xác nhận đã được gửi đến số của bạn , hãy nhập mã xác nhận đó để hoàn tất đăng ký !";
            public const string PhoneConfirmMesEn = "A message containing a confirmation code has been sent to your number, please enter the confirmation code to complete the registration !";
            public const string BataMessageActiveCode = "Ban dung ma so sau day de hoan tat dang ky : ";
            public const string Notification = "Thông báo";
            public const string NotificationEn = "Notification";
            public const string SearchContentFormat = "Từ khóa không đúng định dạng !";
            public const string SearchContentFormatEn = "This content is not correct format !";
            public const string DataExisted = "Dữ liệu đã tồn tại";
            public const string DataNotExisted = "Dữ liệu không tồn tại tồn tại";
            public const string UpdateSuccess = "Cập nhật dữ liệu thành công";

            public const string RoleIsNotExist = "Quyền không tồn tại";
            public const string MenuIsNotExist = "Menu không tồn tại";
            public const string AccountIsNotExist = "Tài khoản không tồn tại hoặc đã bị khóa";
            public const string RoleAndAccountExit = "Tài khoản đã tồn tại quyền này";
            public const string RoleAndAccountandMenuExit = "Tài khoản đã tồn tại quyền với menu này";
            public const string RoleMenuExit = "Quyền đã tồn tại menu này";
            public const string MsgChoseRole = "Mời bạn chọn quyền";

            public const string AccountIsnotRoleExit = "Tài khoản chưa có quyền này";
            public const string ConfirmProcess = "Tiến hành thực hiện dịch vụ với khách hàng ";
            public const string ConfirmProcess1 = " ...";
            public const string ChoseCustomerSuccess = "Chọn khách hàng xử lý thành công";
            public const string ChoseCustomerNotSuccess = "Chọn khách hàng xử lý thất bại";
            public const string CancesProcessCustomer = "Hủy thực hiện dịch vụ với khách hàng  ";
            public const string CancesProcessCustomer1 = "....";

            public const string CancesProcessSuccess = "Hủy xử lý khách hàng thành công";
            public const string CancesProcessIsnotSuccess = "Hủy xử lý khách hàng không thành công";
            public const string lstAccountIsnotNull = "Chưa chọn tài khoản cần thêm quyền";
            public const string lstMenuIsnotNull = "Chưa chọn menu thêm vào quyền cho tài khoản";
            public const string RoleIsInvailMenu = "Quyền đã tồn tại menu";
            public const string RoleIsVailAcount = "Tài khoản đã có quyền quản lý menu";

            public const string ReportExisted = "Liệu trình đã có phiếu test";
            public const string BrandIsNotExist = "Chi nhánh không tồn tại";
            public const string LstShiftNotExit = "Chưa chọn ca làm việc";
            public const string DateShiftExit = "Ngày đã có lịch làm việc";
            public const string DateEquipmentExit = "Ngày đã thiết lập vật tư";

            public const string ConfirmLockCard = "Bạn có chắc chắn muốn khóa loại thẻ này không ?";
            public const string ConfirmUnLockCard = "Bạn có chắc chắn muốn mở khóa loại thẻ này không ?";

            public const string ConfirmLockRoom = "Bạn có chắc chắn muốn khóa phòng này không ?";
            public const string ConfirmUnLockRoom = "Bạn có chắc chắn muốn mở khóa phòng này không ?";

            public const string ConfirmLockBrandch = "Bạn có chắc chắn muốn khóa chi nhánh này không ?";
            public const string ConfirmUnLockBrandch = "Bạn có chắc chắn muốn mở khóa chi nhánh này không ?";
        }

        public static class RankConst
        {
            public const int RankA = 7000000;
            public const int RankB = 30000000;
            public const int RankABonus = 5;
            public const int RankBBonus = 10;
            public const int RankABirthBonus = 10;
            public const int RankBBirthBonus = 15;
        }

        public static class Role
        {
            public const int ChuyenvienReturn = 32;
            public const int LetanReturn = 33;
            public const int Admin = 1;
            public const string AdminName = "admin";
            public const int Cls = 26;
            public const int Cs = 29;
            public const int LetanCs = 22;
            public const int Tablet = 31;
        }

        public static class Center
        {
            public const int Ho = 17;
        }
        public static class SaleStatus
        {
            public const int ChotSale = 1;
            public const int XuatPhieu = 2;
            public const int KhongMuaHang = 3;
            public const int MuaHang = 4;
            public const int HuySale = 5;
            public const int DaThanhToan = 6;
        }
        public static class BillDetailStatus
        {
            public const int Lichmoi = 0;
            public const int Dangtrilieu = 1;
            public const int DangTraHang = 2;
            public const int Datrahang = 3;
            public const int Baoluu = 4;
            public const int Hoanthanh = 5;
        }
        public static class TypeAction
        {
            public const string Addnew = "AddNew";
            public const string Update = "Update";
            public const string Delete = "Delete";
        }
        /// <summary>
        /// Status trong bản appointment
        /// </summary>
        public static class AppStatus
        {

            public const int Chuadenlich = 0; //lịch hẹn chưa tới
            public const int Checkin = 1; //Đến nhưng checkin
            public const int Checkout = 2; // đã tới và checkout
            public const int CheckoutNoAction = 3; //checkout nhưng không thực hiện liệu trình
        }
        /// <summary>
        /// Trạng thái lịch hẹn lễ tân return
        /// </summary>
        public static class AppReturnStatus
        {

            public const string Henmoi = "1"; 
            public const string Choxuly = "2"; 
            public const string Khachvekhongreturn = "3";
        }
        /// <summary>
        /// Trang thai return cua chuyen vien
        /// </summary>
        public static class ReturnStatus
        {

            public const string ChuaXuLy = "0";
            public const string Dangxuly = "1";
            public const string DaXuLy = "2";
            public const string KhongReturn = "3";
        }
        /// <summary>
        /// Trạng thái OP bảng app
        /// </summary>
        public static class AppType
        {

            public const int datlich = 0;
            public const int confirm = 1; 
            public const int confirmed = 2; 
        }
        /// <summary>
        /// Trạng thái trong bảng bill detail và bill
        /// </summary>
        public static class BillStatus
        {

            public const int lichmoi = 0; 
            public const int dangtrilieu = 1; 
            public const int dangtrahang = 2; 
            public const int datrahang = 3;
            public const int baoluu = 4;
            public const int hoanthanh = 5;
        }
        public static class RoleLevel
        {
            public const int Admin = 1;
        }
        public static class Key
        {
            public const string Return = "RETURN";
            public const string StatusAppointment = "STATUS_APPOINTMENT_RETURN";
            public const string StatusAppointment_Default = "1";
            public const string EmpReasonReturnAgree = "EMP_REASON_RETURN_AGREE";
            public const string EmpReasonReturnDontAgree = "EMP_REASON_RETURN_DONT_AGREE";
            public const string StatusReturn = "STATUS_RETURN";
            public const string StatusCustomerCancelSell = "STATUS_CUSTOMER_CANCEL_SELL";
            public const string StatusBillForm = "STATUS_BILL_FORM";
            public const string StatusBillTime = "STATUS_BILL_TIME";
            public const string StatusReception = "STATUS_RECEPTION"; //Trạng thái thực hiện liệu trình //>có hoặc không
            public const string StatusReceptionYes = "1"; //khách có thực hiện liệu trình
            public const string StatusReceptionNo = "0"; //khách không thực hiện liệu trình
            public const string ReasonReception = "REASON_RECEPTION";//lý do khách ko thực hiện liệu trình
            public const string StatusReturnAgree = "1";
            public const string StatusReturnDontAgree = "2";
            public const string ChoVay = "1";//cho vay
            public const int StatusTypeBonusBill = 1;
            public const int StatusTypeBonusBillDetail = 2;
            //bảng sysproduct
            public const string Product_Sale = "RETAIL"; // sản phẩm lẻ
            public const string Product_Service = "SERVICE"; // sản phẩm theo gói
            public const string Product_FromSale = "SALE"; // sản phẩm từ sale đổ về
            //-

            public const string Reason_CancesProcess = "REASON_CANCESPROCESS";
            public const string QuantityShif = "3";
            public const string QuantityMachine = "3";
        }
        // Đối tượng static ko đổi ValidateConst
        public static class ValidateConst
        {
            public const string MinlengthOfText = "Độ dài của {0} phải lớn hơn {1}."; // Phần tử MinlengthOfText truyền vào 2 tham số
            public const string MinlengthOfTextEn = "Length of {0} must be greater {1}.";
            public const string EmailNotCorrectFormat = "Email không đúng định dạng";
            public const string EmailNotCorrectFormatEn = "Email is not correct Format";
            public const string CheckNotEmpty = "{0} không được để trống";
            public const string CheckNotEmptyEn = "{0} is not empty";
            public const string MinMaxlengthOfText = "Độ dài của {0} Phải nhỏ hơn {1} Và  lớn hơn {2}."; // Phần tử MinlengthOfText truyền vào 3 tham số
            public const string MinMaxlengthOfTextEn = "Length of {0} must be smaller {1} and greater {2}.";
            public const string MaxlengthOfText = "Độ dài của {0} Phải nhỏ hơn {1}.";
            public const string MaxlengthOfTextEn = "Length of {0} must be smaller {1}.";
            public const string DateIsNotValid = "{0} không hợp lệ.";
        }

        public static class PrefixOrder
        {
            public const string PrefixPayOnline = "PP-";
            public const string PayLater = "PL-";
        }


    }
}