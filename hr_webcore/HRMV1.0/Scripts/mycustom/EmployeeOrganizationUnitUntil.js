function BuildTable(appName, controllerName, tableUrl, Notification,Error) {
    app.controller(controllerName,
        function ($scope, myService, $filter, $rootScope, $compile) {
            $scope.maxSize = 5; // Limit number for pagination display number.
            $scope.totalCount = 0; // Total number of items in all pages. initial
            $scope.pageIndex = 1; // Current page number. First page is 1.-->
            $scope.pageSizeSelected = 5;
            $scope.isShowFilter = false; $scope.typeEnds = [{ name: "Và", nameEN: "And", value: " and " }, { name: "Hoặc", nameEN: "Or", value: " or " }];
            $scope.typeFilterA = [{ name: ">", nameEN: ">", value: " > '#' " }, { name: "<", nameEN: "<", value: " < '#' " }, { name: "=", nameEN: "=", value: " = '#' " }, { name: "Khác", nameEN: "Not equal", value: " != '#' " }];
            $scope.typeFilterB = [{ name: "Có chứa", nameEN: "Contains", value: " like '%#%' " }, { name: "Bằng", nameEN: "Is", value: " = '#' " }, { name: "Không chứa", nameEN: "Do not contains", value: " != '#' " }];
            $scope.filterColumnsChoosed = [];
            $scope.emailValid = /^[a-z]+[a-z0-9._]+@[a-z]+\.[a-z.]{2,5}$/;
            $scope.treeName = 'sampleTree';
            $rootScope.data1 = [];
            $rootScope.idchose = "";
            $scope.tab2Data = '';
            $scope.flagShow = 'tabs2';
            $scope.currentRoleId = currentRoleId;

  
            $scope.$on("fileProgress", function (e, progress) {
                $scope.progress = progress.loaded / progress.total;
            });
            //truyen staffID to working proces
            $scope.wpFilter = {
                staffID: null
            }
            //model đượct truyền ra từ directive build table
            $scope.employeeData = {};

            //Global theo parentID
            $scope.GlobalListWhereParentID = {
                GenderID: 30,
                Status: 55,
                CountryID: 44,
                Currency: 3, //dùng chung các bảng WorkingProcess và EmployeeBonus_Discipline
                ContractType: 2123
            }

            $scope.CloseForm = function () {
                $.colorbox.close();

            } //Nút bỏ qua Employee
            $scope.tennv = "";

            $scope.init = function () {
                ListRoleID(); 
                $scope.GetListDepartment();
                ListCurrency(); //Loại tiền tệ gọi chung
                ListHRIds(); //hr quản lý và hr chốt công
             
            }

            //ckfinder upload images
            $scope.ChooseImage = function () {
                var finder = new CKFinder();
                finder.selectActionFunction = function (fileUrl) {
                    $scope.$apply($scope.editData.imageLink = fileUrl);
                }
                finder.popup({ width: 800, height: 400 });
            }

            $scope.employeeData = {};
            $scope.sourceDepartment = [];
            $scope.department = {};

            $scope.ChangeEmployeeData = function () {
                $scope.stringFilterValue = $scope.getFilterValue();
                //reload data by default + exten
                $scope.employeeData.reloadByFilter($scope.stringFilterValue);
                $scope.GetListData();
             
            }
             $scope.GetListDepartment = function () {
                //GetListDepartment
                var list = myService.getListDepartment();
                list.then(function (res) {
                    $scope.sourceDepartment = res.data;
                }, function (res) {
                    $scope.msg = "Error";
                });
             }



            //----------------LoadTab---------
            $scope.tabs = {
           
                tabs2: {
                    url: '/WorkingProcess/Index/',
                    container: $("#tabs2")
                },
                tabs3: {
                    url: '/SocialInsuranceDetail/Index/',
                    container: $("#tabs3")
                },
                tabs4: {
                    url: '/EmployeeBonus_Discipline/Index/',
                    container: $("#tabs4")
                },
                tabs5: {
                    url: '/EmployeeRelationships/Index/',
                    container: $("#tabs5")
                }
            };

            $scope.LoadTab = function (idTab) {
             
                var tab = $scope.tabs[idTab];
                $scope.flagShow = idTab;
                if (idTab && tab && tab.url && tab.container)
                    LoadPartialView(tab.url + $scope.StaffID, tab.container);
               
            }


            function GetIdActiveTab() {
                var current_tab = $('#tabs .ui-tabs-panel:eq(' +
                    $('#tabs').tabs('option', 'active') + ')').attr('id');
                $scope.LoadTab(current_tab);
            }

            function LoadPartialView(url, container, data) {
                var dt = Loading();

                $scope.tab2Data = "";
                $scope.tab3Data = "";
                $scope.tab4Data = "";
                $scope.tab5Data = "";
                var urlParts = url.split('/');
                var urlCheck = '/' + urlParts[1] + '/' + urlParts[2] + '/';

                $.ajax({
                    url: url,
                    data: data,
                    type: "POST",
                    dataType: "html",
                    success: function (result) {
                        var htmlResult = $compile(result) ($scope);
                        //$scope.tab2Data = result;
                        switch (urlCheck) {
                            case '/WorkingProcess/Index/':
                                $scope.tab2Data = result;
                                break;
                            case '/SocialInsuranceDetail/Index/':
                                $scope.tab3Data = result;
                                break;
                            case '/EmployeeBonus_Discipline/Index/':
                                $scope.tab4Data = result;
                                break;
                            case '/EmployeeRelationships/Index/':
                                $scope.tab5Data = result;
                                break;
                            default:
                                // code block
                        }
                        //container.html(htmlResult);
                        // $scope.$broadcast('bind-working', container, htmlResult);
                    },
                    complete: function () {
                        dt.finish();
                        //HiddenLoader();
                    },
                    beforeSend: function () {
                        //ShowLoader();
                    }
                });

            }

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
                        AppendToToastr(false, Notification, Error);
                    });
            }

            $scope.GetAddPermission = function (idTable) {
                var tblAction = myService.getTableAction(idTable);
                tblAction.then(function (emp) {
                   
                    $scope.tablePermission = emp.data.result;
                    if ($scope.tablePermission != null) {
                        if ($scope.tablePermission.isEdit == false) {
                            $scope.is_readonly = true;
                        }
                        else {
                            $scope.is_readonly = false;
                        }
                    }
                    //$scope.getColumns();
                    // $scope.BuildAddButton(emp.data.result);
                },
                    function (emp) {
                        AppendToToastr(false, Notification, Error);
                    });
            }
            $scope.getTableInfo();
            $scope.getColumns = function () {
                var getData = myService.GetColumns(tableUrl);
                getData.then(function (emp) {
                    $scope.Columns = emp.data.result;
                    //$scope.GetListData();
                },
                    function (emp) {
                        AppendToToastr(false, Notification, Error);
                    });
            }

            $scope.GetListData = function () {
                var dt = Loading();
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
                    $scope.HRIdLogin = emp.data.hrID.toString();
                    $rootScope.RoleID = emp.data.roleID;

                    $rootScope.PositionID = emp.data.positionID;
                    $scope.SetTotalByColumns = function (totalName) {
                        if (!angular.isUndefined(totalName) && totalName !== null) {
                            return $scope.lstTotal[totalName];
                        }
                        return "-";
                    };
                    dt.finish();
                },
                    function (emp) {
                        AppendToToastr(false, Notification, Error);
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

            //---------------------------------------------------------------------------- List-End ------------------------------------------------------------------------------------




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
                        AppendToToastr(false, Notification, Error);
                    });
            }
            $scope.cancelFilterClick = function () {
                $scope.filterColumnsChoosed = [];
                $scope.isShowFilter = false;
                $scope.tablePermission.isFilterButton = true;
                $scope.GetListData();
            };
            $scope.findNV = function () {
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
                if ($scope.department.currentNode) {
                    //stringFilter += " and ";
                    stringFilter += " p.DeptChild like '%," + $scope.department.currentNode.id + ",%'";

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





            //--------------------------------------------------------------------------- Filter-End -------------------------------------------------------------------------------------

            $scope.CallData = function () {
                ListOrganizationUnit(); //phòng ban
                ListStaff();            //người quản lý hiện tại
                ListWorkingDayMachine();//máy chấm công
                
                ListGerder();           //giới tính
                ListRoleID();           //nhóm quyền
                ListStatus();           //trạng thái làm việc
                ListStatusContract();   //trạng thái hợp đồng
                ListCountry();          //quốc gia
            }

            //---------Lấy quốc gia theo tỉnh thành-----
            $scope.onCountryChange = function () {
                ListProvinceByCountry($scope.editData.CountryID);
            }
            $scope.onContactCountryChange = function () {
                ListContactProvinceByCountry($scope.editData.ContactCountryID);
            }

            //--------------------end------------

            $scope.StaffID = null;

            // -----------------Edit Add Click Employee------------

            $scope.editClick = function (contentItem) {
                var dt = Loading();
                $scope.check = 1;
                $scope.ShowTab = true;
                $rootScope.RoleByStaffID = contentItem.RoleID;
                $scope.requied = false;
                $scope.disabled = true; //ẩn input
                $scope.StaffID = contentItem.UserID;
                $rootScope.StaffID = contentItem.UserID;
                $scope.wpFilter.staffID = contentItem.UserID;
                GetIdActiveTab();
                var edit = myService.getDataById(contentItem.UserID, $scope.tableInfo.id, $scope.tableInfo.DataEditUrl);
                edit.then(function (emp) {
                    $scope.CallData();
                    $scope.editData = emp.data.result;
                  
                    if ($scope.editData.WorkingDayMachineID == 0) {
                        $scope.editData.WorkingDayMachineID = 1;
                    }
                    $scope.editData.Ds_StatusContractID = emp.data.result.StatusContractID;
                    if ($scope.editData.HRIds != null) {
                        $scope.editData.HRIds = emp.data.result.HRIds.split(',');
                    }
                    $scope.onCountryChange();
                    $scope.onContactCountryChange();
                    dt.finish();
                    ShowPopup($,
                        "#SaveContent",
                        $scope.tableInfo.PopupWidth,
                        $scope.tableInfo.PopupHeight);
                },
                    function (emp) {
                        AppendToToastr(false, Notification, Error);
                        dt.finish();

                    });
            }


            
            $scope.addClick = function () {
                var index = $('#tabs a[href="#tabs1"]').parent().index();
                $('#tabs').tabs({ active: index  });
                $scope.check = 0;
                $scope.ShowTab = false;
                $scope.disabled = false; //hiện input
                $scope.requied = true;
                $scope.CallData();
               
                $scope.editData = {
                    Status: 879,
                    OfficePositionID: 1,
                    //ParentID: 42,
                    imageLink: "https://i.vimeocdn.com/portrait/1274237_300x300",
                    StartWorkingDate: new Date(),
                    OrganizationUnitID: 1,
                    HRIds: ["" + $scope.HRIdLogin + ""]
                };
                if (commomRolId != null && (commomRolId == 10 || commomRolId == 25)) {
                    $scope.editData.HRIds= ["" + commomUserID + ""]
                } else {
                    $scope.editData.HRIds = [];
                }
                ShowPopup($,
                    "#SaveContent",
                    $scope.tableInfo.PopupWidth,
                    $scope.tableInfo.PopupHeight);
              
               
            }

            // -----------------End Edit Add Click--------------

            //--------------Save Employee----------------------
            $scope.SaveAction = function (url, form) {
                if (!IsValidDate($scope.editData.BirthDay, date_Birth)) return;
                if (!IsValidDate($scope.editData.StartWorkingDate, dateStartWork)) return;
                if (!IsValidDate($scope.editData.TrialDate, trialDate)) return;
                if (!IsValidDate($scope.editData.TaxDate, taxCodeIssueDate)) return;
                if (!IsValidDate($scope.editData.LastChildBirthday, lastChildBirthday)) return;
                if (!IsValidDate($scope.editData.EndWorkingDate, endWorkingsDate)) return;
                if (!IsValidDate($scope.editData.IDIssuedDate, dateIdentityCard)) return;
                if (form.$valid) {                    
                    if ($scope.editData.HRIds != null && $scope.editData.HRIds instanceof Array) {
                        $scope.editData.HRIds = $scope.editData.HRIds.join();
                    }
                    if ($scope.editData.WorkingDayMachineID == 1) {
                        $scope.editData.WorkingDayMachineID = 0;
                    }                   

                    var updateAction = myService.UpdateData(url, $scope.editData);
                    updateAction.then(function (res) {

                        if (res.data.result.IsSuccess) {
                            $scope.GetListData();
                            $.colorbox.close();
                            $scope.employeeData.reload();
                        }
                        AppendToToastr(res.data.result.IsSuccess, Notification, res.data.result.Message, 500, 5000);
                    },
                        function (res) {
                            AppendToToastr(false, Notification, Error);
                        });
                }
            }
            function IsValidDate(date, fieldName)
            {
                if (((date != null && !moment(date, 'DD/MM/YYYY').isValid()) || (date == undefined)) && !moment(new Date(date)).isValid())
                {
                    AppendToToastr(false, Notification, fieldName + illegal);
                    return false;
                }
                return true;
            }
            //load lại dữ liệu khi save quá trình công tá
            //$rootScope.ReloadEMPWhenSaveWP = function () {
            //    $scope.GetListData();
            //    $scope.employeeData.reload();
            //}
            //------------------Add-Save---------

            // -----------------Xóa------------

            $scope.deleteClick = function (obj) {
                BoostrapDialogConfirm(Notification,
                    notificationDelete,
                    BootstrapDialog.TYPE_WARNING,
                    $scope.deleteActionClick,
                    obj);
            }
            $scope.deleteActionClick = function (obj) {
                var action = myService.deleteAction(obj.StaffID, $scope.tableInfo.id, $scope.tableInfo.DeleteUrl);
                action.then(function (res) {
                    if (res.data.result.IsSuccess) {
                        $scope.GetListData();
                        $scope.employeeData.reload();
                    }
                    AppendToToastr(res.data.result.IsSuccess, Notification, res.data.result.Message, 500, 5000);
                },
                    function (res) {
                        AppendToToastr(false, Notification, Error);
                    });

            }

            // -----------------Xóa--End------------


            //-------------------Excel--------------
            $scope.ExcelClick = function () {
                var filterString = $scope.getFilterValue();
                window.location = $scope.tableInfo.ExcelUrl + "?filterString=" + filterString + "&pageIndex=" + $scope.pageIndex + "&pageSize=" + $scope.pageSizeSelected;
            }
            //-------------------Excel-End----------


            //------------------reload tab1 function-----------
            $scope.reloadTab1 = function (check) {
                if (check == 1) {
                var dt = Loading();
                $scope.editData.WorkingHRID = $scope.editData.HRIDs;
                var edit = myService.getDataById($scope.StaffID, $scope.tableInfo.id, $scope.tableInfo.DataEditUrl);
                edit.then(function (emp) {
                    $scope.CallData();
                    $scope.editData = emp.data.result;
                    $scope.editData.OrganizationUnitID = emp.data.result.OrganizationUnitID;
                    $scope.editData.Ds_StatusContractID = emp.data.result.StatusContractID;
                    if ($scope.editData.HRIds != null) {
                        $scope.editData.HRIds = emp.data.result.HRIds.split(',');
                    }
                    $scope.onCountryChange();
                    $scope.onContactCountryChange();
                    dt.finish();
                },
                    function (emp) {
                        AppendToToastr(false, Notification, Error);
                        });
                } else {
                    
                }
            }
            //------------------end reload tab1 function-----------

            //------------Dropdown trạng thái làm việc-------------
            function ListStatus() {
                var data = {
                    url: "/Common/GetDataByGloballistnotTree?parentid=" + $scope.GlobalListWhereParentID.Status
                }
                var list = myService.getDropdown(data);
                list.then(function(res) {
                        $scope.getListStatus = res.data.result;

                    },
                    function(res) {
                        $scope.msg = "Error";
                    });
            }


            //------------Dropdown trạng thái liên hệ--------
            function ListStatusContract() {
                var data = {
                    url: "/Common/GetDataByGloballistnotTree?parentid=" + $scope.GlobalListWhereParentID.ContractType
                }
                var list = myService.getDropdown(data);
                list.then(function(res) {
                        $scope.getListStatusContract = res.data.result;
                     
                    },
                    function(res) {
                        $scope.msg = "Error";
                    });
            }

            //Dropdown trạng thái nhân viên
            function ListStaff() {
                var data = {
                    url: "/Employee/GetStaff"
                }
                var list = myService.getDropdown(data);
                list.then(function(res) {
                        $scope.getListStaff = res.data.result;
                    },
                    function(res) {
                        $scope.msg = "Error";
                    });
            }

            //Dropdown quốc gia
            function ListCountry() {
                var data = {
                    url: "/Common/GetDataByGloballistnotTree?parentid=" + $scope.GlobalListWhereParentID.CountryID
                }
                var list = myService.getDropdown(data);
                list.then(function(res) {
                        $scope.getListCountry = res.data.result;

                    },
                    function(res) {
                        $scope.msg = "Error";
                    });
            }

            //Dropdown phòng ban
            function ListOrganizationUnit() {
                var data = {
                    url: "/OrganizationUnit/GetOrganizationUnit?chon=1"
                }
                var list = myService.getDropdown(data);
                list.then(function(res) {
                        $scope.getListOrganizationUnit = res.data.result;

                    },
                    function(res) {
                        $scope.msg = "Error";
                    });
            }

            //Employee and Workingprocess - HR quản lý
            function ListHRIds() {
                var data = {
                    url: "/Employee/GetHRIds"
                }
                var list = myService.getDropdown(data);
                list.then(function(res) {
                        $scope.getListHRIds = res.data.result;

                    },
                    function(res) {
                        $scope.msg = "Error";
                    });
            }
            //Employee - roleid
            function ListRoleID() {
                var data = {
                    url: "/Employee/GetRoleID"
                }
                var list = myService.getDropdown(data);
                list.then(function(res) {
                        $scope.getListRoleID = res.data.result;
                        if ($rootScope.RoleID != 1) {
                            delete $scope.getListRoleID[0];
                            delete $scope.getListRoleID[10];
                        }
                    },
                    function(res) {
                        $scope.msg = "Error";
                    });
            }

            //Dropdown giới tính
            function ListGerder() {
                var data = {
                    url: "/Common/GetDataByGloballistnotTree?parentid=" + $scope.GlobalListWhereParentID.GenderID
                }
                var list = myService.getDropdown(data);
                list.then(function(res) {
                        $scope.getListGerder = res.data.result;

                    },
                    function(res) {
                        $scope.msg = "Error";
                    });
            }

            //Dropdown tỉnh thành theo quốc gia
            function ListProvinceByCountry(param) {
                var list = myService.getListProvince(param);
                list.then(function(res) {
                        $scope.getListProvince = res.data.result;
                    },
                    function(res) {
                        $scope.msg = "Error";
                    });
            }

            //Dropdown tỉnh thành theo quốc gia ()
            function ListContactProvinceByCountry(param) {
                var list = myService.getListProvince(param);
                list.then(function(res) {
                        $scope.getListContactProvince = res.data.result;
                    },
                    function(res) {
                        $scope.msg = "Error";
                    });
            }

            //Dropdown Máy chấm công
            function ListWorkingDayMachine() {
                var data = {
                    url: "/Employee/GetWorkingDayMachine"
                }
                var list = myService.getDropdown(data);
                list.then(function(res) {
                    $scope.getListWorkingDayMachine = res.data.result;
                    $scope.getListWorkingDayMachine[0].WorkingDayMachineID = 1;
          
                },
                    function(res) {
                        $scope.msg = "Error";
                    });
            }

            //Workingprocess Loại tiền tệ
            function ListCurrency() {
                var data = {
                    url: "/Common/GetDataByGloballistnotTree?parentid=" + $scope.GlobalListWhereParentID.Currency
                }
                var list = myService.getDropdown(data);
                list.then(function(res) {
                        $scope.getListCurrency = res.data.result;

                    },
                    function(res) {
                        $scope.msg = "Error";
                    });
            }
            //----------------------------------------------------------------------------------------------------------------------------------------------------------------------
           



        });

    app.directive('convertToNumber', function () {
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

    app.directive('convertDropdown', function () {
        return {
            require: 'ngModel',
            link: function (scope, element, attrs, ngModel) {
                ngModel.$parsers.push(function (val) {
                    return val == 0 ? null : val;
                });
                ngModel.$formatters.push(function (val) {
                    return val == 0 ? null : val;
                });
            }
        };
    });
    app.directive("formatDate", function () {
        return {
            require: 'ngModel',
            link: function (scope, elem, attr, modelCtrl) {

                modelCtrl.$formatters.push(function (modelValue) {
                    return modelValue == null ? null : new Date(modelValue);
                });
            }
        }
    });
    app.directive('onlyNumbers', function () {
        return {
            restrict: 'A',
            link: function (scope, elm, attrs, ctrl) {
                elm.on('keydown', function (event) {
                    if (event.shiftKey) { event.preventDefault(); return false; }
                    //console.log(event.which);
                    if ([8, 13, 27, 37, 38, 39, 40].indexOf(event.which) > -1) {
                        // backspace, enter, escape, arrows
                        return true;
                    } else if (event.which >= 49 && event.which <= 57) {
                        // numbers
                        return true;
                    } else if (event.which >= 96 && event.which <= 105) {
                        // numpad number
                        return true;
                    }
                    // else if ([110, 190].indexOf(event.which) > -1) {
                    //     // dot and numpad dot
                    //     return true;
                    // }
                    else {
                        event.preventDefault();
                        return false;
                    }
                });
            }
        }
    });
    app.directive('currencyMask', function () {
        return {
            restrict: 'A',
            require: 'ngModel',
            link: function (scope, element, attrs, ngModelController) {

                var formatNumber = function (value) {

                    value = value.toString();
                    value = value.replace(/[^0-9\.]/g, "");
                    var parts = value.split('.');
                    parts[0] = parts[0].replace(/\d{1,3}(?=(\d{3})+(?!\d))/g, "$&,");
                    if (parts[1] && parts[1].length > 2) {
                        parts[1] = parts[1].substring(0, 2);
                    }

                    return parts.join(".");
                };
                var applyFormatting = function () {
                    var value = element.val();
                    var original = value;
                    if (!value || value.length == 0) {
                        return
                    }
                    value = formatNumber(value);
                    if (value != original) {
                        element.val(value);
                        element.triggerHandler('input')
                    }
                };
                element.bind('keyup', function (e) {
                    var keycode = e.keyCode;
                    var isTextInputKey =
                        (keycode > 47 && keycode < 58) || // number keys
                        keycode == 32 || keycode == 8 || // spacebar or backspace
                        (keycode > 64 && keycode < 91) || // letter keys
                        (keycode > 95 && keycode < 112) || // numpad keys
                        (keycode > 185 && keycode < 193) || // ;=,-./` (in order)
                        (keycode > 218 && keycode < 223); // [\]' (in order)
                    if (isTextInputKey) {
                        applyFormatting();
                    }
                });
                element.bind('blur', function (evt) {
                    if (angular.isDefined(ngModelController.$modelValue)) {
                        var val = ngModelController.$modelValue.split('.');
                        if (val && val.length == 1) {
                            if (val != "") {
                                ngModelController.$setViewValue(val + '.00');
                                ngModelController.$render();
                            }
                        } else if (val && val.length == 2) {
                            if (val[1] && val[1].length == 1) {
                                ngModelController.$setViewValue(val[0] + '.' + val[1] + '0');
                                ngModelController.$render();
                            } else if (val[1].length == 0) {
                                ngModelController.$setViewValue(val[0] + '.00');
                                ngModelController.$render();
                            }
                            applyFormatting();
                        }
                    }
                })
                ngModelController.$parsers.push(function (value) {
                    if (!value || value.length == 0) {
                        return value;
                    }
                    value = value.toString();
                    value = value.replace(/[^0-9\.]/g, "");
                    return value;
                });
                ngModelController.$formatters.push(function (value) {
                    if (!value || value.length == 0) {
                        return value;
                    }
                    value = formatNumber(value);
                    return value;
                });
            }
        };
    });
}
