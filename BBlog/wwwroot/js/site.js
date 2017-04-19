// Write your Javascript code.
$(function () {
    $(document).scroll(function () {
        var $nav = $(".navbar-fixed-top");
        $nav.toggleClass('scrolled', $(this).scrollTop() > 0);
    });
});


// Ajax request
$(function () {
    $('#sidebar').on('click', '.ajaxcall a', function () {
        var url = $(this).attr('href');
        $('#mainContent').load(url);
        return false;
    })
})