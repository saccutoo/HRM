var _global = {
    init:function() {
        runAllForms();
        pageSetUp();
        hideActionNotPermission();

        $(document).on('click', '.close-reload', function () {
            location.reload();
        });

        $(document).on('click', 'a.action', function () {
            var href = this.href;
            var object = this;

            var isConfirm = $(object).hasClass("confirm");
            if (isConfirm) {
                if (!confirm(_global.getMsg('Global_ConfirmDelete'))) {
                    return false;
                }
            }

            $.ajax({
                url: href,
                beforeSend: function () {
                    _global.blockUI('#content');
                },
                success: function (result) {
                    if (result.Json.IsSuccess) {
                        location.reload();
                    } else {
                        _global.showMsgErr(result.Json.Message);
                    }
                    _global.unBlockUI('#content');
                },
                error: function (xhr, status, text) {
                    _global.unBlockUI('#content');
                    _global.showMsgErr(_global.getMsg("Global_ActionFailed"));
                }
            });
            return false;
        });

        $(document).on('click', 'a.dialog,a.dialog-fullscreen', function () {
            var href = this.href;
            var title = this.title;
            var object = this;

            $.ajax({
                url: href,
                beforeSend: function () {
                    _global.blockUI('#content');
                },
                success: function (result) {
                    if ($(object).hasClass("dialog-fullscreen")) {
                        OpenDialogFullScreen(result, title);
                    } else {
                        OpenDialog(result, title, 0, 0);
                    }
                    _global.unBlockUI('#content');
                },
                error: function (xhr, status, text) {
                    _global.unBlockUI('#content');
                    _global.showMsgErr(_global.getMsg("Global_ActionFailed"));
                }
            });
            return false;
        });

        function OpenDialog(data, title) {
            var div = document.createElement('div');
            $("body").prepend(div);

            $(div).html(data).dialog({
                title: title,
                resizable: true,
                width: "auto",
                height: "auto",
                modal: true,
                show: {
                    effect: "fade",
                    duration: 400
                },
                closeOnEscape: true,
                close: function (event, ui) {
                    this.remove();
                }
            }).dialog("open");

            bindForm($(div).attr("id"));
        }

        function OpenDialogFullScreen(data, title) {
            var div = document.createElement('div');
            $("body").prepend(div);

            $(div).html(data).dialog({
                title: title,
                resizable: false,
                draggable: false,
                height: $(window).height(),
                width: '100%',
                closeOnEscape: true,
                open: function (event, ui) {
                    _global.hideBodyScroll();
                },
                close: function (event, ui) {
                    _global.showBodyScroll();
                    this.remove();
                }
            }).dialog("open");
            bindForm($(div).attr("id"));
        }

        function bindForm(dialogId) {
            //đăng ký event submit form dialog    
            var dialogId2 = "#" + dialogId;
            $("form", dialogId2).submit(function () {
                $.ajax({
                    url: this.action,
                    type: this.method,
                    data: $(this).serialize(),
                    success: function (result) {
                        if (result.Json != undefined && result.Json.IsSuccess) {
                            _global.unBlockUI(dialogId2);
                            location.reload();
                            return true;

                            ////cập nhật thành công => redirect/reload
                            //if (result.url !== undefined && result.url !== null && result.url !== '') {
                            //    window.href = result.url;
                            //} else {
                            //    // location.reload();
                            //    window.href = window.location.href;
                            //}
                        } else {
                            $(dialogId2).html(result);
                            bindForm(dialogId);

                            //scroll to err msg
                            $(dialogId2).animate({
                                scrollTop: $('form').offset().top
                            }, 'slow');
                        }
                    },
                    beforeSend: function () {
                        _global.blockUI(dialogId2);
                    },
                    error: function (xhr, status, text) {
                        _global.unBlockUI(dialogId2);
                        _global.showMsgErr(_global.getMsg("Global_ActionFailed"));
                    }
                });
                return false;
            });
        }   
    },
    initModel: function () {
        runAllForms();
        pageSetUp();
        hideActionNotPermission();
    },
    setCookie:function(cname, cvalue, exdays) {
        var d = new Date();
        d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
        var expires = "expires=" + d.toUTCString();
        document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
    },
    getCookie:function(cname) {
        var name = cname + "=";
        var ca = document.cookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) == ' ') {
                c = c.substring(1);
            }
            if (c.indexOf(name) == 0) {
                return c.substring(name.length, c.length);
            }
        }
        return "";
    },
    hideBodyScroll: function () {
        $("htm,body").css('height', '100%');
        $("html,body").css('overflow', 'hidden');
    },
    showBodyScroll: function () {
        $("html,body").css('height', '');
        $("html,body").css('overflow', '');
    },
    blockUI: function (control) {
        $(control).block({
            message: '<i class="icon-lock"></i> Loading...',
            css: {
                border: "none",
                padding: "15px",
                backgroundColor: "#000",
                "-webkit-border-radius": "10px",
                "-moz-border-radius": "10px",
                color: "#fff"
            }
        });
    },
    unBlockUI: function (control) {
        $(control).unblock();
    },
    successMsg: function (msg) {
        $.smallBox({
            title: _global.getMsg('Global_Success'),
            content: msg,
            color: "#3c763d",            
            timeout: 3000
        });
    },
    showMsgErr: function (msg) {
        $.smallBox({
            title: _global.getMsg('Global_Err'),
            content: msg,            
            color: "#a94442",
            timeout: 3000
        });
    },
    getLabel: function(key) {
        return _dicLabel[key];
    },
    getMsg: function (key) {
        return _dicMsg[key];
    }
}


function hideActionNotPermission() {
    //button submit form
    $("form").each(function () {
        var url = $(this).attr("action")||"";
        if (url.indexOf("?") > -1) {
            url = url.substr(0, url.indexOf("?"));
        }
        if (!isExistAction(url)) {
            $(this).find("button").hide();
            $(this).find("button[type=\"submit\"]").hide();
            $(this).find("input[type=\"submit\"]").hide();
        }        
    });

    //tag a model dialog    
    $("a.dialog").each(function () {
        var url = $(this).attr("href") || "";
        if (url.indexOf("?") > -1) {
            url = url.substr(0, url.indexOf("?"));
        }        
        if (!isExistAction(url)) {
            $(this).hide();
        }
    });
}

function isExistAction(url) {    
    url = url.toUpperCase();
    var count = 0;

    //TH1: url đẩy đủ: /Config/Index
    if ($.inArray(url, _arrAction) >= 0) {
        count = count + 1;
    }

    //TH2: url thiếu Action: /Config
    var urlTemp = url+"/INDEX";
    if ($.inArray(urlTemp, _arrAction) >= 0) {
        count = count + 1;
    }

    //TH3: url Không có '/':  Config/Index
    urlTemp = "/" + url;
    if ($.inArray(urlTemp, _arrAction) >= 0) {
        count = count + 1;
    }

    //TH4: url Không có '/' và  action:  Config
    urlTemp = "/" + url + "/INDEX";
    if ($.inArray(urlTemp, _arrAction) >= 0) {
        count = count + 1;
    }

    return count > 0;
}