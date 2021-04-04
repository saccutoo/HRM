function CallAjaxFull(type, url, data, contentType, dataType,callbackAjax) {
    $.ajax({
        type: type,
        url: url,
        data: data,
        contentType: contentType ,//"application/json; charset=utf-8",
        dataType: dataType,// "json", html
        success: function (data) {
            callbackAjax(data);
        },
        complete: function() {
            HideLoadingPage();
        },
        failure: function () {
            alert("fail");
        }
    });
}

function LoadPartialView(url, container, data) {
    $.ajax({
        url: url,
        data: data,
        type: "GET",
        dataType: "html",
        success: function (data) {
            container.html(data);
        },
        complete: function () {
            HideLoadingPage();
        },
        beforeSend: function () {
            ShowLoadingPage();
        }
    });
}
function LoadPartialViewCallBack(url,data,callBack) {
    $.ajax({
        url: url,
        data: data,
        type: "GET",
        dataType: "html",
        success: function (data) {
            callBack(data);
        },
        complete: function () {
            HideLoadingPage();
        },
        beforeSend: function () {
            ShowLoadingPage();
        }
    });
}

function CallAjaxFormData(url, form, callbackAjax) {
    $.ajax({
        url: url,
        data: form,
        processData: false,
        contentType: false,
        type: 'POST',
        success: function(data) {
            callbackAjax(data);
        },
        beforeSend: function () {
            ShowLoadingPage();
        },
        complete: function () {
            HideLoadingPage();
        },
        failure: function () {
            alert("fail");
        }
    });
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
function FormatDateToYearMonthDay(inputDate) {
    if (inputDate === null) {
        return "";
    }
    var date = new Date(inputDate);
    if (!isNaN(date.getTime())) {
        var day = date.getDate().toString();
        var month = (date.getMonth() + 1).toString();
        // Months use 0 index.
        return date.getFullYear() + '/' + (month[1] ? month : '0' + month[0]) + '/' + (day[1] ? day : '0' + day[0]);
    }
};
function FormatDateTime(inputDateTime) {
    if (inputDateTime === null) {
        return "";
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
            btnOKClass: 'btn-primary', // <-- If you didn't specify it, dialog type will be used,
            callback: function (result) {
                if (result) {
                    callback(dataObj);
                }
            }
        }
    );
}

function BoostrapDialogAlert(title, message, type) {
    BootstrapDialog.alert({
            title: title,
            message: message,
            type: type, // <-- Default value is BootstrapDialog.TYPE_PRIMARY
            closable: true, // <-- Default value is false
            draggable: true, // <-- Default value is false
            btnOKLabel: 'OK', // <-- Default value is 'OK',
            btnOKClass: 'btn-warning' // <-- If you didn't specify it, dialog type will be used,
        }
    );
}

function CallAjax(type, url, data, callback) {
    $.ajax({
        type: type,
        url: url,
        data: data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            callback(data);
        },
        complete: function () {
            $(".back-loading").remove();
        },
        beforeSend: function () {
            var loader = $('<div class="back-loading"><img class="img-content"/></div>');
            $("body").prepend(loader);
        },
        failure: function () {
            alert("fail");
        }
    });
}
function CallAjax_WithInputObj(type, url, data, callback, dataObj) {
    $.ajax({
        type: type,
        url: url,
        data: data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            callback(data, dataObj);
        },
        complete: function () {
            $(".back-loading").remove();
        },
        beforeSend: function () {
            var loader = $('<div class="back-loading"><img class="img-content"/></div>');
            $("body").prepend(loader);
        },
        failure: function () {
            alert("fail");
        }
    });
}
function CallAjax_FormData(url, data, callback) {
    $.ajax({
        type: "POST",
        url: url,
        data: data,
        processData: false,
        contentType: false,
        success: function (data) {
            callback(data);
        },
        complete: function () {
            $(".back-loading").remove();
        },
        beforeSend: function () {
            var loader = $('<div class="back-loading"><img class="img-content"/></div>');
            $("body").prepend(loader);
        },
        failure: function () {
            alert("fail");
        }
    });
}

function ConvertMoneyString(data) {
    var value = parseInt(ConvertMoneyStringToFloat(data));
    //    console.log(value)
    return value.toString().replace(new RegExp(',', 'g'), "").replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,");
}

function ConvertMoneyStringToFloat(data) {

    var result = data.toString().replace(new RegExp(',', 'g'), "");
    return parseFloat(result);
}

function validate(evt, self) {
    var maxVal = $(self).attr("max");
    var thisVal = $(self).val();

    var theEvent = evt || window.event;
    if ((evt.keyCode < 48 && evt.keyCode > 57) && (evt.keyCode < 96 && evt.keyCode > 105)) {
        theEvent.preventDefault();
        if (thisVal.length <= 1) {
            $(self).val(0);
        } else {
            var curr = thisVal.substring(0, thisVal.length - 1);
            $(self).val(curr);
        }

    } else {

        if (ConvertMoneyStringToFloat(thisVal) >= ConvertMoneyStringToFloat(maxVal)) {
            $(self).val(ConvertMoneyString(maxVal));
        }
        else if (ConvertMoneyStringToFloat(thisVal) <= 0) {
            $(self).val(0);
        } else {
            $(self).val(ConvertMoneyString(thisVal));
        }
    }
    if (thisVal.length === 0) {
        $(self).val(0);
    }
}
function ShowPopup1(url, width, height) {
    $.colorbox({
        iframe: true,
        innerWidth: width,
        innerHeight: height,
        href: url
    });
}
function ShowPopup(selector, url, width, height) {
    selector.colorbox({
        iframe: true,
        innerWidth: width,
        innerHeight: height,
        href: url
    });
}
function ReloadDatatableAjax(selector,changepage) {
    var _table = $(selector).DataTable();
    _table.ajax.reload(null, changepage);
}
function ReloadDatatableAjaxFromPopup(selector) {
    var _table = window.parent.$(selector).DataTable();
    _table.ajax.reload(null, false);
}

function AppendToToastr(title, content) {
    setTimeout(function () {
        toastr.options = {
            closeButton: true,
            progressBar: true,
            showMethod: 'slideDown',
            timeOut: 4000
        }
        toastr.success(content, title);

    }, 1300);
}

function AppendToToastr_survey(title, content, time, showtime) {
    setTimeout(function () {
        toastr.options = {
            closeButton: true,
            progressBar: true,
            showMethod: 'slideDown',
            timeOut: showtime
        }
        toastr.success(content, title);

    }, time);
}

function autoAdjustTextArea_FormShow(o) {
    o.style.height = o.scrollHeight + 'px';
}


function autoAdjustTextArea(o) {
    o.style.height = '1px';
    o.style.height = o.scrollHeight + 'px';
}
var chars = '\n abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ'.split('');
var randRange = function (min, max) { return max === null ? randRange(0, min) : ~~(Math.random() * (max - min) + min); }
var randChars = function (chrs, len) { return len > 0 ? chrs[randRange(chrs.length)] + randChars(chrs, len - 1) : ''; }
var txtAra = document.getElementsByClassName('noscrollbars')[0];

function checkTime(i) {
    if (i < 10) { i = "0" + i };  // add zero in front of numbers < 10
    return i;
}
function ShowLoadingPage() {
    $('#over').fadeIn('fast', function () {
        $('#over').addClass("overlay");
        $('#proccessing').show();
        $('#proccessing').animate({ 'left': '40%', 'top': '30%' }, 500);
    });
    //setTimeout('hideProcessingTimeout()', 3000);
}

function HideLoadingPage() {
    $('#over').fadeOut('fast', function () {
        $('#proccessing').hide();
    });
}
