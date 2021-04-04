(function (angular) {
    'use strict';
    hrmApplication.controller('pipelineController', [
        '$scope', '$rootScope', '$anchorScroll', '$location', '$timeout', '$window', '$compile', '$interpolate', 'pipelineService', '$http',
        function ($scope, $rootScope, $anchorScroll, $location, $timeout, $window, $compile, $interpolate, pipelineService, $http) {
            $scope.ShowModalAddOrganization = function (name) {
                var options = {
                    id: 'add-pipeline',
                    title: addNewUnit.Text,
                    url: '/Admin/pipeline/ShowFormAddPipeline',
                    //width:1000,
                    //data: '{GroupId: ' + coaTypeId + '}',
                    idform: 'frm-add-pipeline',
                    //urlback: '/Contract/GetContractCoaInfoList',
                    //databack: '{contractId:' + contractId + '}',
                    //divload: 'GridContractCoaInfo',
                    //fnNameReload: 'ResetSelectCoaInfo'
                };
                $rootScope.CreatePopup(options);
            };

        }]);
})(window.angular);


