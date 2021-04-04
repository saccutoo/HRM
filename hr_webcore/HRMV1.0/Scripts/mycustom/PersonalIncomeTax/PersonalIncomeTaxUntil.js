function BuildTable(appName, controllerName, tableUrl) {
    app.controller(controllerName,
        function ($scope, myService, $filter, $rootScope, $compile, $http) {
            $scope.maxSize = 5; // Limit number for pagination display number.
            $scope.totalCount = 0; // Total number of items in all pages. initialize as
            $scope.pageIndex = 1; // Current page number. First page is 1.-->
            $scope.pageSizeSelected = 5;
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

            //model đượct truyền ra từ directive build table
            $scope.employeeData = {};
            $scope.employee = [];
            $scope.RemoveListPolicyAllowance = [];


            //Global theo parentID
            $scope.GlobalListWhereParentID = {
                Status: 60,
                Product: 3320
            }

            $scope.Data = {};


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
                    if ($scope.tablePermission != null) {
                        if ($scope.tablePermission.isEdit == false) {
                            $scope.is_readonly = true;
                        }
                        else {
                            $scope.is_readonly = false;
                        }
                    }
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
                    $scope.CallData();
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
                if (type === 2) {
                    return formatNumber(value);
                }
                return value;
               
                if (type === 3 && value!=null) {

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






            // ----------------- Add chính sách-----------           
            $scope.addClick = function () {
                $scope.Data = {};
                $scope.ErrorFromAmount = "";
                $scope.ErrorStartDate = "";
                $scope.ErrorCurrencyID = "";
                $scope.ErrorStatus = "";
                $scope.ErrorCountryID = "";
                $scope.ErrorProgressiveTax = "";
                $scope.ErrorRateTax = "";
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
                $scope.Data.StartDate = dd + "/" + mm + "/" + yyyy;
                $(".StartDate").datepicker({
                    autoclose: true,
                    format: "dd/mm/yyyy",
                }).datepicker("setDate", $scope.Data.StartDate);
                $(".EndDate").datepicker({
                    autoclose: true,
                    format: "dd/mm/yyyy",
                }).datepicker("setDate", $scope.Data.EndDate);
                ShowPopup($,
                    "#SavePersonalIncomeTax",
                    $scope.tableInfo.PopupWidth,
                    $scope.tableInfo.PopupHeight);
            }

            $scope.CallData = function () {
                ListStatus();           //trạng thái
                ListCurrency();   //tiền tệ
                ListCountry(); // Quốc gia

            }
            //------------Dropdown trạng thái-------------
            function ListStatus() {
                var data = {
                    url: "/Common/GetDataByGloballistnotTree?parentid=" + 88
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getListStatus = res.data.result;
                },
                    function (res) {
                        $scope.msg = "Error";
                    });
            }
            //------------Dropdown tiền tệ-------------
            function ListCurrency() {
                var data = {
                    url: "/Common/GetDataByGloballistnotTree?parentid=" + 3
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getListCurrency = res.data.result;
                },
                    function (res) {
                        $scope.msg = "Error";
                    });
            }
            //------------Dropdown Quốc gia-------------
            function ListCountry() {
                var data = {
                    url: "/Common/GetDataByGloballistnotTree?parentid=" + 44
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getListCountry = res.data.result;

                },
                    function (res) {
                        $scope.msg = "Error";
                    });
            }


            //bắt lỗi khi nhập thu nhập từ
            $scope.ChangeFromAmount = function () {
            
                if ($scope.Data.FromAmount == "" || $scope.Data.FromAmount == null) {
                    $scope.ErrorFromAmount = errorIncome;
                    return
                }
                var position = $scope.Data.FromAmount.indexOf(',');
                if (position != -1) {
                    var x = $scope.Data.FromAmount.split(",");
                    var list = x.join("");

                }
                else if (isNaN($scope.Data.FromAmount)) {
                    $scope.ErrorFromAmount = errorMoneyIsNumber;
                    return
                }
                if (list != null && isNaN(list)) {
                    $scope.ErrorFromAmount = errorMoneyIsNumber;
                    return
                }
                else {
                    $scope.ErrorFromAmount = "";
                    $scope.ErrorProgressiveTax = "";
                    var x = $scope.Data.FromAmount;
                    var x1 = x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                    var x2 = x1.split(",");
                    var list = parseInt(x2.join(""));
                    $scope.Data.FromAmount = list.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                    // Tính thuế khi thu nhập nhỏ hơn 5 triệu
                    if (list ==0) {
                        $scope.Data.RateTax = 5;
                        $scope.Data.ProgressiveTax = 0;
                    }
                    else if (list >0 && list < 5000000) {
                        $scope.Data.RateTax = 5;
                        $scope.Data.ProgressiveTax = 0;
                    }
                    else if (list >= 5000000 && list < 10000000) {
                        $scope.Data.RateTax = 10
                        $scope.Data.ProgressiveTax = 250000;
                        $scope.Data.ProgressiveTax = $scope.Data.ProgressiveTax.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                    }
                   // Tính thuế khi thu nhập bằng 5 triệu
                    else if (list >= 10000000 && list < 18000000) {
                        $scope.Data.RateTax = 15;
                        $scope.Data.ProgressiveTax = 750000;
                        $scope.Data.ProgressiveTax = $scope.Data.ProgressiveTax.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                    }
                    else if (list >= 18000000 && list < 32000000) {
                        $scope.Data.RateTax = 20;
                        $scope.Data.ProgressiveTax = 1150000;
                        $scope.Data.ProgressiveTax = $scope.Data.ProgressiveTax.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");

                    }
                    else if (list >= 32000000 && list < 52000000) {
                        $scope.Data.RateTax = 25;
                        $scope.Data.ProgressiveTax = 4750000;
                        $scope.Data.ProgressiveTax = $scope.Data.ProgressiveTax.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");

                    }
                    else if (list >= 52000000 && list < 80000000) {
                        $scope.Data.RateTax = 30;
                        $scope.Data.ProgressiveTax = 9750000;
                        $scope.Data.ProgressiveTax = $scope.Data.ProgressiveTax.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");

                    }
                    else if (list >= 80000000 ) {
                        $scope.Data.RateTax = 35;
                        $scope.Data.ProgressiveTax = 18150000;
                        $scope.Data.ProgressiveTax = $scope.Data.ProgressiveTax.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");

                    }
                }
               
            }

            //bắt lỗi khi nhập thuế lũy tiến
            $scope.ChangeProgressiveTax = function () {
                if ($scope.Data.ProgressiveTax == "" || $scope.Data.ProgressiveTax == null) {
                    $scope.ErrorProgressiveTax = errorProgressiveTax;
                    return
                }
                var position = $scope.Data.ProgressiveTax.indexOf(',');
                if (position != -1) {
                    var x = $scope.Data.ProgressiveTax.split(",");
                    var list = x.join("");

                }
                else if (isNaN($scope.Data.ProgressiveTax)) {
                    $scope.ErrorProgressiveTax = errorProgressiveTaxIsNumber;
                    return
                }
                if (list != null && isNaN(list)) {
                    $scope.ErrorProgressiveTax = errorProgressiveTaxIsNumber;
                    return
                }
                else {
                    $scope.ErrorProgressiveTax = "";
                    var x = $scope.Data.ProgressiveTax;
                    var x1 = x.replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                    var x2 = x1.toString().split(",");
                    var list = parseInt(x2.join(""));
                    $scope.Data.ProgressiveTax = list.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                }
            }

            //bắt lỗi khi nhập vat
            $scope.ChangeRateTax=function(){
                if ($scope.Data.RateTax == "" || $scope.Data.RateTax == null) {
                    $scope.ErrorRateTax = errorVat;
                    return
                }
                else if (isNaN($scope.Data.RateTax)) {
                    $scope.ErrorRateTax = errorVatIsNumber;
                    return
                }
                else {
                    $scope.ErrorRateTax = "";
                }
            }

            //bắt lỗi khi nhập thu nhập đến
            $scope.ChangeToAmount = function () {
                if ($scope.Data.ToAmount != null && $scope.Data.ToAmount != "") {

                    var position = $scope.Data.ToAmount.indexOf(',');
                    if (position != -1) {
                        var x = $scope.Data.ToAmount.split(",");
                        var list = x.join("");

                    }
                    else if (isNaN($scope.Data.ToAmount)) {
                        $scope.ErrorToAmount = errorMoneyIsNumber;
                        return
                    }
                    if (list != null && isNaN(list)) {
                        $scope.ErrorToAmount = errorMoneyIsNumber;
                        return
                    }
                    else {
                        $scope.ErrorToAmount = "";
                        var x = $scope.Data.ToAmount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var x1 = x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        var x2 = x1.split(",");
                        var list = x2.join("");
                        $scope.Data.ToAmount = list.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                        if ($scope.Data.FromAmount != null && $scope.Data.FromAmount != "") {
                            var x = $scope.Data.FromAmount.split(",").join("");
                            var x1 = $scope.Data.ToAmount.split(",").join("");
                            if (parseInt(x) > parseInt(x1)) {
                                $scope.ErrorFromAmount = compareIncome;
                                return
                            }
                            else {
                                $scope.ErrorFromAmount = "";

                            }
                        }
                        else {
                            $scope.ErrorFromAmount = "";

                        }
                    }
                   
                }
                else {
                    $scope.ErrorFromAmount = "";
                    
                }
            }

            //bắt lỗi khi nhập ngày bắt đầu '
            $scope.ChangeStartDate = function () {
                if ($scope.Data.StartDate == null || $scope.Data.StartDate=="") {
                    $scope.ErrorStartDate = errorDateOfApplication;
                    return
                }
                else {
                    $scope.ErrorStartDate = "";
                }
            }

            //bắt lỗi khi nhập ngày kết thúc
            $scope.ChangeEndDate = function () {
                if ($scope.Data.EndDate != null) {
                    if ($scope.Data.StartDate != null) {
                        var date = $scope.Data.StartDate.split("/");
                        var date1 = date[2] + "/" + date[1] + "/" + date[0];
                        var date = $scope.Data.EndDate.split("/");
                        var date2 = date[2] + "/" + date[1] + "/" + date[0];
                        date1 = new Date(date1);
                        date2 = new Date(date2);
                        if (date1 > date2) {
                            $scope.ErrorStartDate = errorDate;
                            return;
                        }
                        else {
                            $scope.ErrorEndDate = "";
                            $scope.ErrorStartDate = "";

                        }
                    }

                }
            }

            //bắt lỗi khi chọn đơn vị tiền tệ
            $scope.ChangeCurrencyID = function () {
                if ($scope.Data.CurrencyID == null || $scope.Data.CurrencyID == "") {
                    $scope.ErrorCurrencyID = errorSelectCurrencyUnit;
                    return
                }
                else {
                    $scope.ErrorCurrencyID = "";
                }
            }

            //bắt lỗi khi chọn trạng thái
            $scope.ChangeStatus = function () {
                if ($scope.Data.Status == null || $scope.Data.Status == "") {
                    $scope.ErrorStatus = errorStatus;
                    return
                }
                else {
                    $scope.ErrorStatus = "";
                }
            }

            //bắt lỗi khi chọn quốc gia
            $scope.ChangeCountryID = function () {
                if ($scope.Data.CountryID == null || $scope.Data.CountryID == "") {
                    $scope.ErrorCountryID = errorSelectCountry;
                    return
                }
                else {
                    $scope.ErrorCountryID = "";
                }
            }

            // -----------------ADD hoặc edit chính sách--------------
            $scope.Save = function () {
                
                if ($scope.Data.FromAmount == "" || $scope.Data.FromAmount == null) {
                    $scope.ErrorFromAmount = errorIncome;
                    return
                }
                var position = $scope.Data.FromAmount.indexOf(',');
                if (position != -1) {
                    var x = $scope.Data.FromAmount.split(",");
                    var list = x.join("");

                }
                else if (isNaN($scope.Data.FromAmount)) {
                    $scope.ErrorFromAmount = errorIncomeFrom;
                    return
                }
                if (list != null && isNaN(list)) {
                    $scope.ErrorFromAmount = errorIncomeFrom;
                    return
                }
                
                if ($scope.Data.ToAmount != null && $scope.Data.ToAmount !="") {
                    var position = $scope.Data.ToAmount.indexOf(',');
                    if (position != -1) {
                        var x = $scope.Data.ToAmount.split(",");
                        var list = x.join("");
                    }
                    
                }
                if (list != null && isNaN(list)) {
                    $scope.ErrorToAmount = errorIncomeToIsNumber;
                    return
                }
                if ($scope.Data.ToAmount != null && $scope.Data.ToAmount != "" && $scope.Data.FromAmount != "" && $scope.Data.FromAmount != null) {
                    var position = $scope.Data.FromAmount.indexOf(',');
                    var position1 = $scope.Data.ToAmount.indexOf(',');
                    if (position!=-1) {
                        var x = $scope.Data.FromAmount.split(",");
                        var list = x.join("");
                    }
                    else {
                        var list = $scope.Data.FromAmount;
                    }
                    if (position1!=-1) {
                        var x = $scope.Data.ToAmount.split(",");
                        var list1 = x.join("");
                    }
                    else {
                        var list1 = $scope.Data.ToAmount;
                    }
                    if (parseInt(list1) < parseInt(list)) {
                        $scope.ErrorFromAmount = compareIncome;
                        return
                    }
                }
                else if ($scope.Data.StartDate == null || $scope.Data.StartDate == "") {
                    $scope.ErrorStartDate = errorDateStart;
                    return;
                }
                if ($scope.Data.EndDate != null && $scope.Data.EndDate != "") {
                    var date = $scope.Data.StartDate.split("/");
                    var date1 = date[2] + "/" + date[1] + "/" + date[0];
                    var date = $scope.Data.EndDate.split("/");
                    var date2 = date[2] + "/" + date[1] + "/" + date[0];
                    date1 = new Date(date1);
                    date2 = new Date(date2);

                }
                
                if (date1 != null && date2 != null && date1 > date2) {
                    $scope.ErrorStartDate = errorDate;
                    return;
                }
                if ($scope.Data.ProgressiveTax == null || $scope.Data.ProgressiveTax.toString() == "") {
                    $scope.ErrorProgressiveTax = errorProgressiveTax;
                    return
                }
                var position = $scope.Data.ProgressiveTax.toString().indexOf(',');
                if (position != -1) {
                    var x = $scope.Data.ProgressiveTax.split(",");
                    var list = x.join("");

                }
                else if (isNaN($scope.Data.ProgressiveTax)) {
                    $scope.ErrorProgressiveTax = errorProgressiveTaxIsNumber;
                    return
                }
                if (list != null && isNaN(list)) {
                    $scope.ErrorProgressiveTax = errorProgressiveTaxIsNumber;
                    return
                }
                if ($scope.Data.RateTax == "" || $scope.Data.RateTax == null) {
                    $scope.ErrorRateTax = errorVat;
                    return
                }
                else if (isNaN($scope.Data.RateTax)) {
                    $scope.ErrorRateTax = errorVatIsNumber;
                    return
                }
                else if ($scope.Data.CurrencyID == null || $scope.Data.CurrencyID == "") {
                    $scope.ErrorCurrencyID = errorSelectCurrencyUnit;
                    return;
                }
                else if ($scope.Data.Status == null || $scope.Data.Status == "") {
                    $scope.ErrorStatus = errorStatus;
                    return;
                }
                else if ($scope.Data.CountryID == null || $scope.Data.CountryID == "") {
                    $scope.ErrorCountryID = errorSelectCountry;
                    return;
                }
                else {
                    $scope.ErrorFromAmount = "";
                    $scope.ErrorStartDate = "";
                    $scope.ErrorCurrencyID = "";
                    $scope.ErrorStatus = "";
                    $scope.ErrorCountryID = "";
                    $scope.ErrorProgressiveTax = "";
                    $scope.ErrorRateTax = "";
                    var position = $scope.Data.FromAmount.indexOf(',');
                    if (position != -1) {
                        $scope.Data.FromAmount = $scope.Data.FromAmount.split(",").join("");
                    }
                    var position = $scope.Data.ProgressiveTax.toString().indexOf(',');
                    if (position != -1) {
                        $scope.Data.ProgressiveTax = $scope.Data.ProgressiveTax.split(",").join("");
                    }
                    
                    if ($scope.Data.ToAmount != null) {
                        var positionToAmount = $scope.Data.ToAmount.indexOf(',');
                        
                    }
                    if (positionToAmount != null && positionToAmount!=-1) {
                        $scope.Data.ToAmount = $scope.Data.ToAmount.split(",").join("");
                    }


                    var date = $scope.Data.StartDate.split("/");
                    $scope.Data.StartDate = date[2] + "/" + date[1] + "/" + date[0];
                    if ($scope.Data.EndDate != null && $scope.Data.EndDate != "") {
                        var date = $scope.Data.EndDate.split("/");
                        $scope.Data.EndDate = date[2] + "/" + date[1] + "/" + date[0];

                    }

                    var SaveAction = myService.UpdateData("/PersonalIncomeTax/PersonalIncomeTax_Save", $scope.Data);
                    SaveAction.then(function (res) {
                        if (res.data.result.IsSuccess == true) {
                            if ($scope.Data.AutoID != 0 && $scope.Data.AutoID != null) {
                                AppendToToastr(true, notification, successfulUpdate, 500, 5000);
                            }
                            else {
                                AppendToToastr(true, notification, successfulAdd, 500, 5000);

                            }
                            $scope.PersonalIncomeTaxData.reload();
                            $scope.Data = {};
                            $scope.CloseForm();
                        }
                        else {
                            AppendToToastr(false, notification, updateFailed, 500, 5000);
                            $scope.PersonalIncomeTaxData.reload();
                            $scope.CloseForm();

                        }
                    }, function (res) {
                        AppendToToastr(false, notification, errorNotiFalse);
                    });
                }

            }

            //-----------------Edit Chính sách--------------
            $scope.editClick = function (contentItem) {
                $scope.Data = {};
                $scope.ErrorFromAmount = "";
                $scope.ErrorStartDate = "";
                $scope.ErrorCurrencyID = "";
                $scope.ErrorStatus = "";
                $scope.ErrorCountryID = "";
                $scope.ErrorProgressiveTax = "";
                $scope.ErrorRateTax = "";
                $scope.Data.AutoID = contentItem.AutoID;
                $scope.Data.FromAmount = contentItem.FromAmount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                if (contentItem.ToAmount!=null) {
                    $scope.Data.ToAmount = contentItem.ToAmount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                }
                $scope.Data.ProgressiveTax = contentItem.ProgressiveTax.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");;
                $scope.Data.RateTax = contentItem.RateTax;
                $scope.Data.StartDate = FormatDate(contentItem.StartDate);
                if (contentItem.EndDate!=null) {
                    $scope.Data.EndDate = FormatDate(contentItem.EndDate);
                }
                $scope.Data.CurrencyID = contentItem.CurrencyID
                $scope.Data.Status = contentItem.Status.toString();
                $scope.Data.CountryID = contentItem.CountryID.toString();
                $scope.Data.Note = contentItem.Note;
                $(".StartDate").datepicker({
                    autoclose: true,
                    format: "dd/mm/yyyy",
                }).datepicker("setDate", $scope.Data.StartDate);
                $(".EndDate").datepicker({
                    autoclose: true,
                    format: "dd/mm/yyyy",
                }).datepicker("setDate", $scope.Data.EndDate);
                ShowPopup($,
                   "#SavePersonalIncomeTax",
                   $scope.tableInfo.PopupWidth,
                   $scope.tableInfo.PopupHeight);
            }


            //load lại trang khi click bỏ qua
            $scope.CloseForm = function () {
                $scope.ErrorFromAmount = "";
                $scope.ErrorStartDate = "";
                $scope.ErrorCurrencyID = "";
                $scope.ErrorStatus = "";
                $scope.ErrorCountryID = "";
                $scope.ErrorProgressiveTax = "";
                $scope.ErrorRateTax = "";
                $scope.Data = {};
                $.colorbox.close();
            }


            // -----------------End Edit Add Click--------------
            //------------------Add-Save---------

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
                        $scope.PersonalIncomeTaxData.reload();
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







            //Dropdown phòng ban
            function ListOrganizationUnit() {
                var data = {
                    url: "/OrganizationUnit/GetOrganizationUnit?chon=1"
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.getListOrganizationUnit = res.data.result;

                }, function (res) {
                    $scope.msg = "Error";
                })
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
