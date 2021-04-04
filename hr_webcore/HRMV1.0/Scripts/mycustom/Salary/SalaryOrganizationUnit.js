function BuildTable2(appName, controllerName, tableUrl) {
    app.controller(controllerName,
        function ($scope, myService, $filter, $rootScope, $window, $http, $compile) {        
            // $scope.test = $filter('mm/dd/yyyy')("2018/01/01", 'yyyy/MM/dd');
            $scope.maxSize = 5; // Limit number for pagination display number.
            $scope.totalCount = 0; // Total number of items in all pages
            $scope.pageIndex = 1; // Current page number. First page is 1.-->
            $scope.pageSizeSelected = 5;
            $scope.isShowFilter = false; $scope.typeEnds = [{ name: "Và", nameEN: "And", value: " and " }, { name: "Hoặc", nameEN: "Or", value: " or " }];
            $scope.typeFilterA = [{ name: ">", nameEN: ">", value: " > '#' " }, { name: "<", nameEN: "<", value: " < '#' " }, { name: "=", nameEN: "=", value: " = '#' " }, { name: "Khác", nameEN: "Not equal", value: " != '#' " }];
            $scope.typeFilterB = [{ name: "Có chứa", nameEN: "Contains", value: " like '%#%' " }, { name: "Bằng", nameEN: "Is", value: " = '#' " }, { name: "Không chứa", nameEN: "Do not contains", value: " != '#' " }];
            $scope.filterColumnsChoosed = [];
            $scope.Math = $window.Math;
            $scope.emailValid = /^[a-z]+[a-z0-9._]+@[a-z]+\.[a-z.]{2,5}$/;
            $scope.tab2Data = '';
            $scope.flagShow = 'tabs2';
            $rootScope.DoNotLoad = 1;
            Number.prototype.format = function (n, x, s, c) {
                var re = '\\d(?=(\\d{' + (x || 3) + '})+' + (n > 0 ? '\\D' : '$') + ')',
                    num = this.toFixed(Math.max(0, ~~n));

                return (c ? num.replace('.', c) : num).replace(new RegExp(re, 'g'), '$&' + (s || ','));
            };
       
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
            $scope.ToMonth = today;

            $scope.CloseForm = function () {
                $.colorbox.close();

            }
          
            //----------------LoadTab-----------
            $scope.tabs = {
                tabs1: {
                    url: '/Salary/Index',
                    container: $("#tabs1"),
                },
                tabs2: {
                    url: '/SalaryDetail/Index',
                    container: $("#tabs2"),

                },
                tabs3: {
                    url: '/SalaryOrganizationUnit/Index',
                    container: $("#tabs3")

                }
            };

         
            $scope.init = function () {
                ListEmployeeWhereOrganizationUnit(0); //lấy tất cả employee
                var data = {
                    url: "/OrganizationUnit/GetOrganizationUnit?chon=1"
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getListAllOrganizationUnit = res.data.result;
                }, function (res) {
                    $scope.msg = "Error";
                })
            }
            // checkbox
            $scope.checkAll = false;

            $scope.list = {
                employees: []
            };

            $scope.toggleCheck = function () {
                if (!$scope.checkAll) {
                    $scope.checkAll = true;
                    $scope.list.employees = $scope.employees.map(function (employee) {
                        return employee;
                    });
                } else {
                    $scope.checkAll = false;
                    $scope.list.employees = [];
                }

            }
            //-----------------List-----------     
            $scope.getTableInfo = function (DoNotLoad) {
                var getData = myService.getTableInformation(tableUrl);
                getData.then(function (emp) {
                    $scope.tableInfo = emp.data.result;
                    $scope.lstPageSize = $scope.tableInfo.PageSizeList.split(',');
                    $scope.pageSizeSelected = $scope.tableInfo.PageSize;
                    $scope.GetAddPermission(emp.data.result.id, DoNotLoad);
                },
                    function (emp) {
                        AppendToToastr(false, notification, errorNotiFalse);
                    });
            }

            $scope.GetAddPermission = function (idTable, DoNotLoad) {
                var tblAction = myService.getTableAction(idTable);
                tblAction.then(function (emp) {
                    $scope.tablePermission = emp.data.result;
                    $scope.getColumns(DoNotLoad);
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

            $scope.getTableInfo($rootScope.DoNotLoad);

            $scope.getColumns = function (DoNotLoad) {
                var getData = myService.GetColumns(tableUrl);
                getData.then(function (emp) {
                    $scope.Columns = emp.data.result;
                    if (DoNotLoad == undefined) {
                        $scope.GetListData();
                    }
                },
                    function (emp) {
                        AppendToToastr(false, notification, errorNotiFalse);
                    });
            }


            $scope.GetListData = function () {
                var dt = Loading();
                $scope.from = $scope.FromMonth.split("/");
                var data = {
                    pageIndex: $scope.pageIndex,
                    pageSize: $scope.pageSizeSelected,
                    month: $scope.from[0],
                    year: $scope.from[1],
                    filter: $scope.getFilterValue()
                }
                var getDataTbl = myService.GetTableData(data, tableUrl);
                getDataTbl.then(function (emp) {
                    $scope.employees = emp.data.employees;
                    console.log($scope.employees);
                    $scope.totalCount = emp.data.totalCount;
                    $scope.lstTotal = emp.data.lstTotal;
                    $scope.loginID = emp.data.staffID;
                    $scope.SetTotalByColumns = function (totalName, dataFomat) {
                        if (!angular.isUndefined(totalName) &&
                            totalName !== null &&
                            $scope.lstTotal != undefined) {
                            if ($scope.lstTotal[totalName] != undefined &&
                                $scope.lstTotal[totalName] != null) {
                                return parseFloat($scope.lstTotal[totalName]).format(dataFomat, 3, ',', '.');
                            }
                            return $scope.lstTotal[totalName];
                        }
                        return "";
                    }
                   

                    dt.finish();
                },
                    function (emp) {
                        AppendToToastr(false, notification, dataNotFound);
                    });

            }

            //This method is calling from pagination numbe
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

            function convertDateTimeDefaultToString(datetime) {
                var months = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
                var newMonth = months[datetime.getMonth()];
                var x;
                switch (newMonth) {
                    case "January":
                        x = '01';
                        break;
                    case "February":
                        x = '02';
                        break;
                    case "March":
                        x = '03';
                        break;
                    case "April":
                        x = '04';
                        break;
                    case "May":
                        x = '05';
                        break;
                    case "June":
                        x = '06';
                        break;
                    case "July":
                        x = '07';
                        break;
                    case "August":
                        x = '08';
                        break;
                    case "September":
                        x = '09';
                        break;
                    case "October":
                        x = '10';
                        break;
                    case "November":
                        x = '11';
                        break;
                    case "December":
                        x = '12';
                        break;
                    default:
                    // code block
                }
                var getDate = datetime.getDate();
                var getMonth = x;
                var getFullYear = datetime.getFullYear();
                var fullDateTime = getFullYear + "-" + ("0" + getDate).slice(-2) + "-" + getMonth;
                return fullDateTime;
            }

            $scope.getFilterValue = function () {
                var lstObj = $scope.filterColumnsChoosed;
                var stringFilter = "";
                var len = lstObj.length - 1;
                for (var key in lstObj) {
                    var obj = lstObj[key];
                    var tmpFilter = obj.filterSelected.ColumnName + obj.typeFilterSelected.value.replace("#", obj.filterSelected.Type === 3 ? $filter('date')(new Date(obj.textValue), 'yyyy/MM/dd') : obj.textValue) + (parseInt(String(key)) === len ? "" : obj.typeEndsSeleted.value);

                    stringFilter += tmpFilter;
                }
                var array1 = $scope.FromMonth.split("/").map(Number);
                array1.unshift(1);
                var array2 = $scope.ToMonth.split("/").map(Number);
                array2.unshift(1);
                var fromDate = new Date(array1);
                var toDate = new Date(array2);
                var string1 = convertDateTimeDefaultToString(fromDate);
                var string2 = convertDateTimeDefaultToString(toDate);
                if ($scope.FromMonth != null && $scope.ToMonth != null && $scope.tableInfo.id != 25) {
                    stringFilter += " [TimeConvert] BETWEEN '" + string1 + "' AND '" + string2 + "'";
                }
                if ($scope.OrganizationUnitID != null && $scope.tableInfo.id != 25) {
                    stringFilter = "";
                    stringFilter += " OrganizationUnitID = " + $scope.OrganizationUnitID;
                    if ($scope.FromMonth != null && $scope.ToMonth != null && $scope.tableInfo.id != 25) {
                        stringFilter += " and ";
                        var array1 = $scope.FromMonth.split("/");
                        var array2 = $scope.ToMonth.split("/");
                        stringFilter += " [TimeConvert] BETWEEN '" + string1 + "' AND '" + string2 + "'";
                    }

                }
                if ($scope.StaffID != null) {
                    stringFilter = "";
                    stringFilter += " StaffID = " + $scope.StaffID;
                    if ($scope.FromMonth != null && $scope.ToMonth != null && $scope.tableInfo.id != 25) {
                        stringFilter += " and ";
                        var array1 = $scope.FromMonth.split("/");
                        var array2 = $scope.ToMonth.split("/");
                        stringFilter += " [TimeConvert] BETWEEN '" + string1 + "' AND '" + string2 + "'";
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

            //-----------------Filter-End-------

            // -----------------Edit------------

            $scope.SaveAction = function (url, form) {
                if (form.$valid) {
                    $scope.editData.AdvancePayment = Number($scope.editData.AdvancePayment.toString().replace(/[^0-9.-]+/g, ""));
                    $scope.editData.BDOAllowances = Number($scope.editData.BDOAllowances.toString().replace(/[^0-9.-]+/g, ""));
                    $scope.editData.OtherBonus = Number($scope.editData.OtherBonus.toString().replace(/[^0-9.-]+/g, ""));
                    $scope.editData.Nontaxableincome = Number($scope.editData.Nontaxableincome.toString().replace(/[^0-9.-]+/g, ""));
                    $scope.editData.OtherReduction = Number($scope.editData.OtherReduction.toString().replace(/[^0-9.-]+/g, ""));
                    $scope.editData.OtherAllowances = Number($scope.editData.OtherAllowances.toString().replace(/[^0-9.-]+/g, ""));
                    $scope.editData.Margincompensation = Number($scope.editData.Margincompensation.toString().replace(/[^0-9.-]+/g, ""));
                    $scope.editData.Decemberbonus = Number($scope.editData.Decemberbonus.toString().replace(/[^0-9.-]+/g, ""));
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
                var edit = myService.getDataById(contentItem.SalaryID, $scope.tableInfo.id, $scope.tableInfo.DataEditUrl);
                edit.then(function (emp) {
                    $scope.editData = emp.data.result;
                    ShowPopup($,
                        ".EditPayBill",
                        $scope.tableInfo.PopupWidth,
                        $scope.tableInfo.PopupHeight);
                },
                    function (emp) {
                        AppendToToastr(false, notification, errorNotiFalse);
                    });

            }


            // -----------------Edit--End----------

            $scope.showPopupImport = function () {
                ShowPopup($,
                    "#importExcel",
                    $scope.tableInfo.PopupWidth,
                    $scope.tableInfo.PopupHeight);
            }





            $scope.onOrganizationUnitChange = function () {
                if ($scope.OrganizationUnitID != null) {
                    ListEmployeeWhereOrganizationUnit($scope.OrganizationUnitID);
                }
                else {
                    ListEmployeeWhereOrganizationUnit(0);
                }
            }
            $scope.onStaffChange = function () {
                $scope.StaffID = angular.copy($scope.StaffID);
            }
            $scope.refesh = function () {
                $scope.checkboxAll = false;
                $scope.checkAll = false;
                $scope.list.StaffID = [];
                $scope.list.employees = [];
                $scope.AllStaff = false;
            }


            //dropdown nhân viên theo phòng ban

            function ListEmployeeWhereOrganizationUnit(id) {

                var data = {
                    url: "/OrganizationUnit/EmployeeByOrganizationUnitID?id=" + id
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.ListEmployeeWhereOrganizationUnit = res.data.result;
                }, function (res) {
                    $scope.msg = "Error";
                })
            }

            //function chốt phiếu lương
            $scope.AllStaff = false;
    


            //import excel
            $scope.upload = function () {
                //var dt = Loading();
                var url = "/Salary/Upload";
                var config = {
                    headers: {
                        "Content-Type": undefined,
                    }
                };
                var formData = new $window.FormData();
                formData.append("file-0", $scope.files[0]);
                $http.post(url, formData, config).
                    then(function (res) {
                        AppendToToastr(res.data.result.IsSuccess, notification, res.data.result.Message, 500, 5000);
                        $('.modal').modal('hide');
                        $scope.getTableInfo();

                        //dt = finish();
                    }, function (res) {
                        AppendToToastr(false, notification, errorNotiFalse);

                    });

            };

            // Tạo phiếu lương
            $scope.CreatePayslip = function () {
                if ($scope.list.employees != null) {
                    $scope.list.StaffID = $scope.list.employees.map(a => a.StaffID);
                    $scope.list.WPID = $scope.list.employees.map(a => a.WPID);

                    console.log($scope.list.WPID);
                }
                var dt = Loading();
                $scope.from = $scope.FromMonth.split("/");
                if ($scope.list.StaffID.length > 0 || $scope.AllStaff == true) {//đã chọn chốt
                    var ischeckAll = false;
                    if ($scope.AllStaff == true) {
                        ischeckAll = true;
                    }
                    var data = {
                        url: "/Salary/CreatePayslip",
                        listID: $scope.list.StaffID,
                        listWPID: $scope.list.WPID,
                        month: $scope.from[0],
                        year: $scope.from[1],
                        isCheckAll: ischeckAll
                    }
                    var list = myService.CreatePayslipPost(data);
                    list.then(function (res) {
                        $scope.refesh();
                        $scope.GetListData();
                        dt.finish();
                        AppendToToastr(res.data.result.IsSuccess, notification, res.data.result.Message, 500, 5000);

                    },
                        function (res) {
                            AppendToToastr(false, notification, errorNotiFalse);
                        });

                }
                else {
                    dt.finish();
                    AppendToToastr(false, notification, selectCheckBoxPayslip, 500, 5000);
                }

            };

          


            // -----------------Xóa--End------------
            //-------------------Excel------------

            $scope.ExcelClick = function () {
                var filterString = $scope.getFilterValue();
                window.location = $scope.tableInfo.ExcelUrl + "?filterString=" + filterString + "&pageIndex=" + $scope.pageIndex + "&pageSize=" + $scope.pageSizeSelected + "&month=" + $scope.from[0] + "&year=" + $scope.from[1];

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
    app.directive("formatDate", function () {
        return {
            require: 'ngModel',
            link: function (scope, elem, attr, modelCtrl) {
                modelCtrl.$formatters.push(function (modelValue) {

                    return new Date(modelValue);
                });
            }
        }
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
    app.directive("myFiles", function ($parse) {
        return function linkFn(scope, elem, attrs) {
            elem.on("change",
                function (e) {
                    scope.$eval(attrs.myFiles + "=$files", { $files: e.target.files });
                    scope.$apply();
                });
        }
    });
    app.directive('checklistModel', ['$parse', '$compile', function ($parse, $compile) {
        // contains
        function contains(arr, item) {
            if (angular.isArray(arr)) {
                for (var i = 0; i < arr.length; i++) {
                    if (angular.equals(arr[i], item)) {
                        return true;
                    }
                }
            }
            return false;
        }

        // add
        function add(arr, item) {
            arr = angular.isArray(arr) ? arr : [];
            for (var i = 0; i < arr.length; i++) {
                if (angular.equals(arr[i], item)) {
                    return arr;
                }
            }
            arr.push(item);
            return arr;
        }

        // remove
        function remove(arr, item) {
            if (angular.isArray(arr)) {
                for (var i = 0; i < arr.length; i++) {
                    if (angular.equals(arr[i], item)) {
                        arr.splice(i, 1);
                        break;
                    }
                }
            }
            return arr;
        }


        function postLinkFn(scope, elem, attrs) {
            // compile with `ng-model` pointing to `checked`
            $compile(elem)(scope);

            // getter / setter for original model
            var getter = $parse(attrs.checklistModel);
            var setter = getter.assign;

            // value added to list
            var value = $parse(attrs.checklistValue)(scope.$parent);

            // watch UI checked change
            scope.$watch('checked', function (newValue, oldValue) {
                if (newValue === oldValue) {
                    return;
                }
                var current = getter(scope.$parent);
                if (newValue === true) {
                    setter(scope.$parent, add(current, value));
                } else {
                    setter(scope.$parent, remove(current, value));
                }
            });

            // watch original model change
            scope.$parent.$watch(attrs.checklistModel, function (newArr, oldArr) {
                scope.checked = contains(newArr, value);
            }, true);
        }

        return {
            restrict: 'A',
            priority: 1000,
            terminal: true,
            scope: true,
            compile: function (tElement, tAttrs) {
                if (tElement[0].tagName !== 'INPUT' || !tElement.attr('type', 'checkbox')) {
                    throw 'checklist-model should be applied to `input[type="checkbox"]`.';
                }

                if (!tAttrs.checklistValue) {
                    throw 'You should provide `checklist-value`.';
                }

                // exclude recursion
                tElement.removeAttr('checklist-model');

                // local scope var storing individual checkbox model
                tElement.attr('ng-model', 'checked');

                return postLinkFn;
            }
        };
    }]);
    app.directive('compile', ['$compile', function ($compile) {

        return function (scope, element, attrs) {
            scope.$watch(
                function (scope) {
                    // watch the 'compile' expression for changes
                    return scope.$eval(attrs.compile);
                },
                function (value) {
                    // when the 'compile' expression changes
                    // assign it into the current DOM
                    element.html(value);

                    // compile the new DOM and link it to the current
                    // scope.
                    // NOTE: we only compile .childNodes so that
                    // we don't get into infinite loop compiling ourselves
                    $compile(element.contents())(scope);
                }
            );
        };
    }]);
    app.directive('angularCurrency', [function () {
        'use strict';

        return {
            'require': '?ngModel',
            'restrict': 'A',
            'scope': {
                angularCurrency: '=',
                variableOptions: '='
            },
            'compile': compile
        };

        function compile(tElem, tAttrs) {
            var isInputText = tElem.is('input:text');

            return function (scope, elem, attrs, controller) {
                var updateElement = function (newVal) {
                    elem.autoNumeric('set', newVal);
                };

                elem.autoNumeric('init', scope.angularCurrency);
                if (scope.variableOptions === true) {
                    scope.$watch('angularCurrency', function (newValue) {
                        elem.autoNumeric('update', newValue);
                    });
                }

                if (controller && isInputText) {
                    scope.$watch(tAttrs.ngModel, function () {
                        controller.$render();
                    });

                    controller.$render = function () {
                        updateElement(controller.$viewValue);
                    };

                    elem.on('keyup', function () {
                        scope.$applyAsync(function () {
                            controller.$setViewValue(elem.autoNumeric('get'));
                        });
                    });
                    elem.on('change', function () {
                        scope.$applyAsync(function () {
                            controller.$setViewValue(elem.autoNumeric('get'));
                        });
                    });
                } else {
                    if (isInputText) {
                        attrs.$observe('value', function (val) {
                            updateElement(val);
                        });
                    }
                }
            };
        }
    }]);
}
