(function (angular) {
    'use strict';
    hrmApplication.controller('organizationController', [
        '$scope', '$rootScope', '$anchorScroll', '$location', '$timeout', '$window', '$compile', '$interpolate', 'organizationService', '$http',
        function ($scope, $rootScope, $anchorScroll, $location, $timeout, $window, $compile, $interpolate, organizationService, $http) {
            filterStr = "0";
            $rootScope.parentId = 0;
            $scope.ShowModalAddOrganization = function (name) {
                var options = {
                    id: 'add-organization',
                    url: '/Admin/Organization/ShowFormAddOrganization',
                    isNotificationPoup: true,
                    align: "center",
                    //data: '{GroupId: ' + coaTypeId + '}',
                    idform: 'frm-add-Organization',
                    //urlback: '/Contract/GetContractCoaInfoList',
                    //databack: '{contractId:' + contractId + '}',
                    //divload: 'GridContractCoaInfo',
                    //fnNameReload: 'ResetSelectCoaInfo'
                };
                $rootScope.CreatePopup(options);
            };

            $scope.ShowModalMergerOrganization = function (name) {
                var options = {
                    id: 'merger-0rganization',
                    url: '/Admin/Organization/ShowFormMergerOrganization',
                    //width:1000,
                    isNotificationPoup: true,
                    align: "right",
                    data: '{parentId: ' + 3 + '}',
                    idform: 'frm-merger-organization',
                    //urlback: '/Contract/GetContractCoaInfoList',
                    //databack: '{contractId:' + contractId + '}',
                    //divload: 'GridContractCoaInfo',
                    //fnNameReload: 'ResetSelectCoaInfo'
                };
                $rootScope.CreatePopup(options);

            };

            $scope.ShowModalPersonnelTransfer = function (name) {
                var options = {
                    id: 'merger-0rganization',
                    url: '/Admin/Organization/ShowFormPersonnelTransfer',
                    isNotificationPoup: true,
                    align: "right",
                    //width:1000,
                    //data: '{GroupId: ' + coaTypeId + '}',
                    idform: 'frm-merger-organization',
                    //urlback: '/Contract/GetContractCoaInfoList',
                    //databack: '{contractId:' + contractId + '}',
                    //divload: 'GridContractCoaInfo',
                    //fnNameReload: 'ResetSelectCoaInfo'
                };
                $rootScope.CreatePopup(options);
            };

            $rootScope.ClickTreeOrganization = function (item) {
                $scope.pageIndex = $("#" + tableName + "-paging-current-page").val();
                $scope.pageSize = $("#" + tableName + "-paging-items-per-page-value").val();
                filterStr = item;
                $rootScope.parentId = item;
                organizationService.GetStaffById($scope.pageIndex, $scope.pageSize, filterStr, item).then(function (response) {
                    $('#' + tableName).html(response.data);
                });
            }         
        }]);
})(window.angular);


