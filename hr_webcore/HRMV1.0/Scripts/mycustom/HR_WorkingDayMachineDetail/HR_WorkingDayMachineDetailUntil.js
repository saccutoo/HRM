function BuildTable(appName, controllerName, tableUrl) {
    app.controller(controllerName,
        function ($scope, myService, $filter, $rootScope, $compile, $http) {
            $scope.maxSize = 5; // Limit number for pagination display number.
            $scope.totalCount = 0; // Total number of items in all pages. initialize as
            $scope.pageIndex = 1; // Current page number. First page is 1.-->
            $scope.pageSizeSelected = 5;
            $rootScope.data = {
                filter5: '',
                filter1: '',
                filter2: '',
                filter3: '',
                filter4: '',
                filter6: '',
                filter7: '',
                filter8: '',
            }; //dự liệu truyền vào

            //model đượct truyền ra từ directive build table
            $scope.employeeData = {};
            $scope.employee = [];
            $scope.RemoveListPolicyAllowance = [];


            //Global theo parentID
            $scope.GlobalListWhereParentID = {
                Status: 60,
                Product: 3320
            }

            $scope.Data = {};


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
                    $scope.CallData();
                },
                    function (emp) {
                        AppendToToastr(false, notification, errorNotiFalse);
                    });
            }

            $scope.GetListData = function () {
                ShowLoader();
                var data = {
                    pageIndex: $scope.pageIndex,
                    pageSize: $scope.pageSizeSelected,
                }
                var getDataTbl = myService.GetTableData(data, tableUrl);
                getDataTbl.then(function (emp) {
                    $scope.employees = emp.data.employees;
                    $scope.totalCount = emp.data.totalCount;
                    $scope.lstTotal = emp.data.lstTotal;
                    $rootScope.RoleID = emp.data.roleid;

                    $scope.SetTotalByColumns = function (totalName) {
                        if (!angular.isUndefined(totalName) && totalName !== null) {
                            return $scope.lstTotal[totalName];
                        }
                        return "-";
                    };
                    HiddenLoader();
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
            $scope.formatData = function (type, value) {            
                return value;

                if (type === 3 && value != null) {

                    return FormatDate(value);

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



            // ----------------- Add chính sách-----------           
            $scope.addClick = function () {
                $scope.Data = {};
                $scope.ErrorWorkingDayMachine = "";
                $scope.ErrorStatus = "";
                $scope.ErrorStartTime = "";
                $scope.ErrorEndTime = "";
                $scope.ErrorStartDate = "";
                $scope.ErrorWorkingType = "";
                $scope.ErrorTimekeeping = "";
                $(".StartDate").datepicker({
                    autoclose: true,
                    format: "dd/mm/yyyy",
                }).datepicker("setDate", $scope.Data.StartDate);
                $(".EndDate").datepicker({
                    autoclose: true,
                    format: "dd/mm/yyyy",
                }).datepicker("setDate", $scope.Data.EndDate);
                ShowPopup($,
                    "#SaveHR_WorkingDayMachineDetail",
                    $scope.tableInfo.PopupWidth,
                    $scope.tableInfo.PopupHeight);
            }

            $scope.CallData = function () {
                $scope.ListStatus();           //trạng thái
                $scope.WorkingDayMachineSatList();

            }

            //------------Dropdown trạng thái-------------
            $scope.ListStatus=function() {
                var data = {
                    url: "/Common/GetDataByGloballistnotTree?parentid=" + 88,
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getListStatus = res.data.result;
                },
                    function (res) {
                        $scope.msg = "Error";
                    });
            }

            //Lấy danh sách máy chấm công
            $scope.WorkingDayMachineSatList = function () {
                var data = {
                    url: "/HR_WorkingDayMachineSatList/WorkingDayMachineSatList_GetList/",
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.WorkingDayMachineList = res.data.result;

                }, function (res) {
                    $scope.msg = "Error";
                });

            }

            //Bắt lỗi khi chọn máy chấm công
            $scope.ChangeWorkingDayMachine = function () {
                if ($scope.Data.WorkingDayMachineID == null ) {
                    $scope.ErrorWorkingDayMachine=selectTimekeeper;
                    return;
                }
                else {
                    
                    $scope.ErrorWorkingDayMachine = "";                   
                }
            }

            //bắt lỗi khi chọn trạng thái
            $scope.ChangeStatus = function () {
                if ($scope.Data.Status == null ) {
                    $scope.ErrorStatus = errorStatus;
                    return
                }
                else {
                    $scope.ErrorStatus = "";
                }
            }

            //bắt lỗi khi nhập giờ bắt đầu
            $scope.ChangeStartTime = function () {
                if ($scope.Data.StartTime == null || $scope.Data.StartTime == "") {
                    $scope.ErrorStartTime = errorStartTime;
                    return;
                }
                if ($scope.Data.StartTime != null && $scope.Data.StartTime != "") {
                    $scope.Data.StartTime = $scope.Data.StartTime + ":00";
                    var StartTime = $scope.Data.StartTime.split(":");
               }
                if (StartTime != null && StartTime[0] > 23) {
                    $scope.ErrorStartTime = errorSmallHours;
                    return;
                }
                if (StartTime != null  && StartTime[1]>=60) {
                    $scope.ErrorStartTime = errorStartingMinute;
                    return;
                }
                if (StartTime != null && StartTime[2] >= 60) {
                    $scope.ErrorStartTime = errorSecondsStart;
                    return;
                }
                else {
                    $scope.ErrorStartTime = "";
                    $scope.Timekeeping();
                }
                
            }

            //bắt lỗi khi nhập giờ kết thúc
            $scope.ChangeEndTime = function () {
                
                if ($scope.Data.EndTime == null || $scope.Data.EndTime == "") {
                    $scope.ErrorEndTime = errorHoursEnd;
                    return;
                }
                if ($scope.Data.EndTime != null && $scope.Data.EndTime != "") {
                    $scope.Data.EndTime = $scope.Data.EndTime + ":00";
                    var EndTime = $scope.Data.EndTime.split(":");
                }
                if ($scope.Data.StartTime != null && $scope.Data.StartTime != "" && $scope.Data.EndTime != null && $scope.Data.EndTime != "") {
                    var StartTime = $scope.Data.StartTime.split(":");
                    var EndTime = $scope.Data.EndTime.split(":");                    
                }
                if (EndTime != null && EndTime[0]>24) {
                    $scope.ErrorEndTime = ErrorSmallHours;
                    return;
                }
                else if (EndTime != null && EndTime[0] == "24" && EndTime[1] > 00) {
                    $scope.ErrorEndTime = ErrorStartingMinute;
                    return;
                }
                else if (EndTime != null && EndTime[1]!="-" && EndTime[1]>59) {
                    $scope.ErrorEndTime = ErrorStartingMinute;
                    return;
                }
                else if (EndTime != null && EndTime[2] != "-" && EndTime[2] >= 60) {
                    $scope.ErrorEndTime = ErrorSecondsStart;
                    return;
                }
                else if (EndTime != null && EndTime[0] == "24" && EndTime[2] > 00) {
                    $scope.ErrorEndTime = ErrorSecondsStart;
                    return;
                }
                if (EndTime != null && StartTime!=null && EndTime[0] < StartTime[0]) {
                    $scope.ErrorEndTime = errorCompareHours;
                    return;
                }
                if (EndTime != null && StartTime != null && EndTime[0] == StartTime[0] && EndTime[1] < StartTime[1]) {
                    $scope.ErrorEndTime = errorCompareMinutes;
                    return;
                }
                if (EndTime != null && StartTime != null &&  EndTime[1] >= StartTime[1] && EndTime[2] < StartTime[2]) {
                    $scope.ErrorEndTime =errorCompareSecond;
                    return;
                }
                else {
                    $scope.ErrorEndTime = "";
                    $scope.Timekeeping();
                }
            }

            //bắt lỗi khi nhập ngày bắt đầu '
            $scope.ChangeStartDate = function () {
                if ($scope.Data.StartDate == null || $scope.Data.StartDate == "") {
                    $scope.ErrorStartDate = errorDateOfApplication;
                    return
                }
                else {
                    $scope.ErrorStartDate = "";
                }
            }

            //bắt lỗi khi nhập ngày kết thúc
            $scope.ChangeEndDate = function () {
                if ($scope.Data.EndDate != null) {
                    if ($scope.Data.StartDate != null) {
                        var date = $scope.Data.StartDate.split("/");
                        var date1 = date[2] + "/" + date[1] + "/" + date[0];
                        var date = $scope.Data.EndDate.split("/");
                        var date2 = date[2] + "/" + date[1] + "/" + date[0];
                        date1 = new Date(date1);
                        date2 = new Date(date2);
                        if (date1 > date2) {
                            $scope.ErrorStartDate = errorDate;
                            return;
                        }
                        else {
                            $scope.ErrorEndDate = "";
                            $scope.ErrorStartDate = "";

                        }
                    }

                }
            }
            
            //Bắt lỗi khi nhập công
            //$scope.ChangeTimekeeping = function () {
            //    if ($scope.Data.Timekeeping != null || $scope.Data.Timekeeping != "") {
            //        var postion = $scope.Data.Timekeeping.indexOf("_");
            //    }
            //    if (postion != -1 && postion != null) {
            //        $scope.ErrorTimekeeping = "Bạn chưa nhập đầy đủ trường công";
            //        return;
            //    }
            //    else {
            //        $scope.ErrorTimekeeping = "";
            //    }
            //}

            //Bắt lỗi khi chọn kiểu làm việc
            $scope.ChangeWorkingType = function () {
                if ($scope.Data.WorkingType == null || $scope.Data.WorkingType=="") {
                    $scope.ErrorWorkingType = errorStatusWork;
                    return;
                }
                else {
                    $scope.ErrorWorkingType = "";

                }
            }

            //Tính công
            $scope.Timekeeping = function () {
                if ($scope.Data.StartTime != null && $scope.Data.StartTime != "" && $scope.Data.EndTime != null &&  $scope.Data.EndTime != "") {
                    var ListStartTime = $scope.Data.StartTime.split(":");
                    var ListEndTime = $scope.Data.EndTime.split(":");
                    var temp = ListEndTime[0] - ListStartTime[0];
                    if (temp>=0 && temp<1) {
                        $scope.Data.Timekeeping = "";
                    }
                    if (temp < 3 && temp >= 1) {
                        $scope.Data.Timekeeping = 0.25;
                    }
                    else if (temp == 3) {
                        $scope.Data.Timekeeping = 0.25;
                    }
                    else if (temp > 3 && temp < 6) {
                        $scope.Data.Timekeeping = 0.5;
                    }
                    else if (temp >= 6 && temp < 9) {
                        $scope.Data.Timekeeping = 0.75;
                    }
                    else if (temp >= 9 && temp<=10) {
                        $scope.Data.Timekeeping = 1;
                    }
                    else {
                        $scope.Data.Timekeeping = "";
                    }
                    if (ListEndTime[0] == "12" && ListStartTime[0]=="12") {
                        $scope.Data.Timekeeping = 0;
                        return;
                    }
                    if (ListStartTime[0] == "12") {
                        $scope.Data.Timekeeping = "";
                    }
                    if (ListStartTime[0] >= "18" && ListStartTime[1]>="00") {
                        $scope.Data.Timekeeping = "";
                    }
                    if (ListStartTime[0] >= "18" && ListStartTime[1] >= "00") {
                        $scope.Data.Timekeeping = "";
                    }
                    if (ListStartTime[0]<"07" ) {
                        $scope.Data.Timekeeping = "";
                    }
                    //if (ListStartTime[0] == "07" && ListStartTime[1]<="29") {
                    //    $scope.Data.Timekeeping = "";
                    //}
                    
                }
            }

            // -----------------ADD hoặc edit chính sách--------------
            $scope.Save = function () {
                if ($scope.Data.WorkingDayMachineID == null) {
                    $scope.ErrorWorkingDayMachine = selectTimekeeper;
                    return;
                }
                else if ($scope.Data.Status == null) {
                    $scope.ErrorStatus = errorStatus;
                    return;
                }
                else if ($scope.Data.StartTime == null || $scope.Data.StartTime=="") {
                    $scope.ErrorStartTime = errorStartTime;
                    return;
                }
                if ($scope.Data.StartTime != null && $scope.Data.StartTime != "") {
                    var StartTime = $scope.Data.StartTime.split(":");
                }
                if (StartTime != null && StartTime[0] > 23) {
                    $scope.ErrorStartTime = errorSmallHours;
                    return;
                }
                if (StartTime != null && StartTime[1] >= 60) {
                    $scope.ErrorStartTime = errorStartingMinute;
                    return;
                }
                if (StartTime != null && StartTime[2] >= 60) {
                    $scope.ErrorStartTime = errorSecondsStart;
                    return;
                }
                else if ($scope.Data.EndTime == null || $scope.Data.EndTime == "") {
                    $scope.ErrorEndTime = errorStartTime;
                    return;
                }
                if ($scope.Data.StartTime != null && $scope.Data.StartTime != "" && $scope.Data.EndTime != null && $scope.Data.EndTime != "") {
                    var StartTime = $scope.Data.StartTime.split(":");
                    var EndTime = $scope.Data.EndTime.split(":");
                }
                if (EndTime != null && EndTime[0] > 24) {
                    $scope.ErrorEndTime = ErrorSmallHours;
                    return;
                }
                else if (EndTime != null && EndTime[0] == "24" && EndTime[1] > 00) {
                    $scope.ErrorEndTime = ErrorStartingMinute;
                    return;
                }
                else if (EndTime != null && EndTime[1] != "-" && EndTime[1] > 59) {
                    $scope.ErrorEndTime = ErrorStartingMinute;
                    return;
                }
                else if (EndTime != null && EndTime[2] != "-" && EndTime[2] >= 60) {
                    $scope.ErrorEndTime = ErrorSecondsStart;
                    return;
                }
                else if (EndTime != null && EndTime[0] == "24" && EndTime[2] > 00) {
                    $scope.ErrorEndTime = ErrorSecondsStart;
                    return;
                }
                if (EndTime != null && StartTime != null && EndTime[0] < StartTime[0]) {
                    $scope.ErrorEndTime = errorCompareHours;
                    return;
                }
               
                if (EndTime != null && StartTime != null && EndTime[0] == StartTime[0] && EndTime[1] < StartTime[1]) {
                    $scope.ErrorEndTime = errorCompareMinutes;
                    return;
                }
                if (EndTime != null && StartTime != null && EndTime[1] >= StartTime[1] && EndTime[2] < StartTime[2]) {
                    $scope.ErrorEndTime =errorCompareSecond;
                    return;
                }
                if ($scope.Data.StartDate == null || $scope.Data.StartDate == "") {
                    $scope.ErrorStartDate = errorDateOfApplication;
                    return;
                }
                if ($scope.Data.EndDate != null && $scope.Data.EndDate != "") {
                    var date = $scope.Data.StartDate.split("/");
                    var date1 = date[2] + "/" + date[1] + "/" + date[0];
                    var date = $scope.Data.EndDate.split("/");
                    var date2 = date[2] + "/" + date[1] + "/" + date[0];
                    date1 = new Date(date1);
                    date2 = new Date(date2);

                }
                if (date1 != null && date2 != null && date1 > date2) {
                    $scope.ErrorStartDate = errorDate;
                    return;
                }             
                else if ($scope.Data.WorkingType == null || $scope.Data.WorkingType == "") {
                    $scope.ErrorWorkingType = errorStatus;
                    return;
                }
                else {
                    $scope.ErrorWorkingDayMachine = "";
                    $scope.ErrorStatus = "";
                    $scope.ErrorStartTime = "";
                    $scope.ErrorEndTime = "";
                    $scope.ErrorStartDate = "";
                    $scope.ErrorTimekeeping="";
                    $scope.ErrorWorkingType = "";
                    var date = $scope.Data.StartDate.split("/");
                    $scope.Data.StartDate = date[2] + "/" + date[1] + "/" + date[0];
                    if ($scope.Data.EndDate != null && $scope.Data.EndDate != "") {
                        var date = $scope.Data.EndDate.split("/");
                        $scope.Data.EndDate = date[2] + "/" + date[1] + "/" + date[0];
                    }
                    var SaveAction = myService.UpdateData("/HR_WorkingDayMachineDetail/HR_WorkingDayMachineDetail_Save", $scope.Data);
                    SaveAction.then(function (res) {
                        if (res.data.result.IsSuccess == true) {
                            if ($scope.Data.AutoID != 0 && $scope.Data.AutoID != null) {
                                AppendToToastr(true, notification, successfulUpdate, 500, 5000);
                            }
                            else {
                                AppendToToastr(true, notification, successfulAdd, 500, 5000);

                            }
                            $scope.HR_WorkingDayMachineDetailData.reload();
                            $scope.Data = {};
                            $scope.CloseForm();
                        }
                        else {
                            AppendToToastr(false, notification, updateFailed, 500, 5000);
                            $scope.HR_WorkingDayMachineDetailData.reload();
                            $scope.CloseForm();

                        }
                    }, function (res) {
                        AppendToToastr(false, notification, errorNotiFalse);
                    });
                }
                

            }

            //-----------------Edit Chính sách--------------
            $scope.editClick = function (contentItem) {
                $scope.Data = {};
                $scope.ErrorWorkingDayMachine = "";
                $scope.ErrorStatus = "";
                $scope.ErrorStartTime = "";
                $scope.ErrorEndTime = "";
                $scope.ErrorStartDate = "";
                $scope.ErrorWorkingType = "";
                $scope.ErrorTimekeeping="";
                $scope.Data.AutoID = contentItem.AutoID;
                $scope.Data.WorkingDayMachineID = contentItem.WorkingDayMachineID;
                $scope.Data.Status = contentItem.Status.toString();
                $scope.Data.StartTime = contentItem.StartTime;
                $scope.Data.EndTime = contentItem.EndTime;
                $scope.Data.StartDate = FormatDate(contentItem.StartDate);
                if (contentItem.EndDate!=null) {
                    $scope.Data.EndDate = FormatDate(contentItem.EndDate);
                }
                if (contentItem.Timekeeping != null) {
                    $scope.Data.Timekeeping = contentItem.Timekeeping;
                }
                $scope.Data.WorkingType = contentItem.WorkingType.toString();
                $(".StartDate").datepicker({
                    autoclose: true,
                    format: "dd/mm/yyyy",
                }).datepicker("setDate", $scope.Data.StartDate);
                $(".EndDate").datepicker({
                    autoclose: true,
                    format: "dd/mm/yyyy",
                }).datepicker("setDate", $scope.Data.EndDate);
                ShowPopup($,
                   "#SaveHR_WorkingDayMachineDetail",
                   $scope.tableInfo.PopupWidth,
                   $scope.tableInfo.PopupHeight);
            }


            //load lại trang khi click bỏ qua
            $scope.CloseForm = function () {
                $scope.ErrorFromAmount = "";
                $scope.ErrorStartDate = "";
                $scope.ErrorCurrencyID = "";
                $scope.ErrorStatus = "";
                $scope.ErrorCountryID = "";
                $scope.Data = {};
                $.colorbox.close();
            }


            // -----------------End Edit Add Click--------------
            //------------------Add-Save---------

            // -----------------Xóa------------

            $scope.deleteClick = function (obj) {
                BoostrapDialogConfirm(notification,
                    notificationDelete,
                    BootstrapDialog.TYPE_WARNING,
                    $scope.deleteActionClick,
                    obj);
            }
            $scope.deleteActionClick = function (obj) {
                var ID = obj.AutoID;
                var action = myService.deleteAction(ID, $scope.tableInfo.id, $scope.tableInfo.DeleteUrl);
                action.then(function (res) {
                    if (res.data.result.IsSuccess == true) {
                        AppendToToastr(res.data.result.IsSuccess, notification, deleteSuccess, 500, 5000);
                        $scope.HR_WorkingDayMachineDetailData.reload();
                    }
                    else {
                        AppendToToastr(false, notification, errorNotiFalse);

                    }
                },
                    function (res) {
                        AppendToToastr(false, notification, errorNotiFalse);
                    });

            }

            // -----------------Xóa--End------------


            //-------------------Excel--------------
            $scope.ExcelClick = function () {
                var filterString = $scope.getFilterValue();
                window.location = $scope.tableInfo.ExcelUrl + "?pageIndex=" + $scope.pageIndex + "&pageSize=" + $scope.pageSizeSelected + "&filter=" + filterString;
            }
            //-------------------Excel-End----------







            //Dropdown phòng ban
            function ListOrganizationUnit() {
                var data = {
                    url: "/OrganizationUnit/GetOrganizationUnit?chon=1"
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getListOrganizationUnit = res.data.result;

                }, function (res) {
                    $scope.msg = "Error";
                })
            }




            //----------------------------------------------------------------------------------------------------------------------------------------------------------------------





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
                    //console.log(event.which);
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
    app.directive('autoComplete', [
        '$timeout', function ($timeout) {
            return {
                require: 'ngModel',
                link: function ($scope, element, attrs, ctrl) {
                    var fAutoComplete;
                    fAutoComplete = function () {
                        $timeout(function () {
                            if (!$scope[attrs.uiItems]) {
                                fAutoComplete();
                            } else {
                                element.autocomplete({
                                    source: [$scope[attrs.uiItems]]
                                }).on('selected.xdsoft', function (e, newValue) {
                                    ctrl.$setViewValue(newValue);
                                    $scope.$apply();
                                });
                            }
                        }, 5);
                    };
                    return fAutoComplete();
                }
            };
        }
    ])
}
