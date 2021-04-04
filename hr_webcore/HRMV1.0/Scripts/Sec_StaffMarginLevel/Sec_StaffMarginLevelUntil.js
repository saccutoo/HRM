﻿function BuildTable(appName, controllerName, tableUrl) {
    app.controller(controllerName,
        function ($scope, myService, $filter, $rootScope, $compile) {
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

            //Global theo parentID
            $scope.GlobalListWhereParentID = {
                Status: 60,
                Product: 3320
            }

            $scope.editData = {};
            $scope.Mesenger = "";

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
            $scope.ToDay = dd + "/" + mm + "/" + yyyy;
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
            //Thay đổi ChangeStaffLevelID
            $scope.ChangeStaffLevelID = function () {
                if ($scope.editData.StaffLevelID != null) {
                    $scope.Mesenger = "";
                }
            }
            //Thay đổi ChangeStatus
            $scope.ChangeStatus = function () {
                if ($scope.editData.Status != null) {
                    $scope.Mesenger = "";
                }
            }
            //Thay đổi ChangeMinMargin
            $scope.ChangeMinMargin = function () {
                if ($scope.editData.MinMarginMoney != null) {
                    $scope.Mesenger = "";
                    $scope.editData.MinMarginMoney = $scope.editData.MinMarginMoney.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                    $scope.editData.MinMarginMoney = $scope.editData.MinMarginMoney.split(",");
                    $scope.editData.MinMarginMoney = $scope.editData.MinMarginMoney.join("");
                    $scope.editData.MinMarginMoney = $scope.editData.MinMarginMoney.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                }
            }
            //Thay đổi changeMaxMargin
            $scope.changeMaxMargin = function () {
                if ($scope.editData.MaxMarginMoney != null) {
                    $scope.editData.MaxMarginMoney = $scope.editData.MaxMarginMoney.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                    $scope.editData.MaxMarginMoney = $scope.editData.MaxMarginMoney.split(",");
                    $scope.editData.MaxMarginMoney = $scope.editData.MaxMarginMoney.join("");
                    $scope.editData.MaxMarginMoney = $scope.editData.MaxMarginMoney.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                }
            }
            // -----------------Edit Add Click Employee-----------      
            $scope.addClick = function () {
                $scope.Mesenger = "";
                $scope.editData = {};
                ListStatus();
                GetFullName();

                $scope.editData.CreatedOn1 = $scope.ToDay;
                $scope.editData.StartDate1 = $scope.ToDay;
                $scope.editData.AutoID = 0;
                $(".CreatedOn1").datepicker({
                    format: "dd/mm/yyyy",
                    autoclose: true,
                }).datepicker("setDate", $scope.editData.CreatedOn1);
                $(".StartDate1").datepicker({
                    format: "dd/mm/yyyy",
                    autoclose: true,
                }).datepicker("setDate", $scope.editData.StartDate1);
                $(".EndDate1").datepicker({
                    format: "dd/mm/yyyy",
                    autoclose: true,
                }).datepicker("setDate", $scope.editData.EndDate1);
                ShowPopup($,
                    "#SaveWorkingDayMachineSatList",
                    $scope.tableInfo.PopupWidth,
                    $scope.tableInfo.PopupHeight);
            }
            //Lấy tên người đăng nhập
            function GetFullName() {
                var data = {
                    url: "/GlobalList/GetFullName"
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.editData.Fullname = res.data.result.FullName;
                    $scope.editData.CreatedBy = res.data.result.StaffID;
                }, function (res) {
                    $scope.msg = "Error";
                })
            }

            // -----------------Save--------------
            $scope.Save = function (form) {
                var temp = 0;
                if ($scope.editData.StaffLevelID == null) {
                    $scope.Mesenger = errorStaffLevel;
                    temp = 1;
                    return;
                }
                if ($scope.editData.Status == null) {
                    $scope.Mesenger = ErrorStatus;
                    temp = 1;
                    return;
                }
                if ($scope.editData.MinMarginMoney == null || $scope.editData.MinMarginMoney == '') {
                    $scope.Mesenger = errorMinMargin;
                    temp = 1;
                    return;
                }
                if (form.$valid) {
                    $scope.editData.MinMargin = Number($scope.editData.MinMarginMoney.replace(/[^0-9.-]+/g, ""));
                    if ($scope.editData.MaxMarginMoney != null && $scope.editData.MaxMarginMoney != "") {
                        $scope.editData.MaxMargin = Number($scope.editData.MaxMarginMoney.replace(/[^0-9.-]+/g, ""));
                        if ($scope.editData.MinMargin >= $scope.editData.MaxMargin) {
                            $scope.Mesenger = errorCompareMargin;
                            temp = 1;
                            return;
                        }
                    }
                    if (temp == 0) {
                        $scope.Mesenger = "";
                        var seconds = 0;
                        var date = $scope.editData.CreatedOn1.split("/");
                        $scope.editData.CreatedOn = date[2] + "/" + date[1] + "/" + date[0] + " 00:00:00.000";
                        var date1 = $scope.editData.StartDate1.split("/");
                        $scope.editData.StartDate = date1[2] + "/" + date1[1] + "/" + date1[0] + " 00:00:00.000";
                        if ($scope.editData.EndDate1 != null) {
                            var date2 = $scope.editData.EndDate1.split("/");
                            $scope.editData.EndDate = date2[2] + "/" + date2[1] + "/" + date2[0] + " 00:00:00.000";
                            var startDate = new Date($scope.editData.StartDate);
                            var endDate = new Date($scope.editData.EndDate);
                            seconds = (endDate.getTime() - startDate.getTime()) / 1000;
                        }
                        if (seconds < 0) {
                            $scope.Mesenger = errorDate;
                        }
                        else {
                            var SaveAction = myService.UpdateData("/Sec_StaffMarginLevel/_SaveSec_StaffMarginLevel", $scope.editData);
                            SaveAction.then(function (res) {
                                if (res.data.result.IsSuccess == true) {
                                    if ($scope.editData.AutoID != 0) {
                                        AppendToToastr(true, notification, successfulUpdate, 500, 5000);
                                    }
                                    else {
                                        AppendToToastr(true, notification, successfulAdd, 500, 5000);

                                    }
                                    $scope.Sec_StaffMarginLevelData.reload();
                                    $scope.editData = {};
                                    $.colorbox.close();
                                }
                                else {
                                    AppendToToastr(false, notification, updateFailed, 500, 5000);
                                    $scope.GetListData();
                                    $.colorbox.close();

                                }
                            }, function (res) {
                                AppendToToastr(false, notification, errorNotiFalse);
                            });
                        }
                    }
                }
            }

            //-----------------Edit--------------
            $scope.editClick = function (contentItem) {
                $scope.Mesenger = "";
                $scope.editData = {};
                ListStatus();
                var data = {
                    url: "/Sec_StaffMarginLevel/GetSec_StaffMarginLevelByAutoID?id=" + contentItem.AutoID
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.editData.AutoID = contentItem.AutoID;
                    $scope.editData.StaffLevelID = res.data.result.StaffLevelID;
                    $scope.editData.Status = res.data.result.Status.toString();
                    $scope.editData.MinMarginMoney = res.data.result.MinMarginMoney;
                    $scope.editData.MaxMarginMoney = res.data.result.MaxMarginMoney;
                    $scope.editData.CreatedOn1 = FormatDate(res.data.result.CreatedOn);
                    $scope.editData.Fullname = res.data.result.Fullname;
                    $scope.editData.CreatedBy = res.data.result.CreatedBy;
                    $scope.editData.StartDate1 = FormatDate(res.data.result.StartDate);
                    if (res.data.result.EndDate != null) {
                        $scope.editData.EndDate1 = FormatDate(res.data.result.EndDate);
                    }
                    $(".CreatedOn1").datepicker({
                        format: "dd/mm/yyyy",
                        autoclose: true,
                    }).datepicker("setDate", $scope.editData.CreatedOn1);
                    $(".StartDate1").datepicker({
                        format: "dd/mm/yyyy",
                        autoclose: true,
                    }).datepicker("setDate", $scope.editData.StartDate1);
                    $(".EndDate1").datepicker({
                        format: "dd/mm/yyyy",
                        autoclose: true,
                    }).datepicker("setDate", $scope.editData.EndDate1);
                }, function (res) {
                    $scope.msg = "Error";
                })
                ShowPopup($,
                    "#SaveWorkingDayMachineSatList",
                    $scope.tableInfo.PopupWidth,
                    $scope.tableInfo.PopupHeight);
            }
            //load lại trang khi click bỏ qua
            $scope.CloseForm = function () {
                $scope.editData = {};
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
                var action = myService.deleteAction(obj.AutoID, 1, "/Sec_StaffMarginLevel/DeleteSec_StaffMarginLevel");
                action.then(function (res) {
                    if (res.data.result.IsSuccess) {
                        $scope.GetListData();
                        $scope.Sec_StaffMarginLevelData.reload();
                    }
                    AppendToToastr(res.data.result.IsSuccess, notification, res.data.result.Message, 500, 5000);
                },
                    function (res) {
                        AppendToToastr(false, notification, errorNotiFalse);
                    });

            }

            // -----------------Xóa--End------------


            //-------------------Excel--------------
            $scope.ExcelClick = function () {
                var filterString = $scope.getFilterValue();
                window.location = "/Sec_StaffMarginLevel/ExportExcelSec_StaffMarginLevel" + "?pageIndex=" + $scope.pageIndex + "&pageSize=" + $scope.pageSizeSelected + "&filter=" + filterString;
            }
            //-------------------Excel-End----------

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
