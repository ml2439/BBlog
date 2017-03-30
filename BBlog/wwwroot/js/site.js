// Write your Javascript code.
$(function () {
    $(document).scroll(function () {
        var $nav = $(".navbar-fixed-top");
        // var $intro = $(".intro-header");
        $nav.toggleClass('scrolled', $(this).scrollTop() > 0);
    });
});