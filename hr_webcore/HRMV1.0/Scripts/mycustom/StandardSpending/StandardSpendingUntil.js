function BuildTable(appName, controllerName, tableUrl, tableUrl1) {
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
            $scope.employee = [];


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
                    $scope.Data();
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
                if (type === 3 && value != null) {

                    return FormatDate(value);

                }
                if (type === 2 && value != null) {
                    return formatNumber(value);
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

            //-----------------Hiển thị lỗi khi chọn cập bậc nhân viên-----------
            $scope.ChangeStaffLevel = function () {
                if ($scope.Data.StaffLevelId == "" || $scope.Data.StaffLevelId == null) {
                    $scope.ErrorStaffLevel = errorEmployeeLevel;
                    return
                }
                else {
                    $scope.ErrorStaffLevel = "";
                }
            }

            //-----------------Hiển thị lỗi khi nhập tên chính sách-----------    
            $scope.ChangeStandardSpendingAmount = function () {
                if ($scope.Data.StandardSpendingAmount == "" || $scope.Data.StandardSpendingAmount == null) {
                    $scope.ErrorStandardSpendingAmount = errorStandardSpending;
                    return
                }
                var position = $scope.Data.StandardSpendingAmount.indexOf(',');
                if (position != -1) {
                    var x = $scope.Data.StandardSpendingAmount.split(",");
                    var list = x.join("");

                }
                else if (isNaN($scope.Data.StandardSpendingAmount)) {
                    $scope.ErrorStandardSpendingAmount = errorSpendingIsNumber;
                    return
                }
                if (list != null && isNaN(list)) {
                    $scope.ErrorStandardSpendingAmount = errorSpendingIsNumber;
                    return
                }
                else {
                    $scope.ErrorStandardSpendingAmount = "";
                    var x = $scope.Data.StandardSpendingAmount;
                    var x1 = x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                    var x2 = x1.split(",");
                    var list = parseInt(x2.join(""));
                    $scope.Data.StandardSpendingAmount = list.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                }
            }

            //-----------------Hiển thị lỗi khi chưa chọn chính sách-----------    
            $scope.ChangePolicy = function () {
                if ($scope.Data.PolicyID == "" || $scope.Data.PolicyID == null) {
                    $scope.ErrorPolicy = errorSelectPolicy;
                    return
                }
                else {
                    $scope.ErrorPolicy = "";
                }
            }

            //-----------------Hiển thị lỗi khi nhập chi tiêu tối thiểu-----------    
            $scope.ChangeMinSpending = function () {
                $scope.ErrorMinSpending = "";
                if ($scope.Data.MinSpending != "" && $scope.Data.MinSpending != null) {
                    var position = $scope.Data.MinSpending.indexOf(',');
                    if (position != -1) {
                        var x = $scope.Data.MinSpending.split(",");
                        var list = x.join("");
                    }
                    else if (isNaN($scope.Data.MinSpending)) {
                        $scope.ErrorMinSpending = errorSpendingIsNumber;
                        return
                    }
                    if (list != null && isNaN(list)) {
                        $scope.ErrorMinSpending = errorSpendingIsNumber;
                        return
                    }
                    else {
                        var x = $scope.Data.MinSpending;
                        var x1 = x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var x2 = x1.split(",");
                        var list = parseInt(x2.join(""));
                        $scope.Data.MinSpending = list.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                    }
                }
                else {
                    $scope.ErrorMinSpending = "";
                }
                
            }

            //-----------------Hiển thị lỗi khi nhập nhân viên tối thiểu-----------    
            $scope.ChangeMinPerson = function () {
                if ($scope.Data.MinPerson != null) {
                    if ($scope.Data.MinPerson != "") {
                        
                        var position = $scope.Data.MinPerson.toString().indexOf(',');
                        if (position != -1) {
                            $scope.ErrorMinPerson = errorInteger;
                            return

                        }
                        var position = $scope.Data.MinPerson.toString().indexOf('.');
                        if (position != -1) {
                            $scope.ErrorMinPerson = errorInteger;
                            return

                        }
                        if (isNaN($scope.Data.MinPerson)) {
                            $scope.ErrorMinPerson = errorEmployeesIsNumber;
                            return
                        }
                        else {
                            $scope.ErrorMinPerson = "";
                        }
                    }
                    else {
                        $scope.ErrorMinPerson = "";
                    }
                }
                else {
                    $scope.ErrorMinPerson = "";
                }
            }

            //-----------------Hiển thị lỗi khi chưa chọn trạng thái-----------    
            $scope.ChangeStatus = function () {
                if ($scope.Data.Status == "" || $scope.Data.Status == null) {
                    $scope.ErrorStatus = errorStatus;
                    return
                }
                else {
                    $scope.ErrorStatus = "";
                }
            }

            //-----------------Hiển thị lỗi khi chưa nhập ngày bắt đầu-----------    
            $scope.ChangeStartDate = function () {
                if ($scope.Data.StartDate == null || $scope.Data.StartDate == "") {
                    $scope.ErrorStartDate = errorDateStart;
                    return;
                }

                else {
                    $scope.ErrorStartDate = "";
                }
            }

            //-----------------Hiển thị lỗi khi nhập ngày kết thúc-----------    
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

            //-----------------Hiển thị lỗi khi chưa nhập ngày tạo-----------    
            $scope.ChangeCreateDate = function () {
                if ($scope.Data.CreatedDate == null || $scope.Data.CreatedDate == "") {
                    $scope.ErrorCreateDate = errorCreateDate;
                    return;
                }

                else {
                    $scope.ErrorCreateDate = "";
                }
            }


            //-----------------Hiển thị lỗi khi nhập ngày sửa-----------    
            $scope.ChangeModifiedDate = function () {
                if ($scope.Data.ModifiedDate != null) {
                    if ($scope.Data.CreatedDate != null) {
                        var date = $scope.Data.CreatedDate.split("/");
                        var date1 = date[2] + "/" + date[1] + "/" + date[0];
                        var date = $scope.Data.ModifiedDate.split("/");
                        var date2 = date[2] + "/" + date[1] + "/" + date[0];
                        date1 = new Date(date1);
                        date2 = new Date(date2);
                        if (date1 > date2) {
                            $scope.ErrorModifiedDate = errorCompareDate;
                            return;
                        }
                        else {
                            $scope.ErrorCreateDate = "";
                            $scope.ErrorModifiedDate = "";
                        }
                    }

                }

            }
           

            //-----------------Hiển thị lỗi khi chọn phụ cấp-----------    


            // ----------------- Add chính sách-----------           
            $scope.addClick = function () {
                $scope.Data.StandardSpendingAmount = 0;
                $scope.ErrorStaffLevel = "";
                $scope.ErrorStandardSpendingAmount = "";
                $scope.ErrorPolicy = "";
                $scope.ErrorMinSpending = "";
                $scope.ErrorMinPerson = "";
                $scope.ErrorStatus = "";
                $scope.ErrorStartDate = "";
                $scope.ErrorEndDate = "";
                $scope.ErrorCreateDate = "";
                $scope.ErrorModifiedDate = "";

                $scope.Data = {};
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
                $scope.Data.StartDate = dd + "/" + mm + "/" + yyyy;
                $scope.Data.CreatedDate = dd + "/" + mm + "/" + yyyy;
                $(".StartDate").datepicker({
                    autoclose: true,
                    format: "dd/mm/yyyy",
                }).datepicker("setDate", $scope.Data.StartDate);
                $(".EndDate").datepicker({
                    autoclose: true,
                    format: "dd/mm/yyyy",
                }).datepicker("setDate", $scope.Data.EndDate);
                ShowPopup($,
                    "#SaveStandardSpending",
                    $scope.tableInfo.PopupWidth,
                    $scope.tableInfo.PopupHeight);
            }

            //------------Dropdown cấp độ nhân viên-------------
            function ListStaffLevel() {
                var data = {
                    url: "/Common/GetDataByGloballistnotTree?parentid=" + 21
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.ListStaffLevel = res.data.result;

                },
                    function (res) {
                        $scope.msg = "Error";
                    });
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
            ListStatus();

           
            //------------Dropdown policy-------------
            function ListPolicy() {
                var data = {
                    pageIndex: $scope.pageIndex,
                    pageSize: 50000,
                }
                var getDataTbl = myService.GetTableData(data, "/Policy/TableServerSideGetData");
                getDataTbl.then(function (emp) {
                    $scope.ListPolicy = emp.data.employees;

                },
                    function (emp) {
                        AppendToToastr(false, notification, errorNotiFalse);
                    });
            }
            


            $scope.Data = function () {
                ListPolicy();
                ListStaffLevel();
                
            }

           

            // -----------------ADD hoặc edit chính sách--------------
            $scope.Save = function () {
                if ($scope.Data.StaffLevelId == "" || $scope.Data.StaffLevelId == null) {
                    $scope.ErrorStaffLevel = errorEmployeeLevel;
                    return
                }
                
                if ($scope.Data.StandardSpendingAmount == "" || $scope.Data.StandardSpendingAmount == null) {
                    $scope.ErrorStandardSpendingAmount = errorStandardSpending;
                    return
                }
                var position = $scope.Data.StandardSpendingAmount.indexOf(',');
                if (position != -1) {
                    var x = $scope.Data.StandardSpendingAmount.split(",");
                    var list = x.join("");

                }
                else if (isNaN($scope.Data.StandardSpendingAmount)) {
                    $scope.ErrorStandardSpendingAmount = errorSpendingIsNumber;
                    return
                }
                if (list != null && isNaN(list)) {
                    $scope.ErrorStandardSpendingAmount = errorSpendingIsNumber;
                    return
                }
                if ($scope.Data.PolicyID == "" || $scope.Data.PolicyID == null) {
                    $scope.ErrorPolicy = errorSelectPolicy;
                    return
                }

                if ($scope.Data.MinSpending != "" && $scope.Data.MinSpending != null) {
                    var position = $scope.Data.MinSpending.indexOf(',');
                    if (position != -1) {
                        var x = $scope.Data.MinSpending.split(",");
                        var list = x.join("");
                    }
                    else if (isNaN($scope.Data.MinSpending)) {
                        $scope.ErrorMinSpending = errorSpendingIsNumber;
                        return
                    }
                    if (list != null && isNaN(list)) {
                        $scope.ErrorMinSpending = errorSpendingIsNumber;
                        return
                    }                   
                }
                if ($scope.Data.MinPerson != null) {
                    if ($scope.Data.MinPerson != "") {

                        var position = $scope.Data.MinPerson.toString().indexOf(',');
                        if (position != -1) {
                            $scope.ErrorMinPerson = errorInteger;
                            return

                        }
                        var position = $scope.Data.MinPerson.toString().indexOf('.');
                        if (position != -1) {
                            $scope.ErrorMinPerson = errorInteger;
                            return

                        }
                        if (isNaN($scope.Data.MinPerson)) {
                            $scope.ErrorMinPerson = errorEmployeesIsNumber;
                            return
                        }
                        else {
                            $scope.ErrorMinPerson = "";
                        }
                    }
                    else {
                        $scope.ErrorMinPerson = "";
                    }
                }

                if ($scope.Data.Status == "" || $scope.Data.Status == null) {
                    $scope.ErrorStatus = errorStatus;
                    return
                }
                if ($scope.Data.StartDate == null || $scope.Data.StartDate == "") {
                    $scope.ErrorStartDate = errorDateStart;
                    return;
                }
                if ($scope.Data.EndDate != null) {
                    if ($scope.Data.StartDate != null) {
                        var date = $scope.Data.StartDate.split("/");
                        var date1 = date[2] + "/" + date[1] + "/" + date[0];
                        var date = $scope.Data.EndDate.split("/");
                        var date2 = date[2] + "/" + date[1] + "/" + date[0];
                        date1 = new Date(date1);
                        date2 = new Date(date2);
                       
                    }

                }
                if (date1!=null && date2 !=null && date1 > date2) {
                    $scope.ErrorStartDate = errorDate;
                    return;
                }
                else {
                    var position = $scope.Data.StandardSpendingAmount.indexOf(',');
                    if (position != -1) {
                        $scope.Data.StandardSpendingAmount = $scope.Data.StandardSpendingAmount.split(",").join("");
                    }

                    if ($scope.Data.MinSpending != null && $scope.Data.MinSpending != "") {
                        var position = $scope.Data.MinSpending.indexOf(',');
                        if (position != -1) {
                            $scope.Data.MinSpending = $scope.Data.MinSpending.split(",").join("");
                        }
                    }

                    
                    if ($scope.Data.StartDate != null) {
                        var date = $scope.Data.StartDate.split("/");
                        $scope.Data.StartDate = date[2] + "/" + date[1] + "/" + date[0];
                    }
                    if ($scope.Data.EndDate != null) {
                        var date = $scope.Data.EndDate.split("/");
                        $scope.Data.EndDate = date[2] + "/" + date[1] + "/" + date[0];
                    }
                    var SaveAction = myService.UpdateData("/StandardSpending/StandardSpending_Save", $scope.Data);
                    SaveAction.then(function (res) {
                        if (res.data.result.IsSuccess == true) {
                            if ($scope.Data.Id != 0 && $scope.Data.Id != null) {
                                AppendToToastr(true, notification, successfulUpdate, 500, 5000);
                            }
                            else {
                                AppendToToastr(true, notification, successfulAdd, 500, 5000);
                            }
                            $scope.StandardSpendingData.reload();
                            $scope.Data = {};
                            $scope.CloseForm();
                        }
                        else {
                            AppendToToastr(false, notification, updateFailed, 500, 5000);
                            $scope.StandardSpendingData.reload();
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
                $scope.ErrorStaffLevel = "";
                $scope.ErrorStandardSpendingAmount = "";
                $scope.ErrorPolicy = "";
                $scope.ErrorMinSpending = "";
                $scope.ErrorMinPerson = "";
                $scope.ErrorStatus = "";
                $scope.ErrorStartDate = "";
                $scope.ErrorEndDate = "";
                $scope.ErrorCreateDate = "";
                $scope.ErrorModifiedDate = "";
                $scope.Data.Id = contentItem.Id;
                $scope.Data.StaffLevelId = contentItem.StaffLevelId;
                $scope.Data.StandardSpendingAmount = contentItem.StandardSpendingAmount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                $scope.Data.PolicyID = contentItem.PolicyID;
                if (contentItem.MinSpending != null) {
                    $scope.Data.MinSpending = contentItem.MinSpending.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                }
                if (contentItem.MinPerson != null) {
                    $scope.Data.MinPerson = contentItem.MinPerson;
                }
                if (contentItem.Status != null) {
                    $scope.Data.Status = contentItem.Status.toString();
                }
                if (contentItem.StartDate != null) {
                    $scope.Data.StartDate = FormatDate(contentItem.StartDate);
                }
                if (contentItem.EndDate != null) {
                    $scope.Data.EndDate = FormatDate(contentItem.EndDate);
                }
                $(".StartDate").datepicker({
                    autoclose: true,
                    format: "dd/mm/yyyy",
                }).datepicker("setDate", $scope.Data.StartDate);
                $(".EndDate").datepicker({
                    autoclose: true,
                    format: "dd/mm/yyyy",
                }).datepicker("setDate", $scope.Data.EndDate);
                ShowPopup($,
                   "#SaveStandardSpending",
                   $scope.tableInfo.PopupWidth,
                   $scope.tableInfo.PopupHeight);
            }


           

            //load lại trang khi click bỏ qua
            $scope.CloseForm = function () {
                $scope.ErrorStaffLevel = "";
                $scope.ErrorStandardSpendingAmount = "";
                $scope.ErrorPolicy = "";
                $scope.ErrorMinSpending = "";
                $scope.ErrorMinPerson = "";
                $scope.ErrorStatus = "";
                $scope.ErrorStartDate = "";
                $scope.ErrorEndDate = "";
                $scope.ErrorCreateDate = "";
                $scope.ErrorModifiedDate = "";
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
                var ID = obj.Id;
                var action = myService.deleteAction(ID, $scope.tableInfo.id, $scope.tableInfo.DeleteUrl);
                action.then(function (res) {
                    if (res.data.result.IsSuccess == true) {
                        AppendToToastr(res.data.result.IsSuccess, notification, deleteSuccess, 500, 5000);
                        $scope.StandardSpendingData.reload();
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
