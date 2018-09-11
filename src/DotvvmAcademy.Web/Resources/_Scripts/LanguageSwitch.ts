declare let dotvvm;

dotvvm.events.init.subscribe(function () {
    $('.lang-switcher').click(function (e) {
        $('.lang-switcher_list').toggleClass('open');
    });
});


$(document).click(function (e) {
    if ($(e.target).closest($('.lang-switcher')).length == 0) {
        $('.lang-switcher_list').removeClass('open');
    }
})