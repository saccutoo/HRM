function BuildTable(appName, controllerName, tableUrl) {
    app.controller(controllerName,
        function ($scope, myService, $filter, $rootScope) {
            // $scope.test = $filter('mm/dd/yyyy')("2018/01/01", 'yyyy/MM/dd');
            $scope.maxSize = 5; // Limit number for pagination display number.
            $scope.totalCount = 0; // Total number of items in all pages. initialize as a 
            $scope.pageIndex = 1; // Current page number. First page is 1.-->
            $scope.pageSizeSelected = 5;
            $scope.isShowFilter = false; $scope.typeEnds = [{ name: "Và", nameEN: "And", value: " and " }, { name: "Hoặc", nameEN: "Or", value: " or " }];
            $scope.typeFilterA = [{ name: ">", nameEN: ">", value: " > '#' " }, { name: "<", nameEN: "<", value: " < '#' " }, { name: "=", nameEN: "=", value: " = '#' " }, { name: "Khác", nameEN: "Not equal", value: " != '#' " }];
            $scope.typeFilterB = [{ name: "Có chứa", nameEN: "Contains", value: " like '%#%' " }, { name: "Bằng", nameEN: "Is", value: " = '#' " }, { name: "Không chứa", nameEN: "Do not contains", value: " != '#' " }];
            $scope.filterColumnsChoosed = [];

            $scope.emailValid = /^[a-z]+[a-z0-9._]+@[a-z]+\.[a-z.]{2,5}$/;
            $scope.results = '2';
            $rootScope.checkRole = false;


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
                    pageSize: 50000,
                    filter: $scope.getFilterValue()
                }
                var getDataTbl = myService.GetTableData(data, tableUrl);
                getDataTbl.then(function (emp) {
                    $scope.employees = emp.data.employees;
                    $scope.totalCount = emp.data.totalCount;
                    $scope.lstTotal = emp.data.lstTotal;
                    $scope.SetTotalByColumns = function (totalName) {
                        if (!angular.isUndefined(totalName) && totalName !== null) {
                            return $scope.lstTotal[totalName];
                        }
                        return "-";
                    }
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

            //-----------------List-End---------
            //-----------------Filter-----------
            $scope.getFilterColumns = function () {
                var filter = myService.getFilterColumns($scope.tableInfo.id);
                filter.then(function (res) {
                    HiddenLoader();
                    $scope.FilterColumnsItem = res.data.result;
                    $scope.isShowFilter = true;
                    $scope.tablePermission.isFilterButton = false;
                    var filterColumnsItem = {
                        filterColumns: res.data.result,
                        typeFilter: {},
                        typeEnds: $scope.typeEnds,
                        textValue: "",
                        typeEndsSeleted: $scope.typeEnds[0]
                    }
                    $scope.filterColumnsChoosed.push(filterColumnsItem);

                },
                    function (res) {
                        AppendToToastr(false, notification, errorNotiFalse);
                    });
            }
            $scope.cancelFilterClick = function () {
                $scope.filterColumnsChoosed = [];
                $scope.isShowFilter = false;
                $scope.tablePermission.isFilterButton = true;
                $scope.GetListData();
            };
            $scope.setShowTypeEnd = function (index) {
                if ($scope.filterColumnsChoosed.length - 1 === index) {
                    return false;
                };
                return true;
            }
            $scope.addFilterColumns = function () {
                var filterColumnsItem = {
                    filterColumns: $scope.FilterColumnsItem,
                    typeEnds: $scope.typeEnds,
                    textValue: "",
                    typeEndsSeleted: $scope.typeEnds[0]
                }
                $scope.filterColumnsChoosed.push(filterColumnsItem);
            }
            $scope.filterColumnsChange = function (filterSelected, index) {

                var type = filterSelected.Type;
                if (type === 2 || type === 3) {
                    $scope.typeFilter = $scope.typeFilterA;
                    $scope.filterColumnsChoosed[index].typeFilter = $scope.typeFilterA;
                } else if (type === 1) {
                    $scope.typeFilter = $scope.typeFilterB;
                    $scope.filterColumnsChoosed[index].typeFilter = $scope.typeFilterB;
                }
            };
            $scope.removeColumnFilterByIndex = function (index) {
                $scope.filterColumnsChoosed.splice(index, 1);
            };
            $scope.setTypeInput = function (type) {
                return "text";
            };
            $scope.getFilterValue = function () {
                var lstObj = $scope.filterColumnsChoosed;
                var stringFilter = "";
                var len = lstObj.length - 1;
                for (var key in lstObj) {
                    var obj = lstObj[key];
                    //                    if (obj.filterSelected.Type === 3) {
                    //                        obj.textValue = $filter('date')(new Date(obj.textValue), 'yyyy/MM/dd');
                    //                    }
                    // console.log(obj.textValue);
                    var tmpFilter = obj.filterSelected.ColumnName + obj.typeFilterSelected.value.replace("#", obj.filterSelected.Type === 3 ? $filter('date')(new Date(obj.textValue), 'yyyy/MM/dd') : obj.textValue) + (parseInt(String(key)) === len ? "" : obj.typeEndsSeleted.value);
                    stringFilter += tmpFilter;
                }
                return stringFilter;
            };
            $scope.showFilterApplyButton = function () {
                var lstObj = $scope.filterColumnsChoosed;
                if (lstObj.length === 0) {
                    return false;
                }
                for (var key in lstObj) {
                    var obj = lstObj[key];
                    if (obj.textValue === "") {
                        return false;
                    }
                }
                return true;
            };
            //-----------------Filter-End----------

            // -----------------Edit------------
            $scope.editClick = function (contentItem) {
                if (contentItem.UserID != null) {
                    if ($rootScope.idrolre == null) {
                        $rootScope.idrolre = [];
                    }
                    var j = 0;
                    if ($rootScope.idrolre.length == 0) {
                        $rootScope.idrolre.push(contentItem.UserID);
                    }
                    else {
                        for (var i = 0; i < $rootScope.idrolre.length; i++) {
                            if ($rootScope.idrolre[i] == contentItem.UserID) {
                                $rootScope.idrolre.splice(i, 1);
                                j = 1;
                            }
                        }
                        if (j == 0) {
                            $rootScope.idrolre.push(contentItem.UserID);
                        }
                    }
                }
                else {
                    if ($rootScope.idrolre1 == null) {
                        $rootScope.idrolre1 = [];
                    }
                    var j = 0;
                    if ($rootScope.idrolre1.length == 0) {
                        $rootScope.idrolre1.push(contentItem.RoleID);
                    }
                    else {
                        for (var i = 0; i < $rootScope.idrolre1.length; i++) {
                            if ($rootScope.idrolre1[i] == contentItem.RoleID) {
                                $rootScope.idrolre1.splice(i, 1);
                                j = 1;
                            }
                        }
                        if (j == 0) {
                            $rootScope.idrolre1.push(contentItem.RoleID);
                        }
                    }
                }
            }
            //------------chose-click-------------------------------


            $scope.choseClick = function (contentItem) {
                if (document.getElementById(contentItem.NameEN).checked == false) {
                    document.getElementById(contentItem.NameEN).checked = true;
                }
                else {
                    document.getElementById(contentItem.NameEN).checked = false;
                }
                var data = {
                    pageIndex: $scope.pageIndex,
                    pageSize: 50000,
                    filter: $scope.getFilterValue()
                }
                var getSysTableColumnID = myService.getSysTableColumnID(data, '/Sys_table_Column_Role/getSysTableColumnID');
                var edit = myService.getDataById(contentItem.RoleID, 27, '/Sys_table_Column_Role/Get_Sys_Table_Colum_Role');
                edit.then(function (res) {
                    if (res.data.result.length == 0) {
                        getSysTableColumnID.then(function (res1) {
                            for (var j = 0; j < res1.data.employees.length; j++) {
                                document.getElementById(res1.data.employees[j].id).checked = false;
                            }
                        });
                    }
                    else {
                        getSysTableColumnID.then(function (res1) {
                            for (var j = 0; j < res1.data.employees.length; j++) {
                                document.getElementById(res1.data.employees[j].id).checked = false;
                            }
                            for (var i = 0; i < res.data.result.length; i++) {
                                for (var j = 0; j < res1.data.employees.length; j++) {
                                    if (res.data.result[i].TableColumnId == res1.data.employees[j].id) {
                                        document.getElementById(res.data.result[i].TableColumnId).checked = true;
                                    }

                                }
                            }

                        });

                    }

                });

            }

            //-----------------Update------------------
            // Lặp qua từng checkbox để lấy giá trị

            $scope.UpdateClick = function () {
                $(function () {
                    var dt = Loading();

                    $scope.checklistSecRoleList = [];
                    $scope.SecRoeID = [];

                    var checkboxSysTableColumnID = document.getElementsByName('SysTableColumnID');
                    var checkboxSecRoleID = document.getElementsByName('SecRoeID');
                    for (var i = 0; i < checkboxSysTableColumnID.length; i++) {
                        if (checkboxSysTableColumnID[i].checked === true) {
                            $scope.checklistSecRoleList.push(checkboxSysTableColumnID[i].value);
                        }
                    }
                    for (var i = 0; i < checkboxSecRoleID.length; i++) {
                        if (checkboxSecRoleID[i].checked === true) {
                            $scope.SecRoeID.push(checkboxSecRoleID[i].value);
                        }
                    }
                    if ($scope.SecRoeID != "") {
                        if ($scope.checklistSecRoleList != "") {
                            $scope.Data = [];
                            for (var i = 0; i < $scope.SecRoeID.length; i++) {
                                for (var j = 0; j < $scope.checklistSecRoleList.length; j++) {
                                    var RoleID = $scope.SecRoeID[i];
                                    var TableColumnId = $scope.checklistSecRoleList[j];
                                    $scope.editData = { "RoleID": parseInt(RoleID), "TableColumnId": parseInt(TableColumnId) };
                                    $scope.Data.push($scope.editData)
                                    //var insert = myService.UpdateData('/Sys_table_Column_Role/_Save_Sys_Table_Column_Role', $scope.editData);
                                }
                            }
                            var insert = myService.UpdateData('/Sys_table_Column_Role/_Save_Sys_Table_Column_Role', $scope.Data);
                            insert.then(function (res) {
                                if (res.data.result.IsSuccess == true) {
                                    AppendToToastr(true, notification, res.data.result.Message, 500, 5000);
                                    dt.finish();
                                }
                                else {
                                    AppendToToastr(false, notification, res.data.result.Message, 500, 5000);
                                    dt.finish();
                                }
                            });
                        }
                        else {
                            AppendToToastr(false, notification, errorTableColumn, 500, 5000);
                            dt.finish();
                        }
                    }
                    else {
                        AppendToToastr(false, notification, errorSecRole, 500, 5000);
                        dt.finish();
                    }
                });

            }
        });

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