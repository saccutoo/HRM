function BuildTable(appName, controllerName, tableUrl, tableUrl1, tableUrl2, tableUrl3, tableUrl4, Notification, Error) {
    app.controller(controllerName,
        function ($scope, myService, $filter, $rootScope, $window, $http) {
            $scope.isShowFilter = false;
            $scope.pageIndex = 1; 
            $scope.pageSizeSelected = 5;
            $scope.filterColumnsChoosed = [];
            //Loại báo cáo chi tiết : 1-Tài khoản, 2-Phí thu ,3 Thuê TK/refer
            $scope.type = "0";
            //Model đượct truyền ra từ directive build table
            $scope.DetailAccountReportData = {};

            
            $scope.CallClick = function (value) {
                var url = "";
                $rootScope.data = {url}
                if (value === 0 || value === 1) {
                    $rootScope.Type = value;
                }
                switch (value) {
                case 0:
                        url = tableUrl;
                        $rootScope.data.url = '/ImportExcelTable/Upload?type=' + 0;
                        
                    break;
                case 1:
                        url = tableUrl1;
                        $rootScope.data.url = '/ImportExcelTable/Upload?type=' + 1;
                        break;
                case 2:
                        url = tableUrl2;
                        $rootScope.data.url = '/ImportExcelTable/UploadRelationships';
                        break;
                case 3:
                    url = tableUrl3;
                        //$rootScope.data.url = '/ImportExcelTable/UploadRelationships';
                    break;
                case 4:
                    url = tableUrl4;
                    //$rootScope.data.url = '/ImportExcelTable/UploadRelationships';
                    break;
                }
                $rootScope.getTableInfo = function () {
                    var getData = myService.getTableInformation(url);
                    getData.then(function (emp) {
                            $rootScope.tableInfo = emp.data.result;
                            $rootScope.lstPageSize = $scope.tableInfo.PageSizeList.split(',');
                            $rootScope.pageSizeSelected = $scope.tableInfo.PageSize;
                            //$scope.GetAddPermission(emp.data.result.id);
                    },
                        function (emp) {
                            AppendToToastr(false, Notification, Error);
                        });
                }
                $scope.getTableInfo();
                 
            }
            $scope.CallClick(0);
         
           
            //-------------------Excel--------------
            
            //-------------------Excel-End----------

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
    app.directive("myFiles", function ($parse) {
        return function linkFn(scope, elem, attrs) {
            elem.on("change",
                function (e) {
                    scope.$eval(attrs.myFiles + "=$files", { $files: e.target.files });
                    scope.$apply();
                });
        }
    });
}
