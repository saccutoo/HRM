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
            $rootScope.AddList = true;
            $rootScope.isDownLoad = true;
            $scope.Data = {};
            $scope.ListData = [];
            $rootScope.quickFiltercomback = {};
            $scope.Data.StatusInput = '0';
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
                    $scope.ColumnsAddListSalary = [];
                    for (var i = 0; i < emp.data.result.length; i++) {
                        if (emp.data.result[i].ColumnName == 'KPIName' || emp.data.result[i].ColumnName == 'KpiAmount' || emp.data.result[i].ColumnName == 'KpiCode' || emp.data.result[i].ColumnName == 'KpiValue') {
                            $scope.ColumnsAddListSalary.push(emp.data.result[i]);
                        }
                    }
                    $scope.Columns = angular.copy($scope.ColumnsAddListSalary)
                },
                    function (emp) {
                        AppendToToastr(false, notification, errorNotiFalse);
                    });
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
                $scope.ListKPI();
                getQuarter();
                $scope.Data.StatusInput = '0';
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
                $("#year").datepicker({
                    autoclose: true,
                    format: "yyyy",
                    startView: "years",
                    minViewMode: "years",
                    maxViewMode: "years"
                }).datepicker("setDate", new Date());

                var endYear = new Date(new Date().getFullYear(), 11, 31);
                $("#month").datepicker({
                    autoclose: true,
                    format: "mm",
                    endDate: endYear,
                    startView: "months",
                    minViewMode: "months",
                    maxViewMode: "months"
                }).datepicker("setDate", new Date());

                if (angular.equals({}, $rootScope.quickFiltercomback) == false) {
                    if ($rootScope.quickFiltercomback.StaffName != null) {
                        $scope.Data.StaffID = parseInt($rootScope.quickFiltercomback.StaffName.Value);
                        $scope.Data.OrganizationUnitID = $scope.ListEmployees.filter(function (item) {
                            return item.StaffID == $scope.Data.StaffID;
                        })[0].OrganizationUnitID;
                    }
                    if ($rootScope.quickFiltercomback.KPIName != null) {
                        $scope.Data.KpiID = parseInt($rootScope.quickFiltercomback.KPIName.Value);
                        $scope.Data.KpiCode = $scope.getListListKPI.filter(function (item) {
                            return item.GlobalListID == $scope.Data.KpiID;
                        })[0].ValueEN;
                    }
                    if ($rootScope.quickFiltercomback.StaffName == null && $rootScope.quickFiltercomback.OrganizationUnitName) {
                        $scope.Data.OrganizationUnitID = parseInt($rootScope.quickFiltercomback.OrganizationUnitName.Value);
                    }
                }

                ShowPopup($,
                    "#SaveSalaryKPI",
                    $scope.tableInfo.PopupWidth,
                    $scope.tableInfo.PopupHeight);
            }

            $scope.addListClick = function () {
                $scope.getColumns();
                $scope.ListKPI();
                getQuarter();
                $scope.Data.StatusInput = '0';
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
                $("#yearAddList").datepicker({
                    autoclose: true,
                    format: "yyyy",
                    startView: "years",
                    minViewMode: "years",
                    maxViewMode: "years"
                }).datepicker("setDate", new Date());

                var endYear = new Date(new Date().getFullYear(), 11, 31);
                $("#monthAddList").datepicker({
                    autoclose: true,
                    format: "mm",
                    endDate: endYear,
                    startView: "months",
                    minViewMode: "months",
                    maxViewMode: "months"
                }).datepicker("setDate", new Date());
                if (angular.equals({}, $rootScope.quickFiltercomback) == false) {
                    if ($rootScope.quickFiltercomback.StaffName != null) {
                        $scope.Data.StaffID = parseInt($rootScope.quickFiltercomback.StaffName.Value);
                        $scope.Data.OrganizationUnitID = $scope.ListEmployees.filter(function (item) {
                            return item.StaffID == $scope.Data.StaffID;
                        })[0].OrganizationUnitID;
                    }
                    if ($rootScope.quickFiltercomback.StaffName == null && $rootScope.quickFiltercomback.OrganizationUnitName) {
                        $scope.Data.OrganizationUnitID = parseInt($rootScope.quickFiltercomback.OrganizationUnitName.Value);
                    }
                }
                ShowPopup($,
                    "#SaveListSalaryKPI",
                    $scope.tableInfo.PopupWidth,
                    '80%');
            }
            //-----------------Lấy danh sách nhân viên------------------
            $scope.ListEmployees = function () {
                var data = {
                    url: "/StaffPlan/Staff_GetALL"
                }
                var list = myService.getData(data);
                list.then(function (res) {
                    $scope.ListEmployees = res.data.result;
                }, function (res) {
                    $scope.msg = "Error";
                })

            }
            $scope.ListEmployees();
            //-----------------Lấy danh sách phòng ban------------------
            $scope.ListOrganizationUnit = function () {
                var data = {
                    url: "/OrganizationUnitPlan/OrganizationUnit_GetALL"
                }
                var list = myService.getData(data);
                list.then(function (res) {
                    $scope.getListAllOrganizationUnit = res.data.result;

                }, function (res) {
                    $scope.msg = "Error";
                })
            }
            $scope.ListOrganizationUnit();

            $scope.ListKPI = function () {
                var data = {
                    url: "/Common/GetDataByGloballistnotTree?parentid=" + 3494
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getListListKPI = res.data.result;
                    for (var i = 0; i < $scope.getListListKPI.length; i++) {
                        $scope.getListListKPI[i].btnKpiValue = true;
                        $scope.getListListKPI[i].btnKpiAmount = true;
                    }
                },
                    function (res) {
                        $scope.msg = "Error";
                    });
            }

            $scope.ListReward = function () {
                var data = {
                    url: "/Common/GetDataByGloballistnotTree?parentid=" + 3541
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.ListReward = res.data.result;
                },
                    function (res) {
                        $scope.msg = "Error";
                    });
            }
            $scope.ListReward();

            $scope.ListStatusInput = function () {
                var data = {
                    url: "/Common/GetDataByGloballistnotTree?parentid=" + 3600
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.ListStatusInput = res.data.result;
                },
                    function (res) {
                        $scope.msg = "Error";
                    });
            }
            $scope.ListStatusInput();

            $scope.ListStatusOfUse = function () {
                var data = {
                    url: "/Common/GetDataByGloballistnotTree?parentid=" + 3606
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.ListStatusOfUse = res.data.result;
                },
                    function (res) {
                        $scope.msg = "Error";
                    });
            }
            $scope.ListStatusOfUse();

            $scope.ChangeStaff = function () {
                if ($scope.Data.StaffID == null || $scope.Data.StaffID == '') {
                    $scope.Data.OrganizationUnitID = '';
                    return
                }
                else {
                    $scope.Data.OrganizationUnitID = $scope.ListEmployees.filter(function (item) {
                        return item.StaffID == $scope.Data.StaffID;
                    })[0].OrganizationUnitID;
                }

            }

            $scope.changeKpi = function () {
                if ($scope.Data.KpiID == null || $scope.Data.KpiID == '') {
                    $scope.Data.KpiCode = '';
                    return
                }
                else {
                    $scope.Data.KpiCode = $scope.getListListKPI.filter(function (item) {
                        return item.GlobalListID == $scope.Data.KpiID;
                    })[0].ValueEN;
                }

            }
            // -----------------ADD hoặc edit chính sách--------------

            $scope.SaveAction = function (form) {
                if (form.$valid) {
                    $scope.ListData = [];
                    if ($scope.Data.Quarter != null && $scope.Data.Quarter!='') {
                        $scope.Data.Quarter = parseInt($scope.Data.Quarter);
                    }
                    if ($scope.Data.Month != null && $scope.Data.Month != '') {
                        $scope.Data.Month = parseInt($scope.Data.Month);
                    }
                    $scope.ListData.push($scope.Data);
                    var SaveAction = $http({
                        method: "POST",
                        url: "/SalaryKPI/SalaryKPI_Save",
                        data: {
                            data: $scope.ListData,
                        },
                        dataType: "json"
                    });
                    SaveAction.then(function (res) {
                        form.$dirty = false;
                        form.$invalid = false;
                        form.$submitted = false;
                        form.$valid = false;
                        if (res.data.result.IsSuccess == true) {
                            AppendToToastr(true, notification, success, 500, 5000);
                            $scope.SalaryKPIData.reload();
                            $scope.Data = {};
                            $.colorbox.close(form);
                        }
                        else {
                            AppendToToastr(false, notification, updateFailed, 500, 5000);
                            $scope.SalaryKPIData.reload();
                            $.colorbox.close(form);

                        }
                    }, function (res) {
                        AppendToToastr(false, notification, errorNotiFalse);
                    });
                }
                else {
                    AppendToToastr(false, notification, errorFullInformation);
                }
            }

            //-----------------Edit Chính sách--------------
            $scope.editClick = function (contentItem) {
                $scope.ListKPI();
                $scope.Data = {};
                $scope.Data = angular.copy(contentItem)
                //if ($scope.Data.Month != null && $scope.Data.Month < 10) {
                //    $scope.Data.Month = "0" + $scope.Data.Month;
                //}
                if ($scope.Data.StatusInput != null) {
                    $scope.Data.StatusInput = $scope.Data.StatusInput.toString();
                }
                if ($scope.Data.Quarter!=null && $scope.Data.Quarter!=0) {
                    $scope.Data.Quarter = $scope.Data.Quarter.toString();
                }
                if ($scope.Data.Month != null && $scope.Data.Month !=0) {
                    $scope.Data.Month = $scope.Data.Month;
                }
                if ($scope.Data.StatusOfUse!=null) {
                    $scope.Data.StatusOfUse = $scope.Data.StatusOfUse.toString();
                }
                else {
                    $scope.Data.StatusOfUse = null;
                }
                if ($scope.Data.StaffID==0 || $scope.Data.StaffID==null) {
                    $scope.Data.StaffID = null;
                }
                if ($scope.Data.OrganizationUnitID == 0 || $scope.Data.OrganizationUnitID == null) {
                    $scope.Data.OrganizationUnitID = null;
                }

                setTimeout(function () {
                    $("#month").datepicker({
                        autoclose: true,
                        format: "mm",
                        startView: "months",
                        minViewMode: "months",
                        maxViewMode: "months"
                    }).datepicker($scope.Data.Month);
                }, 500);
                setTimeout(function () {
                    $("#year").datepicker({
                        autoclose: true,
                        format: "yyyy",
                        startView: "years",
                        minViewMode: "years",
                        maxViewMode: "years"
                    }).datepicker($scope.Data.Year);
                }, 500);

                ShowPopup($,
                   "#SaveSalaryKPI",
                   $scope.tableInfo.PopupWidth,
                   $scope.tableInfo.PopupHeight);
            }

            $scope.SaveListAction = function (form) {
                $scope.ListData = [];

                if (form.$valid) {
                    if ($scope.Data.Quarter != null && $scope.Data.Quarter != '') {
                        $scope.Data.Quarter = parseInt($scope.Data.Quarter);
                    }
                    for (var i = 0; i < $scope.getListListKPI.length; i++) {
                        $scope.obj = {
                            StaffID: $scope.Data.StaffID,
                            OrganizationUnitID: $scope.Data.OrganizationUnitID,
                            Year: $scope.Data.Year,
                            Month: $scope.Data.Month == null || $scope.Data.Month == '' ? 0 : $scope.Data.Month,
                            KpiID: $scope.getListListKPI[i].GlobalListID,
                            KpiCode: $scope.getListListKPI[i].ValueEN,
                            KpiValue: $scope.getListListKPI[i].KpiValue,
                            KpiAmount: $scope.getListListKPI[i].KpiAmount,
                            Description: $scope.Data.Description,
                            Quarter: $scope.Data.Quarter == null || $scope.Data.Quarter == '' ? 0 : $scope.Data.Quarter,
                            StatusInput: $scope.Data.StatusInput,
                            PolicyBonusID: $scope.Data.PolicyBonusID,
                            StatusOfUse: $scope.Data.StatusOfUse
                        }
                        $scope.ListData.push($scope.obj);
                    }
                    var SaveAction = $http({
                        method: "POST",
                        url: "/SalaryKPI/SalaryKPI_Save",
                        data: {
                            data: $scope.ListData,
                        },
                        dataType: "json"
                    });
                    SaveAction.then(function (res) {
                        form.$dirty = false;
                        form.$invalid = false;
                        form.$submitted = false;
                        form.$valid = false;
                        if (res.data.result.IsSuccess == true) {
                            if ($scope.Data.AutoID != 0 && $scope.Data.AutoID != null) {
                                AppendToToastr(true, notification, successfulUpdate, 500, 5000);
                            }
                            else {
                                AppendToToastr(true, notification, successfulAdd, 500, 5000);

                            }
                            $scope.SalaryKPIData.reload();
                            $scope.Data = {};
                            $scope.CloseFormListSalaryKPI(form);
                        }
                        else {
                            AppendToToastr(false, notification, updateFailed, 500, 5000);
                            $scope.SalaryKPIData.reload();
                            $scope.CloseFormListSalaryKPI(form);
                        }
                    }, function (res) {
                        AppendToToastr(false, notification, errorNotiFalse);
                    });
                }
                else {
                    AppendToToastr(false, notification, errorFullInformation);
                }
            }
            //load lại trang khi click bỏ qua
            $scope.CloseForm = function (form) {
                form.$dirty = false;
                form.$invalid = false;
                form.$submitted = false;
                form.$valid = false;

                $scope.Data = {};
                $.colorbox.close();
            }

            $scope.CloseFormListSalaryKPI = function (form) {
                form.$dirty = false;
                form.$invalid = false;
                form.$submitted = false;
                form.$valid = false;
                $scope.Data = {};
                $scope.getListListKPI = [];
                $.colorbox.close();
            }

            // -----------------End Edit Add Click--------------

            //------------------Tạo table sửa inline------------
            $scope.clickRowListKpiAmount = function (list) {
                for (var i = 0; i < $scope.getListListKPI.length; i++) {
                    $scope.getListListKPI[i].btnKpiValue = true;
                    $scope.getListListKPI[i].btnKpiAmount = true;
                }
                list.btnKpiAmount = false;
            }
            $scope.outFocus = function () {
                for (var i = 0; i < $scope.getListListKPI.length; i++) {
                    $scope.getListListKPI[i].btnKpiValue = true;
                    $scope.getListListKPI[i].btnKpiAmount = true;
                }

            }
            $scope.clickRowListKpiValue = function (list) {
                for (var i = 0; i < $scope.getListListKPI.length; i++) {
                    $scope.getListListKPI[i].btnKpiValue = true;
                    $scope.getListListKPI[i].btnKpiAmount = true;
                }
                list.btnKpiValue = false;
            }
            //------------------Tạo table sửa inline - end------------
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
                        $scope.SalaryKPIData.reload();
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

            $scope.upload = function () {
                var $file = document.getElementById('FileUploadInput'),
                    $formData = new FormData();

                if ($file.files.length > 0) {
                    for (var i = 0; i < $file.files.length; i++) {
                        $formData.append('file-' + i, $file.files[i]);
                    }
                }
                var dt = Loading();
                $.ajax({
                    type: 'POST',
                    url: "/SalaryKPI/ImportExcel",
                    data: $formData,
                    processData: false,
                    contentType: false,
                    beforeSend: function () {
                        //ShowLoadingPage();
                    },
                    success: function (data) {
                        var data1 = JSON.parse(data);
                        if (data1.SM.IsSuccess==false) {
                            AppendToToastr(false, notification, data1.SM.Message, 500, 5000);
                        }
                        else {
                            if (data1 != null && data1.result.length > 0) {
                                window.location = "/SalaryKPI/ExportSalaryKPIDataError";
                            }
                            else {
                                AppendToToastr(true, notification, data1.SM.Message, 500, 5000);
                            }
                        }
                        $scope.SalaryKPIData.reload();
                        $("#exampleModal").modal("hide");
                        $('#val').text('');
                        $('#FileUploadInput').val('');
                        dt.finish();
                    },
                    error: function () {
                        AppendToToastr(false, notification, errorNotiFalse);
                        dt.finish();
                    }
                });
            }

            $scope.downLoadFileClick = function () {
                window.location = "/SalaryKPI/DownLoadTemplate";
            }

            //----------------------------------------------------------------------------------------------------------------------------------------------------------------------          
           
            function getQuarter() {
                var d =  new Date();
                var m = Math.floor(d.getMonth() / 3) + 2;
                $scope.Data.Quarter=(m > 4 ? m - 4 : m).toString();
            }
            getQuarter()
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

    app.directive('autoFocus', function ($timeout) {
        return {
            link: function (scope, element, attrs) {
                attrs.$observe("autoFocus", function (newValue) {
                    if (newValue === "false")
                        $timeout(function () { element.focus() });
                });
            }
        };
    });


    app.directive('price', function () {
        return {
            require: 'ngModel',
            link: function (scope, element, attrs, ngModel) {
                attrs.$set('ngTrim', "false");
                var formatter = function (str, isNum) {
                    str = String(Number(str || 0) / (isNum ? 1 : 100));
                    str = (str == '0' ? '0.0' : str).split('.');
                    str[1] = str[1] || '0';
                    return str[0].replace(/(\d)(?=(\d\d\d)+(?!\d))/g, '$1,') + '.' + (str[1].length == 1 ? str[1] + '0' : str[1]);
                }
                var updateView = function (val) {
                    scope.$applyAsync(function () {
                        ngModel.$setViewValue(val || '');
                        ngModel.$render();
                    });
                }
                var parseNumber = function (val) {
                    var modelString = formatter(ngModel.$modelValue, true);
                    var sign = {
                        pos: /[+]/.test(val),
                        neg: /[-]/.test(val)
                    }
                    sign.has = sign.pos || sign.neg;
                    sign.both = sign.pos && sign.neg;

                    if (!val || sign.has && val.length == 1 || ngModel.$modelValue && Number(val) === 0) {
                        var newVal = (!val || ngModel.$modelValue && Number() === 0 ? '' : val);
                        if (ngModel.$modelValue !== newVal)
                            updateView(newVal);

                        return '';
                    }
                    else {
                        var valString = String(val || '');
                        var newSign = (sign.both && ngModel.$modelValue >= 0 || !sign.both && sign.neg ? '-' : '');
                        var newVal = valString.replace(/[^0-9]/g, '');
                        var viewVal = newSign + formatter(angular.copy(newVal));

                        if (modelString !== valString)
                            updateView(viewVal);

                        return (Number(newSign + newVal) / 100) || 0;
                    }
                }
                var formatNumber = function (val) {
                    if (val) {
                        var str = String(val).split('.');
                        str[1] = str[1] || '0';
                        val = str[0] + '.' + (str[1].length == 1 ? str[1] + '0' : str[1]);
                    }
                    return parseNumber(val);
                }

                ngModel.$parsers.push(parseNumber);
                ngModel.$formatters.push(formatNumber);
            }
        };
    });

    app.directive('currencyMaskFourSpace', function () {
        return {
            restrict: 'A',
            require: 'ngModel',
            link: function (scope, element, attrs, ngModelController) {

                var formatNumber = function (value) {

                    value = value.toString();
                    value = value.replace(/[^-?0-9\.]/g, "");
                    var parts = value.split('.');
                    parts[0] = parts[0].replace(/\d{1,3}(?=(\d{3})+(?!\d))/g, "$&,");
                    if (parts[1] && parts[1].length > 2) {
                        parts[1] = parts[1].substring(0, 4);
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
                    value = value.replace(/[^-?0-9\.]/g, "");
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
}
