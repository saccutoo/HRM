$(document).ready(function () {
    $('.my-request-box-left .request-type .item ').click(function () {
        $('.my-request-box-left .request-type .item ').removeClass('active');
        $(this).addClass('active');
        var id = $(this).attr('data-id');
        var activeTab = $('.hrmv2-tab .nav-item.active').attr("index");
        var month = sessionStorage.getItem("tab-month");
        var year = sessionStorage.getItem("tab-year");

        $('#my-request-box-right-body').load('/Workingday/GetWorkingdaySupplenmentById', { id: id, activeTab: activeTab, month: month, year: year })
    });

    $(document).on('select2:selecting', '#staff-summary-furlong', function (e) {
        var tableName = $('#table-name-furlough').val();
        var furlongDate = $('#month-working-summary-furlong').val();
        var dateNew = furlongDate.split('/');
        var month = dateNew[0];
        var year = dateNew[1];
        if (e.params.args.data.id == "0") {
            filterStr = "";
        }
        else {
            filterStr = 'AND T.Id =' + e.params.args.data.id;
        }
        referenceId = $("#organization").val();
        sessionStorage.setItem("tab-staffId", e.params.args.data.id);
        reloadTable(ControlModel[tableName].TableName, ControlModel[tableName].TableDataUrl, ControlModel[tableName].CurrentPage, ControlModel[tableName].ItemsPerPage, filterStr, referenceId)
    });

    $(document).on('select2:selecting', '#workingday-all-staff-select', function (e) {
        var tableName = $('#table-name-working-day-all-staff').val();
        var dateWdAllStaff = $('#month-working-day-all-staff').val();
        var activeTab = $('.hrmv2-tab .nav-item.active').attr("index");
        sessionStorage.setItem("tab-" + activeTab + "-staffId", e.params.args.data.id);
        var dateNew = dateWdAllStaff.split('/');
        var month = dateNew[0];
        var year = dateNew[1];
        var tableName = $('#table-name-working-day-all-staff').val();
        sessionStorage.setItem("tab-staffId", e.params.args.data.id);
        reloadTableByTab(tableName, e.params.args.data.id, month, year, 'all-staff')
    });

    $('#month-working-day-all-staff-datetimepicker').on('dp.change',function(e){
        var staffSelected = $('#workingday-all-staff-select').val();
        var tableName = $('#table-name-working-day-all-staff').val();
        var activeTab = $('.hrmv2-tab .nav-item.active').attr("index");
        var dateChange = $('#month-working-day-all-staff').val();
        if(dateChange != null){
            var dateNew = dateChange.split('/');
            var m1 = dateNew[0];
            var y1 = dateNew[1];
            sessionStorage.setItem("tab-"+activeTab+"-month", m1 );
            sessionStorage.setItem("tab-" + activeTab + "-year", y1);
            reloadTableByTab(tableName, staffSelected, m1, y1,'all-staff')
        }
    });
    function reloadTableByTab(tableName, staffSelected, month, year, type) {
        stringJson = "{Month:" + month + ",Year:" + year + ",StaffId:" + staffSelected + "}";
        referenceId = $("#organization").val();
        if (type == 'all-staff') {
            if (staffSelected != "") {
                if (staffSelected != "0") {
                    filterStr = "AND T.Id = " + staffSelected;
                }
                else {
                    filterStr = '';
                }
               
            }
        }
        
        reloadTable(ControlModel[tableName].TableName, ControlModel[tableName].TableDataUrl, ControlModel[tableName].CurrentPage, ControlModel[tableName].ItemsPerPage, filterStr, referenceId, stringJson)
    }
    $('.nav-tabs .nav-item-popup').click(function () {
        var index = $(this).attr("index");
        $('#myTabContent-summary-detail-by-tab .tab-pane.fade').removeClass("active in");
        $('.panel-content' + index).addClass("active in");
    })
})
function removePopupWorkingdayDetai() {
    $('#calendar td').removeClass('active');
}

