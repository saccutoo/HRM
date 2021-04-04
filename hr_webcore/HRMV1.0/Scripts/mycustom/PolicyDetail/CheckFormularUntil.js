function BuildTable(appName, controllerName) {
    app.controller(controllerName,
        function ($scope, myService, $filter, $rootScope, $compile, $http) {
            $scope.CheckFormularModel = {};
            $scope.Init = function () {
                $scope.CheckFormularModel.RewardStatus = 3535;
                $scope.IsClickExcel = false;
                $scope.ListRewardLevel();
            }
            //------------Dropdown Level Reward-------------
             $scope.ListRewardLevel = function () {
                 var data = {
                     url: "/Common/GetDataByGloballistnotTree?parentid=" + 3541
                 }
                 var list = myService.getDropdown(data);
                 list.then(function (res) {
                     $scope.listRewardSource = res.data.result;
                 },
                    function (res) {
                        $scope.msg = "Error";
                    });
             };

             $scope.ExcelClick = function (form) {
                 $scope.IsClickExcel = true;
                 if (!form.$error.required) {
                     var filterString = $scope.CheckFormularModel.Month;
                     var filterString1 = $scope.CheckFormularModel.Year;
                     var filterString2 = $scope.CheckFormularModel.RewardStatus;
                     window.location = "/PolicyDetail/CheckFomularReturnExcel" + "?month=" + filterString + "&year=" + filterString1 + "&TypeReward=" + filterString2;
                 }
            };
        });
}

