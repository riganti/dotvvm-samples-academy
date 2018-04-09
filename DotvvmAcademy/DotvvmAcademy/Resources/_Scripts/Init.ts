var dotvvm = (<any>window).dotvvm;

dotvvm.events.init.subscribe(function () {
    $('.lang-btn').click(function (e) {
        $('.lang-switcher_list').toggleClass('open'); 
    });
});

