function BuildTable2(appName, controllerName, tableUrl) {
    app.controller(controllerName,
        function ($scope, myService, $filter, $rootScope, $window, $http, $compile,$timeout) {
            // $scope.test = $filter('mm/dd/yyyy')("2018/01/01", 'yyyy/MM/dd');
            $scope.maxSize = 5; // Limit number for pagination display number.
            $scope.totalCount = 0; // Total number of items in all pages. initialize as a zero1
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
            $scope.filterStatus = "  "
            $scope.stringfilterduyet = "";
            $scope.isShow = true;
            $scope.checkbox = true;
            $scope.peoplenote;
            $scope.showtab13 = true;
            $scope.editData = {};
            $scope.OrganizationUnitID = $rootScope.PhongBan;
            $scope.StaffID = $rootScope.giatri;
            $scope.isRequestOrApproval = true;
            $scope.employeepopup = [];

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

            today = mm + '/' + yyyy;;

            $scope.FromMonth = today;

            $scope.userid1 = 0;

            $scope.CloseForm = function () {
                $.colorbox.close();

            }
            $scope.wpTableData = {}
            $scope.init = function () {
                //lấy list phòng ban
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
                ListStatus();
            }
            // checkbox
            $scope.checkAll = false;
            $scope.list = {
                AutoID: []
            }

            $scope.toggleCheck = function () {

                if (!$scope.checkAll) {
                    $scope.checkAll = true;
                    $scope.list.AutoID = $scope.employees.map(function (employee) {
                        return employee.AutoID;
                    });
                } else {
                    $scope.checkAll = false;
                    $scope.list.AutoID = [];
                }

            }

            //model được truyền ra từ buildtable
            $scope.HRWDSTableData = {}
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
                    emp.data.result.splice(19, 1);
                    $scope.Columns = emp.data.result;
                    $scope.GetListData();
                    $scope.dodai = $scope.Columns.length + 1;
                },
                    function (emp) {
                        AppendToToastr(false, notification, errorNotiFalse);
                    });
            }
            //lấy list trạng thái duyệt
            function ListStatus() {
                var data = {
                    url: "/Common/GetDataByGloballistnotTree?parentid=" + 85
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getListStatus = res.data.result;

                }, function (res) {
                    $scope.msg = "Error";
                })
            }
            $scope.chageDate = function () {

                $rootScope.thangnam = $scope.FromMonth;
            }
            //lấy dữ liệu duyệt bổ sung công
            $scope.GetListData = function () {
                if ($rootScope.giatri != null) {
                    $scope.StaffID = $rootScope.giatri;
                    $scope.userid1 = $rootScope.giatri;
                }
                else if ($scope.StaffID > 0) {
                    $scope.userid1 = $scope.StaffID;
                }
                var status = 0;
                if ($scope.Data.statusfind != "" && $scope.Data.statusfind != null && $scope.checkbox == false) {
                    status = 1;
                }
                if ($scope.checkbox == false) {
                    status = 2;
                }
                if ($scope.stringfilterduyet != "") {
                    status = 3;
                }
                var dt = Loading();
                if ($rootScope.thangnam != null) {
                    $scope.from = $rootScope.thangnam.split("/");
                    $scope.FromMonth = $rootScope.thangnam;
                }
                else {
                    $scope.from = $scope.FromMonth.split("/");
                    $rootScope.thangnam = $scope.FromMonth;
                }
                var data = {
                    pageIndex: $scope.pageIndex == null ? 1 : $scope.pageIndex,
                    pageSize: $scope.pageSizeSelected == null ? 5 : $scope.pageSizeSelected,
                    month: $scope.from[0],
                    year: $scope.from[1],
                    userid: $scope.userid1,
                    status: status,
                    filter: $scope.getFilterValue(),

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
            //lấy lại dữ liệu sau khi duyệt hoặc không duyệt công
            $scope.GetListDataPopUp = function (loai) {
                ShowLoader();
                var status = 0;
                if ($scope.Data.statusfind != "" && $scope.Data.statusfind != null && $scope.checkbox == false) {
                    status = 1;
                }
                if ($scope.checkbox != false) {
                    status = 2;
                }
                if ($scope.stringfilterduyet != "") {
                    status = 3;
                }
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
                    userid: $scope.userid1,
                    status: status,
                    filter: $scope.getFilterValue()
                }

                var getDataTbl = myService.GetTableData(data, tableUrl);
                getDataTbl.then(function (emp) {
                    $scope.employeepopup = emp.data.employees;
                    if ($scope.employeepopup == null || $scope.employeepopup.length == 0) {
                        AppendToToastr(false, notification, errorNoRecordsSelected);
                        return false;
                    }
                    else
                        if ($scope.employeepopup.length > 0 && $scope.employeepopup.filter(function (item) { return item && ([4, 5, 9, 10].indexOf(item.Status) != -1) }).length > 0) {
                            AppendToToastr(false, notification, errorListContainsPublic);
                            return false;
                        }
                    $scope.totalCount = emp.data.totalCount;
                    $scope.lstTotal = emp.data.lstTotal;
                    $scope.SetTotalByColumns = function (totalName) {
                        if (!angular.isUndefined(totalName) && totalName !== null) {
                            return $scope.lstTotal[totalName];
                        }
                        return "-";
                    }
                    if (loai == 1) {
                        $scope.popupduyet = true;
                        $scope.popupnoduyet = false;
                    }
                    else {
                        $scope.popupnoduyet = true;
                        $scope.popupduyet = false;
                    }
                    ShowPopup($,
                        "#duyetcong1",
                        $scope.tableInfo.PopupWidth,
                        $scope.tableInfo.PopupHeight);
                    HiddenLoader();
                },
                    function (emp) {
                        AppendToToastr(false, notification, errorNotiFalse);
                    });

            }
            $scope.Data = {};
            $scope.onStatus = function () {
                if ($scope.Data.statusfind != null) {
                    $scope.Data.statusfind = $scope.Data.statusfind;
                }
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
                if (dataFormat === "dd/mm/yyyy hh:mm:ss" && value != null) {
                    return moment(value).format('DD/MM/YYYY HH:MM:ss');
                }
                if (type === 3) {
                    if (value == null || value == '') {
                        return null
                    }
                    else {
                        return FormatDate(value);
                    }
                }
                if (dataFormat === "N2") {
                    return value.toFixed(2);
                }
                if (dataFormat === "hh:mm:ss") {
                    var vitri;
                    var time;
                    if (value != null) {
                        vitri = value.indexOf("T") + 1;
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
            $scope.getFilterValue = function () {
                var lstObj = $scope.filterColumnsChoosed;
                var stringFilter = "";
                var len = lstObj.length - 1;
                var oldString = $scope.FromMonth;
                var str = oldString.replace("/", "-");
                $scope.newString = str;
                stringFilter += " and a.MonthVacation = '" + $scope.newString + "'  ";
                if ($rootScope.PhongBan != null) {
                    stringFilter += "  AND a.StaffID IN (SELECT StaffID FROM dbo.Staff WHERE OrganizationUnitID = " + $rootScope.PhongBan + " )  ";
                }
                if ($scope.stringfilterduyet != "") {
                    stringFilter += $scope.stringfilterduyet;
                }
                if ($scope.Data.statusfind != null) {
                    stringFilter += " and a.Status = " + $scope.Data.statusfind + " ";
                }
                if ($rootScope.giatri != null & $rootScope.giatri != "") {
                    stringFilter += " and a.StaffID = " + $rootScope.giatri + " ";
                }
                return stringFilter;
            };
            //-----------------Filter-End----------

            //click button duyệt công hoặc không duyệt công
            $scope.DuyetCong = function (loai) {

                $scope.stringfilterduyet = "AND a.AutoID IN (";
                var getData = myService.GetColumns(tableUrl);
                getData.then(function (emp) {
                    emp.data.result.splice(10, 1);
                    emp.data.result.splice(10, 1);
                    emp.data.result.splice(13, 1);
                    emp.data.result.splice(13, 1);
                    emp.data.result.splice(13, 1);
                    emp.data.result.splice(13, 1);
                    $scope.Columnpopup = emp.data.result;
                    $scope.dodaiColumnpopup = $scope.Columnpopup.length;
                    var len = $scope.list.AutoID.length;
                    for (var i = 0; i < len; i++) {
                        if (i < len - 1) {
                            $scope.stringfilterduyet += $scope.list.AutoID[i] + ",";
                        }
                        else {
                            $scope.stringfilterduyet += $scope.list.AutoID[i] + ")";
                        }
                    }
                    $scope.GetListDataPopUp(loai);
                    $scope.stringfilterduyet = "";
                },
                    function (emp) {
                        AppendToToastr(false, notification, errorNotiFalse);
                    });
            }
            //click button duyệt hoặc không duyệt công
            $scope.LuuDuyet = function (loai) {
                var len = $scope.list.AutoID.length;
                $scope.up = 0;
                $scope.errorNote = false;
                $scope.ListData = [];
                for (var i = 0; i < len; i++) {
                    $scope.editData.AutoID = $scope.list.AutoID[i];
                    $scope.editData.Note = document.getElementById($scope.list.AutoID[i]).value;
                    if (loai == 1) {
                        $scope.editData.Type = 1;
                        var obj = { AutoID: $scope.editData.AutoID, Note: $scope.editData.Note, Type: $scope.editData.Type }
                    }
                    else {
                        $scope.editData.Type = 0;
                        if ($scope.editData.Note == "" || $scope.editData.Note == null) {
                            AppendToToastr(false, notification, errorReasonNotApproved);
                            $scope.errorNote = true;                          
                            return;
                        }
                        else {
                            var obj = { AutoID: $scope.editData.AutoID, Note: $scope.editData.Note, Type: $scope.editData.Type }
                        }
                    }
                    $scope.ListData.push(obj)
                }
                if ($scope.ListData != null && $scope.ListData.length>0) {
                    var updateAction = myService.UpdateData("/HR_WorkingDaySupplement/HR_WorkingDaySupplement_Approval", $scope.ListData);
                    var dt = Loading();
                    updateAction.then(function (res) {
                        if (res.data.result.IsSuccess == true) {
                            AppendToToastr(true, notification, timekeeping_Ms_ApprovalSuccess, 500, 5000);
                            $scope.up = 1;
                            $scope.GetListData();
                        }
                        else {
                            AppendToToastr(false, notification, timekeeping_Ms_ApprovalFalse, 500, 5000);
                        }
                        dt.finish();

                    },
                        function (res) {
                            $scope.up = 0;
                            dt.finish();

                        });
                }
                if ($scope.errorNote == false) {
                    $.colorbox.close();
                    $scope.list = {
                        AutoID: []
                    }
                }
            }


            $scope.onStatus = function () {//lựa chọn kiểu công bổ sung do ai duyệt

                if ($scope.StatusValue != null) {
                    $scope.StatusValue = $scope.StatusValue;
                }
            }
            $scope.onOrganizationUnitChange = function () {//lựa chọn phòng ban
                $rootScope.PhongBan = $scope.OrganizationUnitID;
                if ($scope.OrganizationUnitID != null) {
                    ListEmployeeWhereOrganizationUnit($scope.OrganizationUnitID);//lấy lại dữ liệu của nhân viên theo phòng ban
                }
                else {
                    ListEmployeeWhereOrganizationUnit(0);
                }
            }
            $scope.onStaffChange = function () {//lựa chọn nhân viên

                if ($scope.StaffID != "" & $scope.StaffID != null) {
                    $scope.StaffID = angular.copy($scope.StaffID);
                    $rootScope.giatri = $scope.StaffID;
                }
                else {
                    $scope.StaffID = 0;
                    $rootScope.giatri = $scope.StaffID;
                }
            }
            //dropdown nhân viên theo phòng ban
            function ListEmployeeWhereOrganizationUnit(id) {
                var data = {
                    url: "/OrganizationUnit/EmployeeByOrganizationUnitIDSupplement?id=" + id
                }
                var list = myService.getData(data);
                list.then(function (res) {
                    $scope.ListEmployeeWhereOrganizationUnit = res.data.result;

                }, function (res) {
                    $scope.msg = "Error";
                })
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

    app.directive("myFiles", function ($parse) {
        return function linkFn(scope, elem, attrs) {
            elem.on("change", function (e) {
                scope.$eval(attrs.myFiles + "=$files", { $files: e.target.files });
                scope.$apply();
            })
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
}
