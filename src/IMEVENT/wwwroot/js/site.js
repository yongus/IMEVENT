// Write your Javascript code.
//change site color
function changeStyle(url) {
    document.getElementById('stylesheet').href = 'css/' + url;
}

$(document).ready(function () {

// Initiate Chosen select menus
    /*$('select').chosen({
        allow_single_deselect: true,
        disable_search_threshold: 10
    });*/

    //Scroll And sticky Menu   

    window.scrollTo(0, 0);
    if (!!$('#header').offset()) {
        var stickyTop = $('#header').offset().top;
        var windowTop_cus = $(window).scrollTop();

        $(window).scroll(function () {
            var windowTop = $(window).scrollTop();
            if (stickyTop < windowTop) {
                $('.header-wrapper').addClass("subnav-fixed");
            }
            else {
                $('.header-wrapper').removeClass("subnav-fixed");
            }
        });
    }

    $("a[rel=external]").click(function () {
        window.open(jQuery(this).attr('href'));
        return false;
    });

    $('.ifg-close').live('click', function () {
        if (jQuery(this).attr('data-video')) {
            var videoId = jQuery(this).attr('data-video');
            var player = jQueryf(jQuery('#' + videoId)[0]);
            player.api('pause');
        }
    });

});

