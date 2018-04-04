var dotvvm = (<any>window).dotvvm;
var navToggleBtn = $('.nav-toggle-btn');
var navTop = $('nav.navigation-on-top');
var navHeight = navTop.innerHeight();
var navBody = $('.navigation-on-top > .navigation-body');
var headerHeight = $('header').innerHeight();

dotvvm.events.init.subscribe(function () {
    navToggleBtn.click(() => {
        if (navTop.hasClass('nav-open')) {
            closNavSmaller();
        } else {
            openNavSmaller();
            registerNavClickOutside();
        }
    });
});

function closNavSmaller() {
    navTop.removeClass('nav-open');
    navTop.css("height", navHeight);
}

function openNavSmaller() {
    navHeight = navTop.innerHeight();
    navTop.addClass('nav-open');
    var navOpen = $('.navigation-on-top.nav-open');
    navOpen.css("height", navBody.innerHeight());
}

function registerNavClickOutside() {
    $(window).on('click', e => {
        if (!$(e.target).parents("nav.navigation-on-top").length) {
            closNavSmaller();
        }
    });
}
