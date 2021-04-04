function Loading()
{
    return pleaseWait({
        logo: '/Vendor/loading/placeholder-transparent.png',
        backgroundColor: 'rgba(0, 0, 0, 0.46)',
        loadingHtml: '<div class="spinner"><div class="double-bounce1"></div><div class="double-bounce2"></div></div>'
    });

    //var dt = Loading();
    //dt.finish(); 

}


function ShowLoader() {
    $(".loader").css("display", "block");
}

function HiddenLoader() {
    $(".loader").css("display", "none");
}

function ShowPopup(selector, url, width, height) {
    selector.colorbox({
        inline: true,
        iframe: false,
        innerWidth: width,
        innerHeight: height,
        href: url,
        escKey: false,
        overlayClose: false
    });
    $('#cboxOverlay .select2').select2({ dropdownParent: $('#cboxOverlay') });
}
function FormatDate(inputDate) {
    var date = new Date(inputDate);
    if (!isNaN(date.getTime())) {
        var day = date.getDate().toString();
        var month = (date.getMonth() + 1).toString();
        // Months use 0 index.
        return (day[1] ? day : '0' + day[0]) +
            '/' +
            (month[1] ? month : '0' + month[0]) +
            '/' +
            date.getFullYear();
    }
};
function AppendToToastr(isSuccess, title, content, timeStart, timeEnd) {
    setTimeout(function () {
        toastr.options = {
            closeButton: true,
            progressBar: true,
            showMethod: 'slideDown',
            timeOut: timeEnd
        }
        if (isSuccess===true) {
            toastr.success(content, title);
        } else {
            toastr.error(content, title);
        }
        

    }, timeStart);
}
function FormatDateTime(inputDateTime) {
    if (inputDateTime === "" || inputDateTime === undefined) {
        return "-";
    }
    var d = new Date(inputDateTime),
        dformat = [
                d.getDate(), d.getMonth() + 1,
                d.getFullYear()
            ].join('/') +
            ' ' +
            [
                d.getHours(),
                d.getMinutes()
            ].join(':');
    return dformat;
};
function FormatOnlyTime(inputDateTime) {
    var d = new Date(inputDateTime),
        dformat = [

            [
                d.getHours(),
                d.getMinutes()
            ].join(':')];
    return dformat;
};

function BoostrapDialogConfirm(title, message, type, callback, dataObj) {
    BootstrapDialog.confirm({
            title: title,
            message: message,
            type: type, // <-- Default value is BootstrapDialog.TYPE_PRIMARY
            closable: true, // <-- Default value is false
            draggable: true, // <-- Default value is false
            btnCancelLabel: 'CANCEL', // <-- Default value is 'Cancel',
            btnOKLabel: 'OK', // <-- Default value is 'OK',
            btnOKClass: 'btn-warning', // <-- If you didn't specify it, dialog type will be used,
            callback: function (result) {
                if (result) {
                    callback(dataObj);
                }
            }
        }
    );
}