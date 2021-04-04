var app = angular.module("tableApp", ['ui.bootstrap','angular.chosen']);
app.service("myService",
    function ($http) {
        this.GetColumnDataById = function (columnId) {
            var data = {
                columnId: columnId
            }
            var response = $http({
                method: "POST",
                url: "/Common/GetColumnDataById",
                data: JSON.stringify(data),
                dataType: "json"
            });
            return response;
        }
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