﻿function BuildTable4(appName, controllerName, tableUrl) {
    app.controller(controllerName,
        function ($scope, myService, $filter, $rootScope, $window, $http, $compile) {
            // $scope.test = $filter('mm/dd/yyyy')("2018/01/01", 'yyyy/MM/dd');
            $scope.maxSize = 5; // Limit number for pagination display number.
            $scope.totalCount = 0; // Total number of items in all pages. initialize as a 
            $scope.pageIndex = 1; // Current page number. First page is 1.-->
            $scope.pageSizeSelected = 5;
            $scope.isShowFilter = false;
            $scope.typeEnds = [{ name: "Và", value: " and " }, { name: "Hoặc", value: " or " }];
            $scope.filterColumnsChoosed = [];
            $scope.Math = $window.Math;
            $scope.typeFilterA = [{ name: "Lớn hơn", value: " > '#' " }, { name: "Nhỏ hơn", value: " < '#' " }, { name: "Bằng", value: " = '#' " }, { name: "Khác", value: " != '#' " }];
            $scope.typeFilterB = [{ name: "Có chứa", value: " like '%#%' " }, { name: "Bằng", value: " = '#' " }, { name: "Không chứa", value: " != '#' " }];
            $scope.emailValid = /^[a-z]+[a-z0-9._]+@[a-z]+\.[a-z.]{2,5}$/;
            $scope.isShowTimeSSN = false;
            $scope.isTonghopcong = true;
            $scope.showtab13 = false;
            $scope.OrganizationUnitID = $rootScope.PhongBan;
            $scope.StaffID = $rootScope.giatri;
            $scope.isRequestOrApproval = false;

            //convert get datetime now
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

            today = mm + '/' + yyyy;

            $scope.FromMonth = today;


            $scope.CloseForm = function () {
                $.colorbox.close();

            }

            $scope.init = function () {
                var data = {
                    url: "/OrganizationUnit/GetOrganizationUnit?chon=1"
                }
                var list = myService.getData(data);
                list.then(function (res) {
                    $scope.getListAllOrganizationUnit = res.data.result;
                }, function (res) {
                    $scope.msg = "Error";
                    });
                if ($rootScope.PhongBan != null) {
                    ListEmployeeWhereOrganizationUnit($rootScope.PhongBan);
                }
                else {
                    ListEmployeeWhereOrganizationUnit(0); //lấy tất cả employee
                }
                $rootScope.giatri;
            }
            // checkbox
            $scope.checkAll = false;
            $scope.list = {
                StaffID: []
            }

            $scope.toggleCheck = function () {

                if (!$scope.checkAll) {

                    $scope.checkAll = true;
                    $scope.list.StaffID = $scope.employees.map(function (employee) {
                        return employee.StaffID;
                    });
                } else {
                    $scope.checkAll = false;
                    $scope.list.StaffID = [];
                }

            }


            //-----------lazy compile after get list
            //-----------boardcard from employee
            $scope.container = '';
            $scope.htmlResult = '';
            $scope.$on('bind-working', function (e, container, result) {
                $scope.container = container;
                $scope.htmlResult = result;

            });
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
                    $scope.dodai = $scope.Columns.length + 1;
                },
                    function (emp) {
                        AppendToToastr(false, notification, errorNotiFalse);
                    });
            }

            $scope.chageDate = function () {
                $rootScope.thangnam = $scope.FromMonth;
            }
            var userid = 0;
            var status = 0;
            $scope.GetListData = function () {
                var dt = Loading();
                if ($rootScope.thangnam != null) {
                    $scope.from = $rootScope.thangnam.split("/");
                    $scope.FromMonth = $rootScope.thangnam;
                }
                else {
                    $scope.from = $scope.FromMonth.split("/");
                }
                var data = {
                    pageIndex: $scope.pageIndex == null ? 1 : $scope.pageIndex,
                    pageSize: $scope.pageSizeSelected == null ? 5 : $scope.pageSizeSelected,
                    month: $scope.from[0],
                    year: $scope.from[1],
                    userid: userid,
                    status: status,
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
                    dt.finish();
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
            $scope.formatData = function (type, value, dataFormat) {
                if (type === 3) {
                    return FormatDate(value);
                }
                if (dataFormat === "N2") {
                    return value.toFixed(2);
                }
                if (dataFormat === "hh:mm:ss") {
                    var vitri;
                    var time;
                    if (value != null) {
                        vitri = 11;
                        time = value.slice(vitri);
                    }
                    return time;
                }
                if (dataFormat === "shortTime" && value != null) {
                    return value.toString().slice(0, -3);
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
                if ($rootScope.PhongBan != null) {
                    $scope.OrganizationUnitID = $rootScope.PhongBan;
                }
                if ($rootScope.giatri != null) {
                    $scope.StaffID = $rootScope.giatri;
                    stringFilter = $rootScope.giatri;
                }
                else if ($scope.StaffID > 0) {
                    stringFilter = $scope.StaffID;
                }
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

            $scope.showPopupImport = function () {
                ShowPopup($,
                    "#importExcel",
                    $scope.tableInfo.PopupWidth,
                    $scope.tableInfo.PopupHeight);
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
                var action = myService.deleteAction(obj.AutoID, $scope.tableInfo.id, $scope.tableInfo.DeleteUrl);
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


            $scope.OrganizationUnitID = null;
            $scope.StaffID = null;
            $scope.onOrganizationUnitChange = function () { //thay đổi phòng ban
                $rootScope.PhongBan = $scope.OrganizationUnitID;
                if ($scope.OrganizationUnitID != null) {
                    ListEmployeeWhereOrganizationUnit($scope.OrganizationUnitID);
                }
                else {
                    ListEmployeeWhereOrganizationUnit(0);
                }
            }
            $scope.onStaffChange = function () { //thay đổi nhân viên
                $scope.StaffID = angular.copy($scope.StaffID);
                $rootScope.giatri = $scope.StaffID;
            }




            //dropdown nhân viên theo phòng ban
            function ListEmployeeWhereOrganizationUnit(id) {
                var data = {
                    url: "/OrganizationUnit/EmployeeByOrganizationUnitID?id=" + id
                }
                var list = myService.getData(data);
                list.then(function (res) {
                    $scope.ListEmployeeWhereOrganizationUnit = res.data.result;

                }, function (res) {
                    $scope.msg = "Error";
                })
            }


            //import excel
            //$scope.upload = function () {
            //    var url = "/Timekeeping/Upload";
            //    var config = {
            //        headers: {
            //            "Content-Type": undefined,
            //        }
            //    };
            //    var formData = new $window.FormData();
            //    formData.append("file-0", $scope.files[0]);
            //    $http.post(url, formData, config).
            //        then(function (res) {
            //            AppendToToastr(res.data.result.IsSuccess, notification, res.data.result.Message, 500, 5000);
            //            $('.modal').modal('hide');
            //            $scope.getTableInfo();
            //            ListAllOrganizationUnit();
            //        }).catch(function (res) {
            //            AppendToToastr(false, notification, errorNotiFalse);

            //        });
            //};


            // -----------------Xóa--End------------
            //-------------------Excel--------------
            $scope.ExcelClick = function () {
                $scope.from = $scope.FromMonth.split("/");
                var filterString = $scope.getFilterValue();
                window.location = $scope.tableInfo.ExcelUrl + "?filterString=" + filterString + "&pageIndex=" + $scope.pageIndex + "&pageSize=" + $scope.pageSizeSelected + "&month=" + parseInt($scope.from[0]) + "&year=" + parseInt($scope.from[1]);

            }


        });
    //app.directive('convertToNumber',
    //    function () {
    //        return {
    //            require: 'ngModel',
    //            link: function (scope, element, attrs, ngModel) {
    //                ngModel.$parsers.push(function (val) {
    //                    return val !== null ? parseInt(val, 10) : null;
    //                });
    //                ngModel.$formatters.push(function (val) {
    //                    return val !== null ? '' + val : null;
    //                });
    //            }
    //        };
    //    });
    //app.directive("formatDate",
    //    function () {
    //        return {
    //            require: 'ngModel',
    //            link: function (scope, elem, attr, modelCtrl) {
    //                modelCtrl.$formatters.push(function (modelValue) {

    //                    return new Date(modelValue);
    //                });
    //            }
    //        }
    //    });

    //app.directive("myFiles", function ($parse) {
    //    return function linkFn(scope, elem, attrs) {
    //        elem.on("change", function (e) {
    //            scope.$eval(attrs.myFiles + "=$files", { $files: e.target.files });
    //            scope.$apply();
    //        })
    //    }
    //});
    //app.directive('checklistModel', ['$parse', '$compile', function ($parse, $compile) {
    //    // contains
    //    function contains(arr, item) {
    //        if (angular.isArray(arr)) {
    //            for (var i = 0; i < arr.length; i++) {
    //                if (angular.equals(arr[i], item)) {
    //                    return true;
    //                }
    //            }
    //        }
    //        return false;
    //    }

    //    // add
    //    function add(arr, item) {
    //        arr = angular.isArray(arr) ? arr : [];
    //        for (var i = 0; i < arr.length; i++) {
    //            if (angular.equals(arr[i], item)) {
    //                return arr;
    //            }
    //        }
    //        arr.push(item);
    //        return arr;
    //    }

    //    // remove
    //    function remove(arr, item) {
    //        if (angular.isArray(arr)) {
    //            for (var i = 0; i < arr.length; i++) {
    //                if (angular.equals(arr[i], item)) {
    //                    arr.splice(i, 1);
    //                    break;
    //                }
    //            }
    //        }
    //        return arr;
    //    }


    //    function postLinkFn(scope, elem, attrs) {
    //        // compile with `ng-model` pointing to `checked`
    //        $compile(elem)(scope);

    //        // getter / setter for original model
    //        var getter = $parse(attrs.checklistModel);
    //        var setter = getter.assign;

    //        // value added to list
    //        var value = $parse(attrs.checklistValue)(scope.$parent);

    //        // watch UI checked change
    //        scope.$watch('checked', function (newValue, oldValue) {
    //            if (newValue === oldValue) {
    //                return;
    //            }
    //            var current = getter(scope.$parent);
    //            if (newValue === true) {
    //                setter(scope.$parent, add(current, value));
    //            } else {
    //                setter(scope.$parent, remove(current, value));
    //            }
    //        });

    //        // watch original model change
    //        scope.$parent.$watch(attrs.checklistModel, function (newArr, oldArr) {
    //            scope.checked = contains(newArr, value);
    //        }, true);
    //    }

    //    return {
    //        restrict: 'A',
    //        priority: 1000,
    //        terminal: true,
    //        scope: true,
    //        compile: function (tElement, tAttrs) {
    //            if (tElement[0].tagName !== 'INPUT' || !tElement.attr('type', 'checkbox')) {
    //                throw 'checklist-model should be applied to `input[type="checkbox"]`.';
    //            }

    //            if (!tAttrs.checklistValue) {
    //                throw 'You should provide `checklist-value`.';
    //            }

    //            // exclude recursion
    //            tElement.removeAttr('checklist-model');

    //            // local scope var storing individual checkbox model
    //            tElement.attr('ng-model', 'checked');

    //            return postLinkFn;
    //        }
    //    };
    //}]);

}
