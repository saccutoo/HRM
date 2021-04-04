
function StaffInfoShowMore(isShow) {
    if (isShow) {
        $('.hrmv2-section-collapse').addClass('open');
    }
    else {
        $('.hrmv2-section-collapse').removeClass('open');
    }

}
function StaffCareShowMore(isShow) {
    if (isShow) {
        $('.hrmv2-section-staff-care').addClass('open');
    }
    else {
        $('.hrmv2-section-staff-care').removeClass('open');
    }

}

function changePassword() {
    var options = {
        id: 'frmChangePassword',
        isNotificationPoup: false,
        align: "center",
        url: '/Authentication/ChangePassword',
        width: 600,
        //data: '{GroupId: ' + coaTypeId + '}',
        idform: 'frm-add-Organization',
        //urlback: '/Contract/GetContractCoaInfoList',
        //databack: '{contractId:' + contractId + '}',
        //divload: 'GridContractCoaInfo',
        //fnNameReload: 'ResetSelectCoaInfo'
    };
    CreatePopup(options);
}
function relationDetailView(id) {
    var options = {
        id: 'frmRelationShip',
        isNotificationPoup: true,
        align: "center",
        url: '/Staff/_RelationShipView',
        width: 600,
        data: '{Id: ' + id + '}',
        idform: 'frm-relation-ship-view',
        //urlback: '/Contract/GetContractCoaInfoList',
        //databack: '{contractId:' + contractId + '}',
        //divload: 'GridContractCoaInfo',
        //fnNameReload: 'ResetSelectCoaInfo'
    };
    CreatePopup(options);
}

function sectionCheckbox(sectionName) {
    var cbVal = $('#' + sectionName).is(":checked");
    if (cbVal) {
        $('.conntent-for-' + sectionName).removeClass('hrm-v2-display-none');

    }
    else
        $('.conntent-for-' + sectionName).addClass('hrm-v2-display-none');
}
function viewDetailWorkingProcess(id, viewType) {
    if (!viewType) {
        viewType = 0;
    }
    var options = {
        id: 'frmWorkingPressDetail',
        isNotificationPoup: true,
        align: "center",
        url: '/WorkingProcess/GetWorkingProcessDetailById',
        width: 1050,
        data: '{id: ' + id + ',viewType: ' + viewType + '}',
        idform: 'frm-working-process-detail',
        //urlback: '/Contract/GetContractCoaInfoList',
        //databack: '{contractId:' + contractId + '}',
        //divload: 'GridContractCoaInfo',
        //fnNameReload: 'ResetSelectCoaInfo'
    };
    CreatePopup(options);
}
function editWorkingProcess(id) {
    var options = {
        id: 'frmWorkingPressDetail',
        isNotificationPoup: false,
        align: "right",
        url: '/WorkingProcess/GetWorkingProcessDetailById',
        width: 1050,
        data: '{id: ' + id + ',viewType: ' + 1 + '}',
        idform: 'frm-working-process-detail',
        //urlback: '/Contract/GetContractCoaInfoList',
        //databack: '{contractId:' + contractId + '}',
        //divload: 'GridContractCoaInfo',
        //fnNameReload: 'ResetSelectCoaInfo'
    };
    CreatePopup(options);
}
function salaryDetail(id) {
    var options = {
        id: 'frmSalaryDetail',
        isNotificationPoup: true,
        align: "center",
        url: '/Staff/_SalaryDetail',
        width: 600,
        data: '{id: ' + id + '}',
        idform: 'frm-salary-detail',
        //urlback: '/Contract/GetContractCoaInfoList',
        //databack: '{contractId:' + contractId + '}',
        //divload: 'GridContractCoaInfo',
        //fnNameReload: 'ResetSelectCoaInfo'
    };
    CreatePopup(options);
}
function allowanceDetail(id) {
    var options = {
        id: 'frmAllowanceDetail',
        isNotificationPoup: true,
        align: "center",
        url: '/Staff/_AllowanceDetail',
        width: 600,
        data: '{id: ' + id + '}',
        idform: 'frm-allowance-detail',
        //urlback: '/Contract/GetContractCoaInfoList',
        //databack: '{contractId:' + contractId + '}',
        //divload: 'GridContractCoaInfo',
        //fnNameReload: 'ResetSelectCoaInfo'
    };
    CreatePopup(options);
}
function benefitDetail(id) {
    var options = {
        id: 'frmBenefitDetail',
        isNotificationPoup: true,
        align: "center",
        url: '/Staff/_BenefitDetail',
        width: 600,
        data: '{id: ' + id + '}',
        idform: 'frm-benefit-detail',
        //urlback: '/Contract/GetContractCoaInfoList',
        //databack: '{contractId:' + contractId + '}',
        //divload: 'GridContractCoaInfo',
        //fnNameReload: 'ResetSelectCoaInfo'
    };
    CreatePopup(options);
}
function rewardDetail(id, viewType) {
    if (!viewType) {
        viewType = 0;
    }
    var options = {
        id: 'frmRewardDetail',
        isNotificationPoup: true,
        align: "center",
        url: '/Staff/_RewardDetail',
        width: 600,
        data: '{id: ' + id + ',viewType: ' + viewType + '}',
        idform: 'frm-reward-detail',
        //urlback: '/Contract/GetContractCoaInfoList',
        //databack: '{contractId:' + contractId + '}',
        //divload: 'GridContractCoaInfo',
        //fnNameReload: 'ResetSelectCoaInfo'
    };
    CreatePopup(options);
}
function disciplineDetail(id, viewType) {
    if (!viewType) {
        viewType = 0;
    }
    var options = {
        id: 'frmDisciplineDetail',
        isNotificationPoup: true,
        align: "center",
        url: '/Staff/_DisciplineDetail',
        width: 600,
        data: '{id: ' + id + ',viewType:' + viewType + '}',
        idform: 'frm-discipline-detail',
        //urlback: '/Contract/GetContractCoaInfoList',
        //databack: '{contractId:' + contractId + '}',
        //divload: 'GridContractCoaInfo',
        //fnNameReload: 'ResetSelectCoaInfo'
    };
    CreatePopup(options);
}
function addDiscipline() {
    var options = {
        id: 'frmAddDiscipline',
        isNotificationPoup: false,
        align: "center",
        url: '/Staff/_SaveDiscipline',
        width: 800,
        //data: '{id: ' + id + '}',
        idform: 'frm-add-discipline',
        //urlback: '/Contract/GetContractCoaInfoList',
        //databack: '{contractId:' + contractId + '}',
        //divload: 'GridContractCoaInfo',
        //fnNameReload: 'ResetSelectCoaInfo'
    };
    CreatePopup(options);
}
function addReward() {
    var options = {
        id: 'frmAddReward',
        isNotificationPoup: false,
        align: "center",
        url: '/Staff/_SaveReward',
        width: 800,
        //data: '{id: ' + id + '}',
        idform: 'frm-reward-detail',
        //urlback: '/Contract/GetContractCoaInfoList',
        //databack: '{contractId:' + contractId + '}',
        //divload: 'GridContractCoaInfo',
        //fnNameReload: 'ResetSelectCoaInfo'
    };
    CreatePopup(options);
}
function addWorkingProcess() {
    var options = {
        id: 'frmAddWorkingPress',
        isNotificationPoup: false,
        align: "right",
        url: '/WorkingProcess/_AddWorkingPress',
        width: 1000,
        //data: '{GroupId: ' + coaTypeId + '}',
        idform: 'frm-addworking-press',
        //urlback: '/Contract/GetContractCoaInfoList',
        //databack: '{contractId:' + contractId + '}',
        //divload: 'GridContractCoaInfo',
        //fnNameReload: 'ResetSelectCoaInfo'
    };
    CreatePopup(options);
}
function openEditAllowance(id) {
    var options = {
        scrollTo: 'table-quick-add-StaffAllowancePopupWorkingprocess',
        id: 'frmWorkingPressDetail',
        isNotificationPoup: false,
        align: "right",
        url: '/WorkingProcess/GetWorkingProcessDetailById',
        width: 1050,
        data: '{id: ' + id + ',viewType: ' + 1 + '}',
        idform: 'frm-working-process-detail',
        //urlback: '/Contract/GetContractCoaInfoList',
        //databack: '{contractId:' + contractId + '}',
        //divload: 'GridContractCoaInfo',
        //fnNameReload: 'ResetSelectCoaInfo'
    };
    CreatePopup(options);
}
function openEditBenefit(id) {
    var options = {
        scrollTo: 'table-quick-add-StaffBenefitPopupWorkingprocess',
        id: 'frmWorkingPressDetail',
        isNotificationPoup: false,
        align: "right",
        url: '/WorkingProcess/GetWorkingProcessDetailById',
        width: 1050,
        data: '{id: ' + id + ',viewType: ' + 1 + '}',
        idform: 'frm-working-process-detail',
        //urlback: '/Contract/GetContractCoaInfoList',
        //databack: '{contractId:' + contractId + '}',
        //divload: 'GridContractCoaInfo',
        //fnNameReload: 'ResetSelectCoaInfo'
    };
    CreatePopup(options);
}
function openEditSalary(id) {
    var options = {
        scrollTo: 'salary-section',
        id: 'frmWorkingPressDetail',
        isNotificationPoup: false,
        align: "right",
        url: '/WorkingProcess/GetWorkingProcessDetailById',
        width: 1050,
        data: '{id: ' + id + ',viewType: ' + 1 + '}',
        idform: 'frm-working-process-detail',
        //urlback: '/Contract/GetContractCoaInfoList',
        //databack: '{contractId:' + contractId + '}',
        //divload: 'GridContractCoaInfo',
        //fnNameReload: 'ResetSelectCoaInfo'
    };
    CreatePopup(options);
}
function addExperience() {
    var options = {
        id: 'frmAddExperience',
        isNotificationPoup: false,
        align: "center",
        url: '/Staff/_SaveExperience',
        width: 600,
        //data: '{GroupId: ' + coaTypeId + '}',
        idform: 'frm-experience-press',
    };
    CreatePopup(options);
}
function addCertification() {
    var options = {
        id: 'frmAddCertification',
        isNotificationPoup: false,
        align: "center",
        url: '/Staff/_SaveCertification',
        width: 600,
        //data: '{GroupId: ' + coaTypeId + '}',
        idform: 'frm-certification-press',
    };
    CreatePopup(options);
}
function addHealthInsurance() {
    var options = {
        id: 'frmAddHealthInsurance',
        isNotificationPoup: false,
        align: "center",
        url: '/Staff/_SaveHealthInsurance',
        width: 800,
        data: '{staffId: ' + referenceId + '}',
        idform: 'frm-health-insurance-press',
    };
    CreatePopup(options);
}
function historyHealthInsurance() {
    var options = {
        id: 'frmhistoryHealthInsurance',
        isNotificationPoup: true,
        align: "center",
        url: '/Staff/_HistoryHealthInsurance',
        width: 1000,
        data: '{staffId: ' + referenceId + '}',
        idform: 'frm-health-insurance-history',
    };
    CreatePopup(options);
}
function historySocialInsurance() {
    var options = {
        id: 'frmhistorySocialInsurance',
        isNotificationPoup: true,
        align: "center",
        url: '/Staff/_HistorySocialInsurance',
        width: 1000,
        data: '{staffId: ' + referenceId + '}',
        idform: 'frm-social-insurance-history',
    };
    CreatePopup(options);
}
function historyBenefit() {
    var options = {
        id: 'frmhistoryBenefit',
        isNotificationPoup: true,
        align: "center",
        url: '/Staff/_HistoryBenefit',
        width: 1050,
        data: '{staffId: ' + referenceId + '}',
        idform: 'frm-benefit-history',
    };
    CreatePopup(options);
}
function historyAllowance() {
    var options = {
        id: 'frmhistoryAllowance',
        isNotificationPoup: true,
        align: "center",
        url: '/Staff/_HistoryAllowance',
        width: 1000,
        data: '{staffId: ' + referenceId + '}',
        idform: 'frm-allowance-history',
    };
    CreatePopup(options);
}
function addSocialInsurance() {
    var options = {
        id: 'frmAddSocialInsurance',
        isNotificationPoup: false,
        align: "center",
        url: '/Staff/_SaveSocialInsurance',
        width: 600,
        data: '{staffId: ' + referenceId + '}',
        idform: 'frm-Social-insurance-press',
    };
    CreatePopup(options);
}
function experienceDetail(id, viewType) {
    if (!viewType) {
        viewType = 0;
    }
    var options = {
        id: 'frmexperienceDetail',
        isNotificationPoup: true,
        align: "center",
        url: '/Staff/_ExperienceDetail',
        width: 640,
        data: '{id: ' + id + ',viewType: ' + viewType + '}',
        idform: 'frm-experience-detail',
        //urlback: '/Contract/GetContractCoaInfoList',
        //databack: '{contractId:' + contractId + '}',
        //divload: 'GridContractCoaInfo',
        //fnNameReload: 'ResetSelectCoaInfo'
    };
    CreatePopup(options);
}
function certificationDetail(id, viewType) {
    if (!viewType) {
        viewType = 0;
    }
    var options = {
        id: 'frmCertificationDetail',
        isNotificationPoup: true,
        align: "center",
        url: '/Staff/_CertificationDetail',
        width: 600,
        data: '{id: ' + id + ',viewType: ' + viewType + '}',
        idform: 'frm-certification-detail',
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


