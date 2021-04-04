
var app = angular.module("tableApp", ['ui.bootstrap']);
app.service("myService",
    function ($http) {
        this.ListPositionName = function () {
            var response = $http({
                method: "GET",
                url: "/InsurancePosition/GetListPositionName",
                dataType: "json"
            });
            return response;
        }
        this.ListInsuranceStatus = function () {
            var response = $http({
                method: "GET",
                url: "/Insurance/GetInsuranceStatus",
                dataType: "json"
            });
            return response;
        }
        this.ListInsuranceType = function () {
            var response = $http({
                method: "GET",
                url: "/Insurance/GetInsuranceType",
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
            debugger;
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
    });