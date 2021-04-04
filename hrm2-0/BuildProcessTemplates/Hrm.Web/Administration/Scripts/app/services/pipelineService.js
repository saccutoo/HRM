(function () {

    "use strict";

    hrmApplication.factory("pipelineService", ["$http", function ($http) {
        return {
            saveMergerOrganization: function () {
                var response = $http({
                    method: "post",
                    url: "/Admin/Organization/saveMergerOrganization",
                    cache: false,
                    data: {
                        data: data,
                        listOrganizationId: listOrganizationId
                    },
                    contentType: "application/json"
                });
                return response;
            },

        };
    }]);
})();