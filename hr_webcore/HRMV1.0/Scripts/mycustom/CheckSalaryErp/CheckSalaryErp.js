function BuildTable(appName, controllerName, tableUrl, tableUrl1, tableUrl2, Notification, Error) {
    app.controller(controllerName,
        function ($scope, myService, $filter, $rootScope, $window, $http) {
            $scope.isShowFilter = false;
            $scope.FromDateToDate = true;
            $scope.pageIndex = 1; 
            $scope.pageSizeSelected = 5;
            $scope.filterColumnsChoosed = [];
          
            //Model đượct truyền ra từ directive build table
            $scope.DetailAccountReportData = {};

            
       
         
           
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
