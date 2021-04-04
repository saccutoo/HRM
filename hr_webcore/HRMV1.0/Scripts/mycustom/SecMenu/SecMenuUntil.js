﻿
function BuildTable(appName, controllerName, tableUrl) {

    app.controller(controllerName,
        function ($scope, SecMenuService, $filter, $rootScope) {
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
            $scope.GlobalListWhereParentID = {
                MenuPositionID: 1248,
                IsActive: 88
            }
            $scope.CloseForm = function () {
                $.colorbox.close();
            }
            //-----------------List-------------

            $scope.getTableInfo = function () {

                var getData = SecMenuService.getTableInformation(tableUrl);
                console.log(tableUrl)
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
                var tblAction = SecMenuService.getTableAction(idTable);
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
                $scope.chuathaydoi = 0;
                var data = {
                    TableId: 4,
                    filter: ""
                }
                var getData = SecMenuService.GetTableData(data, '/OrganizationUnit/GetColumnByUsserID');
                getData.then(function (emp) {
                    $scope.Columns = emp.data.result;
                    $scope.netcolumns = angular.copy(emp.data.result);
                    var temp = 0;
                    var len = $scope.Columns.length;
                    for (var i = 0; i < len; i++) {
                        if ($scope.Columns[i - temp].Visible == false) { // nếu là đối tượng cần xóa
                            $scope.Columns.splice(0, 1); // thì xóa
                            temp++;
                        }
                    }
                    $scope.neststart = angular.copy($scope.Columns);
                    $scope.dodai = $scope.Columns.length + 1;
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
                var getDataTbl = SecMenuService.GetTableData(data, tableUrl);
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
            //Định dạng cột
            $scope.DinhDangCot = function () {
                ShowPopup($,
                    "#DinhDangCot",
                    $scope.tableInfo.PopupWidth,
                    $scope.tableInfo.PopupHeight);
                if ($scope.chuathaydoi == 0) {
                    for (var i = 0; i < $scope.netcolumns.length; i++) {
                        if ($scope.netcolumns[i].Visible == false) {
                            document.getElementById($scope.netcolumns[i].TableColumnId).checked = false;
                        }
                        else {
                            document.getElementById($scope.netcolumns[i].TableColumnId).checked = true;
                            //$("#" + $scope.netcolumns[i].TableColumnId).prop('checked', true);
                        }
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
                var getDataTbl = SecMenuService.GetTableData(data, '/OrganizationUnit/GetColumnByUsserID');
                getDataTbl.then(function (emp) {
                    $scope.netcolumns = angular.copy(emp.data.result);
                });
                //for (var i = 0; i < $scope.netcolumns.length; i++) {
                //    if ($scope.netcolumns[i].Visible == false) {
                //        document.getElementById($scope.netcolumns[i].TableColumnId).checked = false;
                //    }
                //    else {
                //        document.getElementById($scope.netcolumns[i].TableColumnId).checked = true;
                //    }
                //}
            }
            //Cập nhật lại thay đổi
            $scope.SaveCustomerColumns = function () {
                //Lấy vị trí
                var temp = 0;
                var error = 1;
                for (var i = 0; i < $scope.netcolumns.length; i++) {
                    for (var j = 0; j < $scope.neststart.length; j++) {
                        if (document.getElementById($scope.netcolumns[i].TableColumnId).checked == true) {
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
                        var getDataTbl = SecMenuService.GetTableData(data, '/OrganizationUnit/UpdateColumnByUsserID');
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
                        var getDataTbl = SecMenuService.GetTableData(data, '/OrganizationUnit/UpdateColumnByUsserID');
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
                $scope.chuathaydoi = 1;
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
                        document.getElementById($scope.netcolumns[i].TableColumnId).checked = true;
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
                $scope.findtextcolumn();
            }
            //Chọn check box
            $scope.SelectColumnEx = function (contentItem) {
                $scope.chuathaydoi = 1;
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
                $scope.chuathaydoi = 1;
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
                var filter = SecMenuService.getFilterColumns($scope.tableInfo.id);
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
                ListParent();
                ListStatus();
                ListMenuPosition();
                var edit = SecMenuService.getDataById(contentItem.MenuID, $scope.tableInfo.id, $scope.tableInfo.DataEditUrl);
                edit.then(function (emp) {
                    emp.data.result.IsActive = emp.data.result.IsActive ? String(1) : String(0);
                    $scope.editData = emp.data.result;
                    ShowPopup($,
                        "#SaveContent",
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
                ListParent();
                ListStatus();
                ListMenuPosition();
                $scope.editData = {

                };

                ShowPopup($,
                    "#SaveContent",
                    $scope.tableInfo.PopupWidth,
                    $scope.tableInfo.PopupHeight);

            }
            $scope.SaveAction = function (url, form) {
                if (form.$valid) {
                    $scope.editData.IsActive = $scope.editData.IsActive == 1 ? true : false;
                    var updateAction = SecMenuService.UpdateData(url, $scope.editData);
                    updateAction.then(function (res) {
                        res.data.result.IsActive = res.data.result.IsActive === 1 ? true : false;
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
                var action = SecMenuService.deleteAction(obj.MenuID, $scope.tableInfo.id, $scope.tableInfo.DeleteUrl);
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


            //------------------Get-Parent-Menu------
            function ListParent() {
                var listParent = SecMenuService.getListParentMenu();
                listParent.then(function (res) {
                    $scope.getListParent = res.data.result;
                }, function (res) {
                    $scope.msg = "Error";
                })
            }

            //------------------Get-Parent-Menu-End--

            //------------Get Status-------------
            function ListStatus() {
                var data = {
                    url: "/Common/GetDataByGloballist?parentid=" + $scope.GlobalListWhereParentID.IsActive
                }
                var listStatus = SecMenuService.getDropdown(data);
                listStatus.then(function (res) {
                    $scope.getListStatus = res.data.result;
                    console.log($scope.getListStatus)
                }, function (res) {
                    $scope.msg = "Error";
                })
            }
            //------------Get Status End---------

            //------------Get MenuPosition-------------
            function ListMenuPosition() {
                var data = {
                    url: "/Common/GetDataByGloballist?parentid=" + $scope.GlobalListWhereParentID.MenuPositionID
                }
                var listMenuPosition = SecMenuService.getDropdown(data);
                listMenuPosition.then(function (res) {
                    $scope.getListMenuPosition = res.data.result;
                }, function (res) {
                    $scope.msg = "Error";
                })
            }
            //------------Get MenuPosition End---------

            // -----------------Xóa--End------------
            //-------------------Excel--------------
            $scope.ExcelClick = function () {
                var filterString = $scope.getFilterValue();
                window.location = $scope.tableInfo.ExcelUrl + "?filterString=" + filterString;

            }
            //-------------------Excel-End----------
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
}