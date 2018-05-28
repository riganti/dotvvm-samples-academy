var dotvvm = (<any>window).dotvvm;

dotvvm.events.init.subscribe(function () {
    $('.lang-switcher').click(function (e) {
        $('.lang-switcher_list').toggleClass('open'); 
    });
});

