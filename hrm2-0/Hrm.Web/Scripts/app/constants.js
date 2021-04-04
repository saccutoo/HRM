function CreatePopup(options) {
    $('.hrmv2-modal').removeClass("right");
    $(".hrmv2-modal").modal('hide');

    if (options.align !== null && options.align == "right") {
        $('.hrmv2-modal').addClass("right");
        $(".modal-dialog").css({ 'width': options.width });
    }
    if (options.width != null && options.align == "center") {
        var left = ($(window).width() - options.width) / 2;
        if (left < 0) {
            left = 0;
        }
        $(".modal-content").css({ 'width': options.width });
        $(".modal-dialog").css({ 'width': options.width });

    }



    //if (options.title != null) {
    //    $("#myModalLabel").html(options.title);
    //}
    if (options.isNotificationPoup == true) { $('.hrmv2-modal .close').css({ 'display': 'block' }) }
    else {
        $('.hrmv2-modal .close').css({ 'display': 'none' })
    }
    if (options.data == null) {
        options.data = {};
    }
    if (options.url == null && options.url != undefined && options.url == '') {
        return;
    }
    if (options.isDataTypeJson) {
        $.ajax(
 {
     url: options.url,
     type: "POST",
     data: options.data,
     async: false,
     success: function (response) {
         $("#contentModel").html(response);
         $("#myModal").modal('show');
         if ($("#" + options.scrollTo).length > 0) {
             setTimeout(function () {
                 $('.modal-content.hrm-v2-scroll').animate({
                     scrollTop: $("#" + options.scrollTo).offset().top - 200
                 }, 'slow');
             }, 300);
         }
     }
 });
    }
    else {
        $.ajax(
 {
     url: options.url,
     type: "POST",
     contentType: "application/json; charset=utf-8",
     data: options.data,
     async: false,
     success: function (response) {
         $("#contentModel").html(response);
         $("#myModal").modal('show');
         if ($("#" + options.scrollTo).length > 0) {
             setTimeout(function () {
                 $('.modal-content.hrm-v2-scroll').animate({
                     scrollTop: $("#" + options.scrollTo).offset().top - 200
                 }, 'slow');
             }, 300);
         }
     }
 });
    }


    var $form = $("#" + options.idform);
    $form.find(".btnSubmit").on("click", function () {
        $form.find(".btnSubmit").hide();
        showProcessing();
        var postData = $form.serialize();
        var formURL = $form.attr("action");

        $.ajax({
            url: formURL,
            type: "POST",
            data: $form.serialize(),
            success: function (result) {
                $form.find(".btnSubmit").show();
                if (result.Success == true) {
                    $("#myModal").modal('hide');

                    if (options.Redirect != undefined && options.Redirect == true) {
                        if (options.UrlRedirect != undefined && options.UrlRedirect == true) {
                            window.location.href = options.UrlRedirect;
                        }
                    }
                    else {
                        ReloadData(options.urlback, options.databack, options.divload, options.fnNameReload);
                    }
                } else {
                    jQuery('#' + $form.attr('id') + ' .error').empty();
                    $.each(result.Errors, function (key, value) {

                        jQuery('#' + $form.attr('id') + ' span[data-valmsg-for="' + key + '"]').addClass('field-validation-error').removeClass('field-validation-valid').html('<span for="' + key + '" class="error">' + value + '</span>');
                    });
                }

            }
        });
    });

    $form.find(".btnCancel").on("click", function () {
        $("#myModal").modal('hide');
        $("#contentModel").empty();
    });
}

function ReloadData(urlBank, dataBack, divLoad, fnNameReload) {
    if (dataBack == null) {
        dataBack = {}
    }
    $.ajax(
    {
        url: urlBank,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: dataBack,
        async: false,
        success: function (response) {
            $("#myModal").modal('hide');
            $("#" + divLoad).html(response);
            if (fnNameReload != undefined && fnNameReload != null) {
                window[fnNameReload]();
            }
        }
    });
}
//$(document).ready(function () {
//    $("button[data-dismiss='modal']").click(function () {
//        //$(this).closest(".mymodal").removeClass("min");
//        //$(".container").removeClass($apnData);
//        //$(this).next('.modalMinimize').find("i").removeClass('fa fa-clone').addClass('fa fa-minus');
//        $('.modal.mymodal').modal('hide');
//    });
//});

//var $content, $modal, $apnData, $modalCon;
//$content = $(".min");
//$(".modalMinimize").on("click", function () {
//    $modalCon = $(this).closest(".mymodal").attr("id");
//    $apnData = $(this).closest(".mymodal");
//    $modal = "#" + $modalCon;
//    $(".modal-backdrop").addClass("display-none");
//    $($modal).toggleClass("min");
//    if ($($modal).hasClass("min")) {
//        $(".minmaxCon").append($apnData);
//        $(this).find("i").toggleClass('fa-minus').toggleClass('fa-clone');
//    }
//    else {
//        $(".container").append($apnData);
//        $(this).find("i").toggleClass('fa-clone').toggleClass('fa-minus');
//    };

//});

/*Get parram on Url*/
function getUrlParameter(sParam) {
    var sPageURL = window.location.search.substring(1),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0].toLowerCase() === sParam.toLowerCase()) {
            return sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]);
        }
    }
};

/*End*/
if (!String.prototype.format) {
    String.prototype.format = function () {
        var args = arguments;
        return this.replace(/{(\d+)}/g, function (match, number) {
            return typeof args[number] != 'undefined'
              ? args[number]
              : match
            ;
        });
    };
}

function RenderError(validations, formName) {
    for (var i = 0; i < validations.length; i++) {
        var elementName = validations[i].ColumnName;
        if (elementName != "" && elementName != undefined && elementName != null) {
            var element = $("#" + formName + " span[name='" + elementName + "-error']");
            $(element).text(validations[i].ErrorMessage.format($(element).attr("label-name")));
        }
    }
}
function showMenu(button, menu, e) {
    var container = $(menu);
    var buttonTarget = $(button);
    if (buttonTarget.is(e.target) || buttonTarget.has(e.target).length !== 0) {
        if (container.css('display') == 'none') {
            container.css('display', 'block');
        }
    }
    else
        if (!container.is(e.target) && container.has(e.target).length === 0) {
            container.hide();
        }
}
function ShowMessage(isSuccess, title, content, timeStart, timeEnd, isWarning) {
    setTimeout(function () {
        toastr.options = {
            closeButton: true,
            progressBar: true,
            showMethod: 'slideDown',
            timeOut: timeEnd
        }
        if (isSuccess === true) {
            toastr.success(content, title);
        } else {
            if (isWarning == 1) {
                toastr.warning(content, title);
            } else {
                toastr.error(content, title);
            }
        }
    }, timeStart);
}

function showFilter(tableName, tableUrl, isFilter, groupId) {
    var options = {
        id: 'frmFilter',
        isNotificationPoup: false,
        align: "right",
        url: '/Filter/ShowFilter',
        width: 600,
        data: "{tableName: '" + tableName + "',tableUrl: '" + tableUrl + "',  isFilter: '" + isFilter.toString() + "', groupId: '" + groupId + "'}",
        idform: 'frm-filter',
    };
    CreatePopup(options);
}