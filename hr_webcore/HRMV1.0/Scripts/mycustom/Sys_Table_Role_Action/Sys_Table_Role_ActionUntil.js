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

            $scope.results = '1';
            $scope.idcheck = 0;
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
                    pageSize: 500,
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
            //check box chọn all khi chọn chức năng
            $scope.ChoseAll = function (employee) {
                var id = employee.id;
                var isAdd, isEdit, isDelete, isActive, isFilterButton, isExcel, isSubmit, isApproval, isDisApproval, isCopy, isIndex, isGet;
                isAdd = 'isAdd-' + id;
                isEdit = 'isEdit-' + id;
                isDelete = 'isDelete-' + id;
                isActive = 'isActive-' + id;
                isFilterButton = 'isFilterButton-' + id;
                isExcel = 'isExcel-' + id;
                isSubmit = 'isSubmit-' + id;
                isApproval = 'isApproval-' + id;
                isDisApproval = 'isDisApproval-' + id;
                isCopy = 'isCopy-' + id;
                isIndex = 'isIndex-' + id;
                isGet = 'isGet-' + id;
                if (document.getElementById(id).checked == true) {
                    document.getElementById(isAdd).checked = true;
                    document.getElementById(isEdit).checked = true;
                    document.getElementById(isDelete).checked = true;
                    document.getElementById(isActive).checked = true;
                    document.getElementById(isFilterButton).checked = true;
                    document.getElementById(isExcel).checked = true;
                    document.getElementById(isSubmit).checked = true;
                    document.getElementById(isApproval).checked = true;
                    document.getElementById(isDisApproval).checked = true;
                    document.getElementById(isCopy).checked = true;
                    document.getElementById(isIndex).checked = true;
                    document.getElementById(isGet).checked = true;
                }
                else {
                    document.getElementById(isAdd).checked = false;
                    document.getElementById(isEdit).checked = false;
                    document.getElementById(isDelete).checked = false;
                    document.getElementById(isActive).checked = false;
                    document.getElementById(isFilterButton).checked = false;
                    document.getElementById(isExcel).checked = false;
                    document.getElementById(isSubmit).checked = false;
                    document.getElementById(isApproval).checked = false;
                    document.getElementById(isDisApproval).checked = false;
                    document.getElementById(isCopy).checked = false;
                    document.getElementById(isIndex).checked = false;
                    document.getElementById(isGet).checked = false;
                }
            }
            //-------------------------------------
            // -----------------Edit------------
            $scope.editClick = function (contentItem) {
                if (contentItem.Id != null) {
                    if ($rootScope.idrolre == null) {
                        $rootScope.idrolre = [];
                    }
                    var j = 0;
                    ChoseAll(contentItem.Id);
                    if ($rootScope.idrolre.length == 0) {
                        $rootScope.idrolre.push(contentItem.Id);
                    }
                    else {
                        for (var i = 0; i < $rootScope.idrolre.length; i++) {
                            if ($rootScope.idrolre[i] == contentItem.Id) {
                                $rootScope.idrolre.splice(i, 1);
                                j = 1;
                            }
                        }
                        if (j == 0) {
                            $rootScope.idrolre.push(contentItem.Id);
                        }
                    }
                }
                if (contentItem.TableId != null) {
                    if ($rootScope.idrolre == null) {
                        $rootScope.idrolre = [];
                    }
                    var j = 0;
                    if ($rootScope.idrolre.length == 0) {
                        $rootScope.idrolre.push(contentItem.Id);
                    }
                    else {
                        for (var i = 0; i < $rootScope.idrolre.length; i++) {
                            if ($rootScope.idrolre[i] == contentItem.Id) {
                                $rootScope.idrolre.splice(i, 1);
                                j = 1;
                            }
                        }
                        if (j == 0) {
                            $rootScope.idrolre.push(contentItem.Id);
                        }
                    }
                }
                if (contentItem.RoleID != null) {
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
            //--------------------------------------------
            // -----------------Edit------------
            $scope.editClick1 = function (contentItem) {
                if ($rootScope.idrolre == null) {
                    $rootScope.idrolre = [];
                }
                var j = 0;
                if ($rootScope.idrolre.length == 0) {
                    $rootScope.idrolre.push(contentItem.id);
                }
                else {
                    for (var i = 0; i < $rootScope.idrolre.length; i++) {
                        if ($rootScope.idrolre[i] == contentItem.id) {
                            $rootScope.idrolre.splice(i, 1);
                            j = 1;
                        }
                    }
                    if (j == 0) {
                        $rootScope.idrolre.push(contentItem.id);
                    }
                }
                document.getElementById(contentItem.id).checked = true;
            }
            //Click bên role xem các quyền đã được chọn bên chức năng
            $scope.choseClick = function (contentItem) {
                if (contentItem.TableId != null) {
                    var id2 = contentItem.TableId;
                    if (contentItem.isAdd == true) {
                        document.getElementById('isAdd-' + id2).checked = true;
                        document.getElementById(id2).checked = true;
                    }
                    else {
                        document.getElementById('isAdd-' + id2).checked = false;
                    }
                    if (contentItem.isEdit == true) {
                        document.getElementById('isEdit-' + id2).checked = true;
                        document.getElementById(id2).checked = true;
                    }
                    else {
                        document.getElementById('isEdit-' + id2).checked = false;
                    }
                    if (contentItem.isDelete == true) {
                        document.getElementById('isDelete-' + id2).checked = true;
                        document.getElementById(id2).checked = true;
                    }
                    else {
                        document.getElementById('isDelete-' + id2).checked = false;
                    }
                    if (contentItem.isActive == true) {
                        document.getElementById('isActive-' + id2).checked = true;
                        document.getElementById(id2).checked = true;
                    }
                    else {
                        document.getElementById('isActive-' + id2).checked = false;
                    }
                    if (contentItem.isFilterButton == true) {
                        document.getElementById('isFilterButton-' + id2).checked = true;
                        document.getElementById(id2).checked = true;
                    }
                    else {
                        document.getElementById('isFilterButton-' + id2).checked = false;
                    }
                    if (contentItem.isExcel == true) {
                        document.getElementById('isExcel-' + id2).checked = true;
                        document.getElementById(id2).checked = true;
                    }
                    else {
                        document.getElementById('isExcel-' + id2).checked = false;
                    }
                    if (contentItem.isSubmit == true) {
                        document.getElementById('isSubmit-' + id2).checked = true;
                        document.getElementById(id2).checked = true;
                    }
                    else {
                        document.getElementById('isSubmit-' + id2).checked = false;
                    }
                    if (contentItem.isApproval == true) {
                        document.getElementById('isApproval-' + id2).checked = true;
                        document.getElementById(id2).checked = true;
                    }
                    else {
                        document.getElementById('isApproval-' + id2).checked = false;
                    }
                    if (contentItem.isDisApproval == true) {
                        document.getElementById('isDisApproval-' + id2).checked = true;
                        document.getElementById(id2).checked = true;
                    }
                    else {
                        document.getElementById('isDisApproval-' + id2).checked = false;
                    }
                    if (contentItem.isCopy == true) {
                        document.getElementById('isCopy-' + id2).checked = true;
                        document.getElementById(id2).checked = true;
                    }
                    else {
                        document.getElementById('isCopy-' + id2).checked = false;
                    }
                    if (contentItem.isIndex == true) {
                        document.getElementById('isIndex-' + id2).checked = true;
                        document.getElementById(id2).checked = true;
                    }
                    else {
                        document.getElementById('isIndex-' + id2).checked = false;
                    }
                    if (contentItem.isGet == true) {
                        document.getElementById('isGet-' + id2).checked = true;
                        document.getElementById(id2).checked = true;
                    }
                    else {
                        document.getElementById('isGet-' + id2).checked = false;
                    }
                }
                else {
                    $rootScope.checkRole = false;
                    if (document.getElementById(  contentItem.RoleID).checked == false) {
                        $scope.$parent.GetListData();
                        document.getElementById(contentItem.RoleID).checked = true;
                    }
                    var data = {
                        url: "/Sys_Table_Role_Action/Sys_Table_Role_ActionByIDRole?id=" + contentItem.RoleID
                    }
                    var list = myService.getDropdown(data);
                    list.then(function (res) {
                        var len = res.data.result.length;
                        for (var i = 0; i < len; i++) {
                            $scope.$parent.choseClick(res.data.result[i]);
                        }
                    }, function (res) {
                        $scope.msg = "Error";
                    })
                }
            }
            //-----------------Update------------------
            $scope.UpdateClick = function () {
                if ($rootScope.idrolre != null && $rootScope.idrolre1 != null) {
                    for (var i = 0; i < $rootScope.idrolre.length; i++) {
                        for (var j = 0; j < $rootScope.idrolre1.length; j++) {
                            var id2 = $rootScope.idrolre[i];
                            var isAdd, isEdit, isDelete, isActive, isFilterButton, isExcel, isSubmit, isApproval, isDisApproval, isCopy, isIndex, isGet;
                            if (document.getElementById('isAdd-' + id2).checked == true) {
                                isAdd = 1;
                            }
                            else {
                                isAdd = 0;
                            }
                            if (document.getElementById('isEdit-' + id2).checked == true) {
                                isEdit = 1;
                            }
                            else {
                                isEdit = 0;
                            }
                            if (document.getElementById('isDelete-' + id2).checked == true) {
                                isDelete = 1;
                            }
                            else {
                                isDelete = 0;
                            }
                            if (document.getElementById('isActive-' + id2).checked == true) {
                                isActive = 1;
                            }
                            else {
                                isActive = 0;
                            }
                            if (document.getElementById('isFilterButton-' + id2).checked == true) {
                                isFilterButton = 1;
                            }
                            else {
                                isFilterButton = 0;
                            }
                            if (document.getElementById('isExcel-' + id2).checked == true) {
                                isExcel = 1;
                            }
                            else {
                                isExcel = 0;
                            }
                            if (document.getElementById('isSubmit-' + id2).checked == true) {
                                isSubmit = 1;
                            }
                            else {
                                isSubmit = 0;
                            }
                            if (document.getElementById('isApproval-' + id2).checked == true) {
                                isApproval = 1;
                            }
                            else {
                                isApproval = 0;
                            }
                            if (document.getElementById('isDisApproval-' + id2).checked == true) {
                                isDisApproval = 1;
                            }
                            else {
                                isDisApproval = 0;
                            }
                            if (document.getElementById('isCopy-' + id2).checked == true) {
                                isCopy = 1;
                            }
                            else {
                                isCopy = 0;
                            }
                            if (document.getElementById('isIndex-' + id2).checked == true) {
                                isIndex = 1;
                            }
                            else {
                                isIndex = 0;
                            }
                            if (document.getElementById('isGet-' + id2).checked == true) {
                                isGet = 1;
                            }
                            else {
                                isGet = 0;
                            }
                            var id1 = $rootScope.idrolre1[j];
                            $scope.editData = { id1, id2, isAdd, isEdit, isDelete, isActive, isFilterButton, isExcel, isSubmit, isApproval, isDisApproval, isCopy, isIndex, isGet };
                            myService.UpdateData('/Sys_Table_Role_Action/n_AddSys_Table_Role_Actio', $scope.editData);
                        }
                    }
                    AppendToToastr(true, notification, setRoleSuccessful, 500, 5000);
                }
                else {
                    AppendToToastr(false, notification, mustBeChooseRoleAndFunction, 500, 5000);
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