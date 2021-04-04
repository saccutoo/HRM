function BuildTable(appName, controllerName, tableUrl) {
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

            $scope.Data = {};
            $scope.Mesenger = "";


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
                if (type === 2) {
                    formatNumber(value)
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


            $scope.CallData = function () {
                ListOrganizationUnit(); //phòng ban
                ListStatus();           //trạng thái
                ListProduct();   //sản phẩm
                ListStaffWhereRoleBD(); //BD
                ListStaff();
            }

            // -----------------Edit Add Click Employee-----------           

            //Show poup modal khi click vào nút thêm
            $scope.addClick = function () {
                $scope.Mesenger = "";
                $scope.Data = {};
                ShowPopup($,
                    "#SaveConfigAllowance",
                    $scope.tableInfo.PopupWidth,
                    $scope.tableInfo.PopupHeight);
            }

            //$scope.Config_Approval_GetList = function () {
            //    var data = {
            //        url: "/ConfigAllowance/Config_Approval_GetList/"
            //    }
            //    var list = myService.getDropdown(data);
            //    list.then(function (res) {
            //        $scope.ConfigApprovalList = res.data.result;
            //        console.log($scope.ConfigApprovalList)

            //    }, function (res) {
            //        $scope.msg = "Error";
            //    });
            //}

            //change name
            $scope.ChangeName = function () {
                if ($scope.Data.Name == null || $scope.Data.Name == "") {
                    $scope.ErrorName = errorAllowanceName;
                }
                else {
                    $scope.ErrorName = "";                   
                }
            }

            //Format tiền khi nhập vào input
            $scope.ChangeFromAmount = function () {
                if ($scope.Data.FromAmount == "" || $scope.Data.FromAmount == null) {
                    $scope.ErrorFromAmount = errorMoneyFrom;
                    return
                }
                var position = $scope.Data.FromAmount.indexOf(',');
                if (position != -1) {
                    var x = $scope.Data.FromAmount.split(",");
                    var list = x.join("");

                }
                else if (isNaN($scope.Data.FromAmount)) {
                    $scope.ErrorFromAmount = errorMoneyIsNumber;
                    return
                }
                if (list != null && isNaN(list)) {
                    $scope.ErrorFromAmount = errorMoneyIsNumber;
                    return
                }               
                else {
                    $scope.ErrorFromAmount = "";
                    var x = $scope.Data.FromAmount;
                    var x1 = x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                    var x2 = x1.split(",");
                    var list = x2.join("");
                    $scope.Data.FromAmount = list.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                }
               

            }
            
            //Format tiền khi nhập vào input
            $scope.ChangeToAmount = function () {
                
                if ($scope.Data.ToAmount == null || $scope.Data.ToAmount == "") {
                    $scope.ErrorToAmount = errorMoneyFrom;
                    return
                }
                var position1 = $scope.Data.ToAmount.indexOf(',');
                if (position1 != -1) {
                    var x = $scope.Data.ToAmount.split(",");
                    var list1 = x.join("");

                }
                else if (isNaN($scope.Data.ToAmount)) {
                    $scope.ErrorToAmount = errorMoneyIsNumber;
                    return;
                }
                if (list1 != null && isNaN(list1)) {
                    $scope.ErrorToAmount = errorMoneyIsNumber;
                    return;
                }
                else {
                    $scope.ErrorToAmount = "";
                    var x = $scope.Data.ToAmount;
                    var x1 = x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                    var x2 = x1.split(",");
                    var list = x2.join("");
                    $scope.Data.ToAmount = list.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                }               

            }

            //change sFormular
            $scope.ChangesFormular = function () {
                if ($scope.Data.sFormular == null || $scope.Data.sFormular == "") {
                    $scope.ErrorsFormular = errorAllowanceFormat;
                }
               
                else {
                    $scope.ErrorsFormular = "";
                  
                }
            }

            //
            $scope.ChangeNameEN = function () {
                if ($scope.Data.NameEN == null || $scope.Data.NameEN == "") {
                    $scope.ErrorNameEN = errorAllowanceNameEN;
                }

                else {
                    $scope.ErrorNameEN = "";

                }
            }

            // -----------------insert or update phụ cấp--------------
            $scope.Save = function () {
                $scope.ErrorName = "";
                $scope.ErrorFromAmount = "";
                $scope.ToAmount = "";
                $scope.ErrorsFormular = "";
                if ($scope.Data.Name == null || $scope.Data.Name == "") {
                    $scope.ErrorName = errorAllowanceName;
                    return;
                }
                else if ($scope.Data.NameEN == null || $scope.Data.NameEN == "") {
                    $scope.ErrorNameEN = errorAllowanceNameEN;
                    return
                }
                else if ($scope.Data.FromAmount == null || $scope.Data.FromAmount == "") {
                    $scope.ErrorFromAmount = errorMoneyFrom;
                    return;
                }
                
                var position = $scope.Data.FromAmount.indexOf(',');
                if (position != -1) {
                    var x = $scope.Data.FromAmount.split(",");
                    var list = x.join("");

                }
                else if (isNaN($scope.Data.FromAmount)) {
                    $scope.ErrorFromAmount = errorMoneyIsNumber;
                    return;
                }
                if (list != null && isNaN(list)) {
                    $scope.ErrorFromAmount = errorMoneyIsNumber;
                    return;
                }

                else if ($scope.Data.ToAmount == null || $scope.Data.ToAmount == "") {
                    $scope.ErrorToAmount = errorMoneyTo;
                    return;
                }

                var position1 = $scope.Data.ToAmount.indexOf(',');
                if (position1 != -1) {
                    var x = $scope.Data.ToAmount.split(",");
                    var list1 = x.join("");

                }
                else if (isNaN($scope.Data.ToAmount)) {
                    $scope.ErrorToAmount = errorMoneyIsNumber;
                    return;
                }
                if (list1 != null && isNaN(list1)) {
                    $scope.ErrorToAmountr = errorMoneyIsNumber;
                    return;
                }
                
                if (list != null && list1 != null && parseInt(list) > parseInt(list1)) {
                    $scope.ErrorFromAmount = errorMoneyCompare;
                    return;
                }
                else if(position==-1 && position1==-1 && parseInt($scope.Data.FromAmount) > parseInt($scope.Data.ToAmount)){
                    $scope.ErrorFromAmount = errorMoneyCompare;
                     return;
                }
                else if ((position != -1 && position1 == -1 && parseInt(list) > parseInt($scope.Data.ToAmount))) {
                    $scope.ErrorFromAmount = errorMoneyCompare;
                    return;
                }
                else if ($scope.Data.sFormular == null || $scope.Data.sFormular == "") {
                    $scope.ErrorsFormular = errorAllowanceFormat;
                    return;
                }
                $scope.Data.FromAmount = $scope.Data.FromAmount.split(",").join("");
                $scope.Data.ToAmount = $scope.Data.ToAmount.split(",").join("");
                var SaveAction = myService.UpdateData("/ConfigAllowance/ConfigAllowance_Save", $scope.Data);
                SaveAction.then(function (res) {
                    if (res.data.result.IsSuccess == true) {
                        
                        if ($scope.Data.AllowanceID == 0 && $scope.Data.AllowanceID==undefined) {
                            AppendToToastr(true, notification, successfulAdd, 500, 5000);
                        }
                        else {
                            AppendToToastr(true, notification, successfulUpdate, 500, 5000);

                        }
                        $scope.ConfigAllowanceData.reload();
                        $scope.Data = {};
                        $scope.CloseForm();
                    }
                    else {
                        AppendToToastr(false, notification, updateFailed, 500, 5000);
                        $scope.GetListData();
                        $scope.CloseForm();
                    }
                }, function (res) {
                    AppendToToastr(false, notification, errorNotiFalse);
                });

            }

            //Show poup modal khi click vào nút sửa
            $scope.editClick = function (contentItem) {
                $scope.Data.AllowanceID = contentItem.AllowanceID;
                $scope.Data.Name = contentItem.Name;
                $scope.Data.NameEN = contentItem.NameEN;
                $scope.Data.FromAmount = contentItem.FromAmount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                $scope.Data.ToAmount = contentItem.ToAmount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                $scope.Data.sFormular = contentItem.sFormular;
                $scope.Data.Note = contentItem.Note;
                ShowPopup($,
                   "#SaveConfigAllowance",
                   $scope.tableInfo.PopupWidth,
                   $scope.tableInfo.PopupHeight);
            }

            //đóng popup modal
            $scope.CloseForm = function () {
                $scope.Data = {};
                $scope.ErrorName = "";
                $scope.ErrorNameEN = "";
                $scope.ErrorToAmount = "";
                $scope.ErrorFromAmount = "";
                $scope.ToAmount = "";
                $scope.ErrorsFormular = "";
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
                var ID = obj.AllowanceID;
                var action = myService.deleteAction(ID, $scope.tableInfo.id, $scope.tableInfo.DeleteUrl);
                action.then(function (res) {
                    if (res.data.result.IsSuccess == true) {
                        AppendToToastr(res.data.result.IsSuccess, notification, deleteSuccess, 500, 5000);
                        $scope.GetListData();
                        $scope.ConfigAllowanceData.reload();
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
            //------------Dropdown trạng thái-------------
            function ListStatus() {
                var data = {
                    url: "/Common/GetDataByGloballistnotTree?parentid=" + $scope.GlobalListWhereParentID.Status
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getListStatus = res.data.result;

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
