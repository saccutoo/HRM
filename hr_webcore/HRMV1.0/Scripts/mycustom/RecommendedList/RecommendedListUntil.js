function BuildTable3(appName, controllerName, tableUrl) {
    app.controller(controllerName,
        function ($scope, myService, $filter, $rootScope, $window, $http, $compile, $q) {
            // $scope.test = $filter('mm/dd/yyyy')("2018/01/01", 'yyyy/MM/dd');
            $scope.maxSize = 5; // Limit number for pagination display number.
            $scope.totalCount = 0; // Total number of items in all pages. initialize as a zero.
            $scope.pageIndex = 1; // Current page number. First page is 1.-->
            $scope.pageSizeSelected = 5;
            $scope.isShowFilter = false; $scope.typeEnds = [{ name: "Và", nameEN: "And", value: " and " }, { name: "Hoặc", nameEN: "Or", value: " or " }];
            $scope.typeFilterA = [{ name: ">", nameEN: ">", value: " > '#' " }, { name: "<", nameEN: "<", value: " < '#' " }, { name: "=", nameEN: "=", value: " = '#' " }, { name: "Khác", nameEN: "Not equal", value: " != '#' " }];
            $scope.typeFilterB = [{ name: "Có chứa", nameEN: "Contains", value: " like '%#%' " }, { name: "Bằng", nameEN: "Is", value: " = '#' " }, { name: "Không chứa", nameEN: "Do not contains", value: " != '#' " }];
            $scope.filterColumnsChoosed = [];
            $scope.Math = $window.Math;
            $scope.emailValid = /^[a-z]+[a-z0-9._]+@[a-z]+\.[a-z.]{2,5}$/;
            $scope.isShowRecommended = true;
            $scope.isShowTimeSSN = false;
            $scope.isShowtd = true;
            $scope.popupGuiDuyet = true;
            $scope.popupduyet = true;
            $scope.editData.PercentPayroll = "100%";
            $scope.Messenger = "";
            $scope.showtab13 = true;
            $scope.OrganizationUnitID = $rootScope.PhongBan;
            $scope.StaffID = $rootScope.giatri;
            $scope.isShow2 = true;
            $scope.isRequestOrApproval = true;
            $scope.GetWorkingDayTimePeriod1 = function (staffId) {
                var list = myService.GetWorkingDayTimePeriod(staffId);
                list.then(function (res) {
                    $scope.HrWorkingDay = res.data.result;
                },
                    function (res) {
                        $scope.msg = "Error";
                    });
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


            $scope.FromMonth = today;


            today1 = yyyy + '/' + mm + '/' + dd;

            $scope.FromMonth1 = today1;
            //đóng popup
            $scope.CloseForm = function (form) {
                form.$dirty = false;
                form.$invalid = false;
                form.$submitted = false;
                form.$valid = false;
                $scope.editData = {};
                $scope.listBosungcong = [];
                $scope.listBosungconglamthem = [];
                $scope.TimeOffSelect = [];
                $scope.Messenger = "";
                document.getElementById('aSelect').disabled = false;
                $.colorbox.close();

            }

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
                $rootScope.giatri;
            }

            // checkbox.
            $scope.checkAll = false;
            $scope.list = {
                StaffID: []
            }

            //Lấy công trên máy và hiển thị lên form
            function GetListtimekiping(userid) {
                $scope.from = $scope.FromMonth.split("/");
                var data = {
                    pageIndex: $scope.pageIndex == null ? 1 : $scope.pageIndex,
                    pageSize: $scope.pageSizeSelected == null ? 5 : $scope.pageSizeSelected,
                    month: $scope.from[0],
                    year: $scope.from[1],
                    userid: userid,
                    status: 0,
                    filter: $scope.getFilterValue()
                }
                var getDataTbl = myService.GetTableData(data, "/RecommendedList/TableServerSideGetData1");

                getDataTbl.then(function (emp) {
                    $scope.timekipping = emp.data.employees;
                    $scope.listDate = $scope.timekipping;
                    $scope.listDate1 = angular.copy($scope.listDate)
                    $scope.listBosungconglamthem = [];
                    $scope.listBosungcong = [];

                    var len = $scope.listDate.length;
                    $scope.dimuonvesom = [];
                    //Lấy ngày theo các TH Type : 2,3,4,5
                    var x = [];//Type = 2,3
                    var x1 = [];//Type = 
                    var x2 = [];
                    for (var i = 0; i < len; i++) {
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
                            x1.push(FormatDate(datenow));
                            $scope.listBosungconglamthem.push($scope.listDate[i]);
                            if ($scope.listDate[i].DayWork < 1) {
                                x2.push(FormatDate(datenow));
                                $scope.listBosungcong.push($scope.listDate[i]);
                            }
                        }
                    }

                    $scope.TimeOffSelect = [];
                    var len1 = $scope.listBosungconglamthem != undefined ? $scope.listBosungconglamthem.length : 0;
                    var len2 = $scope.listBosungcong.length;
                    if ($scope.editData.Type == 1) {
                        $scope.editData.DayOff = 0;
                        for (var i = 0; i < len; i++) {
                            var hourlate = $scope.listDate[i].HourLate;
                            var hourearly = $scope.listDate[i].HourEarly;
                            var fromhour = $scope.listDate[i].HourCheckIn;
                            var tohour = $scope.listDate[i].HourCheckOut;
                            if (hourlate != "00:00") {
                                $scope.myobj = { CheckTime: $scope.listDate[i].DayOfWeeks + " - " + x[i] + " - " + fromhour + " - " + hourlate };
                                $scope.TimeOffSelect.push($scope.myobj)
                            }
                            else if (hourearly != "00:00") {
                                $scope.myobj = { CheckTime: $scope.listDate[i].DayOfWeeks + " - " + x[i] + " - " + tohour + " - " + hourearly };
                                $scope.TimeOffSelect.push($scope.myobj)
                            }
                        }
                    }
                    else if ($scope.editData.Type == 2 || $scope.editData.Type == 3) {
                        for (var i = 0; i < len; i++) {
                            var j = len - i - 1;
                            $scope.listDate[i].CheckTime = x[j];
                            $scope.listDate1[i].CheckTime1 = x[j];
                        }
                    }
                    else if ($scope.editData.Type == 4) {
                        $scope.editData.PercentPayrollID = "100";
                        $scope.ReadonlyPercentPayrollID = true;
                        $scope.listDate = $scope.listBosungcong;
                        $scope.listDate1 = $scope.listBosungcong;
                        for (var i = 0; i < len2; i++) {
                            var fromhour = $scope.listBosungcong[i].HourCheckIn;
                            var tohour = $scope.listBosungcong[i].HourCheckOut;
                            if (fromhour == null || fromhour == '') {
                                fromhour = "__:__:__"
                            }
                            if (tohour == null || tohour == '') {
                                tohour = "__:__:__"
                            }

                            var j = len2 - i - 1;
                            $scope.listDate[i].CheckTime = $scope.listBosungcong[j].DayOfWeeks + " - " + x2[j] + " - " + fromhour;
                            $scope.listDate1[i].CheckTime1 = $scope.listBosungcong[j].DayOfWeeks + " - " + x2[j] + " - " + tohour;

                        }
                    }
                    else if ($scope.editData.Type == 5) {
                        $scope.anlamthem = true;
                        $scope.listDate = $scope.listBosungconglamthem;
                        $scope.listDate1 = $scope.listBosungconglamthem;
                        for (var i = 0; i < len1; i++) {
                            var j = len1 - i - 1;
                            $scope.listDate[i].CheckTime = $scope.listBosungconglamthem[j].DayOfWeeks + " - " + x1[j];
                            $scope.listDate1[i].CheckTime1 = $scope.listBosungconglamthem[j].DayOfWeeks + " - " + x1[j];
                        }
                    }
                },
                    function (emp) {
                        AppendToToastr(false, notification, errorNotiFalse);
                    });
            }

            //Chạy các function được gọi
            $scope.CallData = function () {
                $scope.ReadonlyStatus = true;
                KindOfProposal();
                Reason();
                Status();
                ListMucDich();
                $scope.ListGetHour();
                ListCustId();
                getPercent();
                //$scope.ChangeCustId();
            }

            // Gửi duyệt công lại sau khi sửa
            //Sửa công và validate trên modal
            $scope.SaveAction = function (form) {
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
                    $scope.ListRecommended = angular.copy($scope.employees);
                    var len = $scope.ListRecommended.length;
                    for (var i = 0; i < len; i++) {
                        $scope.ListRecommended[i].Date = $scope.ListRecommended[i].Date.slice(0, 10);
                    }
                    url = "/RecommendedList/SaveBoSungCong";
                    $scope.editData.AutoID = $scope.autoid;
                    $scope.editData.StaffID = $scope.StaffID1;
                    $scope.TimeOff = "00:00";
                    $scope.StartTime = "00:00";
                    $scope.EndTime = "00:00";
                    if ($scope.editData.Type == 2 || $scope.editData.Type == 3) { // Trạng thái nghỉ phép hoặc không lương                                           
                        var x = $scope.editData.StartDay.split('/');
                        var y = $scope.editData.EndDate.split('/');
                        var a = new Date(x[2], x[1], x[0]);
                        var b = new Date(y[2], y[1], y[0]);
                        var c = (b - a);
                        var songaytinh = c / (1000 * 60 * 60 * 24);
                        if (songaytinh < 0) {
                            $scope.Messenger = errorDate;
                            return;
                        }
                        var startTime = moment($scope.editData.FromHour, "HH:mm:ss");
                        var endTime = moment($scope.editData.ToHour, "HH:mm:ss");
                        var duration = moment.duration(endTime.diff(startTime));
                        var hours = parseInt(duration.asHours());
                        var minutes = parseInt(duration.asMinutes()) - hours * 60;
                        if (hours < 0) {
                            $scope.Messenger = errorCompareTime;
                            return;
                        }
                        else if (hours == 0 && minutes < 0) {
                            $scope.Messenger = errorCompareTime;
                            return;
                        }
                        var x = $scope.editData.StartDay.replace("/", "-");
                        var z = x.replace("/", "-");
                        $scope.editData.Date = x.split("-").reverse().join("-");
                        $scope.editData.FromTime = $scope.FromMonth2 + " " + $scope.editData.FromHour + ".000";
                        $scope.editData.ToTime = $scope.FromMonth2 + " " + $scope.editData.ToHour + ".000";

                    }
                    if ($scope.editData.Type == 1) { //Trạng thái xin đi muộn về sớm                       
                        var time = $scope.editData.TimeOffSelect;
                        var before = time.substr(-5, 5);

                        var before1 = moment(before, "HH:mm");
                        var after = moment($scope.editData.TimeOff, "HH:mm");
                        var duration = moment.duration(after.diff(before1));
                        var hours = parseInt(duration.asHours());
                        var minutes = parseInt(duration.asMinutes()) - hours * 60;
                        if (hours > 0 || minutes > 0) {
                            $scope.Messenger = errorMinute;
                            return;
                        }

                        var time = $scope.editData.TimeOffSelect.substr(-5, 5);
                        var x = $scope.editData.TimeOffSelect.substr(-29, 10).replace("/", "-");
                        x.replace("/", "-");
                        $scope.editData.Date = x.split("-").reverse().join("-");
                        $scope.editData.TimeOfActual = $scope.FromMonth2 + " " + time + ":00.000";
                        $scope.editData.HourOff = $scope.FromMonth2 + " " + $scope.editData.TimeOff + ":00.000";

                    }

                    if ($scope.editData.Type == 4) { //Trạng thái bổ sung công
                        var x = $scope.editData.StartDay.substr(-21, 10).split('/');
                        var y = $scope.editData.EndDate.substr(-21, 10).split('/');

                        var a = new Date(x[2], x[1], x[0]);
                        var b = new Date(y[2], y[1], y[0]);
                        var c = (b - a);
                        var songaytinh = c / (1000 * 60 * 60 * 24);
                        if (songaytinh < 0) {
                            $scope.Messenger = errorDate;
                            return;
                        }
                        var startTime = moment($scope.editData.FromHour, "HH:mm:ss");
                        var endTime = moment($scope.editData.ToHour, "HH:mm:ss");
                        var duration = moment.duration(endTime.diff(startTime));
                        var hours = parseInt(duration.asHours());
                        var minutes = parseInt(duration.asMinutes()) - hours * 60;
                        if (hours < 0) {
                            $scope.Messenger = errorCompareTime;
                            return;
                        }
                        else if (hours == 0 && minutes < 0) {
                            $scope.Messenger = errorCompareTime;
                            return;
                        }
                    
                        var x = $scope.editData.StartDay.substr(-21, 10).replace("/", "-");
                        var z = x.replace("/", "-");
                        $scope.editData.Date = x.split("-").reverse().join("-");
                        $scope.editData.FromTime = $scope.FromMonth2 + " " + $scope.editData.FromHour + ".000";
                        $scope.editData.ToTime = $scope.FromMonth2 + " " + $scope.editData.ToHour + ".000";

                    }

                    if ($scope.editData.Type == 5) { //Trạng thái bổ sung công làm thêm                      
                        var x1 = $scope.editData.StartTime.slice(0, 2);
                        var x2 = $scope.editData.EndTime.slice(0, 2);

                        var x3 = $scope.editData.StartTime.slice(3, 5);
                        var x4 = $scope.editData.EndTime.slice(3, 5);

                        var temp = x2 - x1;
                        var temp1 = x4 - x3;
                        if (temp < 0) {
                            $scope.Messenger = ErrorCompareHours; return;
                        }
                        else if (temp == 0 && temp1 < 0) {
                            $scope.Messenger = ErrorCompareHours; return;
                        }
                        var x = $scope.editData.StartDay.substr(-10, 10).replace("/", "-");
                        var z = x.replace("/", "-");
                        $scope.editData.Date = x.split("-").reverse().join("-");
                        $scope.editData.FromTime = $scope.FromMonth2 + " " + $scope.editData.StartTime + ":00.000";
                        $scope.editData.ToTime = $scope.FromMonth2 + " " + $scope.editData.EndTime + ":00.000";

                    }
                    var oldString = $scope.FromMonth;
                    var str = oldString.replace("/", "-");
                    $scope.newString = str;
                    $scope.editData.MonthVacation = $scope.newString;
                    if ($scope.editData.DayOff > 1) {
                        if ($scope.editData.Type != 5) {
                            var x = $scope.editData.DayOff.toString().slice(0, 1);
                            var z = $scope.editData.DayOff - parseInt(x);
                            if (z == 0) {
                                $scope.editData.DayOff = 1;
                            }
                            else {
                                $scope.editData.DayOff = z;
                            }
                        }

                    }
                    var updateAction = myService.UpdateData(url, $scope.editData);
                    updateAction.then(function (res) {
                        if (res.data.result.Message == "0") {
                            $scope.Messenger = errorWorkAlreadyExists;
                        }
                        else if (res.data.result.Message == "1") {//kiểm tra có 3 lệnh quên chấm công trên 1 tháng
                            $scope.Messenger = errorReasonToForgetToTimekeeping;
                            $scope.LoiChuaNhap = true;
                        }
                        else if (res.data.result.Message == "2") {
                            $scope.Messenger = errorAdditionalPermissionsExceeded;
                        }
                        else if (res.data.result.Message == "3") {
                            $scope.Messenger = errorPastThirtyMinutes;
                        }
                        else if (res.data.result.Message == "4") {
                            $scope.Messenger = errorCreatingMerit;
                        }
                        else if (res.data.result.Message == "6") {
                            $scope.messagevalidate = errorTardinessLeaveEarly;
                            $scope.LoiChuaNhap = true;
                        }
                        if (res.data.result.IsSuccess == true) {
                            AppendToToastr(true, notification, successfulUpdate, 500, 5000);
                            $scope.GetListData();
                            $scope.TimeInvalid = false;
                            $scope.CloseForm(form);
                        }
                    },
                        function (res) {
                            AppendToToastr(false, notification, errorNotiFalse);
                        });
                }
                else {
                    AppendToToastr(false, notification, errorFullInformation);
                }
            }

            //Click button edit lấy thông tin của công bổ sung
            $scope.Edit_Recommended = function (employees) {
                var dt = Loading();
                if ($rootScope.giatri == null || $rootScope.giatri == undefined) $rootScope.giatri = employees.StaffID;
                $scope.GetWorkingDayTimePeriod1($rootScope.giatri);
                $scope.readonlyFromHour = true;
                $scope.readonlyToHour = true;

                $scope.editData.TimeOff = "00:00";
                $scope.editData.StartTime = "00:00";
                $scope.editData.EndTime = "00:00";

                if (employees.Status == 0) {
                    AppendToToastr(false, notification, errorNotSubmittedorApproval);
                    dt.finish();

                }

                else if (employees.Status == 3 || employees.Status == 1 || employees.Status == 8 || employees.Status == 6) {
                    AppendToToastr(false, notification, errorPending);
                    dt.finish();

                }
                else if (employees.Status == 10 || employees.Status == 5) {
                    AppendToToastr(false, notification, errorApproved);
                    dt.finish();

                }
                else {
                    $scope.StaffID1 = employees.StaffID;
                    $scope.CallData();


                    var data = {
                        AutoID: employees.AutoID,
                    }

                    var list = myService.HR_WorkingDaySupplement_GetListId(data, "/HR_WorkingDaySupplement/HR_WorkingDaySupplement_GetListId");
                    list.then(function (res) {

                        $scope.GetListId = res.data.result;
                        $scope.autoid = $scope.GetListId.AutoID;

                        $scope.editData.Type = $scope.GetListId.Type.toString();

                        $scope.editData.Status = $scope.GetListId.Status.toString();
                        $scope.editData.ReasonType = $scope.GetListId.ReasonType;
                        $scope.editData.Note = $scope.GetListId.Note.toString();


                        if ($scope.GetListId.ReasonType == 1213) { // nếu lý do gặp khách hàng thì hiển thị dữ liệu lên combobox
                            $scope.editData.CustomerID = $scope.GetListId.CustomerID;

                            var customerID = $scope.GetListId.CustomerID;
                            $scope.ChangeCustId();

                            $scope.editData.CustomerContactID = $scope.GetListId.CustomerContactID;
                            $scope.editData.CustomerReasonType = $scope.GetListId.CustomerReasonType;

                        }                  

                        //Lấy công trên máy và hiển thị lên form

                        $scope.from = $scope.FromMonth.split("/");

                        var data = {
                            pageIndex: $scope.pageIndex == null ? 1 : $scope.pageIndex,
                            pageSize: $scope.pageSizeSelected == null ? 5 : $scope.pageSizeSelected,
                            month: $scope.from[0],
                            year: $scope.from[1],
                            userid: $scope.StaffID1,
                            status: 0,
                            filter: $scope.getFilterValue()
                        }
                        var getDataTbl = myService.GetTableData(data, "/RecommendedList/TableServerSideGetData1");

                        getDataTbl.then(function (emp) {

                            $scope.timekipping = emp.data.employees;
                            $scope.listDate = $scope.timekipping;
                            $scope.listBosungconglamthem = [];
                            $scope.listBosungcong = [];
                            //WeekNow();
                            //for (var i = 0; i < $scope.timekipping.length; i++) {
                            //    var datenow = $scope.timekipping[i].CheckTime.slice(0, 10);
                            //    for (var j = 0; j < $scope.listday.length; j++) {
                            //        if ($scope.listday[j] == FormatDate(datenow)) {
                            //            $scope.listDate.push($scope.timekipping[i]);
                            //        }
                            //    }
                            //}
                            $scope.listDate1 = angular.copy($scope.listDate)

                            var len = $scope.listDate.length;
                            $scope.dimuonvesom = [];
                            //Lấy ngày theo các TH Type : 2,3,4,5
                            var x = [];//Type = 2,3
                            var x1 = [];//Type = 
                            var x2 = [];
                            for (var i = 0; i < len; i++) {
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
                                    x1.push(FormatDate(datenow));
                                    $scope.listBosungconglamthem.push($scope.listDate[i]);
                                    if ($scope.listDate[i].DayWork < 1) {
                                        x2.push(FormatDate(datenow));
                                        $scope.listBosungcong.push($scope.listDate[i]);
                                    }
                                }
                            }

                            $scope.TimeOffSelect = [];
                            var len1 = $scope.listBosungconglamthem != undefined ? $scope.listBosungconglamthem.length : 0;
                            var len2 = $scope.listBosungcong.length;
                            if ($scope.editData.Type == 1) {
                                $scope.editData.DayOff = 0;
                                for (var i = 0; i < len; i++) {
                                    var hourlate = $scope.listDate[i].HourLate;
                                    var hourearly = $scope.listDate[i].HourEarly;
                                    var fromhour = $scope.listDate[i].HourCheckIn;
                                    var tohour = $scope.listDate[i].HourCheckOut;
                                    if (hourlate != "00:00") {
                                        $scope.myobj = { CheckTime: $scope.listDate[i].DayOfWeeks + " - " + x[i] + " - " + fromhour + " - " + hourlate };
                                        $scope.TimeOffSelect.push($scope.myobj)
                                    }
                                    else if (hourearly != "00:00") {
                                        $scope.myobj = { CheckTime: $scope.listDate[i].DayOfWeeks + " - " + x[i] + " - " + tohour + " - " + hourearly };
                                        $scope.TimeOffSelect.push($scope.myobj)
                                    }
                                }
                            }
                            else if ($scope.editData.Type == 2 || $scope.editData.Type == 3) {
                                for (var i = 0; i < len; i++) {
                                    var j = len - i - 1;
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
                                    if (fromhour == null || fromhour=='') {
                                        fromhour = "__:__:__";
                                    }
                                    if (tohour == null || tohour=='') {
                                        tohour = "__:__:__";
                                    }

                                    var j = len2 - i - 1;
                                    $scope.listDate[i].CheckTime = $scope.listBosungcong[j].DayOfWeeks + " - " + x2[j] + " - " + fromhour;
                                    $scope.listDate1[i].CheckTime1 = $scope.listBosungcong[j].DayOfWeeks + " - " + x2[j] + " - " + tohour;

                                }
                            }
                            else if ($scope.editData.Type == 5) {
                                $scope.anlamthem = true;
                                $scope.listDate = $scope.listBosungconglamthem;
                                $scope.listDate1 = $scope.listBosungconglamthem;
                                for (var i = 0; i < len1; i++) {
                                    var j = len1 - i - 1;
                                    $scope.listDate[i].CheckTime = $scope.listBosungconglamthem[j].DayOfWeeks + " - " + x1[j];
                                    $scope.listDate1[i].CheckTime1 = $scope.listBosungconglamthem[j].DayOfWeeks + " - " + x1[j];
                                }
                            }
                            if ($scope.GetListId.Type == 1) { //hiển thị dữ liệu với loại xin đi muộn về sớm
                                var date = FormatDate($scope.GetListId.Date);

                                $scope.editData.TimeOff = $scope.GetListId.HourOff.slice(11, 16);
                                var len = $scope.TimeOffSelect.length;
                                for (var i = 0; i < len; i++) {
                                    var dimuon = $scope.TimeOffSelect[i].CheckTime.substr(-29, 10);
                                    if (dimuon == date) {
                                        $scope.editData.TimeOffSelect = $scope.TimeOffSelect[i].CheckTime;
                                        break;
                                    }
                                }
                            }
                            if ($scope.GetListId.FromTime == null || $scope.GetListId.FromTime == undefined) $scope.GetListId.FromTime = '';
                            if ($scope.GetListId.ToTime == null || $scope.GetListId.ToTime == undefined) $scope.GetListId.ToTime = '';
                            if ($scope.GetListId.Type == 2 || $scope.GetListId.Type == 3) { //hiển thị dữ liệu với loại không lương và nghỉ phép
                                $scope.editData.StartDay = FormatDate($scope.GetListId.Date).toString();
                                $scope.editData.EndDate = FormatDate($scope.GetListId.Date).toString();
                                $scope.editData.FromHour = $scope.GetListId.FromTime.slice(11, 19).toString();
                                $scope.editData.ToHour = $scope.GetListId.ToTime.slice(11, 19).toString();
                                $scope.editData.DayOff = $scope.GetListId.DayOff;
                                $scope.readonlyFromHour = false;
                                $scope.readonlyToHour = false;
                                $scope.readonlyEndDate = false;
                            }

                            if ($scope.GetListId.Type == 4) { //hiển thị dữ liệu với loại bổ sung công     
                                if ($scope.GetListId.PercentPayrollID != null && $scope.GetListId.PercentPayrollID!="") {
                                    $scope.editData.PercentPayrollID = $scope.GetListId.PercentPayrollID.toString();
                                }
                                $scope.editData.DayOff = $scope.GetListId.DayOff;

                                var date = FormatDate($scope.GetListId.Date);

                                var len = $scope.listDate.length != undefined ? $scope.listDate.length : 0;
                                for (var i = 0; i < len; i++) {
                                    var position = $scope.listDate[i].CheckTime.indexOf('CN');
                                    if (position != -1) {
                                        var dimuon = $scope.listDate[i].CheckTime.substr(-21, 10)
                                        if (dimuon == date) {
                                            $scope.editData.StartDay = $scope.listDate[i].CheckTime;
                                            $scope.editData.EndDate = $scope.listDate1[i].CheckTime1;
                                            break;
                                        }
                                    }
                                    else {
                                        var dimuon = $scope.listDate[i].CheckTime.substr(-21, 10);
                                        if (dimuon == date) {
                                            $scope.editData.StartDay = $scope.listDate[i].CheckTime;
                                            $scope.editData.EndDate = $scope.listDate[i].CheckTime1;
                                            break;
                                        }
                                    }

                                }


                                $scope.editData.FromHour = $scope.GetListId.FromTime.slice(11, 19).toString();
                                $scope.editData.ToHour = $scope.GetListId.ToTime.slice(11, 19).toString();

                                $scope.readonlyFromHour = false;
                                $scope.readonlyToHour = false;
                                $scope.readonlyEndDate = false;
                                $scope.ReadonlyPercentPayrollID = true;
                            }
                            if ($scope.GetListId.Type == 5) { //hiển thị dữ liệu với loại bổ sung công làm thêm


                                $scope.editData.PercentPayrollID = $scope.GetListId.PercentPayrollID.toString();
                                $scope.editData.DayOff = $scope.GetListId.DayOff;

                                var date = FormatDate($scope.GetListId.Date);

                                var len = $scope.listDate.length != undefined ? $scope.listDate.length : 0;

                                for (var i = 0; i < len; i++) {
                                    var position = $scope.listDate[i].CheckTime.indexOf('CN');
                                    if (position != -1) {
                                        var dimuon = $scope.listDate[i].CheckTime.substr(-10, 10);
                                        if (dimuon == date) {
                                            $scope.editData.StartDay = $scope.listDate[i].CheckTime;
                                            $scope.editData.EndDate = $scope.listDate[i].CheckTime1;
                                            break;

                                        }
                                    }
                                    else {
                                        var dimuon = $scope.listDate[i].CheckTime.substr(-10, 10);
                                        if (dimuon == date) {
                                            $scope.editData.StartDay = $scope.listDate[i].CheckTime;
                                            $scope.editData.EndDate = $scope.listDate[i].CheckTime1;
                                            break;
                                        }
                                    }

                                }
                                $scope.editData.StartTime = $scope.GetListId.FromTime.slice(11, 16).toString();
                                $scope.editData.EndTime = $scope.GetListId.ToTime.slice(11, 16).toString();
                                $scope.readonlyEndDate = true;
                            }
                            ShowPopup($,
                                "#Edit_Recommended",
                                $scope.tableInfo.PopupWidth,
                                $scope.tableInfo.PopupHeight);
                            dt.finish();

                        },
                            function (emp) {
                                AppendToToastr(false, notification, errorNotiFalse);
                                dt.finish();

                            });



                    }, function (res) {
                        $scope.msg = "Error";
                        var dt = finish();

                    })

                }

            }

            function tinhcong2thoigian(fromtime, totime) {
                var x1, x2, cong;
                x1 = parseInt(fromtime.slice(0, 2)) + fromtime.slice(3, 5) / 60;
                x2 = parseInt(totime.slice(0, 2)) + totime.slice(3, 5) / 60;
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
                    else if (temp < 3.5) {
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
                }
                else {
                    cong = temp / 2 * 0.25;
                    if (cong > 1) {
                        cong = 1;
                    }
                }


                return cong;
            }

            $scope.changePercentPayroll = function () {
                if ($scope.editData.Type==5) {
                    $scope.luucongnhan = tinhcong2thoigian($scope.editData.StartTime, $scope.editData.EndTime);
                    $scope.editData.DayOff = $scope.luucongnhan * ($scope.editData.PercentPayrollID / 100) * $scope.songaytinh;
                }
                else if ($scope.editData.Type == 4) {
                    $scope.changeToTimeOut1();
                }
                
            }

            //Thay đổi trạng thái bổ sung công khi click
            $scope.ChangegetListKindOfProposal = function (form) {
                form.$dirty = false;
                form.$invalid = false;
                form.$submitted = false;
                form.$valid = false;
                GetListtimekiping($scope.StaffID1);
                $scope.editData.Note = "";
                $scope.editData.ReasonType = "";
            }
            $scope.TimeInvalid = false;
            //Tính ngày công
            $scope.changeToTimeOut1 = function () {
                $scope.editData.DayOff = 0;
                if ($scope.editData.Type != 5 && $scope.editData.Type != 1) {
                    var timeFrom = moment($scope.editData.FromHour, 'HH:mm:ss').add(1, 'seconds');
                    var timeTo = moment($scope.editData.ToHour, 'HH:mm:ss').add(-1, 'seconds');
                }
                else {
                    var timeFrom = moment($scope.editData.StartTime, 'HH:mm:ss').add(1, 'seconds');
                    var timeTo = moment($scope.editData.EndTime, 'HH:mm:ss').add(-1, 'seconds');
                }               
                if (!timeFrom.isValid() || !timeTo.isValid() || (timeTo.isBefore(timeFrom))) {
                    $scope.TimeInvalid = true;
                    return;
                }
                else {
                    $scope.TimeInvalid = false;
                }
                $scope.Messenger = "";
                $scope.MessengerEN = "";
                if ($scope.editData.StartDay != null && $scope.editData.EndDate != null && $scope.editData.StartDay != "" && $scope.editData.EndDate) {
                    if ($scope.editData.Type == 4) {

                        var position = $scope.editData.StartDay.indexOf('CN');
                        var position1 = $scope.editData.EndDate.indexOf('CN');
                        if (position != -1) {
                            var StartDay = $scope.editData.StartDay.substr(-21, 10);
                        }
                        else {
                            var StartDay = $scope.editData.StartDay.substr(-21, 10);

                        }
                        if (position1 != -1) {
                            var EndDate = $scope.editData.EndDate.substr(-21, 10);
                        }
                        else {
                            var EndDate = $scope.editData.EndDate.substr(-21, 10);
                        }

                    }
                    else if ($scope.editData.Type == 2 || $scope.editData.Type == 3) {
                        var StartDay = $scope.editData.StartDay;
                        var EndDate = $scope.editData.EndDate;

                    }
                    else if ($scope.editData.Type == 5) {
                        var position = $scope.editData.StartDay.indexOf('CN');
                        var position1 = $scope.editData.EndDate.indexOf('CN');
                        if (position != -1) {
                            var StartDay = $scope.editData.StartDay.substr(-10, 10);
                        }
                        else {
                            var StartDay = $scope.editData.StartDay.substr(-10, 10);
                        }
                        if (position1 != -1) {
                            var EndDate = $scope.editData.EndDate.substr(-10, 10);
                        }
                        else {
                            var EndDate = $scope.editData.EndDate.substr(-10, 10);
                        }
                    }
                    var x = StartDay.split('/');
                    var y = EndDate.split('/');
                    var a = new Date(x[2], x[1], x[0]);
                    var b = new Date(y[2], y[1], y[0]);
                    var c = (b - a);
                    var songaytinh = c / (1000 * 60 * 60 * 24);

                    //if ($scope.editData.Type != 5 && $scope.editData.Type != 1) {
                    //    var timeFrom = moment($scope.editData.FromHour, 'HH:mm:ss');
                    //    var timeTo = moment($scope.editData.ToHour, 'HH:mm:ss');
                    //}
                    //else {
                    //    var timeFrom = moment($scope.editData.StartTime, 'HH:mm:ss');
                    //    var timeTo = moment($scope.editData.EndTime, 'HH:mm:ss');
                    //}
                    if ($scope.editData.Type != 5 && $scope.editData.Type != 1) {
                        if (timeFrom.isAfter(moment('12:00:00', 'HH:mm:ss'))
                        && timeFrom.isBefore(moment('13:00:30', 'HH:mm:ss'))
                        && timeTo.isAfter(moment('12:00:00', 'HH:mm:ss'))
                        && timeTo.isBefore(moment('13:00:30', 'HH:mm:ss'))
                        ) {
                            $scope.editData.DayOff = songaytinh;
                        }
                        else
                            if (timeFrom.isBefore(moment($scope.HrWorkingDay.MorningHourMid, 'HH:mm:ss'))) {
                                if (timeTo.isBefore(moment($scope.HrWorkingDay.MorningHourMid, 'HH:mm:ss'))) {
                                    $scope.editData.DayOff = 0.25 + songaytinh;
                                }
                                else
                                    if (timeTo.isBefore(moment($scope.HrWorkingDay.MorningHourEnd, 'HH:mm:ss'))) {
                                        $scope.editData.DayOff = 0.5 + songaytinh;
                                    }
                                    else
                                        if (timeTo.isBefore(moment($scope.HrWorkingDay.AfernoonHourMid, 'HH:mm:ss'))) {
                                            $scope.editData.DayOff = 0.75 + songaytinh;
                                        }
                                        else {
                                            $scope.editData.DayOff = 1 + songaytinh;
                                        }
                            }
                            else
                                if (timeFrom.isBefore(moment($scope.HrWorkingDay.MorningHourEnd, 'HH:mm:ss'))) {
                                    if (timeTo.isBefore(moment($scope.HrWorkingDay.AfernoonHourStart, 'HH:mm:ss'))) {
                                        $scope.editData.DayOff = 0.25 + songaytinh;
                                    }
                                    else
                                        if (timeTo.isBefore(moment($scope.HrWorkingDay.AfernoonHourMid, 'HH:mm:ss'))) {
                                            $scope.editData.DayOff = 0.5 + songaytinh;
                                        }
                                        else {
                                            $scope.editData.DayOff = 0.75 + songaytinh;
                                        }
                                }
                                else
                                    if (timeFrom.isBefore(moment($scope.HrWorkingDay.AfernoonHourMid, 'HH:mm:ss'))) {
                                        if (timeTo.isBefore(moment($scope.HrWorkingDay.AfernoonHourMid, 'HH:mm:ss'))) {
                                            $scope.editData.DayOff = 0.25 + songaytinh;
                                        }
                                        else {
                                            $scope.editData.DayOff = 0.5 + songaytinh;
                                        }
                                    }
                                    else
                                        if (timeFrom.isBefore(moment($scope.HrWorkingDay.AfternoonHourEnd, 'HH:mm:ss'))) {
                                            $scope.editData.DayOff = 0.25 + songaytinh;
                                        }
                                        else {
                                            $scope.editData.DayOff = songaytinh;
                                        }
                    }
                    else {
                        $scope.editData.DayOff = (timeTo.hour() + timeTo.minute() / 60 - timeFrom.hour() - timeFrom.minute() / 60) / 2 * 0.25;
                        if ($scope.editData.DayOff > 1) {
                            $scope.editData.DayOff = 1;
                        }
                        $scope.changePercentPayroll();
                    }
                    if ($scope.editData.Type == 4) {
                        $scope.editData.DayOff = $scope.editData.DayOff * ($scope.editData.PercentPayrollID / 100)
                    }
                }
            }

            //Đẩy thời gian đi muộn về sớm sang input TimeOff
            $scope.ChangeTimeOffSelect = function () {
                if ($scope.editData.TimeOffSelect != null) {
                    var time = $scope.editData.TimeOffSelect;
                    $scope.editData.TimeOff = time.substr(-5, 5);
                    $scope.TimeBefore = $scope.editData.TimeOff;
                }


            }

            //Kiểm tra thời gian đi muộn về sớm có đúng với thời gian thực tế hay không?
            $scope.ChangeTimeOff = function () {

                if ($scope.editData.TimeOffSelect != null && $scope.editData.TimeOffSelect != "") {
                    var time = $scope.editData.TimeOffSelect;
                    var before = time.substr(-5, 5);

                    var before1 = moment(before, "HH:mm");
                    var after = moment($scope.editData.TimeOff, "HH:mm");
                    var duration = moment.duration(after.diff(before1));
                    var hours = parseInt(duration.asHours());
                    var minutes = parseInt(duration.asMinutes()) - hours * 60;
                    if (hours == 0 && minutes == 0) {
                        $scope.Messenger = "";
                        $scope.MessengerEN = "";
                    }
                    else if (hours <= 0 && minutes < 0) {
                        $scope.Messenger = "";
                        $scope.MessengerEN = "";
                    }
                    else {
                        $scope.Messenger = errorMinute;
                    }
                }


                //if (hours<0) {
                //    $scope.Messenger = "Số phút bổ sung phải nhỏ hơn số phút đi muộn / về sớm thực tế.";
                //    console.log($scope.Messenger)
                //}

                //var time = $scope.editData.TimeOffSelect;
                //var before = time.slice(30, 35);
                //var after = moment($scope.editData.TimeOff);
            }

            //Thay đổi dữ liệu khi click vào combobox StartDay
            $scope.changeStartDay = function () {
                if ($scope.editData.StartDay == undefined || $scope.editData.StartDay == "") {
                    $scope.editData.FromHour = "";
                    $scope.readonlyFromHour = true;
                    $scope.editData.DayOff = 0;
                }
                else {

                    $scope.readonlyFromHour = false;
                    var len = $scope.ListFromHour.length;

                    for (var i = 0; i < len; i++) {
                        if ($scope.ListFromHour[i].StartTime == "07:30:00") {
                            $scope.editData.FromHour = "07:30:00";
                            break;
                        }
                        else if ($scope.ListFromHour[i].StartTime == "08:00:00") {
                            $scope.editData.FromHour = "08:00:00";
                            break;
                        }
                        else if ($scope.ListFromHour[i].StartTime == "09:00:00") {
                            $scope.editData.FromHour = "09:00:00";
                            break;
                        }
                        else {
                            $scope.editData.FromHour = "08:30:00";
                        }
                    }
                    if ($scope.editData.Type == 5) {
                        var a = $scope.editData.StartDay;
                        $scope.editData.EndDate = a;
                        $scope.changeToTimeOut1();

                    }
                    if ($scope.editData.Type == 2 || $scope.editData.Type == 3 || $scope.editData.Type == 4) {
                        if ($scope.editData.EndDate != null || $scope.editData.EndDate != "") {
                            $scope.changeToTimeOut1();
                        }
                    }

                }
            }

            //Thay đổi dữ liệu khi click vào combobox EndDate
            $scope.changeEndDate = function () {

                if ($scope.editData.EndDate == undefined) {
                    $scope.editData.ToHour = "";
                    $scope.Messenger = "";
                    $scope.MessengerEN = "";
                    $scope.readonlyToHour = true;
                    $scope.editData.DayOff = 0;

                }
                else {
                    if ($scope.editData.StartDay == undefined || $scope.editData.StartDay == "") {
                        $scope.editData.ToHour = "10:00:00";
                    }
                    else {
                        $scope.Messenger = "";
                        $scope.MessengerEN = "";
                        if ($scope.editData.EndDate == "") {
                            $scope.readonlyToHour = true;
                        }
                        else {
                            $scope.readonlyToHour = false;
                            var len = $scope.ListFromHour.length;
                            for (var i = 0; i < len; i++) {
                                if ($scope.ListFromHour[i].EndTime == "09:30:00") {
                                    $scope.editData.ToHour = "09:30:00";
                                    break;
                                }
                                else if ($scope.ListFromHour[i].EndTime == "10:30:00") {
                                    $scope.editData.ToHour = "10:30:00";
                                    break;
                                }
                                else {
                                    $scope.editData.ToHour = "10:00:00";
                                }
                            }

                            $scope.changeToTimeOut1();

                        }
                    }
                }

            }

            //Lấy danh sách loại bổ sung công
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

            //Lấy danh sách lý do bổ sung công
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

            //lấy danh sách trạng thái bổ sung công
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

            //Lấy danh sách mục đích gặp khách hàng
            function ListMucDich() {
                var data = {
                    url: "/Common/GetDataByGloballistnotTree?parentid=" + 1887
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getListPurpose = res.data.result;
                }, function (res) {
                    $scope.msg = "Error";
                })
            }

            //Lấy danh sách khách hàng
            function ListCustId() {
                var data = {
                    UserID: $scope.StaffID1
                }
                var list = myService.getDropdownChangeCustId(data, "/RecommendedList/Customer_Gets_ByUserID");
                list.then(function (res) {
                    $scope.ListCustId = res.data.result;
                }, function (res) {
                    $scope.msg = "Error";
                })
            }

            //Lấy danh sách người liên hệ 
            $scope.ChangeCustId = function () {

                var customerID = $scope.editData.CustomerID;

                var data = {
                    customerID: customerID,
                    //customerID: 531523,

                }
                var list = myService.getDropdownChangeCustId(data, "/RecommendedList/HR_CustomerContactGetsByCustomerID");
                list.then(function (res) {
                    $scope.ContactName = res.data.result;
                }, function (res) {
                    $scope.msg = "Error";
                })
            }

            //Lấy danh sách giờ bắt đầu và kết thúc làm việc
            $scope.ListGetHour = function () {
                var data = {
                    userid: $scope.StaffID1

                }
                var list = myService.getDropdownChangeCustId(data, "/RecommendedList/HR_WorkingDay_GetHour");
                list.then(function (res) {
                    $scope.ListFromHour = res.data.result;
                    $scope.ListToHour = res.data.result;
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

            $scope.toggleCheck = function () {

                if (!$scope.checkAll) {

                    $scope.checkAll = true;
                    $scope.list.StaffID = $scope.employees.map(function (employee) {
                        return employee.StaffID;
                    });
                } else {
                    $scope.checkAll = false;
                    $scope.list.StaffID = [];
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
                        AppendToToastr(false, notification, errorNotiFalse);
                    });
            }

            $scope.GetAddPermission = function (idTable) {
                var tblAction = myService.getTableAction(idTable);
                tblAction.then(function (emp) {
                    $scope.tablePermission = emp.data.result;
                    if ($scope.tablePermission != null) {
                        if ($scope.tablePermission.isEdit == false) {
                            $scope.is_readonly = true;
                        }
                        else {
                            $scope.is_readonly = false;
                        }
                    }
                    $scope.getColumns();
                    // $scope.BuildAddButton(emp.data.result);
                },
                    function (emp) {
                        AppendToToastr(false, notification, errorNotiFalse);
                    });
            }

            $scope.getTableInfo();

            $scope.getColumns = function () {
                var getData = myService.GetColumns(tableUrl);
                getData.then(function (emp) {
                    $scope.Columns = emp.data.result;
                    $scope.GetListData();
                    $scope.dodai = $scope.Columns.length + 1;
                },
                    function (emp) {
                        AppendToToastr(false, notification, errorNotiFalse);
                    });
            }

            $scope.chageDate = function () {
                $rootScope.thangnam = $scope.FromMonth;
            }

            $scope.status = "0";
            var userid = 0;

            $scope.GetListData = function () {
                var dt = Loading();
                if ($rootScope.thangnam != null) {
                    $scope.from = $rootScope.thangnam.split("/");
                    $scope.FromMonth = $rootScope.thangnam;
                }
                else {
                    $scope.from = $scope.FromMonth.split("/");
                }
                var data = {
                    pageIndex: $scope.pageIndex == null ? 1 : $scope.pageIndex,
                    pageSize: $scope.pageSizeSelected == null ? 5 : $scope.pageSizeSelected,
                    month: $scope.from[0],
                    year: $scope.from[1],
                    userid: userid,
                    status: $scope.status,
                    filter: $scope.getFilterValue(),

                }
                var getDataTbl = myService.GetTableData(data, tableUrl);
                getDataTbl.then(function (emp) {
                    $scope.employees = emp.data.employees;
                    $scope.totalCount = emp.data.totalCount;
                    $scope.lstTotal = emp.data.lstTotal;
                    $scope.userid = emp.data.userid;
                    $scope.SetTotalByColumns = function (totalName) {
                        if (!angular.isUndefined(totalName) && totalName !== null) {
                            return $scope.lstTotal[totalName];
                        }
                        return "-";
                    }
                    dt.finish();
                },
                    function (emp) {
                        AppendToToastr(false, notification, errorNotiFalse);
                    });

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
                if (type === 3 && value != null) {
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
                if (dataFormat === "00:00:00" && value != null) {
                    return value.toString().slice(0, 10);
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

            //-----------------List-End---------
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
                if ($rootScope.PhongBan != null) {
                    $scope.OrganizationUnitID = $rootScope.PhongBan;
                    stringFilter += "  AND a.StaffID IN (SELECT StaffID FROM dbo.Staff WHERE OrganizationUnitID = " + $rootScope.PhongBan + " )  ";
                };
                if ($rootScope.giatri != null & $rootScope.giatri != "") {
                    stringFilter += " and a.StaffID = " + $rootScope.giatri + " ";
                }
                if ($rootScope.giatri != null) {
                    $scope.StaffID = $rootScope.giatri;
                    $scope.userid1 = $rootScope.giatri;
                }
                for (var key in lstObj) {
                    var obj = lstObj[key];
                    //                    if (obj.filterSelected.Type === 3) {
                    //                        obj.textValue = $filter('date')(new Date(obj.textValue), 'yyyy/MM/dd');
                    //                    }
                    // console.log(obj.textValue);
                    var tmpFilter = obj.filterSelected.ColumnName + obj.typeFilterSelected.value.replace("#", obj.filterSelected.Type === 3 ? $filter('date')(new Date(obj.textValue), 'yyyy/MM/dd') : obj.textValue) + (parseInt(String(key)) === len ? "" : obj.typeEndsSeleted.value);
                    stringFilter += tmpFilter;
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

            //-----------------Filter-End-------

            // -----------------Edit------------        

            //$scope.editClick = function (contentItem) {

            //    $scope.from = $scope.FromMonth.split("/");
            //    var edit = myService.getDataByWithMonthYearId(contentItem.StaffID, parseInt($scope.from[0]), parseInt($scope.from[1]), $scope.tableInfo.id, $scope.tableInfo.DataEditUrl);
            //    edit.then(function (emp) {
            //        $scope.editData = emp.data.result;
            //        ShowPopup($,
            //            ".Timekeeping",
            //            $scope.tableInfo.PopupWidth,
            //            $scope.tableInfo.PopupHeight);
            //    },
            //        function (emp) {
            //            AppendToToastr(false, notification, errorNotiFalse);
            //        });

            //}


            // -----------------Edit--End-------

            $scope.showPopupImport = function () {
                ShowPopup($,
                    "#importExcel",
                    $scope.tableInfo.PopupWidth,
                    $scope.tableInfo.PopupHeight);
            }

            // -----------------Xóa-------------

            $scope.onOrganizationUnitChange = function () {
                $rootScope.PhongBan = $scope.OrganizationUnitID;
                if ($scope.OrganizationUnitID != null) {
                    ListEmployeeWhereOrganizationUnit($scope.OrganizationUnitID);
                }
                else {
                    ListEmployeeWhereOrganizationUnit(0);
                }
            }

            $scope.onStaffChange = function () {
                $scope.StaffID = angular.copy($scope.StaffID);
                $rootScope.giatri = $scope.StaffID;
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
                if (thu == 5) {
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
                } console.log($scope.listday);
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

            //import excel
            $scope.upload = function () {
                var url = "/Timekeeping/Upload";
                var config = {
                    headers: {
                        "Content-Type": undefined,
                    }
                };
                var formData = new $window.FormData();
                formData.append("file-0", $scope.files[0]);
                $http.post(url, formData, config).
                    then(function (res) {
                        AppendToToastr(res.data.result.IsSuccess, notification, res.data.result.Message, 500, 5000);
                        $('.modal').modal('hide');
                        $scope.getTableInfo();
                        ListAllOrganizationUnit();
                    }).catch(function (res) {
                        AppendToToastr(false, notification, errorNotiFalse);

                    });
            };



            // -----------------Xóa--End------------
            //-------------------Excel--------------
            $scope.ExcelClick2 = function () {

                $scope.from = $scope.FromMonth.split("/");
                var filterString = $scope.getFilterValue();
                window.location = $scope.tableInfo.ExcelUrl + "?filter=" + filterString + "&pageIndex=" + $scope.pageIndex + "&pageSize=" + $scope.pageSizeSelected + "&month=" + parseInt($scope.from[0]) + "&year=" + parseInt($scope.from[1]) + "&status=" + parseInt($scope.status);

            }

            //-------------------Excel-End----------

            //--load timekipping//



            //getListDate = function () {
            //    var len = $scope.timekipping.length;
            //    $scope.listDate = $scope.timekipping;
            //    for (var i = 0; i < len; i++) {
            //        var x = $scope.timekipping[i].CheckTime.slice(0, 10);
            //        $scope.listDate[i].CheckTime = $scope.timekipping[i].DayOfWeeks + " - " + x;
            //    }
            //    console.log($scope.listDate);
            //}
        });


    app.directive('convertToNumber',
        function () {
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
    app.directive("formatDate",
        function () {
            return {
                require: 'ngModel',
                link: function (scope, elem, attr, modelCtrl) {
                    modelCtrl.$formatters.push(function (modelValue) {

                        return new Date(modelValue);
                    });
                }
            }
        });

    app.directive("myFiles", function ($parse) {
        return function linkFn(scope, elem, attrs) {
            elem.on("change", function (e) {
                scope.$eval(attrs.myFiles + "=$files", { $files: e.target.files });
                scope.$apply();
            })
        }
    });
    app.directive('checklistModel', ['$parse', '$compile', function ($parse, $compile) {
        // contains
        function contains(arr, item) {
            if (angular.isArray(arr)) {
                for (var i = 0; i < arr.length; i++) {
                    if (angular.equals(arr[i], item)) {
                        return true;
                    }
                }
            }
            return false;
        }

        // add
        function add(arr, item) {
            arr = angular.isArray(arr) ? arr : [];
            for (var i = 0; i < arr.length; i++) {
                if (angular.equals(arr[i], item)) {
                    return arr;
                }
            }
            arr.push(item);
            return arr;
        }

        // remove
        function remove(arr, item) {
            if (angular.isArray(arr)) {
                for (var i = 0; i < arr.length; i++) {
                    if (angular.equals(arr[i], item)) {
                        arr.splice(i, 1);
                        break;
                    }
                }
            }
            return arr;
        }


        function postLinkFn(scope, elem, attrs) {
            // compile with `ng-model` pointing to `checked`
            $compile(elem)(scope);

            // getter / setter for original model
            var getter = $parse(attrs.checklistModel);
            var setter = getter.assign;

            // value added to list
            var value = $parse(attrs.checklistValue)(scope.$parent);

            // watch UI checked change
            scope.$watch('checked', function (newValue, oldValue) {
                if (newValue === oldValue) {
                    return;
                }
                var current = getter(scope.$parent);
                if (newValue === true) {
                    setter(scope.$parent, add(current, value));
                } else {
                    setter(scope.$parent, remove(current, value));
                }
            });

            // watch original model change
            scope.$parent.$watch(attrs.checklistModel, function (newArr, oldArr) {
                scope.checked = contains(newArr, value);
            }, true);
        }

        return {
            restrict: 'A',
            priority: 1000,
            terminal: true,
            scope: true,
            compile: function (tElement, tAttrs) {
                if (tElement[0].tagName !== 'INPUT' || !tElement.attr('type', 'checkbox')) {
                    throw 'checklist-model should be applied to `input[type="checkbox"]`.';
                }

                if (!tAttrs.checklistValue) {
                    throw 'You should provide `checklist-value`.';
                }

                // exclude recursion
                tElement.removeAttr('checklist-model');

                // local scope var storing individual checkbox model
                tElement.attr('ng-model', 'checked');

                return postLinkFn;
            }
        };
    }]);
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
                $('#TimeOff').inputmask({ mask: "99:99" });
                $('#StartTime').inputmask({ mask: "99:99" });
                $('#EndTime').inputmask({ mask: "99:99" });
            }
        };
    });
    //app.directive('compile', ['$compile', function ($compile) {

    //    return function (scope, element, attrs) {
    //        scope.$watch(
    //            function (scope) {
    //                // watch the 'compile' expression for changes
    //                return scope.$eval(attrs.compile);
    //            },
    //            function (value) {
    //                // when the 'compile' expression changes
    //                // assign it into the current DOM
    //                element.html(value);

    //                // compile the new DOM and link it to the current
    //                // scope.
    //                // NOTE: we only compile .childNodes so that
    //                // we don't get into infinite loop compiling ourselves
    //                $compile(element.contents())(scope);
    //            }
    //        );
    //    };
    //}]);
}
