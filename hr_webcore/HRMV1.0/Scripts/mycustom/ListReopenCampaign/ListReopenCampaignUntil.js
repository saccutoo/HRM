function BuildTable(appName, controllerName, tableUrl, viewType) {
    app.controller(controllerName,
        function ($scope, myService, $filter, $rootScope, $window, $http) {
            // $scope.test = $filter('mm/dd/yyyy')("2018/01/01", 'yyyy/MM/dd');
            $scope.maxSize = 5; // Limit number for pagination display number
            $scope.totalCount = 0; // Total number of items in all pages. initial
            $scope.pageIndex = 1; // Current page number. First page is 1
            $scope.pageSizeSelected = 10;
            $rootScope.viewType = viewType; //viewType báo cáo theo từng loại
            $scope.isShowFilter = false;
            $scope.typeEnds = [{ name: "Và", value: " and " }, { name: "Hoặc", value: " or " }];
            $scope.filterColumnsChoosed = [];
                       //set tu ngay-den ngay mac dinh cho build table
            var date = new Date(), y = date.getFullYear(), m = date.getMonth();
            $scope.FromDate = $filter("date")(new Date(y, m, 1));
            $scope.ToDate = $filter("date")(Date.now());
            //end - set tu ngay-den ngay mac dinh cho build table
            $rootScope.GetColumnWhereCondition = 1; //cot duoc hien thi theo quy/thang PerformanceReport
            $scope.FromDateToDate = true; //cho phep build table hien thi tu ngay nao den ngay nao            $rootScope.DoNotLoad = 1; //cho build table khong load data lan dau
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


            var res = $rootScope.data;



            //-----------------List-----------     
            //Call và truy?n d? li?u sang builtable g?i l?i d? li?u
            $rootScope.childmethod = function () {
                var filter = $scope.getFilterValue();
                $rootScope.$broadcast("CallParentMethodWithFilter", { res, filter });
            }

            //-----------------List-End---------

            // -----------------fllter------------
            $scope.getFilterValue = function () {
                var lstObj = $scope.filterColumnsChoosed;
                var stringFilter = "";
                var len = lstObj.length - 1;
                for (var key in lstObj) {
                    var obj = lstObj[key];
                    var tmpFilter = obj.filterSelected.ColumnName + obj.typeFilterSelected.value.replace("#", obj.filterSelected.Type === 3 ? $filter('date')(new Date(obj.textValue), 'yyyy/MM/dd') : obj.textValue) + (parseInt(String(key)) === len ? "" : obj.typeEndsSeleted.value);
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

            //-------------------Excel-End----------
            $('.col-md-3.selectdiv').hide();
        });
}
