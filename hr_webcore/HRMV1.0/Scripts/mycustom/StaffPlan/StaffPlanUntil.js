function BuildTable(appName, controllerName, tableUrl) {
    app.controller(controllerName,
        function ($scope, myService, $filter, $rootScope, $compile, $http) {
            $scope.maxSize = 5; // Limit number for pagination display number.
            $scope.totalCount = 0; // Total number of items in all pages. initialize as a 
            $scope.pageIndex = 1; // Current page number. First page is 1.-->
            $scope.pageSizeSelected = 5;
            $scope.isShowFilter = false; $scope.typeEnds = [{ name: "Và", nameEN: "And", value: " and " }, { name: "Hoặc", nameEN: "Or", value: " or " }];
            $scope.typeFilterA = [{ name: ">", nameEN: ">", value: " > '#' " }, { name: "<", nameEN: "<", value: " < '#' " }, { name: "=", nameEN: "=", value: " = '#' " }, { name: "Khác", nameEN: "Not equal", value: " != '#' " }];
            $scope.typeFilterB = [{ name: "Có chứa", nameEN: "Contains", value: " like '%#%' " }, { name: "Bằng", nameEN: "Is", value: " = '#' " }, { name: "Không chứa", nameEN: "Do not contains", value: " != '#' " }];
            $scope.filterColumnsChoosed = [];

            $scope.emailValid = /^[a-z]+[a-z0-9._]+@[a-z]+\.[a-z.]{2,5}$/;
            var today = new Date();
            var yyyy = today.getFullYear();
            $scope.FromMonth = yyyy;
            $scope.Year = yyyy;
            $rootScope.data = {
                filter5: '',
                filter1: '',
                filter2: '',
                filter3: '',
                filter4: '',
                filter6: '',
                filter7: '',
                filter8: '',
                filter: 'Year LIKE ' + $scope.Year,
            }; //dự liệu truyền vào
            $scope.Width = "width:2750px;";
            $scope.Height='height:700px;'
            $scope.IshowTotal = true;
            $scope.showtab13 = true;
            $scope.isShow1 = true;

            $scope.Data = {};
            $scope.Data.SumValue = 0;

            var today = new Date();
            var yyyy = today.getFullYear();
            $scope.FromMonth = yyyy;
            $scope.Data.Year = yyyy;
            //model đượct truyền ra từ directive build table
            $scope.employee = [];


            //Global theo parentID
            $scope.GlobalListWhereParentID = {
                Status: 60,
                Product: 3320
            }


            //-----------------List-------------

            //$scope.toggleCheck = function () {

            //    if (!$scope.checkAll) {

            //        $scope.checkAll = true;
            //        $scope.list.StaffID = $scope.employees.map(function (employee) {
            //            return employee.StaffID;
            //        });
            //    } else {
            //        $scope.checkAll = false;
            //        $scope.list.StaffID = [];
            //    }

            //}
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
                        AppendToToastr(false, Notification, errorNotiFalse);
                    });
            }

            $scope.GetAddPermission = function (idTable) {
                var tblAction = myService.getTableAction(idTable);
                tblAction.then(function (emp) {

                    $scope.tablePermission = emp.data.result;
                    if ($scope.tablePermission && $scope.tablePermission.isEdit == false) {
                        $scope.is_readonly = true;
                    }
                    else {
                        $scope.is_readonly = false;
                    }
                    // $scope.BuildAddButton(emp.data.result);
                },
                    function (emp) {
                        AppendToToastr(false, Notification, errorNotiFalse);
                    });
            }

            $scope.getTableInfo();

            //$scope.getColumns = function () {
            //    var getData = myService.GetColumns(tableUrl);
            //    getData.then(function (emp) {
            //        $scope.Columns = emp.data.result;
            //        $scope.GetListData();
            //        $scope.dodai = $scope.Columns.length + 1;

            //    },
            //        function (emp) {
            //            AppendToToastr(false, Notification, errorNotiFalse);
            //        });
            //}

            //$scope.GetListData = function () {
            //    $(function () {
            //        var dt = Loading();
            //        var data = {
            //            filter: $scope.getFilterValue(),
            //            pageIndex: $scope.pageIndex,
            //            pageSize: $scope.pageSizeSelected,
            //        }
            //        var getDataTbl = myService.GetTableData(data, tableUrl);
            //        getDataTbl.then(function (emp) {
            //            $scope.employees = emp.data.employees;
                        
            //            $scope.totalCount = emp.data.totalCount;
            //            $scope.lstTotal = emp.data.lstTotal;
            //            $scope.ToTalMonth = emp.data.ToTalMonth;
            //            $scope.SetTotalByColumns = function (totalName) {
            //                if (!angular.isUndefined(totalName) && totalName !== null) {
            //                    return $scope.lstTotal[totalName];
            //                }
            //                return "-";
            //            };
            //            dt.finish();
            //        },
            //            function (emp) {
            //                AppendToToastr(false, notification, errorNotiFalse);
            //            });
            //    });


            //}

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
                if (type === 3 && value != null) {

                    return FormatDate(value);

                }
                if (type === 2 && value != null) {
                    return value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                }
                return value;

            }
            $scope.SetHiddenActionColumn = function (showEdit, showDelete) {
                if (showEdit === false && showDelete === false) {
                    return false;
                }
                return true;
            }

            $scope.init = function () {
                $scope.ListOrganizationUnit();
                $scope.ListCurrencyName();
                $scope.ListEmployees();
            }
            //---------------------------------------------------------------------------- List-End ------------------------------------------------------------------------------------
            $scope.getFilterValue = function () {
                
                var stringFilter = "";
                if ($scope.UserID != null && $scope.UserID != "") {
                    stringFilter += " UserID = " + $scope.UserID + " ";
                }
                if ($scope.OrganizationUnitID != null && $scope.OrganizationUnitID != "") {
                    if ($scope.UserID != null && $scope.UserID != "") {
                        stringFilter += " and DS_OrganizationUnitID = " + $scope.OrganizationUnitID + " ";
                    }
                    else {
                        stringFilter += " DS_OrganizationUnitID = " + $scope.OrganizationUnitID + " ";
                    }
                }
                if ($scope.FromMonth != null) {
                    if ($scope.FromMonth != "") {
                        if ($scope.UserID != null && $scope.UserID != "" || $scope.OrganizationUnitID != null && $scope.OrganizationUnitID != "") {
                            stringFilter += "and Year = " + $scope.FromMonth + " ";
                        }
                        else {
                            stringFilter += "Year = " + $scope.FromMonth + " ";
                        }
                    }
                }
                return stringFilter;
            }

            //-----------------Lấy danh sách phòng ban------------------
            $scope.ListOrganizationUnit = function () {
                var data = {
                    url: "/OrganizationUnitPlan/OrganizationUnit_GetALL"
                }
                var list = myService.getData(data);
                list.then(function (res) {
                    $scope.getListAllOrganizationUnit = res.data.result;
                    $rootScope.getListAllOrganizationUnit = $scope.getListAllOrganizationUnit;

                }, function (res) {
                    $scope.msg = "Error";
                })
            }

            //-----------------Lấy danh sách tiền tệ------------------
            $scope.ListCurrencyName = function () {
                var data = {
                    url: "/Common/GetDataByGloballistnotTree?parentid=" + 3
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.ListCurrencyName = res.data.result;
                    $rootScope.ListCurrencyName = $scope.ListCurrencyName;

                },
                    function (res) {
                        $scope.msg = "Error";
                    });
            }

            //-----------------Lấy danh sách nhân viên------------------
            $scope.ListEmployees = function () {             
                var data = {
                    url: "/StaffPlan/Staff_GetALL"
                }
                var list = myService.getData(data);
                list.then(function (res) {               
                    $scope.ListEmployees = res.data.result;
                    $rootScope.ListEmployees = res.data.result;
                }, function (res) {
                    $scope.msg = "Error";
                })
                
            }

            // ----------------- Add chính sách-----------           
            $scope.addClick = function () {
                $scope.ErrorStaff = "";
                $scope.ErrorOrganizationUnit = "";
                $scope.ErrorM1 = "";
                $scope.ErrorM2 = "";
                $scope.ErrorM3 = "";
                $scope.ErrorM4 = "";
                $scope.ErrorM5 = "";
                $scope.ErrorM6 = "";
                $scope.ErrorM7 = "";
                $scope.ErrorM8 = "";
                $scope.ErrorM9 = "";
                $scope.ErrorM10 = "";
                $scope.ErrorM11 = "";
                $scope.ErrorM2 = "";
                $scope.ErrorYear = "";
                $scope.Data = {};
                var today = new Date();
                var yyyy = today.getFullYear();
                $scope.Data.Year = yyyy;
                ShowPopup($,
                    "#SaveStaffPlan",
                    $scope.tableInfo.PopupWidth,
                    $scope.tableInfo.PopupHeight);
            }

            //-----------------Edit Chính sách--------------
            $scope.EditClick = function (employee) {
                employee.Data = {};
                employee.LoadingInput = true;
                employee.Show = false;
                employee.Data.AutoID = employee.AutoID;
                if (employee.UserID != null) {
                    employee.Data.UserID = employee.UserID;
                }
                if (employee.DS_OrganizationUnitID != null) {
                    employee.Data.DS_OrganizationUnitID = employee.DS_OrganizationUnitID;
                }
                if (employee.CurrencyTypeID != null) {
                    employee.Data.CurrencyTypeID = employee.CurrencyTypeID;
                }
                if (employee.Status != null) {
                    if (employee.Status == '0') {
                        employee.Data.Status = "zero"
                    }
                    else {
                        employee.Data.Status = employee.Status.toString();
                    }
                }
                employee.Data.Year = employee.Year;                
                if (employee.M1 != null) {
                    employee.Data.M1 = employee.M1.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                }
                if (employee.M2 != null) {
                    employee.Data.M2 = employee.M2.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                }
                if (employee.M3 != null) {
                    employee.Data.M3 = employee.M3.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                }
                if (employee.M4 != null) {
                    employee.Data.M4 = employee.M4.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                }
                if (employee.M5 != null) {
                    employee.Data.M5 = employee.M5.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                }
                if (employee.M6 != null) {
                    employee.Data.M6 = employee.M6.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                }
                if (employee.M7 != null) {
                    employee.Data.M7 = employee.M7.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                }
                if (employee.M8 != null) {
                    employee.Data.M8 = employee.M8.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                }
                if (employee.M9 != null) {
                    employee.Data.M9 = employee.M9.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                }
                if (employee.M10 != null) {
                    employee.Data.M10 = employee.M10.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                }
                if (employee.M11 != null) {
                    employee.Data.M11 = employee.M11.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                }
                if (employee.M12 != null) {
                    employee.Data.M12 = employee.M12.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                }

            }

            $scope.HideClick = function (employee) {
                employee.ErrorStaffName = "";
                employee.ErrorOrganizationUnit = "";
                employee.ErrorYear = "";
                employee.ErrorM1 = "";
                employee.ErrorM2 = "";
                employee.ErrorM3 = "";
                employee.ErrorM4 = "";
                employee.ErrorM5 = "";
                employee.ErrorM6 = "";
                employee.ErrorM7 = "";
                employee.ErrorM8 = "";
                employee.ErrorM9 = "";
                employee.ErrorM10 = "";
                employee.ErrorM11 = "";
                employee.ErrorM12 = "";
                employee.LoadingInput = false;
                employee.Show = true;
                employee.Data = {};
                employee.Data = null;

            }

            //--------bắt lỗi khi chưa chọn nhân viên------------
            $scope.ChangeListEmployees = function (employee) {
                if (employee.Data.UserID == null || employee.Data.UserID == "") {
                    employee.ErrorStaffName = errorSelectEmployee;
                    return;
                }
                else {
                    employee.ErrorStaffName = "";
                    employee.ErrorOrganizationUnit = "";
                    for (var i = 0; i < $scope.ListEmployees.length; i++) {
                        if (employee.Data.UserID == $scope.ListEmployees[i].UserID) {
                            employee.Data.DS_OrganizationUnitID = $scope.ListEmployees[i].OrganizationUnitID;
                            for (var j = 0; j < $scope.getListAllOrganizationUnit.length; j++) {
                                if ($scope.getListAllOrganizationUnit[j].OrganizationUnitID == employee.Data.DS_OrganizationUnitID) {
                                    employee.Data.CurrencyTypeID = $scope.getListAllOrganizationUnit[j].CurrencyTypeID;
                                    break
                                }
                            }

                        }
                    }
                }
            }

            //--------bắt lỗi khi chưa chọn phòng ban------------
            $scope.ChangeOrganizationUnit = function (employee) {
                if (employee.Data.DS_OrganizationUnitID == null || employee.Data.DS_OrganizationUnitID == "") {
                    employee.ErrorOrganizationUnit = errorDepartment;
                    employee.Data.CurrencyTypeID = "";
                    return;
                }
                else {
                    employee.ErrorOrganizationUnit = "";
                    for (var i = 0; i < $scope.getListAllOrganizationUnit.length; i++) {
                        if ($scope.getListAllOrganizationUnit[i].OrganizationUnitID == employee.Data.DS_OrganizationUnitID) {
                            employee.Data.CurrencyTypeID = $scope.getListAllOrganizationUnit[i].CurrencyTypeID;
                        }
                    }
                }
            }

            //--------bắt lỗi khi chưa nhập năm------------
            $scope.ChangeYear = function (employee) {
                if (employee.Data.Year == null || employee.Data.Year == "") {
                    employee.ErrorYear = errorEnterYear;
                    return;
                }
                else if (isNaN(employee.Data.Year)) {
                    employee.ErrorYear = errorYearIsNumber;
                    return
                }
                else {
                    employee.ErrorYear = "";
                }
            }

            //---------------Bắt lỗi khi edit nhập ô tháng 1----------------------
            $scope.ChangeM1 = function (employee) {
                if (employee.Data.M1 != null && employee.Data.M1 != "") {

                    var position = employee.Data.M1.indexOf('-');
                    if (position != -1) {
                        employee.Data.M1 = employee.Data.M1.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        
                        var position = employee.Data.M1.indexOf('.');
                        if (position != -1) {
                            var x = employee.Data.M1.split(".");
                            var z = x[1].toString();
                            if (z.length > 2) {
                                z = z.substring(0, 2);
                                employee.Data.M1 = "-" + x[0] + "." + z;
                                return
                            }
                            else {
                                employee.ErrorM1 = "";
                                employee.Data.M1 = "-" + x[0] + "." + z;
                            }
                        }

                        employee.Data.M1 = employee.Data.M1.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = employee.Data.M1.indexOf(',');
                        if (position != -1) {
                            var x = employee.Data.M1.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN(employee.Data.M1)) {
                            employee.ErrorM1 = errorMoneyIsNumber;
                            employee.Data.M1 = "-" + employee.Data.M1;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            employee.ErrorM1 = errorMoneyIsNumber;
                            employee.Data.M1 = "-" + employee.Data.M1;
                            return
                        }
                        else {
                            employee.ErrorM1 = "";
                            var x = employee.Data.M1.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var x1 = x.split(",");
                            var list = x1.join("").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            employee.Data.M1 = "-" + list;
                        }

                    }
                    else {

                        var position = employee.Data.M1.indexOf('.');
                        if (position != -1) {
                            var x = employee.Data.M1.split(".");
                            var z = x[1].toString();
                            if (z.length > 2) {
                                z = z.substring(0, 2);
                                employee.Data.M1 = x[0] + "." + z;
                                return
                            }
                            else {
                                employee.ErrorM1 = "";
                            }
                        }

                        var position = employee.Data.M1.indexOf(',');
                        if (position != -1) {
                            var x = employee.Data.M1.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN(employee.Data.M1)) {
                            employee.ErrorM1 = errorMoneyIsNumber;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            employee.ErrorM1 = errorMoneyIsNumber;
                            return
                        }

                        else {
                            employee.ErrorM1 = "";
                            var x = employee.Data.M1.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var x1 = x.split(",");
                            var list = x1.join("").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            employee.Data.M1 = list;

                        }
                    }

                }
                else {
                    employee.ErrorM1 = "";
                }
            }

            //---------------Bắt lỗi khi edit nhập ô tháng 2----------------------
            $scope.ChangeM2 = function (employee) {
                if (employee.Data.M2 != null && employee.Data.M2 != "") {

                    var position = employee.Data.M2.indexOf('-');
                    if (position != -1) {
                        employee.Data.M2 = employee.Data.M2.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = employee.Data.M2.indexOf('.');
                        if (position != -1) {
                            var x = employee.Data.M2.split(".");
                            var z = x[1].toString();
                            if (z.length > 2) {
                                z = z.substring(0, 2);
                                employee.Data.M2 = "-" + x[0] + "." + z;
                                return
                            }
                            else {
                                employee.ErrorM2 = "";
                                employee.Data.M2 = "-" + x[0] + "." + z;
                            }
                        }

                        employee.Data.M2 = employee.Data.M2.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = employee.Data.M2.indexOf(',');
                        if (position != -1) {
                            var x = employee.Data.M2.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN(employee.Data.M2)) {
                            employee.ErrorM2 = errorMoneyIsNumber;
                            employee.Data.M2 = "-" + employee.Data.M2;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            employee.ErrorM2 = errorMoneyIsNumber;
                            employee.Data.M2 = "-" + employee.Data.M2;
                            return
                        }
                        else {
                            employee.ErrorM2 = "";
                            var x = employee.Data.M2.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var x1 = x.split(",");
                            var list = x1.join("").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            employee.Data.M2 = "-" + list;
                        }

                    }
                    else {

                        var position = employee.Data.M2.indexOf('.');
                        if (position != -1) {
                            var x = employee.Data.M2.split(".");
                            var z = x[1].toString();
                            if (z.length > 2) {
                                z = z.substring(0, 2);
                                employee.Data.M2 = x[0] + "." + z;
                                return
                            }
                            else {
                                employee.ErrorM2 = "";
                            }
                        }

                        var position = employee.Data.M2.indexOf(',');
                        if (position != -1) {
                            var x = employee.Data.M2.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN(employee.Data.M2)) {
                            employee.ErrorM2 = errorMoneyIsNumber;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            employee.ErrorM2 = errorMoneyIsNumber;
                            return
                        }
                        else {
                            employee.ErrorM2 = "";
                            var x = employee.Data.M2.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var x1 = x.split(",");
                            var list = x1.join("").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            employee.Data.M2 = list;

                        }
                    }

                }
                else {
                    employee.ErrorM2 = "";
                }
            }

            //---------------Bắt lỗi khi edit nhập ô tháng 3----------------------
            $scope.ChangeM3 = function (employee) {
                if (employee.Data.M3 != null && employee.Data.M3 != "") {

                    var position = employee.Data.M3.indexOf('-');
                    if (position != -1) {

                        employee.Data.M3 = employee.Data.M3.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = employee.Data.M3.indexOf('.');
                        if (position != -1) {
                            var x = employee.Data.M3.split(".");
                            var z = x[1].toString();
                            if (z.length > 2) {
                                z = z.substring(0, 2);
                                employee.Data.M3 = "-" + x[0] + "." + z;
                                return
                            }
                            else {
                                employee.ErrorM3 = "";
                                employee.Data.M3 = "-" + x[0] + "." + z;
                            }
                        }
                        employee.Data.M3 = employee.Data.M3.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = employee.Data.M3.indexOf(',');
                        if (position != -1) {
                            var x = employee.Data.M3.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN(employee.Data.M3)) {
                            employee.ErrorM3 = errorMoneyIsNumber;
                            employee.Data.M3 = "-" + employee.Data.M3;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            employee.ErrorM3 = errorMoneyIsNumber;
                            employee.Data.M3 = "-" + employee.Data.M3;
                            return
                        }
                        else {
                            employee.ErrorM3 = "";
                            var x = employee.Data.M3.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var x1 = x.split(",");
                            var list = x1.join("").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            employee.Data.M3 = "-" + list;
                        }

                    }
                    else {

                        var position = employee.Data.M3.indexOf('.');
                        if (position != -1) {
                            var x = employee.Data.M3.split(".");
                            var z = x[1].toString();
                            if (z.length > 2) {
                                z = z.substring(0, 2);
                                employee.Data.M3 = x[0] + "." + z;
                                return
                            }
                            else {
                                employee.ErrorM3 = "";
                            }
                        }
                        var position = employee.Data.M3.indexOf(',');
                        if (position != -1) {
                            var x = employee.Data.M3.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN(employee.Data.M3)) {
                            employee.ErrorM3 = errorMoneyIsNumber;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            employee.ErrorM3 = errorMoneyIsNumber;
                            return
                        }
                        else {
                            employee.ErrorM3 = "";
                            var x = employee.Data.M3.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var x1 = x.split(",");
                            var list = x1.join("").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            employee.Data.M3 = list;

                        }
                    }

                }
                else {
                    employee.ErrorM3 = "";
                }
            }

            //---------------Bắt lỗi khi edit nhập ô tháng 4----------------------
            $scope.ChangeM4 = function (employee) {
                if (employee.Data.M4 != null && employee.Data.M4 != "") {

                    var position = employee.Data.M4.indexOf('-');
                    if (position != -1) {
                        employee.Data.M4 = employee.Data.M4.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = employee.Data.M4.indexOf('.');
                        if (position != -1) {
                            var x = employee.Data.M4.split(".");
                            var z = x[1].toString();
                            if (z.length > 2) {
                                z = z.substring(0, 2);
                                employee.Data.M4 = "-" + x[0] + "." + z;
                                return
                            }
                            else {
                                employee.ErrorM4 = "";
                                employee.Data.M4 = "-" + x[0] + "." + z;
                            }
                        }
                        employee.Data.M4 = employee.Data.M4.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = employee.Data.M4.indexOf(',');
                        if (position != -1) {
                            var x = employee.Data.M4.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN(employee.Data.M4)) {
                            employee.ErrorM4 = errorMoneyIsNumber;
                            employee.Data.M4 = "-" + employee.Data.M4;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            employee.ErrorM4 = errorMoneyIsNumber;
                            employee.Data.M4 = "-" + employee.Data.M4;
                            return
                        }
                        else {
                            employee.ErrorM4 = "";
                            var x = employee.Data.M4.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var x1 = x.split(",");
                            var list = x1.join("").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            employee.Data.M4 = "-" + list;
                        }

                    }
                    else {

                        var position = employee.Data.M4.indexOf('.');
                        if (position != -1) {
                            var x = employee.Data.M4.split(".");
                            var z = x[1].toString();
                            if (z.length > 2) {
                                z = z.substring(0, 2);
                                employee.Data.M4 = x[0] + "." + z;
                                return
                            }
                            else {
                                employee.ErrorM4 = "";
                            }
                        }
                        var position = employee.Data.M4.indexOf(',');
                        if (position != -1) {
                            var x = employee.Data.M4.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN(employee.Data.M4)) {
                            employee.ErrorM4 = errorMoneyIsNumber;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            employee.ErrorM4 = errorMoneyIsNumber;
                            return
                        }
                        else {
                            employee.ErrorM4 = "";
                            var x = employee.Data.M4.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var x1 = x.split(",");
                            var list = x1.join("").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            employee.Data.M4 = list;

                        }
                    }

                }
                else {
                    employee.ErrorM4 = "";
                }
            }

            //---------------Bắt lỗi khi edit nhập ô tháng 5----------------------
            $scope.ChangeM5 = function (employee) {
                if (employee.Data.M5 != null && employee.Data.M5 != "") {

                    var position = employee.Data.M5.indexOf('-');
                    if (position != -1) {
                        employee.Data.M5 = employee.Data.M5.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = employee.Data.M5.indexOf('.');
                        if (position != -1) {
                            var x = employee.Data.M5.split(".");
                            var z = x[1].toString();
                            if (z.length > 2) {
                                z = z.substring(0, 2);
                                employee.Data.M5 = "-" + x[0] + "." + z;
                                return
                            }
                            else {
                                employee.ErrorM5 = "";
                                employee.Data.M5 = "-" + x[0] + "." + z;
                            }
                        }
                        employee.Data.M5 = employee.Data.M5.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = employee.Data.M5.indexOf(',');
                        if (position != -1) {
                            var x = employee.Data.M5.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN(employee.Data.M5)) {
                            employee.ErrorM5 = errorMoneyIsNumber;
                            employee.Data.M5 = "-" + employee.Data.M5;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            employee.ErrorM5 = errorMoneyIsNumber;
                            employee.Data.M5 = "-" + employee.Data.M5;
                            return
                        }
                        else {
                            employee.ErrorM5 = "";
                            var x = employee.Data.M5.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var x1 = x.split(",");
                            var list = x1.join("").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            employee.Data.M5 = "-" + list;
                        }

                    }
                    else {

                        var position = employee.Data.M5.indexOf('.');
                        if (position != -1) {
                            var x = employee.Data.M5.split(".");
                            var z = x[1].toString();
                            if (z.length > 2) {
                                z = z.substring(0, 2);
                                employee.Data.M5 = x[0] + "." + z;
                                return
                            }
                            else {
                                employee.ErrorM5 = "";
                            }
                        }
                        var position = employee.Data.M5.indexOf(',');
                        if (position != -1) {
                            var x = employee.Data.M5.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN(employee.Data.M5)) {
                            employee.ErrorM5 = errorMoneyIsNumber;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            employee.ErrorM5 = errorMoneyIsNumber;
                            return
                        }
                        else {
                            employee.ErrorM5 = "";
                            var x = employee.Data.M5.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var x1 = x.split(",");
                            var list = x1.join("").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            employee.Data.M5 = list;

                        }
                    }

                }
                else {
                    employee.ErrorM5 = "";
                }
            }

            //---------------Bắt lỗi khi edit nhập ô tháng 6----------------------
            $scope.ChangeM6 = function (employee) {
                if (employee.Data.M6 != null && employee.Data.M6 != "") {

                    var position = employee.Data.M6.indexOf('-');
                    if (position != -1) {
                        employee.Data.M6 = employee.Data.M6.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = employee.Data.M6.indexOf('.');
                        if (position != -1) {
                            var x = employee.Data.M6.split(".");
                            var z = x[1].toString();
                            if (z.length > 2) {
                                z = z.substring(0, 2);
                                employee.Data.M6 = "-" + x[0] + "." + z;
                                return
                            }
                            else {
                                employee.ErrorM6 = "";
                                employee.Data.M6 = "-" + x[0] + "." + z;
                            }
                        }
                        employee.Data.M6 = employee.Data.M6.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = employee.Data.M6.indexOf(',');
                        if (position != -1) {
                            var x = employee.Data.M6.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN(employee.Data.M6)) {
                            employee.ErrorM6 = errorMoneyIsNumber;
                            employee.Data.M6 = "-" + employee.Data.M6;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            employee.ErrorM6 = errorMoneyIsNumber;
                            employee.Data.M6 = "-" + employee.Data.M6;
                            return
                        }
                        else {
                            employee.ErrorM6 = "";
                            var x = employee.Data.M6.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var x1 = x.split(",");
                            var list = x1.join("").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            employee.Data.M6 = "-" + list;
                        }

                    }
                    else {

                        var position = employee.Data.M6.indexOf('.');
                        if (position != -1) {
                            var x = employee.Data.M6.split(".");
                            var z = x[1].toString();
                            if (z.length > 2) {
                                z = z.substring(0, 2);
                                employee.Data.M6 = x[0] + "." + z;
                                return
                            }
                            else {
                                employee.ErrorM6 = "";
                            }
                        }
                        var position = employee.Data.M6.indexOf(',');
                        if (position != -1) {
                            var x = employee.Data.M6.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN(employee.Data.M6)) {
                            employee.ErrorM6 = errorMoneyIsNumber;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            employee.ErrorM6 = errorMoneyIsNumber;
                            return
                        }
                        else {
                            employee.ErrorM6 = "";
                            var x = employee.Data.M6.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var x1 = x.split(",");
                            var list = x1.join("").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            employee.Data.M6 = list;

                        }
                    }

                }
                else {
                    employee.ErrorM6 = "";
                }
            }

            //---------------Bắt lỗi khi edit nhập ô tháng 7----------------------
            $scope.ChangeM7 = function (employee) {
                if (employee.Data.M7 != null && employee.Data.M7 != "") {

                    var position = employee.Data.M7.indexOf('-');
                    if (position != -1) {
                        employee.Data.M7 = employee.Data.M7.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = employee.Data.M7.indexOf('.');
                        if (position != -1) {
                            var x = employee.Data.M7.split(".");
                            var z = x[1].toString();
                            if (z.length > 2) {
                                z = z.substring(0, 2);
                                employee.Data.M7 = "-" + x[0] + "." + z;
                                return
                            }
                            else {
                                employee.ErrorM7 = "";
                                employee.Data.M7 = "-" + x[0] + "." + z;
                            }
                        }
                        employee.Data.M7 = employee.Data.M7.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = employee.Data.M7.indexOf(',');
                        if (position != -1) {
                            var x = employee.Data.M7.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN(employee.Data.M7)) {
                            employee.ErrorM7 = errorMoneyIsNumber;
                            employee.Data.M7 = "-" + employee.Data.M7;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            employee.ErrorM7 = errorMoneyIsNumber;
                            employee.Data.M7 = "-" + employee.Data.M7;
                            return
                        }
                        else {
                            employee.ErrorM7 = "";
                            var x = employee.Data.M7.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var x1 = x.split(",");
                            var list = x1.join("").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            employee.Data.M7 = "-" + list;
                        }

                    }
                    else {

                        var position = employee.Data.M7.indexOf('.');
                        if (position != -1) {
                            var x = employee.Data.M7.split(".");
                            var z = x[1].toString();
                            if (z.length > 2) {
                                z = z.substring(0, 2);
                                employee.Data.M7 = x[0] + "." + z;
                                return
                            }
                            else {
                                employee.ErrorM7 = "";
                            }
                        }
                        var position = employee.Data.M7.indexOf(',');
                        if (position != -1) {
                            var x = employee.Data.M7.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN(employee.Data.M7)) {
                            employee.ErrorM7 = errorMoneyIsNumber;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            employee.ErrorM7 = errorMoneyIsNumber;
                            return
                        }
                        else {
                            employee.ErrorM7 = "";
                            var x = employee.Data.M7.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var x1 = x.split(",");
                            var list = x1.join("").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            employee.Data.M7 = list;

                        }
                    }

                }
                else {
                    employee.ErrorM7 = "";
                }
            }

            //---------------Bắt lỗi khi edit nhập ô tháng 8----------------------
            $scope.ChangeM8 = function (employee) {
                if (employee.Data.M8 != null && employee.Data.M8 != "") {

                    var position = employee.Data.M8.indexOf('-');
                    if (position != -1) {
                        employee.Data.M8 = employee.Data.M8.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = employee.Data.M8.indexOf('.');
                        if (position != -1) {
                            var x = employee.Data.M8.split(".");
                            var z = x[1].toString();
                            if (z.length > 2) {
                                z = z.substring(0, 2);
                                employee.Data.M8 = "-" + x[0] + "." + z;
                                return
                            }
                            else {
                                employee.ErrorM8 = "";
                                employee.Data.M8 = "-" + x[0] + "." + z;
                            }
                        }
                        employee.Data.M8 = employee.Data.M8.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = employee.Data.M8.indexOf(',');
                        if (position != -1) {
                            var x = employee.Data.M8.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN(employee.Data.M8)) {
                            employee.ErrorM8 = errorMoneyIsNumber;
                            employee.Data.M8 = "-" + employee.Data.M8;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            employee.ErrorM8 = errorMoneyIsNumber;
                            employee.Data.M8 = "-" + employee.Data.M8;
                            return
                        }
                        else {
                            employee.ErrorM8 = "";
                            var x = employee.Data.M8.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var x1 = x.split(",");
                            var list = x1.join("").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            employee.Data.M8 = "-" + list;
                        }

                    }
                    else {

                        var position = employee.Data.M8.indexOf('.');
                        if (position != -1) {
                            var x = employee.Data.M8.split(".");
                            var z = x[1].toString();
                            if (z.length > 2) {
                                z = z.substring(0, 2);
                                employee.Data.M8 = x[0] + "." + z;
                                return
                            }
                            else {
                                employee.ErrorM8 = "";
                            }
                        }
                        var position = employee.Data.M8.indexOf(',');
                        if (position != -1) {
                            var x = employee.Data.M8.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN(employee.Data.M8)) {
                            employee.ErrorM8 = errorMoneyIsNumber;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            employee.ErrorM8 = errorMoneyIsNumber;
                            return
                        }
                        else {
                            employee.ErrorM8 = "";
                            var x = employee.Data.M8.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var x1 = x.split(",");
                            var list = x1.join("").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            employee.Data.M8 = list;

                        }
                    }

                }
                else {
                    employee.ErrorM8 = "";
                }
            }

            //---------------Bắt lỗi khi edit nhập ô tháng 9----------------------
            $scope.ChangeM9 = function (employee) {
                if (employee.Data.M9 != null && employee.Data.M9 != "") {

                    var position = employee.Data.M9.indexOf('-');
                    if (position != -1) {
                        employee.Data.M9 = employee.Data.M9.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = employee.Data.M9.indexOf('.');
                        if (position != -1) {
                            var x = employee.Data.M9.split(".");
                            var z = x[1].toString();
                            if (z.length > 2) {
                                z = z.substring(0, 2);
                                employee.Data.M9 = "-" + x[0] + "." + z;
                                return
                            }
                            else {
                                employee.ErrorM9 = "";
                                employee.Data.M9 = "-" + x[0] + "." + z;
                            }
                        }
                        employee.Data.M9 = employee.Data.M9.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = employee.Data.M9.indexOf(',');
                        if (position != -1) {
                            var x = employee.Data.M9.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN(employee.Data.M9)) {
                            employee.ErrorM9 = errorMoneyIsNumber;
                            employee.Data.M9 = "-" + employee.Data.M9;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            employee.ErrorM9 = errorMoneyIsNumber;
                            employee.Data.M9 = "-" + employee.Data.M9;
                            return
                        }
                        else {
                            employee.ErrorM9 = "";
                            var x = employee.Data.M9.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var x1 = x.split(",");
                            var list = x1.join("").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            employee.Data.M9 = "-" + list;
                        }

                    }
                    else {

                        var position = employee.Data.M9.indexOf('.');
                        if (position != -1) {
                            var x = employee.Data.M9.split(".");
                            var z = x[1].toString();
                            if (z.length > 2) {
                                z = z.substring(0, 2);
                                employee.Data.M9 = x[0] + "." + z;
                                return
                            }
                            else {
                                employee.ErrorM9 = "";
                            }
                        }
                        var position = employee.Data.M9.indexOf(',');
                        if (position != -1) {
                            var x = employee.Data.M9.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN(employee.Data.M9)) {
                            employee.ErrorM9 = errorMoneyIsNumber;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            employee.ErrorM9 = errorMoneyIsNumber;
                            return
                        }
                        else {
                            employee.ErrorM9 = "";
                            var x = employee.Data.M9.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var x1 = x.split(",");
                            var list = x1.join("").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            employee.Data.M9 = list;

                        }
                    }

                }
                else {
                    employee.ErrorM9 = "";
                }
            }

            //---------------Bắt lỗi khi edit nhập ô tháng 10----------------------
            $scope.ChangeM10 = function (employee) {
                if (employee.Data.M10 != null && employee.Data.M10 != "") {

                    var position = employee.Data.M10.indexOf('-');
                    if (position != -1) {
                        employee.Data.M10 = employee.Data.M10.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = employee.Data.M10.indexOf('.');
                        if (position != -1) {
                            var x = employee.Data.M10.split(".");
                            var z = x[1].toString();
                            if (z.length > 2) {
                                z = z.substring(0, 2);
                                employee.Data.M10 = "-" + x[0] + "." + z;
                                return
                            }
                            else {
                                employee.ErrorM10 = "";
                                employee.Data.M10 = "-" + x[0] + "." + z;
                            }
                        }
                        employee.Data.M10 = employee.Data.M10.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = employee.Data.M10.indexOf(',');
                        if (position != -1) {
                            var x = employee.Data.M10.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN(employee.Data.M10)) {
                            employee.ErrorM10 = errorMoneyIsNumber;
                            employee.Data.M10 = "-" + employee.Data.M10;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            employee.ErrorM10 = errorMoneyIsNumber;
                            employee.Data.M10 = "-" + employee.Data.M10;
                            return
                        }
                        else {
                            employee.ErrorM10 = "";
                            var x = employee.Data.M10.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var x1 = x.split(",");
                            var list = x1.join("").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            employee.Data.M10 = "-" + list;
                        }

                    }
                    else {

                        var position = employee.Data.M10.indexOf('.');
                        if (position != -1) {
                            var x = employee.Data.M10.split(".");
                            var z = x[1].toString();
                            if (z.length > 2) {
                                z = z.substring(0, 2);
                                employee.Data.M10 = x[0] + "." + z;
                                return
                            }
                            else {
                                employee.ErrorM10 = "";
                            }
                        }
                        var position = employee.Data.M10.indexOf(',');
                        if (position != -1) {
                            var x = employee.Data.M10.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN(employee.Data.M10)) {
                            employee.ErrorM10 = errorMoneyIsNumber;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            employee.ErrorM10 = errorMoneyIsNumber;
                            return
                        }
                        else {
                            employee.ErrorM10 = "";
                            var x = employee.Data.M10.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var x1 = x.split(",");
                            var list = x1.join("").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            employee.Data.M10 = list;

                        }
                    }

                }
                else {
                    employee.ErrorM10 = "";
                }
            }

            //---------------Bắt lỗi khi edit nhập ô tháng 11----------------------
            $scope.ChangeM11 = function (employee) {
                if (employee.Data.M11 != null && employee.Data.M11 != "") {

                    var position = employee.Data.M11.indexOf('-');
                    if (position != -1) {
                        employee.Data.M11 = employee.Data.M11.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = employee.Data.M11.indexOf('.');
                        if (position != -1) {
                            var x = employee.Data.M11.split(".");
                            var z = x[1].toString();
                            if (z.length > 2) {
                                z = z.substring(0, 2);
                                employee.Data.M11 = "-" + x[0] + "." + z;
                                return
                            }
                            else {
                                employee.ErrorM11 = "";
                                employee.Data.M11 = "-" + x[0] + "." + z;
                            }
                        }
                        employee.Data.M11 = employee.Data.M11.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = employee.Data.M11.indexOf(',');
                        if (position != -1) {
                            var x = employee.Data.M11.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN(employee.Data.M11)) {
                            employee.ErrorM11 = errorMoneyIsNumber;
                            employee.Data.M11 = "-" + employee.Data.M11;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            employee.ErrorM11 = errorMoneyIsNumber;
                            employee.Data.M11 = "-" + employee.Data.M11;
                            return
                        }
                        else {
                            employee.ErrorM11 = "";
                            var x = employee.Data.M11.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var x1 = x.split(",");
                            var list = x1.join("").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            employee.Data.M11 = "-" + list;
                        }

                    }
                    else {

                        var position = employee.Data.M11.indexOf('.');
                        if (position != -1) {
                            var x = employee.Data.M11.split(".");
                            var z = x[1].toString();
                            if (z.length > 2) {
                                z = z.substring(0, 2);
                                employee.Data.M11 = x[0] + "." + z;
                                return
                            }
                            else {
                                employee.ErrorM11 = "";
                            }
                        }
                        var position = employee.Data.M11.indexOf(',');
                        if (position != -1) {
                            var x = employee.Data.M11.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN(employee.Data.M11)) {
                            employee.ErrorM11 = errorMoneyIsNumber;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            employee.ErrorM11 = errorMoneyIsNumber;
                            return
                        }
                        else {
                            employee.ErrorM11 = "";
                            var x = employee.Data.M11.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var x1 = x.split(",");
                            var list = x1.join("").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            employee.Data.M11 = list;

                        }
                    }

                }
                else {
                    employee.ErrorM11 = "";
                }
            }

            //---------------Bắt lỗi khi edit nhập ô tháng 12----------------------
            $scope.ChangeM12 = function (employee) {
                if (employee.Data.M12 != null && employee.Data.M12 != "") {

                    var position = employee.Data.M12.indexOf('-');
                    if (position != -1) {
                        employee.Data.M12 = employee.Data.M12.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = employee.Data.M12.indexOf('.');
                        if (position != -1) {
                            var x = employee.Data.M12.split(".");
                            var z = x[1].toString();
                            if (z.length > 2) {
                                z = z.substring(0, 2);
                                employee.Data.M12 = "-" + x[0] + "." + z;
                                return
                            }
                            else {
                                employee.ErrorM12 = "";
                                employee.Data.M12 = "-" + x[0] + "." + z;
                            }
                        }
                        var position = employee.Data.M12.indexOf(',');
                        if (position != -1) {
                            var x = employee.Data.M12.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN(employee.Data.M12)) {
                            employee.ErrorM12 = errorMoneyIsNumber;
                            employee.Data.M12 = "-" + employee.Data.M12;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            employee.ErrorM12 = errorMoneyIsNumber;
                            employee.Data.M12 = "-" + employee.Data.M12;
                            return
                        }
                        else {
                            employee.ErrorM12 = "";
                            var x = employee.Data.M12.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var x1 = x.split(",");
                            var list = x1.join("").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            employee.Data.M12 = "-" + list;
                        }

                    }
                    else {

                        var position = employee.Data.M12.indexOf('.');
                        if (position != -1) {
                            var x = employee.Data.M12.split(".");
                            var z = x[1].toString();
                            if (z.length > 2) {
                                z = z.substring(0, 2);
                                employee.Data.M12 = x[0] + "." + z;
                                return
                            }
                            else {
                                employee.ErrorM12 = "";
                            }
                        }
                        var position = employee.Data.M12.indexOf(',');
                        if (position != -1) {
                            var x = employee.Data.M12.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN(employee.Data.M12)) {
                            employee.ErrorM12 = errorMoneyIsNumber;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            employee.ErrorM12 = errorMoneyIsNumber;
                            return
                        }
                        else {
                            employee.ErrorM12 = "";
                            var x = employee.Data.M12.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var x1 = x.split(",");
                            var list = x1.join("").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            employee.Data.M12 = list;

                        }
                    }

                }
                else {
                    employee.ErrorM12 = "";
                }
            }

            //---------------Bắt lỗi khi edit  ô trạng thái----------------------

            $scope.ChangeStatus = function (employee) {
                if (employee.Data.Status == null || employee.Data.Status == "") {
                    employee.ErrorStatus = errorStatus;
                    return;
                }
                else {
                    employee.ErrorStatus = "";
                }
            }

            //---------------Edit td----------------------
            $scope.SaveEditClick = function (tblDatas) {
                $scope.employees = tblDatas;
                for (var i = 0; i < $scope.employees.length; i++) {
                    if ($scope.employees[i].Data != null) {
                        //---------------Bắt lỗi khi chưa chọn phòng ban---------------------
                        if ($scope.employees[i].Data.UserID == null || $scope.employees[i].Data.UserID == "") {
                            $scope.employees[i].ErrorStaffName = errorSelectEmployee;
                            return;
                        }

                        //---------------Bắt lỗi khi chưa chọn phòng ban---------------------
                        if ($scope.employees[i].Data.DS_OrganizationUnitID == null || $scope.employees[i].Data.DS_OrganizationUnitID == "") {
                            $scope.employees[i].ErrorOrganizationUnit = errorDepartment;
                            return;
                        }

                        //---------------Bắt lỗi khi chưa nhập năm ---------------------
                        if ($scope.employees[i].Data.Year == null || $scope.employees[i].Data.Year == "") {
                            $scope.employees[i].ErrorYear = errorEnterYear;
                            return;
                        }

                        //---------------Bắt lỗi khi chưa chon status ---------------------
                        if ($scope.employees[i].Data.Status == null || $scope.employees[i].Data.Status == "") {
                            $scope.employees[i].ErrorStatus = errorStatus;
                            return;
                        }
                        //---------------Bắt lỗi khi edit ô tháng 1----------------------
                        if ($scope.employees[i].Data.M1 != null && $scope.employees[i].Data.M1 != "") {
                            var position = $scope.employees[i].Data.M1.indexOf('-');
                            if (position != -1) {
                                $scope.employees[i].Data.M1 = $scope.employees[i].Data.M1.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                                var position = $scope.employees[i].Data.M1.indexOf(',');
                                if (position != -1) {
                                    var x = $scope.employees[i].Data.M1.split(",");
                                    var list = x.join("");

                                }
                                else if (isNaN($scope.employees[i].Data.M1)) {
                                    $scope.employees[i].ErrorM1 = errorMoneyIsNumber;
                                    $scope.employees[i].Data.M1 = "-" + $scope.employees[i].Data.M1;
                                    return
                                }
                                if (list != null && isNaN(list)) {
                                    $scope.employees[i].ErrorM1 = errorMoneyIsNumber;
                                    $scope.employees[i].Data.M1 = "-" + $scope.employees[i].Data.M1;
                                    return
                                }
                                else {
                                    $scope.employees[i].Data.M1 = "-" + $scope.employees[i].Data.M1;
                                }

                            }
                            else {
                                var position = $scope.employees[i].Data.M1.indexOf(',');
                                if (position != -1) {
                                    var x = $scope.employees[i].Data.M1.split(",");
                                    var list = x.join("");

                                }
                                else if (isNaN($scope.employees[i].Data.M1)) {
                                    $scope.employees[i].ErrorM1 = errorMoneyIsNumber;
                                    return
                                }
                                if (list != null && isNaN(list)) {
                                    $scope.employees[i].ErrorM1 = errorMoneyIsNumber;
                                    return
                                }
                            }
                        }

                        //---------------Bắt lỗi khi edit ô tháng 2----------------------
                        if ($scope.employees[i].Data.M2 != null && $scope.employees[i].Data.M2 != "") {

                            var position = $scope.employees[i].Data.M2.indexOf('-');
                            if (position != -1) {
                                $scope.employees[i].Data.M2 = $scope.employees[i].Data.M2.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                                var position = $scope.employees[i].Data.M2.indexOf(',');
                                if (position != -1) {
                                    var x = $scope.employees[i].Data.M2.split(",");
                                    var list = x.join("");

                                }
                                else if (isNaN($scope.employees[i].Data.M2)) {
                                    $scope.employees[i].ErrorM2 = errorMoneyIsNumber;
                                    $scope.employees[i].Data.M2 = "-" + $scope.employees[i].Data.M2;
                                    return
                                }
                                if (list != null && isNaN(list)) {
                                    $scope.employees[i].ErrorM2 = errorMoneyIsNumber;
                                    $scope.employees[i].Data.M2 = "-" + $scope.employees[i].Data.M2;
                                    return
                                }
                                else {
                                    $scope.employees[i].Data.M2 = "-" + $scope.employees[i].Data.M2;
                                }

                            }
                            else {
                                var position = $scope.employees[i].Data.M2.indexOf(',');
                                if (position != -1) {
                                    var x = $scope.employees[i].Data.M2.split(",");
                                    var list = x.join("");

                                }
                                else if (isNaN($scope.employees[i].Data.M2)) {
                                    $scope.employees[i].ErrorM2 = errorMoneyIsNumber;
                                    return
                                }
                                if (list != null && isNaN(list)) {
                                    $scope.employees[i].ErrorM2 = errorMoneyIsNumber;
                                    return
                                }
                            }
                        }

                        //---------------Bắt lỗi khi edit ô tháng 3----------------------
                        if ($scope.employees[i].Data.M3 != null && $scope.employees[i].Data.M3 != "") {

                            var position = $scope.employees[i].Data.M3.indexOf('-');
                            if (position != -1) {
                                $scope.employees[i].Data.M3 = $scope.employees[i].Data.M3.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                                var position = $scope.employees[i].Data.M3.indexOf(',');
                                if (position != -1) {
                                    var x = $scope.employees[i].Data.M3.split(",");
                                    var list = x.join("");

                                }
                                else if (isNaN($scope.employees[i].Data.M3)) {
                                    $scope.employees[i].ErrorM3 = errorMoneyIsNumber;
                                    $scope.employees[i].Data.M3 = "-" + $scope.employees[i].Data.M3;
                                    return
                                }
                                if (list != null && isNaN(list)) {
                                    $scope.employees[i].ErrorM3 = errorMoneyIsNumber;
                                    $scope.employees[i].Data.M3 = "-" + $scope.employees[i].Data.M3;
                                    return
                                }
                                else {
                                    $scope.employees[i].Data.M3 = "-" + $scope.employees[i].Data.M3;
                                }

                            }
                            else {
                                var position = $scope.employees[i].Data.M3.indexOf(',');
                                if (position != -1) {
                                    var x = $scope.employees[i].Data.M3.split(",");
                                    var list = x.join("");

                                }
                                else if (isNaN($scope.employees[i].Data.M3)) {
                                    $scope.employees[i].ErrorM3 = errorMoneyIsNumber;
                                    return
                                }
                                if (list != null && isNaN(list)) {
                                    $scope.employees[i].ErrorM3 = errorMoneyIsNumber;
                                    return
                                }
                            }

                        }

                        //---------------Bắt lỗi khi edit ô tháng 4----------------------
                        if ($scope.employees[i].Data.M4 != null && $scope.employees[i].Data.M4 != "") {

                            var position = $scope.employees[i].Data.M4.indexOf('-');
                            if (position != -1) {
                                $scope.employees[i].Data.M4 = $scope.employees[i].Data.M4.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                                var position = $scope.employees[i].Data.M4.indexOf(',');
                                if (position != -1) {
                                    var x = $scope.employees[i].Data.M4.split(",");
                                    var list = x.join("");

                                }
                                else if (isNaN($scope.employees[i].Data.M4)) {
                                    $scope.employees[i].ErrorM4 = errorMoneyIsNumber;
                                    $scope.employees[i].Data.M4 = "-" + $scope.employees[i].Data.M4;
                                    return
                                }
                                if (list != null && isNaN(list)) {
                                    $scope.employees[i].ErrorM4 = errorMoneyIsNumber;
                                    $scope.employees[i].Data.M4 = "-" + $scope.employees[i].Data.M4;
                                    return
                                }
                                else {
                                    $scope.employees[i].Data.M4 = "-" + $scope.employees[i].Data.M4;
                                }

                            }
                            else {
                                var position = $scope.employees[i].Data.M4.indexOf(',');
                                if (position != -1) {
                                    var x = $scope.employees[i].Data.M4.split(",");
                                    var list = x.join("");

                                }
                                else if (isNaN($scope.employees[i].Data.M4)) {
                                    $scope.employees[i].ErrorM4 = errorMoneyIsNumber;
                                    return
                                }
                                if (list != null && isNaN(list)) {
                                    $scope.employees[i].ErrorM4 = errorMoneyIsNumber;
                                    return
                                }
                            }

                        }

                        //---------------Bắt lỗi khi edit ô tháng 5----------------------
                        if ($scope.employees[i].Data.M5 != null && $scope.employees[i].Data.M5 != "") {

                            var position = $scope.employees[i].Data.M5.indexOf('-');
                            if (position != -1) {
                                $scope.employees[i].Data.M5 = $scope.employees[i].Data.M5.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                                var position = $scope.employees[i].Data.M5.indexOf(',');
                                if (position != -1) {
                                    var x = $scope.employees[i].Data.M5.split(",");
                                    var list = x.join("");

                                }
                                else if (isNaN($scope.employees[i].Data.M5)) {
                                    $scope.employees[i].ErrorM5 = errorMoneyIsNumber;
                                    $scope.employees[i].Data.M5 = "-" + $scope.employees[i].Data.M5;
                                    return
                                }
                                if (list != null && isNaN(list)) {
                                    $scope.employees[i].ErrorM5 = errorMoneyIsNumber;
                                    $scope.employees[i].Data.M5 = "-" + $scope.employees[i].Data.M5;
                                    return
                                }
                                else {
                                    $scope.employees[i].Data.M5 = "-" + $scope.employees[i].Data.M5;
                                }

                            }
                            else {
                                var position = $scope.employees[i].Data.M5.indexOf(',');
                                if (position != -1) {
                                    var x = $scope.employees[i].Data.M5.split(",");
                                    var list = x.join("");

                                }
                                else if (isNaN($scope.employees[i].Data.M5)) {
                                    $scope.employees[i].ErrorM5 = errorMoneyIsNumber;
                                    return
                                }
                                if (list != null && isNaN(list)) {
                                    $scope.employees[i].ErrorM5 = errorMoneyIsNumber;
                                    return
                                }
                            }

                        }

                        //---------------Bắt lỗi khi edit ô tháng 6----------------------
                        if ($scope.employees[i].Data.M6 != null && $scope.employees[i].Data.M6 != "") {

                            var position = $scope.employees[i].Data.M6.indexOf('-');
                            if (position != -1) {
                                $scope.employees[i].Data.M6 = $scope.employees[i].Data.M6.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                                var position = $scope.employees[i].Data.M6.indexOf(',');
                                if (position != -1) {
                                    var x = $scope.employees[i].Data.M6.split(",");
                                    var list = x.join("");

                                }
                                else if (isNaN($scope.employees[i].Data.M6)) {
                                    $scope.employees[i].ErrorM6 = errorMoneyIsNumber;
                                    $scope.employees[i].Data.M6 = "-" + $scope.employees[i].Data.M6;
                                    return
                                }
                                if (list != null && isNaN(list)) {
                                    $scope.employees[i].ErrorM6 = errorMoneyIsNumber;
                                    $scope.employees[i].Data.M6 = "-" + $scope.employees[i].Data.M6;
                                    return
                                }
                                else {

                                    $scope.employees[i].Data.M6 = "-" + $scope.employees[i].Data.M6;
                                }

                            }
                            else {
                                var position = $scope.employees[i].Data.M6.indexOf(',');
                                if (position != -1) {
                                    var x = $scope.employees[i].Data.M6.split(",");
                                    var list = x.join("");

                                }
                                else if (isNaN($scope.employees[i].Data.M6)) {
                                    $scope.employees[i].ErrorM6 = errorMoneyIsNumber;
                                    return
                                }
                                if (list != null && isNaN(list)) {
                                    $scope.employees[i].ErrorM6 = errorMoneyIsNumber;
                                    return
                                }
                            }

                        }

                        //---------------Bắt lỗi khi edit ô tháng 7----------------------
                        if ($scope.employees[i].Data.M7 != null && $scope.employees[i].Data.M7 != "") {

                            var position = $scope.employees[i].Data.M7.indexOf('-');
                            if (position != -1) {
                                $scope.employees[i].Data.M7 = $scope.employees[i].Data.M7.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                                var position = $scope.employees[i].Data.M7.indexOf(',');
                                if (position != -1) {
                                    var x = $scope.employees[i].Data.M7.split(",");
                                    var list = x.join("");

                                }
                                else if (isNaN($scope.employees[i].Data.M7)) {
                                    $scope.employees[i].ErrorM7 = errorMoneyIsNumber;
                                    $scope.employees[i].Data.M7 = "-" + $scope.employees[i].Data.M7;
                                    return
                                }
                                if (list != null && isNaN(list)) {
                                    $scope.employees[i].ErrorM7 = errorMoneyIsNumber;
                                    $scope.employees[i].Data.M7 = "-" + $scope.employees[i].Data.M7;
                                    return
                                }
                                else {
                                    $scope.employees[i].Data.M7 = "-" + $scope.employees[i].Data.M7;
                                }

                            }
                            else {
                                var position = $scope.employees[i].Data.M7.indexOf(',');
                                if (position != -1) {
                                    var x = $scope.employees[i].Data.M7.split(",");
                                    var list = x.join("");

                                }
                                else if (isNaN($scope.employees[i].Data.M7)) {
                                    $scope.employees[i].ErrorM7 = errorMoneyIsNumber;
                                    return
                                }
                                if (list != null && isNaN(list)) {
                                    $scope.employees[i].ErrorM7 = errorMoneyIsNumber;
                                    return
                                }
                            }

                        }

                        //---------------Bắt lỗi khi edit ô tháng 8----------------------
                        if ($scope.employees[i].Data.M8 != null && $scope.employees[i].Data.M8 != "") {

                            var position = $scope.employees[i].Data.M8.indexOf('-');
                            if (position != -1) {
                                $scope.employees[i].Data.M8 = $scope.employees[i].Data.M8.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                                var position = $scope.employees[i].Data.M8.indexOf(',');
                                if (position != -1) {
                                    var x = $scope.employees[i].Data.M8.split(",");
                                    var list = x.join("");

                                }
                                else if (isNaN($scope.employees[i].Data.M8)) {
                                    $scope.employees[i].ErrorM8 = errorMoneyIsNumber;
                                    $scope.employees[i].Data.M8 = "-" + $scope.employees[i].Data.M8;
                                    return
                                }
                                if (list != null && isNaN(list)) {
                                    $scope.employees[i].ErrorM8 = errorMoneyIsNumber;
                                    $scope.employees[i].Data.M8 = "-" + $scope.employees[i].Data.M8;
                                    return
                                }
                                else {
                                    $scope.employees[i].Data.M8 = "-" + $scope.employees[i].Data.M8;
                                }

                            }
                            else {
                                var position = $scope.employees[i].Data.M8.indexOf(',');
                                if (position != -1) {
                                    var x = $scope.employees[i].Data.M8.split(",");
                                    var list = x.join("");

                                }
                                else if (isNaN($scope.employees[i].Data.M8)) {
                                    $scope.employees[i].ErrorM8 = errorMoneyIsNumber;
                                    return
                                }
                                if (list != null && isNaN(list)) {
                                    $scope.employees[i].ErrorM8 = errorMoneyIsNumber;
                                    return
                                }
                            }

                        }

                        //---------------Bắt lỗi khi edit ô tháng 9----------------------
                        if ($scope.employees[i].Data.M9 != null && $scope.employees[i].Data.M9 != "") {

                            var position = $scope.employees[i].Data.M9.indexOf('-');
                            if (position != -1) {
                                $scope.employees[i].Data.M9 = $scope.employees[i].Data.M9.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                                var position = $scope.employees[i].Data.M9.indexOf(',');
                                if (position != -1) {
                                    var x = $scope.employees[i].Data.M9.split(",");
                                    var list = x.join("");

                                }
                                else if (isNaN($scope.employees[i].Data.M9)) {
                                    $scope.employees[i].ErrorM9 = errorMoneyIsNumber;
                                    $scope.employees[i].Data.M9 = "-" + $scope.employees[i].Data.M9;
                                    return
                                }
                                if (list != null && isNaN(list)) {
                                    $scope.employees[i].ErrorM9 = errorMoneyIsNumber;
                                    $scope.employees[i].Data.M9 = "-" + $scope.employees[i].Data.M9;
                                    return
                                }
                                else {
                                    $scope.employees[i].ErrorM9 = "";
                                    $scope.employees[i].Data.M9 = "-" + $scope.employees[i].Data.M9;
                                }

                            }
                            else {
                                var position = $scope.employees[i].Data.M9.indexOf(',');
                                if (position != -1) {
                                    var x = $scope.employees[i].Data.M9.split(",");
                                    var list = x.join("");

                                }
                                else if (isNaN($scope.employees[i].Data.M9)) {
                                    $scope.employees[i].ErrorM9 = errorMoneyIsNumber;
                                    return
                                }
                                if (list != null && isNaN(list)) {
                                    $scope.employees[i].ErrorM9 = errorMoneyIsNumber;
                                    return
                                }
                            }

                        }

                        //---------------Bắt lỗi khi edit ô tháng 10----------------------
                        if ($scope.employees[i].Data.M10 != null && $scope.employees[i].Data.M10 != "") {

                            var position = $scope.employees[i].Data.M10.indexOf('-');
                            if (position != -1) {
                                $scope.employees[i].Data.M10 = $scope.employees[i].Data.M10.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                                var position = $scope.employees[i].Data.M10.indexOf(',');
                                if (position != -1) {
                                    var x = $scope.employees[i].Data.M10.split(",");
                                    var list = x.join("");

                                }
                                else if (isNaN($scope.employees[i].Data.M10)) {
                                    $scope.employees[i].ErrorM10 = errorMoneyIsNumber;
                                    $scope.employees[i].Data.M10 = "-" + $scope.employees[i].Data.M10;
                                    return
                                }
                                if (list != null && isNaN(list)) {
                                    $scope.employees[i].ErrorM10 = errorMoneyIsNumber;
                                    $scope.employees[i].Data.M10 = "-" + $scope.employees[i].Data.M10;
                                    return
                                }
                                else {
                                    $scope.employees[i].Data.M10 = "-" + $scope.employees[i].Data.M10;
                                }

                            }
                            else {
                                var position = $scope.employees[i].Data.M10.indexOf(',');
                                if (position != -1) {
                                    var x = $scope.employees[i].Data.M10.split(",");
                                    var list = x.join("");

                                }
                                else if (isNaN($scope.employees[i].Data.M10)) {
                                    $scope.employees[i].ErrorM10 = errorMoneyIsNumber;
                                    return
                                }
                                if (list != null && isNaN(list)) {
                                    $scope.employees[i].ErrorM10 = errorMoneyIsNumber;
                                    return
                                }
                            }

                        }

                        //---------------Bắt lỗi khi edit ô tháng 11----------------------
                        if ($scope.employees[i].Data.M11 != null && $scope.employees[i].Data.M11 != "") {

                            var position = $scope.employees[i].Data.M11.indexOf('-');
                            if (position != -1) {
                                $scope.employees[i].Data.M11 = $scope.employees[i].Data.M11.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                                var position = $scope.employees[i].Data.M11.indexOf(',');
                                if (position != -1) {
                                    var x = $scope.employees[i].Data.M11.split(",");
                                    var list = x.join("");

                                }
                                else if (isNaN($scope.employees[i].Data.M11)) {
                                    $scope.employees[i].ErrorM11 = errorMoneyIsNumber;
                                    $scope.employees[i].Data.M11 = "-" + $scope.employees[i].Data.M11;
                                    return
                                }
                                if (list != null && isNaN(list)) {
                                    $scope.employees[i].ErrorM11 = errorMoneyIsNumber;
                                    $scope.employees[i].Data.M11 = "-" + $scope.employees[i].Data.M11;
                                    return
                                }
                                else {
                                    $scope.employees[i].Data.M11 = "-" + $scope.employees[i].Data.M11;
                                }

                            }
                            else {
                                var position = $scope.employees[i].Data.M11.indexOf(',');
                                if (position != -1) {
                                    var x = $scope.employees[i].Data.M11.split(",");
                                    var list = x.join("");

                                }
                                else if (isNaN($scope.employees[i].Data.M11)) {
                                    $scope.employees[i].ErrorM11 = errorMoneyIsNumber;
                                    return
                                }
                                if (list != null && isNaN(list)) {
                                    $scope.employees[i].ErrorM11 = errorMoneyIsNumber;
                                    return
                                }
                            }

                        }

                        //---------------Bắt lỗi khi edit ô tháng 12----------------------
                        if ($scope.employees[i].Data.M12 != null && $scope.employees[i].Data.M12 != "") {
                            
                            var position = $scope.employees[i].Data.M12.indexOf('-');
                            if (position != -1) {
                                $scope.employees[i].Data.M12 = $scope.employees[i].Data.M12.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                                var position = $scope.employees[i].Data.M12.indexOf(',');
                                if (position != -1) {
                                    var x = $scope.employees[i].Data.M12.split(",");
                                    var list = x.join("");

                                }
                                else if (isNaN($scope.employees[i].Data.M12)) {
                                    $scope.employees[i].ErrorM12 = errorMoneyIsNumber;
                                    $scope.employees[i].Data.M12 = "-" + $scope.employees[i].Data.M12;
                                    return
                                }
                                if (list != null && isNaN(list)) {
                                    $scope.employees[i].ErrorM12 = errorMoneyIsNumber;
                                    $scope.employees[i].Data.M12 = "-" + $scope.employees[i].Data.M12;
                                    return
                                }
                                else {
                                    $scope.employees[i].Data.M12 = "-" + $scope.employees[i].Data.M12;
                                }

                            }
                            else {
                                var position = $scope.employees[i].Data.M12.indexOf(',');
                                if (position != -1) {
                                    var x = $scope.employees[i].Data.M12.split(",");
                                    var list = x.join("");

                                }
                                else if (isNaN($scope.employees[i].Data.M12)) {
                                    $scope.employees[i].ErrorM12 = errorMoneyIsNumber;
                                    return
                                }
                                if (list != null && isNaN(list)) {
                                    $scope.employees[i].ErrorM12 = errorMoneyIsNumber;
                                    return
                                }
                            }

                        }

                        if ($scope.employees[i].Data.DS_OrganizationUnitID == null || $scope.employees[i].Data.DS_OrganizationUnitID == "") {
                            return;
                        }
                        else {
                            //---------------Cắt chuỗi tháng 1----------------------
                            
                            if ($scope.employees[i].Data.M1 != null && $scope.employees[i].Data.M1 != "") {
                                var position = $scope.employees[i].Data.M1.indexOf('-');
                                if (position != -1) {
                                    $scope.employees[i].Data.M1 = $scope.employees[i].Data.M1.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                                    var position = $scope.employees[i].Data.M1.indexOf(',');
                                    if (position != -1) {
                                        $scope.employees[i].Data.M1 = "-" + $scope.employees[i].Data.M1.split(",").join("");
                                    }
                                    else {
                                        $scope.employees[i].Data.M1 = "-" + $scope.employees[i].Data.M1;
                                    }
                                }
                                else {
                                    var position = $scope.employees[i].Data.M1.indexOf(',');
                                    if (position != -1) {
                                        $scope.employees[i].Data.M1 = $scope.employees[i].Data.M1.split(",").join("");
                                    }
                                }
                            }

                            //---------------Cắt chuỗi tháng 2----------------------
                            if ($scope.employees[i].Data.M2 != null && $scope.employees[i].Data.M2 != "") {
                                var position = $scope.employees[i].Data.M2.indexOf('-');
                                if (position != -1) {
                                    $scope.employees[i].Data.M2 = $scope.employees[i].Data.M2.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                                    var position = $scope.employees[i].Data.M2.indexOf(',');
                                    if (position != -1) {
                                        $scope.employees[i].Data.M2 = "-" + $scope.employees[i].Data.M2.split(",").join("");
                                    }
                                    else {
                                        $scope.employees[i].Data.M2 = "-" + $scope.employees[i].Data.M2;
                                    }
                                }
                                else {
                                    var position = $scope.employees[i].Data.M2.indexOf(',');
                                    if (position != -1) {
                                        $scope.employees[i].Data.M2 = $scope.employees[i].Data.M2.split(",").join("");
                                    }
                                }
                            }

                            //---------------Cắt chuỗi tháng 3----------------------
                            if ($scope.employees[i].Data.M3 != null && $scope.employees[i].Data.M3 != "") {
                                var position = $scope.employees[i].Data.M3.indexOf('-');
                                if (position != -1) {
                                    $scope.employees[i].Data.M3 = $scope.employees[i].Data.M3.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                                    var position = $scope.employees[i].Data.M3.indexOf(',');
                                    if (position != -1) {
                                        $scope.employees[i].Data.M3 = "-" + $scope.employees[i].Data.M3.split(",").join("");
                                    }
                                    else {
                                        $scope.employees[i].Data.M3 = "-" + $scope.employees[i].Data.M3;
                                    }
                                }
                                else {
                                    var position = $scope.employees[i].Data.M3.indexOf(',');
                                    if (position != -1) {
                                        $scope.employees[i].Data.M3 = $scope.employees[i].Data.M3.split(",").join("");
                                    }
                                }
                            }

                            //---------------Cắt chuỗi tháng 4----------------------
                            if ($scope.employees[i].Data.M4 != null && $scope.employees[i].Data.M4 != "") {
                                var position = $scope.employees[i].Data.M4.indexOf('-');
                                if (position != -1) {
                                    $scope.employees[i].Data.M4 = $scope.employees[i].Data.M4.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                                    var position = $scope.employees[i].Data.M4.indexOf(',');
                                    if (position != -1) {
                                        $scope.employees[i].Data.M4 = "-" + $scope.employees[i].Data.M4.split(",").join("");
                                    }
                                    else {
                                        $scope.employees[i].Data.M4 = "-" + $scope.employees[i].Data.M4;
                                    }
                                }
                                else {
                                    var position = $scope.employees[i].Data.M4.indexOf(',');
                                    if (position != -1) {
                                        $scope.employees[i].Data.M4 = $scope.employees[i].Data.M4.split(",").join("");
                                    }
                                }
                            }

                            //---------------Cắt chuỗi tháng 5----------------------
                            if ($scope.employees[i].Data.M5 != null && $scope.employees[i].Data.M5 != "") {
                                var position = $scope.employees[i].Data.M5.indexOf('-');
                                if (position != -1) {
                                    $scope.employees[i].Data.M5 = $scope.employees[i].Data.M5.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                                    var position = $scope.employees[i].Data.M5.indexOf(',');
                                    if (position != -1) {
                                        $scope.employees[i].Data.M5 = "-" + $scope.employees[i].Data.M5.split(",").join("");
                                    }
                                    else {
                                        $scope.employees[i].Data.M5 = "-" + $scope.employees[i].Data.M5;
                                    }
                                }
                                else {
                                    var position = $scope.employees[i].Data.M5.indexOf(',');
                                    if (position != -1) {
                                        $scope.employees[i].Data.M5 = $scope.employees[i].Data.M5.split(",").join("");
                                    }
                                }
                            }

                            //---------------Cắt chuỗi tháng 6----------------------
                            if ($scope.employees[i].Data.M6 != null && $scope.employees[i].Data.M6 != "") {
                                var position = $scope.employees[i].Data.M6.indexOf('-');
                                if (position != -1) {
                                    $scope.employees[i].Data.M6 = $scope.employees[i].Data.M6.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                                    var position = $scope.employees[i].Data.M6.indexOf(',');
                                    if (position != -1) {
                                        $scope.employees[i].Data.M6 = "-" + $scope.employees[i].Data.M6.split(",").join("");
                                    }
                                    else {
                                        $scope.employees[i].Data.M6 = "-" + $scope.employees[i].Data.M6;
                                    }
                                }
                                else {
                                    var position = $scope.employees[i].Data.M6.indexOf(',');
                                    if (position != -1) {
                                        $scope.employees[i].Data.M6 = $scope.employees[i].Data.M6.split(",").join("");
                                    }
                                }
                            }

                            //---------------Cắt chuỗi tháng 7----------------------
                            if ($scope.employees[i].Data.M7 != null && $scope.employees[i].Data.M7 != "") {
                                var position = $scope.employees[i].Data.M7.indexOf('-');
                                if (position != -1) {
                                    $scope.employees[i].Data.M7 = $scope.employees[i].Data.M7.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                                    var position = $scope.employees[i].Data.M7.indexOf(',');
                                    if (position != -1) {
                                        $scope.employees[i].Data.M7 = "-" + $scope.employees[i].Data.M7.split(",").join("");
                                    }
                                    else {
                                        $scope.employees[i].Data.M7 = "-" + $scope.employees[i].Data.M7;
                                    }
                                }
                                else {
                                    var position = $scope.employees[i].Data.M7.indexOf(',');
                                    if (position != -1) {
                                        $scope.employees[i].Data.M7 = $scope.employees[i].Data.M7.split(",").join("");
                                    }
                                }
                            }

                            //---------------Cắt chuỗi tháng 8----------------------
                            if ($scope.employees[i].Data.M8 != null && $scope.employees[i].Data.M8 != "") {
                                var position = $scope.employees[i].Data.M8.indexOf('-');
                                if (position != -1) {
                                    $scope.employees[i].Data.M8 = $scope.employees[i].Data.M8.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                                    var position = $scope.employees[i].Data.M8.indexOf(',');
                                    if (position != -1) {
                                        $scope.employees[i].Data.M8 = "-" + $scope.employees[i].Data.M8.split(",").join("");
                                    }
                                    else {
                                        $scope.employees[i].Data.M8 = "-" + $scope.employees[i].Data.M8;
                                    }
                                }
                                else {
                                    var position = $scope.employees[i].Data.M8.indexOf(',');
                                    if (position != -1) {
                                        $scope.employees[i].Data.M8 = $scope.employees[i].Data.M8.split(",").join("");
                                    }
                                }
                            }

                            //---------------Cắt chuỗi tháng 9----------------------
                            if ($scope.employees[i].Data.M9 != null && $scope.employees[i].Data.M9 != "") {
                                var position = $scope.employees[i].Data.M9.indexOf('-');
                                if (position != -1) {
                                    $scope.employees[i].Data.M9 = $scope.employees[i].Data.M9.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                                    var position = $scope.employees[i].Data.M9.indexOf(',');
                                    if (position != -1) {
                                        $scope.employees[i].Data.M9 = "-" + $scope.employees[i].Data.M9.split(",").join("");
                                    }
                                    else {
                                        $scope.employees[i].Data.M9 = "-" + $scope.employees[i].Data.M9;
                                    }
                                }
                                else {
                                    var position = $scope.employees[i].Data.M9.indexOf(',');
                                    if (position != -1) {
                                        $scope.employees[i].Data.M9 = $scope.employees[i].Data.M9.split(",").join("");
                                    }
                                }
                            }

                            //---------------Cắt chuỗi tháng 10----------------------
                            if ($scope.employees[i].Data.M10 != null && $scope.employees[i].Data.M10 != "") {
                                var position = $scope.employees[i].Data.M10.indexOf('-');
                                if (position != -1) {
                                    $scope.employees[i].Data.M10 = $scope.employees[i].Data.M10.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                                    var position = $scope.employees[i].Data.M10.indexOf(',');
                                    if (position != -1) {
                                        $scope.employees[i].Data.M10 = "-" + $scope.employees[i].Data.M10.split(",").join("");
                                    }
                                    else {
                                        $scope.employees[i].Data.M10 = "-" + $scope.employees[i].Data.M10;
                                    }
                                }
                                else {
                                    var position = $scope.employees[i].Data.M10.indexOf(',');
                                    if (position != -1) {
                                        $scope.employees[i].Data.M10 = $scope.employees[i].Data.M10.split(",").join("");
                                    }
                                }
                            }

                            //---------------Cắt chuỗi tháng 11----------------------
                            if ($scope.employees[i].Data.M11 != null && $scope.employees[i].Data.M11 != "") {
                                var position = $scope.employees[i].Data.M11.indexOf('-');
                                if (position != -1) {
                                    $scope.employees[i].Data.M11 = $scope.employees[i].Data.M11.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                                    var position = $scope.employees[i].Data.M11.indexOf(',');
                                    if (position != -1) {
                                        $scope.employees[i].Data.M11 = "-" + $scope.employees[i].Data.M11.split(",").join("");
                                    }
                                    else {
                                        $scope.employees[i].Data.M11 = "-" + $scope.employees[i].Data.M11;
                                    }
                                }
                                else {
                                    var position = $scope.employees[i].Data.M11.indexOf(',');
                                    if (position != -1) {
                                        $scope.employees[i].Data.M11 = $scope.employees[i].Data.M11.split(",").join("");
                                    }
                                }
                            }

                            //---------------Cắt chuỗi tháng 12----------------------
                            if ($scope.employees[i].Data.M12 != null && $scope.employees[i].Data.M12 != "") {

                                var position = $scope.employees[i].Data.M12.indexOf('-');
                                if (position != -1) {
                                    $scope.employees[i].Data.M12 = $scope.employees[i].Data.M12.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                                    var position = $scope.employees[i].Data.M12.indexOf(',');
                                    if (position != -1) {
                                        $scope.employees[i].Data.M12 = "-" + $scope.employees[i].Data.M12.split(",").join("");
                                    }
                                    else {
                                        $scope.employees[i].Data.M12 = "-" + $scope.employees[i].Data.M12;
                                    }
                                }
                                else {
                                    var position = $scope.employees[i].Data.M12.indexOf(',');
                                    if (position != -1) {
                                        $scope.employees[i].Data.M12 = $scope.employees[i].Data.M12.split(",").join("");
                                    }
                                }
                            }

                            if ($scope.employees[i].Data.DS_OrganizationUnitID == null || $scope.employees[i].Data.DS_OrganizationUnitID == "") {
                                return;
                            }

                        }
                    }
                }
                $scope.Data = [];
                for (var i = 0; i < $scope.employees.length; i++) {
                    if ($scope.employees[i].Data != null) {
                        if ($scope.employees[i].Data.Status=='zero') {
                            $scope.employees[i].Data.Status = 0;
                        }
                        $scope.Data.push($scope.employees[i].Data);
                    }
                }
                if ($scope.Data.length > 0 && $scope.Data != null) {
                    SaveAction = myService.UpdateData("/StaffPlan/StaffPlan_Save", $scope.Data);
                    SaveAction.then(function (res) {
                        if (res.data.result.IsSuccess == true) {
                            AppendToToastr(true, notification, successfulUpdate, 500, 5000);
                            $scope.StaffPlanData.reload();
                            $scope.Data = [];
                            $scope.CloseForm();
                        }
                        else {
                            AppendToToastr(false, notification, updateFailed, 500, 5000);
                            $scope.StaffPlanData.reload();
                            $scope.Data = [];
                            $scope.CloseForm();

                        }
                    }, function (res) {
                        AppendToToastr(false, notification, errorNotiFalse);
                    });
                }
                else {
                    AppendToToastr(false, notification, errorNotFixed, 500, 5000);
                }
               

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
                
                var ID = obj.AutoID;
                var action = myService.deleteAction(ID, $scope.tableInfo.id, $scope.tableInfo.DeleteUrl);
                action.then(function (res) {
                    if (res.data.result.IsSuccess == true) {
                        AppendToToastr(res.data.result.IsSuccess, notification, deleteSuccess, 500, 5000);
                        $scope.StaffPlanData.reload();

                    }
                    else {
                        AppendToToastr(false, notification, errorNotiFalse);

                    }
                },
                    function (res) {
                        AppendToToastr(false, notification, errorNotiFalse);
                    });

            }

            // -----------------Xóa--End------------


            //-------------------Excel--------------
            $scope.ExcelClick = function () {
                var filterString = $scope.getFilterValue();
                window.location = $scope.tableInfo.ExcelUrl + "?pageIndex=" + $scope.pageIndex + "&pageSize=" + $scope.pageSizeSelected + "&filter=" + filterString;
            }
            //-------------------Excel-End----------

            //--------bắt lỗi khi chưa chọn nhân viên khi ADD------------
            $scope.ChangeAddEmployees = function () {
                if ($scope.Data.UserID == null || $scope.Data.UserID == "") {
                    $scope.ErrorStaff = errorSelectEmployee;
                    return;
                }
                else {
                    $scope.ErrorStaff = "";
                    $scope.ErrorOrganizationUnit = "";
                    for (var i = 0; i < $scope.ListEmployees.length; i++) {
                        if ($scope.Data.UserID == $scope.ListEmployees[i].UserID) {
                            $scope.Data.DS_OrganizationUnitID = $scope.ListEmployees[i].OrganizationUnitID;
                            for (var j = 0; j < $scope.getListAllOrganizationUnit.length; j++) {
                                if ($scope.getListAllOrganizationUnit[j].OrganizationUnitID == $scope.Data.DS_OrganizationUnitID) {
                                    $scope.Data.CurrencyTypeID = $scope.getListAllOrganizationUnit[j].CurrencyTypeID;
                                    break
                                }
                            }
                            
                        }
                    }
                   
                }
            }

            //--------bắt lỗi khi chưa chọn phòng ban khi ADD------------
            $scope.ChangeAddOrganizationUnit = function () {
                if ($scope.Data.DS_OrganizationUnitID == null || $scope.Data.DS_OrganizationUnitID == "") {
                    $scope.ErrorOrganizationUnit = errorDepartment;
                    $scope.Data.CurrencyTypeID = "";
                    return;
                }
                else {
                    $scope.ErrorOrganizationUnit = "";
                    for (var i = 0; i < $scope.getListAllOrganizationUnit.length; i++) {
                        if ($scope.getListAllOrganizationUnit[i].OrganizationUnitID == $scope.Data.DS_OrganizationUnitID) {
                            $scope.Data.CurrencyTypeID = $scope.getListAllOrganizationUnit[i].CurrencyTypeID;
                        }
                    }
                }
            }

            //---------------Bắt lỗi nhập ô tháng 1 khi ADD----------------------
            $scope.ChangeAddM1 = function () {
                if ($scope.Data.M1 != null && $scope.Data.M1 != "") {
                    var position = $scope.Data.M1.indexOf('-');
                    if (position != -1) {
                        $scope.Data.M1 = $scope.Data.M1.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = $scope.Data.M1.indexOf('.');
                        if (position != -1) {
                            var x = $scope.Data.M1.split(".");
                            var z = x[1].toString();
                            if (z.length > 2) {
                                z = z.substring(0, 2);
                                $scope.Data.M1 = "-" + x[0] + "." + z;
                                return
                            }
                            else {
                                $scope.ErrorM1 = "";
                                $scope.Data.M1 = "-" + x[0] + "." + z;
                            }
                        }
                        $scope.Data.M1 = $scope.Data.M1.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = $scope.Data.M1.indexOf(',');
                        if (position != -1) {
                            var x = $scope.Data.M1.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN($scope.Data.M1)) {
                            $scope.ErrorM1 = errorMoneyIsNumber;
                            $scope.Data.M1 = "-" + $scope.Data.M1;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            $scope.ErrorM1 = errorMoneyIsNumber;
                            $scope.Data.M1 = "-" + $scope.Data.M1;
                            return
                        }
                        else {
                            $scope.ErrorM1 = "";
                            $scope.Data.SumValue = 0;
                            var x = $scope.Data.M1.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var x1 = x.split(",");
                            var list = x1.join("").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            $scope.Data.M1 = "-" + list;

                        }


                    }
                    else {

                        var position = $scope.Data.M1.indexOf('.');
                        if (position != -1) {
                            var x = $scope.Data.M1.split(".");
                            var z = x[1].toString();
                            if (z.length > 2) {
                                z = z.substring(0, 2);
                                $scope.Data.M1 = x[0] + "." + z;
                                return
                            }
                            else {
                                $scope.ErrorM1 = "";
                            }
                        }
                        var position = $scope.Data.M1.indexOf(',');
                        if (position != -1) {
                            var x = $scope.Data.M1.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN($scope.Data.M1)) {
                            $scope.ErrorM1 = errorMoneyIsNumber;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            $scope.ErrorM1 = errorMoneyIsNumber;
                            return
                        }
                        else {
                            $scope.ErrorM1 = "";
                            var x = $scope.Data.M1.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var x1 = x.split(",");
                            var list = x1.join("").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            $scope.Data.M1 = list;
                        }
                    }

                }
                else {
                    $scope.ErrorM1 = "";
                }
            }

            //---------------Bắt lỗi nhập ô tháng 2 khi ADD----------------------
            $scope.ChangeAddM2 = function () {
                if ($scope.Data.M2 != null && $scope.Data.M2 != "") {
                    var position = $scope.Data.M2.indexOf('-');
                    if (position != -1) {
                        $scope.Data.M2 = $scope.Data.M2.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = $scope.Data.M2.indexOf('.');
                        if (position != -1) {
                            var x = $scope.Data.M2.split(".");
                            var z = x[1].toString();
                            if (z.length > 2) {
                                z = z.substring(0, 2);
                                $scope.Data.M2 = "-" + x[0] + "." + z;
                                return
                            }
                            else {
                                $scope.ErrorM2 = "";
                                $scope.Data.M2 = "-" + x[0] + "." + z;
                            }
                        }
                        $scope.Data.M2 = $scope.Data.M2.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = $scope.Data.M2.indexOf(',');
                        if (position != -1) {
                            var x = $scope.Data.M2.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN($scope.Data.M2)) {
                            $scope.ErrorM2 = errorMoneyIsNumber;
                            $scope.Data.M2 = "-" + $scope.Data.M2;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            $scope.ErrorM2 = errorMoneyIsNumber;
                            $scope.Data.M2 = "-" + $scope.Data.M2;
                            return
                        }
                        else {
                            $scope.ErrorM2 = "";
                            $scope.Data.SumValue = 0;
                            var x = $scope.Data.M2.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var x1 = x.split(",");
                            var list = x1.join("").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            $scope.Data.M2 = "-" + list;

                        }


                    }
                    else {

                        var position = $scope.Data.M2.indexOf('.');
                        if (position != -1) {
                            var x = $scope.Data.M2.split(".");
                            var z = x[1].toString();
                            if (z.length > 2) {
                                z = z.substring(0, 2);
                                $scope.Data.M2 = x[0] + "." + z;
                                return
                            }
                            else {
                                $scope.ErrorM2 = "";
                            }
                        }
                        var position = $scope.Data.M2.indexOf(',');
                        if (position != -1) {
                            var x = $scope.Data.M2.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN($scope.Data.M2)) {
                            $scope.ErrorM2 = errorMoneyIsNumber;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            $scope.ErrorM2 = errorMoneyIsNumber;
                            return
                        }
                        else {
                            $scope.ErrorM2 = "";
                            var x = $scope.Data.M2.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var x1 = x.split(",");
                            var list = x1.join("").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            $scope.Data.M2 = list;

                        }
                    }

                }
                else {
                    $scope.ErrorM2 = "";
                }
            }

            //---------------Bắt lỗi nhập ô tháng 3 khi ADD----------------------
            $scope.ChangeAddM3 = function () {
                if ($scope.Data.M3 != null && $scope.Data.M3 != "") {

                    var position = $scope.Data.M3.indexOf('-');
                    if (position != -1) {
                        $scope.Data.M3 = $scope.Data.M3.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = $scope.Data.M3.indexOf('.');
                        if (position != -1) {
                            var x = $scope.Data.M3.split(".");
                            var z = x[1].toString();
                            if (z.length > 2) {
                                z = z.substring(0, 2);
                                $scope.Data.M3 = "-" + x[0] + "." + z;
                                return
                            }
                            else {
                                $scope.ErrorM3 = "";
                                $scope.Data.M3 = "-" + x[0] + "." + z;
                            }
                        }
                        $scope.Data.M3 = $scope.Data.M3.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = $scope.Data.M3.indexOf(',');
                        if (position != -1) {
                            var x = $scope.Data.M3.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN($scope.Data.M3)) {
                            $scope.ErrorM3 = errorMoneyIsNumber;
                            $scope.Data.M3 = "-" + $scope.Data.M3;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            $scope.ErrorM3 = errorMoneyIsNumber;
                            $scope.Data.M3 = "-" + $scope.Data.M3;
                            return
                        }
                        else {
                            $scope.ErrorM3 = "";
                            var x = $scope.Data.M3.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var x1 = x.split(",");
                            var list = x1.join("").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            $scope.Data.M3 = "-" + list;
                        }

                    }
                    else {

                        var position = $scope.Data.M3.indexOf('.');
                        if (position != -1) {
                            var x = $scope.Data.M3.split(".");
                            var z = x[1].toString();
                            if (z.length > 2) {
                                z = z.substring(0, 2);
                                $scope.Data.M3 = x[0] + "." + z;
                                return
                            }
                            else {
                                $scope.ErrorM3 = "";
                            }
                        }
                        var position = $scope.Data.M3.indexOf(',');
                        if (position != -1) {
                            var x = $scope.Data.M3.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN($scope.Data.M3)) {
                            $scope.ErrorM3 = errorMoneyIsNumber;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            $scope.ErrorM3 = errorMoneyIsNumber;
                            return
                        }
                        else {
                            $scope.ErrorM3 = "";
                            var x = $scope.Data.M3.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var x1 = x.split(",");
                            var list = x1.join("").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            $scope.Data.M3 = list;

                        }
                    }

                }
                else {
                    $scope.ErrorM3 = "";
                }
            }

            //---------------Bắt lỗi nhập ô tháng 4 khi ADD----------------------
            $scope.ChangeAddM4 = function () {
                if ($scope.Data.M4 != null && $scope.Data.M4 != "") {

                    var position = $scope.Data.M4.indexOf('-');
                    if (position != -1) {
                        $scope.Data.M4 = $scope.Data.M4.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = $scope.Data.M4.indexOf('.');
                        if (position != -1) {
                            var x = $scope.Data.M4.split(".");
                            var z = x[1].toString();
                            if (z.length > 2) {
                                z = z.substring(0, 2);
                                $scope.Data.M4 = "-" + x[0] + "." + z;
                                return
                            }
                            else {
                                $scope.ErrorM4 = "";
                                $scope.Data.M4 = "-" + x[0] + "." + z;
                            }
                        }
                        $scope.Data.M4 = $scope.Data.M4.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = $scope.Data.M4.indexOf(',');
                        if (position != -1) {
                            var x = $scope.Data.M4.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN($scope.Data.M4)) {
                            $scope.ErrorM4 = errorMoneyIsNumber;
                            $scope.Data.M4 = "-" + $scope.Data.M4;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            $scope.ErrorM4 = errorMoneyIsNumber;
                            $scope.Data.M4 = "-" + $scope.Data.M4;
                            return
                        }
                        else {
                            $scope.ErrorM4 = "";
                            var x = $scope.Data.M4.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var x1 = x.split(",");
                            var list = x1.join("").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            $scope.Data.M4 = "-" + list;
                        }

                    }
                    else {

                        var position = $scope.Data.M4.indexOf('.');
                        if (position != -1) {
                            var x = $scope.Data.M4.split(".");
                            var z = x[1].toString();
                            if (z.length > 2) {
                                z = z.substring(0, 2);
                                $scope.Data.M4 = x[0] + "." + z;
                                return
                            }
                            else {
                                $scope.ErrorM4 = "";
                            }
                        }
                        var position = $scope.Data.M4.indexOf(',');
                        if (position != -1) {
                            var x = $scope.Data.M4.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN($scope.Data.M4)) {
                            $scope.ErrorM4 = errorMoneyIsNumber;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            $scope.ErrorM4 = errorMoneyIsNumber;
                            return
                        }
                        else {
                            $scope.ErrorM4 = "";
                            var x = $scope.Data.M4.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var x1 = x.split(",");
                            var list = x1.join("").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            $scope.Data.M4 = list;

                        }
                    }

                }
                else {
                    $scope.ErrorM4 = "";
                }
            }

            //---------------Bắt lỗi nhập ô tháng 5 khi ADD----------------------
            $scope.ChangeAddM5 = function () {
                if ($scope.Data.M5 != null && $scope.Data.M5 != "") {

                    var position = $scope.Data.M5.indexOf('-');
                    if (position != -1) {
                        $scope.Data.M5 = $scope.Data.M5.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = $scope.Data.M5.indexOf('.');
                        if (position != -1) {
                            var x = $scope.Data.M5.split(".");
                            var z = x[1].toString();
                            if (z.length > 2) {
                                z = z.substring(0, 2);
                                $scope.Data.M5 = "-" + x[0] + "." + z;
                                return
                            }
                            else {
                                $scope.ErrorM5 = "";
                                $scope.Data.M5 = "-" + x[0] + "." + z;
                            }
                        }
                        $scope.Data.M5 = $scope.Data.M5.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = $scope.Data.M5.indexOf(',');
                        if (position != -1) {
                            var x = $scope.Data.M5.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN($scope.Data.M5)) {
                            $scope.ErrorM5 = errorMoneyIsNumber;
                            $scope.Data.M5 = "-" + $scope.Data.M5;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            $scope.ErrorM5 = errorMoneyIsNumber;
                            $scope.Data.M5 = "-" + $scope.Data.M5;
                            return
                        }
                        else {
                            $scope.ErrorM5 = "";
                            var x = $scope.Data.M5.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var x1 = x.split(",");
                            var list = x1.join("").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            $scope.Data.M5 = "-" + list;
                        }

                    }
                    else {

                        var position = $scope.Data.M5.indexOf('.');
                        if (position != -1) {
                            var x = $scope.Data.M5.split(".");
                            var z = x[1].toString();
                            if (z.length > 2) {
                                z = z.substring(0, 2);
                                $scope.Data.M5 = x[0] + "." + z;
                                return
                            }
                            else {
                                $scope.ErrorM5 = "";
                            }
                        }
                        var position = $scope.Data.M5.indexOf(',');
                        if (position != -1) {
                            var x = $scope.Data.M5.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN($scope.Data.M5)) {
                            $scope.ErrorM5 = errorMoneyIsNumber;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            $scope.ErrorM5 = errorMoneyIsNumber;
                            return
                        }
                        else {
                            $scope.ErrorM5 = "";
                            var x = $scope.Data.M5.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var x1 = x.split(",");
                            var list = x1.join("").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            $scope.Data.M5 = list;

                        }
                    }

                }
                else {
                    $scope.ErrorM5 = "";
                }
            }

            //---------------Bắt lỗi nhập ô tháng 6 khi ADD----------------------
            $scope.ChangeAddM6 = function () {
                if ($scope.Data.M6 != null && $scope.Data.M6 != "") {

                    var position = $scope.Data.M6.indexOf('-');
                    if (position != -1) {
                        $scope.Data.M6 = $scope.Data.M6.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = $scope.Data.M6.indexOf('.');
                        if (position != -1) {
                            var x = $scope.Data.M6.split(".");
                            var z = x[1].toString();
                            if (z.length > 2) {
                                z = z.substring(0, 2);
                                $scope.Data.M6 = "-" + x[0] + "." + z;
                                return
                            }
                            else {
                                $scope.ErrorM6 = "";
                                $scope.Data.M6 = "-" + x[0] + "." + z;
                            }
                        }
                        $scope.Data.M6 = $scope.Data.M6.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = $scope.Data.M6.indexOf(',');
                        if (position != -1) {
                            var x = $scope.Data.M6.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN($scope.Data.M6)) {
                            $scope.ErrorM6 = errorMoneyIsNumber;
                            $scope.Data.M6 = "-" + $scope.Data.M6;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            $scope.ErrorM6 = errorMoneyIsNumber;
                            $scope.Data.M6 = "-" + $scope.Data.M6;
                            return
                        }
                        else {
                            $scope.ErrorM6 = "";
                            var x = $scope.Data.M6.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var x1 = x.split(",");
                            var list = x1.join("").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            $scope.Data.M6 = "-" + list;
                        }

                    }
                    else {

                        var position = $scope.Data.M6.indexOf('.');
                        if (position != -1) {
                            var x = $scope.Data.M6.split(".");
                            var z = x[1].toString();
                            if (z.length > 2) {
                                z = z.substring(0, 2);
                                $scope.Data.M6 = x[0] + "." + z;
                                return
                            }
                            else {
                                $scope.ErrorM6 = "";
                            }
                        }
                        var position = $scope.Data.M6.indexOf(',');
                        if (position != -1) {
                            var x = $scope.Data.M6.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN($scope.Data.M6)) {
                            $scope.ErrorM6 = errorMoneyIsNumber;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            $scope.ErrorM6 = errorMoneyIsNumber;
                            return
                        }
                        else {
                            $scope.ErrorM6 = "";
                            var x = $scope.Data.M6.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var x1 = x.split(",");
                            var list = x1.join("").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            $scope.Data.M6 = list;

                        }
                    }

                }
                else {
                    $scope.ErrorM6 = "";
                }
            }

            //---------------Bắt lỗi nhập ô tháng 7 khi ADD----------------------
            $scope.ChangeAddM7 = function () {
                if ($scope.Data.M7 != null && $scope.Data.M7 != "") {

                    var position = $scope.Data.M7.indexOf('-');
                    if (position != -1) {
                        $scope.Data.M7 = $scope.Data.M7.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = $scope.Data.M7.indexOf('.');
                        if (position != -1) {
                            var x = $scope.Data.M7.split(".");
                            var z = x[1].toString();
                            if (z.length > 2) {
                                z = z.substring(0, 2);
                                $scope.Data.M7 = "-" + x[0] + "." + z;
                                return
                            }
                            else {
                                $scope.ErrorM7 = "";
                                $scope.Data.M7 = "-" + x[0] + "." + z;
                            }
                        }
                        $scope.Data.M7 = $scope.Data.M7.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = $scope.Data.M7.indexOf(',');
                        if (position != -1) {
                            var x = $scope.Data.M7.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN($scope.Data.M7)) {
                            $scope.ErrorM7 = errorMoneyIsNumber;
                            $scope.Data.M7 = "-" + $scope.Data.M7;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            $scope.ErrorM7 = errorMoneyIsNumber;
                            $scope.Data.M7 = "-" + $scope.Data.M7;
                            return
                        }
                        else {
                            $scope.ErrorM7 = "";
                            var x = $scope.Data.M7.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var x1 = x.split(",");
                            var list = x1.join("").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            $scope.Data.M7 = "-" + list;
                        }

                    }
                    else {

                        var position = $scope.Data.M7.indexOf('.');
                        if (position != -1) {
                            var x = $scope.Data.M7.split(".");
                            var z = x[1].toString();
                            if (z.length > 2) {
                                z = z.substring(0, 2);
                                $scope.Data.M7 = x[0] + "." + z;
                                return
                            }
                            else {
                                $scope.ErrorM7 = "";
                            }
                        }
                        var position = $scope.Data.M7.indexOf(',');
                        if (position != -1) {
                            var x = $scope.Data.M7.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN($scope.Data.M7)) {
                            $scope.ErrorM7 = errorMoneyIsNumber;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            $scope.ErrorM7 = errorMoneyIsNumber;
                            return
                        }
                        else {
                            $scope.ErrorM7 = "";
                            var x = $scope.Data.M7.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var x1 = x.split(",");
                            var list = x1.join("").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            $scope.Data.M7 = list;

                        }
                    }

                }
                else {
                    $scope.ErrorM7 = "";
                }
            }

            //---------------Bắt lỗi nhập ô tháng 8 khi ADD----------------------
            $scope.ChangeAddM8 = function () {
                if ($scope.Data.M8 != null && $scope.Data.M8 != "") {

                    var position = $scope.Data.M8.indexOf('-');
                    if (position != -1) {
                        $scope.Data.M8 = $scope.Data.M8.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = $scope.Data.M8.indexOf('.');
                        if (position != -1) {
                            var x = $scope.Data.M8.split(".");
                            var z = x[1].toString();
                            if (z.length > 2) {
                                z = z.substring(0, 2);
                                $scope.Data.M8 = "-" + x[0] + "." + z;
                                return
                            }
                            else {
                                $scope.ErrorM8 = "";
                                $scope.Data.M8 = "-" + x[0] + "." + z;
                            }
                        }
                        $scope.Data.M8 = $scope.Data.M8.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = $scope.Data.M8.indexOf(',');
                        if (position != -1) {
                            var x = $scope.Data.M8.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN($scope.Data.M8)) {
                            $scope.ErrorM8 = errorMoneyIsNumber;
                            $scope.Data.M8 = "-" + $scope.Data.M8;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            $scope.ErrorM8 = errorMoneyIsNumber;
                            $scope.Data.M8 = "-" + $scope.Data.M8;
                            return
                        }
                        else {
                            $scope.ErrorM8 = "";
                            var x = $scope.Data.M8.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var x1 = x.split(",");
                            var list = x1.join("").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            $scope.Data.M8 = "-" + list;
                        }

                    }
                    else {

                        var position = $scope.Data.M8.indexOf('.');
                        if (position != -1) {
                            var x = $scope.Data.M8.split(".");
                            var z = x[1].toString();
                            if (z.length > 2) {
                                z = z.substring(0, 2);
                                $scope.Data.M8 = x[0] + "." + z;
                                return
                            }
                            else {
                                $scope.ErrorM8 = "";
                            }
                        }
                        var position = $scope.Data.M8.indexOf(',');
                        if (position != -1) {
                            var x = $scope.Data.M8.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN($scope.Data.M8)) {
                            $scope.ErrorM8 = errorMoneyIsNumber;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            $scope.ErrorM8 = errorMoneyIsNumber;
                            return
                        }
                        else {
                            $scope.ErrorM8 = "";
                            var x = $scope.Data.M8.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var x1 = x.split(",");
                            var list = x1.join("").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            $scope.Data.M8 = list;

                        }
                    }

                }
                else {
                    $scope.ErrorM8 = "";
                }
            }

            //---------------Bắt lỗi nhập ô tháng 9 khi ADD----------------------
            $scope.ChangeAddM9 = function () {
                if ($scope.Data.M9 != null && $scope.Data.M9 != "") {

                    var position = $scope.Data.M9.indexOf('-');
                    if (position != -1) {
                        $scope.Data.M9 = $scope.Data.M9.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = $scope.Data.M9.indexOf('.');
                        if (position != -1) {
                            var x = $scope.Data.M9.split(".");
                            var z = x[1].toString();
                            if (z.length > 2) {
                                z = z.substring(0, 2);
                                $scope.Data.M9 = "-" + x[0] + "." + z;
                                return
                            }
                            else {
                                $scope.ErrorM9 = "";
                                $scope.Data.M9 = "-" + x[0] + "." + z;
                            }
                        }
                        $scope.Data.M9 = $scope.Data.M9.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = $scope.Data.M9.indexOf(',');
                        if (position != -1) {
                            var x = $scope.Data.M9.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN($scope.Data.M9)) {
                            $scope.ErrorM9 = errorMoneyIsNumber;
                            $scope.Data.M9 = "-" + $scope.Data.M9;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            $scope.ErrorM9 = errorMoneyIsNumber;
                            $scope.Data.M9 = "-" + $scope.Data.M9;
                            return
                        }
                        else {
                            $scope.ErrorM9 = "";
                            var x = $scope.Data.M9.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var x1 = x.split(",");
                            var list = x1.join("").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            $scope.Data.M9 = "-" + list;
                        }

                    }
                    else {

                        var position = $scope.Data.M9.indexOf('.');
                        if (position != -1) {
                            var x = $scope.Data.M9.split(".");
                            var z = x[1].toString();
                            if (z.length > 2) {
                                z = z.substring(0, 2);
                                $scope.Data.M9 = x[0] + "." + z;
                                return
                            }
                            else {
                                $scope.ErrorM9 = "";
                            }
                        }
                        var position = $scope.Data.M9.indexOf(',');
                        if (position != -1) {
                            var x = $scope.Data.M9.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN($scope.Data.M9)) {
                            $scope.ErrorM9 = errorMoneyIsNumber;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            $scope.ErrorM9 = errorMoneyIsNumber;
                            return
                        }
                        else {
                            $scope.ErrorM9 = "";
                            var x = $scope.Data.M9.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var x1 = x.split(",");
                            var list = x1.join("").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            $scope.Data.M9 = list;

                        }
                    }

                }
                else {
                    $scope.ErrorM9 = "";
                }
            }

            //---------------Bắt lỗi nhập ô tháng 10 khi ADD----------------------
            $scope.ChangeAddM10 = function () {
                if ($scope.Data.M10 != null && $scope.Data.M10 != "") {

                    var position = $scope.Data.M10.indexOf('-');
                    if (position != -1) {
                        $scope.Data.M10 = $scope.Data.M10.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = $scope.Data.M10.indexOf('.');
                        if (position != -1) {
                            var x = $scope.Data.M10.split(".");
                            var z = x[1].toString();
                            if (z.length > 2) {
                                z = z.substring(0, 2);
                                $scope.Data.M10 = "-" + x[0] + "." + z;
                                return
                            }
                            else {
                                $scope.ErrorM10 = "";
                                $scope.Data.M10 = "-" + x[0] + "." + z;
                            }
                        }
                        $scope.Data.M10 = $scope.Data.M10.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = $scope.Data.M10.indexOf(',');
                        if (position != -1) {
                            var x = $scope.Data.M10.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN($scope.Data.M10)) {
                            $scope.ErrorM10 = errorMoneyIsNumber;
                            $scope.Data.M10 = "-" + $scope.Data.M10;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            $scope.ErrorM10 = errorMoneyIsNumber;
                            $scope.Data.M10 = "-" + $scope.Data.M10;
                            return
                        }
                        else {
                            $scope.ErrorM10 = "";
                            var x = $scope.Data.M10.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var x1 = x.split(",");
                            var list = x1.join("").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            $scope.Data.M10 = "-" + list;
                        }

                    }
                    else {

                        var position = $scope.Data.M10.indexOf('.');
                        if (position != -1) {
                            var x = $scope.Data.M10.split(".");
                            var z = x[1].toString();
                            if (z.length > 2) {
                                z = z.substring(0, 2);
                                $scope.Data.M10 = x[0] + "." + z;
                                return
                            }
                            else {
                                $scope.ErrorM10 = "";
                            }
                        }
                        var position = $scope.Data.M10.indexOf(',');
                        if (position != -1) {
                            var x = $scope.Data.M10.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN($scope.Data.M10)) {
                            $scope.ErrorM10 = errorMoneyIsNumber;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            $scope.ErrorM10 = errorMoneyIsNumber;
                            return
                        }
                        else {
                            $scope.ErrorM10 = "";
                            var x = $scope.Data.M10.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var x1 = x.split(",");
                            var list = x1.join("").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            $scope.Data.M10 = list;

                        }
                    }

                }
                else {
                    $scope.ErrorM10 = "";
                }
            }

            //---------------Bắt lỗi nhập ô tháng 11 khi ADD----------------------
            $scope.ChangeAddM11 = function () {
                if ($scope.Data.M11 != null && $scope.Data.M11 != "") {

                    var position = $scope.Data.M11.indexOf('-');
                    if (position != -1) {
                        $scope.Data.M11 = $scope.Data.M11.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = $scope.Data.M11.indexOf('.');
                        if (position != -1) {
                            var x = $scope.Data.M11.split(".");
                            var z = x[1].toString();
                            if (z.length > 2) {
                                z = z.substring(0, 2);
                                $scope.Data.M11 = "-" + x[0] + "." + z;
                                return
                            }
                            else {
                                $scope.ErrorM11 = "";
                                $scope.Data.M11 = "-" + x[0] + "." + z;
                            }
                        }
                        $scope.Data.M11 = $scope.Data.M11.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = $scope.Data.M11.indexOf(',');
                        if (position != -1) {
                            var x = $scope.Data.M11.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN($scope.Data.M11)) {
                            $scope.ErrorM11 = errorMoneyIsNumber;
                            $scope.Data.M11 = "-" + $scope.Data.M11;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            $scope.ErrorM11 = errorMoneyIsNumber;
                            $scope.Data.M11 = "-" + $scope.Data.M11;
                            return
                        }
                        else {
                            $scope.ErrorM11 = "";
                            var x = $scope.Data.M11.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var x1 = x.split(",");
                            var list = x1.join("").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            $scope.Data.M11 = "-" + list;
                        }

                    }
                    else {

                        var position = $scope.Data.M11.indexOf('.');
                        if (position != -1) {
                            var x = $scope.Data.M11.split(".");
                            var z = x[1].toString();
                            if (z.length > 2) {
                                z = z.substring(0, 2);
                                $scope.Data.M11 = x[0] + "." + z;
                                return
                            }
                            else {
                                $scope.ErrorM11 = "";
                            }
                        }
                        var position = $scope.Data.M11.indexOf(',');
                        if (position != -1) {
                            var x = $scope.Data.M11.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN($scope.Data.M11)) {
                            $scope.ErrorM11 = errorMoneyIsNumber;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            $scope.ErrorM11 = errorMoneyIsNumber;
                            return
                        }
                        else {
                            $scope.ErrorM11 = "";
                            var x = $scope.Data.M11.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var x1 = x.split(",");
                            var list = x1.join("").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            $scope.Data.M11 = list;

                        }
                    }

                }
                else {
                    $scope.ErrorM11 = "";
                }
            }

            //---------------Bắt lỗi nhập ô tháng 12 khi ADD----------------------
            $scope.ChangeAddM12 = function () {
                if ($scope.Data.M12 != null && $scope.Data.M12 != "") {
                    var position = $scope.Data.M12.indexOf('-');
                    if (position != -1) {
                        $scope.Data.M12 = $scope.Data.M12.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = $scope.Data.M12.indexOf('.');
                        if (position != -1) {
                            var x = $scope.Data.M12.split(".");
                            var z = x[1].toString();
                            if (z.length > 2) {
                                z = z.substring(0, 2);
                                $scope.Data.M12 = "-" + x[0] + "." + z;
                                return
                            }
                            else {
                                $scope.ErrorM12 = "";
                                $scope.Data.M12 = "-" + x[0] + "." + z;
                            }
                        }
                        $scope.Data.M12 = $scope.Data.M12.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = $scope.Data.M12.indexOf(',');
                        if (position != -1) {
                            var x = $scope.Data.M12.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN($scope.Data.M12)) {
                            $scope.ErrorM12 = errorMoneyIsNumber;
                            $scope.Data.M12 = "-" + $scope.Data.M12;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            $scope.ErrorM12 = errorMoneyIsNumber;
                            $scope.Data.M12 = "-" + $scope.Data.M12;
                            return
                        }
                        else {
                            $scope.ErrorM12 = "";
                            var x = $scope.Data.M12.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var x1 = x.split(",");
                            var list = x1.join("").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            $scope.Data.M12 = "-" + list;
                        }

                    }
                    else {

                        var position = $scope.Data.M12.indexOf('.');
                        if (position != -1) {
                            var x = $scope.Data.M12.split(".");
                            var z = x[1].toString();
                            if (z.length > 2) {
                                z = z.substring(0, 2);
                                $scope.Data.M12 = x[0] + "." + z;
                                return
                            }
                            else {
                                $scope.ErrorM12 = "";
                            }
                        }
                        var position = $scope.Data.M12.indexOf(',');
                        if (position != -1) {
                            var x = $scope.Data.M12.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN($scope.Data.M12)) {
                            $scope.ErrorM12 = errorMoneyIsNumber;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            $scope.ErrorM12 = errorMoneyIsNumber;
                            return
                        }
                        else {
                            $scope.ErrorM12 = "";
                            var x = $scope.Data.M12.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var x1 = x.split(",");
                            var list = x1.join("").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            $scope.Data.M12 = list;

                        }
                    }

                }
                else {
                    $scope.ErrorM12 = "";
                }
            }

            //---------------Bắt lỗi chọn ô tháng 12 khi ADD----------------------
            $scope.ChangeAddYear = function () {
                if ($scope.Data.Year == null || $scope.Data.Year == "") {
                    $scope.ErrorYear = errorYear;
                    return;
                }
                else {
                    $scope.ErrorYear = "";
                }
            }

            //---------------Bắt lỗi chọn ô trạng thái khi ADD----------------------

            $scope.ChangeAddStatus = function () {
                console.log($scope.Data)
                if ($scope.Data.Status == null || $scope.Data.Status == "") {
                    $scope.ErrorStatus = errorStatus;
                    return;
                }
                else {
                    $scope.ErrorStatus = "";
                }
            }

            // -----------------ADD Tổ chức thực hiện kế hoạch--------------
            $scope.Save = function () {
                
                //bắt lỗi nhân viên
                if ($scope.Data.UserID == null || $scope.Data.UserID == "") {
                    $scope.ErrorStaff = errorSelectEmployee;
                    return;
                }

                // bắt lỗi phòng ban
                if ($scope.Data.DS_OrganizationUnitID == null || $scope.Data.DS_OrganizationUnitID == "") {
                    $scope.ErrorOrganizationUnit = errorDepartment;
                    $scope.Data.CurrencyTypeID = "";
                    return;
                }

                // bắt lỗi trạng thái
                if ($scope.Data.Status == null || $scope.Data.Status == "") {
                    $scope.ErrorStatus = errorStatus;
                    return;
                }

                //bắt lỗi tháng 1
                if ($scope.Data.M1 != null && $scope.Data.M1 != "") {
                    var position = $scope.Data.M1.indexOf('-');
                    if (position != -1) {
                        $scope.Data.M1 = $scope.Data.M1.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = $scope.Data.M1.indexOf(',');
                        if (position != -1) {
                            var x = $scope.Data.M1.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN($scope.Data.M1)) {
                            $scope.ErrorM1 = errorMoneyIsNumber;
                            $scope.Data.M1 = "-" + $scope.Data.M1;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            $scope.ErrorM1 = errorMoneyIsNumber;
                            $scope.Data.M1 = "-" + $scope.Data.M1;
                            return
                        }
                        else {
                            $scope.Data.M1 = "-" + $scope.Data.M1;
                        }

                    }
                    else {
                        var position = $scope.Data.M1.indexOf(',');
                        if (position != -1) {
                            var x = $scope.Data.M1.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN($scope.Data.M1)) {
                            $scope.ErrorM1 = errorMoneyIsNumber;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            $scope.ErrorM1 = errorMoneyIsNumber;
                            return
                        }
                    }

                }

                //bắt lỗi tháng 2
                if ($scope.Data.M2 != null && $scope.Data.M2 != "") {
                    var position = $scope.Data.M2.indexOf('-');
                    if (position != -1) {
                        $scope.Data.M2 = $scope.Data.M2.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = $scope.Data.M2.indexOf(',');
                        if (position != -1) {
                            var x = $scope.Data.M2.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN($scope.Data.M2)) {
                            $scope.ErrorM2 = errorMoneyIsNumber;
                            $scope.Data.M2 = "-" + $scope.Data.M2;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            $scope.ErrorM2 = errorMoneyIsNumber;
                            $scope.Data.M2 = "-" + $scope.Data.M2;
                            return
                        }
                        else {
                            $scope.Data.M2 = "-" + $scope.Data.M2;
                        }


                    }
                    else {
                        var position = $scope.Data.M2.indexOf(',');
                        if (position != -1) {
                            var x = $scope.Data.M2.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN($scope.Data.M2)) {
                            $scope.ErrorM2 = errorMoneyIsNumber;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            $scope.ErrorM2 = errorMoneyIsNumber;
                            return
                        }
                    }

                }

                //bắt lỗi tháng 3
                if ($scope.Data.M3 != null && $scope.Data.M3 != "") {

                    var position = $scope.Data.M3.indexOf('-');
                    if (position != -1) {
                        $scope.Data.M3 = $scope.Data.M3.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = $scope.Data.M3.indexOf(',');
                        if (position != -1) {
                            var x = $scope.Data.M3.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN($scope.Data.M3)) {
                            $scope.ErrorM3 = errorMoneyIsNumber;
                            $scope.Data.M3 = "-" + $scope.Data.M3;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            $scope.ErrorM3 = errorMoneyIsNumber;
                            $scope.Data.M3 = "-" + $scope.Data.M3;
                            return
                        }
                        else {
                            $scope.Data.M3 = "-" + $scope.Data.M3;
                        }

                    }
                    else {
                        var position = $scope.Data.M3.indexOf(',');
                        if (position != -1) {
                            var x = $scope.Data.M3.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN($scope.Data.M3)) {
                            $scope.ErrorM3 = errorMoneyIsNumber;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            $scope.ErrorM3 = errorMoneyIsNumber;
                            return
                        }
                    }

                }

                //bắt lỗi tháng 4
                if ($scope.Data.M4 != null && $scope.Data.M4 != "") {

                    var position = $scope.Data.M4.indexOf('-');
                    if (position != -1) {
                        $scope.Data.M4 = $scope.Data.M4.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = $scope.Data.M4.indexOf(',');
                        if (position != -1) {
                            var x = $scope.Data.M4.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN($scope.Data.M4)) {
                            $scope.ErrorM4 = errorMoneyIsNumber;
                            $scope.Data.M4 = "-" + $scope.Data.M4;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            $scope.ErrorM4 = errorMoneyIsNumber;
                            $scope.Data.M4 = "-" + $scope.Data.M4;
                            return
                        }
                        else {
                            $scope.Data.M4 = "-" + $scope.Data.M4;
                        }

                    }
                    else {
                        var position = $scope.Data.M4.indexOf(',');
                        if (position != -1) {
                            var x = $scope.Data.M4.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN($scope.Data.M4)) {
                            $scope.ErrorM4 = errorMoneyIsNumber;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            $scope.ErrorM4 = errorMoneyIsNumber;
                            return
                        }
                    }

                }

                //bắt lỗi tháng 5
                if ($scope.Data.M5 != null && $scope.Data.M5 != "") {

                    var position = $scope.Data.M5.indexOf('-');
                    if (position != -1) {
                        $scope.Data.M5 = $scope.Data.M5.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = $scope.Data.M5.indexOf(',');
                        if (position != -1) {
                            var x = $scope.Data.M5.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN($scope.Data.M5)) {
                            $scope.ErrorM5 = errorMoneyIsNumber;
                            $scope.Data.M5 = "-" + $scope.Data.M5;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            $scope.ErrorM5 = errorMoneyIsNumber;
                            $scope.Data.M5 = "-" + $scope.Data.M5;
                            return
                        }
                        else {
                            $scope.Data.M5 = "-" + $scope.Data.M5;
                        }

                    }
                    else {
                        var position = $scope.Data.M5.indexOf(',');
                        if (position != -1) {
                            var x = $scope.Data.M5.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN($scope.Data.M5)) {
                            $scope.ErrorM5 = errorMoneyIsNumber;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            $scope.ErrorM5 = errorMoneyIsNumber;
                            return
                        }

                    }

                }

                //bắt lỗi tháng 6
                if ($scope.Data.M6 != null && $scope.Data.M6 != "") {

                    var position = $scope.Data.M6.indexOf('-');
                    if (position != -1) {
                        $scope.Data.M6 = $scope.Data.M6.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = $scope.Data.M6.indexOf(',');
                        if (position != -1) {
                            var x = $scope.Data.M6.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN($scope.Data.M6)) {
                            $scope.ErrorM6 = errorMoneyIsNumber;
                            $scope.Data.M6 = "-" + $scope.Data.M6;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            $scope.ErrorM6 = errorMoneyIsNumber;
                            $scope.Data.M6 = "-" + $scope.Data.M6;
                            return
                        }
                        else {
                            $scope.ErrorM6 = "";
                            var x = $scope.Data.M6.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var x1 = x.split(",");
                            var list = x1.join("").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            $scope.Data.M6 = "-" + $scope.Data.M6;
                        }

                    }
                    else {
                        var position = $scope.Data.M6.indexOf(',');
                        if (position != -1) {
                            var x = $scope.Data.M6.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN($scope.Data.M6)) {
                            $scope.ErrorM6 = errorMoneyIsNumber;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            $scope.ErrorM6 = errorMoneyIsNumber;
                            return
                        }
                    }

                }

                //bắt lỗi tháng 7
                if ($scope.Data.M7 != null && $scope.Data.M7 != "") {

                    var position = $scope.Data.M7.indexOf('-');
                    if (position != -1) {
                        $scope.Data.M7 = $scope.Data.M7.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = $scope.Data.M7.indexOf(',');
                        if (position != -1) {
                            var x = $scope.Data.M7.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN($scope.Data.M7)) {
                            $scope.ErrorM7 = errorMoneyIsNumber;
                            $scope.Data.M7 = "-" + $scope.Data.M7;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            $scope.ErrorM7 = errorMoneyIsNumber;
                            $scope.Data.M7 = "-" + $scope.Data.M7;
                            return
                        }
                        else {
                            $scope.Data.M7 = "-" + $scope.Data.M7;
                        }

                    }
                    else {
                        var position = $scope.Data.M7.indexOf(',');
                        if (position != -1) {
                            var x = $scope.Data.M7.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN($scope.Data.M7)) {
                            $scope.ErrorM7 = errorMoneyIsNumber;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            $scope.ErrorM7 = errorMoneyIsNumber;
                            return
                        }
                    }

                }

                //bắt lỗi tháng 8
                if ($scope.Data.M8 != null && $scope.Data.M8 != "") {

                    var position = $scope.Data.M8.indexOf('-');
                    if (position != -1) {
                        $scope.Data.M8 = $scope.Data.M8.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = $scope.Data.M8.indexOf(',');
                        if (position != -1) {
                            var x = $scope.Data.M8.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN($scope.Data.M8)) {
                            $scope.ErrorM8 = errorMoneyIsNumber;
                            $scope.Data.M8 = "-" + $scope.Data.M8;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            $scope.ErrorM8 = errorMoneyIsNumber;
                            $scope.Data.M8 = "-" + $scope.Data.M8;
                            return
                        }
                        else {
                            $scope.Data.M8 = "-" + $scope.Data.M8;
                        }

                    }
                    else {
                        var position = $scope.Data.M8.indexOf(',');
                        if (position != -1) {
                            var x = $scope.Data.M8.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN($scope.Data.M8)) {
                            $scope.ErrorM8 = errorMoneyIsNumber;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            $scope.ErrorM8 = errorMoneyIsNumber;
                            return
                        }
                    }

                }

                //bắt lỗi tháng 9
                if ($scope.Data.M9 != null && $scope.Data.M9 != "") {

                    var position = $scope.Data.M9.indexOf('-');
                    if (position != -1) {
                        $scope.Data.M9 = $scope.Data.M9.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = $scope.Data.M9.indexOf(',');
                        if (position != -1) {
                            var x = $scope.Data.M9.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN($scope.Data.M9)) {
                            $scope.ErrorM9 = errorMoneyIsNumber;
                            $scope.Data.M9 = "-" + $scope.Data.M9;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            $scope.ErrorM9 = errorMoneyIsNumber;
                            $scope.Data.M9 = "-" + $scope.Data.M9;
                            return
                        }
                        else {
                            $scope.Data.M9 = "-" + $scope.Data.M9;
                        }

                    }
                    else {
                        var position = $scope.Data.M9.indexOf(',');
                        if (position != -1) {
                            var x = $scope.Data.M9.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN($scope.Data.M9)) {
                            $scope.ErrorM9 = errorMoneyIsNumber;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            $scope.ErrorM9 = errorMoneyIsNumber;
                            return
                        }
                    }

                }

                //bắt lỗi tháng 10
                if ($scope.Data.M10 != null && $scope.Data.M10 != "") {

                    var position = $scope.Data.M10.indexOf('-');
                    if (position != -1) {
                        $scope.Data.M10 = $scope.Data.M10.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = $scope.Data.M10.indexOf(',');
                        if (position != -1) {
                            var x = $scope.Data.M10.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN($scope.Data.M10)) {
                            $scope.ErrorM10 = errorMoneyIsNumber;
                            $scope.Data.M10 = "-" + $scope.Data.M10;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            $scope.ErrorM10 = errorMoneyIsNumber;
                            $scope.Data.M10 = "-" + $scope.Data.M10;
                            return
                        }
                        else {
                            $scope.Data.M10 = "-" + $scope.Data.M10;
                        }

                    }
                    else {
                        var position = $scope.Data.M10.indexOf(',');
                        if (position != -1) {
                            var x = $scope.Data.M10.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN($scope.Data.M10)) {
                            $scope.ErrorM10 = errorMoneyIsNumber;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            $scope.ErrorM10 = errorMoneyIsNumber;
                            return
                        }
                    }

                }

                //bắt lỗi tháng 11
                if ($scope.Data.M11 != null && $scope.Data.M11 != "") {

                    var position = $scope.Data.M11.indexOf('-');
                    if (position != -1) {
                        $scope.Data.M11 = $scope.Data.M11.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = $scope.Data.M11.indexOf(',');
                        if (position != -1) {
                            var x = $scope.Data.M11.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN($scope.Data.M11)) {
                            $scope.ErrorM11 = errorMoneyIsNumber;
                            $scope.Data.M11 = "-" + $scope.Data.M11;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            $scope.ErrorM11 = errorMoneyIsNumber;
                            $scope.Data.M11 = "-" + $scope.Data.M11;
                            return
                        }
                        else {
                            $scope.Data.M11 = "-" + $scope.Data.M11;
                        }

                    }
                    else {
                        var position = $scope.Data.M11.indexOf(',');
                        if (position != -1) {
                            var x = $scope.Data.M11.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN($scope.Data.M11)) {
                            $scope.ErrorM11 = errorMoneyIsNumber;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            $scope.ErrorM11 = errorMoneyIsNumber;
                            return
                        }
                    }

                }

                //bắt lỗi tháng 12
                if ($scope.Data.M12 != null && $scope.Data.M12 != "") {

                    var position = $scope.Data.M12.indexOf('-');
                    if (position != -1) {
                        $scope.Data.M12 = $scope.Data.M12.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var position = $scope.Data.M12.indexOf(',');
                        if (position != -1) {
                            var x = $scope.Data.M12.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN($scope.Data.M12)) {
                            $scope.ErrorM12 = errorMoneyIsNumber;
                            $scope.Data.M12 = "-" + $scope.Data.M12;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            $scope.ErrorM12 = errorMoneyIsNumber;
                            $scope.Data.M12 = "-" + $scope.Data.M12;
                            return
                        }
                        else {
                            $scope.Data.M12 = "-" + $scope.Data.M12;
                        }

                    }
                    else {
                        var position = $scope.Data.M12.indexOf(',');
                        if (position != -1) {
                            var x = $scope.Data.M12.split(",");
                            var list = x.join("");

                        }
                        else if (isNaN($scope.Data.M12)) {
                            $scope.ErrorM12 = errorMoneyIsNumber;
                            return
                        }
                        if (list != null && isNaN(list)) {
                            $scope.ErrorM12 = errorMoneyIsNumber;
                            return
                        }
                    }

                }

                //bắt lỗi năm
                if ($scope.Data.Year == null || $scope.Data.Year == "") {
                    $scope.ErrorYear = errorYear;
                    return;
                }
                else {
                    //---------------Cắt chuỗi tháng 1----------------------
                    if ($scope.Data.M1 != null && $scope.Data.M1 != "") {
                        var position = $scope.Data.M1.indexOf('-');
                        if (position != -1) {
                            $scope.Data.M1 = $scope.Data.M1.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var position = $scope.Data.M1.indexOf(',');
                            if (position != -1) {
                                $scope.Data.M1 = "-" + $scope.Data.M1.split(",").join("");
                            }
                            else {
                                $scope.Data.M1 = "-" + $scope.Data.M1;
                            }
                        }
                        else {
                            var position = $scope.Data.M1.indexOf(',');
                            if (position != -1) {
                                $scope.Data.M1 = $scope.Data.M1.split(",").join("");
                            }
                        }
                    }

                    //---------------Cắt chuỗi tháng 2----------------------
                    if ($scope.Data.M2 != null && $scope.Data.M2 != "") {
                        var position = $scope.Data.M2.indexOf('-');
                        if (position != -1) {
                            $scope.Data.M2 = $scope.Data.M2.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var position = $scope.Data.M2.indexOf(',');
                            if (position != -1) {
                                $scope.Data.M2 = "-" + $scope.Data.M2.split(",").join("");
                            }
                            else {
                                $scope.Data.M2 = "-" + $scope.Data.M2;
                            }
                        }
                        else {
                            var position = $scope.Data.M2.indexOf(',');
                            if (position != -1) {
                                $scope.Data.M2 = $scope.Data.M2.split(",").join("");
                            }
                        }
                    }

                    //---------------Cắt chuỗi tháng 3----------------------
                    if ($scope.Data.M3 != null && $scope.Data.M3 != "") {
                        var position = $scope.Data.M3.indexOf('-');
                        if (position != -1) {
                            $scope.Data.M3 = $scope.Data.M3.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var position = $scope.Data.M3.indexOf(',');
                            if (position != -1) {
                                $scope.Data.M3 = "-" + $scope.Data.M3.split(",").join("");
                            }
                            else {
                                $scope.Data.M3 = "-" + $scope.Data.M3;
                            }
                        }
                        else {
                            var position = $scope.Data.M3.indexOf(',');
                            if (position != -1) {
                                $scope.Data.M3 = $scope.Data.M3.split(",").join("");
                            }
                        }
                    }

                    //---------------Cắt chuỗi tháng 4----------------------
                    if ($scope.Data.M4 != null && $scope.Data.M4 != "") {
                        var position = $scope.Data.M4.indexOf('-');
                        if (position != -1) {
                            $scope.Data.M4 = $scope.Data.M4.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var position = $scope.Data.M4.indexOf(',');
                            if (position != -1) {
                                $scope.Data.M4 = "-" + $scope.Data.M4.split(",").join("");
                            }
                            else {
                                $scope.Data.M4 = "-" + $scope.Data.M4;
                            }
                        }
                        else {
                            var position = $scope.Data.M4.indexOf(',');
                            if (position != -1) {
                                $scope.Data.M4 = $scope.Data.M4.split(",").join("");
                            }
                        }
                    }

                    //---------------Cắt chuỗi tháng 5----------------------
                    if ($scope.Data.M5 != null && $scope.Data.M5 != "") {
                        var position = $scope.Data.M5.indexOf('-');
                        if (position != -1) {
                            $scope.Data.M5 = $scope.Data.M5.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var position = $scope.Data.M5.indexOf(',');
                            if (position != -1) {
                                $scope.Data.M5 = "-" + $scope.Data.M5.split(",").join("");
                            }
                            else {
                                $scope.Data.M5 = "-" + $scope.Data.M5;
                            }
                        }
                        else {
                            var position = $scope.Data.M5.indexOf(',');
                            if (position != -1) {
                                $scope.Data.M5 = $scope.Data.M5.split(",").join("");
                            }
                        }
                    }

                    //---------------Cắt chuỗi tháng 6----------------------
                    if ($scope.Data.M6 != null && $scope.Data.M6 != "") {
                        var position = $scope.Data.M6.indexOf('-');
                        if (position != -1) {
                            $scope.Data.M6 = $scope.Data.M6.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var position = $scope.Data.M6.indexOf(',');
                            if (position != -1) {
                                $scope.Data.M6 = "-" + $scope.Data.M6.split(",").join("");
                            }
                            else {
                                $scope.Data.M6 = "-" + $scope.Data.M6;
                            }
                        }
                        else {
                            var position = $scope.Data.M6.indexOf(',');
                            if (position != -1) {
                                $scope.Data.M6 = $scope.Data.M6.split(",").join("");
                            }
                        }
                    }

                    //---------------Cắt chuỗi tháng 7----------------------
                    if ($scope.Data.M7 != null && $scope.Data.M7 != "") {
                        var position = $scope.Data.M7.indexOf('-');
                        if (position != -1) {
                            $scope.Data.M7 = $scope.Data.M7.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var position = $scope.Data.M7.indexOf(',');
                            if (position != -1) {
                                $scope.Data.M7 = "-" + $scope.Data.M7.split(",").join("");
                            }
                            else {
                                $scope.Data.M7 = "-" + $scope.Data.M7;
                            }
                        }
                        else {
                            var position = $scope.Data.M7.indexOf(',');
                            if (position != -1) {
                                $scope.Data.M7 = $scope.Data.M7.split(",").join("");
                            }
                        }
                    }

                    //---------------Cắt chuỗi tháng 8----------------------
                    if ($scope.Data.M8 != null && $scope.Data.M8 != "") {
                        var position = $scope.Data.M8.indexOf('-');
                        if (position != -1) {
                            $scope.Data.M8 = $scope.Data.M8.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var position = $scope.Data.M8.indexOf(',');
                            if (position != -1) {
                                $scope.Data.M8 = "-" + $scope.Data.M8.split(",").join("");
                            }
                            else {
                                $scope.Data.M8 = "-" + $scope.Data.M8;
                            }
                        }
                        else {
                            var position = $scope.Data.M8.indexOf(',');
                            if (position != -1) {
                                $scope.Data.M8 = $scope.Data.M8.split(",").join("");
                            }
                        }
                    }

                    //---------------Cắt chuỗi tháng 9----------------------
                    if ($scope.Data.M9 != null && $scope.Data.M9 != "") {
                        var position = $scope.Data.M9.indexOf('-');
                        if (position != -1) {
                            $scope.Data.M9 = $scope.Data.M9.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var position = $scope.Data.M9.indexOf(',');
                            if (position != -1) {
                                $scope.Data.M9 = "-" + $scope.Data.M9.split(",").join("");
                            }
                            else {
                                $scope.Data.M9 = "-" + $scope.Data.M9;
                            }
                        }
                        else {
                            var position = $scope.Data.M9.indexOf(',');
                            if (position != -1) {
                                $scope.Data.M9 = $scope.Data.M9.split(",").join("");
                            }
                        }
                    }

                    //---------------Cắt chuỗi tháng 10----------------------
                    if ($scope.Data.M10 != null && $scope.Data.M10 != "") {
                        var position = $scope.Data.M10.indexOf('-');
                        if (position != -1) {
                            $scope.Data.M10 = $scope.Data.M10.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var position = $scope.Data.M10.indexOf(',');
                            if (position != -1) {
                                $scope.Data.M10 = "-" + $scope.Data.M10.split(",").join("");
                            }
                            else {
                                $scope.Data.M10 = "-" + $scope.Data.M10;
                            }
                        }
                        else {
                            var position = $scope.Data.M10.indexOf(',');
                            if (position != -1) {
                                $scope.Data.M10 = $scope.Data.M10.split(",").join("");
                            }
                        }
                    }

                    //---------------Cắt chuỗi tháng 11----------------------
                    if ($scope.Data.M11 != null && $scope.Data.M11 != "") {
                        var position = $scope.Data.M11.indexOf('-');
                        if (position != -1) {
                            $scope.Data.M11 = $scope.Data.M11.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var position = $scope.Data.M11.indexOf(',');
                            if (position != -1) {
                                $scope.Data.M11 = "-" + $scope.Data.M11.split(",").join("");
                            }
                            else {
                                $scope.Data.M11 = "-" + $scope.Data.M11;
                            }
                        }
                        else {
                            var position = $scope.Data.M11.indexOf(',');
                            if (position != -1) {
                                $scope.Data.M11 = $scope.Data.M11.split(",").join("");
                            }
                        }
                    }

                    //---------------Cắt chuỗi tháng 12----------------------
                    if ($scope.Data.M12 != null && $scope.Data.M12 != "") {

                        var position = $scope.Data.M12.indexOf('-');
                        if (position != -1) {
                            $scope.Data.M12 = $scope.Data.M12.replace("-", "").toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                            var position = $scope.Data.M12.indexOf(',');
                            if (position != -1) {
                                $scope.Data.M12 = "-" + $scope.Data.M12.split(",").join("");
                            }
                            else {
                                $scope.Data.M12 = "-" + $scope.Data.M12;
                            }
                        }
                        else {
                            var position = $scope.Data.M12.indexOf(',');
                            if (position != -1) {
                                $scope.Data.M12 = $scope.Data.M12.split(",").join("");
                            }
                        }
                    }                   
                    $scope.ListData = [];
                    if ($scope.Data.Status=='zero') {
                        $scope.Data.Status = 0
                    }
                    $scope.ListData.push($scope.Data);
                    var SaveAction = myService.UpdateData("/StaffPlan/StaffPlan_Save", $scope.ListData);
                    SaveAction.then(function (res) {
                        if (res.data.result.IsSuccess == true) {
                            AppendToToastr(true, notification, successfulAdd, 500, 5000);
                            $scope.StaffPlanData.reload();
                            $scope.CloseForm();
                        }
                        else {
                            AppendToToastr(false, notification, updateFailed, 500, 5000);
                            $scope.StaffPlanData.reload();
                            $scope.CloseForm();

                        }
                    }, function (res) {
                        AppendToToastr(false, notification, errorNotiFalse);
                    });

                }


                //    }
                //}, function (res) {
                //    AppendToToastr(false, notification, errorNotiFalse);
                //});
            }

            //load lại trang khi click bỏ qua
            $scope.CloseForm = function () {
                $scope.ErrorStaff = "";
                $scope.ErrorOrganizationUnit = "";
                $scope.ErrorM1 = "";
                $scope.ErrorM2 = "";
                $scope.ErrorM3 = "";
                $scope.ErrorM4 = "";
                $scope.ErrorM5 = "";
                $scope.ErrorM6 = "";
                $scope.ErrorM7 = "";
                $scope.ErrorM8 = "";
                $scope.ErrorM9 = "";
                $scope.ErrorM10 = "";
                $scope.ErrorM11 = "";
                $scope.ErrorM2 = "";
                $scope.ErrorYear = "";
                $scope.Data = {};
                $.colorbox.close();
            }

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
    app.directive('onlyNumbers', function () {
        return {
            restrict: 'A',
            link: function (scope, elm, attrs, ctrl) {
                elm.on('keydown', function (event) {
                    if (event.shiftKey) { event.preventDefault(); return false; }
                    //console.log(event.which);
                    if ([8, 13, 27, 37, 38, 39, 40].indexOf(event.which) > -1) {
                        // backspace, enter, escape, arrows
                        return true;
                    } else if (event.which >= 49 && event.which <= 57) {
                        // numbers
                        return true;
                    } else if (event.which >= 96 && event.which <= 105) {
                        // numpad number
                        return true;
                    }
                        // else if ([110, 190].indexOf(event.which) > -1) {
                        //     // dot and numpad dot
                        //     return true;
                        // }
                    else {
                        event.preventDefault();
                        return false;
                    }
                });
            }
        }
    });
    app.directive('currencyMask', function () {
        return {
            restrict: 'A',
            require: 'ngModel',
            link: function (scope, element, attrs, ngModelController) {

                var formatNumber = function (value) {

                    value = value.toString();
                    value = value.replace(/[^0-9\.]/g, "");
                    var parts = value.split('.');
                    parts[0] = parts[0].replace(/\d{1,3}(?=(\d{3})+(?!\d))/g, "$&,");
                    if (parts[1] && parts[1].length > 2) {
                        parts[1] = parts[1].substring(0, 2);
                    }

                    return parts.join(".");
                };
                var applyFormatting = function () {
                    var value = element.val();
                    var original = value;
                    if (!value || value.length == 0) {
                        return
                    }
                    value = formatNumber(value);
                    if (value != original) {
                        element.val(value);
                        element.triggerHandler('input')
                    }
                };
                element.bind('keyup', function (e) {
                    var keycode = e.keyCode;
                    var isTextInputKey =
                        (keycode > 47 && keycode < 58) || // number keys
                        keycode == 32 || keycode == 8 || // spacebar or backspace
                        (keycode > 64 && keycode < 91) || // letter keys
                        (keycode > 95 && keycode < 112) || // numpad keys
                        (keycode > 185 && keycode < 193) || // ;=,-./` (in order)
                        (keycode > 218 && keycode < 223); // [\]' (in order)
                    if (isTextInputKey) {
                        applyFormatting();
                    }
                });
                element.bind('blur', function (evt) {
                    if (angular.isDefined(ngModelController.$modelValue)) {
                        var val = ngModelController.$modelValue.split('.');
                        if (val && val.length == 1) {
                            if (val != "") {
                                ngModelController.$setViewValue(val + '.00');
                                ngModelController.$render();
                            }
                        } else if (val && val.length == 2) {
                            if (val[1] && val[1].length == 1) {
                                ngModelController.$setViewValue(val[0] + '.' + val[1] + '0');
                                ngModelController.$render();
                            } else if (val[1].length == 0) {
                                ngModelController.$setViewValue(val[0] + '.00');
                                ngModelController.$render();
                            }
                            applyFormatting();
                        }
                    }
                })
                ngModelController.$parsers.push(function (value) {
                    if (!value || value.length == 0) {
                        return value;
                    }
                    value = value.toString();
                    value = value.replace(/[^0-9\.]/g, "");
                    return value;
                });
                ngModelController.$formatters.push(function (value) {
                    if (!value || value.length == 0) {
                        return value;
                    }
                    value = formatNumber(value);
                    return value;
                });
            }
        };
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
