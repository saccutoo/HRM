
function BuildTable(appName, controllerName, tableUrl) {
    app.controller(controllerName, ['$scope', 'myService', '$filter',
        function ($scope, myService, $filter) {
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

            //-----------------List-------------

            $scope.getTableInfo = function () {

                var getData = myService.getTableInformation(tableUrl);

                getData.then(function (emp) {

                        console.log(emp);
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
                    pageSize: $scope.pageSizeSelected,
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
                        console.log($scope.filterColumnsChoosed);
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
                console.log(filterSelected);
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
               
                console.log(stringFilter);
                return stringFilter;
            };
            $scope.showFilterApplyButton = function () {
                var lstObj = $scope.filterColumnsChoosed;
                if (lstObj.length === 0 ) {
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

            $scope.updateAction = function (url) {
                if ($scope.EditForm.$valid) {
                    var updateAction = myService.UpdateData(url, $scope.editData);
                    updateAction.then(function (res) {
                            if (res.data.result.IsSuccess) {
                                $scope.GetListData();
                                $.colorbox.close();
                            }
                            AppendToToastr(res.data.result.IsSuccess, notification, res.data.result.Message, 500, 5000);
                        },
                        function (res) {
                            AppendToToastr(false, notification, errorNotiFalse);
                        });
                }
            }

            $scope.editClick = function (contentItem) {
                var edit = myService.getDataById(contentItem.Id, $scope.tableInfo.id, $scope.tableInfo.DataEditUrl);
                edit.then(function (emp) {
                  
                        $scope.editData = emp.data.result;
                        ShowPopup($,
                            "#EditContent",
                            $scope.tableInfo.PopupWidth,
                            $scope.tableInfo.PopupHeight);
                    },
                    function (emp) {
                        AppendToToastr(false, notification, errorNotiFalse);
                    });

            }


            // -----------------Edit--End----------
            //------------------Add---------------
            $scope.addClick = function () {
                $scope.editData = {};
                ShowPopup($,
                    "#AddContent",
                    $scope.tableInfo.PopupWidth,
                    $scope.tableInfo.PopupHeight);

            }
            $scope.AddAction = function (url) {
                console.log($scope.SaveForm.$valid);
                if ($scope.SaveForm.$valid) {
                    var updateAction = UpdateData(url, $scope.editData);
                    updateAction.then(function (res) {
                            if (res.data.result.IsSuccess) {
                                $scope.GetListData();
                                $.colorbox.close();

                            }
                            AppendToToastr(res.data.result.IsSuccess, notification, res.data.result.Message, 500, 5000);
                        },
                        function (res) {
                            AppendToToastr(false, notification, errorNotiFalse);
                        });
                }
            }

            //------------------Add-End--------------

            // -----------------Xóa------------

            $scope.deleteClick = function (obj) {
                BoostrapDialogConfirm(notification,
                    notificationDelete,
                    BootstrapDialog.TYPE_WARNING,
                    $scope.deleteActionClick,
                    obj);
            }
            $scope.deleteActionClick = function (obj) {
                var action = myService.deleteAction(obj.Id, $scope.tableInfo.id, $scope.tableInfo.DeleteUrl);

                action.then(function (res) {
                        if (res.data.result.IsSuccess) {
                            $scope.GetListData();
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
                window.location = $scope.tableInfo.ExcelUrl + "?filterString=" + filterString;
                console.log($scope.tableInfo.ExcelUrl + "?filterString=" + filterString);
            }
            //-------------------Excel-End----------
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
        function() {
            return {
                require: 'ngModel',
                link: function(scope, elem, attr, modelCtrl) {
                    modelCtrl.$formatters.push(function (modelValue) {
                        console.log(new Date(modelValue));
                        return new Date(modelValue);
                    });
                }
            }
        });
  
}
