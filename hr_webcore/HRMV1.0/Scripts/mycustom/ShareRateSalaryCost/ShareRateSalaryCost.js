function BuildTable(appName, controllerName, tableUrl, viewType) {
    app.controller(controllerName,
        function ($scope, myService, $filter, $rootScope, $window, $http) {
            // $scope.test = $filter('mm/dd/yyyy')("2018/01/01", 'yyyy/MM/dd');
            $scope.maxSize = 5; // Limit number for pagination display number
            $scope.totalCount = 0; // Total number of items in all pages. initial
            $scope.pageIndex = 1; // Current page number. First page is 1
            $scope.pageSizeSelected = 10;
            $rootScope.viewType = viewType; //viewType báo cáo theo từng loại
            $scope.isShowFilter = false; $scope.typeEnds = [{ name: "Và", nameEN: "And", value: " and " }, { name: "Hoặc", nameEN: "Or", value: " or " }];
            $scope.typeFilterA = [{ name: ">", nameEN: ">", value: " > '#' " }, { name: "<", nameEN: "<", value: " < '#' " }, { name: "=", nameEN: "=", value: " = '#' " }, { name: "Khác", nameEN: "Not equal", value: " != '#' " }];
            $scope.typeFilterB = [{ name: "Có chứa", nameEN: "Contains", value: " like '%#%' " }, { name: "Bằng", nameEN: "Is", value: " = '#' " }, { name: "Không chứa", nameEN: "Do not contains", value: " != '#' " }];
            $scope.filterColumnsChoosed = [];
            //set tu ngay-den ngay mac dinh cho build table
            var date = new Date(), y = date.getFullYear(), m = date.getMonth();
            $scope.FromDate = $filter("date")(new Date(y, m, 1));
            $scope.ToDate = $filter("date")(Date.now());
            //end - set tu ngay-den ngay mac dinh cho build table
            //$rootScope.GetColumnWhereCondition = 1; //cot duoc hien thi theo quy/thang PerformanceReport
            $scope.FromDateToDate = true; //cho phep build table hien thi tu ngay nao den ngay nao
            //$scope.StaffStatus = 1; //show StaffStatus mac dinh la hoat dong
            //$scope.quickFilter = { $$hashKey: 'object:31', CreatedBy: 0, CreatedBy1: null, CreatedDate: '0001-01-01T00:00:00', Descriptions: null, DisplayName: null, DisplayValue: null, GlobalListID: 0, HasChild: false, IsActive: false, IsActive1: null, ListChildID: null, ModifiedBy: null, ModifiedBy1: null, ModifiedDate: null, Name: 'Hoạt động', NameEN: null, OrderNo: null, ParentDetailID: 0, ParentID: 0, TreeLevel: 0, Value: '879', ValueEN: null, idOld: null }; // quick filter data
            //$rootScope.DoNotLoad = 1; //cho build table khong load data lan dau
            setTimeout(function () { $('.col-md-3.selectdiv').hide(); }, 2000);
            $rootScope.data = {
                filter5: '',
                filter1: '',
                filter2: '',
                filter3: '',
                filter4: '',
                filter6: '',
                filter7: '',
                filter8: '',
            }; //dự liệu truyền vào
            if (window.location.pathname.toLowerCase() === "/ShareRateSalaryCost/Dept".toLowerCase()) {
                $rootScope.data.filter7 = "2"
            }
            else {
                $rootScope.data.filter7 = "1"
            }
            $scope.IsStaff = window.location.pathname.toLowerCase() == "/ShareRateSalaryCost/Staff".toLowerCase();
            var res = $rootScope.data;
            


            //Radio phòng ban, nhân viên
            //$rootScope.ShowAllOrDetail = function () {
            //    if ($rootScope.data.filter7 == 1) {
            //        $('.Staff').addClass("hide-element");
            //        $('.Department').removeClass("hide-element");
            //        $('.col-md-3.selectdiv.ng-scope').hide();

            //    }
            //    else {
            //        $('.Department').addClass("hide-element");
            //        $('.Staff').removeClass("hide-element");
            //        $('.col-md-3.selectdiv.ng-scope').show();
            //    }
            //}

            //-----------------List-----------     
            //Call và truy?n d? li?u sang builtable g?i l?i d? li?u
            $rootScope.childmethod = function () {
                var filter = $scope.getFilterValue();
                $rootScope.$broadcast("CallParentMethodWithFilter", { res, filter });
            }

            //-----------------List-End---------

            // -----------------fllter------------
            $scope.getFilterValue = function () {
                var lstObj = $scope.filterColumnsChoosed ;
                var stringFilter = "";
                var len = lstObj.length - 1;
                for (var key in lstObj) {
                    var obj = lstObj[key];
                    var tmpFilter = obj.filterSelected.ColumnName + obj.typeFilterSelected.Value.replace("#", obj.filterSelected.Type === 3 ? $filter('date')(new Date(obj.textValue), 'yyyy/MM/dd') : obj.textValue) + (parseInt(String(key)) === len ? "" : obj.typeEndsSeleted.value);
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
            // -----------------filter--End----------

            //import excel

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
                    + "&filter=" + filter
                ;

            }

            //---------------------------------------------------------------------------- List-End ------------------------------------------------------------------------------------



            $scope.getTableInfo = function () {
                var getData = myService.getTableInformation(tableUrl);
                getData.then(function (emp) {
                    $scope.tableInfo = emp.data.result;
                    $scope.lstPageSize = $scope.tableInfo.PageSizeList.split(',');
                    $scope.pageSizeSelected = $scope.tableInfo.PageSize;
                },
                    function (emp) {
                        if (currentLanguage == 5) {
                            AppendToToastr(false, notification, errorNotiFalse);
                        }
                        else {
                            AppendToToastr(false, "Notification", "... Error !");
                        }
                    });
            }


            $scope.CallData = function () {
                ListOrganizationUnit(); //phòng ban
                //ListStatus();           //trạng thái
                //ListProduct();   //sản phẩm
                //ListStaffWhereRoleBD(); //BD
                ListCompany();
                ListEmployeeWhereOrganizationUnit(0);
            }





            // -----------------Edit Add Click Employee------------

            $scope.editClick = function (contentItem) {
                var type = 1;
                if (window.location.pathname.toLowerCase() === "/ShareRateSalaryCost/Dept".toLowerCase()) {
                    var type = 2;
                }
                else {
                    var type = 1;
                }
                var edit = myService.getDataByIdAndCustomer(contentItem.Id, type, $scope.tableInfo.id, $scope.tableInfo.DataEditUrl);
                edit.then(function (emp) {
                    $scope.CallData();
                    $scope.editData = emp.data.result;

                    $scope.editData.Status = (emp.data.result.Status).toString();
                    ShowPopup($,
                        "#PaymentProduct",
                        $scope.tableInfo.PopupWidth,
                        $scope.tableInfo.PopupHeight);
                },
                    function (emp) {
                        if (currentLanguage == 5) {
                            AppendToToastr(false, notification, errorNotiFalse);
                        }
                        else {
                            AppendToToastr(false, "Notification", "... Error !");
                        }

                    });
            }


            $scope.addClick = function (tblInfo) {
                $scope.tableInfo = tblInfo;
                $scope.CallData();
                $scope.editData = {
                    CustomerID: "",
                    Status: "1",
                    ProductID: 3353,
                    CreatedBy: 1,
                    CreatedDate: Date.now()
                };
                ShowPopup($,
                    "#PaymentProduct",
                    $scope.tableInfo.PopupWidth,
                    $scope.tableInfo.PopupHeight);

            }

            $scope.importExcelClick = function (tblInfo) {
                $scope.tableInfo = tblInfo;
                $scope.CallData();
                //$scope.editData = {
                //    CustomerID: "",
                //    Status: "1",
                //    ProductID: 3353,
                //    CreatedBy: 1,
                //    CreatedDate: Date.now()
                //};
                ShowPopup($,
                    "#FileImport",
                    670,
                    450);

            }

            $scope.GetListData = function () {
                ShowLoader();
                var data = {
                    pageIndex: $scope.pageIndex,
                    pageSize: $scope.pageSizeSelected,
                }
                var getDataTbl = myService.GetTableData(data, tableUrl);
                getDataTbl.then(function (emp) {
                    $scope.employees = emp.data.employees;
                    $scope.totalCount = emp.data.totalCount;
                    $scope.lstTotal = emp.data.lstTotal;
                    $rootScope.RoleID = emp.data.roleid;

                    $scope.SetTotalByColumns = function (totalName) {
                        if (!angular.isUndefined(totalName) && totalName !== null) {
                            return $scope.lstTotal[totalName];
                        }
                        return "-";
                    };
                    HiddenLoader();
                },
                    function (emp) {
                        if (currentLanguage == 5) {
                            AppendToToastr(false, notification, errorNotiFalse);
                        }
                        else {
                            AppendToToastr(false, "Notification", "... Error !");
                        }
                    });

            }
            // -----------------End Edit Add Click--------------

            //--------------Save Employee----------------------
            $scope.SaveAction = function (url, form) {
                if (form.$valid) {
                    if (window.location.pathname.toLowerCase() === "/ShareRateSalaryCost/Dept".toLowerCase()) {
                        $scope.editData.Type = 2;
                    } else {
                        $scope.editData.Type = 1;
                    }
                    var updateAction = myService.UpdateData(url, $scope.editData);
                    updateAction.then(function (res) {
                        if (res.data.result.IsSuccess) {
                            $scope.GetListData();
                            $.colorbox.close();
                            $scope.ShareRateSalaryCostData.reload();
                        }
                        AppendToToastr(res.data.result.IsSuccess,currentLanguage==5?"Thông báo":"Notification", res.data.result.Message, 500, 5000);
                    },
                        function (res) {
                            if (currentLanguage == 5) {
                                AppendToToastr(false, notification, errorNotiFalse);
                            }
                            else {
                                AppendToToastr(false, "Notification", "... Error !");
                            }
                        });
                }


            }

            //--------------import Employee----------------------
            $scope.ImportAction = function (url, form) {
         
                    if (window.location.pathname.toLowerCase() === "/ShareRateSalaryCost/Dept".toLowerCase()) {
                        $scope.TypeImport = 2;
                    } else {
                        $scope.TypeImport = 1;
                    }

                    var $file = document.getElementById('FileUploadInput'),
                    $formData = new FormData();

                    if ($file.files.length > 0) {
                        for (var i = 0; i < $file.files.length; i++) {
                            $formData.append('file-' + i, $file.files[i]);
                        }
                    }
                    $formData.append("Type", $scope.TypeImport);
                    var dt = Loading();
                    $.ajax({
                        type: 'POST',
                        url: url,
                        data: $formData,
                        processData: false,
                        contentType: false,
                        beforeSend: function () {
                            //ShowLoadingPage();
                        },
                        success: function (data) {
                            $scope.GetListData();
                            $.colorbox.close();
                            $scope.ShareRateSalaryCostData.reload();
                            dt.finish();
                            var response = data;
                            //CloseForm();
                            if (data.data) alert("Success");
                            window.location = '/ShareRateSalaryCost/Download?fileGuid=' +response.FileGuid
                                + '&filename=' + response.FileName;
                        },
                        error: function (err) {
                            dt.finish();
                        }
                    });
                    //var importAction = myService.ImportData(url, $formData);
                    //importAction.then(function (res) {
                    //    if (res.data) {
                    //        $scope.GetListData();
                    //        $.colorbox.close();
                    //        $scope.ShareRateSalaryCostData.reload();
                    //        var response = res.data;
                    //        if (response) alert("Success");
                    //        window.location = '/ShareRateSalaryCost/Download?fileGuid=' +response.FileGuid
                    //            + '&filename=' +response.FileName;
                    //    }
                    //    AppendToToastr(res.data, currentLanguage == 5 ? "Thông báo" : "Notification", res.data.result.Message, 500, 5000);
                    //},
                    //    function (res) {
                    //        if (currentLanguage == 5) {
                    //            AppendToToastr(false, notification, errorNotiFalse);
                    //        }
                    //        else {
                    //            AppendToToastr(false, "Notification", "... Error !");
                    //        }
                    //    });
                


            }
            //------------------Add-Save---------

            // -----------------Xóa------------

            $scope.deleteClick = function (obj) {
                if (currentLanguage == 4) {
                    BoostrapDialogConfirm("Notification",
                    "Do you want delete this record?",
                    BootstrapDialog.TYPE_WARNING,
                    $scope.deleteActionClick,
                    obj);
                }
                else {
                    BoostrapDialogConfirm(notification,
                    notificationDelete,
                    BootstrapDialog.TYPE_WARNING,
                    $scope.deleteActionClick,
                    obj);
                }
                
            }
            $scope.deleteActionClick = function (obj) {
                var type = 1;
                if (window.location.pathname.toLowerCase() === "/ShareRateSalaryCost/Dept".toLowerCase()) {
                    type = 2;
                    //IsStaff = false;
                }
                var action = myService.deleteAction(obj.Id, type, $scope.tableInfo.DeleteUrl);
                action.then(function (res) {
                    if (res.data.result.IsSuccess) {
                        $scope.getTableInfo();
                        $scope.ShareRateSalaryCostData.reload();
                    }
                    AppendToToastr(res.data.result.IsSuccess,currentLanguage==5?"Thông báo":"Notification", res.data.result.Message, 500, 5000);
                },
                    function (res) {
                        if (currentLanguage == 5) {
                            AppendToToastr(false, notification, errorNotiFalse);
                        }
                        else {
                            AppendToToastr(false, "Notification", "... Error !");
                        }
                    });

            }

            // -----------------Xóa--End------------


            //-------------------Excel--------------
            $scope.ExcelClick = function () {
                if (window.location.pathname.toLowerCase() === "/ShareRateSalaryCost/Dept".toLowerCase()) {
                    $rootScope.data.filter7 = "2"
                }
                else {
                    $rootScope.data.filter7 = "1"
                }
                
                var filterString = $scope.getFilterValue();
                window.location = $scope.tableInfo.ExcelUrl + "?pageIndex=" + $scope.pageIndex + "&pageSize=" + $scope.pageSizeSelected + "&filter=" + filterString;
            }
            //-------------------Excel-End----------


            function ListStaff() {
                var data = {
                    url: "/Employee/GetStaff"
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getListStaff = res.data.result;
                },
                    function (res) {
                        $scope.msg = "Error";
                    });
            }
            //------------Dropdown trạng thái-------------
            function ListStatus() {
                var data = {
                    url: "/Common/GetDataByGloballistnotTree?parentid=" + $scope.GlobalListWhereParentID.Status
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getListStatus = res.data.result;

                },
                    function (res) {
                        $scope.msg = "Error";
                    });
            }


            //Dropdown phòng ban
            function ListOrganizationUnit() {
                var data = {
                    url: "/OrganizationUnit/GetOrganizationUnit?chon=1"
                }
                var list = myService.getDropdown(data);
                list.then(function(res) {
                    $scope.getListOrganizationUnit = res.data.result;

                }, function(res) {
                    $scope.msg = "Error";
                });
            }

            //dropdown nhân viên theo phòng ban
            function ListEmployeeWhereOrganizationUnit(id) {
                var data = {
                    url: "/OrganizationUnit/EmployeeByOrganizationUnitID?id=" + id
                }
                var list = myService.getData(data);
                list.then(function(res) {
                    $scope.ListEmployeeWhereOrganizationUnit = res.data.result;

                }, function(res) {
                    $scope.msg = "Error";
                });
            }

            //dropdown danh sách báo cáo
            function ListCompany(id) {
                var data = {
                    url: "/ShareRateSalaryCost/GetListCompany"
                }
                var list = myService.getData(data);
                list.then(function (res) {
                    $scope.ListCompany = res.data.result;

                }, function (res) {
                    $scope.msg = "Error";
                });
            }
            //đóng popup modal
            $scope.CloseForm = function () {
                $scope.Data = {};
                $scope.ErrorName = "";
                $scope.ErrorFromAmount = "";
                $scope.ToAmount = "";
                $scope.ErrorsFormular = "";
                $.colorbox.close();
            }
            //----------------------------------------------------------------------------------------------------------------------------------------------------------------------
            $scope.getTableInfo();


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
   
}
