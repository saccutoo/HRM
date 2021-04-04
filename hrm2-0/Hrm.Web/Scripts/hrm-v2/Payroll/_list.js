
function ShowSalaryPaySlip(salaryTypeId) {
    var options = {
        url: '/Payroll/GetFormSalaryPaySlip',
        width: 1000,
        isDataTypeJson: true,
        data: {
            salaryTypeId: salaryTypeId
        },
        isNotificationPoup: false,
        align: "right",
    };
    CreatePopup(options);
}

function choseAvatar() {
    $('#file-upload').click();
}

function changeInputFile() {

    var fileData = [];
    var file = $('#file-upload')[0].files;
    var formData = new FormData();
    for (var i = 0; i < file.length; i++) {
        formData.append('attachment', file[i]);
    }
    $.ajax({
        type: 'POST',
        url: "/Payroll/SaveFile",
        data: formData,
        processData: false,
        contentType: false,
        success: function (response) {
            if (response.IsSuccess) {
                if (response.Result != null && response.FileName != '') {
                    $('#payroll-avata').val(response.FileName);
                    $('#avater-img').attr('src', response.Result);
                    $('#file-upload').val("");
                }
            }
        },
    });
}

$(document).on('select2:selecting', '#WorkingProcess-WorkingprocessTypeId', function (e) {
    $('#WorkingProcess-WorkingprocessTypeId').val(e.params.args.data.id)
    $('#working-process-decision').load('/WorkingProcess/ChangeWorkingprocessType', { model: $('form#create-payroll-form').serializeObject() });
});

function SavePayroll() {
    $("#addPayrollform span[name*='-error']").text("");
    console.log($('form#addPayrollform').serializeArray());
    console.log($('form#addPayrollform').serializeObject());
    let data = $('form#addPayrollform').serializeObject();
    $.ajax(
        {
            url: "/Payroll/SavePayroll",
            type: "POST",
            data: {
                model: data
            },
            success: function (response) {
                if (response.Invalid) {
                    let validations = response.Result;
                    RenderError(validations, "addPayrollform");
                }
                else if (response.Result) {
                    ShowMessage(true, msgSuccessful, response.Message, 0, 4000, 0);
                    gotoCheckPayroll(response.Result.Id, response.Result.Month, response.Result.Year);
                }
                else {
                    ShowMessage(false, msgFalse, response.Message, 0, 4000, 0);
                }
            }
        });
}

//function goToAddForm() {
//    var data = $('form#addPayrollform').serializeObject();
//    //Bind to sessionStorage 
//    sessionStorage.setItem("addFormPayroll", JSON.stringify(data));
//    if ($("#IsOnboarding").val() != undefined && $("#IsOnboarding").val().toLowerCase() == "true") {
//        window.location.href = "/Payroll/Add?type=1";
//    }
//    else {
//        gotoAddPayroll();
//    }
//}

//function editPayroll(id) {
//    window.location.href = "/Payroll-Detail?PayrollId=" + id + "&activetab=1&viewtype=1"
//}

function latch() {
    let checkedLst = $('input[type="checkbox"][name=chk-column-row]:checked').map(function () {
        return $(this).attr('value');
    }).get();
    $.ajax(
        {
            url: "/Payroll/Latch",
            type: "POST",
            data: {
                modelLst: checkedLst
            },
            success: function (response) {
                if (response.Invalid) {
                    //let validations = response.Result;
                    //RenderError(validations, "addPayrollform");
                }
                else if (response.Result) {
                    ShowMessage(true, msgSuccessful, response.Message, 0, 4000, 0);
                    location.reload();
                }
                else {
                    ShowMessage(false, msgFalse, response.Message, 0, 4000, 0);
                }
            }
        });
}

// Begin Direction //
function gotoPayrollList() {
    return location.href = '/salary-table-salary';
}

function gotoAddPayroll() {
    location.href = '/Payroll/Add';
}

function gotoCheckPayroll(payrollId, month, year) {
    return location.href = '/Payroll/Summary?id=' + payrollId + '&month=' + month + '&year=' + year;
}

function gotoSummary(payrollId) {
    return location.href = '/Payroll/Summary/' + payrollId;
}
// End Direction //