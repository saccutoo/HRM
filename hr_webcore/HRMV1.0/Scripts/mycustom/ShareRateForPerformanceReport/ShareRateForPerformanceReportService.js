var app = angular.module("tableApp", ['ui.bootstrap']);
app.service("myService",
    function ($http) {

        //this.getTableAction = function (idTable) {
        //    var data = {
        //        idTable: idTable
        //    }
        //    var response = $http({
        //        method: "POST",
        //        url: "/Common/GetTableAction",
        //        data: JSON.stringify(data),
        //        dataType: "json"
        //    });
        //    return response;
        //}

    });