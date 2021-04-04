function BuildTable(appName, controllerName, tableUrl, viewType) {
    app.controller(controllerName,
        function ($scope, myService, $filter, $rootScope) {
            // $scope.test = $filter('mm/dd/yyyy')("2018/01/01", 'yyyy/MM/dd');
            $scope.maxSize = 5; // Limit number for pagination display number
            $scope.totalCount = 0; // Total number of items in all pages. 
            $scope.pageIndex = 1; // Current page number. First page is 
            $scope.pageSizeSelected = 10;
            $rootScope.viewType = viewType; //viewType báo cáo theo t?ng lo
            $rootScope.viewFilter = 1; //viewType báo cáo theo t?ng lo
            $scope.isShowFilter = false;
            $scope.typeEnds = [{ name: "Và", value: " and " }, { name: "Ho?c", value: " or " }];
            $scope.filterColumnsChoosed = [];
            $scope.typeFilterA = [{ name: "L?n hon", value: " > '#' " }, { name: "Nh? hon", value: " < '#' " }, { name: "B?ng", value: " = '#' " }, { name: "Khác", value: " != '#' " }];
            $scope.typeFilterB = [{ name: "Có ch?a", value: " like '%#%' " }, { name: "B?ng", value: " = '#' " }, { name: "Không ch?a", value: " != '#' " }];
            $rootScope.GetColumnWhereCondition = 1; //cot duoc hien thi theo quy/thang PerformanceReport
            $scope.viewONN = true;
            var reportType = "4";
            if (!isShowGeneral) {
                reportType = "2";
            }

            $rootScope.data = {
                filter5: new Date().getFullYear().toString(),
                filter1: '1',
                filter2: '12',
                filter3: '1',
                filter4: '4',
                filter6: '3',
                filter7: reportType,
                filter8: '1',
                filter9: '',
                filter10: '',
                filter11: '',
                filter12: '',

            }; //d? li?u truy?n vào
            //show c?t theo quý
            $rootScope.getColumnQuater = function () {
                $('.Achievement').addClass("hide-element");
                $('.TotalMonth').addClass("hide-element");
                $('.TotalQuarter').removeClass("hide-element");
                for (var i = 1; i <= 12; i++) {
                    $('.L' + i + '').addClass("hide-element");
                }
                for (var i = 1; i <= 4; i++) {
                    $('.Q' + i + '').removeClass("hide-element");
                }

            }

            $scope.ChangeValueQuarter = function () {
                switch ($rootScope.data.filter3) {
                    case "1":
                        $rootScope.data.filter1 = "1";
                        break;
                    case "2":
                        $rootScope.data.filter1 = "3";
                        break;
                    case "3":
                        $rootScope.data.filter1 = "6";
                        break;
                    case "4":
                        $rootScope.data.filter1 = "9";
                        break;
                }
                switch ($rootScope.data.filter4) {
                    case "1":
                        $rootScope.data.filter2 = "3";
                        break;
                    case "2":
                        $rootScope.data.filter2 = "6";
                        break;
                    case "3":
                        $rootScope.data.filter2 = "9";
                        break;
                    case "4":
                        $rootScope.data.filter2 = "12";
                        break;
                }
            }
            $scope.ChangeValueMonth = function () {
                switch ($rootScope.data.filter1) {
                    case "1":
                    case "2":
                    case "3":
                        $rootScope.data.filter3 = "1";
                        break;
                    case "4":
                    case "5":
                    case "6":
                        $rootScope.data.filter3 = "2";
                        break;
                    case "7":
                    case "8":
                    case "9":
                        $rootScope.data.filter3 = "3";
                        break;
                    case "10":
                    case "11":
                    case "12":
                        $rootScope.data.filter3 = "4";
                        break;
                }
                switch ($rootScope.data.filter2) {
                    case "1":
                    case "2":
                    case "3":
                        $rootScope.data.filter4 = "1";
                        break;
                    case "4":
                    case "5":
                    case "6":
                        $rootScope.data.filter4 = "2";
                        break;
                    case "7":
                    case "8":
                    case "9":
                        $rootScope.data.filter4 = "3";
                        break;
                    case "10":
                    case "11":
                    case "12":
                        $rootScope.data.filter4 = "4";
                        break;
                }
            }

            var res = $rootScope.data;
            //h?y b? l?c
            $scope.ResetFilterString = function () {
                $scope.OrganizationUnitName = null;
                $scope.StaffName = null;
                //$rootScope.data = {
                //    filter5: '2018',
                //    filter1: '1',
                //    filter2: '12',
                //    filter3: '1',
                //    filter4: '4',
                //    filter6: '1',
                //    filter7: '1',
                //    filter8: '1',
                //}; //d? li?u truy?n vào
            }

            //show c?t theo tháng
            $rootScope.getColumnMonth = function () {
                $('.Achievement').addClass("hide-element");
                $('.TotalQuarter').addClass("hide-element");
                $('.TotalMonth').removeClass("hide-element");
                for (var i = 1; i <= 4; i++) {
                    $('.Q' + i + '').addClass("hide-element");
                }
                for (var i = 1; i <= 12; i++) {
                    $('.L' + i + '').removeClass("hide-element");
                }
            }
            $scope.CloseForm = function () {
                $.colorbox.close();
            }
            //show t? tháng d?n tháng, t? quý d?n quý
            $rootScope.showColumn = function () {
                if (parseInt($rootScope.data.filter1) > parseInt($rootScope.data.filter2)) {
                    if (parseInt($rootScope.data.filter1) == 12)
                        $rootScope.data.filter2 = $rootScope.data.filter1;
                    else {
                        $rootScope.data.filter2 = ((parseInt($rootScope.data.filter1)) + 1).toString();
                    }
                }
                if (parseInt($rootScope.data.filter3) > parseInt($rootScope.data.filter4)) {
                    if (parseInt($rootScope.data.filter3) == 4)
                        $rootScope.data.filter4 = $rootScope.data.filter3;
                    else {
                        $rootScope.data.filter4 = ((parseInt($rootScope.data.filter3)) + 1).toString();

                    }
                }
                if ($rootScope.data.filter8 == 1) {
                    for (var i = 1; i <= 12; i++) {
                        if (i >= $rootScope.data.filter1 && i <= $rootScope.data.filter2) {
                            $('.L' + i + '').removeClass("hide-element");
                        }
                        else {
                            $('.L' + i + '').addClass("hide-element");
                        }
                    }

                }
                else {
                    for (var i = 1; i <= 4; i++) {
                        if (i >= $rootScope.data.filter3 && i <= $rootScope.data.filter4) {
                            $('.Q' + i + '').removeClass("hide-element");
                        }
                        else {
                            $('.Q' + i + '').addClass("hide-element");
                        }
                    }
                }
            }

            //Call và truy?n d? li?u sang builtable g?i l?i d? li?u
            $rootScope.childmethod = function () {
                var filter = $scope.getFilterValue();
                $rootScope.$broadcast("CallParentMethod", { res, filter });
            }

            //Radio T?ng h?p , chi ti?t
            $rootScope.ShowAllOrDetail = function () {
                if ($rootScope.data.filter7 == 1) {
                    $('.Staff').hide();
                    $('.OrganizationUnitID').show();
                    $('.Unit').show();
                }
                else if ($rootScope.data.filter7 == 3) {
                    $('.Staff').hide();
                    $('.OrganizationUnitID').hide();
                    $('.Unit').show();
                }
                else if ($rootScope.data.filter7 == 4) {
                    $('.Staff').hide();
                    $('.OrganizationUnitID').hide();
                    $('.Unit').hide();
                }
                else {
                    $('.Staff').show();
                    $('.OrganizationUnitID').show();
                    $('.Unit').show();
                }
            }

            //-----------------List-----------     
            $scope.getTableInfo = function () {
                $scope.Listyears = [];
                var currentYear = new Date().getFullYear();
                var startYear = 2015;
                while (startYear <= currentYear) {
                    $scope.Listyears.push(startYear);
                    startYear++;
                }

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
                    viewType: viewType,
                    filter1: $rootScope.data.filter1,
                    filter2: $rootScope.data.filter2,
                    filter3: $rootScope.data.filter3,
                    filter4: $rootScope.data.filter4,
                    filter5: $rootScope.data.filter5,
                    filter6: $rootScope.data.filter6,
                    filter7: $rootScope.data.filter7,
                    filter8: $rootScope.data.filter8,
                    filter9: $rootScope.data.filter9,
                    filter10: $rootScope.data.filter10,
                    filter11: $rootScope.data.filter11,
                    filter12: $rootScope.data.filter12,
                    filter: $scope.getFilterValue(),
                }
                var getDataTbl = myService.GetTableData(data, tableUrl);
                getDataTbl.then(function (emp) {
                    $scope.employees = emp.data.employees;
                    $scope.totalCount = emp.data.totalCount;
                    $scope.lstTotal = emp.data.lstTotal;
                    $scope.loginID = emp.data.staffID;
                    $scope.SetTotalByColumns = function (totalName) {
                        if (!angular.isUndefined(totalName) && totalName !== null) {
                            return $scope.lstTotal[totalName];
                        }
                        return "-";
                    }
                    setTimeout(function () {
                        $rootScope.ShowAllOrDetail();
                    }, 200);
                    HiddenLoader();
                },
                    function (emp) {
                        AppendToToastr(false, notification, dataNotFound);
                    });

            }


            //$scope.changePageSize = function () {
            //    $scope.pageIndex = 1;
            //    $rootScope.childmethod();
            //    $rootScope.showColumn();
            //};

            //$scope.pageChanged = function () {
            //    $rootScope.childmethod();
            //    $rootScope.showColumn();
            //}
            $scope.formatData = function (type, value, dataFormat) {
                if (type === 3) {
                    return FormatDate(value);
                }
                else if (dataFormat === "N2" && value != null) {

                    return value.toFixed(2);
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

                if ($scope.OrganizationUnitName != null) {
                    stringFilter = "";
                    if ($scope.OrganizationUnitName != "") {
                        stringFilter += "p.OrganizationUnitName like N'!!" + $scope.OrganizationUnitName + "!!'";
                    }
                    console.log($scope.OrganizationUnitName);
                }
                if ($scope.StaffName != null && $rootScope.data.filter7 != 1) {
                    stringFilter = "";
                    if ($scope.OrganizationUnitName != null) {
                        stringFilter += "p.OrganizationUnitName like N'!!" + $scope.OrganizationUnitName + "!!'";
                    }
                    if ($scope.StaffName != "" && $scope.OrganizationUnitName != null) {
                        stringFilter += " and ";
                    }
                    if ($scope.StaffName != "") {
                        stringFilter += "p.StaffName like N'!!" + $scope.StaffName + "!!'";
                    }

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

            //-----------------Filter-End--------


            //-------------------Excel--------------
            $scope.ExcelClick = function () {
                var filter = $scope.getFilterValue();
                window.location =
                    $scope.tableInfo.ExcelUrl
                    + "?pageIndex=" + $scope.pageIndex
                    + "&pageSize=" + $scope.pageSizeSelected
                    + "&viewType=" + viewType
                    + "&filter1=" + $rootScope.data.filter1
                    + "&filter2=" + $rootScope.data.filter2
                    + "&filter3=" + $rootScope.data.filter3
                    + "&filter4=" + $rootScope.data.filter4
                    + "&filter5=" + $rootScope.data.filter5
                    + "&filter6=" + $rootScope.data.filter6
                    + "&filter7=" + $rootScope.data.filter7
                    + "&filter8=" + $rootScope.data.filter8
                    + "&filter9=" + $rootScope.data.filter9
                    + "&filter10=" + $rootScope.data.filter10
                    + "&filter11=" + $rootScope.data.filter11
                    + "&filter12=" + $rootScope.data.filter12
                    + "&filter=" + filter
                ;

            }

            //-------------------Excel-End----------


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


}
