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
            $rootScope.OpeningAdditionalWork = true;
            $scope.Data = {};
            $scope.ListData = [];
            $rootScope.quickFiltercomback = {};
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
            var 
            today = mm + '/' + yyyy;
            if ((mm - 1)<10) {
                if ((mm - 1) == 0) {
                    lastMonth = 12 + '/' + (yyyy - 1);
                }
                else {
                    lastMonth = '0' + (mm - 1) + '/' + yyyy;
                }
            }
            else {
                lastMonth =(mm - 1) + '/' + yyyy;
            }

            $scope.Data.FromMonth = today;


            today1 = yyyy + '/' + mm + '/' + dd;

            $scope.FromMonth1 = today1;
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


            $scope.SetHiddenActionColumn = function (showEdit, showDelete) {
                if (showEdit === false && showDelete === false) {
                    return false;
                }
                return true;
            }

            //---------------------------------------------------------------------------- List-End ------------------------------------------------------------------------------------

            $scope.OpeningAdditionalWorkClick = function () {
                $scope.isShow = false;
                if ($rootScope.quickFiltercomback.StaffName != null && $rootScope.quickFiltercomback.StaffName != '') {
                    $scope.Data.StaffID = parseInt($rootScope.quickFiltercomback.StaffName.Value);
                }
                $scope.Data.FromMonth = today;
                $(".datepicker").datepicker({
                    autoclose: true,
                    format: "mm/yyyy",
                    startDate: lastMonth,
                    endDate: today,
                    startView: "months",
                    minViewMode: "months",
                    maxViewMode: "years"
                }).datepicker("setDate", $scope.Data.FromMonth);
                if ($scope.Data.StaffID != null && $scope.Data.StaffID != '' && $scope.Data.FromMonth != null && $scope.Data.FromMonth != '') {
                    $scope.GetListtimekiping($scope.Data.StaffID, $scope.Data.FromMonth);
                }
                ShowPopup($,
                      "#SaveOpeningAdditionalWork",
                      $scope.tableInfo.PopupWidth,
                      $scope.tableInfo.PopupHeight);
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
                //var data = {
                //    url: "/OrganizationUnit/EmployeeByOrganizationUnitID?id=" + 0
                //}
                //var list = myService.getData(data);
                //list.then(function (res) {
                //    $scope.ListEmployees = res.data.result;
                //}, function (res) {
                //    $scope.msg = "Error";
                //})
            }
            $scope.ListEmployees();

            //------------Dropdown trạng thái-------------
            $scope.ListStatus = function () {
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
            $scope.ListStatus();
            $scope.changDate = function () {
                if ($scope.Data.StaffID != null && $scope.Data.StaffID != '' && $scope.Data.FromMonth != null && $scope.Data.FromMonth != '' && ($scope.Data.FromMonth == today || $scope.Data.FromMonth == lastMonth)) {
                    $scope.GetListtimekiping($scope.Data.StaffID, $scope.Data.FromMonth);
                    $scope.ErrorStaff = "";
                    $scope.ErrorDate = "";
                }
            }
            $scope.ChangeStaff = function () {
                if ($scope.Data.StaffID != null && $scope.Data.StaffID != '' && $scope.Data.FromMonth != null && $scope.Data.FromMonth != '') {
                    $scope.GetListtimekiping($scope.Data.StaffID, $scope.Data.FromMonth);
                }
            }
            //-----------------Lấy danh ngày bổ sung------------------
            //Lấy công trên máy và hiển thị lên form
            $scope.GetListtimekiping = function (userid, FromMonth) {
                var date = FromMonth.split("/");
                var data = {
                    pageIndex: $scope.pageIndex == null ? 1 : $scope.pageIndex,
                    pageSize: $scope.pageSizeSelected == null ? 5 : $scope.pageSizeSelected,
                    month: date[0],
                    year: date[1],
                    filter: userid
                }
                var getDataTbl = myService.GetTableData(data, "/Timekeeping_TimeSSN/TableServerSideGetData");

                getDataTbl.then(function (emp) {
                    $scope.timekipping = angular.copy(emp.data.employees);
                    $scope.listDateSuscess = [];
                    for (var i = 0; i < $scope.timekipping.length; i++) {
                        var d1 = new Date();
                        var d2 = new Date($scope.timekipping[i].CheckTime);
                        if (d1>=d2) {
                            $scope.timekipping[i].CheckTime = $scope.timekipping[i].CheckTime.slice(0, 10);
                            var date = $scope.timekipping[i].CheckTime.split('-');                         
                            var obj = { CheckTime: date[2] + '/' + date[1] + '/' + date[0] }
                            $scope.listDateSuscess.push(obj);
                        }
                    }
                    $scope.timekipping = angular.copy($scope.listDateSuscess);

                },
                    function (emp) {
                        AppendToToastr(false, notification, errorNotiFalse);
                    });
            }

            $scope.SaveAction = function (form) {
                if (form.$error.$required == undefined) {
                    $scope.ListData = [];
                    if ($scope.Data.ListOpenDay != null && $scope.Data.ListOpenDay.length > 0) {
                        for (var i = 0; i < $scope.Data.ListOpenDay.length; i++) {
                            var date = $scope.Data.ListOpenDay[i].split("/");
                            date = date[2] + "/" + date[1] + "/" + date[0];
                            var obj = { StaffID: $scope.Data.StaffID, OpenDay: date, Status: $scope.Data.Status };
                            $scope.ListData.push(obj);
                        }
                    }
                    else {
                        var date = $scope.Data.OpenDay.split("/");
                        $scope.Data.OpenDay = date[2] + "/" + date[1] + "/" + date[0];
                        $scope.ListData.push($scope.Data);
                    }
                    if ($scope.isShow==true) {
                        var SaveAction = $http({
                            method: "POST",
                            url: "/HR_OpeningAdditionalWork/OpeningAdditionalWorkEdit",
                            data: $scope.Data,
                            dataType: "json"
                        });
                    }
                    else {                                           
                        var SaveAction = $http({
                            method: "POST",
                            url: "/HR_OpeningAdditionalWork/OpeningAdditionalWorkSave",
                            data:$scope.ListData,
                            dataType: "json"
                        });
                    }
                    SaveAction.then(function (res) {
                        form.$dirty = false;
                        form.$invalid = false;
                        form.$submitted = false;
                        form.$valid = false;
                        if (res.data.result.IsSuccess == true) {                         
                            AppendToToastr(true, notification, success, 500, 5000);
                            $scope.HR_OpeningAdditionalWorkData.reload();
                            $scope.Data = {};
                            $scope.CloseForm(form);
                        }
                        else {
                            if ($scope.isShow == true && res.data.Status == 0) {
                                AppendToToastr(false, notification, res.data.result.Message, 500, 5000);
                                $scope.HR_OpeningAdditionalWorkData.reload();
                                $scope.CloseForm(form);
                            }
                            else {
                                AppendToToastr(false, notification, updateFailed, 500, 5000);
                                $scope.HR_OpeningAdditionalWorkData.reload();
                                $scope.CloseForm(form);
                            }


                        }
                    }, function (res) {
                        AppendToToastr(false, notification, errorNotiFalse);
                    });
                }
                else {
                    AppendToToastr(false, notification, errorFullInformation);
                }
            }

            $scope.editClick = function (contentItem) {
                $scope.isShow = true;
                $scope.Data = angular.copy(contentItem);
                $scope.Data.Status = $scope.Data.Status.toString();
                $scope.Data.OpenDay = FormatDate($scope.Data.OpenDay);
                var date = $scope.Data.OpenDay.split('/');
                if (date[0] == 29 || date[0] == 30 || date[0] == 31) {
                    var month = parseInt(date[1]) +1;
                    $scope.Data.FromMonth ="0" + month + '/' + date[2];
                }
                else {
                    $scope.Data.FromMonth = date[1] + '/' + date[2];
                }
                

                if ($scope.Data.FromMonth == today || $scope.Data.FromMonth == lastMonth) {
                    $scope.GetListtimekiping($scope.Data.StaffID, $scope.Data.FromMonth);
                    $(".datepicker").datepicker({
                        autoclose: true,
                        format: "mm/yyyy",
                        startDate: lastMonth,
                        endDate: today,
                        startView: "months",
                        minViewMode: "months",
                        maxViewMode: "years"
                    }).datepicker("setDate", $scope.Data.FromMonth);
                }
                else {
                    $scope.Data.FromMonth = null;
                    $(".datepicker").datepicker({
                        autoclose: true,
                        format: "mm/yyyy",
                        startDate: lastMonth,
                        endDate: today,
                        startView: "months",
                        minViewMode: "months",
                        maxViewMode: "years"
                    }).datepicker();
                }

                ShowPopup($,
                     "#SaveOpeningAdditionalWork",
                     $scope.tableInfo.PopupWidth,
                     $scope.tableInfo.PopupHeight);
            }



            //load lại trang khi click bỏ qua
            $scope.CloseForm = function (form) {
                form.$dirty = false;
                form.$invalid = false;
                form.$submitted = false;
                form.$valid = false;
                $scope.Data = {};
                $scope.timekipping = [];
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
                var AutoID = obj.AutoID;
                var action = myService.deleteAction(AutoID, $scope.tableInfo.id, $scope.tableInfo.DeleteUrl);
                action.then(function (res) {
                    if (res.data.result.IsSuccess == true) {
                        AppendToToastr(res.data.result.IsSuccess, notification, deleteSuccess, 500, 5000);
                        $scope.HR_OpeningAdditionalWorkData.reload();
                    }
                    else {
                        if (res.data.Status==1) {
                            AppendToToastr(false, notification, res.data.result.Message);
                        }
                        else {
                            AppendToToastr(false, notification, errorNotiFalse);
                        }
                       
                    }
                },
                    function (res) {
                        AppendToToastr(false, notification, errorNotiFalse);
                    });

            }

            // -----------------Xóa--End------------

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
