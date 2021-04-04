
(function (angular) {
    'use strict';
   
    var appModule = angular.module('angularTable', []);
    appModule.directive('buildTable', ['myService', '$filter', '$window', '$http', '$rootScope', function (myService, $filter, $window, $http, $rootScope) {
        return {
            restrict: 'E',
            scope: {
                tableModel: '=?',
                tableUrl: '=',
                tableAdd: '=?',
                tableUpload: '=?',
                tableEdit: '=?',
                tableCopy: '=?',
                tableView: '=?',
                tableDinhdang: '=?',
                tableDelete: '=?',
                tableExcelClick: '=?',
                tableParamFilter: '=?',
                options: '&?'
            },

            //templateUrl: '/Scripts/directive/angular-table/table-template.html',
            templateUrl: '/Common/BuildTable',
            replace: true,
            link: function (scope, element, attr) {

                //param integer n: length of decimal
                //param integer x: length of whole part
                //param mixed   s: sections delimiter
                //param mixed   c: decimal delimiter

                Number.prototype.format = function (n, x, s, c) {
                    var re = '\\d(?=(\\d{' + (x || 3) + '})+' + (n > 0 ? '\\D' : '$') + ')',
                        num = this.toFixed(Math.max(0, ~~n));

                    return (c ? num.replace('.', c) : num).replace(new RegExp(re, 'g'), '$&' + (s || ','));
                };

                //12345678.9.format(2, 3, '.', ',');  // "12.345.678,90"
                //123456.789.format(4, 4, ' ', ':');  // "12 3456:7890"
                //12345678.9.format(0, 3, '-'); 

                scope.filterColumnsChoosed = [];
                scope.customFilter = ""; // filter add from controller
                scope.maxSize = 5; // Limit number for pagination display number.
                scope.totalCount = 0; // Total number of items in all pages. initialize as a zero
                scope.pageIndex = 1; // Current page number. First page is 1.-->
                scope.pageSizeSelected = 5;
                scope.typeEnds = [{ name: "Và", value: " and " }, { name: "Hoặc", value: " or " }];
                scope.tblInfo = {};
                scope.quickFilter = {
                
                }; // quick filter data
                scope.currentLanguage = currentLanguage;

                scope.listFilterColumns = []; // list columns bind quick search
                scope.typeFilterA = []; // filter have type value = 1
                scope.typeFilterB = []; // filter have type value = 2
                scope.typeFilterC = []; // filter have type value = 3
                scope.typeFilterD = []; // filter have type value = 4
                scope.filterColumnsItem = {}; // data filter template
                scope.typeFilter = [];
                scope.tableModel = {}; //model public function 
                scope.totalCount = 0;                           
                $rootScope.ImportExcel = function () {
                    var dt = Loading();
                        var config = {
                            headers: {
                                "Content-Type": undefined,
                            }
                        };
                        var formData = new $window.FormData();
                        formData.append("file-0", scope.files[0]);
                        $http.post($rootScope.data.url, formData, config).
                            then(function (res) {
                                AppendToToastr(res.data.result.IsSuccess, notification, res.data.result.Message, 500, 5000);
                                $('.modal').modal('hide');
                                scope.GetListData();
                                dt.finish();
                            }, function (res) {
                                AppendToToastr(false, notification, errorNotiFalse);

                            });
                }
              
                //----- table-------
                scope.getTableInfo = function () {
                    var getData = myService.getTableInformation(scope.tableUrl);
                    getData.then(function (emp) {
                        if (emp && emp.data && emp.data.result) {
                            scope.tblInfo = emp.data.result;
                            scope.lstPageSize = scope.tblInfo.PageSizeList.split(',');
                            scope.pageSizeSelected = scope.tblInfo.PageSize;
                            scope.GetAddPermission(emp.data.result.id);
                            scope.getFilterColumns(scope.tblInfo);
                        }

                    }, function (emp) {
                        AppendToToastr(false, notification, errorNotiFalse);
                    });
                }
                //get permission action button
                scope.GetAddPermission = function (idTable) {
                    var tblAction = myService.getTableAction(idTable);
                    tblAction.then(function (emp) {
                        scope.tablePermission = emp.data.result;
                        scope.getColumns();
                        // $scope.BuildAddButton(emp.data.result);
                    }, function (emp) {
                        AppendToToastr(false, notification, errorNotiFalse);
                    });
                }
                //get columns table
                scope.getColumns = function () {
                    var getData = myService.GetColumns(scope.tableUrl);
                    getData.then(function (emp) {
                        scope.tblColumns = emp.data.result;
                        scope.tblColumns = BindWidthToCss(scope.tblColumns);
                        scope.GetListData();
                    }, function (emp) {
                        AppendToToastr(false, notification, errorNotiFalse);
                    });
                }

                scope.GetListData = function () {
                    //scope.bindQuickFilterToFilter(scope.tblColumns);
                    scope.tableParamData = scope.tableParamFilter != null ? scope.tableParamFilter : {};
                    scope.tableParamData['pageIndex'] = scope.pageIndex ? scope.pageIndex : 1;
                    scope.tableParamData['pageSize'] = scope.pageSizeSelected ? scope.pageSizeSelected : 20;
                    scope.tableParamData['filter1'] = scope.filter1;
                    scope.tableParamData['filter2'] = scope.filter2;
                    scope.tableParamData['filter3'] = scope.filter3;
                    scope.tableParamData['filter4'] = scope.filter4;
                    scope.tableParamData['filter5'] = scope.filter5;
                    scope.tableParamData['filter6'] = scope.filter6;
                    scope.tableParamData['filter7'] = scope.filter7;
                    if ($rootScope.StaffID!=null) {
                        scope.tableParamData['SessionStaffID'] = $rootScope.StaffID;
                    }
                    scope.tableParamData['filter'] = scope.getFilterValue();
                    var getDataTbl = myService.GetTableData(scope.tableParamData, scope.tableUrl);
                    getDataTbl.then(function (emp) {
                        scope.tblDatas = emp.data.employees;
                        scope.totalCount = emp.data.totalCount;
                        scope.lstTotal = emp.data.lstTotal;
                        scope.SetTotalByColumns = function (totalName, dataFomat) {
                            if (!angular.isUndefined(totalName) && totalName !== null && scope.lstTotal != undefined) {
                                if (scope.lstTotal[totalName] != undefined && scope.lstTotal[totalName] != null) {
                                    return parseFloat(scope.lstTotal[totalName]).format(dataFomat, 3, ',', '.');
                                }
                                return scope.lstTotal[totalName];
                            }
                            return "";
                        };

                        HiddenLoader();
                    }, function (ex) {
                        AppendToToastr(false, notification, errorNotiFalse);
                    });
                }


                //----end get table

                //------ xử lý filter
                scope.addFilterColumns = function () {
                    var data = angular.copy(scope.filterColumnsItem)
                    scope.filterColumnsChoosed.push(data);
                }
                scope.removeColumnFilterByIndex = function (index) {
                    scope.filterColumnsChoosed.splice(index, 1);
                }

                scope.getFilterColumns = function (tblInfo) {
                    if (!tblInfo) return;
                    var filter = myService.getFilterColumns(tblInfo.id);
                    filter.then(function (res) {
                        HiddenLoader();
                        //scope.FilterColumnsItem = angular.copy(res.data.result);
                        scope.listFilterColumns = angular.copy(res.data.result);
                        scope.GetSelectBox(scope.listFilterColumns);

                        scope.filterColumnsItem = {
                            filterColumns: angular.copy(res.data.result),
                            typeFilter: [],
                            typeEnds: scope.typeEnds,
                            textValue: "",
                            typeEndsSeleted: scope.typeEnds[0],
                            hasLink: false,
                            SelectBox: scope.listFilterColumns.SelectBox
                        }

                        scope.addFilterColumns();
                    }, function (res) {
                        AppendToToastr(false, notification, errorNotiFalse);
                    });
                }
                scope.GetSelectBox = function (data) {
                    angular.forEach(data, function (item) {
                        if (item.Type == 4) {
                            item = scope.GetColumnDataById(item);

                        }
                    })
                }
                scope.GetColumnDataById = function (item) {
                    var getDataTbl = myService.GetColumnDataById(item.Id);
                    getDataTbl.then(function (emp) {
                        item['SelectBox'] = emp.data.result;
                    }, function (ex) {
                        AppendToToastr(false, notification, errorNotiFalse);
                    });
                }

                scope.cancelFilterClick = function () {
                    scope.filterColumnsChoosed = [];
                    scope.GetListData();
                    if (scope.toggle) {
                        scope.toggle = false;
                    }


                }
                scope.showFilterApplyButton = function () {
                    var lstObj = scope.filterColumnsChoosed;
                    if (lstObj.length === 0) {
                        return false;
                    }
                    for (var key in lstObj) {
                        var obj = lstObj[key];
                        if (obj.textValue === "") {
                            return false;
                        }
                    }
                    return true;
                }

                //toggle filter
                scope.toggleChange = function () {
                    if (scope.isShowFilter) {
                        if (scope.toggle) scope.toggle = false;
                        else scope.toggle = true;
                        //  return true;
                    }
                    // return false;
                }

                scope.getFilterValue = function () {
                    var lstObj = scope.filterColumnsChoosed;
                    lstObj = scope.getLinkGroup(lstObj);
                    var stringFilter = "";
                    var len = lstObj.length - 1;
                    for (var key in lstObj) {
                        var obj = lstObj[key];

                        var valueFilter = '';
                        if (obj.typeFilterSelected) {
                            switch (obj.filterSelected.Type) {
                                case 3: valueFilter = "'" + $filter('date')(new Date(obj.textValue), 'yyyy/MM/dd') + "'"; break;
                                case 4: valueFilter = obj.textValue.Value; break;
                                default: valueFilter = obj.textValue; break;
                            }
                        }

                        var textValue = (obj.typeFilterSelected ? obj.typeFilterSelected.Descriptions.replace("#value", valueFilter) : '');

                        var tmpFilter =
                            (obj.positionLink == true ? obj.typeLinkValue : "")
                            + (obj.filterSelected ? obj.filterSelected.ColumnName : "") + " "
                            + textValue
                            + (obj.positionLink == false ? obj.typeLinkValue : "")
                            + (parseInt(String(key)) === len ? "" : obj.typeEndsSeleted.value);
                        stringFilter += tmpFilter;
                    }
                    if (scope.customFilter) {
                        if (stringFilter && stringFilter != " ")
                            stringFilter += " and ";
                        stringFilter += scope.customFilter;
                    }
                    return stringFilter;
                };
                scope.getLinkGroup = function (lstObj) {
                    var length = lstObj.length;
                    if (length > 1) {
                        lstObj[0].typeLinkValue = lstObj[0].hasLink == true ? '(' : '';
                        lstObj[0].positionLink = true;

                        lstObj[length - 1].typeLinkValue = lstObj[length - 2].hasLink == true ? ')' : '';
                        lstObj[length - 1].positionLink = false;
                        var pre, now;

                        if (length > 2) {
                            for (var i = 1; i < lstObj.length - 1; i++) {
                                pre = lstObj[i - 1].hasLink;
                                now = lstObj[i].hasLink;
                                if (pre && !now) {
                                    lstObj[i].typeLinkValue = ')';
                                    lstObj[i].positionLink = false;
                                }
                                else if (!pre && now) {
                                    lstObj[i].typeLinkValue = '(';
                                    lstObj[i].positionLink = true;
                                }
                                else
                                    lstObj[i].typeLinkValue = '';
                            }
                        }
                    }
                    return lstObj;
                }

                //getlisttypfilter
                scope.getListTypeFilter = function () {
                    var getData = myService.getDataByGloballistNotTree(6);
                    getData.then(function (emp) {
                        scope.typeFilter = emp.data.result;
                        SplitTypeFilter(scope.typeFilter);
                    }, function (emp) {
                        AppendToToastr(false, notification, errorNotiFalse);
                    });
                }
                function SplitTypeFilter(source) {
                    angular.forEach(source, function (item) {
                        switch (item.Value) {
                            case '1': scope.typeFilterA.push(item); break;
                            case '2': scope.typeFilterB.push(item); break;
                            case '3': scope.typeFilterC.push(item); break;
                            case '4': scope.typeFilterD.push(item); break;
                            default: break;
                        }
                    })
                }
                scope.filterColumnsChange = function (filterSelected, filterItemChoosed, index) {
                    var type = filterSelected.Type;
                    switch (type) {
                        case 1: scope.filterColumnsChoosed[index].typeFilter = scope.typeFilterA; break;
                        case 2: scope.filterColumnsChoosed[index].typeFilter = scope.typeFilterB; break;
                        case 3: scope.filterColumnsChoosed[index].typeFilter = scope.typeFilterC; break;
                        case 4: scope.filterColumnsChoosed[index].typeFilter = scope.typeFilterD;
                            filterSelected = scope.GetColumnDataById(filterSelected);
                            break;
                        default: break;
                    }
                    filterItemChoosed.typeFilterSelected = filterItemChoosed.typeFilter[0];
                };
                //----------- quick filter
                scope.bindQuickFilterToFilter = function () {
                    var lisObj = scope.listFilterColumns;
                    var rs = []
                    for (var i = 0; i < lisObj.length; i++) {
                        if (scope.quickFilter[lisObj[i].ColumnName]) {
                            var item = {
                                filterColumns: lisObj,
                                typeFilter: scope.typeFilter,
                                typeEnds: scope.typeEnds,
                                textValue: scope.quickFilter[lisObj[i].ColumnName],
                                typeFilterSelected: {},
                                typeEndsSeleted: scope.typeEnds[0],
                                filterSelected: lisObj[i],
                                hasLink: false,
                                positionLink: false,
                                typeLinkValue: '',
                            }
                            rs.push(item)
                        }
                    }

                    for (var i = 0; i < rs.length; i++) {
                        rs[i].typeFilter = scope.getFilter(rs[i].filterSelected);
                        rs[i].typeFilterSelected = rs[i].typeFilter[0];
                    }
                    scope.filterColumnsChoosed = rs;
                    scope.isShowFilter = true;
                    scope.GetListData();
                }
                scope.getFilter = function (filterSelected) {
                    var type = filterSelected.Type;
                    switch (type) {
                        case 1: return scope.typeFilterA;
                        case 2: return scope.typeFilterB;
                        case 3: return scope.typeFilterC;
                        case 4: return scope.typeFilterD;
                        default: break;
                    }
                };
                //------end xử lý filter

                //---- xử lý các phần exten của table

                scope.changePageSize = function () {
                    scope.pageIndex = 1;
                    scope.GetListData();
                };
                scope.pageChanged = function () {
                    scope.GetListData();
                };

                scope.formatData = function (type, value) {
                    if (type === 3) {
                        return FormatDate(value);
                    }
                    return value;
                }
                scope.setShowTypeEnd = function (index) {
                    if (scope.filterColumnsChoosed.length - 1 === index) {
                        return false;
                    };
                    return true;
                }

                function BindWidthToCss(model) {
                    var styleWidth = '';
                    angular.forEach(model, function (item) {
                        styleWidth = (item.Width) ? ('width : ' + item.Width + 'px') : '100%';
                        item.Css = (item.Css) ? (item.Css + ';' + styleWidth) : styleWidth;
                    })
                    return model;
                }
                function FormatDate(inputDate) {
                    var date = new Date(inputDate);
                    if (!isNaN(date.getTime())) {
                        var day = date.getDate().toString();
                        var month = (date.getMonth() + 1).toString();
                        // Months use 0 index.
                        return (day[1] ? day : '0' + day[0]) +
                            '/' +
                            (month[1] ? month : '0' + month[0]) +
                            '/' +
                            date.getFullYear();
                    }
                };

                scope.Addlinkfilter = function (index, flag) {
                    scope.filterColumnsChoosed[index].hasLink = flag;
                }

                scope.showLink = function (index) {
                    return scope.filterColumnsChoosed[index].hasLink;
                }

                scope.SetTotalByColumns = function (totalName, dataFomat) {
                    if (!angular.isUndefined(totalName) && totalName !== null && scope.lstTotal != undefined) {
                        if (scope.lstTotal[totalName] != undefined && scope.lstTotal[totalName] != null) {
                            return parseFloat(scope.lstTotal[totalName]).format(dataFomat, 3, ',', '.');
                        }
                        return scope.lstTotal[totalName];
                    }
                    return "";
                };
                //---- end xử lý các phần exten của table


                //exten function or controller
                scope.tableModel.reloadByFilter = function (strFilter) {
                    scope.customFilter = strFilter;
                    scope.GetListData();
                }
                scope.tableModel.reload = function () {
                    scope.GetListData();
                }
                //----end extenn--

                $rootScope.ExcelExtensionClick = function () {
                    var filterString = scope.getFilterValue();
                    window.location = $rootScope.tableInfo.ExcelUrl + "?filterString=" + filterString + "&pageIndex=" + scope.pageIndex + "&pageSize=" + scope.pageSizeSelected + "&Type=" + $rootScope.Type;
                }

                scope.init = function () {
                    scope.getTableInfo();
                    scope.getListTypeFilter();

                }
                scope.init();
                $('.table-panel').scroll(function (e) {
                    $('.table-panel').css("top", -$(".table-panel").scrollTop() - 1);
                    $('.divTableHeading .divTableCell').css("top", $(".table-panel").scrollTop() - 1);
                });               
            }
        };
    }]);
    appModule.directive('checkSalaryErp', ['myService', '$filter', '$rootScope', '$timeout', '$window', function (myService, $filter, $rootScope, $timeout, $window) {
        return {
            restrict: 'E',
            scope: {
                tableModel: '=?',
                tableUrl: '=',
                tableAdd: '=?',
                tableEdit: '=?',
                tableCopy: '=?',
                tableDelete: '=?',
                tableParamFilter: '=?',
                tableChangePageSize: '=?',
                tablePageChanged: '=?',
                notification: '=',
                validateDatetime: '=',
                options: '&?'
            },

            templateUrl: '/Common/CheckSalaryErp',
            replace: true,
            link: function (scope, element, attr) {
                //param integer n: length of decimal
                //param integer x: length of whole part
                //param mixed   s: sections delimiter
                //param mixed   c: decimal delimiter

                Number.prototype.format = function (n, x, s, c) {
                    var re = '\\d(?=(\\d{' + (x || 3) + '})+' + (n > 0 ? '\\D' : '$') + ')',
                        num = this.toFixed(Math.max(0, ~~n));

                    return (c ? num.replace('.', c) : num).replace(new RegExp(re, 'g'), '$&' + (s || ','));
                };

                //12345678.9.format(2, 3, '.', ',');  // "12.345.678,90"
                //123456.789.format(4, 4, ' ', ':');  // "12 3456:7890"
                //12345678.9.format(0, 3, '-');       // "12-345-679"

                scope.filterColumnsChoosed = [];
                scope.customFilter = ""; // filter add from controller
                scope.maxSize = 5; // Limit number for pagination display number.
                scope.totalCount = 0; // Total number of items in all pages. initialize as a zero
                scope.pageIndex = 1; // Current page number. First page is 1.-->
                scope.pageSizeSelected = 10;
                scope.typeEnds = [{ name: "Và", value: " and " }, { name: "Hoặc", value: " or " }];
                scope.tblInfo = {};
                scope.quickFilter = {

                }; // quick filter data
                scope.listFilterColumns = []; // list columns bind quick search
                scope.typeFilterA = []; // filter have type value = 1
                scope.typeFilterB = []; // filter have type value = 2
                scope.typeFilterC = []; // filter have type value = 3
                scope.typeFilterD = []; // filter have type value = 4
                scope.filterColumnsItem = {}; // data filter template
                scope.typeFilter = [];
                scope.tableModel = {}; //model public function 
                scope.totalCount = 0;
                //convert get datetime now
                var today = new Date();
                var dd = today.getDate();
                var mm = today.getMonth() + 1;
                var yyyy = today.getFullYear();
                if (dd < 10) {
                    dd = '0' + dd;
                }

                if (mm < 10) {
                    mm = '0' + mm;
                }

                today = mm + '/' + yyyy;

                scope.FromMonth = today;
                scope.ToMonth = today;

              
            

                var endYear = new Date(new Date().getFullYear(), 11, 31);
                $("#datePicker").datepicker({
                    autoclose: true,
                    format: "mm/yyyy",
                    startDate: "1/2013",
                    endDate: endYear,
                    startView: "months",
                    minViewMode: "months",
                    maxViewMode: "years"
                }).datepicker("setDate", new Date());
                $("#datePicker2").datepicker({
                    autoclose: true,
                    format: "mm/yyyy",
                    startDate: "1/2013",
                    endDate: endYear,
                    startView: "months",
                    minViewMode: "months",
                    maxViewMode: "years"
                }).datepicker("setDate", new Date());
              

                //----- table-------
                scope.getTableInfo = function () {
                    var getData = myService.getTableInformation(scope.tableUrl);
                    getData.then(function (emp) {
                        if (emp && emp.data && emp.data.result) {
                            scope.tblInfo = emp.data.result;
                            scope.lstPageSize = scope.tblInfo.PageSizeList.split(',');
                            scope.pageSizeSelected = scope.tblInfo.PageSize;
                            scope.GetAddPermission(emp.data.result.id);
                            scope.getFilterColumns(scope.tblInfo);
                        }

                    }, function (emp) {
                        AppendToToastr(false, scope.notification, errorNotiFalse);
                    });
                }
                //get permission action button
                scope.GetAddPermission = function (idTable) {
                    var tblAction = myService.getTableAction(idTable);
                    tblAction.then(function (emp) {
                        scope.tablePermission = emp.data.result;
                        scope.getColumns();
                     
                    }, function (emp) {
                        AppendToToastr(false, scope.notification, errorNotiFalse);
                    });
                }

        
                //get columns table
                scope.getColumns = function () {
                    var getData = myService.GetColumns(scope.tableUrl);
                    getData.then(function (emp) {
                        scope.tblColumns = emp.data.result;
                        scope.GetListData();
                    }, function (emp) {
                        AppendToToastr(false, scope.notification, errorNotiFalse);
                    });
                }
                scope.GetListData = function () {
                    //if (scope.FromDate == null || scope.ToDate == null) {
                    //    AppendToToastr(false, scope.notification, scope.validateDatetime);
                    //} else {
                        var dt = Loading();
                    
                        //scope.bindQuickFilterToFilter(scope.tblColumns);
                        scope.tableParamData = scope.tableParamFilter != null ? scope.tableParamFilter : {};
                        scope.tableParamData['pageIndex'] = scope.pageIndex;
                        scope.tableParamData['pageSize'] = scope.pageSizeSelected;
                        scope.tableParamData['viewType'] = $rootScope.viewType;
                        //scope.tableParamData['FromDate'] = new Date(scope.FromDate.setHours(0, 0, 0));
                        //scope.tableParamData['ToDate'] = new Date(scope.ToDate.setHours(23, 59, 59));
                        //scope.tableParamData['filter1'] = $rootScope.data.filter1;
                        //scope.tableParamData['filter2'] = $rootScope.data.filter2;
                        //scope.tableParamData['filter3'] = $rootScope.data.filter3;
                        //scope.tableParamData['filter4'] = $rootScope.data.filter4;
                        //scope.tableParamData['filter5'] = $rootScope.data.filter5;
                        //scope.tableParamData['filter6'] = $rootScope.data.filter6;
                        //scope.tableParamData['filter7'] = $rootScope.data.filter7;
                        scope.tableParamData['filter'] = scope.getFilterValue();
                        var getDataTbl = myService.GetTableData(scope.tableParamData, scope.tableUrl);
                        getDataTbl.then(function (emp) {
                            scope.tblDatas = emp.data.employees;
                            scope.totalCount = emp.data.totalCount;
                            scope.lstTotal = emp.data.lstTotal;
                            scope.SetTotalByColumns = function (totalName, dataFomat) {
                                if (!angular.isUndefined(totalName) &&
                                    totalName !== null &&
                                    scope.lstTotal != undefined) {
                                    if (scope.lstTotal[totalName] != undefined &&
                                        scope.lstTotal[totalName] != null) {
                                        return parseFloat(scope.lstTotal[totalName]).format(dataFomat, 3, ',', '.');
                                    }
                                    return scope.lstTotal[totalName];
                                }
                                return "";
                            };
                            
                            dt.finish();

                        },
                            function (ex) {
                                AppendToToastr(false, scope.notification, errorNotiFalse);
                            });
                    //}
                }

               

                //---end get table

                //------ xử lý filter
                scope.addFilterColumns = function () {
                    var data = angular.copy(scope.filterColumnsItem)
                    scope.filterColumnsChoosed.push(data);
                }
                scope.removeColumnFilterByIndex = function (index) {
                    scope.filterColumnsChoosed.splice(index, 1);
                }

                scope.getFilterColumns = function (tblInfo) {
                    if (!tblInfo) return;
                    var filter = myService.getFilterColumns(tblInfo.id);
                    filter.then(function (res) {
                        HiddenLoader();
                        //scope.FilterColumnsItem = angular.copy(res.data.result);
                        scope.listFilterColumns = angular.copy(res.data.result);
                        scope.GetSelectBox(scope.listFilterColumns);

                        scope.filterColumnsItem = {
                            filterColumns: angular.copy(res.data.result),
                            typeFilter: [],
                            typeEnds: scope.typeEnds,
                            textValue: "",
                            typeEndsSeleted: scope.typeEnds[0],
                            hasLink: false,
                            SelectBox: scope.listFilterColumns.SelectBox
                        }

                        scope.addFilterColumns();
                    }, function (res) {
                        AppendToToastr(false, scope.notification, errorNotiFalse);
                    });
                }
                scope.GetSelectBox = function (data) {
                    angular.forEach(data, function (item) {
                        if (item.Type == 4) {
                            item = scope.GetColumnDataById(item);

                        }
                    })
                }
                scope.GetColumnDataById = function (item) {
                    var getDataTbl = myService.GetColumnDataById(item.Id);
                    getDataTbl.then(function (emp) {
                        item['SelectBox'] = emp.data.result;
                    }, function (ex) {
                        AppendToToastr(false, scope.notification, errorNotiFalse);
                    });
                }

                scope.cancelFilterClick = function () {
                    scope.filterColumnsChoosed = [];
                    scope.GetListData();
                }

                scope.showFilterApplyButton = function () {
                    var lstObj = scope.filterColumnsChoosed;
                    if (lstObj.length === 0) {
                        return false;
                    }
                    for (var key in lstObj) {
                        var obj = lstObj[key];
                        if (obj.textValue === "") {
                            return false;
                        }
                    }
                    return true;
                }
                //toggle filter
                scope.toggleChange = function () {
                    if (scope.isShowFilter) {
                        if (scope.toggle) scope.toggle = false;
                        else scope.toggle = true;
                        //  return true;
                    }
                    // return false;
                }
                scope.getFilterValue = function () {
                    var lstObj = scope.filterColumnsChoosed;
                    lstObj = scope.getLinkGroup(lstObj);
                    var stringFilter = "";
                    var len = lstObj.length - 1;
                    for (var key in lstObj) {
                        var obj = lstObj[key];

                        var valueFilter = '';
                        if (obj.typeFilterSelected) {
                            switch (obj.filterSelected.Type) {
                                case 3: valueFilter = "'" + $filter('date')(new Date(obj.textValue), 'yyyy/MM/dd') + "'"; break;
                                case 4: valueFilter = obj.textValue.Value; break;
                                default: valueFilter = obj.textValue; break;
                            }
                        }

                        var textValue = (obj.typeFilterSelected ? obj.typeFilterSelected.Descriptions.replace("#value", valueFilter) : '');

                        var tmpFilter =
                            (obj.positionLink == true ? obj.typeLinkValue : "")
                            + (obj.filterSelected ? obj.filterSelected.ColumnName : "") + " "
                            + textValue
                            + (obj.positionLink == false ? obj.typeLinkValue : "")
                            + (parseInt(String(key)) === len ? "" : obj.typeEndsSeleted.value);
                        stringFilter += tmpFilter;
                    }
                    if (scope.customFilter) {
                        if (stringFilter && stringFilter != " ")
                            stringFilter += " and ";
                        stringFilter += scope.customFilter;
                    }
                 
                    //var array1 = scope.FromMonth.split("/").map(Number);
                    //array1.unshift(1);
                    //var array2 = scope.ToMonth.split("/").map(Number);
                    //array2.unshift(1);
                    //var fromDate = new Date(array1);
                    //var toDate = new Date(array2);
                    //var string1 = convertDateTimeDefaultToString(fromDate);
                    //var string2 = convertDateTimeDefaultToString(toDate);
                    //if (scope.FromMonth != null && scope.ToMonth != null) {
                    //    if (stringFilter && stringFilter != " ")
                    //        stringFilter += " and ";
                    //    stringFilter += "[TimeConvert] BETWEEN '" + string1 + "' AND '" + string2 + "'";
                    //}         
                    return stringFilter;
                };
                scope.getLinkGroup = function (lstObj) {
                    var length = lstObj.length;
                    if (length > 1) {
                        lstObj[0].typeLinkValue = lstObj[0].hasLink == true ? '(' : '';
                        lstObj[0].positionLink = true;

                        lstObj[length - 1].typeLinkValue = lstObj[length - 2].hasLink == true ? ')' : '';
                        lstObj[length - 1].positionLink = false;
                        var pre, now;

                        if (length > 2) {
                            for (var i = 1; i < lstObj.length - 1; i++) {
                                pre = lstObj[i - 1].hasLink;
                                now = lstObj[i].hasLink;
                                if (pre && !now) {
                                    lstObj[i].typeLinkValue = ')';
                                    lstObj[i].positionLink = false;
                                }
                                else if (!pre && now) {
                                    lstObj[i].typeLinkValue = '(';
                                    lstObj[i].positionLink = true;
                                }
                                else
                                    lstObj[i].typeLinkValue = '';
                            }
                        }
                    }
                    return lstObj;
                }

                //getlisttypfilter
                scope.getListTypeFilter = function () {
                    var getData = myService.getDataByGloballistNotTree(6);
                    getData.then(function (emp) {
                        scope.typeFilter = emp.data.result;
                        SplitTypeFilter(scope.typeFilter);
                    }, function (emp) {
                        AppendToToastr(false, scope.notification, errorNotiFalse);
                    });
                }
                function SplitTypeFilter(source) {
                    angular.forEach(source, function (item) {
                        switch (item.Value) {
                            case '1': scope.typeFilterA.push(item); break;
                            case '2': scope.typeFilterB.push(item); break;
                            case '3': scope.typeFilterC.push(item); break;
                            case '4': scope.typeFilterD.push(item); break;
                            default: break;
                        }
                    })
                }
                scope.filterColumnsChange = function (filterSelected, filterItemChoosed, index) {
                    var type = filterSelected.Type;
                    switch (type) {
                        case 1: scope.filterColumnsChoosed[index].typeFilter = scope.typeFilterA; break;
                        case 2: scope.filterColumnsChoosed[index].typeFilter = scope.typeFilterB; break;
                        case 3: scope.filterColumnsChoosed[index].typeFilter = scope.typeFilterC; break;
                        case 4: scope.filterColumnsChoosed[index].typeFilter = scope.typeFilterD;
                            filterSelected = scope.GetColumnDataById(filterSelected);
                            break;
                        default: break;
                    }
                };
                //----------- quick filter
                scope.bindQuickFilterToFilter = function () {
                    var lisObj = scope.listFilterColumns;
                    var rs = []
                    for (var i = 0; i < lisObj.length; i++) {
                        if (scope.quickFilter[lisObj[i].ColumnName]) {
                            var item = {
                                filterColumns: lisObj,
                                typeFilter: scope.typeFilter,
                                typeEnds: scope.typeEnds,
                                textValue: scope.quickFilter[lisObj[i].ColumnName],
                                typeFilterSelected: {},
                                typeEndsSeleted: scope.typeEnds[0],
                                filterSelected: lisObj[i],
                                hasLink: false,
                                positionLink: false,
                                typeLinkValue: '',
                            }
                            rs.push(item)
                        }
                    }

                    for (var i = 0; i < rs.length; i++) {
                        rs[i].typeFilter = scope.getFilter(rs[i].filterSelected);
                        rs[i].typeFilterSelected = rs[i].typeFilter[0];
                    }
                    scope.filterColumnsChoosed = rs;
                    scope.isShowFilter = true;
                    scope.GetListData();
                }
                scope.bindQuickFilterToFilterButNotLoadData = function () {
                    var lisObj = scope.listFilterColumns;
                    var rs = []
                    for (var i = 0; i < lisObj.length; i++) {
                        if (scope.quickFilter[lisObj[i].ColumnName]) {
                            var item = {
                                filterColumns: lisObj,
                                typeFilter: scope.typeFilter,
                                typeEnds: scope.typeEnds,
                                textValue: scope.quickFilter[lisObj[i].ColumnName],
                                typeFilterSelected: {},
                                typeEndsSeleted: scope.typeEnds[0],
                                filterSelected: lisObj[i],
                                hasLink: false,
                                positionLink: false,
                                typeLinkValue: '',
                            }
                            rs.push(item)
                        }
                    }

                    for (var i = 0; i < rs.length; i++) {
                        rs[i].typeFilter = scope.getFilter(rs[i].filterSelected);
                        rs[i].typeFilterSelected = rs[i].typeFilter[0];
                    }
                    scope.filterColumnsChoosed = rs;
                    scope.isShowFilter = true;
                };

                scope.getFilter = function (filterSelected) {
                    var type = filterSelected.Type;
                    switch (type) {
                        case 1: return scope.typeFilterA;
                        case 2: return scope.typeFilterB;
                        case 3: return scope.typeFilterC;
                        case 4: return scope.typeFilterD;
                        default: break;
                    }
                };
                //------end xử lý filter




                //This method is calling from pagination number
                scope.PageChanged = function () {
                        scope.GetListData();
                };

                //This method is calling from dropDown
                scope.ChangePageSize = function () {
                    scope.pageIndex = 1;
                        scope.GetListData();
                };



                scope.formatData = function (type, value) {
                    if (type === 3) {
                        return FormatDate(value);
                    }
                    return value;
                }
                scope.setShowTypeEnd = function (index) {
                    if (scope.filterColumnsChoosed.length - 1 === index) {
                        return false;
                    };
                    return true;
                }

               
                function FormatDate(inputDate) {
                    var date = new Date(inputDate);
                    if (!isNaN(date.getTime())) {
                        var day = date.getDate().toString();
                        var month = (date.getMonth() + 1).toString();
                        // Months use 0 index.
                        return (day[1] ? day : '0' + day[0]) +
                            '/' +
                            (month[1] ? month : '0' + month[0]) +
                            '/' +
                            date.getFullYear();
                    }
                };

                function convertDateTimeDefaultToString(datetime) {
                    var months = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
                    var newMonth = months[datetime.getMonth()];
                    var x;
                    switch (newMonth) {
                        case "January":
                            x = '01';
                            break;
                        case "February":
                            x = '02';
                            break;
                        case "March":
                            x = '03';
                            break;
                        case "April":
                            x = '04';
                            break;
                        case "May":
                            x = '05';
                            break;
                        case "June":
                            x = '06';
                            break;
                        case "July":
                            x = '07';
                            break;
                        case "August":
                            x = '08';
                            break;
                        case "September":
                            x = '09';
                            break;
                        case "October":
                            x = '10';
                            break;
                        case "November":
                            x = '11';
                            break;
                        case "December":
                            x = '12';
                            break;
                        default:
                        // code block
                    }
                    var getDate = datetime.getDate();
                    var getMonth = x;
                    var getFullYear = datetime.getFullYear();
                    var fullDateTime = ("0" + getDate).slice(-2) + "-" + getMonth + "-" + getFullYear + " " + ("0" + datetime.getHours()).slice(-2) + ":" + ("0" + datetime.getMinutes()).slice(-2) + ":" + ("0" + datetime.getSeconds()).slice(-2);
                    return fullDateTime;
                }

                //-------------------Excel--------------
                scope.ExcelClick = function () {
                    var filterString1 = scope.getFilterValue();
                    var filterString = filterString1.replace(/%/g, "!!");
                    window.location = scope.tblInfo.ExcelUrl + "?filterString=" + filterString + "&pageIndex=" + scope.pageIndex + "&pageSize=" + scope.pageSizeSelected;

                }
                scope.Addlinkfilter = function (index, flag) {
                    scope.filterColumnsChoosed[index].hasLink = flag;
                }

                scope.showLink = function (index) {
                    return scope.filterColumnsChoosed[index].hasLink;
                }

                scope.SetTotalByColumns = function (totalName) {
                    if (!angular.isUndefined(totalName) && totalName !== null && scope.lstTotal != undefined) {
                        if (scope.lstTotal[totalName] != undefined && scope.lstTotal[totalName] != null) {
                            return parseFloat(scope.lstTotal[totalName]).format(scope.lstTotal[totalName].DataFomat, 3, ',', '.');
                        }
                        return scope.lstTotal[totalName];
                    }
                    return "";
                };
                //---- end xử lý các phần exten của table


                //exten function or controller
                scope.tableModel.reloadByFilter = function (strFilter) {
                    scope.customFilter = strFilter;
                    scope.GetListData();
                }
                scope.tableModel.reload = function () {
                    scope.GetListData();
                }
                //----end extenn--


                scope.init = function () {
                    scope.getTableInfo($rootScope.DoNotLoad);
                    scope.getListTypeFilter();

                }

                scope.init();

                $('.fixedTable').scroll(function (e) {
                    var prevLeft = 0;
                    $('.fixedTable').scroll(function (evt) {
                        var currentLeft = $(this).scrollLeft();
                        if (prevLeft != currentLeft) {
                            prevLeft = currentLeft;
                            $('.divTable .divTableHeading .divTableCell.fix-column').css("z-index", "1000");
                            $('.divTable .divTableRow .divTableCell.fix-column').css("z-index", "0");

                        } else {
                            $('.divTable .divTableHeading .divTableCell.fix-column').css("z-index", "1000");
                            $('.divTable .divTableRow .divTableCell.fix-column').css("z-index", "0");
                        }
                        $('.fixedTable').css("left", -$(".fixedTable").scrollLeft() - 1);
                        $('.fixedTable .divTableCell.fix-column').css("left", $(".fixedTable").scrollLeft() - 1);
                        $('.fixedTable').css("top", -$(".fixedTable").scrollTop() - 1);
                        $('.divTableHeading .divTableCell').css("top", $(".fixedTable").scrollTop() - 1);

                    });
                }
                );
            }
        };
    }]);
    appModule.directive('compile', ['$compile', function ($compile) {

        return function (scope, element, attrs) {
            scope.$watch(
                function (scope) {
                    // watch the 'compile' expression for changes
                    return scope.$eval(attrs.compile);
                },
                function (value) {
                    // when the 'compile' expression changes
                    // assign it into the current DOM                 
                    element.html(value.replace('#tableTitle.Edit#', editTitle).replace('#tableTitle.Delete#', deleteTitle).replace('#Department#', department).replace('#TypeMoney#', typeMoney).replace('#Employee#', employeeOption).replace('#tableTitle.Hide#', hideTitle).replace('#Contract#', contractType).replace('#NameReplace#', optionShowName).replace('#FullNameReplace#', optionShowFullName).replace('#TypeStatus#', statusType));

                    // compile the new DOM and link it to the current
                    // scope.
                    // NOTE: we only compile .childNodes so that
                    // we don't get into infinite loop compiling ourselves
                    $compile(element.contents())(scope);

                }
            );
        };
    }]);
    appModule.directive('buildFixedTable', ['myService', '$filter', '$rootScope', '$timeout', '$window', function (myService, $filter, $rootScope, $timeout, $window) {
        return {
            restrict: 'E',
            scope: {
                tableModel: '=?',
                tableUrl: '=',
                tableAdd: '=?',
                tableAddList:'=?',
                tableEdit: '=?',
                tableCopy: '=?',
                tableDelete: '=?',
                tableImport: '=?',
                tableDownLoadFile: '=?',
                tableParamFilter: '=?',
                tableChangePageSize: '=?',
                tablePageChanged: '=?',
                tableOpeningAdditionalWork: '=?',
                notification: '=',
                validateDatetime:'=',
                options: '&?'
            },

            templateUrl: '/Common/BuildFixedTable',
            replace: true,
            link: function (scope, element, attr) {
                //param integer n: length of decimal
                //param integer x: length of whole part
                //param mixed   s: sections delimiter
                //param mixed   c: decimal delimiter

                Number.prototype.format = function (n, x, s, c) {
                    var re = '\\d(?=(\\d{' + (x || 3) + '})+' + (n > 0 ? '\\D' : '$') + ')',
                        num = this.toFixed(Math.max(0, ~~n));

                    return (c ? num.replace('.', c) : num).replace(new RegExp(re, 'g'), '$&' + (s || ','));
                };
                
                scope.filterColumnsChoosed = [];
                scope.customFilter = ""; // filter add from controller
                scope.maxSize = 5; // Limit number for pagination display number.
                scope.totalCount = 0; // Total number of items in all pages. initialize as a zero
                scope.pageIndex = 1; // Current page number. First page is 1.-->
                scope.pageSizeSelected = 10;
                scope.typeEnds = [{ name: "Và", value: " and " }, { name: "Hoặc", value: " or " }];
                scope.tblInfo = {};
                scope.quickFilter = scope.$parent.quickFilter !=undefined?scope.$parent.quickFilter:{
                    Date: scope.$parent.Monday
                }; // quick filter data
                $rootScope.SelectFile = function () {
                    $('#FileUploadInput').trigger('click');
                }
                $rootScope.quickFiltercomback = {};
                scope.currentLanguage = currentLanguage;
                scope.listFilterColumns = []; // list columns bind quick search
                scope.typeFilterA = []; // filter have type value = 1
                scope.typeFilterB = []; // filter have type value = 2
                scope.typeFilterC = []; // filter have type value = 3
                scope.typeFilterD = []; // filter have type value = 4
                scope.filterColumnsItem = {}; // data filter template
                scope.typeFilter = [];
                scope.tableModel = {}; //model public function 
                scope.totalCount = 0;
                scope.StaffStatus = scope.$parent.StaffStatus;
                scope.FromDate =scope.$parent.FromDate!=undefined ? scope.$parent.FromDate : Date.now();
                scope.ToDate = scope.$parent.ToDate != undefined ? scope.$parent.ToDate : Date.now();
                scope.FromDateToDate = scope.$parent.FromDateToDate;
                scope.FromDateToDate1 = scope.$parent.FromDateToDate1;
                scope.FormatColumn = scope.$parent.FormatColumn;
                scope.ViewFilter = $rootScope.viewFilter;
                scope.startdate = scope.$parent.FromDate != undefined ? scope.$parent.FromDate : Date.now();
                scope.enddate = scope.$parent.ToDate != undefined ? scope.$parent.ToDate : Date.now();
                scope.OrganizationUnitID = scope.$parent.OrganizationUnitID;
                scope.status = scope.$parent.status;
                scope.showreportldepartment = scope.$parent.showreportldepartment;
                scope.OpeningAdditionalWork = $rootScope.OpeningAdditionalWork;
                scope.AddList = $rootScope.AddList;
                scope.isDownLoad = $rootScope.isDownLoad;
                $('#btnCopy').hide();
                //----- table-------

                scope.getTableInfo = function (DoNotLoad) {
                    var data = {
                        url: "/OrganizationUnit/GetOrganizationUnitWhereParent?ParentID=1276"
                    }
                    var list = myService.getData(data);
                    list.then(function (res) {
                        scope.getListAllOrganizationUnit = res.data.result;
                    }, function (res) {
                        scope.msg = "Error";
                    });
                    var getData = myService.getTableInformation(scope.tableUrl);
                    getData.then(function (emp) {
                        if (emp && emp.data && emp.data.result) {
                            scope.tblInfo = emp.data.result;
                            scope.lstPageSize = scope.tblInfo.PageSizeList.split(',');
                            scope.pageSizeSelected = scope.tblInfo.PageSize;
                            scope.GetAddPermission(emp.data.result.id, DoNotLoad);
                            scope.getFilterColumns(scope.tblInfo);
                        }

                    }, function (emp) {
                        AppendToToastr(false, scope.notification, errorNotiFalse);
                    });
                }
                //get permission action button
                scope.GetAddPermission = function (idTable, DoNotLoad) {
                    var tblAction = myService.getTableAction(idTable);
                    tblAction.then(function (emp) {
                        scope.tablePermission = emp.data.result;
                        scope.getColumns(DoNotLoad);
                        // $scope.BuildAddButton(emp.data.result);
                    }, function (emp) {
                        AppendToToastr(false, scope.notification, errorNotiFalse);
                    });
                }

                //get columns table
                scope.getColumns = function (DoNotLoad) {
                    var getData = myService.GetColumns(scope.tableUrl);
                    getData.then(function (emp) {
                        scope.tblColumns = emp.data.result;
                        if (DoNotLoad == undefined) {
                            scope.GetListData();
                        }
                    }, function (emp) {
                        AppendToToastr(false, scope.notification, errorNotiFalse);
                    });
                }
                scope.GetListData = function () {
                    debugger;
                    if (scope.status == 1) {
                        scope.tableUrl = '/ReportLBDX/Report_L_By_Staff_BDX';
                        scope.getTableInfo(0);
                    }
                    else if (scope.status == 0) {
                        scope.tableUrl = '/ReportLBDX/Report_L_By_Department_BDX';
                        scope.getTableInfo(0);
                    }

                    if (scope.FromDate == null || scope.ToDate == null) {
                        AppendToToastr(false, scope.notification, scope.validateDatetime);
                    } else {
                        var dt = Loading();
                     
                        scope.tableParamData = scope.tableParamFilter != null ? scope.tableParamFilter : {};
                        scope.tableParamData['pageIndex'] = scope.pageIndex;
                        scope.tableParamData['pageSize'] = scope.pageSizeSelected;
                        scope.tableParamData['viewType'] = $rootScope.viewType;
                        scope.tableParamData['FromDate'] = new Date(scope.FromDate.setHours(0, 0, 0));
                        scope.tableParamData['ToDate'] = new Date(scope.ToDate.setHours(23, 59, 59));
                        scope.tableParamData['filter1'] = $rootScope.data.filter1;
                        scope.tableParamData['filter2'] = $rootScope.data.filter2;
                        scope.tableParamData['filter3'] = $rootScope.data.filter3;
                        scope.tableParamData['filter4'] = $rootScope.data.filter4;
                        scope.tableParamData['filter5'] = $rootScope.data.filter5;
                        scope.tableParamData['filter6'] = $rootScope.data.filter6;
                        scope.tableParamData['filter7'] = $rootScope.data.filter7;
                        scope.tableParamData['filter8'] = $rootScope.data.filter8;
                        scope.tableParamData['filter9'] = $rootScope.data.filter9;
                        scope.tableParamData['filter10'] = $rootScope.data.filter10;
                        scope.tableParamData['filter11'] = $rootScope.data.filter11;
                        scope.tableParamData['filter12'] = $rootScope.data.filter12;
                        scope.tableParamData['filter13'] = $rootScope.data.filter13;
                        scope.tableParamData['startdate'] = convertDateTimeDefaultToString(new Date(scope.FromDate.setHours(12, 0, 0)));
                        scope.tableParamData['enddate'] = convertDateTimeDefaultToString(new Date(scope.ToDate.setHours(12, 0, 0)));
                        scope.tableParamData['startdate2'] = convertDateTimeDefaultToString(new Date(scope.FromDate.setHours(12, 0, 0)));
                        scope.tableParamData['enddate2'] = convertDateTimeDefaultToString(new Date(scope.ToDate.setHours(12, 0, 0)));
                        scope.tableParamData['OrganizationUnitID'] = scope.OrganizationUnitID;
                        scope.tableParamData['Period'] = ($('#period').val() != undefined && $('#period').val() != null) ? moment(new Date($('#period').val().replace('string:', ''))).format("DD/MM/YYYY"): '';

                        var filter = scope.getFilterValue();
                        if (scope.tableUrl == "/LogAction/TableServerSideGetData") {
                            if (filter == " ") {
                                scope.tableParamData['filter'] = $rootScope.data.filter;
                            }
                            else if (filter == "") {
                                scope.tableParamData['filter'] = $rootScope.data.filter;
                            }
                            else {
                                scope.tableParamData['filter'] = scope.getFilterValue();
                                var postiton = filter.indexOf("Year");
                                if (postiton == -1) {
                                    if (scope.quickFilter.Year != null || scope.quickFilter.Year != "") {
                                        scope.tableParamData['filter'] += " and " + $rootScope.data.filter;
                                    }
                                }
                            }
                        }
                        else {
                            scope.tableParamData['filter'] = scope.getFilterValue();
                        }

                        var getDataTbl = myService.GetTableData(scope.tableParamData, scope.tableUrl);
                        getDataTbl.then(function (emp) {
                                scope.tblDatas = emp.data.employees;
                                scope.totalCount = emp.data.totalCount;
                                $rootScope.SumCS2 = emp.data.totalCount;
                                $rootScope.TotalAccountActive = emp.data.TotalAccountActive;
                                scope.lstTotal = emp.data.lstTotal;
                                scope.SetTotalByColumns = function(totalName, dataFomat) {
                                    if (!angular.isUndefined(totalName) &&
                                        totalName !== null &&
                                        scope.lstTotal != undefined) {
                                        if (scope.lstTotal[totalName] != undefined &&
                                            scope.lstTotal[totalName] != null) {
                                            return parseFloat(scope.lstTotal[totalName]).format(dataFomat, 3, ',', '.');
                                        }
                                        return scope.lstTotal[totalName];
                                    }
                                    return "";
                                };

                                if ($rootScope.GetColumnWhereCondition != 0 &&
                                    $rootScope.GetColumnWhereCondition != undefined) {
                                    setTimeout(function() {
                                            if (typeof $rootScope.ShowAllOrDetail === "function") {
                                                $rootScope.ShowAllOrDetail();
                                            }
                                        },
                                        200);
                                }
                                if (scope.tableUrl == "/LogAction/TableServerSideGetData") {
                                    if (scope.tblDatas != null) {
                                        for (var i = 0; i < scope.tblDatas.length; i++) {
                                            if (scope.tblDatas[i].Message != null || scope.tblDatas[i].Message == "") {
                                                scope.tblDatas[i].Message = scope.tblDatas[i].Message.replace("{", "");
                                                scope.tblDatas[i].Message = scope.tblDatas[i].Message.replace("}", "");
                                                scope.tblDatas[i].Message = scope.tblDatas[i].Message.split('""').join('');
                                                scope.tblDatas[i].Message = scope.tblDatas[i].Message.split('"').join('');
                                                scope.tblDatas[i].Message = scope.tblDatas[i].Message.split(",").join("<br/>");
                                            }
                                        }
                                    }
                                    
                                }
                                dt.finish();

                            },
                            function(ex) {
                                AppendToToastr(false, scope.notification, errorNotiFalse);
                            });
                    }
                }

                $rootScope.$on("CallParentMethod", function (event, data) {
                    var dt = Loading();
                    //scope.bindQuickFilterToFilter(scope.tblColumns); 
                    scope.tableParamData = scope.tableParamFilter != null ? scope.tableParamFilter : {};
                    scope.tableParamData['pageIndex'] = scope.pageIndex;
                    scope.tableParamData['pageSize'] = scope.pageSizeSelected;
                    scope.tableParamData['viewType'] = $rootScope.viewType;
                    scope.tableParamData['FromDate'] = new Date(scope.FromDate.setHours(0, 0, 0));
                    scope.tableParamData['ToDate'] = new Date(scope.ToDate.setHours(23, 59, 59));
                    scope.tableParamData['filter1'] = data.res.filter1;
                    scope.tableParamData['filter2'] = data.res.filter2;
                    scope.tableParamData['filter3'] = data.res.filter3;
                    scope.tableParamData['filter4'] = data.res.filter4;
                    scope.tableParamData['filter5'] = data.res.filter5;
                    scope.tableParamData['filter6'] = data.res.filter6;
                    scope.tableParamData['filter7'] = data.res.filter7;
                    scope.tableParamData['filter8'] = data.res.filter8;
                    scope.tableParamData['filter9'] = data.res.filter9;
                    scope.tableParamData['filter10'] = $rootScope.data.filter10;
                    scope.tableParamData['filter11'] = $rootScope.data.filter11;
                    scope.tableParamData['filter12'] = $rootScope.data.filter12;
                    scope.tableParamData['filter13'] = $rootScope.data.filter13;
                    scope.tableParamData['filter'] = data.filter + (scope.getFilterValue().trim() != '' && data.filter ? ' and ' + scope.getFilterValue() : scope.getFilterValue());
                    var getDataTbl = myService.GetTableData(scope.tableParamData, scope.tableUrl);
                    getDataTbl.then(function (emp) {
                        scope.tblDatas = emp.data.employees;
                        scope.totalCount = emp.data.totalCount;
                        scope.lstTotal = emp.data.lstTotal;
                        scope.SetTotalByColumns = function (totalName, dataFomat) {
                            if (!angular.isUndefined(totalName) && totalName !== null && scope.lstTotal != undefined) {
                                if (scope.lstTotal[totalName] != undefined && scope.lstTotal[totalName] != null) {
                                    return parseFloat(scope.lstTotal[totalName]).format(dataFomat, 3, ',', '.');
                                }
                                return scope.lstTotal[totalName];
                            }
                            return "";
                        };
                       
                        if ($rootScope.GetColumnWhereCondition != 0 && $rootScope.GetColumnWhereCondition != undefined) {
                            if (data.res.filter8 == 1) {
                                $timeout(function () {
                                    if (typeof $rootScope.getColumnMonth === "function") {
                                        $rootScope.getColumnMonth();
                                    }
                                    if (typeof $rootScope.showColumn === "function") {
                                        $rootScope.showColumn();
                                    }
                                }, 700);
                            }
                            else {
                                $timeout(function () {
                                    if (typeof $rootScope.getColumnQuater === "function") {
                                        $rootScope.getColumnQuater();
                                    }
                                    if (typeof $rootScope.showColumn === "function") {
                                        $rootScope.showColumn();
                                    }
                                }, 700);
                            }
                            setTimeout(function () {
                                if (typeof $rootScope.ShowAllOrDetail === "function") {
                                    $rootScope.ShowAllOrDetail();
                                }
                            }, 200);
                            
                        }
                        dt.finish();
                    }, function (ex) {
                        AppendToToastr(false, scope.notification, errorNotiFalse);
                    });

                });

                $rootScope.$on("CallParentMethodWithFilter", function (event, data) {
                    var dt = Loading();
                    scope.bindQuickFilterToFilterButNotLoadData(scope.tblColumns);
                    scope.tableParamData = scope.tableParamFilter != null ? scope.tableParamFilter : {};
                    scope.tableParamData['pageIndex'] = scope.pageIndex;
                    scope.tableParamData['pageSize'] = scope.pageSizeSelected;
                    scope.tableParamData['viewType'] = $rootScope.viewType;
                    scope.tableParamData['FromDate'] = new Date(scope.FromDate.setHours(0, 0, 0));
                    scope.tableParamData['ToDate'] = new Date(scope.ToDate.setHours(23, 59, 59));
                    scope.tableParamData['filter1'] = data.res.filter1;
                    scope.tableParamData['filter2'] = data.res.filter2;
                    scope.tableParamData['filter3'] = data.res.filter3;
                    scope.tableParamData['filter4'] = data.res.filter4;
                    scope.tableParamData['filter5'] = data.res.filter5;
                    scope.tableParamData['filter6'] = data.res.filter6;
                    scope.tableParamData['filter7'] = data.res.filter7;
                    scope.tableParamData['filter8'] = data.res.filter8;
                    scope.tableParamData['filter9'] = data.res.filter9;
                    scope.tableParamData['filter10'] = $rootScope.data.filter10;
                    scope.tableParamData['filter11'] = $rootScope.data.filter11;
                    scope.tableParamData['filter12'] = $rootScope.data.filter12;
                    scope.tableParamData['filter13'] = $rootScope.data.filter13;
                    scope.tableParamData['filter'] = data.filter + (scope.getFilterValue().trim() != '' && data.filter ? ' and ' + scope.getFilterValue() : scope.getFilterValue());
                    var getDataTbl = myService.GetTableData(scope.tableParamData, scope.tableUrl);
                    getDataTbl.then(function (emp) {
                        scope.tblDatas = emp.data.employees;
                        scope.totalCount = emp.data.totalCount;
                        scope.lstTotal = emp.data.lstTotal;
                        scope.SetTotalByColumns = function (totalName, dataFomat) {
                            if (!angular.isUndefined(totalName) && totalName !== null && scope.lstTotal != undefined) {
                                if (scope.lstTotal[totalName] != undefined && scope.lstTotal[totalName] != null) {
                                   
                                    return parseFloat(scope.lstTotal[totalName]).format(dataFomat, 3, ',', '.');
                                }
                                return scope.lstTotal[totalName];
                            }
                            return "";
                        };
                        if ($rootScope.GetColumnWhereCondition != 0 && $rootScope.GetColumnWhereCondition != undefined) {
                            if (data.res.filter8 == 1) {
                                $timeout(function () {
                                    if (typeof $rootScope.getColumnMonth === "function") {
                                        $rootScope.getColumnMonth();
                                    }
                                    if (typeof $rootScope.showColumn === "function") {
                                        $rootScope.showColumn();
                                    }
                                }, 700);
                            }
                            else {
                                $timeout(function () {
                                    if (typeof $rootScope.getColumnQuater === "function") {
                                        $rootScope.getColumnQuater();
                                    }
                                    if (typeof $rootScope.showColumn === "function") {
                                        $rootScope.showColumn();
                                    }
                                }, 700);
                            }
                            setTimeout(function () {
                                if (typeof $rootScope.ShowAllOrDetail === "function") {
                                    $rootScope.ShowAllOrDetail();
                                }
                            }, 200);
                            
                        }
                        dt.finish();
                    }, function (ex) {
                        AppendToToastr(false, scope.notification, errorNotiFalse);
                    });

                });

                $rootScope.CallCrossController = function () {
                    var data = {};

                    data.res = $rootScope.res1;
                    data.filter = $rootScope.filter1;
                    var dt = Loading();
                    scope.bindQuickFilterToFilterButNotLoadData(scope.tblColumns);
                    scope.tableParamData = scope.tableParamFilter != null ? scope.tableParamFilter : {};
                    scope.tableParamData['pageIndex'] = scope.pageIndex;
                    scope.tableParamData['pageSize'] = scope.pageSizeSelected;
                    scope.tableParamData['viewType'] = $rootScope.viewType;
                    scope.tableParamData['FromDate'] = new Date(scope.FromDate.setHours(0, 0, 0));
                    scope.tableParamData['ToDate'] = new Date(scope.ToDate.setHours(23, 59, 59));
                    scope.tableParamData['filter1'] = data.res.filter1;
                    scope.tableParamData['filter2'] = data.res.filter2;
                    scope.tableParamData['filter3'] = data.res.filter3;
                    scope.tableParamData['filter4'] = data.res.filter4;
                    scope.tableParamData['filter5'] = data.res.filter5;
                    scope.tableParamData['filter6'] = data.res.filter6;
                    scope.tableParamData['filter7'] = data.res.filter7;
                    scope.tableParamData['filter8'] = data.res.filter8;
                    scope.tableParamData['filter9'] = data.res.filter9;
                    scope.tableParamData['filter10'] = $rootScope.data.filter10;
                    scope.tableParamData['filter11'] = $rootScope.data.filter11;
                    scope.tableParamData['filter12'] = $rootScope.data.filter12;
                    scope.tableParamData['filter13'] = $rootScope.data.filter13;
                    scope.tableParamData['filter'] = data.filter + (scope.getFilterValue().trim() != '' && data.filter ? ' and ' + scope.getFilterValue() : scope.getFilterValue());
                    var getDataTbl = myService.GetTableData(scope.tableParamData, scope.tableUrl);
                    getDataTbl.then(function (emp) {
                        scope.tblDatas = emp.data.employees;
                        scope.totalCount = emp.data.totalCount;
                        scope.lstTotal = emp.data.lstTotal;
                        scope.SetTotalByColumns = function (totalName, dataFomat) {
                            if (!angular.isUndefined(totalName) && totalName !== null && scope.lstTotal != undefined) {
                                if (scope.lstTotal[totalName] != undefined && scope.lstTotal[totalName] != null) {

                                    return parseFloat(scope.lstTotal[totalName]).format(dataFomat, 3, ',', '.');
                                }
                                return scope.lstTotal[totalName];
                            }
                            return "";
                        };
                        if ($rootScope.GetColumnWhereCondition != 0 && $rootScope.GetColumnWhereCondition != undefined) {
                            if (data.res.filter8 == 1) {
                                $timeout(function () {
                                    if (typeof $rootScope.getColumnMonth === "function") {
                                        $rootScope.getColumnMonth();
                                    }
                                    if (typeof $rootScope.showColumn === "function") {
                                        $rootScope.showColumn();
                                    }
                                }, 700);
                            }
                            else {
                                $timeout(function () {
                                    if (typeof $rootScope.getColumnQuater === "function") {
                                        $rootScope.getColumnQuater();
                                    }
                                    if (typeof $rootScope.showColumn === "function") {
                                        $rootScope.showColumn();
                                    }
                                }, 700);
                            }
                            setTimeout(function () {
                                if (typeof $rootScope.ShowAllOrDetail === "function") {
                                    $rootScope.ShowAllOrDetail();
                                }
                            }, 200);
                            dt.finish();
                        }

                    }, function (ex) {
                        AppendToToastr(false, scope.notification, errorNotiFalse);
                    });

                };

                //---end get table

                //------ xử lý filter
                scope.addFilterColumns = function () {
                    var data = angular.copy(scope.filterColumnsItem)
                    scope.filterColumnsChoosed.push(data);
                }
                scope.removeColumnFilterByIndex = function (index) {
                    scope.filterColumnsChoosed.splice(index, 1);
                }

                scope.getFilterColumns = function (tblInfo) {
                    if (!tblInfo) return;
                    var filter = myService.getFilterColumns(tblInfo.id);
                    filter.then(function (res) {
                        HiddenLoader();
                        //scope.FilterColumnsItem = angular.copy(res.data.result);
                        scope.listFilterColumns = angular.copy(res.data.result);
                        scope.GetSelectBox(scope.listFilterColumns);

                        scope.filterColumnsItem = {
                            filterColumns: angular.copy(res.data.result),
                            typeFilter: [],
                            typeEnds: scope.typeEnds,
                            textValue: "",
                            typeEndsSeleted: scope.typeEnds[0],
                            hasLink: false,
                            SelectBox: scope.listFilterColumns.SelectBox
                        }

                        scope.addFilterColumns();
                    }, function (res) {
                        AppendToToastr(false, scope.notification, errorNotiFalse);
                    });
                }
                scope.GetSelectBox = function (data) {
                    angular.forEach(data, function (item) {
                        if (item.Type == 4) {
                            item = scope.GetColumnDataById(item);

                        }
                    })
                }
                scope.GetColumnDataById = function (item) {
                    var getDataTbl = myService.GetColumnDataById(item.Id);
                    getDataTbl.then(function (emp) {
                        item['SelectBox'] = emp.data.result;
                    }, function (ex) {
                        AppendToToastr(false, scope.notification, errorNotiFalse);
                    });
                }

                scope.cancelFilterClick = function () {
                    scope.filterColumnsChoosed = [];                  
                    scope.GetListData();

                }

                scope.showFilterApplyButton = function () {
                    var lstObj = scope.filterColumnsChoosed;
                    if (lstObj.length === 0) {
                        return false;
                    }
                    for (var key in lstObj) {
                        var obj = lstObj[key];
                        if (obj.textValue === "") {
                            return false;
                        }
                    }
                    return true;
                }
                //toggle filter
                scope.toggleChange = function () {
                    if (scope.isShowFilter) {
                        if (scope.toggle) scope.toggle = false;
                        else scope.toggle = true;
                        //  return true;
                    }
                    // return false;
                }
                scope.getFilterValue = function () {
                
                    var lstObj = scope.filterColumnsChoosed;
                    lstObj = scope.getLinkGroup(lstObj);
                    var stringFilter = "";
                    var len = lstObj.length - 1;
                    for (var key in lstObj) {
                        var obj = lstObj[key];

                        var valueFilter = '';
                        if (obj.typeFilterSelected) {
                            switch (obj.filterSelected.Type) {
                                case 3: valueFilter = "'" + $filter('date')(new Date(obj.textValue), 'yyyy/MM/dd') + "'"; break;
                                case 4: valueFilter = obj.textValue.Value; break;
                                default: valueFilter = obj.textValue; break;
                            }
                        }

                        var textValue = (obj.typeFilterSelected ? obj.typeFilterSelected.Descriptions.replace("#value", valueFilter) : '');

                        var tmpFilter =
                            (obj.positionLink == true ? obj.typeLinkValue : "")
                            + (obj.filterSelected ? obj.filterSelected.ColumnName : "") + " "
                            + textValue
                            + (obj.positionLink == false ? obj.typeLinkValue : "")
                            + (parseInt(String(key)) === len ? "" : obj.typeEndsSeleted.value);
                        stringFilter += tmpFilter;
                    }
                    if (scope.customFilter) {
                        if (stringFilter && stringFilter != " ")
                            stringFilter += " and ";
                        stringFilter += scope.customFilter;
                    }
                     
                    return stringFilter;
                };
                scope.getLinkGroup = function (lstObj) {
                    var length = lstObj.length;
                    if (length > 1) {
                        lstObj[0].typeLinkValue = lstObj[0].hasLink == true ? '(' : '';
                        lstObj[0].positionLink = true;

                        lstObj[length - 1].typeLinkValue = lstObj[length - 2].hasLink == true ? ')' : '';
                        lstObj[length - 1].positionLink = false;
                        var pre, now;

                        if (length > 2) {
                            for (var i = 1; i < lstObj.length - 1; i++) {
                                pre = lstObj[i - 1].hasLink;
                                now = lstObj[i].hasLink;
                                if (pre && !now) {
                                    lstObj[i].typeLinkValue = ')';
                                    lstObj[i].positionLink = false;
                                }
                                else if (!pre && now) {
                                    lstObj[i].typeLinkValue = '(';
                                    lstObj[i].positionLink = true;
                                }
                                else
                                    lstObj[i].typeLinkValue = '';
                            }
                        }
                    }
                    return lstObj;
                }

                //getlisttypfilter
                scope.getListTypeFilter = function () {
                    var getData = myService.getDataByGloballistNotTree(6);
                    getData.then(function (emp) {
                        scope.typeFilter = emp.data.result;
                        SplitTypeFilter(scope.typeFilter);
                    }, function (emp) {
                        AppendToToastr(false, scope.notification, errorNotiFalse);
                    });
                }
                function SplitTypeFilter(source) {
                    angular.forEach(source, function (item) {
                        switch (item.Value) {
                            case '1': scope.typeFilterA.push(item); break;
                            case '2': scope.typeFilterB.push(item); break;
                            case '3': scope.typeFilterC.push(item); break;
                            case '4': scope.typeFilterD.push(item); break;
                            default: break;
                        }
                    })
                }
                scope.filterColumnsChange = function (filterSelected, filterItemChoosed, index) {
                    var type = filterSelected.Type;
                    switch (type) {
                        case 1: scope.filterColumnsChoosed[index].typeFilter = scope.typeFilterA; break;
                        case 2: scope.filterColumnsChoosed[index].typeFilter = scope.typeFilterB; break;
                        case 3: scope.filterColumnsChoosed[index].typeFilter = scope.typeFilterC; break;
                        case 4: scope.filterColumnsChoosed[index].typeFilter = scope.typeFilterD;
                            filterSelected = scope.GetColumnDataById(filterSelected);
                            break;
                        default: break;
                    }
                };
                //----------- quick filter
                scope.bindQuickFilterToFilter = function () {
                    var lisObj = scope.listFilterColumns;
                    var rs = []
                    for (var i = 0; i < lisObj.length; i++) {
                        if (scope.quickFilter[lisObj[i].ColumnName]) {
                            var item = {
                                filterColumns: lisObj,
                                typeFilter: scope.typeFilter,
                                typeEnds: scope.typeEnds,
                                textValue: scope.quickFilter[lisObj[i].ColumnName],
                                typeFilterSelected: {},
                                typeEndsSeleted: scope.typeEnds[0],
                                filterSelected: lisObj[i],
                                hasLink: false,
                                positionLink: false,
                                typeLinkValue: '',
                            }
                            rs.push(item)
                        }
                    }

                    for (var i = 0; i < rs.length; i++) {
                        rs[i].typeFilter = scope.getFilter(rs[i].filterSelected);
                        rs[i].typeFilterSelected = rs[i].typeFilter[0];
                    }
                    scope.filterColumnsChoosed = rs;
                    scope.isShowFilter = true;
                    scope.GetListData();
                }
                scope.bindQuickFilterToFilterButNotLoadData = function () {
                    var lisObj = scope.listFilterColumns;
                    var rs = []
                    for (var i = 0; i < lisObj.length; i++) {
                        if (scope.quickFilter[lisObj[i].ColumnName]) {
                            var item = {
                                filterColumns: lisObj,
                                typeFilter: scope.typeFilter,
                                typeEnds: scope.typeEnds,
                                textValue: scope.quickFilter[lisObj[i].ColumnName],
                                typeFilterSelected: {},
                                typeEndsSeleted: scope.typeEnds[0],
                                filterSelected: lisObj[i],
                                hasLink: false,
                                positionLink: false,
                                typeLinkValue: '',
                            }
                            rs.push(item)
                        }
                    }

                    for (var i = 0; i < rs.length; i++) {
                        rs[i].typeFilter = scope.getFilter(rs[i].filterSelected);
                        rs[i].typeFilterSelected = rs[i].typeFilter[0];
                    }
                    scope.filterColumnsChoosed = rs;
                    scope.isShowFilter = true;
                };

                scope.getFilter = function (filterSelected) {
                    var type = filterSelected.Type;
                    switch (type) {
                        case 1: return scope.typeFilterA;
                        case 2: return scope.typeFilterB;
                        case 3: return scope.typeFilterC;
                        case 4: return scope.typeFilterD;
                        default: break;
                    }
                };
                //------end xử lý filter

              

            
                //This method is calling from pagination number
                scope.PageChanged = function () {
                    if (typeof $rootScope.childmethod === "function") {
                        $rootScope.childmethod();
                    } else {
                        scope.GetListData();
                    }
                   
                };

                //This method is calling from dropDown
                scope.ChangePageSize = function () {
                    scope.$parent.filterColumnsChoosed = angular.copy(scope.filterColumnsChoosed);
                    scope.pageIndex = 1;
                    if (typeof $rootScope.childmethod === "function") {
                        $rootScope.childmethod();
                    } else {
                        scope.GetListData();
                    }
                };



                scope.formatData = function (type, value) {
                    if (type === 3) {
                        return FormatDate(value);
                    }
                    return value;
                }
                scope.setShowTypeEnd = function (index) {
                    if (scope.filterColumnsChoosed.length - 1 === index) {
                        return false;
                    };
                    return true;
                }

                //function BindWidthToCss(model) {
                //    var styleWidth = '';
                //    angular.forEach(model, function (item) {
                //        styleWidth = (item.Width) ? ('width : ' + item.Width + 'px') : '100%';
                //        item.Css = (item.Css) ? (item.Css + ';' + styleWidth) : styleWidth;
                //    })
                //    return model;
                //}
                function FormatDate(inputDate) {
                    var date = new Date(inputDate);
                    if (!isNaN(date.getTime())) {
                        var day = date.getDate().toString();
                        var month = (date.getMonth() + 1).toString();
                        // Months use 0 index.
                        return (day[1] ? day : '0' + day[0]) +
                            '/' +
                            (month[1] ? month : '0' + month[0]) +
                            '/' +
                            date.getFullYear();
                    }
                };

                function convertDateTimeDefaultToString(datetime) {
                    var months = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
                    var newMonth = months[datetime.getMonth()];
                    var x;
                    switch (newMonth) {
                        case "January":
                            x = '01';
                            break;
                        case "February":
                            x = '02';
                            break;
                        case "March":
                            x = '03';
                            break;
                        case "April":
                            x = '04';
                            break;
                        case "May":
                            x = '05';
                            break;
                        case "June":
                            x = '06';
                            break;
                        case "July":
                            x = '07';
                            break;
                        case "August":
                            x = '08';
                            break;
                        case "September":
                            x = '09';
                            break;
                        case "October":
                            x = '10';
                            break;
                        case "November":
                            x = '11';
                            break;
                        case "December":
                            x = '12';
                            break;
                        default:
                        // code block
                    }
                    var getDate = datetime.getDate();
                    var getMonth = x;
                    var getFullYear = datetime.getFullYear();
                    var fullDateTime = ("0" + getDate).slice(-2) + "-" + getMonth + "-" + getFullYear + " " + ("0" + datetime.getHours()).slice(-2) + ":" + ("0" + datetime.getMinutes()).slice(-2) + ":" + ("0" + datetime.getSeconds()).slice(-2);
                    return fullDateTime;
                }

                //-------------------Excel--------------
                scope.ExcelClick = function () {
                    //scope.bindQuickFilterToFilter(scope.tblColumns);
                    //scope.bindQuickFilterToFilterButNotLoadData(scope.tblColumns); 
                    var StringFromDate = convertDateTimeDefaultToString(new Date(scope.FromDate.setHours(0,0,0)));
                    var StringToDate = convertDateTimeDefaultToString(new Date(scope.ToDate.setHours(23, 59, 59)));
                    var StringFromDate1 = convertDateTimeDefaultToString(new Date(scope.FromDate.setHours(12, 0, 0)));
                    var StringToDate1 = convertDateTimeDefaultToString(new Date(scope.ToDate.setHours(12, 0, 0)));
                    var filterGet = scope.getFilterValue();
                    var filter = filterGet.replace(/%/g, "!!");
                    window.location =
                        scope.tblInfo.ExcelUrl +
                        "?pageIndex=" +
                        scope.pageIndex +
                        "&pageSize=" +
                        scope.pageSizeSelected +
                        "&filter1=" +
                        $rootScope.data.filter1 +
                        "&filter2=" +
                        $rootScope.data.filter2 +
                        "&filter3=" +
                        $rootScope.data.filter3 +
                        "&filter4=" +
                        $rootScope.data.filter4 +
                        "&filter5=" +
                        $rootScope.data.filter5 +
                        "&filter6=" +
                        $rootScope.data.filter6 +
                        "&filter7=" +
                        $rootScope.data.filter7 +
                        "&filter8=" +
                        $rootScope.data.filter8 +
                        "&filter9=" +
                        $rootScope.data.filter9 +
                        "&filter10=" +
                        $rootScope.data.filter10 +
                        "&filter11=" +
                        $rootScope.data.filter11 +
                        "&filter12=" +
                        $rootScope.data.filter12 +
                        "&filter13=" +
                        $rootScope.data.filter13 +
                        "&StringFromDate=" +
                        StringFromDate +
                        "&StringToDate=" + StringToDate +
                        "&startdate=" + StringFromDate1 +
                        "&enddate=" + StringToDate1 +
                        "&startdate2=" + StringFromDate1 +
                        "&enddate2=" + StringToDate1 +
                        "&OrganizationUnitID=" + scope.OrganizationUnitID +
                        "&filter=" +
                        filter;
                }

                scope.Addlinkfilter = function (index, flag) {
                    scope.filterColumnsChoosed[index].hasLink = flag;
                }

                scope.showLink = function (index) {
                    return scope.filterColumnsChoosed[index].hasLink;
                }

                scope.SetTotalByColumns = function (totalName) {
                    if (!angular.isUndefined(totalName) && totalName !== null && scope.lstTotal != undefined) {
                        if (scope.lstTotal[totalName] != undefined && scope.lstTotal[totalName] != null) {
                            return parseFloat(scope.lstTotal[totalName]).format(scope.lstTotal[totalName].DataFomat, 3, ',', '.');
                        }
                        return scope.lstTotal[totalName];
                    }
                    return "";
                };
                //---- end xử lý các phần exten của table


                //exten function or controller
                scope.tableModel.reloadByFilter = function (strFilter) {
                    scope.customFilter = strFilter;
                    scope.GetListData();
                }
                scope.tableModel.reload = function () {
                    scope.GetListData();
                }
                //----end extenn--

                scope.changeTypeOne = function () {
                    $rootScope.quickFiltercomback = angular.copy(scope.quickFilter);
                }
                scope.changeTypeTwo = function () {
                    $rootScope.quickFiltercomback = angular.copy(scope.quickFilter);
                } 
                scope.changeTypeThree = function () {
                    $rootScope.quickFiltercomback = angular.copy(scope.quickFilter);
                }
                scope.changeTypeFour = function () {
                    $rootScope.quickFiltercomback = angular.copy(scope.quickFilter);
                }
                scope.init = function () {
                    scope.getTableInfo($rootScope.DoNotLoad);
                    scope.getListTypeFilter();

                }

                scope.init();

                $('.fixedTable').scroll(function (e) {
                    var prevLeft = 0;
                    var currentLeft = $(this).scrollLeft();
                    if (prevLeft != currentLeft) {
                        prevLeft = currentLeft;
                        $('.divTable .divTableHeading .divTableCell.fix-column').css("z-index", "1000");
                        $('.divTable .divTableRow .divTableCell.fix-column').css("z-index", "0");

                    } else {
                        $('.divTable .divTableHeading .divTableCell.fix-column').css("z-index", "1000");
                        $('.divTable .divTableRow .divTableCell.fix-column').css("z-index", "0");
                    }
                    $('.fixedTable').css("left", -$(".fixedTable").scrollLeft() - 1);
                    $('.fixedTable .divTableCell.fix-column').css("left", $(".fixedTable").scrollLeft() - 1);
                    $('.fixedTable').css("top", -$(".fixedTable").scrollTop() - 1);
                    $('.divTableHeading .divTableCell').css("top", $(".fixedTable").scrollTop() - 1);
                });

                //------------------Định dạng cột của table----------
                scope.ColseSelectAllColumn = true;
                scope.SelectAllColumn = false;

                //Funtion  lấy tất cả cột được cấp quyền trong bảng sys_table_Column_role
                scope.getAllColumn=function(tableUrl){
                    var getData = myService.GetAllColumns(tableUrl);
                    getData.then(function (emp) {
                        scope.listColumnFormat = angular.copy(emp.data.result)
                        scope.listColumnSortable = angular.copy(emp.data.result)
                        if ((scope.listColumnSortable.filter(function (item) { return item.isActive == true}).length)==0) {
                            scope.ColseSelectAllColumn = false;
                            scope.SelectAllColumn = true;
                        }
                    }, function (emp) {
                        AppendToToastr(false, scope.notification, errorNotiFalse);
                    });
                }

                scope.getAllColumn(scope.tableUrl);

                //Click nút định dạng cột
                scope.columnFormat = function (tblInfo) {                 
                    scope.TotalColumnSelect = scope.listColumnSortable.filter(function (item) { return item.isActive == true}).length
                    for (var i = 0; i < scope.listColumnFormat.length; i++) {
                        if (scope.listColumnFormat[i].Active == false) {
                            scope.SelectAllColumn = true;
                            scope.ColseSelectAllColumn = false;
                            return;
                        }
                    }
                    ShowPopup($,
                  "#ColumnFormat",
                  1100,
                  650);
                    $('#sortable').sortable();
                }

                //Đóng model định dạng cột
                scope.CloseColumnFormat = function () {
                    $.colorbox.close();
                    scope.getAllColumn(scope.tableUrl);

                }

                //Xóa column
                scope.removeColumn = function (column) {
                    for (var i = 0; i < scope.listColumnFormat.length; i++) {
                        if (scope.listColumnFormat[i].Id == column.Id) {
                            scope.listColumnFormat[i].isActive = false;
                            break;
                        }
                    }
                    for (var i = 0; i < scope.listColumnSortable.length; i++) {
                        if (scope.listColumnSortable[i].Id == column.Id) {
                            scope.listColumnSortable[i].isActive = false;
                            break;
                        }
                    }
                    scope.TotalColumnSelect = scope.listColumnSortable.filter(function (item) { return item.isActive == true}).length
                }

                //click ô checkbox
                scope.SelectColumnEx = function (column) {
                    if (column.isActive == true) {
                        for (var i = 0; i < scope.listColumnSortable.length; i++) {
                            if (scope.listColumnSortable[i].Id == column.Id) {
                                scope.listColumnSortable[i].isActive = false;
                                break;
                            }
                        }
                        for (var i = 0; i < scope.listColumnFormat.length; i++) {
                            if (scope.listColumnFormat[i].Id == column.Id) {
                                scope.listColumnFormat[i].isActive = false;
                                break;
                            }
                        }
                    }
                    else {
                        scope.Order = scope.listColumnSortable[scope.listColumnSortable.length - 1].Order;
                        scope.max = Math.max.apply(Math, scope.listColumnSortable.map(function (item) { return item.Order; }));
                        for (var i = 0; i < scope.listColumnSortable.length; i++) {
                            if (scope.listColumnSortable[i].Id == column.Id) {
                                scope.listColumnSortable[i].isActive = true;
                                scope.listColumnSortable[i].Order = scope.max +1;
                                break;
                            }
                        }
                        for (var i = 0; i < scope.listColumnFormat.length; i++) {
                            if (scope.listColumnFormat[i].Id == column.Id) {
                                scope.listColumnFormat[i].isActive = true;
                                break;
                            }
                        }
                    }

                    scope.TotalColumnSelect = scope.listColumnSortable.filter(function (item) { return item.isActive == true}).length
                }

                //Xóa tất cả
                scope.clickColseSelectAllColumns = function () {
                    for (var i = 0; i < scope.listColumnSortable.length; i++) {
                        scope.listColumnSortable[i].isActive = false;
                    }
                    for (var i = 0; i < scope.listColumnFormat.length; i++) {
                        scope.listColumnFormat[i].isActive = false;
                    }
                    scope.ColseSelectAllColumn = false;
                    scope.SelectAllColumn = true;
                    scope.TotalColumnSelect = scope.listColumnSortable.filter(function (item) { return item.isActive == true}).length
                }
                
                //Chọn tất cả
                scope.clickSelectAllColumns = function () {
                    for (var i = 0; i < scope.listColumnFormat.length; i++) {
                        scope.listColumnFormat[i].isActive = true;
                    }
                    scope.listColumnSortable = angular.copy(scope.listColumnFormat);
                    scope.ColseSelectAllColumn = true;
                    scope.SelectAllColumn = false;
                    scope.TotalColumnSelect = scope.listColumnSortable.filter(function (item) { return item.isActive == true }).length;
                    $timeout(function () {
                        $('#sortable').sortable();
                    },100)
                }

                //Bỏ tìm kiếm
                scope.deleteFind = function () {
                    if (scope.txtSearchColumn != null && scope.txtSearchColumn != "") {
                        scope.txtSearchColumn = '';
                    }
                }

                scope.SaveCustomerColumns = function () {
                   scope.DataColumn = [];
                    for (var i = 0; i < scope.listColumnSortable.length; i++) {
                        var column = '#column-' + scope.listColumnSortable[i].ColumnName
                        var obj = { Id: scope.listColumnSortable[i].Id, isActive: scope.listColumnSortable[i].isActive, OrderNo: $(column).index(), ColumnName: scope.listColumnSortable[i].ColumnName, TableId: scope.listColumnSortable[i].TableId }
                        scope.DataColumn.push(obj);
                    }
                    myService.UpdateColumn(scope.DataColumn).then(function (res) {
                        if (res.data.result.IsSuccess == true) {
                            AppendToToastr(true, notification, res.data.result.Message, 500, 5000);
                            scope.getColumns();
                            scope.GetListData(scope.tableUrl);
                            scope.getAllColumn(scope.tableUrl);
                            $.colorbox.close();
                        }
                        else {
                            AppendToToastr(false, notification, res.data.result.Message, 500, 5000);
                        }
                    }, function (res) {
                        AppendToToastr(false, notification, errorNotiFalse);
                    });
                }

                //----------------- kết thúc định dạng cột------------------
            }
        };
    }]);

    appModule.directive('buildTable1', ['myService', '$filter', '$rootScope', '$timeout', '$window', function (myService, $filter, $rootScope, $timeout, $window) {
        return {
            restrict: 'E',
            scope: {
                tableModel: '=?',
                tableUrl: '=',
                tableAdd: '=?',
                tableEdit: '=?',
                tableCopy: '=?',
                tableDelete: '=?',
                tableHide: '=?',
                tableExcelClick: '=?',
                tableDownLoadFile: '=?',
                tableParamFilter: '=?',
                tableChangePageSize: '=?',
                tablePageChanged: '=?',
                tableDinhdang: '=?',
                changeListEmployees: '=?',
                changeOrganizationUnit: '=?',
                changeStatus: '=?',
                changeYear: '=?',
                changeM1: '=?',
                changeM2: '=?',
                changeM3: '=?',
                changeM4: '=?',
                changeM5: '=?',
                changeM6: '=?',
                changeM7: '=?',
                changeM8: '=?',
                changeM9: '=?',
                changeM10: '=?',
                changeM11: '=?',
                changeM12: '=?',
                saveEditClick: '=?',
                changeEditTableColumn: '=?',
                saveMerge: '=?',
                notification: '=',
                validateDatetime: '=',
                options: '&?'
            },

            templateUrl: '/Common/BuildTable1',
            replace: true,
            link: function (scope, element, attr) {
                //param integer n: length of decimal
                //param integer x: length of whole part
                //param mixed   s: sections delimiter
                //param mixed   c: decimal delimiter

                //-----------------Lấy danh sách phòng ban------------------
                scope.ListOrganizationUnit = function () {
                    var data = {
                        url: "/OrganizationUnitPlan/OrganizationUnit_GetALL"
                    }
                    var list = myService.getData(data);
                    list.then(function (res) {
                        scope.getListAllOrganizationUnit = angular.copy(res.data.result);
                        $rootScope.getListAllOrganizationUnit = angular.copy(res.data.result);
                    }, function (res) {
                        scope.msg = "Error";
                    })
                }
                //-----------------Lấy danh sách nhân viên------------------
                scope.ListEmployees = function () {
                    var data = {
                        url: "/StaffPlan/Staff_GetALL"
                    }
                    var list = myService.getData(data);
                    list.then(function (res) {
                        scope.ListEmployees = res.data.result;
                        $rootScope.ListEmployees = angular.copy(res.data.result);
                    }, function (res) {
                        scope.msg = "Error";
                    })
                }
                //-----------------Lấy danh sách tiền tệ------------------
                scope.ListCurrencyName = function () {
                    var data = {
                        url: "/Common/GetDataByGloballistnotTree?parentid=" + 3
                    }
                    var list = myService.getDropdown(data);
                    list.then(function (res) {
                        scope.ListCurrencyName = res.data.result;
                        $rootScope.ListCurrencyName = angular.copy(res.data.result);
                    },
                        function (res) {
                            scope.msg = "Error";
                        });
                }

                //------------Dropdown trạng thái-------------

                //-- Neu value la 0 tu dong format thanh string zero de tranh truong hop value = 0 khi select no nhan la null
                scope.GetListStatus = function () {
                    var data = {
                        url: "/Common/GetDataByGloballistnotTree?parentid=" + 88
                    }
                    var list = myService.getDropdown(data);
                    list.then(function (res) {
                        scope.getListStatus = res.data.result;;
                        scope.listStatusFormatValueToZero = res.data.result;
                        // convert displayValue string to int
                        for (var i = 0 ; i < scope.listStatusFormatValueToZero.length ; i++) {
                            scope.listStatusFormatValueToZero[i].Value = parseInt(scope.listStatusFormatValueToZero[i].Value) == 0 ? 'zero' : scope.listStatusFormatValueToZero[i].Value;
                        }
                        $rootScope.listStatusFormartValueToZero = angular.copy(scope.listStatusFormatValueToZero);
                        $rootScope.getListStatus = angular.copy(scope.getListStatus);
                    },
                       function (res) {
                           scope.msg = "Error";
                       });
                }

                ////------------Dropdown hợp đồng-------------
                scope.ListContractTypeName = function () {
                    var data = {
                        url: "/Common/GetDataByGloballistnotTree?parentid=" + 1949
                    }
                    var list = myService.getDropdown(data);
                    list.then(function (res) {
                        scope.ListContractTypeName = res.data.result;

                    },
                        function (res) {
                            $scope.msg = "Error";
                        });
                }
                //------------------------------------------------------------------------------------------
                Number.prototype.format = function (n, x, s, c) {
                    var re = '\\d(?=(\\d{' + (x || 3) + '})+' + (n > 0 ? '\\D' : '$') + ')',
                        num = this.toFixed(Math.max(0, ~~n));

                    return (c ? num.replace('.', c) : num).replace(new RegExp(re, 'g'), '$&' + (s || ','));
                };

                //12345678.9.format(2, 3, '.', ',');  // "12.345.678,90"
                //123456.789.format(4, 4, ' ', ':');  // "12 3456:7890"
                //12345678.9.format(0, 3, '-');       // "12-345-679"

                scope.currentLanguage = currentLanguage;
                scope.filterColumnsChoosed = [];
                scope.customFilter = ""; // filter add from controller
                scope.maxSize = 5; // Limit number for pagination display number.
                scope.totalCount = 0; // Total number of items in all pages. initialize as a zero
                scope.pageIndex = 1; // Current page number. First page is 1.-->
                scope.pageSizeSelected = 10;
                scope.typeEnds = [{ name: "Và", value: " and " }, { name: "Hoặc", value: " or " }];
                scope.tblInfo = {};
                scope.quickFilter = {
                    Year: scope.$parent.Year
                }; // quick filter data.

                scope.listFilterColumns = []; // list columns bind quick search
                scope.typeFilterA = []; // filter have type value = 1
                scope.typeFilterB = []; // filter have type value = 2
                scope.typeFilterC = []; // filter have type value = 3
                scope.typeFilterD = []; // filter have type value = 4
                scope.filterColumnsItem = {}; // data filter template
                scope.typeFilter = [];
                scope.tableModel = {}; //model public function 
                scope.totalCount = 0;
                scope.StaffStatus = scope.$parent.StaffStatus;
                scope.FromDate = scope.$parent.FromDate != undefined ? scope.$parent.FromDate : Date.now();
                scope.ToDate = scope.$parent.ToDate != undefined ? scope.$parent.ToDate : Date.now();
                scope.FromDateToDate = scope.$parent.FromDateToDate;
                scope.FromDateToDate1 = scope.$parent.FromDateToDate1;
                scope.FormatColumn = scope.$parent.FormatColumn;
                scope.Width = scope.$parent.Width;
                scope.Height = scope.$parent.Height;
                scope.IshowTotal = scope.$parent.IshowTotal;
                if ($rootScope.isDownLoad!=null && $rootScope.isDownLoad==true ) {
                    scope.isDownLoad = $rootScope.isDownLoad;
                }
               


                //----- table-------
                scope.getTableInfo = function (DoNotLoad) {
                    var getData = myService.getTableInformation(scope.tableUrl);
                    getData.then(function (emp) {
                        if (emp && emp.data && emp.data.result) {
                            scope.tblInfo = emp.data.result;
                            scope.lstPageSize = scope.tblInfo.PageSizeList.split(',');
                            scope.pageSizeSelected = scope.tblInfo.PageSize;
                            scope.GetAddPermission(emp.data.result.id, DoNotLoad);
                            scope.getFilterColumns(scope.tblInfo);                          
                        }

                    }, function (emp) {
                        AppendToToastr(false, scope.notification, errorNotiFalse);
                    });
                }

                //get permission action button
                scope.GetAddPermission = function (idTable, DoNotLoad) {
                    var tblAction = myService.getTableAction(idTable);
                    tblAction.then(function (emp) {
                        scope.tablePermission = emp.data.result;
                        scope.getColumns(DoNotLoad);
                        // $scope.BuildAddButton(emp.data.result);
                    }, function (emp) {
                        AppendToToastr(false, scope.notification, errorNotiFalse);
                    });
                }

                //get columns table
                scope.getColumns = function (DoNotLoad) {
                    var getData = myService.GetColumns(scope.tableUrl);
                    getData.then(function (emp) {
                        scope.tblColumns = emp.data.result;
                        if (DoNotLoad == undefined) {
                            scope.GetListData();
                           
                        }
                        //scope.Data();


                    }, function (emp) {
                        AppendToToastr(false, scope.notification, errorNotiFalse);
                    });
                }

             
             

                scope.GetListData = function () {
                    if (scope.FromDate == null || scope.ToDate == null) {
                        AppendToToastr(false, scope.notification, scope.validateDatetime);
                    } else {
                        var dt = Loading();
                        //scope.bindQuickFilterToFilter(scope.tblColumns);
                        scope.tableParamData = scope.tableParamFilter != null ? scope.tableParamFilter : {};
                        scope.tableParamData['pageIndex'] = scope.pageIndex;
                        scope.tableParamData['pageSize'] = scope.pageSizeSelected;
                        scope.tableParamData['viewType'] = $rootScope.viewType;
                        scope.tableParamData['FromDate'] = new Date(scope.FromDate.setHours(0, 0, 0));
                        scope.tableParamData['ToDate'] = new Date(scope.ToDate.setHours(23, 59, 59));
                        scope.tableParamData['filter1'] = $rootScope.data.filter1;
                        scope.tableParamData['filter2'] = $rootScope.data.filter2;
                        scope.tableParamData['filter3'] = $rootScope.data.filter3;
                        scope.tableParamData['filter4'] = $rootScope.data.filter4;
                        scope.tableParamData['filter5'] = $rootScope.data.filter5;
                        scope.tableParamData['filter6'] = $rootScope.data.filter6;
                        scope.tableParamData['filter7'] = $rootScope.data.filter7;
                        //scope.tableParamData['filter'] = scope.getFilterValue();
                        
                        if (scope.quickFilter!=null) {
                            if (scope.quickFilter.WorkingDayMachineName != null) {
                                scope.tableParamData['filter7'] = scope.quickFilter.WorkingDayMachineName.Value;
                            }
                            if (scope.quickFilter.Fullname != null) {
                                scope.tableParamData['filter6'] = scope.quickFilter.Fullname.Value;
                            }
                        }
                        var filter = scope.getFilterValue();
                        if (filter == " ") {
                            scope.tableParamData['filter'] = $rootScope.data.filter;
                        }
                        else if (filter == "") {
                            if (scope.quickFilter.Year != null || scope.quickFilter.Year != "") {
                                scope.tableParamData['filter'] = $rootScope.data.filter;
                            }
                            else {
                                scope.tableParamData['filter'] = "";
                            }
                        }
                        else {
                            scope.tableParamData['filter'] = filter;
                            var postiton = filter.indexOf("Year");
                            if (postiton == -1) {
                                if (scope.quickFilter.Year != null || scope.quickFilter.Year != "") {
                                    scope.tableParamData['filter'] += " and Year LIKE N'%" + scope.quickFilter.Year + "%'";
                                }
                            }
                        }
                        var getDataTbl = myService.GetTableData(scope.tableParamData, scope.tableUrl);
                        getDataTbl.then(function (emp) {
                            scope.tblDatas = emp.data.employees;
                            if (scope.tblDatas != null && scope.tblDatas.length>0) {
                                for (var i = 0; i < scope.tblDatas.length; i++) {
                                    if (scope.tblDatas[i].CHECKTIME != null) {
                                        scope.tblDatas[i].CHECKTIME = scope.tblDatas[i].CHECKTIME.replace("T", " ");
                                    }
                                }
                            }
                           
                            scope.totalCount = emp.data.totalCount;
                            scope.lstTotal = emp.data.lstTotal;
                            if (scope.tblDatas !=null) {
                                for (var i = 0; i < scope.tblDatas.length; i++) {
                                    scope.tblDatas[i].Show = true;
                                }
                            }
                            
                            scope.ToTalMonth = emp.data.ToTalMonth;
                            scope.TotalQuarter = emp.data.TotalQuarter;
                            if (scope.TotalQuarter) {
                                scope.isShowTotalByQuarter = true;
                            }
                            scope.SetTotalByColumns = function (totalName, dataFomat) {
                                if (!angular.isUndefined(totalName) &&
                                    totalName !== null &&
                                    scope.lstTotal != undefined) {
                                    if (scope.lstTotal[totalName] != undefined &&
                                        scope.lstTotal[totalName] != null) {
                                        return parseFloat(scope.lstTotal[totalName]).format(dataFomat, 3, ',', '.');
                                    }
                                    return scope.lstTotal[totalName];
                                }
                                return "";
                            };

                            if ($rootScope.GetColumnWhereCondition != 0 &&
                                $rootScope.GetColumnWhereCondition != undefined) {
                                setTimeout(function () {
                                    if (typeof $rootScope.ShowAllOrDetail === "function") {
                                        $rootScope.ShowAllOrDetail();
                                    }
                                },
                                    200);
                            }
                            dt.finish();
                        },
                            function (ex) {
                                AppendToToastr(false, scope.notification, errorNotiFalse);
                                dt.finish();

                            });
                    }
                }

                $rootScope.$on("CallParentMethod", function (event, data) {
                    var dt = Loading();
                    scope.bindQuickFilterToFilter(scope.tblColumns); 
                    scope.tableParamData = scope.tableParamFilter != null ? scope.tableParamFilter : {};
                    scope.tableParamData['pageIndex'] = scope.pageIndex;
                    scope.tableParamData['pageSize'] = scope.pageSizeSelected;
                    scope.tableParamData['viewType'] = $rootScope.viewType;
                    scope.tableParamData['FromDate'] = new Date(scope.FromDate.setHours(0, 0, 0));
                    scope.tableParamData['ToDate'] = new Date(scope.ToDate.setHours(23, 59, 59));
                    scope.tableParamData['filter1'] = data.res.filter1;
                    scope.tableParamData['filter2'] = data.res.filter2;
                    scope.tableParamData['filter3'] = data.res.filter3;
                    scope.tableParamData['filter4'] = data.res.filter4;
                    scope.tableParamData['filter5'] = data.res.filter5;
                    scope.tableParamData['filter6'] = data.res.filter6;
                    scope.tableParamData['filter7'] = data.res.filter7;
                    scope.tableParamData['filter'] = data.filter + (scope.getFilterValue().trim() != '' && data.filter ? ' and ' + scope.getFilterValue() : scope.getFilterValue());
                    var getDataTbl = myService.GetTableData(scope.tableParamData, scope.tableUrl);
                    getDataTbl.then(function (emp) {
                        scope.tblDatas = emp.data.employees;
                        scope.totalCount = emp.data.totalCount;
                        scope.lstTotal = emp.data.lstTotal;
                        scope.SetTotalByColumns = function (totalName, dataFomat) {
                            if (!angular.isUndefined(totalName) && totalName !== null && scope.lstTotal != undefined) {
                                if (scope.lstTotal[totalName] != undefined && scope.lstTotal[totalName] != null) {
                                    return parseFloat(scope.lstTotal[totalName]).format(dataFomat, 3, ',', '.');
                                }
                                return scope.lstTotal[totalName];
                            }
                            return "";
                        };

                        if ($rootScope.GetColumnWhereCondition != 0 && $rootScope.GetColumnWhereCondition != undefined) {
                            if (data.res.filter8 == 1) {
                                $timeout(function () {
                                    if (typeof $rootScope.getColumnMonth === "function") {
                                        $rootScope.getColumnMonth();
                                    }
                                    if (typeof $rootScope.showColumn === "function") {
                                        $rootScope.showColumn();
                                    }
                                }, 700);
                            }
                            else {
                                $timeout(function () {
                                    if (typeof $rootScope.getColumnQuater === "function") {
                                        $rootScope.getColumnQuater();
                                    }
                                    if (typeof $rootScope.showColumn === "function") {
                                        $rootScope.showColumn();
                                    }
                                }, 700);
                            }
                            setTimeout(function () {
                                if (typeof $rootScope.ShowAllOrDetail === "function") {
                                    $rootScope.ShowAllOrDetail();
                                }
                            }, 200);

                        }
                        dt.finish();
                    }, function (ex) {
                        AppendToToastr(false, scope.notification, errorNotiFalse);
                    });

                });

                $rootScope.$on("CallParentMethodWithFilter", function (event, data) {
                    var dt = Loading();
                    scope.bindQuickFilterToFilter(scope.tblColumns);
                    scope.tableParamData = scope.tableParamFilter != null ? scope.tableParamFilter : {};
                    scope.tableParamData['pageIndex'] = scope.pageIndex;
                    scope.tableParamData['pageSize'] = scope.pageSizeSelected;
                    scope.tableParamData['viewType'] = $rootScope.viewType;
                    scope.tableParamData['FromDate'] = new Date(scope.FromDate.setHours(0, 0, 0));
                    scope.tableParamData['ToDate'] = new Date(scope.ToDate.setHours(23, 59, 59));
                    scope.tableParamData['filter1'] = data.res.filter1;
                    scope.tableParamData['filter2'] = data.res.filter2;
                    scope.tableParamData['filter3'] = data.res.filter3;
                    scope.tableParamData['filter4'] = data.res.filter4;
                    scope.tableParamData['filter5'] = data.res.filter5;
                    scope.tableParamData['filter6'] = data.res.filter6;
                    scope.tableParamData['filter7'] = data.res.filter7;
                    scope.tableParamData['filter'] = data.filter + (scope.getFilterValue().trim() != '' && data.filter ? ' and ' + scope.getFilterValue() : scope.getFilterValue());
                    var getDataTbl = myService.GetTableData(scope.tableParamData, scope.tableUrl);
                    getDataTbl.then(function (emp) {
                        scope.tblDatas = emp.data.employees;
                        scope.totalCount = emp.data.totalCount;
                        scope.lstTotal = emp.data.lstTotal;
                        scope.SetTotalByColumns = function (totalName, dataFomat) {
                            if (!angular.isUndefined(totalName) && totalName !== null && scope.lstTotal != undefined) {
                                if (scope.lstTotal[totalName] != undefined && scope.lstTotal[totalName] != null) {

                                    return parseFloat(scope.lstTotal[totalName]).format(dataFomat, 3, ',', '.');
                                }
                                return scope.lstTotal[totalName];
                            }
                            return "";
                        };
                        if ($rootScope.GetColumnWhereCondition != 0 && $rootScope.GetColumnWhereCondition != undefined) {
                            if (data.res.filter8 == 1) {
                                $timeout(function () {
                                    if (typeof $rootScope.getColumnMonth === "function") {
                                        $rootScope.getColumnMonth();
                                    }
                                    if (typeof $rootScope.showColumn === "function") {
                                        $rootScope.showColumn();
                                    }
                                }, 700);
                            }
                            else {
                                $timeout(function () {
                                    if (typeof $rootScope.getColumnQuater === "function") {
                                        $rootScope.getColumnQuater();
                                    }
                                    if (typeof $rootScope.showColumn === "function") {
                                        $rootScope.showColumn();
                                    }
                                }, 700);
                            }
                            setTimeout(function () {
                                if (typeof $rootScope.ShowAllOrDetail === "function") {
                                    $rootScope.ShowAllOrDetail();
                                }
                            }, 200);
                            dt.finish();
                        }
                        dt.finish();
                    }, function (ex) {
                        AppendToToastr(false, scope.notification, errorNotiFalse);
                    });

                });

                //---end get table

                //------ xử lý filter
                scope.addFilterColumns = function () {
                    var data = angular.copy(scope.filterColumnsItem)
                    scope.filterColumnsChoosed.push(data);
                }
                scope.removeColumnFilterByIndex = function (index) {
                    scope.filterColumnsChoosed.splice(index, 1);
                }

                scope.getFilterColumns = function (tblInfo) {
                    if (!tblInfo) return;
                    var filter = myService.getFilterColumns(tblInfo.id);
                    filter.then(function (res) {
                        HiddenLoader();
                        //scope.FilterColumnsItem = angular.copy(res.data.result);
                        scope.listFilterColumns = angular.copy(res.data.result);
                        scope.GetSelectBox(scope.listFilterColumns);

                        scope.filterColumnsItem = {
                            filterColumns: angular.copy(res.data.result),
                            typeFilter: [],
                            typeEnds: scope.typeEnds,
                            textValue: "",
                            typeEndsSeleted: scope.typeEnds[0],
                            hasLink: false,
                            SelectBox: scope.listFilterColumns.SelectBox
                        }

                        scope.addFilterColumns();
                    }, function (res) {
                        AppendToToastr(false, scope.notification, errorNotiFalse);
                    });
                }
                scope.GetSelectBox = function (data) {
                    angular.forEach(data, function (item) {
                        if (item.Type == 4) {
                            item = scope.GetColumnDataById(item);

                        }
                    })
                }
                scope.GetColumnDataById = function (item) {
                    var getDataTbl = myService.GetColumnDataById(item.Id);
                    getDataTbl.then(function (emp) {
                        item['SelectBox'] = emp.data.result;
                    }, function (ex) {
                        AppendToToastr(false, scope.notification, errorNotiFalse);
                    });
                }

                scope.cancelFilterClick = function () {
                    scope.filterColumnsChoosed = [];
                    scope.quickFilter = {
                        Year: scope.$parent.Year
                    };

                    scope.GetListData();
                }

                scope.showFilterApplyButton = function () {
                    var lstObj = scope.filterColumnsChoosed;
                    if (lstObj.length === 0) {
                        return false;
                    }
                    for (var key in lstObj) {
                        var obj = lstObj[key];
                        if (obj.textValue === "") {
                            return false;
                        }
                    }
                    return true;
                }
                //toggle filter
                scope.toggleChange = function () {
                    if (scope.isShowFilter) {
                        if (scope.toggle) scope.toggle = false;
                        else scope.toggle = true;
                        //  return true;
                    }
                    // return false;
                }

                scope.getFilterValue = function () {
   
                    var lstObj = scope.filterColumnsChoosed;
                    lstObj = scope.getLinkGroup(lstObj);
                    var stringFilter = "";
                    var len = lstObj.length - 1;
                    for (var key in lstObj) {
                        var obj = lstObj[key];

                        var valueFilter = '';
                        if (obj.typeFilterSelected) {
                            switch (obj.filterSelected.Type) {
                                case 3: valueFilter = "'" + $filter('date')(new Date(obj.textValue), 'yyyy/MM/dd') + "'"; break;
                                case 4: valueFilter = obj.textValue.Value; break;
                                default: valueFilter = obj.textValue; break;
                            }
                        }

                        var textValue = (obj.typeFilterSelected ? obj.typeFilterSelected.Descriptions.replace("#value", valueFilter) : '');

                        var tmpFilter =
                            (obj.positionLink == true ? obj.typeLinkValue : "")
                            + (obj.filterSelected ? obj.filterSelected.ColumnName : "") + " "
                            + textValue
                            + (obj.positionLink == false ? obj.typeLinkValue : "")
                            + (parseInt(String(key)) === len ? "" : obj.typeEndsSeleted.value);
                        stringFilter += tmpFilter;
                    }
                    if (scope.customFilter) {
                        if (stringFilter && stringFilter != " ")
                            stringFilter += " and ";
                        stringFilter += scope.customFilter;
                    }
         
                    return stringFilter;
                };

                scope.getLinkGroup = function (lstObj) {
                    var length = lstObj.length;
                    if (length > 1) {
                        lstObj[0].typeLinkValue = lstObj[0].hasLink == true ? '(' : '';
                        lstObj[0].positionLink = true;

                        lstObj[length - 1].typeLinkValue = lstObj[length - 2].hasLink == true ? ')' : '';
                        lstObj[length - 1].positionLink = false;
                        var pre, now;

                        if (length > 2) {
                            for (var i = 1; i < lstObj.length - 1; i++) {
                                pre = lstObj[i - 1].hasLink;
                                now = lstObj[i].hasLink;
                                if (pre && !now) {
                                    lstObj[i].typeLinkValue = ')';
                                    lstObj[i].positionLink = false;
                                }
                                else if (!pre && now) {
                                    lstObj[i].typeLinkValue = '(';
                                    lstObj[i].positionLink = true;
                                }
                                else
                                    lstObj[i].typeLinkValue = '';
                            }
                        }
                    }
                    return lstObj;
                }

                //getlisttypfilter
                scope.getListTypeFilter = function () {
                    var getData = myService.getDataByGloballistNotTree(6);
                    getData.then(function (emp) {
                        scope.typeFilter = emp.data.result;
                        SplitTypeFilter(scope.typeFilter);
                    }, function (emp) {
                        AppendToToastr(false, scope.notification, errorNotiFalse);
                    });
                }
                function SplitTypeFilter(source) {
                    angular.forEach(source, function (item) {
                        switch (item.Value) {
                            case '1': scope.typeFilterA.push(item); break;
                            case '2': scope.typeFilterB.push(item); break;
                            case '3': scope.typeFilterC.push(item); break;
                            case '4': scope.typeFilterD.push(item); break;
                            default: break;
                        }
                    })
                }
                scope.filterColumnsChange = function (filterSelected, filterItemChoosed, index) {
                    var type = filterSelected.Type;
                    switch (type) {
                        case 1: scope.filterColumnsChoosed[index].typeFilter = scope.typeFilterA; break;
                        case 2: scope.filterColumnsChoosed[index].typeFilter = scope.typeFilterB; break;
                        case 3: scope.filterColumnsChoosed[index].typeFilter = scope.typeFilterC; break;
                        case 4: scope.filterColumnsChoosed[index].typeFilter = scope.typeFilterD;
                            filterSelected = scope.GetColumnDataById(filterSelected);
                            break;
                        default: break;
                    }
                };
                //----------- quick filter
                scope.bindQuickFilterToFilter = function () {
                    var lisObj = scope.listFilterColumns;
                    var rs = []
                    for (var i = 0; i < lisObj.length; i++) {
                        if (scope.quickFilter[lisObj[i].ColumnName]) {
                            var item = {
                                filterColumns: lisObj,
                                typeFilter: scope.typeFilter,
                                typeEnds: scope.typeEnds,
                                textValue: scope.quickFilter[lisObj[i].ColumnName],
                                typeFilterSelected: {},
                                typeEndsSeleted: scope.typeEnds[0],
                                filterSelected: lisObj[i],
                                hasLink: false,
                                positionLink: false,
                                typeLinkValue: '',
                            }
                            rs.push(item)
                        }
                    }

                    for (var i = 0; i < rs.length; i++) {
                        rs[i].typeFilter = scope.getFilter(rs[i].filterSelected);
                        rs[i].typeFilterSelected = rs[i].typeFilter[0];
                    }
                    scope.filterColumnsChoosed = rs;
                    scope.isShowFilter = true;
                    scope.GetListData();
                }

                scope.bindQuickFilterToFilterButNotLoadData = function () {
                    var lisObj = scope.listFilterColumns;
                    var rs = []
                    for (var i = 0; i < lisObj.length; i++) {
                        if (scope.quickFilter[lisObj[i].ColumnName]) {
                            var item = {
                                filterColumns: lisObj,
                                typeFilter: scope.typeFilter,
                                typeEnds: scope.typeEnds,
                                textValue: scope.quickFilter[lisObj[i].ColumnName],
                                typeFilterSelected: {},
                                typeEndsSeleted: scope.typeEnds[0],
                                filterSelected: lisObj[i],
                                hasLink: false,
                                positionLink: false,
                                typeLinkValue: '',
                            }
                            rs.push(item)
                        }
                    }

                    for (var i = 0; i < rs.length; i++) {
                        rs[i].typeFilter = scope.getFilter(rs[i].filterSelected);
                        rs[i].typeFilterSelected = rs[i].typeFilter[0];
                    }
                    scope.filterColumnsChoosed = rs;
                    scope.isShowFilter = true;
                };

                scope.getFilter = function (filterSelected) {
                    var type = filterSelected.Type;
                    switch (type) {
                        case 1: return scope.typeFilterA;
                        case 2: return scope.typeFilterB;
                        case 3: return scope.typeFilterC;
                        case 4: return scope.typeFilterD;
                        default: break;
                    }
                };
                //------end xử lý filter




                //This method is calling from pagination number
                scope.PageChanged = function () {
                    if (typeof $rootScope.childmethod === "function") {
                        $rootScope.childmethod();
                    } else {
                        scope.GetListData();
                    }

                };

                //This method is calling from dropDown
                scope.ChangePageSize = function () {
                    scope.pageIndex = 1;
                    if (typeof $rootScope.childmethod === "function") {
                        $rootScope.childmethod();
                    } else {
                        scope.GetListData();
                    }
                };



                scope.formatData = function (type, value) {
                    if (type === 3) {
                        return FormatDate(value);
                    }
                    return value;
                }
                scope.setShowTypeEnd = function (index) {
                    if (scope.filterColumnsChoosed.length - 1 === index) {
                        return false;
                    };
                    return true;
                }

                //function BindWidthToCss(model) {
                //    var styleWidth = '';
                //    angular.forEach(model, function (item) {
                //        styleWidth = (item.Width) ? ('width : ' + item.Width + 'px') : '100%';
                //        item.Css = (item.Css) ? (item.Css + ';' + styleWidth) : styleWidth;
                //    })
                //    return model;
                //}
                function FormatDate(inputDate) {
                    var date = new Date(inputDate);
                    if (!isNaN(date.getTime())) {
                        var day = date.getDate().toString();
                        var month = (date.getMonth() + 1).toString();
                        // Months use 0 index.
                        return (day[1] ? day : '0' + day[0]) +
                            '/' +
                            (month[1] ? month : '0' + month[0]) +
                            '/' +
                            date.getFullYear();
                    }
                };

                function convertDateTimeDefaultToString(datetime) {
                    var months = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
                    var newMonth = months[datetime.getMonth()];
                    var x;
                    switch (newMonth) {
                        case "January":
                            x = '01';
                            break;
                        case "February":
                            x = '02';
                            break;
                        case "March":
                            x = '03';
                            break;
                        case "April":
                            x = '04';
                            break;
                        case "May":
                            x = '05';
                            break;
                        case "June":
                            x = '06';
                            break;
                        case "July":
                            x = '07';
                            break;
                        case "August":
                            x = '08';
                            break;
                        case "September":
                            x = '09';
                            break;
                        case "October":
                            x = '10';
                            break;
                        case "November":
                            x = '11';
                            break;
                        case "December":
                            x = '12';
                            break;
                        default:
                            // code block
                    }
                    var getDate = datetime.getDate();
                    var getMonth = x;
                    var getFullYear = datetime.getFullYear();
                    var fullDateTime = ("0" + getDate).slice(-2) + "-" + getMonth + "-" + getFullYear + " " + ("0" + datetime.getHours()).slice(-2) + ":" + ("0" + datetime.getMinutes()).slice(-2) + ":" + ("0" + datetime.getSeconds()).slice(-2);
                    return fullDateTime;
                }

                //-------------------Excel--------------
                scope.ExcelClick = function () {
                    //scope.bindQuickFilterToFilter(scope.tblColumn);
                    //scope.bindQuickFilterToFilterButNotLoadData(scope.tblColumns);
                    var StringFromDate = convertDateTimeDefaultToString(new Date(scope.FromDate.setHours(0, 0, 0)));
                    var StringToDate = convertDateTimeDefaultToString(new Date(scope.ToDate.setHours(23, 59, 59)));
                    var filterGet = scope.getFilterValue();
                    var postiton = filterGet.indexOf("Year");
                    if (postiton == -1) {
                        if (scope.quickFilter.Year != null || scope.quickFilter.Year != "") {
                            if (filterGet == " ") {
                                filterGet += "Year LIKE N'%" + scope.quickFilter.Year + "%'";
                            }
                            else {
                                filterGet += " and Year LIKE N'%" + scope.quickFilter.Year + "%'";
                            }
                        }
                    }

                    var filter = filterGet.replace(/%/g, "!!");


                    window.location =
                        scope.tblInfo.ExcelUrl +
                        "?pageIndex=" +
                        scope.pageIndex +
                        "&pageSize=" +
                        scope.pageSizeSelected +
                        "&filter1=" +
                        $rootScope.data.filter1 +
                        "&filter2=" +
                        $rootScope.data.filter2 +
                        "&filter3=" +
                        $rootScope.data.filter3 +
                        "&filter4=" +
                        $rootScope.data.filter4 +
                        "&filter5=" +
                        $rootScope.data.filter5 +
                        "&filter6=" +
                        $rootScope.data.filter6 +
                        "&filter7=" +
                        $rootScope.data.filter7 +
                        "&StringFromDate=" +
                        StringFromDate +
                        "&StringToDate=" + StringToDate +
                        "&filter=" +
                        filter;
                }
                scope.Addlinkfilter = function (index, flag) {
                    scope.filterColumnsChoosed[index].hasLink = flag;
                }

                scope.showLink = function (index) {
                    return scope.filterColumnsChoosed[index].hasLink;
                }

                scope.SetTotalByColumns = function (totalName) {
                    if (!angular.isUndefined(totalName) && totalName !== null && scope.lstTotal != undefined) {
                        if (scope.lstTotal[totalName] != undefined && scope.lstTotal[totalName] != null) {
                            return parseFloat(scope.lstTotal[totalName]).format(scope.lstTotal[totalName].DataFomat, 3, ',', '.');
                        }
                        return scope.lstTotal[totalName];
                    }
                    return "";
                };


                //---- end xử lý các phần exten của table


                //exten function or controller
                scope.tableModel.reloadByFilter = function (strFilter) {
                    scope.customFilter = strFilter;
                    scope.GetListData();
                }
                scope.tableModel.reload = function () {
                    scope.GetListData();
                }
                //----end extenn--
                scope.changeTypeFour = function () {
                    
                    $rootScope.quickFiltercomback = angular.copy(scope.quickFilter);
                }

                scope.init = function () {
                    scope.getTableInfo($rootScope.DoNotLoad);
                    scope.getListTypeFilter();
                    scope.ListEmployees();
                    scope.ListCurrencyName();
                    scope.GetListStatus();
                    scope.ListOrganizationUnit();
                    scope.ListContractTypeName();
                }
                scope.init();
                $('.fixedTable').scroll(function (e) {
                    var prevLeft = 0;
                    $('.fixedTable').scroll(function (evt) {
                        var currentLeft = $(this).scrollLeft();
                        if (prevLeft != currentLeft) {
                            prevLeft = currentLeft;
                            $('.divTable .divTableHeading .divTableCell.fix-column').css("z-index", "1000");
                            //$('.divTable .divTableRow .divTableCell.fix-column').css("z-index", "0");

                        } else {
                            $('.divTable .divTableHeading .divTableCell.fix-column').css("z-index", "1000");
                            //$('.divTable .divTableRow .divTableCell.fix-column').css("z-index", "0");
                        }
                        $('.fixedTable').css("left", -$(".fixedTable").scrollLeft() - 1);
                        $('.fixedTable .divTableCell.fix-column').css("left", $(".fixedTable").scrollLeft() - 1);
                        $('.fixedTable').css("top", -$(".fixedTable").scrollTop() - 1);
                        $('.divTableHeading .divTableCell').css("top", $(".fixedTable").scrollTop() - 1);

                    });
                }
                );

                //------------------Định dạng cột của table----------
                scope.ColseSelectAllColumn = true;
                scope.SelectAllColumn = false;

                //Funtion  lấy tất cả cột được cấp quyền trong bảng sys_table_Column_role
                scope.getAllColumn = function (tableUrl) {
                    var getData = myService.GetAllColumns(tableUrl);
                    getData.then(function (emp) {
                        scope.listColumnFormat = angular.copy(emp.data.result)
                        scope.listColumnSortable = angular.copy(emp.data.result)
                        if ((scope.listColumnSortable.filter(function (item) { return item.isActive == true }).length) == 0) {
                            scope.ColseSelectAllColumn = false;
                            scope.SelectAllColumn = true;
                        }
                    }, function (emp) {
                        AppendToToastr(false, scope.notification, errorNotiFalse);
                    });
                }

                scope.getAllColumn(scope.tableUrl);

                //Click nút định dạng cột
                scope.columnFormat = function (tblInfo) {
                    scope.TotalColumnSelect = scope.listColumnSortable.filter(function (item) { return item.isActive == true }).length
                    for (var i = 0; i < scope.listColumnFormat.length; i++) {
                        if (scope.listColumnFormat[i].Active == false) {
                            scope.SelectAllColumn = true;
                            scope.ColseSelectAllColumn = false;
                            return;
                        }
                    }
                    ShowPopup($,
                  "#ColumnFormat",
                  1100,
                  650);
                    $('#sortable').sortable();
                }

                //Đóng model định dạng cột
                scope.CloseColumnFormat = function () {
                    $.colorbox.close();
                    scope.getAllColumn(scope.tableUrl);

                }

                //Xóa column
                scope.removeColumn = function (column) {
                    for (var i = 0; i < scope.listColumnFormat.length; i++) {
                        if (scope.listColumnFormat[i].Id == column.Id) {
                            scope.listColumnFormat[i].isActive = false;
                            break;
                        }
                    }
                    for (var i = 0; i < scope.listColumnSortable.length; i++) {
                        if (scope.listColumnSortable[i].Id == column.Id) {
                            scope.listColumnSortable[i].isActive = false;
                            break;
                        }
                    }
                    scope.TotalColumnSelect = scope.listColumnSortable.filter(function (item) { return item.isActive == true }).length
                }

                //click ô checkbox
                scope.SelectColumnEx = function (column) {
                    if (column.isActive == true) {
                        for (var i = 0; i < scope.listColumnSortable.length; i++) {
                            if (scope.listColumnSortable[i].Id == column.Id) {
                                scope.listColumnSortable[i].isActive = false;
                                break;
                            }
                        }
                        for (var i = 0; i < scope.listColumnFormat.length; i++) {
                            if (scope.listColumnFormat[i].Id == column.Id) {
                                scope.listColumnFormat[i].isActive = false;
                                break;
                            }
                        }
                    }
                    else {
                        scope.Order = scope.listColumnSortable[scope.listColumnSortable.length - 1].Order;
                        scope.max = Math.max.apply(Math, scope.listColumnSortable.map(function (item) { return item.Order; }));
                        for (var i = 0; i < scope.listColumnSortable.length; i++) {
                            if (scope.listColumnSortable[i].Id == column.Id) {
                                scope.listColumnSortable[i].isActive = true;
                                scope.listColumnSortable[i].Order = scope.max + 1;
                                break;
                            }
                        }
                        for (var i = 0; i < scope.listColumnFormat.length; i++) {
                            if (scope.listColumnFormat[i].Id == column.Id) {
                                scope.listColumnFormat[i].isActive = true;
                                break;
                            }
                        }
                    }

                    scope.TotalColumnSelect = scope.listColumnSortable.filter(function (item) { return item.isActive == true }).length
                }

                //Xóa tất cả
                scope.clickColseSelectAllColumns = function () {
                    for (var i = 0; i < scope.listColumnSortable.length; i++) {
                        scope.listColumnSortable[i].isActive = false;
                    }
                    for (var i = 0; i < scope.listColumnFormat.length; i++) {
                        scope.listColumnFormat[i].isActive = false;
                    }
                    scope.ColseSelectAllColumn = false;
                    scope.SelectAllColumn = true;
                    scope.TotalColumnSelect = scope.listColumnSortable.filter(function (item) { return item.isActive == true }).length
                }

                //Chọn tất cả
                scope.clickSelectAllColumns = function () {
                    for (var i = 0; i < scope.listColumnFormat.length; i++) {
                        scope.listColumnFormat[i].isActive = true;
                    }
                    scope.listColumnSortable = angular.copy(scope.listColumnFormat);
                    scope.ColseSelectAllColumn = true;
                    scope.SelectAllColumn = false;
                    scope.TotalColumnSelect = scope.listColumnSortable.filter(function (item) { return item.isActive == true }).length;
                    $timeout(function () {
                        $('#sortable').sortable();
                    }, 100)
                }

                //Bỏ tìm kiếm
                scope.deleteFind = function () {
                    if (scope.txtSearchColumn != null && scope.txtSearchColumn != "") {
                        scope.txtSearchColumn = '';
                    }
                }

                scope.SaveCustomerColumns = function () {
                    scope.DataColumn = [];
                    for (var i = 0; i < scope.listColumnSortable.length; i++) {
                        var column = '#column-' + scope.listColumnSortable[i].ColumnName
                        var obj = { Id: scope.listColumnSortable[i].Id, isActive: scope.listColumnSortable[i].isActive, OrderNo: $(column).index(), ColumnName: scope.listColumnSortable[i].ColumnName, TableId: scope.listColumnSortable[i].TableId }
                        scope.DataColumn.push(obj);
                    }
                    myService.UpdateColumn(scope.DataColumn).then(function (res) {
                        if (res.data.result.IsSuccess == true) {
                            AppendToToastr(true, notification, res.data.result.Message, 500, 5000);
                            scope.getColumns();
                            scope.GetListData(scope.tableUrl);
                            scope.getAllColumn(scope.tableUrl);
                            $.colorbox.close();
                        }
                        else {
                            AppendToToastr(false, notification, res.data.result.Message, 500, 5000);
                        }
                    }, function (res) {
                        AppendToToastr(false, notification, errorNotiFalse);
                    });
                }

                //----------------- kết thúc định dạng cột------------------

            }
        };
    }]);
    appModule.directive('myDatePicker', function () {
        return {
            restrict: 'A',
            require: '?ngModel',
            link: function (scope, element, attrs, ngModelController) {
      
                // Private variables
                var datepickerFormat = 'dd/mm/yyyy',
                    momentFormat = 'DD/MM/YYYY',
                    datepicker,
                    elPicker;
      
                // Init date picker and get objects http://bootstrap-datepicker.readthedocs.org/en/release/index.html
                datepicker = element.datepicker({
                    autoclose: true,
                    keyboardNavigation: false,
                    todayHighlight: true,
                    format: datepickerFormat
                });
                elPicker = datepicker.data('datepicker').picker;
      
                // Adjust offset on show
                datepicker.on('show', function (evt) {
                    elPicker.css('left', parseInt(elPicker.css('left')) + +attrs.offsetX);
                    elPicker.css('top', parseInt(elPicker.css('top')) + +attrs.offsetY);
                });
      
                // Only watch and format if ng-model is present https://docs.angularjs.org/api/ng/type/ngModel.NgModelController
                if (ngModelController) {
                    // So we can maintain time
                    var lastModelValueMoment;
      
                    ngModelController.$formatters.push(function (modelValue) {
                        //
                        // Date -> String
                        //
      
                        // Get view value (String) from model value (Date)
                        var viewValue,
                            m = moment(modelValue);
                        if (modelValue && m.isValid()) {
                            // Valid date obj in model
                            lastModelValueMoment = m.clone(); // Save date (so we can restore time later)
                            viewValue = m.format(momentFormat);
                        } else {
                            // Invalid date obj in model
                            lastModelValueMoment = undefined;
                            viewValue = undefined;
                        }
      
                        // Update picker
                        element.datepicker('update', viewValue);
      
                        // Update view
                        return viewValue;
                    });
      
                    ngModelController.$parsers.push(function (viewValue) {
                        //
                        // String -> Date
                        //
      
                        // Get model value (Date) from view value (String)
                        var modelValue,
                            m = moment(viewValue, momentFormat, true);
                        if (viewValue && m.isValid()) {
                            // Valid date string in view
                            if (lastModelValueMoment) { // Restore time
                                m.hour(lastModelValueMoment.hour());
                                m.minute(lastModelValueMoment.minute());
                                m.second(lastModelValueMoment.second());
                                m.millisecond(lastModelValueMoment.millisecond());
                            }
                            modelValue = m.toDate();
                        } else {
                            // Invalid date string in view
                            modelValue = undefined;
                        }
      
                        // Update model
                        return modelValue;
                    });
      
                    datepicker.on('changeDate', function (evt) {
                        // Only update if it's NOT an <input> (if it's an <input> the datepicker plugin trys to cast the val to a Date)
                        if (evt.target.tagName !== 'INPUT') {
                            ngModelController.$setViewValue(moment(evt.date).format(momentFormat)); // $seViewValue basically calls the $parser above so we need to pass a string date value in
                            ngModelController.$render();
                        }
                    });
                }
      
            }
        };
    });
    appModule.directive('convertToNumber', function () {
        return {
            require: 'ngModel',
            link: function (scope, element, attrs, ngModel) {
                ngModel.$parsers.push(function (val) {
                    return val !== null ? parseInt(val, 10) : null;
                });
                ngModel.$formatters.push(function (val) {
                    return val !== null ? '' + val : null;
                });
            }
        };
    });
    appModule.directive("myFiles", function ($parse) {
        return function linkFn(scope, elem, attrs) {
            elem.on("change",
                function (e) {
                    scope.$eval(attrs.myFiles + "=$files", { $files: e.target.files });
                    scope.$apply();
                });
        }
    });
    appModule.directive('formatCurreny', [function () {
        return {
            require: 'ngModel',
            link: function (scope, element, attrs, ngModel) {
                attrs.$set('ngTrim', "false");

                var formatter = function (str, isNum) {
                    str = String(Number(str || 0) / (isNum ? 1 : 100));
                    str = (str == '0' ? '0.0' : str).split('.');
                    str[1] = str[1] || '0';
                    return str[0].replace(/(\d)(?=(\d\d\d)+(?!\d))/g, '$1,') + '.' + (str[1].length == 1 ? str[1] + '0' : str[1]);
                }
                var updateView = function (val) {
                    scope.$applyAsync(function () {
                        ngModel.$setViewValue(val || '');
                        ngModel.$render();
                    });
                }
                var parseNumber = function (val) {
                    var modelString = formatter(ngModel.$modelValue, true);
                    var sign = {
                        pos: /[+]/.test(val),
                        neg: /[-]/.test(val)
                    }
                    sign.has = sign.pos || sign.neg;
                    sign.both = sign.pos && sign.neg;

                    if (!val || sign.has && val.length == 1 || ngModel.$modelValue && Number(val) === 0) {
                        var newVal = (!val || ngModel.$modelValue && Number() === 0 ? '' : val);
                        if (ngModel.$modelValue !== newVal)
                            updateView(newVal);

                        return '';
                    }
                    else {
                        var valString = String(val || '');
                        var newSign = (sign.both && ngModel.$modelValue >= 0 || !sign.both && sign.neg ? '-' : '');
                        var newVal = valString.replace(/[^0-9]/g, '');
                        var viewVal = newSign + formatter(angular.copy(newVal));

                        if (modelString !== valString)
                            updateView(viewVal);

                        return (Number(newSign + newVal) / 100) || 0;
                    }
                }
                var formatNumber = function (val) {
                    if (val) {
                        var str = String(val).split('.');
                        str[1] = str[1] || '0';
                        val = str[0] + '.' + (str[1].length == 1 ? str[1] + '0' : str[1]);
                    }
                    return parseNumber(val);
                }

                ngModel.$parsers.push(parseNumber);
                ngModel.$formatters.push(formatNumber);
            }
        };
    }]);
    appModule.directive('numbersFormatOnly', function () {
        return {
            require: 'ngModel',
            link: function (scope, element, attr, ngModelCtrl) {
                function fromUser(text) {
                    if (text) {
                        var transformedInput = text.replace(/[^0-9]/g, '');

                        if (transformedInput !== text) {
                            ngModelCtrl.$setViewValue(transformedInput);
                            ngModelCtrl.$render();
                        }
                        return transformedInput;
                    }
                    return undefined;
                }
                ngModelCtrl.$parsers.push(fromUser);
            }
        };
    });
    appModule.directive('convertToYear', function () {
        return {
            require: 'ngModel',
            link: function (scope, element, attr, ngModelCtrl) {
                var endYear = new Date(new Date().getFullYear(), 11, 31);
                $(".datePicker12345").datepicker({
                    autoclose: true,
                    format: "yyyy",
                    startView: "years",
                    minViewMode: "years",
                    maxViewMode: "years"
                }).datepicker("setDate", new Date());               
            }
        };
    });
    appModule.directive('convertToMonthYear', function () {
        return {
            require: 'ngModel',
            link: function (scope, element, attr, ngModelCtrl) {
                var endYear = new Date(new Date().getFullYear(), 11, 31);            
                $(".MonthYear").datepicker({
                    autoclose: true,
                    format: "mm/yyyy",
                    startDate: "1/2013",
                    endDate: endYear,
                    startView: "months",
                    minViewMode: "months",
                    maxViewMode: "years"
                }).datepicker("setDate", new Date());
                $(".input-date").datepicker({
                    autoclose: true,
                    format: "yyyy",
                    startView: "years",
                    minViewMode: "years",
                    maxViewMode: "years"
                });
            }
        };
    });
    appModule.directive('convertToMonth', function () {
        return {
            require: 'ngModel',
            link: function (scope, element, attr, ngModelCtrl) {
                var endYear = new Date(new Date().getFullYear(), 11, 31);
                $(".is-month").datepicker({
                    autoclose: true,
                    format: "mm",
                    startView: "months",
                    minViewMode: "months",
                    maxViewMode: "years"
                });
            }
        };
    });
    appModule.directive('compilerAddEvents', function () {
        return {
            link: function (scope, element, attr) {
                $('#checkAll').prop('checked', false);
                $('#btnCopy').hide();
                $('#checkAll').change(function () {
                    $('.cb-element').prop('checked', this.checked);
                    if ($('.cb-element:checked').length > 0) {
                        $('#btnCopy').show();
                    }
                    else {
                        $('#btnCopy').hide();
                    }
                });
                $('.cb-element').change(function () {
                    if ($('.cb-element:checked').length == $('.cb-element').length) {
                        $('#checkAll').prop('checked', true);
                    }
                    else {
                        $('#checkAll').prop('checked', false);
                    }
                    if ($('.cb-element:checked').length > 0) {
                        $('#btnCopy').show();
                    }
                    else {
                        $('#btnCopy').hide();
                    }
                });
            }
        };
    });
    appModule.directive('sgNumberInput', ['$filter', '$locale', function ($filter, $locale) {
        return {
            require: 'ngModel',
            restrict: "A",
            link: function ($scope, element, attrs, ctrl) {
                var fractionSize = parseInt(attrs['fractionSize']) || 0;
                var numberFilter = $filter('number');
                //format the view value
                ctrl.$formatters.push(function (modelValue) {
                    var retVal = numberFilter(modelValue, fractionSize);
                    var isValid = isNaN(modelValue) == false;
                    ctrl.$setValidity(attrs.name, isValid);
                    return retVal;
                });
                //parse user's input
                ctrl.$parsers.push(function (viewValue) {
                    var caretPosition = getCaretPosition(element[0]), nonNumericCount = countNonNumericChars(viewValue);
                    viewValue = viewValue || '';
                    //Replace all possible group separators
                    var trimmedValue = viewValue.trim().replace(/,/g, '').replace(/`/g, '').replace(/'/g, '').replace(/\u00a0/g, '').replace(/ /g, '');
                    //If numericValue contains more decimal places than is allowed by fractionSize, then numberFilter would round the value up
                    //Thus 123.109 would become 123.11
                    //We do not want that, therefore I strip the extra decimal numbers
                    var separator = $locale.NUMBER_FORMATS.DECIMAL_SEP;
                    var arr = trimmedValue.split(separator);
                    var decimalPlaces = arr[1];
                    if (decimalPlaces != null && decimalPlaces.length > fractionSize) {
                        //Trim extra decimal places
                        decimalPlaces = decimalPlaces.substring(0, fractionSize);
                        trimmedValue = arr[0] + separator + decimalPlaces;
                    }
                    var numericValue = parseFloat(trimmedValue);
                    var isEmpty = numericValue == null || viewValue.trim() === "";
                    var isRequired = attrs.required || false;
                    var isValid = true;
                    if (isEmpty && isRequired) {
                        isValid = false;
                    }
                    if (isEmpty == false && isNaN(numericValue)) {
                        isValid = false;
                    }
                    ctrl.$setValidity(attrs.name, isValid);
                    if (isNaN(numericValue) == false && isValid) {
                        var newViewValue = numberFilter(numericValue, fractionSize);
                        element.val(newViewValue);
                        var newNonNumbericCount = countNonNumericChars(newViewValue);
                        var diff = newNonNumbericCount - nonNumericCount;
                        var newCaretPosition = caretPosition + diff;
                        if (nonNumericCount == 0 && newCaretPosition > 0) {
                            newCaretPosition--;
                        }
                        setCaretPosition(element[0], newCaretPosition);
                    }
                    return isNaN(numericValue) == false ? numericValue : null;
                });
            } //end of link function
        };
        //#region helper methods
        function getCaretPosition(inputField) {
            // Initialize
            var position = 0;
            // IE Support
            if (document.selection) {
                inputField.focus();
                // To get cursor position, get empty selection range
                var emptySelection = document.selection.createRange();
                // Move selection start to 0 position
                emptySelection.moveStart('character', -inputField.value.length);
                // The caret position is selection length
                position = emptySelection.text.length;
            }
            else if (inputField.selectionStart || inputField.selectionStart == 0) {
                position = inputField.selectionStart;
            }
            return position;
        }
        function setCaretPosition(inputElement, position) {
            if (inputElement.createTextRange) {
                var range = inputElement.createTextRange();
                range.move('character', position);
                range.select();
            }
            else {
                if (inputElement.selectionStart) {
                    inputElement.focus();
                    inputElement.setSelectionRange(position, position);
                }
                else {
                    inputElement.focus();
                }
            }
        }
        function countNonNumericChars(value) {
            return (value.match(/[^a-z0-9]/gi) || []).length;
        }
        //#endregion helper methods
    }]);
})(angular);