(function (angular) {
    'use strict';
    hrmApplication.controller('masterDataController', [
        '$scope', '$rootScope', '$anchorScroll', '$location', '$timeout', '$window', '$compile', '$interpolate', 'masterDataService', '$http',
        function ($scope, $rootScope, $anchorScroll, $location, $timeout, $window, $compile, $interpolate, masterDataService, $http) {
            $scope.isShowAddClick = true;
            $scope.isShowAddForm = false;
            $scope.Data = {};
            $scope.Data.GroupId = "0";
            $scope.Name = "ItemsPerPage";
            $rootScope.GroupId = 0;

            //Hàm lấy thông tin master data
            $scope.GetAllMasterData = function (GroupId) {
                $scope.pageIndex = $("#" + tableName + "-paging-current-page").val();
                $scope.pageSize = $("#" + tableName + "-paging-items-per-page-value").val();

                masterDataService.GetAllMasterData($scope.pageIndex, $scope.pageSize, GroupId, '').then(function (response) {
                    $('#' + tableName).html(response.data);
                });
            }


            //Mở form thêm danh mục
            $scope.ShowAddForm = function () {
                $scope.isShowAddClick = false;
                $scope.isShowAddForm = true;
            }

            //Đóng form thêm danh mục
            $scope.Colse = function () {
                $scope.isShowAddClick = true;
                $scope.isShowAddForm = false;
            }

            //Lọc theo danh mục cha
            $scope.clickCategory = function (list) {
                $scope.Name = list.Name;
                $rootScope.GroupId = list.Id;
                $scope.GetAllMasterData(list.Id);
                filterStr = "GroupId=" + $rootScope.GroupId;
            }

            //Thêm anh mục cha
            $scope.SaveCategoryParent = function (form) {
                masterDataService.SaveMasterData($scope.Data).then(function (response) {
                    if (response.data.Data != null) {
                        masterDataService.ReLoad().then(function (response) {
                            var $tempDiv = angular.element('<div>').html(response.data);
                            response = $tempDiv;
                            var compilerElem = $compile(response)($scope);
                            $('#category-body-ul').html(compilerElem);
                            $scope.Colse();
                            alert('ok')
                        })
                    }
                });         
            }

            $scope.ShowModalAdd = function (name) {
                var options = {
                    id: 'frmAddCategory',
                    url: '/Admin/MasterData/ShowFormAddMasterData',
                    width: 600,
                    isNotificationPoup: false,
                    align: "center",
                    //data: '{GroupId: ' + coaTypeId + '}',
                    idform: 'frmAddOrEditContract',
                    //urlback: '/Contract/GetContractCoaInfoList',
                    //databack: '{contractId:' + contractId + '}',
                    //divload: 'GridContractCoaInfo',
                    //fnNameReload: 'ResetSelectCoaInfo'
                };
                $rootScope.CreatePopup(options);
            };        
        }]);
})(window.angular);


