function BuildTable(appName, controllerName, tableUrl) {
    app.controller(controllerName,
        function ($scope, myService, $filter, $rootScope) {
            // $scope.test = $filter('mm/dd/yyyy')("2018/01/01", 'yyyy/MM/dd');
            $scope.maxSize = 5; // Limit number for pagination display number.
            $scope.totalCount = 0; // Total number of items in all pages. initialize as a zero
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
                if (tableUrl == "/OrganizationUnit/GetCompany") {
                    var data = {
                        url: "/OrganizationUnit/GetCompany"
                    }
                    var list = myService.getDropdown(data);
                    list.then(function (res) {
                        $scope.employees = res.data.result;

                    }, function (res) {
                        $scope.msg = "Error";
                    })
                }
                else {
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
                    pageSize: 100,
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
                        $rootScope.idrolre1.push(contentItem.OrganizationUnitID);
                    }
                    else {
                        for (var i = 0; i < $rootScope.idrolre1.length; i++) {
                            if ($rootScope.idrolre1[i] == contentItem.OrganizationUnitID) {
                                $rootScope.idrolre1.splice(i, 1);
                                j = 1;
                            }
                        }
                        if (j == 0) {
                            $rootScope.idrolre1.push(contentItem.OrganizationUnitID);
                        }
                    }
                }
            }
            //------------chose-click-------------------------------
            $scope.choseClick = function (contentItem) {
                if (contentItem.OrganizationUnitID != null) {
                    $rootScope.checkRole = false;
                    $scope.$parent.GetListData();
                    if (document.getElementById(contentItem.OrganizationUnitID).checked == false) {
                        document.getElementById(contentItem.OrganizationUnitID).checked = true;
                    }
                    else {
                        document.getElementById(contentItem.UserID).checked = false;
                    }
                    var edit = myService.getDataById(contentItem.OrganizationUnitID, 46, '/Sec_User_ViewCompany/GetUserByIdCompany');
                    edit.then(function (res) {
                        var len = res.data.result.length;
                        for (var i = 0; i < len; i++) {
                            $scope.choseClick(res.data.result[i]);
                        }
                    });
                }
                else {
                    if (document.getElementById(contentItem.UserID).checked == false) {
                        document.getElementById(contentItem.UserID).checked = true;
                    }
                    else {
                        document.getElementById(contentItem.UserID).checked = false;
                    }
                }
                $scope.editClick(contentItem);
            }
            //-----------------Update------------------
            $scope.UpdateClick = function () {
                if ($rootScope.idrolre != null && $rootScope.idrolre1 != null) {
                    for (var i = 0; i < $rootScope.idrolre.length; i++) {
                        for (var j = 0; j < $rootScope.idrolre1.length; j++) {
                            var id2 = $rootScope.idrolre[i];
                            var id1 = $rootScope.idrolre1[j];
                            $scope.editData = { id1, id2 };
                            myService.UpdateData('/Sec_User_ViewCompany/SaveSec_User_ViewCompany', $scope.editData);
                        }
                    }
                    AppendToToastr(true, notification, setRoleSuccessful, 500, 5000);
                }
                else {
                    AppendToToastr(false, notification, mustBeChooseUserAndCompany, 500, 5000);
                }
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