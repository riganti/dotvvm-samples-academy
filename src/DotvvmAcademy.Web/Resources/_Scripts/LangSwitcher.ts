declare let dotvvm;

dotvvm.events.init.subscribe(function () {
    $('.lang-switcher').click(function (e) {
        $('.lang-switcher_list').toggleClass('open'); 
    });
});

