(function () {

    "use strict";

    hrmApplication.factory("masterDataService", ["$http", function ($http) {
        return {
            GetAllMasterData: function (pageNumber, pageSize, groupId, filter) {
                var response = $http({
                    method: "post",
                    url: "/Admin/MasterData/GetAllMasterData",
                    cache: false,
                    data: {
                        param: {
                            PageSize: pageSize,
                            PageNumber: pageNumber,
                            FilterField: filter
                        },
                        groupId:groupId
                    },
                    contentType: "application/json"
                });
                return response;
            },
            GetAllMasterGroupId: function () {
                var response = $http({
                    method: "post",
                    url: "/Admin/MasterData/GetAllMasterGroupId",
                    cache: false,
                    contentType: "application/json"
                });
                return response;
            },
            SaveMasterData: function (Data) {
                var response = $http({
                    method: "post",
                    url: "/Admin/MasterData/SaveMasterData",
                    cache: false,
                    data:{
                        Data:Data
                    },
                    contentType: "application/json"
                });
                return response;
            },
            SaveListMasterData: function (Data, ListData) {
                var response = $http({
                    method: "post",
                    url: "/Admin/MasterData/SaveListMasterData",
                    cache: false,
                    data: {
                        data: Data,
                        ListData: ListData
                    },
                    contentType: "application/json"
                });
                return response;
            },
            ReLoad: function () {
                var response = $http({
                    method: "post",
                    url: "/Admin/MasterData/ReloadCategory",
                    cache: false,
                    contentType: "application/json"
                });
                return response;
            },
        };
    }]);
})();