(function (angular) {
    'use strict';
    hrmApplication.controller('saveOrganizationController', [
        '$scope', '$rootScope', '$anchorScroll', '$location', '$timeout', '$window', '$compile', '$interpolate', 'organizationService', '$http',
        function ($scope, $rootScope, $anchorScroll, $location, $timeout, $window, $compile, $interpolate, organizationService, $http) {
            $scope.Organization = {};
            $scope.SaveOrganization = function () {
                organizationService.SaveOrganization($scope.Organization).then(function (response) {
                    if (response.data.isSuccess == true) {
                        organizationService.ReloadTreeOrganization().then(function (response) {
                            var $tempDiv = angular.element('<div>').html(response.data);
                            response = $tempDiv;
                            var compilerElem = $compile(response)($scope);
                            $('#organization-tree').html(compilerElem);
                            $("#myModal").modal('hide');
                            alert('ok');
                        });
                    }
                    else {
                        alert('Đã tồn tại mã phòng ban');
                    }
                })
            }
        }]);
})(window.angular);


