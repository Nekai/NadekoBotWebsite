function stickyNav() {
    if ($(window).width() > 900) {
        if ($(this).scrollTop() > 370) {
            $(".main-nav").addClass("navbar-fixed-top");
        }
        else {
            $(".main-nav").removeClass("navbar-fixed-top");
        }
    } else {
        $(".main-nav").removeClass("navbar-fixed-top");
    }
}
$(window).resize(function () {
    stickyNav();
});
$(document).scroll(function () {
    stickyNav();
});