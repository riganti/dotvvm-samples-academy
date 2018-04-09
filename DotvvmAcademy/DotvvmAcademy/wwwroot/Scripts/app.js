var dotvvm = window.dotvvm;
dotvvm.events.init.subscribe(function () {
});
var ko = window.ko;
ko.bindingHandlers["svg"] = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        var url = valueAccessor();
        var request = new XMLHttpRequest();
        request.open("GET", url, true);
        request.onreadystatechange = function () {
            if (request.readyState === 4) {
                if (request.status === 200 || request.status === 0) {
                    $(element).replaceWith(request.response);
                }
            }
        };
        request.send();
    }
};
//# sourceMappingURL=app.js.map