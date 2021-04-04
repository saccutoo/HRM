function BuildTable4(appName, controllerName, tableUrl, Notification, Error) {
    app.controller(controllerName,
        function ($scope, myService, $filter, $rootScope, $interval, $compile) {
            $scope.maxSize = 5; // Limit number for pagination display number.
            $scope.totalCount = 0; // Total number of items in all pages. initialize as a zer
            $scope.pageIndex = 1; // Current page number. First page is 1.-->
            $scope.pageSizeSelected = 5;
            $scope.isShowFilter = false;
            $scope.typeEnds = [{ name: "Và", nameEN: "And", value: " and " }, { name: "Hoặc", nameEN: "Or", value: " or " }];
            $scope.filterColumnsChoosed = [];

            $scope.typeFilterA = [{ name: ">", nameEN: ">", value: " > '#' " }, { name: "<", nameEN: "<", value: " < '#' " }, { name: "=", nameEN: "=", value: " = '#' " }, { name: "Khác", nameEN: "Not equal", value: " != '#' " }];
            $scope.typeFilterB = [{ name: "Có chứa", nameEN: "Contains", value: " like '%#%' " }, { name: "Bằng", nameEN: "Is", value: " = '#' " }, { name: "Không chứa", nameEN: "Do not contains", value: " != '#' " }];
            $scope.emailValid = /^[a-z]+[a-z0-9._]+@[a-z]+\.[a-z.]{2,5}$/;
            // $scope.test = $filter('mm/dd/yyyy')("2018/01/01", 'yyyy/MM/dd');
            $scope.urlEmployeeBonusDiscipline = "/EmployeeBonus_Discipline/TableServerSideGetData";
            $scope.urlEmployeeBonusDiscipline2 = "/EmployeeBonus_Discipline/TableServerSideGetData2";
            $scope.GlobalListWhereParentID = {
                RelationshipID: 2160,
                Status: 2129,
                ActionID: 2175,
                GroupID:2182
            } //Global theo parentID
            $scope.EmployeeBonus = 0;
            $scope.EmployeeDiscipline = 1;

            $scope.EBTableData = {};
            $scope.EDTableData = {};

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
                        AppendToToastr(false, Notification, Error);
                    });
            }

            $scope.GetAddPermission = function (idTable) {
                var tblAction = myService.getTableAction(idTable);
                tblAction.then(function (emp) {
                    $scope.tablePermission = emp.data.result;
                    if ($scope.tablePermission.isEdit == false) {
                        $scope.is_readonly = true;
                    }
                    else {
                        $scope.is_readonly = false;
                    }
                    $scope.getColumns();
                    // $scope.BuildAddButton(emp.data.result);
                },
                    function (emp) {
                        AppendToToastr(false, Notification, Error);
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
                        AppendToToastr(false, Notification, Error);
                    });
            }

            $scope.GetListData = function () {
                
                ShowLoader();
                var data = {
                    pageIndex: $scope.pageIndex,
                    pageSize: $scope.pageSizeSelected,
                    SessionStaffID:$rootScope.StaffID,
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
                    };
                    //----compile after load data
                    //$scope.container.html($scope.htmlResult);
                    HiddenLoader();
                },
                    function (emp) {

                        AppendToToastr(false, Notification, Error);
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

            //---------------------------------------------------------------------------- List-End ------------------------------------------------------------------------------------




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
                        AppendToToastr(false, Notification, Error);
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
                if ($rootScope.idchose != "" && stringFilter != "") {
                    stringFilter += " and o.Name = N'" + $rootScope.idchose + "'";
                }
                else if ($rootScope.idchose != "") {
                    stringFilter = " o.Name = N'" + $rootScope.idchose + "'";
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





            //--------------------------------------------------------------------------- Filter-End -------------------------------------------------------------------------------------
            //-------------------Excel--------------
            $scope.ExcelBonusClick = function () {
                var filterString = $scope.getFilterValue();
                window.location = $scope.tableInfo.ExcelUrl + "?filterString=" + filterString + "&pageIndex=" +
                    $scope.pageIndex +
                    "&pageSize=" +
                    $scope.pageSizeSelected + "&Type=" + 0 + "&SessionStaffID=" + $rootScope.StaffID;
            }
            $scope.ExcelDisciplineClick = function () {
                var filterString = $scope.getFilterValue();
                window.location = $scope.tableInfo.ExcelUrl + "?filterString=" + filterString +"&pageIndex=" +
                    $scope.pageIndex +
                    "&pageSize=" +
                    $scope.pageSizeSelected + "&Type=" + 1 + "&SessionStaffID=" + $rootScope.StaffID;;
            }
            //-------------------Excel-End----------

            //----------------------------------------------------------------------------------------------------------------------------------------------------------------------



            //add 
            $scope.addClick = function () {
                $scope.editData = {
                    Type: 0,
                    CurrencyID:194
                };
                ListAction($scope.EmployeeBonus);
                ListGroup($scope.EmployeeBonus);
                ShowPopup($,
                    ".SaveEmployeeBonusDiscipline",
                    $scope.tableInfo.PopupWidth,
                    $scope.tableInfo.PopupHeight);
            }
            $scope.addClick2 = function () {

                $scope.editData = {
                    Type: 1,
                    CurrencyID: 194
                };
                ListAction($scope.EmployeeDiscipline);
                ListGroup($scope.EmployeeDiscipline);
                ShowPopup($,
                    ".SaveEmployeeBonusDiscipline",
                    $scope.tableInfo.PopupWidth,
                    $scope.tableInfo.PopupHeight);
            }

            $scope.SaveAction = function (url, form) {
                if (form.$valid) {
                    $scope.editData.StaffID = $rootScope.StaffID;
                    var updateAction = myService.UpdateData(url, $scope.editData);
                    updateAction.then(function (res) {
                        if (res.data.result.IsSuccess) {
                            $scope.GetListData();
                        }
                        ShowPopup($,
                            "#SaveContent",
                            $scope.tableInfo.PopupWidth,
                            $scope.tableInfo.PopupHeight);
                        $scope.EBTableData.reload();
                        $scope.EDTableData.reload();
                        AppendToToastr(res.data.result.IsSuccess, Notification, res.data.result.Message, 500, 5000);

                    },
                        function (res) {
                            AppendToToastr(false, Notification, Error);
                        });


                }
            }
            $scope.editClick = function (contentItem) {
                var edit = myService.getDataById(contentItem.AutoID, $scope.tableInfo.id, $scope.tableInfo.DataEditUrl);
                edit.then(function (emp) {
                    ListAction($scope.EmployeeBonus);
                    ListGroup($scope.EmployeeBonus);
                    $scope.editData = emp.data.result;
                    ShowPopup($,
                        ".SaveEmployeeBonusDiscipline",
                        $scope.tableInfo.PopupWidth,
                        $scope.tableInfo.PopupHeight);
                },
                    function (emp) {
                        AppendToToastr(false, Notification, Error);
                    });
            }
            $scope.editClick2 = function (contentItem) {
                var edit = myService.getDataById(contentItem.AutoID, $scope.tableInfo.id, $scope.tableInfo.DataEditUrl);
                edit.then(function (emp) {
                    ListAction($scope.EmployeeDiscipline);
                    ListGroup($scope.EmployeeDiscipline);
                    $scope.editData = emp.data.result;
                    ShowPopup($,
                        ".SaveEmployeeBonusDiscipline",
                        $scope.tableInfo.PopupWidth,
                        $scope.tableInfo.PopupHeight);
                },
                    function (emp) {
                        AppendToToastr(false, Notification, Error);
                    });
            }

            $scope.CloseForm = function () {
                ShowPopup($,
                    "#SaveContent",
                    $scope.tableInfo.PopupWidth,
                    $scope.tableInfo.PopupHeight);
            }

            // -----------------Xóa------------

            $scope.deleteClick = function (obj) {
                BoostrapDialogConfirm(Notification,
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
                        $scope.EBTableData.reload();
                        $scope.EDTableData.reload();
                    }
                    AppendToToastr(res.data.result.IsSuccess, Notification, res.data.result.Message, 500, 5000);
                },
                    function (res) {
                        AppendToToastr(false, Notification, Error);
                    });

            }


            // -----------------Xóa--End------------
            //dropdown Hình thức khen thưởng or kỷ luật
            function ListAction(type) {
                var data = {
                    url: "/EmployeeBonus_Discipline/GetActionByParentAndType?parentid=" + $scope.GlobalListWhereParentID.ActionID +"&type=" + type
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getListAction = res.data.result;

                }, function (res) {
                    $scope.msg = "Error";
                })
            }
            //dropdown Nhóm khen thưởng or kỷ luật
            function ListGroup(type) {
                var data = {
                    url: "/EmployeeBonus_Discipline/GetActionByParentAndType?parentid=" + $scope.GlobalListWhereParentID.GroupID + "&type=" + type
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getListGroup = res.data.result;

                }, function (res) {
                    $scope.msg = "Error";
                })
            }
        });

   
}
