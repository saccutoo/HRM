function BuildTable(appName, controllerName, tableUrl) {
    app.controller(controllerName,
        function ($scope, myService, $filter, $rootScope) {
            // $scope.test = $filter('mm/dd/yyyy')("2018/01/01", 'yyyy/MM/dd');
            $scope.maxSize = 5; // Limit number for pagination display number.
            $scope.totalCount = 0; // Total number of items in all pages. initialize as a zero
            $scope.pageIndex = 1; // Current page number. First page is 1.-->
            $scope.pageSizeSelected = 5;
            $scope.isShowFilter = false;
            $scope.typeEnds = [{ name: "Và", nameEN: "And", value: " and " }, { name: "Hoặc", nameEN: "Or", value: " or " }];
            $scope.typeFilterA = [{ name: ">", nameEN: ">", value: " > '#' " }, { name: "<", nameEN: "<", value: " < '#' " }, { name: "=", nameEN: "=", value: " = '#' " }, { name: "Khác", nameEN: "Not equal", value: " != '#' " }];
            $scope.typeFilterB = [{ name: "Có chứa", nameEN: "Contains", value: " like '%#%' " }, { name: "Bằng", nameEN: "Is", value: " = '#' " }, { name: "Không chứa", nameEN: "Do not contains", value: " != '#' " }];
            $scope.filterColumnsChoosed = [];

            $scope.emailValid = /^[a-z]+[a-z0-9._]+@[a-z]+\.[a-z.]{2,5}$/;

            $scope.treeName = 'sampleTree';
            $rootScope.data1 = [];
            $scope.is_Show = false;

            $scope.results = '1';
            $scope.checkRole = false;

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
                    pageSize: 500,
                    filter: $scope.getFilterValue()
                }
                var getDataTbl = myService.GetTableData(data, tableUrl);
                getDataTbl.then(function (emp) {
                    $scope.employees = emp.data.employees;
                    sampleData = emp.data.employees;
                    if (sampleData[0].ParentID != null) {
                        extractData();
                        $("#treeview").kendoTreeView({
                            checkboxes: {
                                checkChildren: true
                            },
                            check: onCheck,
                            dataSource: $rootScope.data1
                        });
                        // function that gathers IDs of checked nodes
                        checkedNodeIds = function (nodes, checkedNodes) {
                            for (var i = 0; i < nodes.length; i++) {
                                if (nodes[i].checked) {
                                    checkedNodes.push(nodes[i].id);
                                }
                                if (nodes[i].hasChildren) {
                                    checkedNodeIds(nodes[i].children.view(), checkedNodes);
                                }
                            }
                        }
                        // show checked node IDs on datasource change
                        function onCheck() {
                            var checkedNodes = [],
                                treeView = $("#treeview").data("kendoTreeView"),
                                message;
                            checkedNodeIds(treeView.dataSource.view(), checkedNodes);

                            $rootScope.idrolre1 = checkedNodes;
                        }
                    }
                    $scope.totalCount = emp.data.totalCount;
                    $scope.lstTotal = emp.data.lstTotal;
                    $scope.SetTotalByColumns = function (totalName) {
                        if (!angular.isUndefined(totalName) && totalName !== null) {
                            return $scope.lstTotal[totalName];
                        }
                        return "-";
                    };
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
                    var tmpFilter = obj.filterSelected.ColumnName + obj.typeFilterSelected.value.replace("#", obj.filterSelected.Type === 3 ? $filter('date')(new Date(obj.textValue), 'yyyy/MM/dd') : obj.textValue) + (parseInt(String(key)) === len ? "" : obj.typeEndsSeleted.value);
                    stringFilter += tmpFilter;
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

            // -----------------Edit-------------------
            $scope.editClick = function (contentItem) {
                if ($rootScope.idrolre == null) {
                    $rootScope.idrolre = [];
                }
                var j = 0;
                if ($rootScope.idrolre.length == 0) {
                    $rootScope.idrolre.push(contentItem.RoleID);
                }
                else {
                    for (var i = 0; i < $rootScope.idrolre.length; i++) {
                        if ($rootScope.idrolre[i] == contentItem.RoleID) {
                            $rootScope.idrolre.splice(i, 1);
                            j = 1;
                        }
                    }
                    if (j == 0) {
                        $rootScope.idrolre.push(contentItem.RoleID);
                    }
                }
            }
            //---------------Chose-ShoW-Treeview--------
            $scope.choseClick = function (contentItem) {
                $scope.showPane();
                $rootScope.data1 = [];
                extractData();
                if (document.getElementById(contentItem.RoleID).checked == false) {
                    document.getElementById(contentItem.RoleID).checked = true;
                }
                else {
                    document.getElementById(contentItem.RoleID).checked = false;
                }
                $scope.editClick(contentItem);
                var edit = myService.getDataById(contentItem.RoleID, 11, '/SecRoleMenu/GetMenuByIDRole');
                for (var j = 0; j < $rootScope.data1.length; j++) {
                    $rootScope.data1[j].checked = null;
                }
                edit.then(function (emp) {
                    $rootScope.temp = [];
                    for (var i = 0; i < emp.data.result.length; i++) {
                        $rootScope.temp.push(emp.data.result[i].MenuID);
                    }
                    for (var i = 0; i < $rootScope.temp.length; i++) {
                        var x = 0;
                        for (j in $rootScope.data1) {//node_0
                            x = $rootScope.data1[j].id;
                            if ($rootScope.temp[i] == x) {
                                $rootScope.data1[j].checked = true;
                                for (k in $rootScope.data1[j].items) {//node_1
                                    $rootScope.data1[j].items[k].checked = true;
                                    for (l in $rootScope.data1[j].items[k].items) {//node_2
                                        $rootScope.data1[j].items[k].items[l].checked = true;
                                        for (m in $rootScope.data1[j].items[k].items[l].items) {//node_3
                                            $rootScope.data1[j].items[k].items[l].items[m].checked = true;
                                            for (n in $rootScope.data1[j].items[k].items[l].items[m].items) {//node_4
                                                $rootScope.data1[j].items[k].items[l].items[m].items[n].checked = true;
                                                for (o in $rootScope.data1[j].items[k].items[l].items[m].items[n].items) {//node_5
                                                    $rootScope.data1[j].items[k].items[l].items[m].items[n].items[o].checked = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else {
                                for (k in $rootScope.data1[j].items) {//node_1
                                    x = $rootScope.data1[j].items[k].id;
                                    if ($rootScope.temp[i] == x) {
                                        $rootScope.data1[j].items[k].checked = true;
                                        for (l in $rootScope.data1[j].items[k].items) {//node_2
                                            $rootScope.data1[j].items[k].items[l].checked = true;
                                            for (m in $rootScope.data1[j].items[k].items[l].items) {//node_3
                                                $rootScope.data1[j].items[k].items[l].items[m].checked = true;
                                                for (n in $rootScope.data1[j].items[k].items[l].items[m].items) {//node_4
                                                    $rootScope.data1[j].items[k].items[l].items[m].items[n].checked = true;
                                                    for (o in $rootScope.data1[j].items[k].items[l].items[m].items[n].items) {//node_5
                                                        $rootScope.data1[j].items[k].items[l].items[m].items[n].items[o].checked = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else {
                                        for (l in $rootScope.data1[j].items[k].items) {//node_2
                                            x = $rootScope.data1[j].items[k].items[l].id;
                                            if ($rootScope.temp[i] == x) {
                                                $rootScope.data1[j].items[k].items[l].checked = true;
                                                for (m in $rootScope.data1[j].items[k].items[l].items) {//node_3
                                                    $rootScope.data1[j].items[k].items[l].items[m].checked = true;
                                                    for (n in $rootScope.data1[j].items[k].items[l].items[m].items) {//node_4
                                                        $rootScope.data1[j].items[k].items[l].items[m].items[n].checked = true;
                                                        for (o in $rootScope.data1[j].items[k].items[l].items[m].items[n].items) {//node_5
                                                            $rootScope.data1[j].items[k].items[l].items[m].items[n].items[o].checked = true;
                                                        }
                                                    }
                                                }
                                            }
                                            else {
                                                for (m in $rootScope.data1[j].items[k].items[l].items) {//node_3
                                                    x = $rootScope.data1[j].items[k].items[l].items[m].id;
                                                    if ($rootScope.temp[i] == x) {
                                                        $rootScope.data1[j].items[k].items[l].items[m].checked = true;
                                                        for (n in $rootScope.data1[j].items[k].items[l].items[m].items) {//node_4
                                                            $rootScope.data1[j].items[k].items[l].items[m].items[n].checked = true;
                                                            for (o in $rootScope.data1[j].items[k].items[l].items[m].items[n].items) {//node_5
                                                                $rootScope.data1[j].items[k].items[l].items[m].items[n].items[o].checked = true;
                                                            }
                                                        }
                                                    }
                                                    else {
                                                        for (n in $rootScope.data1[j].items[k].items[l].items[m].items) {//node_4
                                                            x = $rootScope.data1[j].items[k].items[l].items[m].items[n].id;
                                                            if ($rootScope.temp[i] == x) {
                                                                $rootScope.data1[j].items[k].items[l].items[m].items[n].checked = true;
                                                                for (o in $rootScope.data1[j].items[k].items[l].items[m].items[n].items) {//node_5
                                                                    $rootScope.data1[j].items[k].items[l].items[m].items[n].items[o].checked = true;
                                                                }
                                                            }
                                                            else {
                                                                for (o in $rootScope.data1[j].items[k].items[l].items[m].items[n].items) {//node_5
                                                                    x = $rootScope.data1[j].items[k].items[l].items[m].items[n].items[o].id;
                                                                    if ($rootScope.temp[i] == x) {
                                                                        $rootScope.data1[j].items[k].items[l].items[m].items[n].items[o].checked = true;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    $scope.hidePane();
                    var parent = document.getElementById("demo");
                    var child = document.getElementById("treeview");
                    parent.removeChild(child);
                    $('<div id="treeview"></div>').appendTo('div.demo1');
                    $("#treeview").kendoTreeView({
                        checkboxes: {
                            checkChildren: true
                        },
                        check: onCheck,
                        dataSource: $rootScope.data1
                    });
                    // function that gathers IDs of checked nodes
                    checkedNodeIds = function (nodes, checkedNodes) {
                        for (var i = 0; i < nodes.length; i++) {
                            if (nodes[i].checked) {
                                checkedNodes.push(nodes[i].id);
                            }
                            if (nodes[i].hasChildren) {
                                checkedNodeIds(nodes[i].children.view(), checkedNodes);
                            }
                        }
                    }
                    // show checked node IDs on datasource change
                    function onCheck() {
                        var checkedNodes = [],
                            treeView = $("#treeview").data("kendoTreeView"),
                            message;
                        checkedNodeIds(treeView.dataSource.view(), checkedNodes);

                        $rootScope.idrolre1 = checkedNodes;
                    }
                });
            }
            //Load dữ liệu
            $scope.showPane = function () {
                document.getElementsByClassName('loading-pane')[0].style.display = '';
            };
            $scope.hidePane = function () {
                document.getElementsByClassName('loading-pane')[0].style.display = 'none';
            };
            //-----------------Update------------------
            $scope.UpdateClick = function () {
                if ($rootScope.idrolre != null && $rootScope.idrolre1 != null) {
                    for (var i = 0; i < $rootScope.idrolre.length; i++) {
                        for (var j = 0; j < $rootScope.idrolre1.length; j++) {
                            var id1 = $rootScope.idrolre[i];
                            var id2 = $rootScope.idrolre1[j];
                            $scope.editData = { id1, id2};
                            myService.UpdateData('/SecRoleMenu/_AddSecRoleMenu', $scope.editData);
                        }
                    }
                    AppendToToastr(true, notification, setRoleSuccessful, 500, 5000);
                }
                else {
                    AppendToToastr(false, notification, mustBeChooseUserAndMenu, 500, 5000);
                }
            }

            //----------Data---------------
            var extractData = function () {
                var ii = 0;
                for (ii ; ii < sampleData.length; ii++) {
                    if (sampleData[ii].ParentID === 0)
                        $rootScope.data1.push(getItem(ii));
                }
            }
            var getItem = function (index) {
                var item = {
                    id: sampleData[index].MenuID,
                    pid: sampleData[index].ParentID,
                    text: sampleData[index].Name,
                    checked: null,
                    hasChildren: sampleData[index].HasChild
                }
                if (sampleData[index].HasChild === true) {
                    item.expanded = true;
                    item.items = [];
                    var childItems = [];
                    for (var i = 0; i < sampleData.length; i++) {
                        if (sampleData[i].ParentID === item.id) {
                            childItems.push(getItem(i));
                        }
                    }
                    item.items = childItems;
                }
                return item;
            }
            //----------Show-popup----------
            $('#treeview').mousedown(function (event) {
                var treeview = $("#treeview").data("kendoTreeView");
                if (event.which == 3) {
                    if (confirm(clickOkOrCancelTreeview)) {
                        treeview.expand(".k-item");
                    } else {
                        treeview.collapse(".k-item");
                    }
                }
            });
        });
    app.directive('loadingPane', function ($timeout, $window) {
        return {
            restrict: 'A',
            link: function (scope, element, attr) {
                var directiveId = 'loadingPane';

                var targetElement;
                var paneElement;
                var throttledPosition;

                function init(element) {
                    targetElement = element;

                    paneElement = angular.element('<div>');
                    paneElement.addClass('loading-pane');

                    if (attr['id']) {
                        paneElement.attr('data-target-id', attr['id']);
                    }

                    var spinnerImage = angular.element('<div>');
                    spinnerImage.addClass('spinner-image');
                    spinnerImage.appendTo(paneElement);

                    angular.element('body').append(paneElement);

                    setZIndex();

                    //reposition window after a while, just in case if:
                    // - watched scope property will be set to true from the beginning
                    // - and initial position of the target element will be shifted during page rendering
                    $timeout(position, 100);
                    $timeout(position, 200);
                    $timeout(position, 300);


                    angular.element($window).scroll(throttledPosition);
                    angular.element($window).resize(throttledPosition);
                }

                function updateVisibility(isVisible) {
                    if (isVisible) {
                        show();
                    } else {
                        hide();
                    }
                }

                function setZIndex() {
                    var paneZIndex = 10000;

                    paneElement.css('zIndex', paneZIndex).find('.spinner-image').css('zIndex', paneZIndex + 1);
                }

                function position() {
                    paneElement.css({
                        'left': targetElement.offset().left,
                        'top': targetElement.offset().top - $(window).scrollTop(),
                        'width': targetElement.outerWidth(),
                        'height': '90%'
                    });
                }

                function show() {
                    paneElement.show();
                    position();
                }

                function hide() {
                    paneElement.hide();
                }

                init(element);

                scope.$watch(attr[directiveId], function (newVal) {
                    updateVisibility(newVal);
                });

                scope.$on('$destroy', function cleanup() {
                    paneElement.remove();
                    $(window).off('scroll', throttledPosition);
                    $(window).off('resize', throttledPosition);
                });
            }
        };
    });
}