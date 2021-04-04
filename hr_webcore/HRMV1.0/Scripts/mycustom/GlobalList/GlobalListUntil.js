function BuildTable(appName, controllerName, tableUrl) {
    app.controller(controllerName,
        function ($scope, myService, $filter, $rootScope) {
            // $scope.test = $filter('mm/dd/yyyy')("2018/01/01", 'yyyy/MM/dd');
            $scope.maxSize = 5; // Limit number for pagination display number.
            $scope.totalCount = 0; // Total number of items in all pages. initialize as a zero
            $scope.pageIndex = 1; // Current page number. First page is 1.-->
            $scope.pageSizeSelected = 5;
            $scope.isShowFilter = false;
            $scope.typeEnds = [{ name: "Và", value: " and " }, { name: "Hoặc", value: " or " }];
            $scope.filterColumnsChoosed = [];
            $scope.editData = {};
            $scope.typeFilterA = [{ name: "Lớn hơn", value: " > '#' " }, { name: "Nhỏ hơn", value: " < '#' " }, { name: "Bằng", value: " = '#' " }, { name: "Khác", value: " != '#' " }];
            $scope.typeFilterB = [{ name: "Có chứa", value: " like N'%#%' " }, { name: "Bằng", value: " = N'#' " }, { name: "Không chứa", value: " != N'#' " }];
            $scope.emailValid = /^[a-z]+[a-z0-9._]+@[a-z]+\.[a-z.]{2,5}$/;

            $scope.currentLanguage = currentLanguage;
            $scope.bienloc = "";
            $scope.isEdit = false;


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

            $scope.getTableInfo();

            $scope.getColumns = function () {
                var getData = myService.GetColumns(tableUrl);

                getData.then(function (emp) {

                    $scope.Columns = emp.data.result;
                    $scope.dodai = $scope.Columns.length + 1;
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
                    GetFullName();
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
            //-----------------Filter-----------
            $scope.getFilterColumns = function () {
                var filter = myService.getFilterColumns($scope.tableInfo.id);
                filter.then(function (res) {
                    HiddenLoader();
                    $scope.FilterColumnsItem = res.data.result;
                    $scope.isShowFilter = true;
                    $scope.tablePermission.isFilterButton = false;
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
                    var tmpFilter = obj.filterSelected.ColumnName + obj.typeFilterSelected.value.replace("#", obj.filterSelected.Type === 3 ? $filter('date')(new Date(obj.textValue), 'yyyy/MM/dd') : obj.textValue) + (parseInt(String(key)) === len ? " " : obj.typeEndsSeleted.value);
                    stringFilter += tmpFilter;

                }

                if ($scope.bienloc != "" && stringFilter != "" && $scope.bienloc!=null) {
                    stringFilter += "and c.ParentID = " + $scope.bienloc;
                }
                else if ($scope.bienloc != "" && $scope.bienloc != null) {
                    stringFilter += "c.ParentID = " + $scope.bienloc;
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
            //-----------------Loc-Tree------------
            $scope.FilterOrganizationUnitByNodeId = function () {
                if ($scope.organizationUnitTree.currentNode.id != undefined) {
                    $scope.bienloc = $scope.organizationUnitTree.currentNode.id;
                    $scope.GetListData();
                }
            }
            //-------------------------------------
            // -----------------Edit------------
            $scope.editClick = function (contentItem) {
                $scope.bienloc = null;
                $scope.chitietshow = false;
                $scope.is_TenPBSpecial = false;
                $scope.is_add = false;
                $scope.isEdit = true;
                $scope.check_click = false;
                ListStatus();
                $scope.ListParent();
                GetFullName();
                var edit = myService.getDataById(contentItem.GlobalListID, $scope.tableInfo.id, $scope.tableInfo.DataEditUrl);
                setTimeout(function () {
                    edit.then(function (emp) {
                        $scope.editData = emp.data.result;
                        console.log($scope.editData)
                        if ($scope.editData.ParentID==0) {
                            $scope.editData.model = $scope.editData.GlobalListID;
                            $scope.editData.ParentID = "";
                        }
                        else {
                            CategoryList($scope.editData.ParentID);
                            $scope.editData.model = $scope.editData.ParentID;
                            $scope.editData.ParentID = $scope.editData.GlobalListID;
                        }
                        if (emp.data.result.IsActive != null) {
                            emp.data.result.IsActive = emp.data.result.IsActive ? String(1) : String(0);
                        }
                        $scope.editData.CreatedDate = new Date(emp.data.result.CreatedDate);
                        if (emp.data.result.ModifiedDate != null) {
                            $scope.editData.ModifiedDate = new Date(emp.data.result.ModifiedDate);
                        }
                        else {
                            $scope.editData.ModifiedDate = new Date();
                        }
                        $scope.editData.ModifiedBy1 = $scope.getName;
                        ShowPopup($,
                            "#AddContent",
                            $scope.tableInfo.PopupWidth,
                            $scope.tableInfo.PopupHeight);
                    },
                        function (emp) {
                            AppendToToastr(false, notification, errorNotiFalse);
                        });
                }, 300);
            }
            $scope.chitiet = function (contentItem) {
                $scope.bienloc = null;
                $scope.chitietshow = true;
                $scope.is_TenPBSpecial = false;
                $scope.is_add = false;
                $scope.isEdit = true;
                $scope.check_click = false;
                ListStatus();
                $scope.ListParent();
                GetFullName();
                CategoryList(contentItem.ParentID);
                var edit = myService.getDataById(contentItem.GlobalListID, $scope.tableInfo.id, $scope.tableInfo.DataEditUrl);
                setTimeout(function () {
                    edit.then(function (emp) {
                        $scope.editData = emp.data.result;
                        if ($scope.editData.ParentID == 0) {
                            $scope.editData.model = $scope.editData.GlobalListID;
                            $scope.editData.ParentID = "";
                        }
                        else {
                            CategoryList($scope.editData.ParentID);
                            $scope.editData.model = $scope.editData.ParentID;
                            $scope.editData.ParentID = $scope.editData.GlobalListID;
                        }
                        if (emp.data.result.IsActive != null) {
                            emp.data.result.IsActive = emp.data.result.IsActive ? String(1) : String(0);
                        }
                        $scope.editData.CreatedDate = new Date(emp.data.result.CreatedDate);
                        if (emp.data.result.ModifiedDate != null) {
                            $scope.editData.ModifiedDate = new Date(emp.data.result.ModifiedDate);
                        }
                        else {
                            $scope.editData.ModifiedDate = "";
                        }
                        $scope.editData.ModifiedBy1 = emp.data.result.ModifiedBy1;
                        ShowPopup($,
                            "#AddContent",
                            $scope.tableInfo.PopupWidth,
                            $scope.tableInfo.PopupHeight);
                    },
                        function (emp) {
                            AppendToToastr(false, notification, errorNotiFalse);
                        });
                }, 50);
            }
            // -----------------Edit--End----------
            //Check ký tự đặc biệt
            function CheckTenPBSpecial() {
                $scope.is_TenPBSpecial = false;
                var nick = $scope.editData.Name;
                var splChars = "*|,\":<>[]{}`\';()@&$#%";
                for (var i = 0; i < nick.length; i++) {
                    if (splChars.indexOf(nick.charAt(i)) != -1) {
                        $scope.is_TenPBSpecial = true;
                        $scope.check = 1;
                    }
                }
            }
            //------------------Add---------------
            $scope.addClick = function () {
                $scope.editData = {};
                $scope.is_TenPBSpecial = false;
                $scope.is_add = true;
                $scope.isEdit = false;
                $scope.check_click = true;
                ListStatus();
                GetFullName();
                $scope.ListParent();
                $scope.getCategoryList = null;
                $scope.editData = {
                    CreatedBy1: $scope.getName,
                    CreatedDate: new Date()
                };
                if ($scope.bienloc != null && $scope.bienloc!=0) {
                    $scope.editData.model = $scope.bienloc;
                }
                ShowPopup($,
                    "#AddContent",
                    $scope.tableInfo.PopupWidth,
                    $scope.tableInfo.PopupHeight);
            }
            //---------truyền biến rootscope-----
            $rootScope.ParentID = null;

            $scope.onParantChange = function () {
                $scope.check_click = false;
                CategoryList($scope.editData.model);
            }
            //----------end rootscope------------
            $scope.SaveAction = function (url, form) {
                if (form.$valid) {
                    CheckTenPBSpecial();
                    if ($scope.is_TenPBSpecial == true) {
                    }
                    else {
                        if ($scope.isEdit == false) {
                            $scope.editData.IsActive = $scope.editData.IsActive == 1 ? true : false;
                            $scope.editData.ParentID = $scope.editData.model;
                            var updateAction = myService.UpdateData(url, $scope.editData);
                            updateAction.then(function (res) {
                                form.$dirty = false;
                                form.$invalid = false;
                                form.$submitted = false;
                                form.$valid = false;
                                if (res.data.result.IsSuccess) {
                                    $scope.GetListData();
                                    ShowPopup($,
                                        "#SaveContent",
                                        $scope.tableInfo.PopupWidth,
                                        $scope.tableInfo.PopupHeight);
                                    $.colorbox.close();
                                }
                                AppendToToastr(res.data.result.IsSuccess, notification, res.data.result.Message, 500, 5000);
                            },
                                function (res) {
                                    AppendToToastr(false, notification, errorNotiFalse);

                                });
                        }
                        else {
                            $scope.editData.IsActive = $scope.editData.IsActive == 1 ? true : false;
                            if ($scope.editData.ParentID == "" && $scope.editData.ParentID != null) {
                                $scope.editData.GlobalListID = $scope.editData.model;
                                $scope.editData.ParentID = 0;
                            }
                            else {
                                $scope.editData.GlobalListID = $scope.editData.ParentID;
                                $scope.editData.ParentID = $scope.editData.model;
                            }
                            var updateAction = myService.UpdateData(url, $scope.editData);
                            updateAction.then(function (res) {
                                form.$dirty = false;
                                form.$invalid = false;
                                form.$submitted = false;
                                form.$valid = false;
                                if (res.data.result.IsSuccess) {
                                    $.colorbox.close();
                                    $scope.GetListData();

                                }
                                AppendToToastr(res.data.result.IsSuccess, notification, res.data.result.Message, 500, 5000);
                            },
                                function (res) {
                                    AppendToToastr(false, notification, errorNotiFalse);
                                    $scope.GetListData();

                                });
                        }
                    }
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

                var action = myService.deleteAction(obj.GlobalListID, $scope.tableInfo.id, $scope.tableInfo.DeleteUrl);

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

            $scope.CloseForm = function (form) {
                form.$dirty = false;
                form.$invalid = false;
                form.$submitted = false;
                form.$valid = false;
                $scope.editData = {};                
                $.colorbox.close();
            }
            function convert(array) {
                var map = {}
                for (var i = 0; i < array.length; i++) {
                    var obj = array[i]
                    if (!(obj.GlobalListID in map)) {
                        map[obj.GlobalListID] = obj
                        map[obj.GlobalListID].children = []
                    }

                    if (typeof map[obj.GlobalListID].Name == 'undefined') {
                        map[obj.GlobalListID].GlobalListID = obj.GlobalListID
                        map[obj.GlobalListID].Name = obj.Name
                        map[obj.GlobalListID].ParentID = obj.ParentID
                    }

                    var parent = obj.ParentID || '-';
                    if (!(parent in map)) {
                        map[parent] = {}
                        map[parent].children = []
                    }

                    map[parent].children.push(map[obj.GlobalListID])
                }
                return map['-']
            }
            //------------------Get-Parent------
            $scope.organizationUnitTree = {};
            $scope.sourceOrganizationUnit = [];
            $scope.ListParent=function() {
                var list = myService.getListParent();
                list.then(function (res) {
                    $scope.sourceOrganizationUnit = res.data;
                }, function (res) {
                    $scope.msg = "Error";
                })
            }
            $scope.ListParent();
            //------------------Get-Parent-End--
            //----------------Get-FullName-By-UserID----
            function GetFullName() {
                var list = myService.getFullName();
                list.then(function (res) {
                    $scope.getName = res.data.result.FullName;
                }, function (res) {
                    $scope.msg = "Error";
                })
            }
            //------------------------------------------
            //----------------Get-CategoryList--------
            function CategoryList(param) {
                var list = myService.getCategoryList(param);
                list.then(function (res) {
                    $scope.getCategoryList = res.data.result;
                }, function (res) {
                    $scope.msg = "Error";
                })
            }

            //---------------Get-CategoryList-End-----


            //------------Get Status-------------
            function ListStatus() {
                var list = myService.getListStatus();
                list.then(function (res) {
                    $scope.getListStatus = res.data.result;

                }, function (res) {
                    $scope.msg = "Error";
                })
            }
            //------------Get Status End---------



            // -----------------Xóa--End------------
            //-------------------Excel--------------
            $scope.ExcelClick = function () {
                var filterString = $scope.getFilterValue();
                window.location = $scope.tableInfo.ExcelUrl + "?filterString=" + filterString;

            }
            //-------------------Excel-End----------
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
    app.directive('compile', ['$compile', function ($compile) {

        return function (scope, element, attrs) {
            scope.$watch(
                function (scope) {
                    // watch the 'compile' expression for changes
                    return scope.$eval(attrs.compile);
                },
                function (value) {
                    // when the 'compile' expression changes
                    // assign it into the current DOM
                    element.html(value);

                    // compile the new DOM and link it to the current
                    // scope.
                    // NOTE: we only compile .childNodes so that
                    // we don't get into infinite loop compiling ourselves
                    $compile(element.contents())(scope);
                }
            );
        };
    }]);
    app.directive("formatDate",
        function () {
            return {
                require: 'ngModel',
                link: function (scope, elem, attr, modelCtrl) {
                    modelCtrl.$formatters.push(function (modelValue) {

                        return new Date(modelValue);
                    });
                }
            }
        });
    app.directive("selectpicker",
        [
            "$timeout",
            function ($timeout) {
                return {
                    restrict: "A",
                    require: ["?ngModel", "?collectionName"],
                    compile: function (tElement, tAttrs, transclude) {
                        console.log("init bootstrap-select");
                        tElement.selectpicker();

                        if (angular.isUndefined(tAttrs.ngModel)) {
                            throw new Error("Please add ng-model attribute!");
                        } else if (angular.isUndefined(tAttrs.collectionName)) {
                            throw new Error("Please add data-collection-name attribute!");
                        }

                        return function (scope, element, attrs, ngModel) {
                            if (angular.isUndefined(ngModel)) {
                                return;
                            }

                            scope.$watch(attrs.ngModel, function (newVal, oldVal) {
                                if (newVal !== oldVal) {
                                    $timeout(function () {
                                        console.log("value selected");
                                        element.selectpicker("val", element.val());
                                    });
                                }
                            });

                            scope.$watch(attrs.collectionName, function (newVal, oldVal) {
                                $timeout(function () {
                                    console.log("select collection updated");
                                    element.selectpicker("refresh");
                                });
                            });

                            ngModel.$render = function () {
                                element.selectpicker("val", ngModel.$viewValue || "");
                            };

                            ngModel.$viewValue = element.val();
                        };
                    }
                }
            }
        ]
    );
}
