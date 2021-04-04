(function (angular) {
    'use strict';
    hrmApplication.controller('saveMasterDataController', [
        '$scope', '$rootScope', '$anchorScroll', '$location', '$timeout', '$window', '$compile', '$interpolate', 'masterDataService', '$http',
        function ($scope, $rootScope, $anchorScroll, $location, $timeout, $window, $compile, $interpolate, masterDataService, $http) {

            $scope.ListColor = [
                { position: 0, style: "color:white;border:1px solid #2ca01c;position:relative", color: "white;" },
                { position: 1, style: "background-color:#2ca01c", color: "#2ca01c" },
                { position: 2, style: "background-color:#fb89eb", color: "#fb89eb" },
                { position: 3, style: "background-color:#ffbb42", color: "#ffbb42" },
                { position: 4, style: "background-color:#cd52fe", color: "#cd52fe" },
                { position: 5, style: "background-color:blue", color: "blue" },
                { position: 6, style: "background-color:#ccc", color: "#ccc" },
                { position: 7, style: "background-color:black", color: "black" },
            ];

            $scope.MasterData = {}
            $scope.ListMasterDataByLanguage = [
                {
                    MasterData: { LanguageId: "0", MasterDataId: $rootScope.GroupId }
                }
            ];

            $scope.AddDataLanguage = function () {
                if ($scope.ListMasterDataByLanguage.length > 0) {
                    $scope.ListMasterDataByLanguage.push(
                    {
                        MasterData: {LanguageId:"0",MasterDataId:$rootScope.GroupId},
                        Style: { "margin-top": "10px" }
                    });
                }
                else {
                    $scope.ListMasterDataByLanguage.push(
                        {
                            MasterData: {},
                        }
                    );
                }

            }

            $scope.removeDataLanguage = function (index) {
                if ($scope.ListMasterDataByLanguage[0].Style != null) {
                    $scope.ListMasterDataByLanguage[0].Style = {};
                }
                $scope.ListMasterDataByLanguage.splice(index, 1);
               
            }


            $scope.SaveMasterData = function () {
                $scope.MasterData.GroupId = $rootScope.GroupId;
                $scope.ListMasterData = [];
                for (var i = 0; i < $scope.ListMasterDataByLanguage.length; i++) {
                    $scope.ListMasterData.push($scope.ListMasterDataByLanguage[i].MasterData)
                }
                masterDataService.SaveListMasterData($scope.MasterData, $scope.ListMasterData).then(function (response) {
                    if (response.data.Success == true) {
                        $scope.pageIndex = $("#" + tableName + "-paging-current-page").val();
                        $scope.pageSize = $("#" + tableName + "-paging-items-per-page-value").val();

                        masterDataService.GetAllMasterData($scope.pageIndex, $scope.pageSize, $rootScope.GroupId, filterStr).then(function (response) {
                            $('#' + tableName).html(response.data);
                        });
                        alert("OK");
                        $scope.MasterData = {};
                        $scope.ListMasterDataByLanguage = [
                            {
                                MasterData: { LanguageId: "0", MasterDataId: $rootScope.GroupId }
                            }
                        ];
                        $("#myModal").modal('hide');

                    }
                    else {
                        alert("False")
                    }
                });
            }
            
            $scope.SaveAndContinue = function () {
                $scope.MasterData.GroupId = $rootScope.GroupId;
                $scope.ListMasterData = [];
                for (var i = 0; i < $scope.ListMasterDataByLanguage.length; i++) {
                    $scope.ListMasterData.push($scope.ListMasterDataByLanguage[i].MasterData)
                }
                masterDataService.SaveListMasterData($scope.MasterData, $scope.ListMasterData).then(function (response) {
                    if (response.data.Success == true) {

                        $scope.pageIndex = $("#" + tableName + "-paging-current-page").val();
                        $scope.pageSize = $("#" + tableName + "-paging-items-per-page-value").val();

                        masterDataService.GetAllMasterData($scope.pageIndex, $scope.pageSize, $rootScope.GroupId, filterStr).then(function (response) {
                            $('#' + tableName).html(response.data);
                        });
                        alert("OK");
                        $scope.MasterData = {};
                        $scope.ListMasterDataByLanguage = [
                            {
                                MasterData: { LanguageId: "0", MasterDataId: $rootScope.GroupId }
                            }
                        ];
                    }
                    else {
                        alert("False")
                    }
                });
            }
            $scope.clickColor = function (position) {
                for (var i = 0; i < $scope.ListColor.length; i++) {
                    if($scope.ListColor[i].position==position){
                        $scope.MasterData.Color = $scope.ListColor[i].color;
                        break;
                    }
                    else if ($scope.ListColor[i].position == 0) {
                        $scope.MasterData.Color = '';
                    }
                }
               
            }
            //$("#click-more-color").click(function () {
            //    debugger
            //    $("#more-color").spectrum("toggle");
            //    return false
            //})

            $scope.clickMoreColor = function () {
                $timeout(function () {
                    $("#more-color").spectrum("show");
                }, 1)
            }

            $timeout(function () {
                $("#more-color").spectrum({
                    color: "#f00",
                    showInput: true,
                    showInitial: true,
                    allowEmpty: true,
                    change: function (color) {
                        if (color == null) {
                            $scope.MasterData.Color = '';
                        }
                        else {
                            $scope.MasterData.Color = color.toHexString();
                        }
                        $scope.$apply();
                    }
                });
            },300)
           
        }]);
})(window.angular);


