function BuildTable(appName, controllerName, tableUrl, tableUrl1, tableUrl2) {
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
            $scope.FormatColumn = true;
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
            $scope.getTableColumnsSpendingAdjustmentRate = function () {
                var getData = myService.GetColumns(tableUrl2);
                getData.then(function (emp) {
                    debugger;
                    $scope.ListColumnSpendingAdjustmentRate = emp.data.result;
                    //$scope.GetListConfigAllowance();
                },
                    function (emp) {
                        AppendToToastr(false, notification, errorNotiFalse);
                    });
            }
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
                    $scope.getColumns1();
                    // $scope.BuildAddButton(emp.data.result);
                },
                    function (emp) {
                        AppendToToastr(false, notification, errorNotiFalse);
                    });
            }
            $scope.getTableInfo();
            $scope.getTableColumnsSpendingAdjustmentRate();
            //$scope.getColumns = function () {
            //    var getData = myService.GetColumns(tableUrl);
            //    getData.then(function (emp) {
            //        $scope.Columns = emp.data.result;
            //        $scope.GetListData();
            //    },
            //        function (emp) {
            //            AppendToToastr(false, notification, errorNotiFalse);
            //        });
            //}

            //$scope.GetListData = function () {
            //    ShowLoader();
            //    var data = {
            //        pageIndex: $scope.pageIndex,
            //        pageSize: $scope.pageSizeSelected,
            //    }
            //    var getDataTbl = myService.GetTableData(data, tableUrl);
            //    getDataTbl.then(function (emp) {
            //        $scope.employees = emp.data.employees;
            //        $scope.totalCount = emp.data.totalCount;
            //        $scope.lstTotal = emp.data.lstTotal;
            //        $rootScope.RoleID = emp.data.roleid;

            //        $scope.SetTotalByColumns = function (totalName) {
            //            if (!angular.isUndefined(totalName) && totalName !== null) {
            //                return $scope.lstTotal[totalName];
            //            }
            //            return "-";
            //        };
            //        HiddenLoader();
            //    },
            //        function (emp) {
            //            AppendToToastr(false, notification, errorNotiFalse);
            //        });

            //}

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
                if (type === 3) {

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



            //-----------------Hiển thị lỗi khi nhập tên chính sách-----------    
            $scope.ChangeName = function () {
                if ($scope.Data.Name == "" || $scope.Data.Name == null) {
                    $scope.ErrorName = errorNamePolicy;
                }
                else {
                    $scope.ErrorName = "";
                }
            }

            //-----------------Hiển thị lỗi khi chưa chọn trạng thái-----------    
            $scope.ChangeStatus = function () {
                if ($scope.Data.Status == "" || $scope.Data.Status == null) {
                    $scope.ErrorStatus = errorStatus;
                }
                else {
                    $scope.ErrorStatus = "";
                }
            }

            //-----------------Hiển thị lỗi khi chưa nhập ngày bắt đầu-----------    
            $scope.ChangeStartDate = function () {
                if ($scope.Data.StartDate == null || $scope.Data.StartDate == "") {
                    $scope.ErrorStartDate = errorSelectDateStart;
                    return;
                }

                else {
                    $scope.ErrorStartDate = "";
                }
            }

            //-----------------Hiển thị lỗi khi chưa nhập ngày kết thúc-----------    
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
            //------------Dropdown chính sách -------------
            $scope.GetListPolicy = function () {
                var data = {
                    url: "/Common/GetListPolicy"
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.ListPolicy = res.data.result;
                },
                    function (res) {
                        $scope.msg = "Error";
                    });
            }
            $scope.GetListPolicy();

            //$scope.ChangeAllowanceID = function (content) {

            //    var tempArr = [];
            //    var ListAllowanceID = [];
            //    var a = [...document.querySelectorAll('#AllowanceID')].map(e=>e.value);
            //    for (var i = 0; i < a.length; i++) {
            //        if (a[i]!="") {
            //            var number = a[i].slice(7);
            //            tempArr.push(number)
            //        }

            //    }


            //    for (i = 0; i < tempArr.length; i++) {
            //        if (ListAllowanceID.filter(e=>e == tempArr[i]).length > 0) {
            //            AppendToToastr(false, "Phụ cấp này đang tồn tại trong danh sách !");
            //            return
            //        }
            //        else {
            //            ListAllowanceID.push(tempArr[i]);
            //        }
            //    }

            //}

            //-----------------Hiển thị lỗi khi chưa nhập ngày-----------    
            $scope.ChangeDate = function () {
                if ($scope.Data.Date == null || $scope.Data.Date == "") {
                    $scope.ErrorDate = errorCreateDate;
                    return;
                }
                else {
                    $scope.ErrorDate = "";
                }
            }
            $scope.ChangeRewards = function () {
                if ($scope.Data.PolicyBonusID == null || $scope.Data.PolicyBonusID == "") {
                    $scope.ErrorRewards = errorRewards;
                    return;
                }
                else {
                    $scope.ErrorRewards = "";
                }
            }
            //-----------------Hiển thị lỗi khi chọn phụ cấp-----------    


            // ----------------- Add chính sách-----------           
            $scope.addClick = function () {
                $scope.ErrorName = "";
                $scope.ErrorDate = "";
                $scope.ErrorStatus = "";
                ListStatus();
                $scope.Data = {};
                $scope.employees1 = [];
                $scope.RemoveListPolicyAllowance = [];
                $scope.Data.ListSpendingAdjustmentRate = [];
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
                $scope.Data.Date = dd + "/" + mm + "/" + yyyy;
                $(".StartDate").datepicker({
                    autoclose: true,
                    format: "dd/mm/yyyy"
                }).datepicker("setDate", $scope.Data.StartDate);
                $(".EndDate").datepicker({
                    autoclose: true,
                    format: "dd/mm/yyyy"
                }).datepicker("setDate", $scope.Data.EndDate);
                $(".Date").datepicker({
                    autoclose: true,
                    format: "dd/mm/yyyy"
                }).datepicker("setDate", $scope.Data.Date);
                ShowPopup($,
                    "#SavePolicy",
                    $scope.tableInfo.PopupWidth,
                    $scope.tableInfo.PopupHeight);
            }


            //------------Dropdown trạng thái-------------
            function ListStatus() {
                var data = {
                    url: "/Common/GetDataByGloballistnotTree?parentid=" + 88
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getListStatus = res.data.result;
                },
                    function (res) {
                        $scope.msg = "Error";
                    });
            }

            //------------Dropdown KPI------------
            function ListKPI() {
                var data = {
                    url: "/Common/GetDataByGloballistnotTree?parentid=" + 3494
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getListKPI = res.data.result;
                },
                    function (res) {
                        $scope.msg = "Error";
                    });
            }
            ListKPI();
            function ListKpiByPolicyID(PolicyID) {
                var data = {
                    url: "/Policy/ListKpiByPolicyID?PolicyId=" + PolicyID
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.ListKpiByPolicyID = angular.copy(res.data.result);
                    $scope.Data.ListKPI = [];
                    if ($scope.ListKpiByPolicyID != null && $scope.ListKpiByPolicyID.length > 0) {
                        for (var i = 0; i < $scope.ListKpiByPolicyID.length; i++) {
                            $scope.Data.ListKPI.push($scope.ListKpiByPolicyID[i].KpiID);
                        }
                    }
                },
                    function (res) {
                        $scope.msg = "Error";
                    });
            }

            //------------Dropdown thưởng chính sách------------
            function getListBonusPolicy() {
                var data = {
                    url: "/Common/GetDataByGloballistnotTree?parentid=" + 3541
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getListBonusPolicy = res.data.result;
                },
                    function (res) {
                        $scope.msg = "Error";
                    });
            }
            getListBonusPolicy();
            // -----------------ADD hoặc edit chính sách--------------
            $scope.Save = function () {
                if ($scope.Data.Name == "" || $scope.Data.Name == null) {
                    $scope.ErrorName = errorNamePolicy;
                    return;
                }
                else if ($scope.Data.Status == null || $scope.Data.Status == "") {
                    $scope.ErrorStatus = errorStatus;
                    return;
                }
                else if ($scope.Data.StartDate == null || $scope.Data.StartDate == "") {
                    $scope.ErrorStartDate = errorSelectDateStart;
                    return;
                }

                else if ($scope.Data.Date == null || $scope.Data.Date == "") {
                    $scope.ErrorDate = errorCreateDate;
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
                if ($scope.Data.PolicyBonusID == null || $scope.Data.PolicyBonusID == '') {
                    $scope.ErrorRewards = errorRewards;
                    return;
                }
                else {
                    $scope.ErrorName = "";
                    $scope.ErrorStatus = "";
                    $scope.ErrorStartDate = "";
                    $scope.ErrorEndDate = "";
                    $scope.ErrorDate = "";

                    var tempArr = [];
                    var ListAllowanceID = [];
                    var a = [...document.querySelectorAll('#AllowanceID')].map(e=>e.value);
                    for (var i = 0; i < a.length; i++) {
                        var number = a[i].slice(7);
                        tempArr.push(number)
                    }


                    for (i = 0; i < tempArr.length; i++) {
                        if (ListAllowanceID.filter(e=>e == tempArr[i]).length > 0) {
                            AppendToToastr(false, notification, errorPolicyDupicate);
                            return
                        }
                        else {
                            ListAllowanceID.push(tempArr[i]);
                        }
                    }
                    for (var i = 0; i < a.length; i++) {
                        if (a[i] == "") {
                            AppendToToastr(false, notification, errorSelectPolicyAllowance);
                            return
                        }
                    }
                    var date = $scope.Data.StartDate.split("/");
                    $scope.Data.StartDate = date[2] + "/" + date[1] + "/" + date[0];

                    if ($scope.Data.EndDate != null && $scope.Data.EndDate != "") {
                        var date = $scope.Data.EndDate.split("/");
                        $scope.Data.EndDate = date[2] + "/" + date[1] + "/" + date[0];

                    }

                    var date = $scope.Data.Date.split("/");
                    $scope.Data.CreatedDate = date[2] + "/" + date[1] + "/" + date[0];
                    var check = $http({
                        method: "POST",
                        url: "/Policy/CheckPolicyName",
                        data: {
                            data: $scope.Data,
                        },
                        dataType: "json"
                    });
                    check.then(function (res) {
                        if (res.data.result.Message == "1") {
                            $("#check").modal("show");
                            $scope.Messenger = MessengerCheck;
                        }
                        else {
                            $scope.continue();
                        }
                    }, function (res) {
                        AppendToToastr(false, notification, errorNotiFalse);
                    });                  
                }



            }

            $scope.continue = function () {
                var SaveAction = $http({
                    method: "POST",
                    url: "/Policy/Policy_Save",
                    data: {
                        data: $scope.Data,
                        List: $scope.employees1,                           
                        RemoveListPolicyAllowance: $scope.RemoveListPolicyAllowance,
                    },
                    dataType: "json"
                });
                SaveAction.then(function (res) {
                    if (res.data.result.IsSuccess == true) {
                        if ($scope.Data.PolicyID != 0 && $scope.Data.PolicyID != null) {
                            AppendToToastr(true, notification, successfulUpdate, 500, 5000);
                        }
                        else {
                            AppendToToastr(true, notification, successfulAdd, 500, 5000);
                        }
                        $scope.PolicyData.reload();
                        $scope.Data = {};
                        $scope.CloseForm();
                        $("#check").modal("hide");
                    }
                    else {
                        AppendToToastr(false, notification, updateFailed, 500, 5000);
                        $scope.PolicyData.reload();
                        $scope.CloseForm();
                        $("#check").modal("hide");
                    }
                }, function (res) {
                    AppendToToastr(false, notification, errorNotiFalse);
                });
            }
            //-----------------Edit Chính sách--------------
            $scope.editClick = function (contentItem) {
                $scope.list = [...document.querySelectorAll('#AllowanceID')].map(e=>e.value);
                $scope.Data = {};
                $scope.RemoveListPolicyAllowance = [];
                var getListSpending = $http({
                    method: "POST",
                    url: "/Policy/SpendingAdjustmentRateGetData",
                    data: {
                        policyId: contentItem.PolicyID != undefined ? contentItem.PolicyID : 0
                    },
                    dataType: "json"
                });
                getListSpending.then(function (res) {
                    $scope.Data.ListSpendingAdjustmentRate = res.data.ListSpending;
                });

                ListStatus();

                $scope.Data.PolicyID = contentItem.PolicyID;
                if (contentItem.PolicyBonusID != null && contentItem.PolicyBonusID != 0) {
                    $scope.Data.PolicyBonusID = contentItem.PolicyBonusID;
                }
                ListKpiByPolicyID($scope.Data.PolicyID);
                $scope.Data.Name = contentItem.Name;

                if (contentItem.Status != null) {
                    $scope.Data.Status = contentItem.Status.toString();
                }
                if (contentItem.StartDate != null) {
                    $scope.Data.StartDate = FormatDate(contentItem.StartDate);
                }
                else {
                    $scope.Data.StartDate = null;
                }
                if (contentItem.EndDate != null) {
                    $scope.Data.EndDate = FormatDate(contentItem.EndDate);
                }
                else {
                    $scope.Data.EndDate = null;
                }
                if (contentItem.CreatedDate != null) {
                    $scope.Data.Date = FormatDate(contentItem.CreatedDate);
                }
                else {
                    $scope.Data.Date = null;
                }
                $scope.Data.Note = contentItem.Note;
                if (contentItem.PolicyincludeID != null && contentItem.PolicyincludeID != 0) {
                    $scope.Data.PolicyincludeID = contentItem.PolicyincludeID;
                }

                $(".StartDate").datepicker({
                    autoclose: true,
                    format: "dd/mm/yyyy"
                }).datepicker("setDate", $scope.Data.StartDate);
                $(".EndDate").datepicker({
                    autoclose: true,
                    format: "dd/mm/yyyy"
                }).datepicker("setDate", $scope.Data.EndDate);
                $(".Date").datepicker({
                    autoclose: true,
                    format: "dd/mm/yyyy"
                }).datepicker("setDate", $scope.Data.Date);

                var data = {
                    pageIndex: $scope.pageIndex,
                    pageSize: $scope.pageSizeSelected,
                    filter: "PolicyID=" + $scope.Data.PolicyID,
                }

                var getDataTbl = myService.GetTableData(data, tableUrl1);
                getDataTbl.then(function (emp) {
                    $scope.employees1 = emp.data.employees;

                    HiddenLoader();
                },
                    function (emp) {

                        AppendToToastr(false, notification, errorNotiFalse);
                    });

                ShowPopup($,
                   "#SavePolicy",
                   $scope.tableInfo.PopupWidth,
                   $scope.tableInfo.PopupHeight);
            }

            $scope.getColumns1 = function () {
                var getData = myService.GetColumns(tableUrl1);
                getData.then(function (emp) {
                    $scope.Columns1 = emp.data.result;
                    $scope.GetListConfigAllowance();
                },
                    function (emp) {
                        AppendToToastr(false, notification, errorNotiFalse);
                    });
            }

            //Xóa phụ cấp trong mảng
            $scope.removeChoice = function (index, employee) {
                $scope.employees1.splice(index, 1);
                if (employee.AutoID != undefined) {
                    $scope.RemoveListPolicyAllowance.push(employee.AutoID);
                }

            };

            //Thêm phụ cấp trong mảng
            $scope.addNewChoice = function () {
                var newItemNo = $scope.employees1.length + 1;
                $scope.employees1.push({});
            };

            //Xóa AdjustmentRate trong mảng
            $scope.removeAdjustmentRate = function (index) {
                $scope.Data.ListSpendingAdjustmentRate.splice(index, 1);
            };
            //Thêm AdjustmentRate trong mảng
            $scope.addAdjustmentRate = function () {
                $scope.Data.ListSpendingAdjustmentRate.push({});
            };

            //load lại trang khi click bỏ qua
            $scope.CloseForm = function () {
                $scope.ErrorName = "";
                $scope.ErrorDate = "";
                $scope.ErrorStatus = "";
                $scope.Data = {};
                $scope.employees1 = [];
                $scope.RemoveListPolicyAllowance = [];
                $.colorbox.close();
            }

            //lấy danh sách phụ cấp
            $scope.GetListConfigAllowance = function () {
                var data = {
                    pageIndex: $scope.pageIndex,
                    pageSize: 10000,
                }
                var getDataTbl = myService.GetTableData(data, "/ConfigAllowance/TableServerSideGetData");
                getDataTbl.then(function (emp) {
                    $scope.ListConfigAllowance = emp.data.employees
                    HiddenLoader();
                },
                    function (emp) {
                        AppendToToastr(false, notification, errorNotiFalse);
                    });

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
                var ID = obj.PolicyID;
                var action = myService.deleteAction(ID, $scope.tableInfo.id, $scope.tableInfo.DeleteUrl);
                action.then(function (res) {
                    if (res.data.result.IsSuccess == true) {
                        AppendToToastr(res.data.result.IsSuccess, notification, deleteSuccess, 500, 5000);
                        $scope.PolicyData.reload();
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


            function ListStaff() {
                var data = {
                    url: "/Employee/GetStaff"
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getListStaff = res.data.result;
                },
                    function (res) {
                        $scope.msg = "Error";
                    });
            }

            function ListProduct() {
                var data = {
                    url: "/Common/GetDataByGloballistnotTree?parentid=" + $scope.GlobalListWhereParentID.Product
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getListProduct = res.data.result;

                },
                    function (res) {
                        $scope.msg = "Error";
                    });
            }
            function ListStaffWhereRoleBD() {
                var data = {
                    url: "/PaymentProduct/GetsBD"
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getListStaffWhereRoleBD = res.data.result;

                },
                    function (res) {
                        $scope.msg = "Error";
                    });
            }



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
