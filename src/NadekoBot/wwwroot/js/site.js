function stickyNav() {
    var doc = document.documentElement;
    var top = (window.pageYOffset || doc.scrollTop) - (doc.clientTop || 0);

    var elem = document.getElementById('main_nav');
    if (window.innerWidth >= 768) {
        if (top > 370) {
            elem.classList.add('navbar-fixed-top');
        }
        else {
            elem.classList.remove('navbar-fixed-top');
        }
    } else {
        elem.classList.remove('navbar-fixed-top');
    }
}
window.addEventListener("resize", stickyNav);
document.addEventListener("scroll", stickyNav);