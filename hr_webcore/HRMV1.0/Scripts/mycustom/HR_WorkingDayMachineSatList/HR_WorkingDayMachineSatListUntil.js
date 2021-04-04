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


            $scope.CallData = function () {
                ListOrganizationUnit(); //phòng ban
                ListStatus();           //trạng thái
                ListProduct();   //sản phẩm
                ListStaffWhereRoleBD(); //BD
                ListStaff();
            }

            // -----------------Edit Add Click Employee-----------           


            $scope.addClick = function () {
                $scope.WorkingDayMachineADD = true;
                $scope.WorkingDayMachineEdit = false;

                $scope.Data = {};
                //var today = new Date();
                //var dd = today.getDate();
                //var mm = today.getMonth() + 1;
                //var yyyy = today.getFullYear();
                //if (dd < 10) {
                //    dd = '0' + dd;
                //}
                //if (mm < 10) {
                //    mm = '0' + mm;
                //}
                //$scope.Data.Date = dd + "/" + mm + "/" + yyyy;
                //var disabledDays = [0, 6];
                $(".datePicker123").datepicker({
                    autoclose: true,
                    format: "dd/mm/yyyy",
                    daysOfWeekDisabled: [0, 1, 2, 3, 4, 5]
                }).datepicker("setDate", $scope.Data.Date);
                $scope.WorkingDayMachineSatList();
                ShowPopup($,
                    "#SaveWorkingDayMachineSatList",
                    $scope.tableInfo.PopupWidth,
                    $scope.tableInfo.PopupHeight);
                $scope.Mesenger = "";

            }           
            
            //Lấy danh sách máy chấm công
            $scope.WorkingDayMachineSatList = function () {
                var data = {
                    url: "/HR_WorkingDayMachineSatList/WorkingDayMachineSatList_GetList/"
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.WorkingDayMachineList = res.data.result;

                },function (res) {
                        $scope.msg = "Error";
                 });
              
            }

            //Hàm khi chọn combobox 
            $scope.ChangeWorkingDayMachineEdit = function () {
                if ($scope.Data.WorkingDayMachineIDEdit <0 || $scope.Data.WorkingDayMachineIDEdit == null) {
                    $scope.Mesenger = selectTimekeeper;
                }
                else {
                    $scope.Mesenger = "";
                    $scope.array = {};
                    $scope.WorkingDayMachineIDEdit = $scope.Data.WorkingDayMachineIDEdit;
                    var len = $scope.WorkingDayMachineList.length;
                    for (var i = 0; i < len; i++) {
                        if ($scope.WorkingDayMachineIDEdit == $scope.WorkingDayMachineList[i].WorkingDayMachineID) {
                            $scope.array = $scope.WorkingDayMachineList[i];
                            document.getElementById("Check").checked = true;
                            break;
                        }
                    }

                    if ($scope.array.StatusSat == 1) {
                        document.getElementById("Check").checked = true;
                    }
                    else {
                        document.getElementById("Check").checked = false;
                    }
                }

            }

            $scope.ChangeWorkingDayMachineADD = function () {

                if ($scope.Data.ListWorkingDayMachineID < 0 || $scope.Data.ListWorkingDayMachineID == null) {
                    $scope.Mesenger = selectTimekeeper;
                }
                else {
                    $scope.Mesenger = "";
                    $scope.array = {};
                    //$scope.WorkingDayMachineID = $scope.Data.WorkingDayMachineID;
                    //var len = $scope.WorkingDayMachineList.length;
                    //for (var i = 0; i < len; i++) {
                    //    if ($scope.WorkingDayMachineID == $scope.WorkingDayMachineList[i].WorkingDayMachineID) {
                    //        $scope.array = $scope.WorkingDayMachineList[i];
                    //        document.getElementById("Check").checked = true;
                    //        break;
                    //    }
                    //}

                    //if ($scope.array.StatusSat == 1) {
                    //    document.getElementById("Check").checked = true;
                    //}
                    //else {
                    //    document.getElementById("Check").checked = false;
                    //}
                }

            }


            $scope.ChangeDate = function () {
                if ($scope.Data.Date == "" || $scope.Data.Date == null) {
                    $scope.Mesenger = errorCreateDate;
                    return;
                }
                else {
                    $scope.Mesenger = "";

                }
            }
           

            // -----------------Save công thứ 7--------------
            $scope.Save = function () {

                //if ($scope.Data.WorkingDayMachineID < 0 || $scope.Data.WorkingDayMachineID == null) {
                //    $scope.Mesenger = "Vui lòng chọn máy chấm công";
                //    return;
                //}
                if ($scope.WorkingDayMachineADD == true) {
                    if ($scope.Data.ListWorkingDayMachineID < 0 || $scope.Data.ListWorkingDayMachineID == null) {
                        $scope.Mesenger = selectTimekeeper;
                        return;
                    }
                }
                if ($scope.WorkingDayMachineEdit == true) {
                    if ($scope.Data.WorkingDayMachineIDEdit < 0 || $scope.Data.WorkingDayMachineIDEdit == null) {
                        $scope.Mesenger = selectTimekeeper;
                        return;
                    }
                }
                if ($scope.Data.Date == null || $scope.Data.Date == "") {
                    $scope.Mesenger = errorCreateDate;
                    return;
                }
               
                else {
                    $scope.Mesenger = "";
                    
                    
                    var checked = document.getElementById('Check');
                    if (checked.checked) {
                        $scope.Data.IsFullday = 1;
                    }
                    else {
                        $scope.Data.IsFullday = 0;
                    }
                    var date = $scope.Data.Date.split("/");
                    $scope.Data.Day = date[2] + "/" + date[1] + "/" + date[0];
                    if ($scope.WorkingDayMachineEdit == true) {
                        $scope.Data.WorkingDayMachineID = $scope.Data.WorkingDayMachineIDEdit;
                        var SaveAction = $http({
                            method: "POST",
                            url: "/HR_WorkingDayMachineSatList/WorkingDayMachineSatList_Save",
                            data: {
                                data: $scope.Data,
                            },
                            dataType: "json"
                        });
                    }
                    if ($scope.WorkingDayMachineADD == true) {

                        var SaveAction = $http({
                            method: "POST",
                            url: "/HR_WorkingDayMachineSatList/WorkingDayMachineSatList_Save",
                            data: {
                                data: $scope.Data,
                                ListWorkingDayMachineID: $scope.Data.ListWorkingDayMachineID,
                            },
                            dataType: "json"
                        });
                      
                    }
                    SaveAction.then(function (res) {
                        if (res.data.result.IsSuccess == true) {
                            if ($scope.Data.AutoID != 0 && $scope.Data.AutoID != null) {
                                AppendToToastr(true, notification, successfulUpdate, 500, 5000);
                            }
                            else {
                                AppendToToastr(true, notification, successfulAdd, 500, 5000);

                            }
                            $scope.WorkingDayMachineSatListData.reload();
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
               
               
            }

            //-----------------Edit công thứ 7--------------
            $scope.editClick = function (contentItem) {
                $scope.WorkingDayMachineSatList();
                $scope.Data.AutoID = contentItem.AutoID
                $scope.Data.WorkingDayMachineIDEdit = contentItem.WorkingDayMachineID;
                $scope.Data.Date = FormatDate(contentItem.Day);
                $scope.Data.Note = contentItem.Note;
                if (contentItem.IsFullday==1) {
                    document.getElementById("Check").checked = true;
                }
                else {
                    document.getElementById("Check").checked = false;
                }

                var disabledDays = [0, 6];
                $(".datePicker123").datepicker({
                    autoclose: true,
                    format: "dd/mm/yyyy",
                    daysOfWeekDisabled: [0, 1, 2, 3, 4, 5]
                }).datepicker("setDate", $scope.Data.Date);

                $scope.WorkingDayMachineADD = false;
                $scope.WorkingDayMachineEdit = true;
                ShowPopup($,
                   "#SaveWorkingDayMachineSatList",
                   $scope.tableInfo.PopupWidth,
                   $scope.tableInfo.PopupHeight);
            }
            //load lại trang khi click bỏ qua
            $scope.CloseForm = function () {
                $scope.Mesenger = "";
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
                        $scope.GetListData();
                        $scope.WorkingDayMachineSatListData.reload();
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
