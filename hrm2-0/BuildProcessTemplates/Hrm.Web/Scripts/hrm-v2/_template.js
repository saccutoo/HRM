//------------------------------ JS FOR DROPDOWN-------------------------------------------------------

$(document).on('click', '.hrmv2-dropdown-container', function () {
    if ($(this).hasClass('active')) {
        $(this).removeClass('active');
    }
    else {
        $(this).addClass('active');
    }
});
$(document).on('mouseover', '.select2-results__option', function () {
    $('.select2-results__option').attr("aria-selected", "false")
});
$(document).on('mouseover', '.dropdown-result-selectable', function () {
    $('.dropdown-result-selectable').removeClass('active');
    $(this).addClass('active');
});
function select(value, dropdownName) {
    var dropdownModel = ControlModel[dropdownName];
    var listData = dropdownModel.Data;
    var filtered = listData.filter(function (item) { return item && item[dropdownModel.ValueField] == value });
    if (filtered.length > 0) {
        //$('#' + dropdownName + '-selected-value').text(filtered[0].Name);
        $('#' + dropdownName + '-selected-value').text(filtered[0][dropdownModel.DisplayField]);
        $('#' + dropdownModel.Name + '-value').val(filtered[0][dropdownModel.ValueField]);
        $('#' + dropdownModel.Name + '-value').attr("value", filtered[0][dropdownModel.ValueField]);
        $('#' + dropdownModel.Name + '-value').trigger('change');
        if (dropdownModel.IsUseImage) {
            $('#' + dropdownName + '-selected-image').attr("src", baseUrl + filtered[0].ImageName);
        }
    }
}

//----------------------------End JS FOR DROPDOWN-------------------------------------------------------

//------------------------------ JS FOR TABLE-------------------------------------------------------

var filterStr = '';
var referenceId = 0;
var stringJson = '';

function reloadTable(tableName, dataUrl, pageNumber, pageSize, filter, referenceId, stringJson) {
    $.ajax(
    {
        url: dataUrl,
        type: "POST",
        data: {
            tableData: ControlModel[tableName], param: {
                pageNumber: pageNumber, pageSize: pageSize, FilterField: filter, ReferenceId: referenceId, StringJson: stringJson
            }
        },
        success: function (response) {
            $('#' + tableName).html(response);
            $('#cover-spin').hide();
        }
    });
}

function reloadTableConfig(tableName, dataUrl, pageNumber, pageSize, filter, referenceId, tableConfigName, groupId, stringJson) {
    $.ajax(
    {
        url: dataUrl,
        type: "POST",
        data: {
            tableData: ControlModel[tableName], param: {
                pageNumber: pageNumber, pageSize: pageSize, FilterField: filter, ReferenceId: referenceId, GroupId: groupId, StringJson: stringJson
            }, tableConfigName: tableConfigName
        },
        success: function (response) {
            $('#' + tableName).html(response);
            $('#cover-spin').hide();
        }
    });
}

function changePage(tableName, direction) {
    var maxItemPerPage = Math.ceil((ControlModel[tableName].TotalRecord == 0 ? 1 : ControlModel[tableName].TotalRecord) / ControlModel[tableName].ItemsPerPage);
    var currentPage = $('#' + tableName + '-paging-current-page').val();
    var itemsPerPage = $('#' + tableName + '-paging-items-per-page').val();
    var dataUrl = webBaseUrl + ControlModel[tableName].TableDataUrl;
    var currentPageNew = parseInt(currentPage);
    if (direction == 'previous') currentPageNew = currentPageNew - 1;
    else if (direction == 'next') currentPageNew = parseInt(currentPage) + 1;
    else if (direction == 'first') currentPageNew = 1;
    else if (direction == 'last') currentPageNew = maxItemPerPage;

    reloadTable(tableName, dataUrl, currentPageNew, itemsPerPage, filterStr, referenceId, stringJson);
}

function changeItemsPerpage(tableName) {
    var maxItemPerPage = Math.ceil((ControlModel[tableName].TotalRecord == 0 ? 1 : ControlModel[tableName].TotalRecord) / ControlModel[tableName].ItemsPerPage);
    var currentPage = 1;
    var itemsPerPage = $('#' + tableName + '-paging-items-per-page').val();
    var dataUrl = webBaseUrl + ControlModel[tableName].TableDataUrl;
    reloadTable(tableName, dataUrl, currentPage, itemsPerPage, filterStr, referenceId, stringJson);
}

function pagingStyle(object) {
    var maxItemPerPage = Math.ceil((object.TotalRecord == 0 ? 1 : object.TotalRecord) / object.ItemsPerPage);
    $('#' + object.tableName + '-paging-first').prop('disabled', false);
    $('#' + object.tableName + '-paging-previous').prop('disabled', false);
    $('#' + object.tableName + '-paging-next').prop('disabled', false);
    $('#' + object.tableName + '-paging-last').prop('disabled', false);

    if (object.CurrentPage == 1) {
        $('#' + object.TableName + '-paging-first').prop('disabled', true);
        $('#' + object.TableName + '-paging-previous').prop('disabled', true);
    }
    if (object.CurrentPage == maxItemPerPage) {
        $('#' + object.TableName + '-paging-next').prop('disabled', true);
        $('#' + object.TableName + '-paging-last').prop('disabled', true);
    }
}

function showButtonHeader(tableName) {
    $('#' + tableName + ' .hrm-v2-header-button').hide();
    $('#' + tableName + ' .hrm-v2-header-collum').hide();
    var checkedNumber = $('#' + tableName + ' .chk_column:checked').length;
    if (checkedNumber > 0) {
        $('#' + tableName + ' .hrm-v2-header-button').show();
    }
    else {
        $('#' + tableName + ' .hrm-v2-header-collum').show();
    }
}


//----------------------------End JS FOR TABLE-------------------------------------------------------


//----------------------------JS FOR ORG CHART------------------------------------------------------
$(document).on('click', '.emp-card', function () {
    var id = $(this).attr("id-value");
    $("#org-chart-panel").load('/Staff/OrgChartView', { id: id, date: moment().format('MM/DD/YYYY') });
});
$(document).on('click', '.organization-card', function () {
    $('#cover-spin').show();
    var id = $(this).attr("id-value");
    $("#organization-chart-panel").load('/Admin/Organization/OrganizationChartView', { id: id });
    $('#cover-spin').hide();
});


//----------------------------END JS FOR ORG CHART------------------------------------------------------

//--------------------------------------------------------------------------------------------------//
function check(element, id, parentId) {
    $(element).closest(".cb-control").find(".checkbox:first").removeClass("mix-state");
    var currentState = $(".checkbox-tree input[value=" + id + "]").is(":checked");

    var lstChild = $(".checkbox-tree input[parent-id=" + id + "]");
    if (lstChild != undefined && lstChild.length > 0) {
        if (!currentState) {
            $(element).closest(".checklist-item").find(".completed-counting:first").html("(0/" + lstChild.length + ")");
        }
        else {
            $(element).closest(".checklist-item").find(".completed-counting:first").html("(" + lstChild.length + "/" + lstChild.length + ")");
        }
    }
    else {
        $(element).closest(".checklist-item").find(".completed-counting:first").html("(0/0)");
    }
    //down
    downState(id, parentId, currentState);
    //up
    upState(id, parentId, currentState);
}
function downState(id, parentId, currentState) {
    var lstChild = $(".checkbox-tree input[parent-id=" + id + "]");
    if (lstChild != undefined && lstChild.length > 0) {
        for (var i = 0; i < lstChild.length; i++) {
            var childId = $(lstChild[i]).attr("value");
            var childParentid = $(lstChild[i]).attr("parent-id");
            $(lstChild[i]).closest(".cb-control").find(".checkbox:first").removeClass("mix-state");
            if (currentState == undefined || !currentState) {
                $(lstChild[i]).prop("checked", false);
            }
            else {
                $(lstChild[i]).prop("checked", true);
            }
            downState(childId, childParentid, currentState);
        }
    }
}
function upState(id, parentId, currentState) {
    var lstUpFather = $(".checkbox-tree input[value=" + parentId + "]");
    if (lstUpFather != undefined && lstUpFather.length > 0) {
        var father = lstUpFather[0];
        var fatherId = $(father).attr("value");
        var fatherParentid = $(father).attr("parent-id");
        var lstFatherChild = $(".checkbox-tree input[parent-id=" + fatherId + "]");
        if (lstFatherChild != undefined && lstFatherChild.length > 0) {
            var checked = 0;
            var mixed = 0;
            for (var i = 0; i < lstFatherChild.length; i++) {
                if ($(lstFatherChild[i]).is(":checked")) {
                    checked++;
                }
                if ($($(lstFatherChild[i]).closest(".cb-control").find(".checkbox:first")).hasClass("mix-state")) {
                    mixed++;
                }
            }
            $(father).closest(".checklist-item").find(".completed-counting:first").html("(" + checked + "/" + lstFatherChild.length + ")");
            var checkbox = $(father).closest(".cb-control").find(".checkbox:first");
            if (checked == 0) {
                if (mixed == 0) {
                    $(checkbox).removeClass("mix-state");
                    $(father).prop("checked", false);
                    upState(fatherId, fatherParentid, false);
                }
                else {
                    $(checkbox).addClass("mix-state");
                    $(father).prop("checked", false);
                    upState(fatherId, fatherParentid, false);
                }
            }
            else
                if (checked == lstFatherChild.length) {
                    $(checkbox).removeClass("mix-state");
                    $(father).prop("checked", true);
                    upState(fatherId, fatherParentid, true);
                }
                else {
                    $(checkbox).addClass("mix-state");
                    $(father).prop("checked", false);
                    upState(fatherId, fatherParentid, false);
                }
        }
    }
}
$(document).on('click', '.month-control-button', function () {
    var staffid = parseInt($("#page-staffid").val());
    var month = sessionStorage.getItem("tab-0-month");
    var year = sessionStorage.getItem("tab-0-year");
    if (month == null && year == null) {
        month = moment(new Date).format('M');
        year = moment(new Date).format('YYYY');
    }
    var viewtype = $("#page-viewtype").val();
    if ($(this).hasClass("month-next")) {
        if (month == 12) {
            month = 1;
            year = parseInt(year) + 1;
        }
        else {

            month = parseInt(month) + 1;
        }
    }
    else {
        if (month == 1) {
            month = 12;
            year = parseInt(year) - 1;
        }
        else {
            month = parseInt(month) - 1;
        }
    }
    sessionStorage.setItem("tab-0-month", month);
    sessionStorage.setItem("tab-0-year", year);
    $("#cover-spin").show();
    window.location = webBaseUrl + "workingday-detail?staffid=" + staffid + "&month=" + month + "&year=" + year + "&viewtype=" + viewtype;
});

$.fn.serializeObject = function () {
    var o = {};
    var disabled = this.find(':input:disabled').removeAttr('disabled');
    var a = this.serializeArray();
    disabled.attr('disabled', 'disabled');
    $.each(a, function () {
        if (o[this.name]) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
        } else {
            o[this.name] = this.value || '';
        }
    });
    return o;
};

//Select 2
function templateResult(state) {
    var icon = '';
    if (state.element != undefined && state.element != '') {
        icon = state.element.attributes.icon.value;
    }
    if (!state.id) { return state.text; }
    var $state = $('<span> ' + state.text + ' </span>');
    if (icon != null && icon != '') {
        $state = $('<span><i class="' + icon + '"></i> ' + state.text + '</span>');
    }
    return $state;
}
function templateSelect(state) {
    var icon = '';
    if (state.element != undefined && state.element != '') {
        icon = state.element.attributes.icon.value;
    }
    if (!state.id) { return state.text; }
    var $state = $('<span></span>');
    if (icon != null && icon != '') {
        $state = $('<div style="height:26px;display:flex;align-items:center;font-size:14px;"><div><i class="' + icon + '"></i></div></div>');
    }
    return $state;
}

//Alert Confirm
function confirmTemplate(messeage, callbackFunction) {
    bootbox.confirm({
        message: messeage,
        buttons: {
            confirm: {
                label: 'Yes',
                className: 'btn-success'
            },
            cancel: {
                label: 'No',
                className: 'btn-danger'
            }
        },
        callback: function (result) {
            if (result == 'Yes') {
                callbackFunction;
            }
        }
    });
}

////Inline editing
//function showEditInline(tableClassName, sqlTableName, id, data) {
//    var tableModel = ControlModel[tableClassName];
//    var rowId = '#' + tableClassName + '-row-' + id;
//    var fields = tableModel.Fields.filter(function (item) {
//        return item && item.Visible;
//    });    
//    var columns = tableModel.ListTableColumns;
//    var colModel = [];
//    var html = '';
//    for (var i = 0; i < fields.length; i++) {
//        for (var j = 0; j < columns.length; j++) {
//            if (columns[j].ColumnName == fields[i].FieldName) {
//                var col = columns[j];
//                if (fields[i].DataFormat == 'Date') {
//                    col.ColumnValue = moment(data[fields[i].FieldName]).format('DD/MM/YYYY');
//                }
//                else
//                    col.ColumnValue = data[fields[i].FieldName];
//                colModel.push(col);
//            }
//        }
//    }
//    $(rowId).load('/Filter/InlineEditor', { columns: colModel, fields: fields, dataId: id, tableClassName: tableClassName, sqlTableName: sqlTableName });
//}
//function saveInlineRow(id, tableClassName, sqlTableName) {
//    var tableModel = ControlModel[tableClassName];
//    var columns = tableModel.ListTableColumns;
//    var updateQuery = '';
//    var data = {};
//    for (var j = 0; j < columns.length; j++) {
//        var element = $('#row-column-' + columns[j].Id);
//        if (element.length > 0) {
//            if (updateQuery == '') {
//                updateQuery += 'UPDATE ' + sqlTableName + ' ';
//                updateQuery += 'SET ' + columns[j].ColumnName + ' = ';                
//            }
//            else {
//                updateQuery += ', ' + columns[j].ColumnName + ' = ';
//            }
//            if (columns[j].ColumnDataTypeId == 471 || columns[j].ColumnDataTypeId == 473) {
//                updateQuery += $(element[0]).val();
//            }
//            else
//            if (columns[j].ColumnDataTypeId == 472) {
//                updateQuery += '\'' + moment($(element[0]).val(), 'DD/MM/YYYY').format('YYYY-MM-DD') + '\'';
//            }
//            else {
//                updateQuery += 'N\'' + $(element[0]).val() + '\'';
//            }
//            data[columns[j].ColumnName] = $(element[0]).val();
//        }
//    }
//    if (updateQuery != '') {
//        updateQuery += ' WHERE Id = ' + id;
//    }
//    //call ajax to run sql update here
//    //
//    debugger
//}
function exportData(tableName, url) {
    $('#cover-spin').show();
    $.ajax(
    {
        url: url,
        type: "POST",
        data: {
            tableConfig: ControlModel[tableName]
        },
        success: function (response) {
            $('#export-' + tableName).html(response);
            var cells = [];
            var lstTr = $('#export-' + tableName).find('tr');
            for (var i = 0; i < lstTr.length; i++) {
                //var row = [];
                var lstChild = $(lstTr[i]).find('th');
                if (lstChild.length <= 0) {
                    lstChild = $(lstTr[i]).find('td');
                }

                for (var j = 0; j < lstChild.length; j++) {
                    var cell = {};
                    cell.X = i;
                    cell.Y = j;
                    cell.DataFormat = $(lstChild[j]).attr('data-format');
                    cell.Value = $(lstChild[j]).text().trim();
                    cell.Width = $(lstChild[j]).outerWidth();
                    cells.push(cell);
                }
                //rows.push(row);
            }
            $('#export-' + tableName).html("");
            $.ajax(
            {
                url: '/Common/SetStorage/',
                type: "POST",
                data: {
                    cells: cells,
                    tableName: tableName
                },
                success: function () {
                    $('#cover-spin').hide();
                    window.location = webBaseUrl + "Common/Export?tableName=" + tableName;
                },
                error: function (er) {
                    $('#cover-spin').hide();
                }
            });
            //$.ajax(
            //{
            //    url: '/Common/Export',
            //    type: "POST",
            //    data: {
            //        cells: cells
            //    },
            //    success: function (response) {

            //        $('#cover-spin').hide();
            //    },
            //    error: function (er) {
            //        $('#cover-spin').hide();

            //    }
            //});
        },
        error: function (er) {
            $('#cover-spin').hide();

        }
    });
}
function removeRow(e, id) {
    $(e).closest('.system-table-tr').hide();
    $(e).closest('.system-table-tr').find('.item-isDeleted').val("True");
}
(function ($) {
    $.fn.inputFilter = function (inputFilter) {
        return this.on("input keydown keyup mousedown mouseup select contextmenu drop", function () {
            if (inputFilter(this.value)) {
                this.oldValue = this.value;
                this.oldSelectionStart = this.selectionStart;
                this.oldSelectionEnd = this.selectionEnd;
            } else if (this.hasOwnProperty("oldValue")) {
                this.value = this.oldValue;
                this.setSelectionRange(this.oldSelectionStart, this.oldSelectionEnd);
            } else {
                this.value = "";
            }
        });
    };
}(jQuery));

$(document).on('change', '.floating-select.select2-hidden-accessible', function (e) {
    if ($('#' + e.currentTarget.id).val() != "") {
        $('#' + e.currentTarget.id).closest('.floating-label').find('#select2-' + e.currentTarget.id + '-container').css('color', '#223133')
    }
    else {
        $('#' + e.currentTarget.id).closest('.floating-label').find('#select2-' + e.currentTarget.id + '-container').css('color', '#7c7c7c')
    }
});
$(document).on('change', '.number-only', function (e) {
    if(e.currentTarget.value == "")
    {
        e.currentTarget.value = "0"
    }
    //else {
    //}
});

function stringToDateUtc(_date, _format, _delimiter) {
    var formatLowerCase = _format.toLowerCase();
    var formatItems = formatLowerCase.split(_delimiter);
    var dateItems = _date.split(_delimiter);
    var monthIndex = formatItems.indexOf("mm");
    var dayIndex = formatItems.indexOf("dd");
    var yearIndex = formatItems.indexOf("yyyy");
    var month = parseInt(dateItems[monthIndex]);
    month -= 1;
    var formatedDate = new Date(Date.UTC(dateItems[yearIndex], month, dateItems[dayIndex]));
    return formatedDate;
}