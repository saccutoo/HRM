function BuildTable(appName, controllerName, tableUrl, tableUrl1) {
    app.controller(controllerName,
        function ($scope, myService, $filter, $rootScope, $compile, $http) {
            $scope.maxSize = 5; // Limit number for pagination display number.
            $scope.totalCount = 0; // Total number of items in all pages. initialize as
            $scope.pageIndex = 1; // Current page number. First page is 1.-->
            $scope.pageSizeSelected = 5;
            $rootScope.data = {
                filter5: '',
                filter1: '',
                filter2: '',
                filter3: '',
                filter4: '',
                filter6: '',
                filter7: '',
                filter8: '',
            }; //dự liệu truyền vào
            //model đượct truyền ra từ directive build table
            $scope.employeeData = {};
            $scope.employee = [];
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
                    if ($scope.tablePermission != null) {
                        if ($scope.tablePermission.isEdit == false) {
                            $scope.is_readonly = true;
                        }
                        else {
                            $scope.is_readonly = false;
                        }
                    }
                    // $scope.BuildAddButton(emp.data.result);
                },
                    function (emp) {
                        AppendToToastr(false, notification, errorNotiFalse);
                    });
            }
            $scope.getTableInfo();
            $scope.SetHiddenActionColumn = function (showEdit, showDelete) {
                if (showEdit === false && showDelete === false) {
                    return false;
                }
                return true;
            }


            // ----------------- Add chính sách-----------           
            $scope.addClick = function () {
                $scope.RessetValidion();
                $scope.PolicyDetailModel = {};
                $scope.PolicyDetailModel.Status = "1";
                ShowPopup($,
                    "#SavePolicyDetail",
                    $scope.tableInfo.PopupWidth,
                    $scope.tableInfo.PopupHeight);
            }
           
         
            //------------Dropdown chính sách -------------
            $scope.GetListPolicy = function () {
                var data = {
                    url: "/Common/GetListPolicy"
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.ListPolicy = res.data.result;
                },
                    function (res) {
                        $scope.msg = "Error";
                    });
            }
            //------------Dropdown chính sách -------------
            $scope.GetListStaffLevel = function () {
                var data = {
                    url: "/Common/GetDataByGloballistnotTree?parentid=" + 21
                }
                var list = myService.getDropdown(data);
                list.then(function (res) {
                    $scope.ListStaffLevel = res.data.result;
                },
                    function (res) {
                        $scope.msg = "Error";
                    });
            }

            //------------Dropdown trạng thái-------------
             $scope.ListStatus = function (){
                var data = {
                    url: "/Common/GetDataByGloballistnotTree?parentid=" + 88
                }
                var list = myService.getDropdown(data);
                 list.then(function (res) {
                    $scope.getListStatus = res.data.result;
                },
                    function (res) {
                        $scope.msg = "Error";
                    });
             }

            //------------Get List KPI Code, KPI Value trạng thái-------------
             $scope.GetListKPI = function () {
                 $scope.getListKpiCode = [];
                 var data = {
                     url: "/Common/GetDataByGloballistnotTree?parentid=" + 3494
                 }
                 var list = myService.getDropdown(data);
                 list.then(function (res) {
                     $scope.getListKpi = res.data.result;
                     $.each($scope.getListKpi, function (index, obj) {
                         if(obj && obj.ValueEN != null && obj.ValueEN != undefined)
                         $scope.getListKpiCode.push(obj.ValueEN);
                     });
                     console.log('listKpiCode', $scope.getListKpiCode);
                 },
                    function (res) {
                        $scope.msg = "Error";
                    });
             }

            $scope.Init = function () {
                // get chinh xach
                $scope.GetListPolicy();
                // get cap
                $scope.GetListStaffLevel();
                //get trạng thái
                $scope.ListStatus();
                //get masterdata KPI from Globallist
                $scope.GetListKPI();
            };
            $scope.Init();
            // -----------------ADD hoặc edit chính sách--------------
            $scope.fncValidationBtnSave = function () {
                $scope.formValid = true;
                if ($scope.PolicyDetailModel.PolicyID == '' || $scope.PolicyDetailModel.PolicyID == undefined) {
                    $scope.PolicyIdError = true; $scope.formValid = false;
                }
                else $scope.PolicyIdError = false;
                if ($scope.PolicyDetailModel.Status == '' || $scope.PolicyDetailModel.Status == undefined) {
                    $scope.StatusError = true;
                    $scope.formValid = false;
                }
                else $scope.StatusError = false;
                if ($scope.PolicyDetailModel.StaffLevelId == '' || $scope.PolicyDetailModel.StaffLevelId == undefined) {
                    $scope.StaffLevelIdError = true;
                    $scope.formValid = false;
                }
                else $scope.StaffLevelIdError = false;
                if ($scope.PolicyDetailModel.EndDate < $scope.PolicyDetailModel.StartDate) {
                    $scope.CompareDateError = true;
                    $scope.formValid = false;
                }
                else $scope.CompareDateError = false;

                if ($scope.PolicyDetailModel.BasicSalaryTo == '' || $scope.PolicyDetailModel.BasicSalaryTo == undefined) {
                    $scope.BasicSalaryToError = true;
                    $scope.formValid = false;
                }
                else $scope.BasicSalaryToError = false;
                if ($scope.PolicyDetailModel.BasicSalaryFrom == '' || $scope.PolicyDetailModel.BasicSalaryFrom == undefined) {
                    $scope.BasicSalaryFromError = true;
                    $scope.formValid = false;
                }
                else $scope.BasicSalaryFromError = false;

                if (!$scope.CheckEsistKpiFormular($scope.getListKpiCode, $scope.ConvertFormular($scope.PolicyDetailModel.SFormular))) {
                    $scope.SFormularError = true;
                    $scope.formValid = false;
                }
                else {
                    $scope.SFormularError = false;
                }
                if (!$scope.CheckEsistKpiFormular($scope.getListKpiCode, $scope.ConvertFormular($scope.PolicyDetailModel.SFormularCompensation))) {
                    $scope.SFormularCompensationError = true;
                    $scope.formValid = false;
                }
                else {
                    $scope.SFormularCompensationError = false;
                }
                if (!$scope.CheckEsistKpiFormular($scope.getListKpiCode, $scope.ConvertFormular($scope.PolicyDetailModel.SFormularAllowances))) {
                    $scope.SFormularAllowancesError = true;
                    $scope.formValid = false;
                }
                else {
                    $scope.SFormularAllowancesError = false;
                }
                if (!$scope.CheckEsistKpiFormular($scope.getListKpiCode, $scope.ConvertFormular($scope.PolicyDetailModel.SFormularProbation))) {
                    $scope.SFormularProbationError = true;
                    $scope.formValid = false;
                }
                else {
                    $scope.SFormularProbationError = false
                }
                if (!$scope.CheckEsistKpiFormular($scope.getListKpiCode, $scope.ConvertFormular($scope.PolicyDetailModel.SFormularBonus))) {
                    $scope.SFormularBonusError = true;
                    $scope.formValid = false;
                }
                else {
                    $scope.SFormularBonusError = false
                }
                if (!$scope.CheckEsistKpiFormular($scope.getListKpiCode, $scope.ConvertFormular($scope.PolicyDetailModel.SFormularDecemberbonus))) {
                    $scope.SFormularDecemberbonusError = true;
                    $scope.formValid = false;
                }
                else {
                    $scope.SFormularDecemberbonusError = false
                }

                if (!$scope.CheckEsistKpiFormular($scope.getListKpiCode, $scope.ConvertFormular($scope.PolicyDetailModel.SFormularDecemberbonus))) {
                    $scope.StandardProbationError = true;
                    $scope.formValid = false;
                }
                else {
                    $scope.StandardProbationError = false
                }
                if (!$scope.CheckEsistKpiFormular($scope.getListKpiCode, $scope.ConvertFormular($scope.PolicyDetailModel.SFormularKPIYear))) {
                    $scope.SFormularKPIYearError = true;
                    $scope.formValid = false;
                }
                else {
                    $scope.SFormularKPIYearError = false
                }
            }
            
            $scope.Save = function () {
                $scope.IsClickBtnSave = true;
                    $scope.fncValidationBtnSave();
                    if ($scope.formValid) {
                        //Kiểm tra tất cả công thức trên có tính được kết quả không? 
                        var fullFomularBeReplace = '';
                        fullFomularBeReplace = ($scope.ReplaceKpicodeFormular($scope.getListKpi, $scope.ConvertFormular($scope.PolicyDetailModel.SFormular), $scope.PolicyDetailModel.SFormular) != undefined ? $scope.ReplaceKpicodeFormular($scope.getListKpi, $scope.ConvertFormular($scope.PolicyDetailModel.SFormular), $scope.PolicyDetailModel.SFormular) : '') + ';';
                        fullFomularBeReplace += ($scope.ReplaceKpicodeFormular($scope.getListKpi, $scope.ConvertFormular($scope.PolicyDetailModel.SFormularCompensation), $scope.PolicyDetailModel.SFormularCompensation) != undefined ? $scope.ReplaceKpicodeFormular($scope.getListKpi, $scope.ConvertFormular($scope.PolicyDetailModel.SFormularCompensation), $scope.PolicyDetailModel.SFormularCompensation) : ' ') + ';';
                        fullFomularBeReplace += ($scope.ReplaceKpicodeFormular($scope.getListKpi, $scope.ConvertFormular($scope.PolicyDetailModel.SFormularAllowances), $scope.PolicyDetailModel.SFormularAllowances) != undefined ? $scope.ReplaceKpicodeFormular($scope.getListKpi, $scope.ConvertFormular($scope.PolicyDetailModel.SFormularAllowances), $scope.PolicyDetailModel.SFormularAllowances) : ' ') + ';';
                        fullFomularBeReplace += ($scope.ReplaceKpicodeFormular($scope.getListKpi, $scope.ConvertFormular($scope.PolicyDetailModel.SFormularProbation), $scope.PolicyDetailModel.SFormularProbation) != undefined ? $scope.ReplaceKpicodeFormular($scope.getListKpi, $scope.ConvertFormular($scope.PolicyDetailModel.SFormularProbation), $scope.PolicyDetailModel.SFormularProbation) : ' ') + ';';
                        fullFomularBeReplace += ($scope.ReplaceKpicodeFormular($scope.getListKpi, $scope.ConvertFormular($scope.PolicyDetailModel.SFormularBonus), $scope.PolicyDetailModel.SFormularBonus) != undefined ? $scope.ReplaceKpicodeFormular($scope.getListKpi, $scope.ConvertFormular($scope.PolicyDetailModel.SFormularBonus), $scope.PolicyDetailModel.SFormularBonus) : '') + ';';
                        fullFomularBeReplace += ($scope.ReplaceKpicodeFormular($scope.getListKpi, $scope.ConvertFormular($scope.PolicyDetailModel.SFormularDecemberbonus), $scope.PolicyDetailModel.SFormularDecemberbonus) != undefined ? $scope.ReplaceKpicodeFormular($scope.getListKpi, $scope.ConvertFormular($scope.PolicyDetailModel.SFormularDecemberbonus), $scope.PolicyDetailModel.SFormularDecemberbonus) : '') + ';';
                        fullFomularBeReplace += ($scope.ReplaceKpicodeFormular($scope.getListKpi, $scope.ConvertFormular($scope.PolicyDetailModel.StandardProbation), $scope.PolicyDetailModel.StandardProbation) != undefined ? $scope.ReplaceKpicodeFormular($scope.getListKpi, $scope.ConvertFormular($scope.PolicyDetailModel.StandardProbation), $scope.PolicyDetailModel.StandardProbation) : '') + ';';
                        fullFomularBeReplace += ($scope.ReplaceKpicodeFormular($scope.getListKpi, $scope.ConvertFormular($scope.PolicyDetailModel.SFormularKPIYear), $scope.PolicyDetailModel.SFormularKPIYear) != undefined ? $scope.ReplaceKpicodeFormular($scope.getListKpi, $scope.ConvertFormular($scope.PolicyDetailModel.SFormularKPIYear), $scope.PolicyDetailModel.SFormularKPIYear) : '') 
                        console.log("fullFomularBeReplace", fullFomularBeReplace);
                        var getListFomularRunError = $http({
                            method: "POST",
                            url: "/PolicyDetail/PolicyDetail_GetListFomularRunError",
                            data: {
                                fomularStr: fullFomularBeReplace
                            },
                            dataType: "json"
                        });
                        getListFomularRunError.then(function (res) {
                            $scope.fomularRunError = false;
                            if (res.data.resultFomular.indexOf('SFormularError') != -1) {
                                $scope.SFormularError = true;
                                $scope.fomularRunError = true;
                            }
                            else {
                                $scope.SFormularError = false;
                            }
                            if (res.data.resultFomular.indexOf('SFormularProbationError') != -1) {
                                $scope.SFormularProbationError = true;
                                $scope.fomularRunError = true;
                            }
                            else {
                                $scope.SFormularProbationError = false;
                            }
                            if (res.data.resultFomular.indexOf('SFormularCompensationError') != -1) {
                                $scope.SFormularCompensationError = true;
                                $scope.fomularRunError = true;
                            }
                            else {
                                $scope.SFormularCompensationError = false;
                            }
                            if (res.data.resultFomular.indexOf('SFormularAllowancesError') != -1) {
                                $scope.SFormularAllowancesError = true;
                                $scope.fomularRunError = true;
                            }
                            else {
                                $scope.SFormularAllowancesError = false;
                            }
                            if (res.data.resultFomular.indexOf('SFormularBonusError') != -1) {
                                $scope.SFormularBonusError = true;
                                $scope.fomularRunError = true;
                            }
                            else {
                                $scope.SFormularBonusError = false;
                            }
                            if (res.data.resultFomular.indexOf('SFormularDecemberbonusError') != -1) {
                                $scope.SFormularDecemberbonusError = true;
                                $scope.fomularRunError = true;
                            }
                            else {
                                $scope.SFormularDecemberbonusError = false;
                            }
                            if (res.data.resultFomular.indexOf('StandardProbationError') != -1) {
                                $scope.StandardProbationError = true;
                                $scope.fomularRunError = true;
                            }
                            else {
                                $scope.StandardProbationError = false;
                            }
                            if (res.data.resultFomular.indexOf('SFormularKPIYearError') != -1) {
                                $scope.SFormularKPIYearError = true;
                                $scope.fomularRunError = true;
                            }
                            else {
                                $scope.SFormularKPIYearError = false;
                            }
                            // Kiểm tra nếu tất cả các công thức trên đều Run đưoc.
                            if(!$scope.fomularRunError){
                                // Kiểm tra cùng cấp và cùng chính sách thì không được trùng công thức
                                var GetListFormular = $http({
                                    method: "POST",
                                    url: "/PolicyDetail/PolicyDetail_GetListFormularByStaffIdAndPolicyId",
                                    data: {
                                        id: $scope.PolicyDetailModel.Id != undefined ? $scope.PolicyDetailModel.Id : 0,
                                        staffId: $scope.PolicyDetailModel.StaffLevelId,
                                        policyId: $scope.PolicyDetailModel.PolicyID
                                    },
                                    dataType: "json"
                                });
                                GetListFormular.then(function (res) {
                                    var arrayTemp = [];
                                    for (var i = 0; i < res.data.result.length; i++) {
                                        if (res.data.result[i] != null)
                                        arrayTemp.push(res.data.result[i].replace(/ /g, ''));
                                    }
                                    var formularTemp = angular.copy($scope.PolicyDetailModel.SFormular == null ? '' : $scope.PolicyDetailModel.SFormular).replace(/ /g, '');
                                    if (arrayTemp.indexOf(formularTemp) !== -1) {
                                        $scope.SFormularValid = true;
                                    }
                                    else {
                                        $scope.SFormularValid = false;
                                        var SaveAction = $http({
                                            method: "POST",
                                            url: "/PolicyDetail/PolicyDetail_Save",
                                            data: {
                                                data: $scope.PolicyDetailModel
                                            },
                                            dataType: "json"
                                        });
                                        SaveAction.then(function (res) {
                                            if (res.data.result.IsSuccess == true) {
                                                if ($scope.PolicyDetailModel.PolicyID != 0 || $scope.PolicyDetailModel.PolicyID != null) {
                                                    AppendToToastr(true, notification, successfulUpdate, 500, 5000);
                                                }
                                                else {
                                                    AppendToToastr(true, notification, successfulAdd, 500, 5000);

                                                }
                                                $scope.PolicyDetailData.reload();
                                                $scope.CloseForm();
                                            }
                                            else {
                                                AppendToToastr(false, notification, res.data.result.Message, 500, 5000);
                                                $scope.PolicyDetailData.reload();
                                                $scope.CloseForm();

                                            }
                                        }, function (res) {
                                            AppendToToastr(false, notification, errorNotiFalse);
                                        });

                                    }
                                });
                            }

                        });
                        //End kiểm tra
                    }
            }
          
            //-----------------Edit Chính sách--------------
            $scope.editClick = function (dataItem) {
                $scope.PolicyDetailModel = angular.copy(dataItem);
                $scope.RessetValidion();
                $scope.PolicyDetailModel.Status = $scope.PolicyDetailModel.Status.toString();
                ShowPopup($,
                    "#SavePolicyDetail",
                    $scope.tableInfo.PopupWidth,
                    $scope.tableInfo.PopupHeight);
            }
            //load lại trang khi click bỏ qua
            $scope.CloseForm = function () {
                $scope.PolicyDetailModel = {};
                $.colorbox.close();
            }
            $scope.ConvertFormular = function (formular) {
                var listformularMatch = [];
                if (formular == undefined) formular = null;
                if (formular != null) {
                    var dateReg = /\[\w+\]/g;
                    if (formular.toString().indexOf("[") != -1 && formular.toString().indexOf("]") != -1) {
                        listformularMatch = formular.match(dateReg) == null ? [] : formular.match(dateReg);
                    }
                }
                return listformularMatch;
            }

            $scope.CheckEsistKpiFormular = function (listKPI, listformular) {
                var result = true;
                var listKPITemp = [];
                for (var i = 0; i < listKPI.length; i++) {
                    if (listKPI[i] != null && listKPI[i] != undefined)
                    listKPITemp.push(listKPI[i].toLowerCase());
                }
                for (var i = 0; i < listformular.length; i++) {
                    if (listformular[i] != null && listformular[i] != undefined) {
                        if (listKPITemp.indexOf(listformular[i].replace(/\[/g, '').replace(/\]/g, '').toLowerCase()) == -1)
                            result = false;
                    }
                }
                return result;
            } 
            $scope.ReplaceKpicodeFormular = function (listKpi, listformular, strFormular) {
                var strFormulartemp = angular.copy(strFormular);
                for (var i = 0; i < listformular.length; i++) {
                    var kpiValueItem = listKpi.filter(function (obj) { return obj.ValueEN.toLowerCase() == listformular[i].replace(/\[/g, '').replace(/\]/g, '').toLowerCase() })[0].Value;
                    if (kpiValueItem == null || kpiValueItem == undefined) { kpiValueItem = 0 };
                    strFormulartemp = strFormulartemp.replace(listformular[i], kpiValueItem);
                }
                return strFormulartemp;
            }

            $scope.OpenDailogCheckFormular = function (obj) {
                    if (obj != '' && obj != undefined) {
                        $scope.PolicyDetailFormularModel = {
                            Formular: '',
                            FormularReplace: '',
                            ResultFormular: ''
                        };
                        var listformular = $scope.ConvertFormular(obj);
                        // check KPI code có trong hệ thống chưa?
                        if ($scope.CheckEsistKpiFormular($scope.getListKpiCode, listformular)) {
                            $scope.PolicyDetailFormularModel.Formular = obj;
                            $scope.PolicyDetailFormularModel.FormularReplace = $scope.ReplaceKpicodeFormular($scope.getListKpi, listformular, obj);
                            ShowPopup($, "#cboxCheckSFormular", '50%', '350px');
                        }
                        else {
                            AppendToToastr(false, notification, alertfomularNotEsist);
                        }
                    }
            }
            $scope.CloseCheckSFormular = function () {
                ShowPopup($,
                    "#SavePolicyDetail",
                    $scope.tableInfo.PopupWidth,
                    $scope.tableInfo.PopupHeight);
            }
            $scope.CheckSFormular = function () {
                var getFormularReplace = $http({
                    method: "POST",
                    url: "/PolicyDetail/PolicyDetail_GetResultSFormular",
                    data: {
                        sFormularstr: $scope.PolicyDetailFormularModel.FormularReplace
                    },
                    dataType: "json"
                });
                getFormularReplace.then(function (res) {
                    $scope.PolicyDetailFormularModel.ResultFormular = res.data.result;
                });
            }

            $scope.RessetValidion = function () {
                $scope.IsClickBtnSave = false;
                $scope.PolicyIdError = false;
                $scope.StaffLevelIdError = false;
                $scope.BasicSalaryFromError = false;
                $scope.BasicSalaryToError = false;
                $scope.SFormularValid = false;
                $scope.SFormularError = false;
                $scope.SFormularProbationError = false;
                $scope.SFormularAllowancesError = false;
                $scope.SFormularBonusError = false;
                $scope.SFormularCompensationError = false;
                $scope.SFormularDecemberbonusError = false;
                $scope.StandardProbationError = false;
                $scope.SFormularKPIYearError = false;
            };

            $scope.deleteClick = function (obj) {
                BoostrapDialogConfirm(notification,
                    notificationDelete,
                    BootstrapDialog.TYPE_WARNING,
                    $scope.deleteActionClick,
                    obj);
            }
            $scope.deleteActionClick = function (obj) {
                var id = obj.Id;
                var action = myService.deleteAction(id, $scope.tableInfo.id, '/PolicyDetail/PolicyDetail_Delete');
                action.then(function (res) {
                    if (res.data.result.IsSuccess == true) {
                        AppendToToastr(res.data.result.IsSuccess, notification, deleteSuccess, 500, 5000);
                        $scope.PolicyDetailData.reload();
                    }
                    else {
                        AppendToToastr(false, notification, errorNotiFalse);

                    }
                },
                    function (res) {
                        AppendToToastr(false, notification, errorNotiFalse);
                    });

            }

            $scope.copyClick = function () {
                $scope.IsClickBtnCopy = false;
                ShowPopup($,
                    "#CopyPolicyDetail",
                    "40%", "35%");
            }

            $scope.SaveCopyPolicyDetail = function (form) {
                $scope.IsClickBtnCopy = true;
                if (!form.$error.required) {
                    var lstPolicyCopy = [];
                    $("input[name='chbox_policy']:checked").each(function () {
                        lstPolicyCopy.push($(this).val());
                    });
                    var copyPolicyDetail = $http({
                        method: "POST",
                        url: "/PolicyDetail/PolicyDetail_CopyPolicyDetail",
                        data: {
                            lstPolicyCopy: lstPolicyCopy,
                            policyId: $scope.PolicyDetailModel.PolicyID,
                            staffLevelId: $scope.PolicyDetailModel.StaffLevelId
                        },
                        dataType: "json"
                    });
                    copyPolicyDetail.then(function (res) {
                        if (res.data.IsSuccess) {
                            $scope.IsClickBtnCopy = false;
                            $('#btnCopy').hide();
                            AppendToToastr(res.data.IsSuccess, notification, res.data.Message, 500, 5000);
                            $scope.PolicyDetailData.reload();
                            $scope.CloseForm();
                        }
                        else {
                            AppendToToastr(res.data.IsSuccess, notification, res.data.Message, 500, 5000);
                        }
                    });
                }
            }
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
                    if ([8, 13, 27, 37, 38, 39, 40, 48].indexOf(event.which) > -1) {
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
    app.directive('autoComplete', [
        '$timeout', function ($timeout) {
            return {
                require: 'ngModel',
                link: function ($scope, element, attrs, ctrl) {
                    var fAutoComplete;
                    fAutoComplete = function () {
                        $timeout(function () {
                            if (!$scope[attrs.uiItems]) {
                                fAutoComplete();
                            } else {
                                element.autocomplete({
                                    source: [$scope[attrs.uiItems]]
                                }).on('selected.xdsoft', function (e, newValue) {
                                    ctrl.$setViewValue(newValue);
                                    $scope.$apply();
                                });
                            }
                        }, 5);
                    };
                    return fAutoComplete();
                }
            };
        }
    ])
}

