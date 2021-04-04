﻿function BuildTable(appName, controllerName, tableUrl) {
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
                $scope.ErrorName = "";
                $scope.ErrorNameEN = "";
                $scope.ErrorStatus = "";
                $scope.ErrorCountryID = "";

                ShowPopup($,
                    "#SaveProvince",
                    $scope.tableInfo.PopupWidth,
                    $scope.tableInfo.PopupHeight);
            }

            $scope.CallData = function () {
                $scope.ListStatus();           //Trạng thái
                $scope.ListCountry();          //Quốc gia

            }

            //------------Dropdown trạng thái-------------
            $scope.ListStatus = function () {
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

            //------------Dropdown quốc gia-------------
            $scope.ListCountry = function () {
                var data = {
                    url: "/Common/GetDataByGloballistnotTree?parentid=" + 44,
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.ListCountry = res.data.result;
                },
                    function (res) {
                        $scope.msg = "Error";
                    });
            }

            //bắt lỗi khi nhập tên tỉnh thành phố
            $scope.ChangeName = function () {
                if ($scope.Data.Name == null || $scope.Data.Name=="") {
                    $scope.ErrorName = errorNameProvince;
                    return
                }
                else {
                    $scope.ErrorName = "";
                }
            }

            //bắt lỗi khi nhập tên tỉnh thành phố EN
            $scope.ChangeNameEN = function () {
                if ($scope.Data.NameEN == null || $scope.Data.NameEN == "") {
                    $scope.ErrorNameEN = errorNameENProvince;
                    return
                }
                else {
                    $scope.ErrorNameEN = "";
                }
            }

            //bắt lỗi khi chọn quốc gia
            $scope.ChangeCountry = function () {
                if ($scope.Data.CountryID == null) {
                    $scope.ErrorCountryID = errorSelectCountry;
                    return
                }
                else {
                    $scope.ErrorCountryID = "";
                }
            }

            //bắt lỗi khi chọn trạng thái
            $scope.ChangeStatus = function () {
                if ($scope.Data.Status == null) {
                    $scope.ErrorStatus = errorStatus;
                    return
                }
                else {
                    $scope.ErrorStatus = "";
                }
            }
          

            // -----------------ADD hoặc edit chính sách--------------
            $scope.Save = function () {
               
                if ($scope.Data.Name == null || $scope.Data.Name == "") {
                    $scope.ErrorName = errorNameProvince;
                    return
                }
                else if ($scope.Data.NameEN == null || $scope.Data.NameEN == "") {
                    $scope.ErrorNameEN = errorNameENProvince;
                    return
                }
                else if ($scope.Data.CountryID == null) {
                    $scope.ErrorCountryID = errorSelectCountry;
                    return
                }
                else if ($scope.Data.Status == null) {
                    $scope.ErrorStatus = errorStatus;
                    return
                }
                else {
                    var SaveAction = myService.UpdateData("/Province/Province_Save", $scope.Data);
                    SaveAction.then(function (res) {
                        if (res.data.result.IsSuccess == true) {
                            if ($scope.Data.ProvinceID != 0 && $scope.Data.ProvinceID != null) {
                                AppendToToastr(true, notification, successfulUpdate, 500, 5000);
                            }
                            else {
                                AppendToToastr(true, notification, successfulAdd, 500, 5000);

                            }
                            $scope.ProvinceData.reload();
                            $scope.Data = {};
                            $scope.CloseForm();
                        }
                        else {
                            AppendToToastr(false, notification, updateFailed, 500, 5000);
                            $scope.ProvinceData.reload();
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
                $scope.ErrorName = "";
                $scope.ErrorNameEN = "";
                $scope.ErrorStatus = "";
                $scope.ErrorCountryID = "";
                $scope.Data.ProvinceID = contentItem.ProvinceID;
                $scope.Data.Name = contentItem.Name;
                $scope.Data.NameEN = contentItem.NameEN;
                $scope.Data.CountryID = contentItem.CountryID;
                $scope.Data.Status = contentItem.Status.toString();
                ShowPopup($,
                   "#SaveProvince",
                   $scope.tableInfo.PopupWidth,
                   $scope.tableInfo.PopupHeight);
            }

            //load lại trang khi click bỏ qua
            $scope.CloseForm = function () {
                $scope.ErrorName = "";
                $scope.ErrorNameEN = "";
                $scope.ErrorStatus = "";
                $scope.ErrorCountryID = "";
                $scope.Data = {};
                $.colorbox.close();
            }


            // -----------------Xóa------------

            $scope.deleteClick = function (obj) {
                BoostrapDialogConfirm(notification,
                    notificationDelete,
                    BootstrapDialog.TYPE_WARNING,
                    $scope.deleteActionClick,
                    obj);
            }

            $scope.deleteActionClick = function (obj) {
                var ID = obj.ProvinceID;
                var action = myService.deleteAction(ID, $scope.tableInfo.id, $scope.tableInfo.DeleteUrl);
                action.then(function (res) {
                    if (res.data.result.IsSuccess == true) {
                        AppendToToastr(res.data.result.IsSuccess, notification, deleteSuccess, 500, 5000);
                        $scope.ProvinceData.reload();
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
