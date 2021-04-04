'use strict'
function BuildTable1(appName, controllerName, tableUrl) {
    app.controller(controllerName,
        function ($scope, myService, $filter, $rootScope, $interval, $compile) {
            $scope.maxSize = 5; // Limit number for pagination display number.
            $scope.totalCount = 0; // Total number of items in all pages
            $scope.pageIndex = 1; // Current page number. First page is 1.-->
            $scope.pageSizeSelected = 5;
            $scope.isShowFilter = false;
            $scope.filterColumnsChoosed = [];
            $scope.emailValid = /^[a-z]+[a-z0-9._]+@[a-z]+\.[a-z.]{2,5}$/;
            $scope.treeName = 'sampleTree';
            $rootScope.data1 = [];
            $scope.editData = {};
            $rootScope.idchose = "";
            $scope.tab2Data = '';
            $scope.flagShow = '';
            $scope.autoid = 0;
            $scope.isShowTimeSSN = true;
            $scope.songaytinh = 1;
            $scope.clicktab = 0;
            $scope.showtab13 = false;
            $scope.isRequestOrApproval = false;
            var url = "";            
            $scope.$on("fileProgress", function (e, progress) {
                $scope.progress = progress.loaded / progress.total;
            });
            //truyen staffID to working proces
            $scope.wpFilter = {
                staffID: null
            }

            //convert get datetime now
            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1;
            var yyyy = today.getFullYear();
            if (dd < 10) {
                dd = '0' + dd;
            }

            if (mm < 10) {
                mm = '0' + mm;
            }

            today = mm + '/' + yyyy;
            var today1 = yyyy + '/' + mm + '/' + dd;
            var today2 = yyyy + '-' + mm + '-' + dd;

            $scope.FromMonth = today;


            $scope.FromMonth1 = today1;
            $scope.FromMonth2 = today2;

            var oldString = $scope.FromMonth;
            var str = oldString.replace("/", "-");
            $scope.newString = str;
            $scope.editData.MonthVacation = $scope.newString;

            //model đượct truyền ra từ directive build table
            $scope.employeeData = {};

            //Global theo parentID
            $scope.GlobalListWhereParentID = {
                TypeName: 84, //loại để nghị 84
                Reason: 86,  //lý do 86
                Status: 85 //trạng thái 85
            }           
            $scope.CallData = function () {
                KindOfProposal();
                Reason();
                Status();
            }

            //Xóa bổ sung công theo autoid
            $scope.deleteBosung = function (giatri) {
                BoostrapDialogConfirm(notification,
                    notificationDelete,
                    BootstrapDialog.TYPE_WARNING,
                    $scope.deleteActionClick,
                    giatri);
            }
            $scope.deleteActionClick = function (giatri) {
                var data = {
                    url: "/HR_WorkingDaySupplement/HR_WorkingDaySupplement_DeleteByAutoId?autoid=" + giatri.AutoID
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    //$scope.getBoSungCong = res.data.result;
                    if (res.data.result.IsSuccess==true) {
                        AppendToToastr(true, notification, deleteSuccess, 500, 5000);

                        var tableUrl = "/HR_WorkingDaySupplement/TableServerSideGetData";
                        //lọc các bản tìm kiếm trong tháng đang để dạng bản nháp
                        var strimgfilter = " and a.MonthVacation LIKE '%" + $scope.newString + "%' AND a.Status = 0 ";
                        var user = 0;
                        if ($rootScope.giatri != null) {
                            user = $rootScope.giatri;
                            strimgfilter += " and a.StaffID = " + user + " ";
                        }
                        var data = {
                            pageIndex: 1,
                            pageSize: 1000,
                            month: $scope.from[0],
                            year: $scope.from[1],
                            userid: user,
                            status: status,
                            filter: strimgfilter,
                            bosungcong: 1,
                        }
                        //lấy dữ liệu bổ sung công với user đăng nhập hoặc tìm kiếm
                        var getDataTbl = myService.GetTableData(data, tableUrl);
                        getDataTbl.then(function (emp) {
                            $scope.employeebosungs = emp.data.employees;

                        });
                    }
                    else {
                        AppendToToastr(false, notification, errorNotiFalse, 500, 5000);
                    }
                }, function (res) {
                    $scope.msg = "Error";
                });
            }
            $scope.GetWorkingDayTimePeriod = function (staffId) {
                var list = myService.GetWorkingDayTimePeriod(staffId);
                list.then(function (res) {
                    $scope.HrWorkingDay = res.data.result;
                },
                    function (res) {
                        $scope.msg = "Error";
                    });
            }
            //tạo popup bổ sung công
            $scope.addNewChoice = function (giatri) {
                //khởi tạo các biến hiển thị
                $scope.editData = {};//khởi tạo lại data
                $scope.LoiChuaNhap = false; //lỗi validate khi bấm tạo bổ sung công
                $scope.LoiBoSungCong = false; //lỗi validate nhập số phút đi muộn về sớm lơn hơn quy định
                $scope.LoiChonBoSungCong = false; //lỗi validate chọn ngày tháng
                $scope.is_Type = false;  //ẩn hiện Type khi thêm mới hoặc sửa
                $scope.LoiChonNgay = false; //lỗi validate chọn ngày tháng bắt đầu kết thúc
                $scope.editData = {};
                $scope.disabled = true; //ẩn không chọn trạng thái duyệt
                $scope.showkh = false; //ẩn hiện khi chọn lý do gặp khách hàng
                $scope.showone = false; //ẩn hiện khi chọn các loại đề nghị khác loại xin đi muộn về sớm
                $scope.readonlyFromTime = true; //ẩn hiện cho phép nhập thời gian sau khi chọn ngày bắt đầu
                $scope.readonlyToTime = true; //ẩn hiện cho phép nhập thời gian sau khi chọn ngày kết thúc
                $scope.showlamthem = false; //ẩn hiện khi chọn bổ sung công làm thêm
              
                GetHour(); //lấy giờ làm theo user
                //khởi tạo các giá trị mặc định
                if (giatri!=0) {
                    $scope.editData.Type = giatri.Type.toString();
                }
                else {
                    $scope.editData.Type = "1";
                }
                $scope.editData.Status = "0";
                $scope.editData.DayOff = "0";
                $scope.editData.PercentPayrollID = "100"
                $scope.autoid = 0;
                if ($rootScope.giatri == null || $rootScope.giatri == undefined) $rootScope.giatri = LoginStaffId;
                $scope.GetWorkingDayTimePeriod($rootScope.giatri);
                //lấy dữ liệu ngày tháng cho combobox với Type = 1
                getListDate();
                //kiểm tra đang sửa dữ liệu không phải tạo mới
                if (giatri != 0) {
                    $scope.is_Type = true; //ẩn hiện Type khi thêm mới hoặc sửa
                    $scope.readonlyFromTime = false;
                    $scope.readonlyToTime = false;
                    //lấy dữ liệu bổ sung công bản cần chỉnh sửa theo autoid
                    $scope.autoid = giatri.AutoID;
                    var data = {
                        url: "/HR_WorkingDaySupplement/HR_WorkingDaySupplement_GetListId?autoid=" + giatri.AutoID
                    }
                    var list = myService.getDropdown(data);
                    list.then(function (res) {
                        $scope.getBoSungCong = res.data.result;
                        $scope.editData.Type = $scope.getBoSungCong.Type.toString(); //gắn loại đề nghị để hiển thị
                        $scope.editData.ReasonType = $scope.getBoSungCong.ReasonType; //gắn lý do để hiện thị
                        $scope.editData.Note = $scope.getBoSungCong.Note.toString(); //gắn ghi chú để hiển thị
                        $scope.editData.Status = "0";
                        if ($scope.editData.Type == 1) {
                            $scope.showone = false;
                            var date = FormatDate($scope.getBoSungCong.Date);
                            var timemiss = "";
                            $scope.editData.HourOff = $scope.getBoSungCong.HourOff.substr(-8, 5); //cắt chuỗi lấy giờ đi muộn về sớm
                            var len = $scope.dimuonvesom.length;
                            for (var i = 0; i < len; i++) {
                                var checkTime = $scope.dimuonvesom[i].CheckTime.split('-');
                                var dimuon = $scope.dimuonvesom[i].CheckTime.substr(-29, 10);
                                var time = checkTime[checkTime.length - 1];
                                if (dimuon == date && time.trim() == $scope.getBoSungCong.TimeOfActual.substr(-8, 5)) {
                                    timemiss = $scope.dimuonvesom[i].CheckTime;
                                    break;
                                }
                            }
                            $scope.editData.TimeMissDate = timemiss.toString(); //so sánh lấy giá trị của ngày đến muộn về sớm
                        }
                        if ($scope.editData.Type == 2 || $scope.editData.Type == 3) {

                            $scope.showone = true;
                            $scope.editData.fromdate = FormatDate($scope.getBoSungCong.Date).toString(); //cắt chuỗi lấy ngày bắt đầu
                            $scope.editData.FromTimeStart = $scope.getBoSungCong.FromTime.slice(11, 19).toString(); //cắt chuỗi lấy giờ bắt đầu để hiển thị
                            $scope.editData.todate = FormatDate($scope.getBoSungCong.Date).toString(); //cắt chuỗi lấy ngày kết thúc
                            $scope.editData.ToTimeStart = $scope.getBoSungCong.ToTime.slice(11, 19).toString(); //cắt chuỗi lấy giờ kết thúc để hiển thị
                            $scope.editData.DayOff = $scope.getBoSungCong.DayOff; //lấy ngày công
                        }
                        if ($scope.editData.Type == 4) {
                            $scope.showone = true;
                            if ($scope.getBoSungCong.PercentPayrollID != null && $scope.getBoSungCong.PercentPayrollID != "") {
                                $scope.editData.PercentPayrollID = $scope.getBoSungCong.PercentPayrollID.toString();
                            }
                            $scope.editData.DayOff = $scope.getBoSungCong.DayOff;
                            var date = FormatDate($scope.getBoSungCong.Date);
                            var len = $scope.listBosungcong.length;
                            for (var i = 0; i < len; i++) {
                                var dimuon = $scope.listBosungcong[i].CheckTime.substr(-21, 10);
                                if (dimuon == date) {
                                    $scope.editData.fromdate = $scope.listBosungcong[i].CheckTime;
                                    $scope.editData.todate = $scope.listDate1[i].CheckTime1;
                                    break;
                                }
                            }
                            $scope.editData.FromTimeStart = $scope.getBoSungCong.FromTime.slice(11, 19).toString(); //cắt chuỗi lấy thời gian bổ sung công
                            $scope.editData.ToTimeStart = $scope.getBoSungCong.ToTime.slice(11, 19).toString();
                        }
                        if ($scope.editData.Type == 5) {

                            $scope.showone = true;
                            $scope.showlamthem = true;
                            $scope.editData.PercentPayrollID = $scope.getBoSungCong.PercentPayrollID.toString(); //lấy hệ số làm thêm
                            $scope.editData.DayOff = $scope.getBoSungCong.DayOff;
                            var date = FormatDate($scope.getBoSungCong.Date);
                            var len = $scope.listBosungconglamthem.length;
                            for (var i = 0; i < len; i++) {
                                var dimuon = $scope.listBosungconglamthem[i].CheckTime.substr(-10, 10);
                                if (dimuon == date) {
                                    $scope.editData.fromdate = $scope.listBosungconglamthem[i].CheckTime;
                                    $scope.editData.todate = $scope.listBosungconglamthem[i].CheckTime;
                                    break;
                                }
                            }
                            $scope.editData.FromTimeStart = $scope.getBoSungCong.FromTime.slice(11, 16).toString(); //cắt chuỗi lấy thời gian bổ sung công làm thêm
                            $scope.editData.ToTimeStart = $scope.getBoSungCong.ToTime.slice(11, 16).toString();
                        }
                        //kiểm tra lý do là gặp khách hàng tiến hành gắn các giá trị và mở các ô input phù hợp
                        if ($scope.editData.ReasonType == 1213) {
                            $scope.showkh = true
                            $scope.editData.CustomerID = $scope.getBoSungCong.CustomerID;
                            $scope.editData.CustomerContactID = $scope.getBoSungCong.CustomerContactID;
                            $scope.editData.CustomerReasonType = $scope.getBoSungCong.CustomerReasonType;
                        }
                    }, function (res) {
                        $scope.msg = "Error";
                    });
                }
                ShowPopup($,
                    "#Addbosungcong",
                    $scope.tableInfo.PopupWidth,
                    $scope.tableInfo.PopupHeight);
            }

            $scope.CloseForm = function () {
                $.colorbox.close();
                //$scope.GetListData();

            } //Nút bỏ qua Employee
            $scope.CloseFormAdd = function (form) {
                form.$dirty = false;
                form.$invalid = false;
                form.$submitted = false;
                form.$valid = false;
                ShowPopup($,
                    "#bosungcong",
                    $scope.tableInfo.PopupWidth,
                    $scope.tableInfo.PopupHeight);
                //lấy dữ liệu cho bổ sung công
                $scope.BoSungCong();
            }
            $scope.tennv = "";

            $scope.init = function () {
                var data = {
                    url: "/OrganizationUnit/GetOrganizationUnit?chon=1"
                }
                var list = myService.getData(data);
                list.then(function (res) {
                    $scope.getListAllOrganizationUnit = res.data.result;
                }, function (res) {
                    $scope.msg = "Error";
                });
                if ($rootScope.PhongBan != null) {
                    ListEmployeeWhereOrganizationUnit($rootScope.PhongBan);
                }
                else {
                    ListEmployeeWhereOrganizationUnit(0); //lấy tất cả employee
                }
                checkDuyetcong();
            }
            function checkDuyetcong() {
                var data = {
                    url: "/Timekeeping_TimeSSN/HR_CheckMissDayApproval"
                }
                var list = myService.getData(data);
                list.then(function (res) {
                    if (res.data.ShowTabApproval == 1) {
                        $scope.isQL = true;
                    }
                    else {
                        $scope.isQL = false;
                    }
                }, function (res) {
                    $scope.msg = "Error";
                })
            }
            //dropdown nhân viên theo phòng ban
            function ListEmployeeWhereOrganizationUnit(id) {
                var data = {
                    url: "/OrganizationUnit/EmployeeByOrganizationUnitID?id=" + id
                }
                var list = myService.getData(data);
                list.then(function (res) {
                    $scope.ListEmployeeWhereOrganizationUnit = res.data.result;

                }, function (res) {
                    $scope.msg = "Error";
                })
            }
            //lấy dữ liệu nhân viên khi thay đổi phòng ban
            $scope.onOrganizationUnitChange = function () {
                $rootScope.PhongBan = $scope.OrganizationUnitID;
                if ($scope.OrganizationUnitID != null) {
                    ListEmployeeWhereOrganizationUnit($scope.OrganizationUnitID);
                }
                else {
                    ListEmployeeWhereOrganizationUnit(0);
                }
            }
            //lấy mã user khi thay đổi nhân viên
            $scope.onStaffChange = function () {
                $scope.StaffID = angular.copy($scope.StaffID);
                $rootScope.giatri = $scope.StaffID;
            }


            $scope.employeeData = {};
            $scope.sourceDepartment = [];
            $scope.department = {};




            //----------------LoadTab-----------
            $scope.tabs = {
                tabs1: {
                    url: '/Timekeeping_TimeSSN/Index',
                    container: $("#tabs1"),
                },
                tabs2: {
                    url: '/HR_WorkingDaySupplement/Index',
                    container: $("#tabs2"),

                },
                tabs3: {
                    url: '/RecommendedList/Index',
                    container: $("#tabs3"),

                },
                tabs4: {
                    url: '/HR_WorkingDaySummary/Index',
                    container: $("#tabs4"),
                },
                tabs5: {
                    url: '/Timekeeping/Index',
                    container: $("#tabs5"),

                },
                tabs6: {
                    url: '/Timekeeping_ManagerVacation/Index',
                    container: $("#tabs6"),
                }
            };

            $scope.LoadTab = function (idTab) {
                $scope.clicktab = 1;
                var tab = $scope.tabs[idTab];
                $scope.flagShow = idTab;
                if (idTab && tab && tab.url && tab.container) {
                    LoadPartialView(tab.url, tab.container);
                }
                if (idTab == "tabs1") {
                    $scope.GetListData();
                    $scope.OrganizationUnitID = $rootScope.PhongBan;
                    $scope.onOrganizationUnitChange();
                    $scope.StaffID = $rootScope.giatri;
                    $scope.clicktab = 0;
                }
            }


            function GetIdActiveTab() {
                var current_tab = $('#tabs .ui-tabs-panel:eq(' +
                    $('#tabs').tabs('option', 'active') + ')').attr('id');
                $scope.LoadTab(current_tab);
            }

            function LoadPartialView(url, container, data) {
                if (url == '/Timekeeping_TimeSSN/Index') {
                    $scope.getColumns();
                }
                else {
                    //container.html('');
                    $scope.Columns = [];
                    $scope.employees = [];
                    switch (url) {
                        case '/HR_WorkingDaySupplement/Index':
                            $scope.tab2Data = '';
                            break;
                        case '/RecommendedList/Index':
                            $scope.tab3Data = '';
                            break;
                        case '/HR_WorkingDaySummary/Index':
                            $scope.tab4Data = '';
                            break;
                        case '/Timekeeping/Index':
                            $scope.tab5Data = '';
                            break;
                        case '/Timekeeping_ManagerVacation/Index':
                            $scope.tab6Data = '';
                            break;
                        default:
                            // code block
                    }

                    $.ajax({
                        url: url,
                        data: data,
                        async: false,
                        type: "POST",
                        dataType: "html",
                        success: function (result) {
                            //var htmlResult = $compile(result)($scope);
                            switch (url) {
                                case '/HR_WorkingDaySupplement/Index':
                                    $scope.tab2Data = result;
                                    break;
                                case '/RecommendedList/Index':
                                    $scope.tab3Data = result;

                                    break;
                                case '/HR_WorkingDaySummary/Index':
                                    $scope.tab4Data = result;
                                    break;
                                case '/Timekeeping/Index':
                                    $scope.tab5Data = result;
                                    break;
                                case '/Timekeeping_ManagerVacation/Index':
                                    $scope.tab6Data = result;
                                    break;
                                default:
                                    // code block
                            }
                            //container.html(htmlResult);
                            // $scope.$broadcast('bind-working', container, htmlResult);
                        },
                        complete: function () {
                            //HiddenLoader();
                        },
                        beforeSend: function () {
                            //ShowLoader();
                        }
                    });
                }

            }

            //-----------------List-------------

            $scope.getTableInfo = function () {

                var getData = myService.getTableInformation(tableUrl);
                getData.then(function (emp) {
                    $scope.tableInfo = emp.data.result;
                    $scope.lstPageSize = $scope.tableInfo.PageSizeList.split(',');
                    $scope.pageSizeSelected = $scope.tableInfo.PageSize;
                    $scope.GetAddPermission(emp.data.result.id);
                },
                    function (emp) {
                    });
            }

            $scope.GetAddPermission = function (idTable) {
                var tblAction = myService.getTableAction(idTable);
                tblAction.then(function (emp) {
                    $scope.tablePermission = emp.data.result;

                    $scope.getColumns();
                    // $scope.BuildAddButton(emp.data.result);
                },
                    function (emp) {
                    });
            }
            $scope.getTableInfo();

            $scope.getColumns = function () {
                var getData = myService.GetColumns(tableUrl);
                getData.then(function (emp) {
                    $scope.Columns = emp.data.result;
                    $scope.GetListData();
                    $scope.dodai = $scope.Columns.length;
                },
                    function (emp) {
                        AppendToToastr(false, notification, errorNotiFalse);
                    });
            }
            $scope.chageDate = function () {

                if ($scope.clicktab == 0) {
                    $rootScope.thangnam = $scope.FromMonth;
                }
            }
            //khởi tạo userid mặc định, nếu = 0 thi sẽ lấy theo người đăng nhập
            var userid = 0;
            var status = 0;
            $scope.GetListData = function () {
                GetDateApproval();
                var dt = Loading();
                if ($rootScope.thangnam != null) {
                    $scope.from = $rootScope.thangnam.split("/");
                    $scope.FromMonth = $rootScope.thangnam;
                }
                else {
                    $scope.from = $scope.FromMonth.split("/");
                    $rootScope.thangnam = $scope.FromMonth;
                }
                var data = {
                    pageIndex: $scope.pageIndex == null ? 1 : $scope.pageIndex,
                    pageSize: $scope.pageSizeSelected == null ? 5 : $scope.pageSizeSelected,
                    month: $scope.from[0],
                    year: $scope.from[1],
                    userid: userid,
                    status: status,
                    filter: $scope.getFilterValue()
                }
                myService.GetTableData(data, '/HR_WorkingDaySummary/GetWorkingDayTemporary').then(function (response) {
                    $rootScope.dulieu2 = response.data.employees;
                    $scope.dulieuCopy2 = angular.copy(response.data.employees);
                });
                var getDataTbl = myService.GetTableData(data, tableUrl);
                getDataTbl.then(function (emp) {
                    $scope.employees = angular.copy(emp.data.employees);
                    $rootScope.dulieu = angular.copy(emp.data.employees);
                    $scope.dulieuCopy = angular.copy(emp.data.employees);
                    $scope.totalCount = emp.data.totalCount;
                    $scope.lstTotal = angular.copy(emp.data.lstTotal);
                    $scope.SetTotalByColumns = function (totalName) {
                        if (!angular.isUndefined(totalName) && totalName !== null) {
                            return $scope.lstTotal[totalName];
                        }
                        return "-";
                    };
                    dt.finish();
                },
                    function (emp) {
                        AppendToToastr(false, notification, errorNotiFalse);
                    });
            }
           
            $scope.changeReason = function () {
                if ($scope.editData.ReasonType == 1213) {
                    $scope.showkh = true;
                    GetCustomers();
                }
                else {
                    $scope.showkh = false;
                }
            }
            function dates() {
                var dayOfWeek = 5;//friday
                var date = new Date;
                var diff = date.getDay() - dayOfWeek;
                if (diff > 0) {
                    date.setDate(date.getDate() + 6);
                }
                else if (diff < 0) {
                    date.setDate(date.getDate() + ((-1) * diff))
                }
                $scope.dateweeknext6 = date;
            }
            function WeekNow() {
                dates();
                $scope.listday = [];
                var week = [];
                var curr = $scope.dateweeknext6; // lấy thứ 6 của tuần đó hoặc tuần tiếp theo
                var first = curr.getDate() - curr.getDay() - 3;
                var d = $scope.dateweeknext6;
                var thu = d.getDay();
                if (thu == 0 || thu >= 5) {
                    thu = 4;
                }
                if (thu > 4) {
                    for (var i = 1; i < thu - 3; i++) {
                        var next = new Date(curr.getTime());
                        week.push(next);
                        next.setDate(first + i);
                    }
                }
                else {
                    for (var i = 1; i < 4 + thu; i++) {
                        var next = new Date(curr.getTime());
                        week.push(next);
                        next.setDate(first + i);
                    }
                }

                //Cắt ghép ngày
                for (var i = 0; i < week.length; i++) {
                    var month = {
                        Jan: "01", Feb: "02", Mar: "03", Apr: "04", May: "05", Jun: "06",
                        Jul: "07", Aug: "08", Sep: "09", Oct: "10", Nov: "11", Dec: "12"
                    };

                    var n = week[i].toString();
                    var date = n.split(" ");

                    var v = [date[2], month[date[1]], date[3]].join("/");

                    $scope.listday.push(v);
                }
                $scope.listday = ["29/03/2019", "30/03/2019", "31/03/2019", "01/04/2019", "02/04/2019", "03/04/2019", "04/04/2019", "05/04/2019", "06/04/2019", "07/04/2019", "08/04/2019", "09/04/2019"
                    , "10/04/2019", "11/04/2019", "12/04/2019", "13/04/2019", "14/04/2019", "15/04/2019", "16/04/2019", "17/04/2019", "18/04/2019", "19/04/2019", "20/04/2019", "21/04/2019"];
            }

            var getListDate = function () {
                //Lấy lại dữ liệu sau mỗi lấy ngày
                //$scope.GetListData();
                GetDateApproval();
                //WeekNow();
                $scope.listDate = [];
                if ($scope.dulieuCopy!=null ||$scope.dulieuCopy.length>0 ) {
                    $rootScope.dulieu = angular.copy($scope.dulieuCopy);
                }
                
                //Khai báo các list chứa dữ liệu tạm sau trước khi xử lý
                for (var i = 0; i < $rootScope.dulieu.length; i++) {
                    var datenow = $rootScope.dulieu[i].CheckTime.slice(0, 10);
                    for (var j = 0; j < $scope.listday.length; j++) {
                        if ($scope.listday[j].ListDate == FormatDate(datenow)) {
                            $scope.listDate.push($rootScope.dulieu[i]);
                        }
                    }
                }
                $scope.listDate2 = [];
               
                if ($scope.dulieuCopy2 != null || $scope.dulieuCopy2.length > 0) {
                    $rootScope.dulieu2 = angular.copy($scope.dulieuCopy2);
                }
                //Khai báo các list chứa dữ liệu tạm sau trước khi xử lý
                for (var i = 0; i < $rootScope.dulieu2.length; i++) {
                    var datenow = $rootScope.dulieu2[i].Date.slice(0, 10);
                    for (var j = 0; j < $scope.listday.length; j++) {
                        if ($scope.listday[j].ListDate == FormatDate(datenow)) {
                            $scope.listDate2.push($rootScope.dulieu2[i]);
                        }
                    }
                }

                $scope.listDate1 = $scope.listDate;
                $scope.listBosungconglamthem = [];
                $scope.listBosungcong = [];
                var len = $scope.listDate.length;
                $scope.dimuonvesom = [];
                //Lấy ngày theo các TH Type : 2,3,4,5
                var x = [];//Type = 2,3
                var x1 = [];//Type = 5
                var x2 = [];//Type = 4
                for (var i = 0; i < len; i++) {
                    //khởi tạo và so sánh ngày, lấy từ ngày t6 tuần trước đến t5 tuần này, t6 vẫn lấy từ t5 tuần trước đến t6
                    var datenow = $scope.listDate[i].CheckTime.slice(0, 10);
                    x.push(FormatDate(datenow));
                    var fromdate = datenow;
                    var todate = $scope.FromMonth1;
                    var z = fromdate.split('-');
                    var t = todate.split('/');
                    var a = new Date(z[0], z[1], z[2]);
                    var b = new Date(t[0], t[1], t[2]);
                    var c = (b - a);
                    var songay = c / (1000 * 60 * 60 * 24);
                    if (songay >= 0) {
                        x1.push(FormatDate(datenow)); //truyền dữ liệu vào mảng x1 cho type = 5
                        $scope.listBosungconglamthem.push($scope.listDate[i]);
                        if ($scope.listDate[i].DayWork < 1) { //kiểm tra ngày công < 1 sẽ thêm vào mảng x2 cho type = 4
                            x2.push(FormatDate(datenow));
                            $scope.listBosungcong.push($scope.listDate[i]);
                        }
                    }

                }
                var len1 = $scope.listBosungconglamthem.length;
                var len2 = $scope.listBosungcong.length;

                if ($scope.editData.Type == 1) {
                    $scope.editData.DayOff = 0;
                    for (var i = 0; i < $scope.listDate2.length; i++) {
                        //kiểm tra những ngày có đi muộn về sớm
                        var hourlate = $scope.listDate2[i].HourLate;
                        var hourearly = $scope.listDate2[i].HourEarly;
                        var fromhour = MinMax($scope.listDate2[i].StartTimeSupplement, $scope.listDate2[i].HourCheckIn, 'min');
                        var tohour = MinMax($scope.listDate2[i].EndTimeSupplement, $scope.listDate2[i].HourCheckOut, 'max');
                        if (fromhour == "") {
                            fromhour = "__:__:__"
                        }
                        if (tohour == "") {
                            tohour = "__:__:__"
                        }
                        if (hourlate != "00:00") {
                            $scope.myobj = { CheckTime: $scope.listDate2[i].DayOfWeeks + " - " + FormatDate($scope.listDate2[i].Date.slice(0, 10)) + " - " + fromhour + " - " + hourlate };
                            $scope.dimuonvesom.push($scope.myobj)//thay đổi định dạng mảng Objiect truyển vào combobox
                        }
                        if (hourearly != "00:00") {
                            $scope.myobj = { CheckTime: $scope.listDate2[i].DayOfWeeks + " - " + FormatDate($scope.listDate2[i].Date.slice(0, 10)) + " - " + tohour + " - " + hourearly };
                            $scope.dimuonvesom.push($scope.myobj)
                        }
                    }
                }
                else if ($scope.editData.Type == 2 || $scope.editData.Type == 3) {
                    for (var i = 0; i < len; i++) {
                        var j = len - i - 1;
                        //khai báo và định dạng kiểu ngày cho type = 2 và 3
                        $scope.listDate[i].CheckTime = x[j];
                        $scope.listDate1[i].CheckTime1 = x[j];
                    }
                }
                else if ($scope.editData.Type == 4) {
                    $scope.listDate = $scope.listBosungcong;
                    $scope.listDate1 = $scope.listBosungcong;
                    for (var i = 0; i < len2; i++) {
                        var fromhour = $scope.listBosungcong[i].HourCheckIn;
                        var tohour = $scope.listBosungcong[i].HourCheckOut;
                        if (fromhour == null || fromhour == "") {
                            fromhour = "__:__:__"
                        }
                        if (tohour == null || tohour == "") {
                            tohour = "__:__:__"
                        }
                        var j = len2 - i - 1;
                        var day = $scope.listBosungcong[i].DayOfWeeks;
                        //khai báo và định dạng kiểu ngày cho type = 4
                        $scope.listDate[i].CheckTime = day + " - " + x2[i] + " - " + fromhour;
                        $scope.listDate1[i].CheckTime1 = day + " - " + x2[i] + " - " + tohour;
                    }   
                }
                else if ($scope.editData.Type == 5) {
                    $scope.anlamthem = true;
                    $scope.listDate = $scope.listBosungconglamthem;
                    $scope.listDate1 = $scope.listBosungconglamthem;
                    for (var i = 0; i < len1; i++) {
                        var j = len1 - i - 1;
                        //khai báo và định dạng kiểu ngày cho type = 5
                        $scope.listDate[i].CheckTime = $scope.listBosungconglamthem[j].DayOfWeeks + " - " + x1[j];
                        $scope.listDate1[i].CheckTime1 = $scope.listBosungconglamthem[j].DayOfWeeks + " - " + x1[j];
                    }
                }

            }
            function MinMax(first, second, direction)
            {
                if (direction = 'min')
                {
                    if (first == null) return second;
                    else
                        if (second == null) return first;
                        else
                            if (moment(first, 'HH:mm:ss').isBefore(moment(second, 'HH:mm:ss'))) return first;
                            else return second;
                }
                else
                    if (direction = 'max') {
                        if (first == null) return second;
                        else
                            if (second == null) return first;
                            else
                                if (moment(first, 'HH:mm:ss').isBefore(moment(second, 'HH:mm:ss'))) return second;
                                else return first;
                    }
            }
            //This method is calling from pagination number
            $scope.pageChanged = function () {
                $scope.GetListData();
            };

            //This method is calling from dropDown
            $scope.changePageSize = function () {
                $scope.pageIndex = 1;
                $scope.GetListData();
            };
            $scope.formatData = function (type, value, dataFormat) {
                if (type === 3) {
                    return FormatDate(value);
                }
                if (dataFormat === "N2") {
                    return value.toFixed(2);
                }
                if (dataFormat === "hh:mm:ss") {
                    var vitri;
                    var time;
                    if (value != null) {
                        vitri = value.indexOf("T") + 1;
                        time = value.slice(vitri);
                    }
                    return time;
                }
                if (dataFormat === "shortTime" && value != null) {
                    return value.toString().slice(0, -3);
                }
                return value;
            }
            $scope.SetHiddenActionColumn = function (showEdit, showDelete) {
                if (showEdit === false && showDelete === false) {
                    return false;
                }
                return true;
            }

            //---------------------------------------------------------------------------- List-End ------------------------------------------------------------------------------------
            //-----------------Filter-----------
            $scope.getFilterColumns = function () {
                var filter = myService.getFilterColumns($scope.tableInfo.id);
                filter.then(function (res) {
                    HiddenLoader();
                    $scope.FilterColumnsItem = res.data.result;
                    $scope.isShowFilter = true;
                    $scope.tablePermission.isFilterButton = false;
                    var filterColumnsItem = {
                        filterColumns: res.data.result,
                        typeFilter: {},
                        typeEnds: $scope.typeEnds,
                        textValue: "",
                        typeEndsSeleted: $scope.typeEnds[0]
                    }
                    $scope.filterColumnsChoosed.push(filterColumnsItem);
                },
                    function (res) {
                        AppendToToastr(false, notification, errorNotiFalse);
                    });
            }
            $scope.cancelFilterClick = function () {
                $scope.filterColumnsChoosed = [];
                $scope.isShowFilter = false;
                $scope.tablePermission.isFilterButton = true;
                $scope.GetListData();
            };
            $scope.findNV = function () {
                $scope.GetListData();
            };
            $scope.setShowTypeEnd = function (index) {
                if ($scope.filterColumnsChoosed.length - 1 === index) {
                    return false;
                };
                return true;
            }
            $scope.addFilterColumns = function () {
                var filterColumnsItem = {
                    filterColumns: $scope.FilterColumnsItem,
                    typeEnds: $scope.typeEnds,
                    textValue: "",
                    typeEndsSeleted: $scope.typeEnds[0]
                }
                $scope.filterColumnsChoosed.push(filterColumnsItem);
            }
            $scope.filterColumnsChange = function (filterSelected, index) {
                var type = filterSelected.Type;
                if (type === 2 || type === 3) {
                    $scope.typeFilter = $scope.typeFilterA;
                    $scope.filterColumnsChoosed[index].typeFilter = $scope.typeFilterA;
                } else if (type === 1) {
                    $scope.typeFilter = $scope.typeFilterB;
                    $scope.filterColumnsChoosed[index].typeFilter = $scope.typeFilterB;
                }
            };
            $scope.removeColumnFilterByIndex = function (index) {
                $scope.filterColumnsChoosed.splice(index, 1);
            };
            $scope.setTypeInput = function (type) {
                return "text";
            };
            $scope.getFilterValue = function () {
                var lstObj = $scope.filterColumnsChoosed;
                var stringFilter = "";
                var len = lstObj.length - 1;
                for (var key in lstObj) {
                    var obj = lstObj[key];
                    var tmpFilter = obj.filterSelected.ColumnName + obj.typeFilterSelected.value.replace("#", obj.filterSelected.Type === 3 ? $filter('date')(new Date(obj.textValue), 'yyyy/MM/dd') : obj.textValue) + (parseInt(String(key)) === len ? "" : obj.typeEndsSeleted.value);
                    stringFilter += tmpFilter;
                }

                if ($rootScope.PhongBan != null) {
                    $scope.OrganizationUnitID = $rootScope.PhongBan;
                }
                //kiểm tra xem đang tìm kiếm trên các tab khác hay không, thay filter bằng userid và lọc theo user
                if ($rootScope.giatri != null) {
                    stringFilter = $rootScope.giatri;
                }
                else {
                    stringFilter = $scope.StaffID;
                }
                return stringFilter;
            };
            $scope.showFilterApplyButton = function () {
                var lstObj = $scope.filterColumnsChoosed;
                if (lstObj.length === 0) {
                    return false;
                }
                for (var key in lstObj) {
                    var obj = lstObj[key];
                    if (obj.textValue === "") {
                        return false;
                    }
                }
                return true;
            };
            //--------------------------------------------------------------------------- Filter-End -------------------------------------------------------------------------------------

            $scope.BoSungCong = function () {
                var user = 0;
                var oldString = $scope.FromMonth;
                var str = oldString.replace("/", "-");
                $scope.newString = str;
                $scope.editData.MonthVacation = $scope.newString;
                //lấy dữ liệu các cột và cắt các cột không hiển thị lên
                var getData = myService.GetColumns("/HR_WorkingDaySupplement/TableServerSideGetData");
                getData.then(function (emp) {
                    emp.data.result.splice(0, 1);
                    emp.data.result.splice(12, 1);
                    emp.data.result.splice(12, 1);
                    emp.data.result.splice(12, 1);
                    emp.data.result.splice(12, 1);
                    emp.data.result.splice(12, 1);
                    emp.data.result.splice(12, 1);
                    emp.data.result.splice(12, 1);
                    $scope.Columnpopup = emp.data.result;
                    $scope.dodaiColumnpopup = $scope.Columnpopup.length + 1;
                },
                    function (emp) {
                    });
                var tableUrl = "/HR_WorkingDaySupplement/TableServerSideGetData";
                //lọc các bản tìm kiếm trong tháng đang để dạng bản nháp
                var strimgfilter = " and a.MonthVacation LIKE '%" + $scope.newString + "%' AND a.Status = 0 ";
                var user = 0;
                if ($rootScope.giatri != null) {
                    var user = $rootScope.giatri;
                    strimgfilter += " and a.StaffID = " + user + " ";
                }
                var data = {
                    pageIndex: 1,
                    pageSize: 1000,
                    month: $scope.from[0],
                    year: $scope.from[1],
                    userid: user,
                    status: status,
                    filter: strimgfilter,
                    bosungcong: 1,
                }
                //lấy dữ liệu bổ sung công với user đăng nhập hoặc tìm kiếm
                var getDataTbl = myService.GetTableData(data, tableUrl);
                getDataTbl.then(function (emp) {
                    $scope.employeebosungs = emp.data.employees;
                });
                ShowPopup($,
                    "#bosungcong",
                    $scope.tableInfo.PopupWidth,
                    $scope.tableInfo.PopupHeight);
            }
            //--------------------end------------
            $scope.StaffID = null;
            //Gửi duyệt công theo userid đăng nhập hoặc tìm kiếm
            $scope.submitAddDayMiss = function () {
                if ($scope.employeebosungs != null) {
                    url = "/Timekeeping_TimeSSN/HR_WorkingDaySupplement_Send";
                    $scope.editData = {
                        user: $scope.employeebosungs[0].StaffID
                    };
                    var updateAction = myService.UpdateData(url, $scope.editData);
                    updateAction.then(function (res) {
                        if (res.data.result.IsSuccess) {
                            $scope.CloseForm();
                        }
                        AppendToToastr(res.data.result.IsSuccess, notification, res.data.result.Message, 500, 5000);
                    },
                        function (res) {
                            AppendToToastr(false, notification, errorNotiFalse);
                        });
                }
            }
            //chuyển đổi định dạng ngày
            function formatDate(date) {
                var day = date.slice(0, 2);
                var month = date.slice(3, 5);
                var year = date.slice(6, 10);
                var newdate = year + '-' + month + '-' + day;

                return newdate;
            }
            function tinhcong2thoigian(fromtime, totime) {
                var x1, x2, cong;

                if (fromtime != null) {
                    var fromTimeHour = parseInt(fromtime.slice(0, 2));
                    var fromTimeMinute = parseInt(fromtime.slice(3, 5));
                    if ($scope.editData.Type == 5 && fromTimeHour == 12) {
                        x1 = fromTimeHour + 1;
                    }
                    else
                        x1 = fromTimeHour + fromTimeMinute / 60;
                }
                if (totime != null) {
                    var toTimeHour = parseInt(totime.slice(0, 2));
                    var toTimeMinute = parseInt(totime.slice(3, 5));
                    if (toTimeHour >= 13 && fromTimeHour < 12 && $scope.editData.Type == 5) {
                        x2 = (toTimeHour - 1) + toTimeMinute / 60;
                    }
                    else if (toTimeHour == 12 && $scope.editData.Type == 5) {
                        x2 = toTimeHour;
                    }
                    else
                        x2 = toTimeHour + toTimeMinute / 60;
                }
                var temp = x2 - x1;
                if ($scope.editData.Type != 5) {
                    if (fromtime == '12:01:00' && totime == '12:59:00') {
                        cong = 0;
                    }
                    else if (fromtime == '09:00:00' && totime == '12:00:00') {
                        cong = 0.5;
                    }
                    else if (fromtime == '09:00:00' && totime == '18:00:00') {
                        cong = 1;
                    }
                    else if (fromtime == '09:00:00' && totime == '17:30:00') {
                        cong = 1;
                    }
                    else if (fromtime == '08:30:00' && totime == '17:30:00') {
                        cong = 1;
                    }
                    else if (temp < 3.5 && temp > 0) {
                        cong = 0.25;
                    }
                    else if (temp >= 3.5 && temp <= 6) {
                        cong = 0.5;
                    }
                    else if (temp > 6 && temp <= 9) {
                        cong = 0.75;
                    }
                    else if (temp > 9) {
                        cong = 1;
                    }
                    else {
                        cong = 0;
                    }
                }
                else {
                    cong = temp / 2 * 0.25;
                    cong = Math.round(cong * 10000000000) / 10000000000
                    if (cong > 1) {
                        cong = 1;
                    }
                }
                return cong;
            }
            //-----------Save-Bo-Sung-Cong-----------
            $scope.SaveAdditionalWork = function (form) {
                if (form.$valid) {
                    if ($scope.TimeInvalid) {
                        $scope.messagevalidate = "Thời gian bổ sung không hợp lệ.";
                        $scope.messagevalidateEN = "Time for working time updating is not valid.";
                        if (currentLanguage == 5)
                            AppendToToastr(false, notification, $scope.messagevalidate);
                        else
                            AppendToToastr(false, notification, $scope.messagevalidateEN);
                        return;
                    }
                    if ($scope.LoiChonNgay == true) {
                    }
                    else {
                        $scope.LoiChuaNhap = false; //xóa lỗi khi lưu lại
                        $scope.LoiChonBoSungCong = false; //lỗi validate chọn ngày tháng
                        var startdate = "";
                        var enddate = "";
                        var oldString = $scope.FromMonth;
                        var str = oldString.replace("/", "-");
                        $scope.newString = str;
                        $scope.editData.MonthVacation = $scope.newString; //khai báo tháng năm bổ sung
                        var temp = 0;
                        if ($scope.editData.TimeMissDate != null) {
                            $scope.CheckHourOff = $scope.editData.TimeMissDate.substr(-5, 5); //lấy giờ đi muộn về sớm thực tế
                            $scope.editData.TimeOfActual = $scope.editData.TimeMissDate.substr(-5, 5);
                        }
                        if ($scope.editData.HourOff != null) {
                            //cắt chuỗi so sánh số phút đi muộn về sớm đã nhập đúng chưa
                            var minitus1 = parseInt($scope.editData.HourOff.slice(0, 2) * 60) + parseInt($scope.editData.HourOff.slice(3, 5)); //giờ đi muộn về sớm bổ sung
                            var minitus2 = parseInt($scope.CheckHourOff.slice(0, 2) * 60) + parseInt($scope.CheckHourOff.slice(3, 5));
                            if (isNaN(minitus1) || isNaN(minitus2)) {
                                temp = 1;
                            }
                            else
                                temp = minitus1 - minitus2;
                        }
                        if (temp > 0 && $scope.LoiChuaNhap != true) {//validate lớp 1 kiểm tra lỗi đi muộn về sớm nhập đúng hay sai, qua được đến lớp 2, nếu không sẽ dừng lại
                            $scope.LoiBoSungCong = true; //kiểm tra lỗi bổ sung đi muộn về sớm nhiều hơn so với giờ đi muộn về sớm thực tế
                        }
                        else if (temp <= 0 && $scope.LoiChuaNhap != true) {
                            $scope.Data = [];
                            $scope.LoiBoSungCong = false; //xóa lỗi khi chọn đúng thời gian đi muộn về sớm không lớn hơn thời gian đi muộn về sớm thực tế
                            url = "/Timekeeping_TimeSSN/SaveBoSungCong";
                            if ($scope.autoid != 0) {
                                $scope.editData.AutoID = $scope.autoid;
                            }
                            if ($rootScope.giatri != null) {
                                $scope.editData.StaffID = $rootScope.giatri;
                            }
                            if ($scope.editData.Type == 1 && $scope.editData.HourOff == '00:00') {
                                $scope.messagevalidate = errorTimeIsZero;
                                $scope.LoiChuaNhap = true;
                            }
                            if ($scope.LoiChuaNhap == false) {//validate lớp 2, lớp kiểm tra trước khi truyền dữ liệu, qua lớp 2 đến lớp 3 kiểm tra theo các luật và dữ liệu
                                //cập nhật lại dữ liệu sau khi đã qua hết validate
                                if ($scope.editData.Type == 2 || $scope.editData.Type == 3) {
                                    startdate = formatDate($scope.editData.fromdate);
                                    enddate = formatDate($scope.editData.todate);
                                    var x = $scope.editData.fromdate;
                                    $scope.editData.Date = x.split("/").reverse().join("-");
                                    //cập nhật các trường dữ liệu thời gian cho đúng kiểu yyyy-mm-dd hh:mm:ss.000
                                    $scope.editData.FromTime = $scope.editData.Date + " " + $scope.editData.FromTimeStart + ".000";
                                    $scope.editData.ToTime = $scope.editData.Date + " " + $scope.editData.ToTimeStart + ".000";
                                    $scope.editData.PercentPayrollID = null;
                                }
                                if ($scope.editData.Type == 1) {
                                    var x = $scope.editData.TimeMissDate.substr(-29, 10);
                                    $scope.editData.Date = x.split("/").reverse().join("-");
                                    //cập nhật thời gian xin đi muộn về sớm đúng dạng yyyy-mm-dd hh:mm:ss.000                              
                                    $scope.editData.FullHourOff = $scope.FromMonth2 + " " + $scope.editData.HourOff + ":00.000";
                                    $scope.editData.PercentPayrollID = null;
                                }
                                if ($scope.editData.Type == 4 || $scope.editData.Type == 5) {
                                    var x = "";
                                    if ($scope.editData.Type == 5) {
                                        x = $scope.editData.fromdate.substr(-10, 10);
                                    }
                                    else {
                                        x = $scope.editData.fromdate.substr(-21, 10);
                                    }
                                    $scope.editData.Date = x.split("/").reverse().join("-");
                                    if ($scope.editData.Type == 4) {
                                        startdate = formatDate($scope.editData.fromdate.substr(-21, 10));
                                        enddate = formatDate($scope.editData.todate.substr(-21, 10));
                                        //cập nhật các trường dữ liệu thời gian cho đúng kiểu yyyy-mm-dd hh:mm:ss.000
                                        $scope.editData.FromTime = $scope.editData.Date + " " + $scope.editData.FromTimeStart + ".000";
                                        $scope.editData.ToTime = $scope.editData.Date + " " + $scope.editData.ToTimeStart + ".000";
                                    } else {
                                        startdate = formatDate($scope.editData.fromdate);
                                        enddate = formatDate($scope.editData.todate);
                                        //cập nhật các trường dữ liệu thời gian cho đúng kiểu yyyy-mm-dd hh:mm:ss.000
                                        $scope.editData.FromTime = $scope.editData.Date + " " + $scope.editData.FromTimeStart + ":00.000";
                                        $scope.editData.ToTime = $scope.editData.Date + " " + $scope.editData.ToTimeStart + ":00.000";
                                        $scope.editData.PercentPayrollID = $scope.editData.PercentPayrollID.toString();
                                    }
                                }
                                //kiểm tra đang bổ sung 1 ngày hay nhiều ngày
                                if ($scope.oneday == false && $scope.autoid == 0) {
                                    //lấy danh sách các ngày lưu vào mảng
                                    
                                    const dateToString =
                                        d => `${d.getFullYear()}-${('00' + (d.getMonth() + 1)).slice(-2)}-${('00' +
                                            d.getDate()).slice(-2)}`
                                    let initialTime = new Date(startdate),
                                        endTime = new Date(enddate),
                                        daylist = [],
                                        dayMillisec = 24 * 60 * 60 * 1000;
                                    for (let q = initialTime; q <= endTime; q = new Date(q.getTime() + dayMillisec)) {

                                        const myDate = new Date(Date.parse(q));
                                        daylist.push(dateToString(myDate));
                                    }
                                    var len = daylist.length;
                                    var temp = $scope.editData.DayOff; //lưu ngày công ban đầu
                                    var tempfromtime = $scope.editData.FromTime; //lưu thời gian đến
                                    var temptotime = $scope.editData.ToTime; //lưu thời gian về
                                    for (var i = 0; i < len; i++) {
                                        $scope.editData.Date = daylist[i];
                                        if (i == 0) { //ngày bổ sung đầu tiên lấy từ giờ bắt đầu bổ sung đến hết ngày
                                            GetHour();
                                            //lấy giờ đến, giờ về
                                            var totime = $scope.getListHour[4].EndTime;
                                            if ($scope.editData.Type == 5) {
                                                $scope.editData.FromTime = $scope.editData.Date + " " + tempfromtime + ":00.000";//giờ bắt đầu bổ sung
                                                $scope.editData.ToTime = $scope.editData.Date + " " + totime + ":00.000";//giờ kết thúc bổ sung tính bằng giờ làm việc cuối cùng của ngày đó
                                            } else {
                                                $scope.editData.FromTime = tempfromtime;//giờ bắt đầu bổ sung
                                                $scope.editData.ToTime = $scope.editData.Date + " " + totime + ".000";//giờ kết thúc bổ sung tính bằng giờ làm việc cuối cùng của ngày đó
                                            }
                                            $scope.editData.DayOff = tinhcong2thoigian(tempfromtime.slice(11, 19), totime);
                                            if ($scope.editData.Type == 4) {
                                                $scope.editData.DayOff = $scope.editData.DayOff * ($scope.editData.PercentPayrollID / 100 )
                                            }
                                            var obj = angular.copy($scope.editData);
                                            $scope.Data.push(obj);
                                           
                                        }
                                        else if (i < len - 1 && $scope.LoiChuaNhap == false) {
                                            GetHour();
                                            //lấy giờ đến, giờ về
                                            var fromtime = $scope.getListHour[0].StartTime;
                                            var totime = $scope.getListHour[4].EndTime;
                                            if ($scope.editData.Type == 5) {
                                                $scope.editData.FromTime = $scope.editData.Date + " " + fromtime + ":00.000";//giờ bắt đầu bổ sung tính bằng giờ làm việc đầu tiên của ngày đó
                                                $scope.editData.ToTime = $scope.editData.Date + " " + totime + ":00.000";//giờ kết thúc bổ sung tính bằng giờ làm việc cuối cùng của ngày đó
                                            } else {
                                                $scope.editData.FromTime = $scope.editData.Date + " " + fromtime + ".000";//giờ bắt đầu bổ sung tính bằng giờ làm việc đầu tiên của ngày đó
                                                $scope.editData.ToTime = $scope.editData.Date + " " + totime + ".000";//giờ kết thúc bổ sung tính bằng giờ làm việc cuối cùng của ngày đó
                                            }
                                            $scope.editData.DayOff = 1;
                                            if ($scope.editData.Type == 4) {
                                                $scope.editData.DayOff = $scope.editData.DayOff * ($scope.editData.PercentPayrollID / 100)
                                            }
                                            var obj = angular.copy($scope.editData);
                                            $scope.Data.push(obj);
                                            
                                        }
                                        else if ($scope.LoiChuaNhap == false) {
                                            var fromtime = $scope.getListHour[0].StartTime;
                                            if ($scope.editData.Type == 5) {
                                                $scope.editData.FromTime = $scope.editData.Date + " " + fromtime + ":00.000";
                                                $scope.editData.ToTime = $scope.editData.Date + " " + temptotime + ":00.000";
                                            } else {
                                                $scope.editData.FromTime = $scope.editData.Date + " " + fromtime + ".000";;
                                                $scope.editData.ToTime =$scope.editData.Date + " " + temptotime.slice(11, 19);
                                            }
                                            $scope.editData.DayOff = tinhcong2thoigian(fromtime, temptotime.slice(11, 19));
                                            if ($scope.editData.Type == 4) {
                                                $scope.editData.DayOff = $scope.editData.DayOff * ($scope.editData.PercentPayrollID / 100)
                                            }
                                            var obj = angular.copy($scope.editData);
                                            $scope.Data.push(obj);
                                           
                                        }
                                    }
                                    $scope.editData.DayOff = temp;
                                   
                                }//thêm mới nhiều ngày
                                else if ($scope.oneday == false && $scope.autoid != 0) {//sửa một ngày thành nhiều ngày
                                    $scope.messagevalidate = errorWork;
                                    $scope.LoiChuaNhap = true;
                                }
                               if ($scope.LoiChuaNhap == false) {
                                   $scope.Data.push($scope.editData);
                                   debugger
                                   var dt = Loading();
                                    var updateAction = myService.UpdateData("/Timekeeping_TimeSSN/SaveListWorkingday", $scope.Data);
                                    updateAction.then(function (res) {
                                        if (res.data.result.Message == "0") {
                                            $scope.messagevalidate = errorWorkAlreadyExists;
                                            $scope.LoiChuaNhap = true;
                                        }
                                        else if (res.data.result.Message == "1") {//kiểm tra có 3 lệnh quên chấm công trên 1 tháng
                                            $scope.messagevalidate = errorReasonToForgetToTimekeeping;
                                            $scope.LoiChuaNhap = true;
                                        }
                                        else if (res.data.result.Message == "2") {
                                            $scope.messagevalidate = errorAdditionalPermissionsExceeded;
                                            $scope.LoiChuaNhap = true;
                                        }
                                        else if (res.data.result.Message == "3") {
                                            $scope.messagevalidate = errorPastThirtyMinutes;
                                            $scope.LoiChuaNhap = true;
                                        }
                                        else if (res.data.result.Message == "4") {
                                            $scope.messagevalidate = errorCreatingMerit;
                                            $scope.LoiChuaNhap = true;
                                        }
                                        else if (res.data.result.Message == "6") {
                                            $scope.messagevalidate = errorTardinessLeaveEarly;
                                            $scope.LoiChuaNhap = true;
                                        }
                                        else {
                                            AppendToToastr(true,
                                                notification,
                                                successfulUpdate,
                                                500,
                                                5000);
                                            //$scope.GetListData();
                                            $scope.CloseFormAdd(form);
                                            $scope.BoSungCong();
                                            form.$dirty = false;
                                            form.$invalid = false;
                                            form.$submitted = false;
                                            form.$valid = false;
                                        }
                                        $scope.Data = [];
                                        dt.finish();
                                    });
                                }
                            }
                            else {//không vượt qua validate lớp 2
                                $scope.LoiChuaNhap = true;
                            }
                        }
                    }
                }
                else {
                    AppendToToastr(false, notification, errorFullInformation);
                }
            }
            //kiểm tra đã thay đổi giá trị ngày đi muộn về sớm
            $scope.changeToDimuon = function () {
                $scope.LoiChuaNhap = false; //lỗi validate khi bấm tạo bổ sung công
                $scope.LoiBoSungCong = false; //lỗi validate nhập số phút đi muộn về sớm lơn hơn quy định
                $scope.LoiChonBoSungCong = false; //lỗi validate chọn ngày tháng
                if ($scope.editData.TimeMissDate != null) {
                    $scope.editData.HourOff = $scope.editData.TimeMissDate.substr(-5, 5);
                    $scope.editData.TimeOfActual = $scope.editData.TimeMissDate.substr(-5, 5);
                    $scope.CheckHourOff = $scope.editData.TimeMissDate.substr(-5, 5);
                }
            }
            //hàm lấy giờ theo useid đăng nhập hoặc tìm kiếm
            function GetHour() {
                var userid = 0;
                if ($rootScope.giatri != null) {
                    userid = $rootScope.giatri;
                }
                var data = {
                    url: "/Timekeeping_TimeSSN/HR_WorkingDay_GetHour?userid=" + userid
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getListHour = res.data.result;
                }, function (res) {
                    $scope.msg = "Error";
                })
            }
            //hàm lấy khách hàng theo userid đăng nhập hoặc tìm kiếm
            function GetCustomers() {
                var userid = 0;
                if ($rootScope.giatri != null) {
                    userid = $rootScope.giatri;
                }
                var data = {
                    url: "/Timekeeping_TimeSSN/GetCustomers?userid=" + userid
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getListCustomers = res.data.result;
                }, function (res) {
                    $scope.msg = "Error";
                })
            }
            //hàm lấy ngày theo userid đăng nhập hoặc tìm kiếm
            function GetDateApproval() {
                var userid = 0;
                if ($rootScope.giatri != null) {
                    userid = $rootScope.giatri;
                }
                var data = {
                    url: "/Timekeeping_TimeSSN/GetDateApproval?userid=" + userid
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.listday = res.data.result;
                }, function (res) {
                    $scope.msg = "Error";
                })
            }
            //kiểm tra đã thay đổi khách hàng
            $scope.changeCustomer = function () {
                GetCustomerContacts($scope.editData.CustomerID);
            }
            //hàm lấy tên liên hệ theo id khách hàng
            function GetCustomerContacts(customerid) {
                var data = {
                    url: "/Timekeeping_TimeSSN/GetCustomerContacts?customerid=" + customerid
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getListCustomerContacts = res.data.result;
                }, function (res) {
                    $scope.msg = "Error";
                })
            }
            function KindOfProposal() {
                var data = {
                    url: "/Common/GetDataByGloballistnotTree?parentid=" + 84
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getListKindOfProposal = res.data.result;
                }, function (res) {
                    $scope.msg = "Error";
                })
            }
            //hàm lấy mục đích
            function ListMucDich() {
                var data = {
                    url: "/Common/GetDataByGloballistnotTree?parentid=" + 1887
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getListMucDich = res.data.result;
                }, function (res) {
                    $scope.msg = "Error";
                })
            }
            //hàm lấy lý do
            function Reason() {
                var data = {
                    url: "/Common/GetDataByGloballistnotTree?parentid=" + $scope.GlobalListWhereParentID.Reason
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getListReason = res.data.result;
                }, function (res) {
                    $scope.msg = "Error";
                })
            }
            //hàm lấy trạng thái bổ sung
            function Status() {
                var data = {
                    url: "/Common/GetDataByGloballistnotTree?parentid=" + $scope.GlobalListWhereParentID.Status
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getListStatus = res.data.result;
                }, function (res) {
                    $scope.msg = "Error";
                })
            }
            //hàm lấy 
            function Type() {
                var data = {
                    url: "/Common/GetDataByGloballistnotTree?parentid=" + 84
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getType = res.data.result;
                }, function (res) {
                    $scope.msg = "Error";
                })
            }

            function getPercent() {
                var data = {
                    url: "/Common/GetDataByGloballistnotTree?parentid=" + 1589
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getPercent = res.data.result;
                }, function (res) {
                    $scope.msg = "Error";
                })
            }

            $scope.chinhfromtime = 0;
            $scope.chinhtotime = 0;
            //kiểm tra đã thay đổi ngày tháng
            $scope.changeFromTime = function () {
                $scope.LoiChuaNhap = false; //lỗi validate khi bấm tạo bổ sung công
                $scope.LoiBoSungCong = false; //lỗi validate nhập số phút đi muộn về sớm lơn hơn quy định
                $scope.LoiChonBoSungCong = false; //lỗi validate chọn ngày tháng
                $scope.oneday = true; //kiểm tra đang bổ sung 1 hay nhiều ngày
                var fromdate = $scope.editData.fromdate;
                var todate = $scope.editData.todate;
                if ($scope.editData.Type == 4) {
                    if ($scope.editData.fromdate != null) {
                        fromdate = $scope.editData.fromdate.substr(-21, 10);
                    }
                    if ($scope.editData.todate != null) {
                        todate = $scope.editData.todate.substr(-21, 10);
                    }
                }
                if (fromdate != todate) {
                    $scope.oneday = false;
                }
                else {
                    $scope.oneday = true;
                }
                if ($scope.editData.Type == 5)
                    $scope.oneday = true;
                GetHour();
                if ($scope.showlamthem == true) {
                    if ($scope.editData.FromTimeStart != "" && $scope.editData.FromTimeStart != null && $scope.editData.ToTimeStart != "" && $scope.editData.ToTimeStart != null) {
                        $scope.LoiChonBoSungCong = false;
                    }
                    else {
                        $scope.LoiChonBoSungCong = true;
                    }
                }
                //if ($scope.chinhfromtime == 0) {
                //    if ($scope.editData.Type != 5) {
                //        $scope.editData.FromTimeStart = $scope.getListHour[0].StartTime;
                //        $scope.chinhfromtime++;
                //    }
                //}
                $scope.readonlyFromTime = false;
                if ($scope.editData.Type == 5) {
                    $scope.editData.todate = $scope.editData.fromdate;
                }
                var fromdate = $scope.editData.fromdate;
                var todate = $scope.editData.todate;

                if ($scope.editData.Type == 1 || $scope.editData.Type == 4 || $scope.editData.Type == 5) {
                    fromdate = fromdate != null ? fromdate.substr(-21, 10) : "";
                    todate = todate != null ? todate.substr(-21, 10) : "";
                    if (fromdate != null && todate != null) {
                        var x = fromdate.split('/');
                        var y = todate.split('/');
                        var startDate = x[2] + "-" + x[1] + "-" + x[0];
                        var toDate = y[2] + "-" + y[1] + "-" + y[0]
                        var a = new Date(startDate);
                        var b = new Date(toDate);
                        var c = (b - a);
                        //$scope.songaytinh = c / (1000 * 60 * 60 * 24);
                        $scope.songaytinh = Math.round(Math.abs(((b - a) / (1000 * 60 * 60 * 24))));
                    }
                }
                else if ($scope.editData.Type == 2 || $scope.editData.Type == 3) {
                    if (fromdate != null && todate != null) {
                        var x = fromdate.split('/');
                        var y = todate.split('/');
                        var a = new Date(x[2], x[1], x[0]);
                        var b = new Date(y[2], y[1], y[0]);
                        var c = (b - a);
                        $scope.songaytinh = c / (1000 * 60 * 60 * 24);
                    }
                }
                $scope.changeToTimeOut();
            }
            //kiểm tra đã thay đổi ngày tháng
            $scope.changeToTime = function () {
                $scope.LoiChuaNhap = false; //lỗi validate khi bấm tạo bổ sung công
                $scope.LoiBoSungCong = false; //lỗi validate nhập số phút đi muộn về sớm lơn hơn quy định
                $scope.LoiChonBoSungCong = false; //lỗi validate chọn ngày tháng
                $scope.oneday = true; //kiểm tra đang bổ sung 1 hay nhiều ngày
                var fromdate = $scope.editData.fromdate;
                var todate = $scope.editData.todate;
                if ($scope.editData.Type == 4) {
                    if ($scope.editData.fromdate != null) {
                        fromdate = $scope.editData.fromdate.substr(-21, 10);
                    }
                    if ($scope.editData.todate != null) {
                        todate = $scope.editData.todate.substr(-21, 10);
                    }
                }
                if (fromdate != todate) {
                    $scope.oneday = false;
                }
                else {
                    $scope.oneday = true;
                }
                //GetHour();
                //if ($scope.chinhtotime == 0) {
                //    $scope.editData.ToTimeStart = $scope.getListHour[0].EndTime;
                //    $scope.chinhtotime++;
                //}
                $scope.readonlyToTime = false;
                var fromdate = $scope.editData.fromdate;
                var todate = $scope.editData.todate;
                if ($scope.editData.Type == 1 || $scope.editData.Type == 4 || $scope.editData.Type == 5) {
                    fromdate = fromdate != null ? fromdate.substr(-21, 10) : "";
                    todate = todate != null ? todate.substr(-21, 10) : "";
                    if (fromdate != null && todate != null) {
                        var x = fromdate.split('/');
                        var y = todate.split('/');
                        var startDate = x[2] + "-" + x[1] + "-" + x[0];
                        var toDate = y[2] + "-" + y[1] + "-" + y[0]
                        var a = new Date(startDate);
                        var b = new Date(toDate);
                        var c = (b - a);
                        //$scope.songaytinh = c / (1000 * 60 * 60 * 24);
                        $scope.songaytinh = Math.round(Math.abs(((b - a) / (1000 * 60 * 60 * 24))));
                    }
                }
                else if ($scope.editData.Type == 2 || $scope.editData.Type == 3) {
                    if (fromdate != null && todate != null) {
                        var x = fromdate.split('/');
                        var y = todate.split('/');
                        var a = new Date(x[2], x[1], x[0]);
                        var b = new Date(y[2], y[1], y[0]);
                        var c = (b - a);
                        $scope.songaytinh = c / (1000 * 60 * 60 * 24);
                    }
                }
                $scope.changeToTimeOut();
         }
            //kiểm tra đã thay đổi hệ số làm thêm
            $scope.changePercentPayroll = function () {
                if ($scope.editData.Type == 5) {
                    $scope.songaytinh = 1;
                    $scope.luucongnhan = tinhcong2thoigian($scope.editData.FromTimeStart, $scope.editData.ToTimeStart);                   
                    $scope.editData.DayOff = $scope.luucongnhan * ($scope.editData.PercentPayrollID / 100) * $scope.songaytinh;
                }
                if ($scope.editData.Type == 4) {
                    $scope.changeToTimeOut();
                }
            }
            //kiểm tra đã thay đổi loại đề nghị
            $scope.changeType = function (form) {
                //Tắt thông báo đang hiện
                form.$dirty = false;
                form.$invalid = false;
                form.$submitted = false;
                form.$valid = false;

                $scope.LoiChuaNhap = false; //lỗi validate khi bấm tạo bổ sung công
                $scope.LoiBoSungCong = false; //lỗi validate nhập số phút đi muộn về sớm lơn hơn quy định
                $scope.LoiChonBoSungCong = false; //lỗi validate chọn ngày tháng
                var temptype = $scope.editData.Type; //lưu tạm type đã chọn
                getListDate();
                if ($scope.editData.Type != 1) {
                    $scope.showone = true;
                    $scope.anlamthem = false;
                    if ($scope.editData.Type == 5) {
                        $scope.showlamthem = true;
                        $scope.editData.FromTimeStart = null;
                        $scope.editData.ToTimeStart = null;
                    }
                    else
                        $scope.showlamthem = false;
                }
                else {
                    $scope.showone = false;
                }
                $scope.editData = {}; //khi thay đổi type cho dữ liệu cũ về null
                $scope.editData.Type = temptype;
                $scope.editData.Status = "0";
                $scope.editData.DayOff = "0";
                $scope.editData.PercentPayrollID = "100"
            }
            $scope.TimeInvalid = false;
            //kiểm tra đã thay đổi giờ phút
            $scope.changeToTimeOut = function () {
                $scope.editData.DayOff = 0;
                var timeFrom = moment($scope.editData.FromTimeStart, 'HH:mm:ss').add(1, 'seconds');
                var timeTo = moment($scope.editData.ToTimeStart, 'HH:mm:ss').add(-1, 'seconds');
                if (!timeFrom.isValid() || !timeTo.isValid() || (timeTo.isBefore(timeFrom))) {
                    $scope.TimeInvalid = true;
                    return;
                }
                else {
                    $scope.TimeInvalid = false;
                }
                if ($scope.editData.Type != 5) {
                    if (timeFrom.isAfter(moment('12:00:00', 'HH:mm:ss'))
                        && timeFrom.isBefore(moment('13:00:30', 'HH:mm:ss'))
                        && timeTo.isAfter(moment('12:00:00', 'HH:mm:ss'))
                        && timeTo.isBefore(moment('13:00:30', 'HH:mm:ss'))
                        ) {
                        $scope.editData.DayOff = $scope.songaytinh;
                    }
                    else
                        if (timeFrom.isBefore(moment($scope.HrWorkingDay.MorningHourMid, 'HH:mm:ss'))) {
                            if (timeTo.isBefore(moment($scope.HrWorkingDay.MorningHourMid, 'HH:mm:ss'))) {
                                $scope.editData.DayOff = (0.25 + $scope.songaytinh);
                                
                            }
                            else
                                if (timeTo.isBefore(moment($scope.HrWorkingDay.AfernoonHourStart, 'HH:mm:ss'))) {
                                    $scope.editData.DayOff = (0.5 + $scope.songaytinh);
                                   
                                }
                                else
                                    if (timeTo.isBefore(moment($scope.HrWorkingDay.AfernoonHourMid, 'HH:mm:ss'))) {
                                        $scope.editData.DayOff = (0.75 + $scope.songaytinh);
                                    }
                                    else {
                                        $scope.editData.DayOff = (1 + $scope.songaytinh);
                                    }
                        }
                        else
                            if (timeFrom.isBefore(moment($scope.HrWorkingDay.MorningHourEnd, 'HH:mm:ss'))) {
                                if (timeTo.isBefore(moment($scope.HrWorkingDay.AfernoonHourStart, 'HH:mm:ss'))) {
                                    $scope.editData.DayOff = 0.25 + $scope.songaytinh;
                                }
                                else
                                    if (timeTo.isBefore(moment($scope.HrWorkingDay.AfernoonHourMid, 'HH:mm:ss'))) {
                                        $scope.editData.DayOff = 0.5 + $scope.songaytinh;
                                    }
                                    else {
                                        $scope.editData.DayOff = 0.75 + $scope.songaytinh;
                                    }
                            }
                            else

                                if (timeFrom.isBefore(moment($scope.HrWorkingDay.AfernoonHourMid, 'HH:mm:ss'))) {
                                    if (timeTo.isBefore(moment($scope.HrWorkingDay.AfernoonHourMid, 'HH:mm:ss'))) {
                                        $scope.editData.DayOff = 0.25 + $scope.songaytinh;
                                    }
                                    else {
                                        $scope.editData.DayOff = 0.5 + $scope.songaytinh;
                                    }
                                }
                                else
                                    if (timeFrom.isBefore(moment($scope.HrWorkingDay.AfternoonHourEnd, 'HH:mm:ss'))) {
                                        $scope.editData.DayOff = 0.25 + $scope.songaytinh;
                                    }
                                    else {
                                        $scope.editData.DayOff = $scope.songaytinh;
                                    }
                }
                else {
                    $scope.editData.DayOff = (timeTo.hour() + timeTo.minute() / 60 - timeFrom.hour() - timeFrom.minute() / 60) / 2 * 0.25;
                    if ($scope.editData.DayOff > 1) {
                        $scope.editData.DayOff = 1;
                    }
                }
                $scope.luucongnhan = angular.copy($scope.editData.DayOff);
                if ($scope.editData.DayOff < 0) {
                    $scope.editData.DayOff = 0;
                    $scope.LoiChonNgay = true;
                }
                else {
                    $scope.LoiChonNgay = false;
                }
                if ($scope.editData.Type == 5) {
                    $scope.changePercentPayroll();
                }
                if ($scope.editData.Type == 4) {
                    $scope.editData.DayOff = $scope.editData.DayOff * ($scope.editData.PercentPayrollID / 100)
                }
            }
            $scope.CallData(); //lấy dữ liệu cho các combobox
            ListMucDich(); //lấy dữ liệu cho combobox mục đích
            Type(); //lấy dữ liệu cho combobox loại đề nghị
            getPercent();
            $scope.changePercentPayroll();
        });

    app.directive('convertToNumber', function () {
        return {
            require: 'ngModel',
            link: function (scope, element, attrs, ngModel) {
                ngModel.$parsers.push(function (val) {
                    return val !== null ? parseInt(val, 10) : null;
                });
                ngModel.$formatters.push(function (val) {
                    return val !== null ? '' + val : null;
                });
            }
        };
    });
    app.directive('convertDropdown', function () {
        return {
            require: 'ngModel',
            link: function (scope, element, attrs, ngModel) {
                ngModel.$parsers.push(function (val) {
                    return val == 0 ? null : val;
                });
                ngModel.$formatters.push(function (val) {
                    return val == 0 ? null : val;
                });
            }
        };
    });
    app.directive("formatDate", function () {
        return {
            require: 'ngModel',
            link: function (scope, elem, attr, modelCtrl) {

                modelCtrl.$formatters.push(function (modelValue) {
                    return modelValue == null ? null : new Date(modelValue);
                });
            }
        }
    });
    app.directive('onlyNumbers', function () {
        return {
            restrict: 'A',
            link: function (scope, elm, attrs, ctrl) {
                elm.on('keydown', function (event) {
                    if (event.shiftKey) { event.preventDefault(); return false; }
                    if ([8, 13, 27, 37, 38, 39, 40].indexOf(event.which) > -1) {
                        // backspace, enter, escape, arrows
                        return true;
                    } else if (event.which >= 49 && event.which <= 57) {
                        // numbers
                        return true;
                    } else if (event.which >= 96 && event.which <= 105) {
                        // numpad number
                        return true;
                    }
                        // else if ([110, 190].indexOf(event.which) > -1) {
                        //     // dot and numpad dot
                        //     return true;
                        // }
                    else {
                        event.preventDefault();
                        return false;
                    }
                });
            }
        }
    });
    app.directive('currencyMask', function () {
        return {
            restrict: 'A',
            require: 'ngModel',
            link: function (scope, element, attrs, ngModelController) {

                var formatNumber = function (value) {

                    value = value.toString();
                    value = value.replace(/[^0-9\.]/g, "");
                    var parts = value.split('.');
                    parts[0] = parts[0].replace(/\d{1,3}(?=(\d{3})+(?!\d))/g, "$&,");
                    if (parts[1] && parts[1].length > 2) {
                        parts[1] = parts[1].substring(0, 2);
                    }

                    return parts.join(".");
                };
                var applyFormatting = function () {
                    var value = element.val();
                    var original = value;
                    if (!value || value.length == 0) {
                        return
                    }
                    value = formatNumber(value);
                    if (value != original) {
                        element.val(value);
                        element.triggerHandler('input')
                    }
                };
                element.bind('keyup', function (e) {
                    var keycode = e.keyCode;
                    var isTextInputKey =
                        (keycode > 47 && keycode < 58) || // number keys
                        keycode == 32 || keycode == 8 || // spacebar or backspace
                        (keycode > 64 && keycode < 91) || // letter keys
                        (keycode > 95 && keycode < 112) || // numpad keys
                        (keycode > 185 && keycode < 193) || // ;=,-./` (in order)
                        (keycode > 218 && keycode < 223); // [\]' (in order)
                    if (isTextInputKey) {
                        applyFormatting();
                    }
                });
                element.bind('blur', function (evt) {
                    if (angular.isDefined(ngModelController.$modelValue)) {
                        var val = ngModelController.$modelValue.split('.');
                        if (val && val.length == 1) {
                            if (val != "") {
                                ngModelController.$setViewValue(val + '.00');
                                ngModelController.$render();
                            }
                        } else if (val && val.length == 2) {
                            if (val[1] && val[1].length == 1) {
                                ngModelController.$setViewValue(val[0] + '.' + val[1] + '0');
                                ngModelController.$render();
                            } else if (val[1].length == 0) {
                                ngModelController.$setViewValue(val[0] + '.00');
                                ngModelController.$render();
                            }
                            applyFormatting();
                        }
                    }
                })
                ngModelController.$parsers.push(function (value) {
                    if (!value || value.length == 0) {
                        return value;
                    }
                    value = value.toString();
                    value = value.replace(/[^0-9\.]/g, "");
                    return value;
                });
                ngModelController.$formatters.push(function (value) {
                    if (!value || value.length == 0) {
                        return value;
                    }
                    value = formatNumber(value);
                    return value;
                });
            }
        };
    });
    app.directive('convertClockpicker', function () {
        return {
            restrict: 'A',
            require: 'ngModel',
            link: function (scope, element, attrs, ngModelController) {
                $('.clockpicker').clockpicker({
                    align: 'right',
                    autoclose: true
                });
            }
        };
    });
    app.directive('convertTime', function () {
        return {
            restrict: 'A',
            require: 'ngModel',
            link: function (scope, element, attrs, ngModelController) {
                $(function () { $('.timeselect').inputmask({ mask: "99:99" }); });
            }
        };
    });
}
