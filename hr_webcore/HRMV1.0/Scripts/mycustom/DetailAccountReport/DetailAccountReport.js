function BuildTable(appName, controllerName, tableUrl,tableUrl1,tableUrl2) {
    app.controller(controllerName,
        function ($scope, myService, $filter, $rootScope, $window, $http) {
            $scope.isShowFilter = false; 
            $rootScope.GetColumnWhereCondition = 0; //cot duoc hien thi theo quy/thang PerformanceReport
            $scope.FromDateToDate = true;
            $scope.FormatColumn = false;
            $rootScope.DoNotLoad = 1;
            var date = new Date(), y = date.getFullYear(), m = date.getMonth();
            $scope.FromDate = $filter("date")(new Date(y, m, 1));
            $scope.ToDate = $filter("date")(Date.now());
            //Dữ liệu filter param truyền vào
            $rootScope.data = {
                filter5: '1',
                filter1: '1',
                filter2: '1',
                filter3: '1',
                filter4: '1',
                filter6: '1',
                filter7: '1',
                filter8: '1'
            }; 
            //Loại báo cáo chi tiết : 1-Tài khoản, 2-Phí thu ,3 Thuê TK/refer
            $scope.type = "1";
            //Model đượct truyền ra từ directive build table
            $scope.DetailAccountReportData = {};

            //Close Form
            $scope.CloseForm = function () {
                $.colorbox.close();
            }     
            //Save and edit
            $scope.SaveAction = function (url, form) {
                if (form.$valid) {
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
}
