
function BuildTable(appName, controllerName, tableUrl) {
    app.controller(controllerName,
        function ($scope, myService, $filter, $q) {
            // $scope.test = $filter('mm/dd/yyyy')("2018/01/01", 'yyyy/MM/dd');
            $scope.maxSize = 5; // Limit number for pagination display number.
            $scope.totalCount = 0; // Total number of items in all pages. initialize as a zero
            $scope.pageIndex = 1; // Current page number. First page is 1.-->
            $scope.pageSizeSelected = 5;
            $scope.isShowFilter = false;
            $scope.typeEnds = [{ name: "Và", nameEN: "And", value: " and " }, { name: "Hoặc", nameEN: "Or", value: " or " }];
            $scope.filterColumnsChoosed = [];

            $scope.typeFilterA = [{ name: ">=", nameEN: ">=", value: " >= '#' " }, { name: "<=", nameEN: "=<", value: " <= '#' " }, { name: "=", nameEN: "=", value: " = '#' " }, { name: "Khác", nameEN: "Not equal", value: " != '#' " }];
            $scope.typeFilterB = [{ name: "Có chứa", nameEN: "Contains", value: " like '%#%' " }, { name: "Bằng", nameEN: "Is", value: " = '#' " }, { name: "Không chứa", nameEN: "Do not contains", value: " != '#' " }];
            $scope.emailValid = /^[a-z]+[a-z0-9._]+@[a-z]+\.[a-z.]{2,5}$/;
            $scope.typeFilterC = [{ name: "=", nameEN: "=", value: " = '#' " }];
            $scope.checkboxModel = {
                value: "Insurance"
            };
            $scope.CallClick = function () {
                //-----------------List-------------
                $scope.getTableInfo = function () {
                    var getData = myService.getTableInformation(tableUrl);
                    getData.then(function (emp) {
                        $scope.tableInfo = emp.data.result;
                        $scope.lstPageSize = $scope.tableInfo.PageSizeList.split(',');
                        $scope.pageSizeSelected = $scope.tableInfo.PageSize;
                        $scope.GetAddPermission(emp.data.result.id);
                    },
                        function (emp) {
                            AppendToToastr(false, notification, errorNotiFalse);
                        });
                }
                $scope.getTableInfo();
                $scope.GetAddPermission = function (idTable) {
                    var tblAction = myService.getTableAction(idTable);
                    tblAction.then(function (emp) {
                        $scope.tablePermission = emp.data.result;
                        $scope.getColumns();
                        // $scope.BuildAddButton(emp.data.result);
                    },
                        function (emp) {
                            AppendToToastr(false, notification, errorNotiFalse);
                        });
                }
                $scope.getColumns = function () {
                    var getData = myService.GetColumns(tableUrl);
                    getData.then(function (emp) {
                        $scope.Columns = emp.data.result;
                        $scope.GetListData();
                    },
                        function (emp) {
                            AppendToToastr(false, notification, errorNotiFalse);
                        });
                }
                $scope.GetListData = function () {
                    ShowLoader();
                    var data = {
                        pageIndex: $scope.pageIndex,
                        pageSize: $scope.pageSizeSelected,
                        filter: $scope.getFilterValue()
                    }
                    var getDataTbl = myService.GetTableData(data, tableUrl);
                    getDataTbl.then(function (emp) {
                        $scope.employees = emp.data.employees;
                        $scope.totalCount = emp.data.totalCount;
                        $scope.lstTotal = emp.data.lstTotal;
                        $scope.SetTotalByColumns = function (totalName) {
                            if (!angular.isUndefined(totalName) && totalName !== null) {
                                return $scope.lstTotal[totalName];

                            }
                            return "-";
                        }
                        HiddenLoader();
                    },
                        function (emp) {
                            AppendToToastr(false, notification, errorNotiFalse);
                        });

                }


                //This method is calling from pagination number
                $scope.pageChanged = function () {
                    $scope.GetListData();
                };

                //This method is calling from dropDown
                $scope.changePageSize = function () {
                    $scope.pageIndex = 1;
                    $scope.GetListData();
                };
                $scope.formatData = function (type, value) {
                    if (type === 3) {

                        return FormatDate(value);

                    }
                    return value;
                }
                $scope.SetHiddenActionColumn = function (showEdit, showDelete) {
                    if (showEdit === false && showDelete === false) {
                        return false;
                    }
                    return true;
                }

                //-----------------List-End---------
                // -----------------Edit------------

                $scope.updateAction1 = function (url, form) {
                    if (form.$valid) {
                        $scope.editData.RateCompany = $scope.editData.RateCompany.replace(/\,/g, '');
                        $scope.editData.RatePerson = $scope.editData.RatePerson.replace(/\,/g, '');
                        var updateAction = myService.UpdateData(url, $scope.editData);
                        updateAction.then(function (res) {
                            if (res.data.result.IsSuccess) {
                                $scope.GetListData();
                                $.colorbox.close();
                            }
                            AppendToToastr(res.data.result.IsSuccess, notification, res.data.result.Message, 500, 5000);
                        },
                            function (res) {
                                AppendToToastr(false, notification, errorNotiFalse);
                            });
                    }
                }

                $scope.editClick = function (contentItem) {
                    ListInsuranceType();
                    ListInsuranceStatus();
                    setTimeout(function () {
                        var edit = myService.getDataById(contentItem.AutoID, $scope.tableInfo.id, $scope.tableInfo.DataEditUrl);
                        edit.then(function (emp) {
                            $scope.editData = emp.data.result;
                            $scope.editData.RateCompany = accounting.formatNumber(emp.data.result.RateCompany, 2, ",", ".");
                            $scope.editData.RatePerson = accounting.formatNumber(emp.data.result.RatePerson, 2, ",", ".");
                            $scope.editData.ApplyDate = new Date($scope.editData.ApplyDate);
                        }, function (emp) {
                            AppendToToastr(false, notification, errorNotiFalse);
                        });
                    }, 200);
                    ShowPopup($,
                         "#EditContent",
                         $scope.tableInfo.PopupWidth,
                         $scope.tableInfo.PopupHeight);
                }


                // -----------------Edit--End----------
                //------------------Add---------------
                $scope.addClick = function () {
                    $scope.editData = {
                        Status: 2015,
                        ApplyDate: new Date()
                    };
                    ListInsuranceType();
                    ListInsuranceStatus();
                    ShowPopup($,
                        "#EditContent",
                        $scope.tableInfo.PopupWidth,
                        $scope.tableInfo.PopupHeight);

                }
                $scope.AddAction = function (url, form) {
                    if (form.$valid) {
                        $scope.editData.RateCompany = $scope.editData.RateCompany.replace(/\,/g, '');
                        $scope.editData.RatePerson = $scope.editData.RatePerson.replace(/\,/g, '');
                        var updateAction = myService.UpdateData(url, $scope.editData);
                        updateAction.then(function (res) {
                            if (res.data.result.IsSuccess) {
                                $scope.GetListData();
                                $.colorbox.close();
                            }
                            AppendToToastr(res.data.result.IsSuccess, notification, res.data.result.Message, 500, 5000);

                        },
                            function (res) {
                                AppendToToastr(false, notification, errorNotiFalse);
                            });
                    }
                }

                //------------------Add-End--------------

                // -----------------Xóa------------

                $scope.deleteClick = function (obj) {
                    BoostrapDialogConfirm(notification,
                        notificationDelete,
                        BootstrapDialog.TYPE_WARNING,
                        $scope.deleteActionClick,
                        obj);
                }
                $scope.deleteActionClick = function (obj) {
                    var action = myService.deleteAction(obj.AutoID, $scope.tableInfo.id, $scope.tableInfo.DeleteUrl);
                    action.then(function (res) {
                        if (res.data.result.IsSuccess) {
                            $scope.GetListData();
                        }
                        AppendToToastr(res.data.result.IsSuccess, notification, res.data.result.Message, 500, 5000);
                    },
                        function (res) {
                            AppendToToastr(false, notification, errorNotiFalse);
                        });

                }

                // -----------------Xóa--End------------

                //------------Get Insurance Type-------------
                function ListInsuranceType() {
                    var listInsuranceType = myService.ListInsuranceType();
                    listInsuranceType.then(function (res) {
                        $scope.getListInsuranceType = res.data.result;
                    }, function (res) {
                        $scope.msg = "Error";
                    })
                }
                //------------Get Insurance Type End---------

                //------------Get Insurance Status-------------
                function ListInsuranceStatus() {
                    var listInsuranceStatus = myService.ListInsuranceStatus();
                    listInsuranceStatus.then(function (res) {
                        $scope.getListInsuranceStatus = res.data.result;
                    }, function (res) {
                        $scope.msg = "Error";
                    })
                }
                //------------Get Insurance Status End---------



                //-------------------Excel--------------
                $scope.ExcelClick = function () {
                    var filterString = $scope.getFilterValue();
                    window.location = $scope.tableInfo.ExcelUrl + "?filterString=" + filterString;
                }
                //-------------------Excel-End----------
                $scope.CloseForm = function () {
                    $.colorbox.close();
                }
            }
            $scope.CallClick();
            $scope.CallClick2 = function () {

                //-----------------List-------------
                $scope.getTableInfo = function () {
                    var getData = myService.getTableInformation("/InsurancePosition/TableServerSideGetData");
                    getData.then(function (emp) {
                        $scope.tableInfo = emp.data.result;
                        $scope.lstPageSize = $scope.tableInfo.PageSizeList.split(',');
                        $scope.pageSizeSelected = $scope.tableInfo.PageSize;
                        $scope.GetAddPermission(emp.data.result.id);
                    },
                        function (emp) {
                            AppendToToastr(false, notification, errorNotiFalse);
                        });
                }
                $scope.getTableInfo();
                $scope.GetAddPermission = function (idTable) {
                    var tblAction = myService.getTableAction(idTable);
                    tblAction.then(function (emp) {
                        $scope.tablePermission = emp.data.result;
                        $scope.getColumns();
                        // $scope.BuildAddButton(emp.data.result);
                    },
                        function (emp) {
                            AppendToToastr(false, notification, errorNotiFalse);
                        });
                }
                $scope.getColumns = function () {
                    var getData = myService.GetColumns("/InsurancePosition/TableServerSideGetData");
                    getData.then(function (emp) {
                        $scope.Columns = emp.data.result;
                        $scope.GetListData();
                    },
                        function (emp) {
                            AppendToToastr(false, notification, errorNotiFalse);
                        });
                }
                $scope.GetListData = function () {
                    ShowLoader();
                    var data = {
                        pageIndex: $scope.pageIndex,
                        pageSize: $scope.pageSizeSelected,
                        filter: $scope.getFilterValue()
                    }
                    var getDataTbl = myService.GetTableData(data, "/InsurancePosition/TableServerSideGetData");
                    getDataTbl.then(function (emp) {
                        $scope.employees = emp.data.employees;
                        $scope.totalCount = emp.data.totalCount;
                        $scope.lstTotal = emp.data.lstTotal;
                        $scope.SetTotalByColumns = function (totalName) {
                            if (!angular.isUndefined(totalName) && totalName !== null) {
                                return $scope.lstTotal[totalName];

                            }
                            return "-";
                        }
                        HiddenLoader();
                    },
                        function (emp) {
                            AppendToToastr(false, notification, errorNotiFalse);
                        });

                }


                //This method is calling from pagination number
                $scope.pageChanged = function () {
                    $scope.GetListData();
                };

                //This method is calling from dropDown
                $scope.changePageSize = function () {
                    $scope.pageIndex = 1;
                    $scope.GetListData();
                };
                $scope.formatData = function (type, value) {
                    if (type === 3) {

                        return FormatDate(value);

                    }
                    return value;
                }
                $scope.SetHiddenActionColumn = function (showEdit, showDelete) {
                    if (showEdit === false && showDelete === false) {
                        return false;
                    }
                    return true;
                }

                //-----------------List-End---------

                // -----------------Edit------------

                $scope.updateAction = function (url, form) {
                    if (form.$valid) {
                        $scope.editData.Amount = $scope.editData.Amount.replace(/\,/g, '');
                        var updateAction = myService.UpdateData(url, $scope.editData);
                        updateAction.then(function (res) {
                            if (res.data.result.IsSuccess) {
                                $scope.GetListData();
                                $.colorbox.close();
                            }
                            AppendToToastr(res.data.result.IsSuccess, notification, res.data.result.Message, 500, 5000);
                        },
                            function (res) {
                                AppendToToastr(false, notification, errorNotiFalse);
                            });
                    }
                }

                $scope.editClick = function (contentItem) {
                    var edit = myService.getDataById(contentItem.AutoID, $scope.tableInfo.id, $scope.tableInfo.DataEditUrl);
                    edit.then(function (emp) {
                        ListPositionName();
                        ListInsuranceStatus();
                        $scope.editData = emp.data.result;
                        $scope.editData.Amount = accounting.formatNumber(emp.data.result.Amount, 2, ",", ".");
                        $scope.editData.ApplyDate = new Date($scope.editData.ApplyDate);
                        ShowPopup($,
                            "#EditContent",
                            $scope.tableInfo.PopupWidth,
                            $scope.tableInfo.PopupHeight);
                    },
                        function (emp) {
                            AppendToToastr(false, notification, errorNotiFalse);
                        });

                }


                // -----------------Edit--End----------
                //------------------Add---------------
                $scope.addClick = function () {
                    $scope.editData = {
                        Status: 2015,
                        ApplyDate: new Date()
                    };
                    ListPositionName();
                    ListInsuranceStatus();
                    ShowPopup($,
                        "#AddContent",
                        $scope.tableInfo.PopupWidth,
                        $scope.tableInfo.PopupHeight);

                }

                $scope.AddAction = function (url, form) {
                    if (form.$valid) {
                        $scope.editData.Amount = $scope.editData.Amount.replace(/\,/g, '');
                        var updateAction = myService.UpdateData(url, $scope.editData);
                        updateAction.then(function (res) {
                            if (res.data.result.IsSuccess) {
                                $scope.GetListData();
                                $.colorbox.close();
                            }
                            AppendToToastr(res.data.result.IsSuccess, notification, res.data.result.Message, 500, 5000);
                        },
                            function (res) {
                                AppendToToastr(false, notification, errorNotiFalse);
                            });
                    }
                }

                //------------------Add-End--------------

                // -----------------Xóa------------

                $scope.deleteClick = function (obj) {
                    BoostrapDialogConfirm(notification,
                        notificationDelete,
                        BootstrapDialog.TYPE_WARNING,
                        $scope.deleteActionClick,
                        obj);
                }
                $scope.deleteActionClick = function (obj) {
                    var action = myService.deleteAction(obj.AutoID, $scope.tableInfo.id, $scope.tableInfo.DeleteUrl);
                    action.then(function (res) {
                        if (res.data.result.IsSuccess) {
                            $scope.GetListData();
                        }
                        AppendToToastr(res.data.result.IsSuccess, notification, res.data.result.Message, 500, 5000);
                    },
                        function (res) {
                            AppendToToastr(false, notification, errorNotiFalse);
                        });

                }

                $scope.CloseForm = function () {
                    $.colorbox.close();
                }

                //------------Get Insurance Status-------------
                function ListInsuranceStatus() {
                    var list = myService.ListInsuranceStatus();
                    list.then(function (res) {
                        $scope.getListInsuranceStatus = res.data.result;
                    }, function (res) {
                        $scope.msg = "Error";
                    })
                }
                //------------Get Insurance Status End---------

                //------------Get Insurance Status-------------
                function ListPositionName() {
                    var list = myService.ListPositionName();
                    list.then(function (res) {
                        $scope.getListPositionName = res.data.result;
                    }, function (res) {
                        $scope.msg = "Error";
                    })
                }
                //------------Get Insurance Status End---------

                //-------------------Excel--------------
                $scope.ExcelClick = function () {
                    var filterString = $scope.getFilterValue();
                    window.location = $scope.tableInfo.ExcelUrl + "?filterString=" + filterString;

                }
                //-------------------Excel-End----------

            }
            //-----------------Filter-----------
            $scope.GetSelectBox = function (data) {
                angular.forEach(data, function (item) {
                    if (item.Type == 4) {
                        item = $scope.GetColumnDataById(item);

                    }
                })
            }
            $scope.GetColumnDataById = function (item) {
                var getDataTbl = myService.GetColumnDataById(item.Id);
                getDataTbl.then(function (emp) {
                    item['SelectBox'] = emp.data.result;
                }, function (ex) {
                    AppendToToastr(false, scope.notification, errorNotiFalse);
                });
            }
            $scope.getFilterColumns = function () {
                var filter = myService.getFilterColumns($scope.tableInfo.id);
                filter.then(function (res) {
                    HiddenLoader();
                    $scope.FilterColumnsItem = res.data.result;
                    $scope.isShowFilter = true;
                    $scope.tablePermission.isFilterButton = false;
                    $scope.GetSelectBox($scope.FilterColumnsItem);
                    var filterColumnsItem = {
                        filterColumns: res.data.result,
                        typeFilter: {},
                        typeEnds: $scope.typeEnds,
                        textValue: "",
                        typeEndsSeleted: $scope.typeEnds[0]
                    }
                    $scope.filterColumnsChoosed.push(filterColumnsItem);
                },
                    function (res) {
                        AppendToToastr(false, notification, errorNotiFalse);
                    });
            }


            

            $scope.cancelFilterClick = function () {
                $scope.filterColumnsChoosed = [];
                $scope.isShowFilter = false;
                $scope.tablePermission.isFilterButton = true;
                $scope.GetListData();
            };

            $scope.setShowTypeEnd = function (index) {
                if ($scope.filterColumnsChoosed.length - 1 === index) {
                    return false;
                };
                return true;
            }

            $scope.addFilterColumns = function () {
                var filterColumnsItem = {
                    filterColumns: $scope.FilterColumnsItem,
                    typeEnds: $scope.typeEnds,
                    textValue: "",
                    typeEndsSeleted: $scope.typeEnds[0]
                }
                $scope.filterColumnsChoosed.push(filterColumnsItem);
            }

            $scope.filterColumnsChange = function (filterSelected, index) {
                var type = filterSelected.Type;
                if (type === 2 || type === 3) {
                    $scope.typeFilter = $scope.typeFilterA;
                    $scope.filterColumnsChoosed[index].typeFilter = $scope.typeFilterA;
                } else if (type === 1) {
                    $scope.typeFilter = $scope.typeFilterB;
                    $scope.filterColumnsChoosed[index].typeFilter = $scope.typeFilterB;
                }
                else if (type === 4) {
                    $scope.typeFilter = $scope.typeFilterC;
                    $scope.filterColumnsChoosed[index].typeFilter = $scope.typeFilterC;
                }
                
            };

            $scope.removeColumnFilterByIndex = function (index) {
                $scope.filterColumnsChoosed.splice(index, 1);
            };

            $scope.setTypeInput = function (type) {
                return "text";
            };

            $scope.getFilterValue = function () {
                var lstObj = $scope.filterColumnsChoosed;
                var stringFilter = "";
                var len = lstObj.length - 1;
                for (var key in lstObj) {
                    var obj = lstObj[key];

                    var valueFilter = '';
                    if (obj.typeFilterSelected) {
                        switch (obj.filterSelected.Type) {
                            case 3: valueFilter = $filter('date')(new Date(obj.textValue), 'yyyy/MM/dd'); break;
                            case 4: valueFilter = obj.textValue.Value; break;
                            default: valueFilter = obj.textValue; break;
                        }
                    }

                    var textValue = (obj.typeFilterSelected ? obj.typeFilterSelected.value.replace("#", valueFilter) : '');

                    var tmpFilter =
                        (obj.positionLink == true ? obj.typeLinkValue : "")
                        + (obj.filterSelected ? obj.filterSelected.ColumnName : "") + " "
                        + textValue
                        + (obj.positionLink == false ? obj.typeLinkValue : "")
                        + (parseInt(String(key)) === len ? "" : obj.typeEndsSeleted.value);
                    //stringFilter += tmpFilter;
                    if (stringFilter && stringFilter != " ") {
                        stringFilter += tmpFilter;
                    }
                    else {
                        stringFilter += tmpFilter;
                    }
                }
               
                   

                return stringFilter;
            };

            $scope.showFilterApplyButton = function () {
                var lstObj = $scope.filterColumnsChoosed;
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
            };
            //-----------------Filter-End----------


        });
    app.directive('convertToNumber',
        function () {
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
    app.directive("formatDate",
        function () {
            return {
                require: 'ngModel',
                link: function (scope, elem, attr, modelCtrl) {
                    modelCtrl.$formatters.push(function (modelValue) {
                        return new Date(modelValue)();
                    });
                }
            }
        });


}
