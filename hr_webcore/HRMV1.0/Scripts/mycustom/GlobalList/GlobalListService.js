var app = angular.module("tableApp", ['ui.bootstrap', 'angularTreeview', 'angularTable', 'angular.chosen', 'ngSanitize']);
app.service("myService",
    function ($http) {
        this.getListStatus = function () {
            var response = $http({
                method: "GET",
                url: "/Common/GetStatus88",
                dataType: "json"
            });
            return response;
        }

        this.getListParent = function () {
            var response = $http({
                method: "GET",
                url: "/GlobalList/GetListGlobalistByParentId",
                dataType: "json"
            });
            return response;
        }

        this.getCategoryList = function (parameter) {
            var data = {
                parameter: parameter
            }
            var response = $http({
                method: "POST",
                url: "/GlobalList/GetCategoryList",
                data: JSON.stringify(data),
                dataType: "json"
            });
            return response;
        }

        this.getFullName = function () {
            var response = $http({
                method: "GET",
                url: "/GlobalList/GetFullName",
                dataType: "json"
            });
            return response;
        }

        this.getListDepartment = function (pageIndex, pageSize, filter) {
            var data = {
                pageIndex: pageIndex ? pageIndex : 1,
                pageSize: pageSize ? pageSize : 500,
                filter: filter ? filter : ""
            }
            var response = $http({
                method: "POST",
                url: "/Employee/GetListDepartment",
                data: JSON.stringify(data),
                dataType: "json"
            });
            return response;
        }

        this.getTableAction = function (idTable) {
            var data = {
                idTable: idTable
            }
            var response = $http({
                method: "POST",
                url: "/Common/GetTableAction",
                data: JSON.stringify(data),
                dataType: "json"
            });
            return response;
        }

        this.getTableInformation = function (tableUrl) {
            var data = {
                url: tableUrl
            }
            var response = $http({
                method: "POST",
                url: "/Common/GetTableInfo",
                data: JSON.stringify(data),
                dataType: "json"
            });
            return response;
        }
        this.UpdateData = function (url, editData) {
            var response = $http({
                method: "POST",
                url: url,
                data: JSON.stringify(editData),
                dataType: "json"
            });
            return response;
        }

        this.GetColumns = function (tableUrl) {
            var data = {
                url: tableUrl
            }
            var response = $http({
                method: "POST",
                url: "/Common/GetTableColumns",
                data: JSON.stringify(data),
                dataType: "json"
            });
            return response;
        }

        this.GetTableData = function (data, tableUrl) {
            var response = $http({
                method: "POST",
                url: tableUrl,
                data: JSON.stringify(data),
                dataType: "json"
            });
            return response;
        }
        this.deleteAction = function (id, tableId, deleteUrl) {
            var data = {
                id: id,
                idTable: tableId
            }
            var response = $http({
                method: "POST",
                url: deleteUrl,
                data: JSON.stringify(data),
                dataType: "json"
            });
            return response;
        }




        this.getFilterColumns = function (idTable) {
            ShowLoader();
            var data = {
                idTable: idTable
            }
            var response = $http({
                method: "POST",
                url: "/Common/GetTableFilterColumns",
                data: JSON.stringify(data),
                dataType: "json"
            });
            return response;
        };

        this.getDataById = function (id, idTable, dataEditUrl) {
            var data = {
                idTable: idTable,
                id: id
            }
            var response = $http({
                method: "POST",
                url: dataEditUrl,
                data: JSON.stringify(data),
                dataType: "json"
            });
            return response;
        }
        this.UpdateColumn = function (data) {
            var response = $http({
                method: "POST",
                url: "/Common/SaveColumn",
                data: {
                    data: data,
                },
                dataType: "json"
            });
            return response;
        }
        this.GetAllColumns = function (tableUrl) {
            var data = {
                url: tableUrl
            }
            var response = $http({
                method: "POST",
                url: "/Common/getAllColumns",
                data: JSON.stringify(data),
                dataType: "json"
            });
            return response;
        }
    });