var app = angular.module("tableApp", ['ui.bootstrap', 'angularTable', 'angular.chosen', 'ngSanitize', 'bcherny/formatAsCurrency']);
app.service("myService",
    function ($http) {
        //lấy thông tin phòng ban theo công ty
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
        this.ListOrganizationUnitWhereParent = function (ParentID) {
            var data = {
                ParentID: ParentID
            }
            var response = $http({
                method: "POST",
                url: "/OrganizationUnit/GetOrganizationUnitWhereParent",
                data: JSON.stringify(data),
                dataType: "json"
            });
            return response;
        }
        this.ListOrganizationUnitWhereCompany = function (CompanyID) {
            var data = {
                CompanyID: CompanyID
            }
            var response = $http({
                method: "POST",
                url: "/OrganizationUnit/GetOrganizationUnitWhereCompany",
                data: JSON.stringify(data),
                dataType: "json"
            });
            return response;
        }
        this.getDataByGloballist = function (ParentID) {
            var data = {
                ParentID: ParentID
            }
            var response = $http({
                method: "POST",
                url: "/Common/GetDataByGloballist",
                data: JSON.stringify(data),
                dataType: "json"
            });
            return response;
        }
        this.getDataByGloballistNotTree = function (ParentID) {
            var data = {
                ParentID: ParentID
            }
            var response = $http({
                method: "POST",
                url: "/Common/GetDataByGloballistnotTree",
                data: JSON.stringify(data),
                dataType: "json"
            });
            return response;
        }
        //lấy thông tin tỉnh thành theo quốc gia
        this.getListProvince = function (CountryID) {
            var data = {
                CountryID: CountryID
            }
            var response = $http({
                method: "POST",
                url: "/Employee/GetProvinceByCountry",
                data: JSON.stringify(data),
                dataType: "json"
            });
            return response;
        }
        //-----------------OrganizationUnit----------------------
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
        //lấy thông tin các dropdown
        this.getDropdown = function (data) {
            var response = $http({
                method: "GET",
                url: data.url,
                data: JSON.stringify(data),
                dataType: "json"
            });
            return response;
        }
        //lấy dữ liệu
        this.getData = function (data) {
            var response = $http({
                method: "GET",
                url: data.url,
                data: JSON.stringify(data),
                dataType: "json"
            });
            return response;
        }
        //truyền dữ liệu lên controller
        this.postData = function (data) {
            var response = $http({
                method: "POST",
                url: data.url,
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

        this.ImportData = function (url, importData) {
            var response = $http({
                method: "POST",
                url: url,
                data: importData,
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
            })
            return response;
        }
        this.getDataByIdAndCustomer = function (id, CustomerID, idTable, dataEditUrl) {
            var data = {
                idTable: idTable,
                id: id,
                CustomerID: CustomerID
            }

            var response = $http({
                method: "POST",
                url: dataEditUrl,
                data: JSON.stringify(data),
                dataType: "json"
            })
            return response;
        }
        this.getDataByWithMonthYearId = function (id, month, year, idTable, dataEditUrl) {
            var data = {
                idTable: idTable,
                id: id,
                month: month,
                year: year
            }

            var response = $http({
                method: "POST",
                url: dataEditUrl,
                data: JSON.stringify(data),
                dataType: "json"
            })
            return response;
        }

        this.FormEditDayMiss = function (data, tableUrl) {

            var response = $http({
                method: "POST",
                url: tableUrl,
                data: JSON.stringify(data),
                dataType: "json"
            });
            return response;
        }

        this.getDropdownChangeCustId = function (data, tableUrl) {
            var response = $http({
                method: "POST",
                url: tableUrl,
                data: JSON.stringify(data),
                dataType: "json"
            });
            return response;
        }
        this.HR_WorkingDaySupplement_GetListId = function (data, tableUrl) {
            var response = $http({
                method: "POST",
                url: tableUrl,
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