function openPopupAddStaff(isOnboarding) {
    var options = {
        id: 'frmAddStaff',
        isNotificationPoup: false,
        align: "right",
        url: '/Staff/_AddStaff',
        width: 495,
        data: '{isOnboarding: ' + isOnboarding + '}',
        idform: 'frm-add-staff',
        //urlback: '/Contract/GetContractCoaInfoList',
        //databack: '{contractId:' + contractId + '}',
        //divload: 'GridContractCoaInfo',
        //fnNameReload: 'ResetSelectCoaInfo'
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
        url: "/Staff/SaveFile",
        data: formData,
        processData: false,
        contentType: false,
        success: function (response) {
            if (response.IsSuccess) {
                if (response.Result != null && response.FileName != '') {
                    $('#staff-avata').val(response.FileName);
                    $('#avater-img').attr('src', response.Result);
                    $('#file-upload').val("");
                }
            }
        },
    });
}
$(document).on('select2:selecting', '#WorkingProcess-WorkingprocessTypeId', function (e) {
    $('#WorkingProcess-WorkingprocessTypeId').val(e.params.args.data.id)
    $('#working-process-decision').load('/WorkingProcess/ChangeWorkingprocessType', { model: $('form#create-staff-form').serializeObject() });
});
function SaveStaff() {
    $("#addStaffform span[name*='-error']").text("");
    $.ajax(
    {
        url: "/Staff/SaveStaff",
        type: "POST",
        data: {
            model: $('form#addStaffform').serializeObject()
        },
        success: function (response) {
            if (response.Invalid) {
                var validations = response.Result;
                RenderError(validations, "addStaffform");
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
function saveStaffFull() {
    $("#create-staff-form span[name*='-error']").text("");
    $.ajax({
        type: "POST",
        url: "/Staff/SaveStaffFull",
        data: $('form#create-staff-form').serializeObject(),
        success: function (response) {
            if (response.Invalid)
            {
                var validations = response.Result;
                var tabStaffErr = false;
                var tabWpErr = false;
                var tabSalaryErr = false;
                for (var i = 0; i < validations.length; i++) {
                    var elementName = validations[i].ColumnName;
                    if (elementName.indexOf("Staff.") != -1 || elementName.indexOf("ListRole") != -1) {
                        tabStaffErr = true;
                    }
                    if (elementName.indexOf("StaffOnboardInfo.") != -1 || elementName.indexOf("WorkingProcess.") != -1 || elementName.indexOf(".PolicyId") != -1 || elementName.indexOf(".PaymentMethod") != -1 || elementName.indexOf(".PaymentForm") != -1) {
                        tabWpErr = true;
                    }
                    if (elementName.indexOf(".PolicyId") != -1 || elementName.indexOf(".PaymentMethod") != -1 || elementName.indexOf(".PaymentForm") != -1) {
                        tabSalaryErr = true;
                    }
                }
                if (tabWpErr == true) {
                    $("#add-working-process-tab-warning").removeClass("hidden");
                } else {
                    $("#add-working-process-tab-warning").addClass("hidden");
                }
                if (tabStaffErr == true) {
                    $("#add-staff-profile-tab-warning").removeClass("hidden");
                }
                else {
                    $("#add-staff-profile-tab-warning").addClass("hidden");
                }
                if (tabSalaryErr == true) {
                    $("#add-benefits-tab-warning").removeClass("hidden");
                }
                else {
                    $("#add-benefits-tab-warning").addClass("hidden");
                }
                RenderError(validations, "create-staff-form");
            }
            else
            {
                if (response.Success == true) {
                    ShowMessage(true, msgSuccessful, response.Message, 0, 4000, 0);
                }
            }
        }
    });
}
function goToAddForm() {
    var data = $('form#addStaffform').serializeObject();
    //Bind to sessionStorage 
    sessionStorage.setItem("addFormStaff", JSON.stringify(data));
    if ($("#IsOnboarding").val() != undefined && $("#IsOnboarding").val().toLowerCase() == "true") {
        window.location.href = "/Staff/Add?type=1";
    }
    else {
        window.location.href = "/Staff/Add";
    }
}

function saveStaffInfomation() {
    $("#staff-info-form span[name*='-error']").text("");
    $.ajax({
        type: "POST",
        url: "/Staff/SaveStaffInfomation",
        data: $('form#staff-info-form').serializeObject(),
        success: function (response) {
            if (response.Invalid) {
                var validations = response.Result;
                RenderError(validations, "staff-info-form");
            }
            else {
                if (response.Success == true) {
                    ShowMessage(true, msgSuccessful, response.Message, 0, 4000, 0);
                }
            }
        }
    });
}
function editStaff(id) {
    window.location.href = "/Staff-Detail?StaffId=" + id + "&activetab=1&viewtype=1"
}