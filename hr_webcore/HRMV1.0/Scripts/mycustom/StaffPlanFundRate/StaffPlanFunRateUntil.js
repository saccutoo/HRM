function BuildTable(appName, controllerName, tableUrl) {
    app.controller(controllerName,
        function ($scope, myService, $filter, $rootScope, $compile, $http) {
            $scope.maxSize = 5; // Limit number for pagination display number.
            $scope.totalCount = 0; // Total number of items in all pages. initialize as a 
            $scope.pageIndex = 1; // Current page number. First page is 1.-->
            $scope.pageSizeSelected = 5;
            $scope.isShowFilter = false; $scope.typeEnds = [{ name: "Và", nameEN: "And", value: " and " }, { name: "Hoặc", nameEN: "Or", value: " or " }];
            $scope.typeFilterA = [{ name: ">", nameEN: ">", value: " > '#' " }, { name: "<", nameEN: "<", value: " < '#' " }, { name: "=", nameEN: "=", value: " = '#' " }, { name: "Khác", nameEN: "Not equal", value: " != '#' " }];
            $scope.typeFilterB = [{ name: "Có chứa", nameEN: "Contains", value: " like '%#%' " }, { name: "Bằng", nameEN: "Is", value: " = '#' " }, { name: "Không chứa", nameEN: "Do not contains", value: " != '#' " }];
            $scope.filterColumnsChoosed = [];
            $scope.emailValid = /^[a-z]+[a-z0-9._]+@[a-z]+\.[a-z.]{2,5}$/;
            var today = new Date();
            var yyyy = today.getFullYear();
            $scope.FromMonth = yyyy;
            $scope.Year = yyyy;
            $rootScope.data = {
                filter5: '',
                filter1: '',
                filter2: '',
                filter3: '',
                filter4: '',
                filter6: '',
                filter7: '',
                filter8: '',
                filter: 'Year LIKE ' + $scope.Year,
            }; //dự liệu truyền vào
            $scope.Width = "width:1860px;";
            $scope.Height = 'height:480px;'

            $scope.IshowTotal = true;

            $scope.isShowTimeSSN = false;
            $scope.showtab13 = true;
            $scope.isShow1 = true;
            $scope.Data = {};
            $scope.Data.SumValue = 0;


            //model đượct truyền ra từ directive build table
            $scope.employee = [];


            //Global theo parentID
            $scope.GlobalListWhereParentID = {
                Status: 60,
                Product: 3320
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
                        AppendToToastr(false, Notification, errorNotiFalse);
                    });
            }

            $scope.GetAddPermission = function (idTable) {
                var tblAction = myService.getTableAction(idTable);
                tblAction.then(function (emp) {

                    $scope.tablePermission = emp.data.result;
                    if ($scope.tablePermission && $scope.tablePermission.isEdit == false) {
                        $scope.is_readonly = true;
                    }
                    else {
                        $scope.is_readonly = false;
                    }
                    // $scope.BuildAddButton(emp.data.result);
                },
                    function (emp) {
                        AppendToToastr(false, Notification, errorNotiFalse);
                    });
            }

            $scope.getTableInfo();

            $scope.formatData = function (type, value) {
                if (type === 3 && value != null) {

                    return FormatDate(value);

                }
                if (type === 2 && value != null) {
                    return value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                }
                return value;

            }
            $scope.SetHiddenActionColumn = function (showEdit, showDelete) {
                if (showEdit === false && showDelete === false) {
                    return false;
                }
                return true;
            }

            //$scope.init = function () {
            //    $scope.ListOrganizationUnit();
            //    $scope.ListCurrencyName();
            //    $scope.ListEmployees();
            //    $scope.ListStatus();
            //}
            //---------------------------------------------------------------------------- List-End ------------------------------------------------------------------------------------


            //-----------------Hiển thị lỗi khi chọn phụ cấp-----------    


            //-----------------Lấy value khi click tìm kiếm--------------
            $scope.getFilterValue = function () {
                var stringFilter = "";
                if ($scope.OrganizationUnitID != null && $scope.OrganizationUnitID != "") {
                    stringFilter += " OrganizationUnitID = " + $scope.OrganizationUnitID + " ";
                }
                if ($scope.FromMonth != null) {
                    if ($scope.FromMonth != "") {
                        stringFilter += "Year = " + $scope.FromMonth + " ";
                    }

                }
                if ($scope.OrganizationUnitID != null && $scope.FromMonth != null && $scope.OrganizationUnitID != "" && $scope.FromMonth != "") {
                    stringFilter = " OrganizationUnitID = " + $scope.OrganizationUnitID + " and a.Year = " + $scope.FromMonth + " ";
                }
                return stringFilter;
            };

      //------------ Add chính sách-----------           
            $scope.addClick = function () {
                $scope.editFromsubmit = false;
                $scope.StaffPlanFundRateModel = {};
                var today = new Date();
                var yyyy = today.getFullYear();
                $scope.StaffPlanFundRateModel.Year = yyyy;
                ShowPopup($,
                    "#StaffPlanFunRateSave",
                    $scope.tableInfo.PopupWidth,
                    $scope.tableInfo.PopupHeight);
            }

            //-----------------Edit Chính sách--------------
            $scope.EditClick = function (employee) {
                employee.Data = {};
                employee.LoadingInput = true;
                employee.Show = false;
                employee.Data.AutoID = employee.AutoID;
                employee.Data.StaffID = employee.StaffID;
                employee.Data.StatusId = employee.StatusFormat;
                if (employee.DS_OrganizationUnitID != null) {
                    employee.Data.DS_OrganizationUnitID = employee.DS_OrganizationUnitID;
                }
                if (employee.CurrencyTypeID != null) {
                    employee.Data.CurrencyTypeID = employee.CurrencyTypeID;
                }
                employee.Data.Year = employee.Year;
                if (employee.Q1 != null) {
                    employee.Data.Q1 = employee.Q1;
                }
                if (employee.Q2 != null) {
                    employee.Data.Q2 = employee.Q2;
                }
                if (employee.Q3 != null) {
                    employee.Data.Q3 = employee.Q3;
                }
                if (employee.Q4 != null) {
                    employee.Data.Q4 = employee.Q4;
                }
                if (employee.Note != null) {
                    employee.Data.Note = employee.Note;
                }
            }

            $scope.HideClick = function (employee) {
                employee.LoadingInput = false;
                employee.Show = true;
            }
            //---------------Edit td----------------------
            $scope.SaveEditClick = function (tblDatas) {
                var lstTemp = [];
                for (var i = 0; i < tblDatas.length; i++) {
                    if (tblDatas[i].Data != null) {
                        if (tblDatas[i].Data && tblDatas[i].Data.StatusId == 'zero') {
                             tblDatas[i].Data.Status = 0;
                        }
                        else{
                            tblDatas[i].Data.Status = 1;
                        }
                        lstTemp.push(tblDatas[i].Data);
                    }
                }
                if (lstTemp.length == 0) return;
                if ($scope.ValidationButtonUpdate(lstTemp)) {
                    var SaveAction = myService.UpdateData("/StaffPlanFundRate/StaffPlanFundRate_Save", lstTemp);
                    if (SaveAction != null) {
                        SaveAction.then(function (res) {
                            if (res.data.result.IsSuccess == true) {
                                AppendToToastr(true, notification, successfulUpdate, 500, 5000);
                                $scope.StaffPlanFundRateData.reload();
                                $scope.Data = [];
                                $scope.CloseForm();
                            }
                            else {
                                AppendToToastr(false, notification, updateFailed, 500, 5000);
                                $scope.StaffPlanFundRateData.reload();
                                $scope.Data = [];
                                $scope.CloseForm();

                            }
                        }, function (res) {
                            AppendToToastr(false, notification, errorNotiFalse);
                        });
                    }
                    else {
                        AppendToToastr(false, notification, errorNotFixed, 500, 5000);

                    }
                }
         

            }
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
                        $scope.StaffPlanFundRateData.reload();
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
            // -----------------ADD tổ chức phòng ban--------------
            $scope.Save = function (form) {
                $scope.editFromsubmit = true;
                if (!form.$error.required) {
                    var lstTemp = [];
                    lstTemp.push($scope.StaffPlanFundRateModel);
                    var SaveAction = $http({
                        method: "POST",
                        url: "/StaffPlanFundRate/StaffPlanFundRate_Save",
                        data: {
                            data: lstTemp
                        },
                        dataType: "json"
                    });
                    SaveAction.then(function (res) {
                        if (res.data.result.IsSuccess == true) {
                            $scope.editFromsubmit = false;
                            if ($scope.StaffPlanFundRateModel.AutoID != 0 || $scope.StaffPlanFundRateModel.AutoID != null) {
                                AppendToToastr(true, notification, successfulUpdate, 500, 5000);
                            }
                            else {
                                AppendToToastr(true, notification, successfulAdd, 500, 5000);

                            }
                            $scope.StaffPlanFundRateData.reload();
                            $scope.CloseForm();
                        }
                        else {
                            AppendToToastr(false, notification, res.data.result.Message, 500, 5000);
                            $scope.StaffPlanFundRateData.reload();
                            $scope.CloseForm();

                        }
                    }, function (res) {
                        AppendToToastr(false, notification, errorNotiFalse);
                    });
                }
               
            }

            //load lại trang khi click bỏ qua
            $scope.CloseForm = function () {
                $scope.editFromsubmit = false;
                $scope.StaffPlanFundRateModel = {};
                $.colorbox.close();
            }
            $scope.changeEditTableColumn = function (item) {
                //if (item.StaffID == '' || item.StaffID == null || item.StaffID == undefined) item.ErrorStaff = errorStaff;
                //else {
                //    item.ErrorStaff = '';
                //}
                if (item.DS_OrganizationUnitID == '' || item.DS_OrganizationUnitID == null || item.DS_OrganizationUnitID == undefined) item.ErrorOrganizationUnit = errorOrganizationUnit;
                else {
                    item.ErrorOrganizationUnit = '';
                }
                if (item.StatusId == '' || item.StatusId == null || item.StatusId == undefined) item.ErrorStatus = errorStatus;
                else {
                    item.ErrorStatus = '';
                }
                if (item.Year == '' || item.Year == null || item.Year == undefined) item.ErrorYear = errorYear;
                else {
                    item.ErrorYear = '';
                }
            }
            $scope.ValidationButtonUpdate = function (objectUpdate) {
                var result = true;
               $.each(objectUpdate, function (index, obj) {
                   //if (obj.StaffID == '' || obj.StaffID == null || obj.StaffID == undefined) return result =  false;
                   if (obj.DS_OrganizationUnitID == '' || obj.DS_OrganizationUnitID == null ||  obj.DS_OrganizationUnitID == undefined) return result =   false;
                   if (obj.StatusId == '' || obj.StatusId == null || obj.StatusId == undefined) return  result =  false;
                   if (obj.Year == '' || obj.Year == null || obj.Year == undefined)return result = false;
               })
               return result;

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
                    if ([8, 13, 27, 37, 38, 39, 40, 190].indexOf(event.which) > -1) {
                        // backspace, enter, escape, arrows
                        return true;
                    } else if (event.which >= 48 && event.which <= 57) {
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
    app.directive('currencyMaskFourSpace', function () {
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
}
