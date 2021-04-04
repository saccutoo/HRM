(function (angular) {
    'use strict';
    hrmApplication.controller('saveMergerOrganizationController', [
        '$scope', '$rootScope', '$anchorScroll', '$location', '$timeout', '$window', '$compile', '$interpolate', 'organizationService', '$http',
        function ($scope, $rootScope, $anchorScroll, $location, $timeout, $window, $compile, $interpolate, organizationService, $http) {
            $scope.MergerOrganization = {};
            $scope.checkMergeOrganizationOld = true;
            $scope.checkMergeOrganizationNew = false;
            $scope.saveMergerOrganization = function (value) {
                $scope.listOrganizationId = '64,65';
                organizationService.saveMergerOrganization($scope.MergerOrganization, $scope.listOrganizationId).then(function (response) {
                    if (response.data.isSuccess == true) {
                        var item = $rootScope.parentId;
                        organizationService.GetStaffById($scope.pageIndex, $scope.pageSize, filterStr, item).then(function (response) {
                            $('#' + tableName).html(response.data);
                        });
                        organizationService.ReloadTreeOrganization().then(function (response) {
                            var $tempDiv = angular.element('<div>').html(response.data);
                            response = $tempDiv;
                            var compilerElem = $compile(response)($scope);
                            $('#organization-tree').html(compilerElem);
                            alert("ok");
                            $("#myModal").modal('hide');
                        });
                    }
                    else {
                        alert('Đã tồn tại mã phòng ban');
                    }
                })
                

            }
            $scope.changeOrganization = function () {

                $scope.listOrganizations = angular.copy(listOrganizations);
                var obj = $scope.listOrganizations.filter(function (obj) {
                    return (obj.Id.toString() == $scope.MergerOrganization.Id)
                });
                $scope.MergerOrganization = {};
                $scope.MergerOrganization = obj[0];
                select($scope.MergerOrganization.Status, 'status-list');
                select($scope.MergerOrganization.PrantId, 'organization-parentId-list');
                select($scope.MergerOrganization.CurrencyTypeId, 'currency-list');
                select($scope.MergerOrganization.OrganizationType, 'type-list');
                select($scope.MergerOrganization.Branch, 'branch-list');

            }
            $scope.changeMergeOrganizationNew=function(){
                $scope.checkMergeOrganizationOld = false;
                $scope.checkMergeOrganizationNew = true;
            }
            $scope.changeMergeOrganizationOld = function () {
                $scope.checkMergeOrganizationOld = true;
                $scope.checkMergeOrganizationNew = false;
            }
        }]);
})(window.angular);


