function BuildTable(appName, controllerName, tableUrl) {
    app.controller(controllerName, ['$scope', 'myService', '$filter', '$rootScope',
        function ($scope, myService, $filter, $rootScope) {
            $scope.maxSize = 5; // Limit number for pagination display number.
            $scope.totalCount = 0; // Total number of items in all pages. initialize as
            $scope.pageIndex = 1; // Current page number. First page is 1.-->
            $scope.pageSizeSelected = 5;
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
            $scope.editData = {}
            $scope.editData.FromDate = dd + "/" + mm + "/" + yyyy;
            $scope.month1 = "'" + yyyy + "-" + mm + "-28'";
            $scope.month2 = "'" + yyyy + "-" + mm + "-29'";
            $rootScope.data = {
                filter5: '',
                filter1: '',
                filter2: '',
                filter3: '',
                filter4: '',
                filter6: '',
                filter7: '',
                filter8: '',
                //filter: '(CONVERT(NVARCHAR(MAX),CHECKTIME,126) > ' + $scope.month1 + ' AND CONVERT(NVARCHAR(MAX),CHECKTIME,126) <  ' + $scope.month2 + ')',
                filter:'',
            }; //dự liệu truyền vào
            $rootScope.IsBtnMeger = true;

            //model đượct truyền ra từ directive build table
            $scope.employeeData = {};
            $scope.employee = [];
            $scope.RemoveListPolicyAllowance = [];
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

            // -----------------Edit------------
            $scope.editClick = function (contentItem) {
                $rootScope.test = 1;
                $scope.results = '1';
                $scope.is_edit = false;
                $scope.is_add = false;
                $scope.is_MaPB = false;
                $scope.is_TenPB = false;
                $scope.is_TenENPB = false;
                $scope.is_TenPBSpecial = false;
                $scope.isphone = false;
                ListOrganizationUnit();
                var edit = myService.getDataById(contentItem.OrganizationUnitID, $scope.tableInfo.id, $scope.tableInfo.DataEditUrl);
                edit.then(function (emp) {
                    emp.data.result.Status = emp.data.result.Status ? String(1) : String(0);
                    $scope.editData = emp.data.result;
                    if (emp.data.result.ParentID == "" || emp.data.result.ParentID == null) {
                        ListOrganizationUnit();
                    }
                    ShowPopup($,
                        "#SaveContent",
                        $scope.tableInfo.PopupWidth,
                        $scope.tableInfo.PopupHeight);
                },
                    function (emp) {
                        AppendToToastr(false, notification, errorNotiFalse);
                    });
            }
            //list máy chấm công
            $scope.ListHR_WorkingDayMachine = function () {
                var data = {
                    url: "/HR_Holiday/Get_HR_WorkingDayMachine"
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getListHR_WorkingDayMachine = res.data.result;
                },
                    function (res) {
                        $scope.msg = "Error";
                    });
            }

            $scope.ListHR_WorkingDayMachine();
            //Dropdown nhân viên theo máy chấm công
            $scope.ChangeWorkingDayMachineIDOld = function () {
                if ($scope.editData.WorkingDayMachineIDOld == '' || $scope.editData.WorkingDayMachineIDOld == null) {
                    if ($scope.editData.WorkingDayMachineIDOld == 0) {
                        $scope.ErrorWorkingDayMachineIDOld = '';
                        var data = {
                            url: "/GopCong/EmployeeByWorkingDayMachineID?id=" + $scope.editData.WorkingDayMachineIDOld
                        }
                        var list = myService.getData(data);
                        list.then(function (res) {
                            $scope.ListEmployeeWhereWorkingDayMachine = res.data.result;
                        }, function (res) {
                            $scope.msg = "Error";
                        })
                    }
                    else {
                        $scope.ErrorWorkingDayMachineIDOld = errorSelectTimekeeperOld;
                        return;
                    }

                }
                else {
                    $scope.ErrorWorkingDayMachineIDOld = '';
                    var data = {
                        url: "/GopCong/EmployeeByWorkingDayMachineID?id=" + $scope.editData.WorkingDayMachineIDOld
                    }
                    var list = myService.getData(data);
                    list.then(function (res) {
                        $scope.ListEmployeeWhereWorkingDayMachine = res.data.result;
                    }, function (res) {
                        $scope.msg = "Error";
                    })
                }

            }

            $scope.ChangeWorkingDayMachine = function () {
                if ($scope.editData.WorkingDayMachineIDNew == '' || $scope.editData.WorkingDayMachineIDNew == null) {
                    if ($scope.editData.WorkingDayMachineIDNew == 0) {
                        $scope.ErrorWorkingDayMachineIDNew = '';
                    }
                    else {
                        $scope.ErrorWorkingDayMachineIDNew = errorSelectTimekeeperNew;
                        return;
                    }
                }
                if ($scope.editData.WorkingDayMachineIDOld != '' || $scope.editData.WorkingDayMachineIDOld != null) {
                    if ($scope.editData.WorkingDayMachineIDOld == $scope.editData.WorkingDayMachineIDNew) {
                        $scope.ErrorWorkingDayMachineIDNew = errorCompareTimekeeper;
                        return;
                    }
                    else {
                        $scope.ErrorWorkingDayMachineIDNew = '';
                    }
                }
                else {
                    $scope.ErrorWorkingDayMachineIDNew = '';
                }
            }

            $scope.changeDate = function () {
                if ($scope.editData.FromDate == '' || $scope.editData.FromDate == null) {
                    $scope.ErrorFromDate = errorDeliveryDate;
                    return;
                }
                else {
                    $scope.ErrorFromDate = "";
                }
            }

            $scope.ChangeStaff = function () {
                if ($scope.editData.StaffID == undefined) {
                    $scope.ErrorStaff = errorSelectStaff;
                    return;
                }
                else if ($scope.editData.StaffID.length == 0) {
                    $scope.ErrorStaff = errorSelectStaff;
                    return;
                }
                else {
                    $scope.ErrorStaff = "";
                }
            }
            $scope.ListEmployeeWhereWorkingDayMachine = function () {
                var data = {
                    url: "/GopCong/EmployeeByWorkingDayMachineID?id=" + 0
                }
                var list = myService.getData(data);
                list.then(function (res) {
                    $scope.ListEmployeeWhereWorkingDayMachine = res.data.result;
                }, function (res) {
                    $scope.msg = "Error";
                })
            }

            $scope.ListEmployeeWhereWorkingDayMachine();

            $scope.megerClick = function (item) {
                ShowPopup($,
                   "#SaveMerge",
                   $scope.tableInfo.PopupWidth,
                   $scope.tableInfo.PopupHeight);
            }
            //Gộp công chuyển công
            $scope.transferTimekeeper = function () {
                if ($scope.editData.WorkingDayMachineIDOld == '' || $scope.editData.WorkingDayMachineIDOld == null) {
                    if ($scope.editData.WorkingDayMachineIDOld == 0) {
                        $scope.ErrorWorkingDayMachineIDOld = '';
                    }
                    else {
                        $scope.ErrorWorkingDayMachineIDOld = errorSelectTimekeeperOld;
                        return;
                    }

                }
                if ($scope.editData.WorkingDayMachineIDNew == '' || $scope.editData.WorkingDayMachineIDNew == null) {
                    if ($scope.editData.WorkingDayMachineIDNew == 0) {
                        $scope.ErrorWorkingDayMachineIDNew = '';
                    }
                    else {
                        $scope.ErrorWorkingDayMachineIDNew = errorSelectTimekeeperNew;
                        return;
                    }
                }
                if ($scope.editData.FromDate == '' || $scope.editData.FromDate == null) {
                    $scope.ErrorFromDate = errorDeliveryDate;
                    return;
                }
                if ($scope.editData.StaffID == undefined) {
                    $scope.ErrorStaff = errorSelectStaff;
                    return;
                }
                if ($scope.editData.StaffID.length == 0) {
                    $scope.ErrorStaff = errorSelectStaff;
                    return;
                }
                var url = '/GopCong/Merge';
                $scope.editData.ListUserId = '';

                if ($scope.editData.StaffID != null && $scope.editData.StaffID.length > 0) {
                    for (var i = 0; i < $scope.editData.StaffID.length; i++) {
                        if ($scope.editData.ListUserId == '') {
                            $scope.editData.ListUserId += $scope.editData.StaffID[i];
                        }
                        else {
                            $scope.editData.ListUserId += ',' + $scope.editData.StaffID[i];

                        }
                    }
                }
                if ($scope.editData.FromDate != null && $scope.editData.FromDate != '') {
                    var date = $scope.editData.FromDate.split('/');
                    $scope.editData.FromDate = date[2] + '/' + date[1] + '/' + date[0];
                }
                $scope.ErrorWorkingDayMachineIDOld = '';
                $scope.ErrorWorkingDayMachineIDNew = '';
                $scope.ErrorFromDate = '';
                $scope.ErrorStaff = '';
                var updateAction = myService.UpdateData(url, $scope.editData);
                if (form.$valid) {
                    console.log('ok')
                }
                //updateAction.then(function (res) {
                //    if (res.data.result.IsSuccess) {
                //        AppendToToastr(res.data.result.IsSuccess, notification, res.data.result.Message, 500, 5000);
                //        $scope.editData = {};
                //    }
                //    else {
                //        AppendToToastr(false, notification, errorNotiFalse);
                //    }

                //},
                //    function (res) {
                //        AppendToToastr(false, notification, errorNotiFalse);
                //    });
            }

            $scope.OK = function () {
                if (Merge.$valid) {
                    console.log('ok')
                }
            }

            $scope.CancelMerge=function(){
                $scope.editData={};
                $.colorbox.close();
            }
            //-----------------Lấy value khi click tìm kiếm--------------           
        }]);
  
    app.directive('convertToNumber',
        function () {
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
    app.directive("formatDate",
        function () {
            return {
                require: 'ngModel',
                link: function (scope, elem, attr, modelCtrl) {
                    modelCtrl.$formatters.push(function (modelValue) {
                        return new Date(modelValue);
                    });
                }
            }
        });
}