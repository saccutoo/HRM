(function (angular) {
    'use strict';
    hrmApplication.controller('savePersonnelTransferController', [
        '$scope', '$rootScope', '$anchorScroll', '$location', '$timeout', '$window', '$compile', '$interpolate', 'organizationService', '$http',
        function ($scope, $rootScope, $anchorScroll, $location, $timeout, $window, $compile, $interpolate, organizationService, $http) {
            $scope.PersonnelTransfer = {};
            $scope.SavePersonnelTransfer = function () {
                $scope.PersonnelTransfer.StaffId = 1;
                organizationService.savePersonnelTransfer($scope.PersonnelTransfer).then(function (response) {
                    if (response.data.isSuccess == true) {
                        alert("ok");
                        $("#myModal").modal('hide');
                        $scope.pageIndex = $("#" + tableName + "-paging-current-page").val();
                        $scope.pageSize = $("#" + tableName + "-paging-items-per-page-value").val();
                        var item = $rootScope.parentId;
                        organizationService.GetStaffById($scope.pageIndex, $scope.pageSize, filterStr, item).then(function (response) {
                            $('#' + tableName).html(response.data);
                        });
                    }
                    else {
                        alert('Đã tồn tại mã phòng ban');
                    }
                })
            }
        }]);
})(window.angular);


