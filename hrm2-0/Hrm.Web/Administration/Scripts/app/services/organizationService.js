(function () {

    "use strict";

    hrmApplication.factory("organizationService", ["$http", function ($http) {
        return {
            GetStaffById: function (pageNumber, pageSize, filter, parentId) {
                var response = $http({
                    method: "post",
                    url: "/Admin/Organization/GetStaffById",
                    cache: false,
                    data: {
                        param: {
                            PageSize: pageSize,
                            PageNumber: pageNumber,
                            FilterField: filter
                        },
                        parentId: parentId
                    },
                    contentType: "application/json"
                });
                return response;
            },

            SaveOrganization: function (data) {
                var response = $http({
                    method: "post",
                    url: "/Admin/Organization/SaveOrganization",
                    cache: false,
                    data: data,
                    contentType: "application/json"
                });
                return response;
            },

            ReloadTreeOrganization: function () {
                var response = $http({
                    method: "post",
                    url: "/Admin/Organization/ReloadTreeOrganization",
                    cache: false,
                    contentType: "application/json"
                });
                return response;
            },

            savePersonnelTransfer: function (data) {
                var response = $http({
                    method: "post",
                    url: "/Admin/Organization/savePersonnelTransfer",
                    cache: false,
                    data: data,
                    contentType: "application/json"
                });
                return response;
            },

            saveMergerOrganization: function (data, listOrganizationId) {
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