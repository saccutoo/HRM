var table_thead_left = 0;
$(document).ready(function () {

    table_thead_left = $('.table-thead').offset().left;
    var w = $('table.scroll').width();
    if (w > $("#content").width()) {
        $("#content").width(w);
        //$('#ribbon').css({ 'width': ($("#content").width() + 30) + "px" });
    }
    else {
        $("#content").css({ 'width': '100%' });
        //  $('#ribbon').css({ 'width': '100%' });
    }

    if ($(window).scrollTop() > 32) {
        $('#menuheaderlist').css({ 'margin-left': '0px' });
        $('#menuheaderlist').addClass('stick-header-list');
    }
    else {
        $("#menuheaderlist").removeClass('stick-header-list');
    }

    $('table .table-thead').css({ 'width': w });
    $('#hd-list').css({ 'width': ($(window).width() - 40) });

    $("html").css({ 'overflow-x': 'scroll' });
    $("#header").css({ 'width': '100%' });
    CallAllTableJs();
});

function CallAllTableJs() {
    // scrollTable();
    var w = $('table.scroll').width();
    $('table .table-thead').css({ 'width': w });
    $("#pagecontent").css({ 'width': ($(window).width() - $('table.scroll').offset().left) });
    var window_top = $(window).scrollTop();
    table_thead_left = $('table.scroll').offset().left;
    if (window_top > 20) {
        var x = 0 - $('table.scroll').scrollLeft();
        $(".table-thead").offset({
            left: x + table_thead_left
        });

        $(".table-thead").addClass('stick-grid');
        $('table .table-thead').css({ 'top': $("#menuheaderlist").height() + 20 + "px" });
        //if ($("#menuheaderlist").hasClass("stick-header-list")) {
        //    $(".table-thead").addClass('stick-grid');
        //} else {
        //    $(".table-thead").removeClass('stick-grid');
        //}
        var documentScrollLeft = $(document).scrollLeft();
        if (documentScrollLeft > 0) {
            $('table .table-thead').css({ 'margin-left': (documentScrollLeft) + "px" });
        } else {
            $('table .table-thead').css({ 'margin-left': 37 + "px" });
        }
    }
    ResizeColumn();
}


$(window).scroll(function () {

    //CallAllTableJs();
    //var td = $("table.scroll").find('tbody tr:eq(0) td');
    //var th = $("table.scroll").find('thead tr th');

    //for (var i = 0; i < td.length; i++) {
    //    for (var i = 0; i < td.length; i++) {
    //        var wt = $(th[i]).width();
    //        $(td[i]).attr({ 'width': wt-10 });

    //    }
    //}
    var s = $("#menuheaderlist");
    if (s != undefined) {
        var pos = s.position();

        var windowpos = $(window).scrollTop();
        if (pos != undefined) {
            if (windowpos > parseInt(pos.top)) {
                s.css({ 'margin-left': '0px' });
                s.addClass('stick-header-list');
            } else {
                s.removeClass('stick-header-list');
            }
        }
    }

    //if ($(window).scrollTop() > 32) {


    //    //scrolify($('table.scroll'));

    //}
    //else {

    //}
    scrollTable();
    //console.log($(window).scrollTop());
});

$(window).resize(function () {



    //$("html").css({ 'overflow-x': 'scroll' });
    //table_thead_left = $('table.scroll').offset().left;
    //$("#pagecontent").css({ 'width': ($(window).width() - $('table.scroll').offset().left) });
    //ResizeColumn();
    //scrollTable();



});



function scrollTable() {
    var $window = $(window),
       $headerList = $('#hd-list');
    var window_top = $window.scrollTop();
    var div_hd_list = 0;
    var headerListHeight = 0;
    if ($headerList != null && $headerList.length) {
        div_hd_list = $headerList.offset().top;
        headerListHeight = $headerList.height() + 30;
    }

    var documentScrollLeft = $(document).scrollLeft();
    //Hiển thị title table và kéo theo trình duyệt

    if ($("#menuheaderlist").hasClass("stick-header-list")) {
        $(".table-thead").addClass('stick-grid');
    } else {
        $(".table-thead").removeClass('stick-grid');
    }

    if (window_top > 0) {

        //  $(".table-thead").css({ 'width': $('table.scroll').width() });
        var top = headerListHeight;
        $(".table-thead").css({ top: top });
        var x = 0 - $('table.scroll').scrollLeft();
        $(".table-thead").offset({
            left: x + table_thead_left
        });

    }
    else {
        var wt = $('table.scroll').width();
        var w = $(window).width();

        // $(".table-thead").css("width", "");
        $(".table-thead").css({ 'left': "" });
        if ((documentScrollLeft) <= (wt - (w - 40))) {
            $("#menuheaderlist").css({ 'margin-left': (documentScrollLeft - 10) + "px" });


        }
        $('.breadcrumb').css({ 'margin-left': (documentScrollLeft - 10) + "px" });

        if (documentScrollLeft > 0) {
            $('#header').css({ 'margin-left': (documentScrollLeft) + "px" });
        } else {
            $('#header').css({ 'margin-left': 0 + "px" });
        }
    }
    //Kéo phân trang theo table
    if (documentScrollLeft - 10 < 0) {
        documentScrollLeft = 0;
        $("#pagenumber").css({ 'margin-left': (documentScrollLeft) + "px" });

    } else {
        $("#pagenumber").css({ 'margin-left': (documentScrollLeft - 10) + "px" });


    }





}

function ResizeColumn() {
    //var td = $("table.scroll").find('tbody tr:eq(0) td');
    //var th = $("table.scroll").find('thead tr th');

    ////for (var i = 0; i < td.length; i++) {
    //for (var i = 0; i < td.length; i++) {
    //    var wt = $(th[i]).width();
    //    console.log(wt);
    //    $(td[i]).css({ 'width': wt + "px" });

    //}
    //// }

    var $table = $('table.scroll'),
         $bodyCells = $table.find('thead tr:first').children(),
         colWidth;

    // Adjust the width of thead cells when window resizes
    // Get the tbody columns width array



    colWidth = $bodyCells.map(function () {
        return $(this).width();
    }).get();
    //  Set the width of thead columns
    //$table.find('thead tr').children().each(function (i, v) {
    //    $(v).width((colWidth[i]));
    //});

    $table.find('tbody tr').children().each(function (i, v) {
        $(v).width((colWidth[i]));
    });
}
