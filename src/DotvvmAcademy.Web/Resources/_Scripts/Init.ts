var dotvvm = (<any>window).dotvvm;

dotvvm.events.init.subscribe(function () {
    $('.lang-btn').click(function () {
        $('.lang-switcher_list').toggleClass('open'); 
    });
});

