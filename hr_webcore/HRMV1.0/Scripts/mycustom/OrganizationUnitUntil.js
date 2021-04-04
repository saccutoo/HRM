function BuildTable(appName, controllerName, tableUrl) {
    app.controller(controllerName,
        function ($scope, myService, $filter, $rootScope) {
            // $scope.test = $filter('mm/dd/yyyy')("2018/01/01", 'yyyy/MM/dd');
            $scope.maxSize = 5; // Limit number for pagination display number.
            $scope.totalCount = 0; // Total number of items in all pages. initialize as a zero
            $scope.pageIndex = 1; // Current page number. First page is 1.-->
            $scope.pageSizeSelected = 5;
            $scope.isShowFilter = false;
            $scope.filterColumnsChoosed = [];
            $scope.ShowDinhDang = true;
            $scope.currentRoleId = currentRoleId;

            $scope.emailValid = /^[a-zA-Z]+[a-zA-Z0-9._]+@[a-zA-Z]+\.[a-z.]{2,5}$/;
            $scope.phoneNumber = /^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$/;
            $scope.results = '1';
            $scope.is_edit = false;
            $rootScope.test = 1;
            $scope.check = 0;
            $scope.listPhone = [{ number: "096" }, { number: "098" }, { number: "032" }, { number: "033" }, { number: "037" }, { number: "038" }, { number: "039" },
            { number: "091" }, { number: "094" }, { number: "0123" }, { number: "0124" }, { number: "0125" }, { number: "0127" }, { number: "0129" }, { number: "090" },
            { number: "093" }, { number: "0120" }, { number: "0121" }, { number: "0122" }, { number: "0126" }, { number: "0128" }, { number: "092" }, { number: "0186" },
            { number: "0188" }, { number: "099" }, { number: "0199" }, { number: "095" }]

            $scope.GlobalListWhereParentID = {
                CurrencyTypeID: 3, // tiền tệ
                Status: 88, // trạng thái
                DS_BUID: 62, // loại
                CompanyType: 1622 // công ty
            }

            $scope.GetListDepartment = function () {
                var list = myService.getListDepartment();
                list.then(function (res) {
                    $scope.sourceDepartment = res.data;
                
                }, function (res) {
                    $scope.msg = "Error";
                });
            }

            $scope.GetListDepartment();
            $scope.employeeData = {};
            $scope.sourceDepartment = [];
            $scope.department = {};
            //model build table for organization
            $scope.orTableData = {}
            $scope.ChangeEmployeeData = function () {
                $scope.stringFilterValue = $scope.getFilterValue();
                //reload data by default + exten
                $scope.orTableData.reloadByFilter($scope.stringFilterValue);
            }
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
                var data = {
                    TableId: 16,
                    filter: ""
                }
                var getData = myService.GetTableData(data, '/OrganizationUnit/GetColumnByUsserID');
                getData.then(function (emp) {
                    $scope.Columns = emp.data.result;
                    $scope.netcolumns = angular.copy(emp.data.result);
                    $scope.neststart = angular.copy(emp.data.result);
                    var temp = 0;
                    for (var i = 0; i < $scope.Columns.length; i++) {
                        if ($scope.Columns[i].Visible === false) { // nếu là đối tượng cần xóa
                            $scope.Columns.splice(i - temp, 1); // thì xóa
                            temp++;
                        }
                    }
                    //$scope.GetListData();
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
            $scope.CloseForm = function () {
                $.colorbox.close();
            }
            //Định dạng cột
            $scope.DinhDangCot = function () {
                ShowPopup($,
                    "#DinhDangCot",
                    $scope.tableInfo.PopupWidth,
                    $scope.tableInfo.PopupHeight);
                var temp = 0;
                for (var i = 0; i < $scope.netcolumns.length; i++) {
                    if ($scope.netcolumns[i].Visible == false) {
                        $scope.neststart.splice(i - temp, 1);
                        temp + 1;
                        document.getElementById($scope.netcolumns[i].TableColumnId).checked = false;
                    }
                    else {
                        document.getElementById($scope.netcolumns[i].TableColumnId).checked = true;
                    }
                }
                $scope.tableId = $scope.neststart[0].TableId;
                $scope.Cot = $scope.neststart.length;

                setTimeout(function () {
                    $(function () {
                        $('.sortable').sortable();
                    });
                }, 50);


            }
            //Tìm kiếm cột dữ liệu
            $scope.findtextcolumn = function () {
                var data = {
                    TableId: $scope.tableId,
                    filter: $scope.txtSearchColumn
                }
                var getDataTbl = myService.GetTableData(data, '/OrganizationUnit/GetColumnByUsserID');
                getDataTbl.then(function (emp) {
                    $scope.netcolumns = angular.copy(emp.data.result);
                });
                var temp = 0;
                for (var i = 0; i < $scope.netcolumns.length; i++) {
                    for (var j = 0; j < $scope.neststart.length; j++) {
                        if ($scope.netcolumns[i].Id == $scope.neststart[j].TableColumnId) {
                            temp = 1;
                            break;
                        }
                    }
                    if (temp == 1) {
                        document.getElementById($scope.netcolumns[i].TableColumnId).checked = true;
                    }
                    else {
                        document.getElementById($scope.netcolumns[i].TableColumnId).checked = false;
                    }
                    temp = 0;
                }
            }
            //Cập nhật lại thay đổi
            $scope.SaveCustomerColumns = function () {
                //Lấy vị trí
                var temp = 0;
                var error = 1;
                for (var i = 0; i < $scope.netcolumns.length; i++) {
                    for (var j = 0; j < $scope.neststart.length; j++) {
                        if ($scope.netcolumns[i].TableColumnId == $scope.neststart[j].TableColumnId) {
                            temp = 1;
                            break;
                        }
                    }
                    var x = $("#nestable2-" + $scope.netcolumns[i].TableColumnId).index();
                    if (temp == 1) {
                        var data = {
                            TableId: $scope.tableId,
                            Visible: 'True',
                            OrderNo: x,
                            TableColumnId: $scope.netcolumns[i].TableColumnId
                        }
                        var getDataTbl = myService.GetTableData(data, '/OrganizationUnit/UpdateColumnByUsserID');
                        getDataTbl.then(function (res) {
                        },
                            function (res) {
                                error = 0;
                            });
                    }
                    else {
                        var data = {
                            TableId: $scope.tableId,
                            Visible: 'False',
                            OrderNo: x,
                            TableColumnId: $scope.netcolumns[i].TableColumnId
                        }
                        var getDataTbl = myService.GetTableData(data, '/OrganizationUnit/UpdateColumnByUsserID');
                        getDataTbl.then(function (res) {
                        },
                            function (res) {
                                error = 0;
                            });
                    }
                    temp = 0;
                }
                if (error = 0) {
                    AppendToToastr(false, notification, errorNotiFalse);
                }
                else {
                    AppendToToastr(true, notification, successfulUpdate, 500, 5000);
                    $scope.getColumns();
                    $scope.Close();
                }
            }
            //Bỏ chọn toàn bộ
            $scope.bochon = true;
            $scope.SelectAllColumns = function () {
                var len = $scope.netcolumns.length;
                if ($scope.bochon == true) {
                    for (var i = 0; i < len; i++) {
                        document.getElementById($scope.netcolumns[i].TableColumnId).checked = false;
                        $scope.neststart.splice(0, 1);
                    }
                    $scope.bochon = false;
                    $scope.Cot = 0;
                }
                else {
                    for (var i = 0; i < len; i++) {
                        document.getElementById($scope.Columns[i].TableColumnId).checked = true;
                        $scope.neststart.push($scope.Columns[i]);
                    }
                    $scope.bochon = true;
                }
                $scope.Cot = $scope.neststart.length;
                setTimeout(function () {
                    $(function () {
                        $('.sortable').sortable();
                    });
                }, 50);

            }
            //xóa tìm kiếm
            $scope.deletefind = function () {
                $scope.txtSearchColumn = "";
            }
            //Chọn check box
            $scope.SelectColumnEx = function (contentItem) {
                if (document.getElementById(contentItem.TableColumnId).checked == true) {
                    document.getElementById(contentItem.TableColumnId).checked = true;
                    $scope.neststart.push(contentItem);
                }
                else {
                    document.getElementById(contentItem.TableColumnId).checked = false;
                    for (var i = 0; i < $scope.neststart.length; i++) {
                        if ($scope.neststart[i].TableColumnId === contentItem.TableColumnId) { // nếu là đối tượng cần xóa
                            $scope.neststart.splice(i, 1); // thì xóa
                        }
                    }
                }
                $scope.Cot = $scope.neststart.length;
                setTimeout(function () {
                    $(function () {
                        $('.sortable').sortable();
                    });
                }, 50);
            }
            //Bỏ qua
            $scope.Close = function () {
                $.colorbox.close();
            }
            //Xóa đã chọn
            $scope.remove = function (contentItem) {
                document.getElementById(contentItem.TableColumnId).checked = false;
                for (var i = 0; i < $scope.neststart.length; i++) {
                    if ($scope.neststart[i].TableColumnId === contentItem.TableColumnId) { // nếu là đối tượng cần xóa
                        $scope.neststart.splice(i, 1); // thì xóa
                    }
                }
                $scope.Cot = $scope.neststart.length;
                setTimeout(function () {
                    $(function () {
                        $('.sortable').sortable();
                    });
                }, 50);
            }
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
                if ($scope.department.currentNode) {
                    stringFilter += " p.DeptChild like '%," + $scope.department.currentNode.id + ",%'";
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
                    $scope.editData.RoleId = angular.copy(contentItem.RoleId);
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
            //Dropdown ListRole 
            function ListRole() {
                var data = {
                    url: "/Common/GetListRole"
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getListRole = res.data.result;

                }, function (res) {
                    $scope.msg = "Error";
                })
            }

            //Dropdown công ty
            function ListCompany() {
                var data = {
                    url: "/Common/GetDataByGloballist?parentid=" + $scope.GlobalListWhereParentID.CompanyType
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getListCompany = res.data.result;

                }, function (res) {
                    $scope.msg = "Error";
                })
            }
            //Dropdown trạng thái
            function ListStatus() {
                var data = {
                    url: "/Common/GetDataByGloballist?parentid=" + $scope.GlobalListWhereParentID.Status
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getListStatus = res.data.result;

                }, function (res) {
                    $scope.msg = "Error";
                })
            }
            //Dropdown Khối
            function ListUnit() {
                var data = {
                    url: "/Common/GetDataByGloballist?parentid=" + "3480"
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getListUnit = res.data.result;

                }, function (res) {
                    $scope.msg = "Error";
                })
            }
            //Dropdown loại
            function ListDS_BUID() {
                var data = {
                    url: "/Common/GetDataByGloballist?parentid=" + $scope.GlobalListWhereParentID.DS_BUID
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getListDS_BUID = res.data.result;

                }, function (res) {
                    $scope.msg = "Error";
                })
            }
            //Dropdown tiền tệ
            function ListCurrencyType() {
                var data = {
                    url: "/Common/GetDataByGloballist?parentid=" + $scope.GlobalListWhereParentID.CurrencyTypeID
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getListCurrencyType = res.data.result;

                }, function (res) {
                    $scope.msg = "Error";
                })
            }
            //Dropdown công ty
            function ListCompanyOrganization() {
                var data = {
                    url: "/OrganizationUnit/GetCompany"
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getCompany = res.data.result;

                }, function (res) {
                    $scope.msg = "Error";
                })
            }
            //Dropdown phòng ban
            function ListOrganizationUnit() {
                var data = {
                    url: "/OrganizationUnit/GetOrganizationUnit?chon=0"
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getListOrganizationUnit = res.data.result;

                }, function (res) {
                    $scope.msg = "Error";
                })
            }
            //Check ký tự đặc biệt
            function CheckTenPBSpecial() {
                var nick = $scope.editData.Name;
                var splChars = "*|,\":<>[]{}`\';()@&$#%";
                for (var i = 0; i < nick.length; i++) {
                    if (splChars.indexOf(nick.charAt(i)) != -1) {
                        $scope.is_TenPBSpecial = true;
                        $scope.check = 1;
                    }
                }
            }
            // -----------------Edit--End----------
            //------------------Add---------------
            $scope.addClick = function () {
                $scope.results = '1';
                $scope.is_edit = false;
                $scope.is_add = true;
                $scope.is_MaPB = false;
                $scope.is_TenPB = false;
                $scope.is_TenENPB = false;
                $scope.is_TenPBSpecial = false;
                $scope.isphone = false;
                $rootScope.test = 0;
                $scope.editData = {
                    DS_CompanyID: null,
                    DS_UnitID: null,
                    OrderNo: 1,
                    ParentID: 0,
                    IsImplementationReport: false,
                    Type: null,
                    MarginMultiplierRate: 1
                }
                ShowPopup($,
                    "#SaveContent",
                    $scope.tableInfo.PopupWidth,
                    $scope.tableInfo.PopupHeight);

            }
            $scope.SaveAction = function (url, form) {
                $scope.isphone = false;
                if ($scope.editData.Phone != "" && $scope.editData.Phone != null) {
                    var phonecheck = $scope.editData.Phone;
                    var phonecheck1;
                    if (phonecheck.slice(0, 1) != "0" && phonecheck.slice(0, 1) != "+") {
                        $scope.isphone = true;
                    }
                    else {
                        if (phonecheck.length != 10 && phonecheck.slice(0, 1) == "0") {
                            $scope.isphone = true;
                        }
                        else if (phonecheck.length != 12 && phonecheck.slice(0, 1) == "+") {
                            $scope.isphone = true;
                        }
                    }
                    if ($scope.editData.Phone.slice(0, 3) == "+84") {
                        phonecheck1 = phonecheck.replace("+84", "0");
                    }
                    else {
                        phonecheck1 = phonecheck;
                    }
                    var phonenumber1 = phonecheck1.slice(0, 3);
                    var phonenumber2 = phonecheck1.slice(0, 4);
                    var len = $scope.listPhone.length;
                    var j = 0;
                    for (var i = 0; i < len; i++) {
                        if (phonenumber1 == $scope.listPhone[i].number || phonenumber2 == $scope.listPhone[i].number) {
                            j++;
                        }
                    }
                    if (j == 0) {
                        $scope.isphone = true;
                    }
                }
                if (form.$valid) {
                    if ($rootScope.test != 0) {
                        CheckTenPBSpecial();
                        if ($scope.check != 0 || $scope.isphone == true) {
                            $scope.check = 0;
                        }
                        else {
                            if ($scope.editData.IsImplementationReport == true) {
                                $scope.editData.IsImplementationReport = 1;
                            }
                            else {
                                $scope.editData.IsImplementationReport = 0;
                            }
                            var updateAction = myService.UpdateData(url, $scope.editData);
                            updateAction.then(function (res) {
                                if (res.data.bientrung == 1) {
                                    $scope.is_TenPB = true;
                                }
                                else if (res.data.bientrung == 2) {
                                    $scope.is_TenENPB = true;
                                }
                                else if (res.data.bientrung == 3) {
                                    $scope.is_MaPB = true;
                                }
                                else {
                                    if (res.data.result.IsSuccess) {
                                        $scope.GetListData();
                                        $.colorbox.close();
                                        $scope.orTableData.reload();
                                    }
                                    AppendToToastr(res.data.result.IsSuccess, notification, res.data.result.Message, 500, 5000);
                                }
                            },
                                function (res) {
                                    AppendToToastr(false, notification, errorNotiFalse);
                                });
                        }
                    }
                    else {
                        CheckTenPBSpecial();
                        if ($scope.check != 0 || $scope.isphone == true) {
                            $scope.check = 0;
                        }
                        else {
                            var updateAction = myService.UpdateData(url, $scope.editData);
                            updateAction.then(function (res) {
                                if (res.data.bientrung == 1) {
                                    $scope.is_TenPB = true;
                                }
                                else if (res.data.bientrung == 2) {
                                    $scope.is_TenENPB = true;
                                }
                                else if (res.data.bientrung == 3) {
                                    $scope.is_MaPB = true;
                                }
                                else {
                                    if (res.data.result.IsSuccess) {
                                        $scope.GetListData();
                                        $.colorbox.close();
                                        $scope.orTableData.reload();
                                    }
                                    AppendToToastr(res.data.result.IsSuccess, notification, res.data.result.Message, 500, 5000);
                                }
                            },
                                function (res) {
                                    AppendToToastr(false, notification, errorNotiFalse);
                                });
                        }
                    }
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
                var data = {
                    url: "/OrganizationUnit/GetEmployee?OrganizationUnitID=" + obj.OrganizationUnitID
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getListCurrencyType = res.data.result;
                    if (res.data.result != null) {
                        AppendToToastr(false, notification, relatedDataArises);
                    }
                    else {
                        var action = myService.deleteAction(obj.OrganizationUnitID, $scope.tableInfo.id, $scope.tableInfo.DeleteUrl);
                        action.then(function (res) {
                            if (res.data.result.IsSuccess) {
                                $scope.GetListData();
                                $scope.orTableData.reload();
                            }
                            AppendToToastr(res.data.result.IsSuccess, notification, res.data.result.Message, 500, 5000);
                        },
                            function (res) {
                                AppendToToastr(false, notification, errorNotiFalse);
                            });
                    }
                }, function (res) {
                    $scope.msg = "Error";
                })

            }

            // -----------------Xóa--End------------
            //-------------------Excel--------------
            $scope.ExcelClick = function () {
                var filterString = $scope.getFilterValue();
                window.location = $scope.tableInfo.ExcelUrl + "?filterString=" + filterString;
            }
            //-------------------Excel-End----------
            ListCompanyOrganization();
            ListCompany();
            ListStatus();
            ListRole();
            ListUnit();
            ListDS_BUID();
            ListCurrencyType();
            ListOrganizationUnit();
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