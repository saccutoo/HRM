$(document).ready(function() {
    jQuery(document).on('click', '.mega - dropdown', function(e) {
        e.stopPropagation()
    })

    $('.slide-show-box').slick({
        dots: true,
        infinite: true,
        speed: 300,
        slidesToShow: 1,
        adaptiveHeight: true,
        autoplay: true,
  		autoplaySpeed: 5000
    });
});

function loadingElement() {
    kendo.ui.progress($(document.body), true);
    $('.k-loading-mask').height($(document).height());

};

function stopLoadingElement() {
    kendo.ui.progress($(document.body), false);
};
function checkHeightContent(ele) {
    for (var i = 0; i < $(ele).length; i++) {
        var element = $($(ele)[i]);
        if (element.height() > 130) {
            element.parent().find('.showMore').addClass('active');
            element.css('height', '130px');
            element.addClass('active');
        } else {
            element.parent().find('.showMore').removeClass('active');
        }
    }

}