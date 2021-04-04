
function BuildTable2(appName, controllerName, tableUrl, Notification, Error) {
    app.controller(controllerName,
        function ($scope, myService, $filter, $rootScope, $compile) {
            // $scope.test = $filter('mm/dd/yyyy')("2018/01/01", 'yyyy/MM/dd');
            $scope.maxSize = 5; // Limit number for pagination display number.
            $scope.totalCount = 0; // Total number of items in all pages. initialize as a ze
            $scope.pageIndex = 1; // Current page number. First page is 1.-->
            $scope.pageSizeSelected = 5;
            $scope.isShowFilter = false; $scope.typeEnds = [{ name: "Và", nameEN: "And", value: " and " }, { name: "Hoặc", nameEN: "Or", value: " or " }];
            $scope.typeFilterA = [{ name: ">", nameEN: ">", value: " > '#' " }, { name: "<", nameEN: "<", value: " < '#' " }, { name: "=", nameEN: "=", value: " = '#' " }, { name: "Khác", nameEN: "Not equal", value: " != '#' " }];
            $scope.typeFilterB = [{ name: "Có chứa", nameEN: "Contains", value: " like '%#%' " }, { name: "Bằng", nameEN: "Is", value: " = '#' " }, { name: "Không chứa", nameEN: "Do not contains", value: " != '#' " }];
            $scope.filterColumnsChoosed = [];

            $scope.emailValid = /^[a-z]+[a-z0-9._]+@[a-z]+\.[a-z.]{2,5}$/;
            $scope.urlSocialInsuranceDetail = "/SocialInsuranceDetail/TableServerSideGetData";


            $scope.initSID = function () {
                $scope.CallData();
       
                $scope.editData = {
                    CreatedDate: new Date(),
                    RateCompany: $rootScope.RateCompany,
                    RatePerson: $rootScope.RatePerson
                };
            }
            //model được truyền ra từ buildtable
            $scope.tableSocialModel = {}
    
         
            $scope.CallData = function () {
                ListInsuranceStatus();
                GetSumRate();
                ListRegime();
                ListStatus();
            }

            //Global theo parentID
            $scope.GlobalListWhereParentID = {
                Status: 2151,
                Regime: 2156,
                ApproveStatus: 2129
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
                    }
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

            //-----------------List-End---------
          

            // -----------------Edit-----------
            function formatDate(timestamp) {
                if (timestamp != null) {
                    var x = new Date(timestamp);
                    var mm = x.getMonth() + 1;
                    var yy = x.getFullYear();
                    return mm + "/" + yy;
                }
            }
         
            $scope.editClick = function (contentItem) {
                $scope.CallData();
                var edit = myService.getDataById(contentItem.AutoID, $scope.tableInfo.id, $scope.tableInfo.DataEditUrl);
                edit.then(function (emp) {
                        $scope.editData = emp.data.result;
                        if ($scope.editData.Regime != null) {
                            $scope.editData.Regime = emp.data.result.Regime.split(',');
                        }
                        $scope.editData.MonthStart = formatDate($scope.editData.MonthStart);
                        $scope.editData.FromMonth = formatDate($scope.editData.FromMonth);
                        $scope.editData.ToMonth = formatDate($scope.editData.ToMonth);
                            ShowPopup($,
                                ".SaveSocialInsuranceDetail",
                                $scope.tableInfo.PopupWidth,
                                $scope.tableInfo.PopupHeight);
                        },
                        function (emp) {
                            AppendToToastr(false, Notification, Error);
                        });
            }


            // -----------------Edit--End----------
         
            //------------------Add---------------
          
         
          
            $scope.SaveAction = function (url,form) {
                if (form.$valid) {
                    if ($scope.editData.Regime != null) {
                        $scope.editData.Regime=$scope.editData.Regime.join();
                    }
                    $scope.editData.StaffID = $rootScope.StaffID;
                    var updateAction = myService.UpdateData(url, $scope.editData);
                    updateAction.then(function (res) {
                        if (res.data.result.IsSuccess) {
                            $scope.GetListData();
                            DataLastID();
                            ShowPopup($,
                            "#SaveContent",
                            $scope.tableInfo.PopupWidth,
                            $scope.tableInfo.PopupHeight);
                        }
                        $scope.tableSocialModel.reload();
                        AppendToToastr(res.data.result.IsSuccess, Notification, res.data.result.Message, 500, 5000);
                    },
                        function (res) {
                        
                            AppendToToastr(false, Notification, Error);
                        });
                }
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
                console.log(obj);
                var action = myService.deleteAction(obj.AutoID, $scope.tableInfo.id, $scope.tableInfo.DeleteUrl);
                action.then(function (res) {
                    if (res.data.result.IsSuccess) {
                        $scope.GetListData();
                        DataLastID();
                        $scope.tableSocialModel.reload();
                    }
                    AppendToToastr(res.data.result.IsSuccess, Notification, res.data.result.Message, 500, 5000);
                },
                    function (res) {
                        AppendToToastr(false, Notification, Error);
                    });

            }

            $scope.CloseForm = function () {
                ShowPopup($,
                   "#SaveContent",
                   $scope.tableInfo.PopupWidth,
                   $scope.tableInfo.PopupHeight);
            }

            //-------------------Excel--------------
            $scope.ExcelClick = function () {
                var filterString = $scope.getFilterValue();
                window.location = $scope.tableInfo.ExcelUrl + "?filterString=" + filterString + "&pageIndex=" +
                    $scope.pageIndex +
                    "&pageSize=" +
                    $scope.pageSizeSelected + "&SessionStaffID=" + $rootScope.StaffID;

            }
            //-------------------Excel-End----------

            //-----
          
            //bản ghi mới nhất
            function DataLastID() {
                var data = {
                    url: "/SocialInsuranceDetail/GetSocialInsuranceLastID?SessionStaffID=" + $rootScope.StaffID
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getDataLastID = res.data.result;
                }, function (res) {
                    $scope.msg = "Error";
                })
            }
            DataLastID();

            //
            function ListInsuranceStatus() {
                var data = {
                    url: "/Common/GetDataByGloballist?parentid=" + $scope.GlobalListWhereParentID.Status
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getInsuranceStatus = res.data.result;
                }, function (res) {
                    $scope.msg = "Error";
                })
            }
            //SocialDetail Tỷ lệ đóng %
            function GetSumRate() {
                var data = {
                    url: "/Insurance/GetSumRate"
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getSumRate = res.data.result;
                    $rootScope.RateCompany = res.data.result.RateCompany;
                    $rootScope.RatePerson = res.data.result.RatePerson;
                }, function (res) {
                    $scope.msg = "Error";
                })
            }
            //SocialDetail chế độ hưởng
            function ListRegime() {
                var data = {
                    url: "/Common/GetDataByGloballistnotTree?parentid=" + $scope.GlobalListWhereParentID.Regime
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $rootScope.getRegimes = res.data.result;
                }, function (res) {
                    $scope.msg = "Error";
                })
            }
            //dropdown Trạng thái duyệt 
            function ListStatus() {
                var data = {
                    url: "/Common/GetDataByGloballist?parentid=" + $scope.GlobalListWhereParentID.ApproveStatus
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getListStatus = res.data.result;

                }, function (res) {
                    $scope.msg = "Error";
                })
            }

            $scope.addClick = function () {
                $scope.CallData();
                $scope.editData = {
                    CreatedDate: new Date(),
                    RateCompany: $rootScope.RateCompany,
                    RatePerson: $rootScope.RatePerson,
                    ApproveStatus: 2133
                };
                ShowPopup($,
                    ".SaveSocialInsuranceDetail",
                    $scope.tableInfo.PopupWidth,
                    $scope.tableInfo.PopupHeight);
            }
           
        });

}
