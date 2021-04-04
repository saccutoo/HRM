function BuildTable1(appName, controllerName, tableUrl, tableUrl2, Notification, Error, CheckWPEndDate) {
    app.controller(controllerName,
        function ($scope, myService, $filter, $rootScope) {

            // $scope.test = $filter('mm/dd/yyyy')("2018/01/01", 'yyyy/MM/dd');
            $scope.maxSize = 5; // Limit number for pagination display number.
            $scope.totalCount = 0; // Total number of items in all pages. initialize as a zer0
            $scope.pageIndex = 1; // Current page number. First page is 1.-->
            $scope.pageSizeSelected = 5;
            $scope.isShowFilter = false; $scope.typeEnds = [{ name: "Và", nameEN: "And", value: " and " }, { name: "Hoặc", nameEN: "Or", value: " or " }];
            $scope.typeFilterA = [{ name: ">", nameEN: ">", value: " > '#' " }, { name: "<", nameEN: "<", value: " < '#' " }, { name: "=", nameEN: "=", value: " = '#' " }, { name: "Khác", nameEN: "Not equal", value: " != '#' " }];
            $scope.typeFilterB = [{ name: "Có chứa", nameEN: "Contains", value: " like '%#%' " }, { name: "Bằng", nameEN: "Is", value: " = '#' " }, { name: "Không chứa", nameEN: "Do not contains", value: " != '#' " }];
            $scope.filterColumnsChoosed = [];
            $scope.emailValid = /^[a-z]+[a-z0-9._]+@[a-z]+\.[a-z.]{2,5}$/;
            $scope.urlWorkingProcess = "/WorkingProcess/TableServerSideGetData";
            //model được truyền ra từ buildtable
            $scope.wpTableData = {}

            $scope.CallData = function () {
                ListPolicy();
                ListOfficePosition();
                ListOfficeRole();
                ListStaffLevel();
                ListContractType();
                //ListCurrency();
                ListStatus();
                ListWorkingStatus();
                ListWPTypeID();
                ListCompany();
                ListOffice();
                ListConfigAllowance();
            }


            $scope.GlobalListWhereParentID = {
                Office: 2140,
                OfficeRole: 2126,
                StaffLevel: 21,
                ContractType: 2123,
                //Currency: 3,
                Status: 2129,
                WorkingStatus: 55,
                WPTypeID: 2135,
                OfficePositionID: 20
            } //Global theo parentID

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
                    $scope.getColumns2();
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
                    //$scope.GetListData();
                },
                    function (emp) {
                        AppendToToastr(false, Notification, Error);
                    });
            }
            $scope.getColumns2 = function () {
                var getData = myService.GetColumns(tableUrl2);
                getData.then(function (emp) {
                    $scope.Columns2 = emp.data.result;
                    //$scope.GetListData();
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
                    // $scope.container.html($scope.htmlResult);
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





            //--------------------------------------------------------------------------- Filter-End --------------------------------------------------------------------------------

            //-------------------Excel--------------
            $scope.ExcelClick = function () {
                var filterString = $scope.getFilterValue();
                window.location = $scope.tableInfo.ExcelUrl + "?filterString=" + filterString;
            }
            //-------------------Excel-End----------

            //----------------------------------------------------------------------------------------------------------------------------------------------------------------------

            $rootScope.OrganizationUnitID = null;
            $scope.onCompanyChange = function () {
                $rootScope.OrganizationUnitID = angular.copy($scope.editData.CompanyID);
                ListOrganizationUnit($rootScope.OrganizationUnitID);
            }

            //add working process
            $scope.addWPClick = function () {

                $scope.CallData();
                $scope.EmployeeAllowanceList = []; //mảng phụ cấp (EmployeeAllowance)
                $scope.editData = {
                    Status: 2133,
                    WorkingStatus: 879,
                    WPTypeID: 2136,
                    EmployeeAllowanceList: $scope.EmployeeAllowanceList,
                    CurrencyID: 194,
                    isShowSalary: true
                };
                $scope.editData.Iscopy = 0;
                $scope.addNewChoice = function () {
                    var newItemNo = $scope.EmployeeAllowanceList.length + 1;
                    $scope.EmployeeAllowanceList.push({});
                };
                $scope.removeChoice = function (index) {
                    $scope.EmployeeAllowanceList.splice(index, 1);
                };
                ShowPopup($,
                    ".SaveWPContent",
                    $scope.tableInfo.PopupWidth,
                    $scope.tableInfo.PopupHeight);
            }

            $scope.SaveWPAction = function (url, form) {

                if (form.$valid) {
                    if ($scope.editData.HRIDs != null && $scope.editData.HRIDs instanceof Array) {
                        $scope.editData.HRIDs = $scope.editData.HRIDs.join();
                    }
                    if ($scope.editData.Iscopy == 1) {
                        $scope.editData.WPID = 0;
                    }

                    if ($scope.editData.WPEndDate != null &&
                        new Date($scope.editData.WPEndDate).setHours(0, 0, 0, 0) < new Date($scope.editData.WPStartDate).setHours(0, 0, 0, 0)) {
                        AppendToToastr(false, Notification, CheckWPEndDate, 500, 5000);
                        return;
                    }
                    if ($scope.editData.EmployeeAllowanceList.length > 0 && $scope.editData.EmployeeAllowanceList!=null) {
                        if ($scope.editData.EmployeeAllowanceList.filter(function (item) {
                            return item &&
                                        (item.AllowanceID == null || item.AllowanceID == undefined || item.AllowanceID == 0)
                        }).length > 0) {
                            AppendToToastr(false, Notification, allowanceType_error);
                            return;
                        }
                        if ($scope.editData.EmployeeAllowanceList.filter(function (item) {
                            return item &&
                                        (item.Amount == null || item.Amount == undefined || item.Amount == 0)
                        }).length > 0) {
                            AppendToToastr(false, Notification, allowanceAmount_error);
                            return;
                        }
                        if ($scope.editData.EmployeeAllowanceList.filter(function (item) {
                            return item &&
                            (item.StartDate == null || item.StartDate == undefined);
                        }).length > 0) {
                            AppendToToastr(false, Notification, allowanceStartDate_error);
                            return;
                        }                       
                    }
                    $scope.editData.StaffID = $rootScope.StaffID;
                    var updateAction = myService.UpdateData(url, $scope.editData);
                    updateAction.then(function (res) {
                        if (res.data.result.IsSuccess) {
                            $scope.GetListData();
                            ShowPopup($,
                                "#SaveContent",
                                $scope.tableInfo.PopupWidth,
                                $scope.tableInfo.PopupHeight);
                            $scope.wpTableData.reload();

                        }
                        AppendToToastr(res.data.result.IsSuccess, Notification, res.data.result.Message, 500, 5000);
                        // $rootScope.ReloadEMPWhenSaveWP();
                    },
                        function (res) {
                            AppendToToastr(false, Notification, Error);
                        });

                } else {
                    AppendToToastr(false, Notification, Error);
                }



            }



            $scope.editWPClick = function (contentItem) {
                $scope.editData.Iscopy = 0;
                var edit = myService.getDataById(contentItem.WPID, $scope.tableInfo.id, $scope.tableInfo.DataEditUrl);
                edit.then(function (emp) {

                    $scope.CallData();
                    $scope.EmployeeAllowanceList = []; //mảng phụ cấp (EmployeeAllowance)
                    $scope.EmployeeAllowanceRemoveList = []; //mảng xóa phụ cấp (EmployeeAllowance)
                    $scope.EmployeeAllowanceList = emp.data.result1;
                    $scope.addNewChoice = function () {
                        var newItemNo = $scope.EmployeeAllowanceList.length + 1;
                        $scope.EmployeeAllowanceList.push({});
                    };
                    $scope.removeChoice = function (index) {
                        $scope.EmployeeAllowanceRemoveList.push($scope.EmployeeAllowanceList.splice(index, 1)[0]);
                    };
                    ListConfigAllowance();
                    $scope.editData = emp.data.result;
                    if ($scope.editData.HRIDs != null) {
                        $scope.editData.HRIDs = emp.data.result.HRIDs.split(',');
                    }
                    $scope.onCompanyChange();
                    $scope.editData.EmployeeAllowanceList = $scope.EmployeeAllowanceList;
                    $scope.editData.EmployeeAllowanceDeleteList = $scope.EmployeeAllowanceRemoveList;
                    $rootScope.StaffID = $scope.editData.StaffID;
                    ShowPopup($,
                        ".SaveWPContent",
                        $scope.tableInfo.PopupWidth,
                        $scope.tableInfo.PopupHeight);
                    $scope.wpTableData.reload();
                },
                    function (emp) {
                        AppendToToastr(false, Notification, Error);
                    });
            }

            $scope.IsCopyClick = function (obj) {
                BoostrapDialogConfirm(Notification,
                    "Bạn có muốn copy bản ghi này?",
                    BootstrapDialog.TYPE_WARNING,
                    $scope.IsCopy,
                    obj);
            }
            $scope.IsCopy = function (contentItem) {
                var edit = myService.getDataById(contentItem.WPID, $scope.tableInfo.id, $scope.tableInfo.DataEditUrl);
                edit.then(function (emp) {
                    $scope.CallData();
                    $scope.EmployeeAllowanceList = []; //mảng phụ cấp (EmployeeAllowance)
                    $scope.EmployeeAllowanceRemoveList = []; //mảng xóa phụ cấp (EmployeeAllowance)
                    $scope.EmployeeAllowanceList = emp.data.result1;

                    $scope.addNewChoice = function () {
                        var newItemNo = $scope.EmployeeAllowanceList.length + 1;
                        $scope.EmployeeAllowanceList.push({});
                    };
                    $scope.removeChoice = function (index) {
                        $scope.EmployeeAllowanceRemoveList.push($scope.EmployeeAllowanceList.splice(index, 1)[0]);
                    };

                    ListConfigAllowance();
                    $scope.editData = emp.data.result;
                    if ($scope.editData.HRIDs != null) {
                        $scope.editData.HRIDs = emp.data.result.HRIDs.split(',');
                    }
                    $scope.onCompanyChange();
                    $scope.editData.EmployeeAllowanceList = $scope.EmployeeAllowanceList;
                    $scope.editData.EmployeeAllowanceDeleteList = $scope.EmployeeAllowanceRemoveList;
                    $scope.editData.Iscopy = 1;
                    ShowPopup($,
                        ".SaveWPContent",
                        $scope.tableInfo.PopupWidth,
                        $scope.tableInfo.PopupHeight);
                    $scope.wpTableData.reload();
                },
                    function (emp) {
                        AppendToToastr(false, Notification, Error);
                    });
            }

            $scope.CloseFormWP = function () {
                ShowPopup($,
                    "#SaveContent",
                    $scope.tableInfo.PopupWidth,
                    $scope.tableInfo.PopupHeight);
            }

            // -----------------Xóa------------

            $scope.deleteWPClick = function (obj) {
                BoostrapDialogConfirm(Notification,
                    notificationDelete,
                    BootstrapDialog.TYPE_WARNING,
                    $scope.deleteActionClick,
                    obj);
            }
            $scope.deleteActionClick = function (obj) {
                var action = myService.deleteAction(obj.WPID, $scope.tableInfo.id, $scope.tableInfo.DeleteUrl);
                action.then(function (res) {
                    if (res.data.result.IsSuccess) {
                        $scope.GetListData();
                        $scope.wpTableData.reload();
                        AppendToToastr(res.data.result.IsSuccess, Notification, res.data.result.Message, 500, 5000);
                    }

                },
                    function (res) {
                        AppendToToastr(false, Notification, Error);
                    });

            }

            // -----------------Xóa--End------------
            //$scope.AllowanceChange = function (array, key, id, index) {
            //    for (var i = 0; i < array.length; i++) {

            //        if (array[i][key] == id) {
            //            $scope.IndexAmount = i;
            //            ListConfigAllowance($scope.IndexAmount,index);
            //            return i;
            //        }
            //    }
            //    return null;
            //}





            //dropdown Chức vụ
            function ListOfficePosition() {
                var data = {
                    url: "/Common/GetDataByGloballist?parentid=" + $scope.GlobalListWhereParentID.OfficePositionID
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getListOfficePosition = res.data.result;
                    if ($rootScope.RoleByStaffID == 10 && $rootScope.RoleID == 10 && $rootScope.PositionID != 254) {
                        delete $scope.getListOfficePosition[3];
                        delete $scope.getListOfficePosition[4];
                        delete $scope.getListOfficePosition[5];
                        delete $scope.getListOfficePosition[6];
                        delete $scope.getListOfficePosition[7];
                        delete $scope.getListOfficePosition[2];
                    }
                },
                    function (res) {
                        $scope.msg = "Error";
                    });
            }

            //dropdown Vị trí
            function ListOfficeRole() {
                var data = {
                    url: "/Common/GetDataByGloballist?parentid=" + $scope.GlobalListWhereParentID.OfficeRole
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getListOfficeRole = res.data.result;

                },
                    function (res) {
                        $scope.msg = "Error";
                    });
            }

            //dropdown Cấp bậc
            function ListStaffLevel() {
                var data = {
                    url: "/Common/GetDataByGloballist?parentid=" + $scope.GlobalListWhereParentID.StaffLevel
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getListStaffLevel = res.data.result;

                },
                    function (res) {
                        $scope.msg = "Error";
                    });
            }

            //dropdown Loại hợp đồng
            function ListContractType() {
                var data = {
                    url: "/Common/GetDataByGloballistnotTree?parentid=" + $scope.GlobalListWhereParentID.ContractType
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getListContractType = res.data.result;
                },
                    function (res) {
                        $scope.msg = "Error";
                    });
            }



            //dropdown Trạng thái duyệt 
            function ListStatus() {
                var data = {
                    url: "/Common/GetDataByGloballist?parentid=" + $scope.GlobalListWhereParentID.Status
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getListStatus = res.data.result;
                },
                    function (res) {
                        $scope.msg = "Error";
                    });
            }

            //dropdown Trạng thái làm việc  
            function ListWorkingStatus() {
                var data = {
                    url: "/Common/GetDataByGloballist?parentid=" + $scope.GlobalListWhereParentID.WorkingStatus
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getListWorkingStatus = res.data.result;

                },
                    function (res) {
                        $scope.msg = "Error";
                    });
            }

            //dropdown Trạng thái  
            function ListWPTypeID() {
                var data = {
                    url: "/Common/GetDataByGloballist?parentid=" + $scope.GlobalListWhereParentID.WPTypeID
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getListWPTypeID = res.data.result;

                },
                    function (res) {
                        $scope.msg = "Error";
                    });
            }

            //dropdown Công ty  
            function ListCompany() {
                var data = {
                    url: "/OrganizationUnit/GetCompany"
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getCompany = res.data.result;

                },
                    function (res) {
                        $scope.msg = "Error";
                    });
            }

            //dropdown Phòng ban  
            function ListOrganizationUnit(CompanyID) {
                var list = myService.ListOrganizationUnitWhereCompany(CompanyID);
                list.then(function (res) {
                    $scope.getListOrganizationUnit = res.data.result;

                },
                    function (res) {
                        $scope.msg = "Error";
                    });
            }
            //dropdown Văn phòng  
            function ListOffice() {
                var data = {
                    url: "/Common/GetDataByGloballist?parentid=" + $scope.GlobalListWhereParentID.Office
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getOffice = res.data.result;

                },
                    function (res) {
                        $scope.msg = "Error";
                    });
            }

            //dropdown Chính sách  
            function ListPolicy() {
                var data = {
                    url: "/WorkingProcess/GetPolicy"
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getPolicy = res.data.result;
                },
                    function (res) {
                        $scope.msg = "Error";
                    });
            }

            //-------------------------------------------- ConfigAllowance--------------------------

            //dropdown Loại phụ cấp
            function ListConfigAllowance(indexAmount, index) {

                var data = {
                    url: "/ConfigAllowance/GetConfigAllowance"
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getConfigAllowance = res.data.result;

                    //$rootScope.fromAmount = angular.copy($scope.getConfigAllowance[indexAmount].FromAmount);
                    //$rootScope.toAmount = angular.copy($scope.getConfigAllowance[indexAmount].ToAmount);
                    //$scope.min[index] = $rootScope.fromAmount;
                    //$scope.max[index] = $rootScope.toAmount;
                    //console.log($scope.max[index])

                },
                    function (res) {
                        $scope.msg = "Error";
                    });
            }



            //------------------------------------------- End ConfigAllowance ------------------------

        });

}
