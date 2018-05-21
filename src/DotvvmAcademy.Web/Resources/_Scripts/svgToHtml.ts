declare var ko;

ko.bindingHandlers["svg"] = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        var url = valueAccessor();
        var request = new XMLHttpRequest();
        request.open("GET", url, true);
        request.onreadystatechange = function () {
            if (request.readyState === 4) {
                if (request.status === 200 || request.status === 0) {
                    //element.outerHTML = request.response;
                    $(element).replaceWith(request.response);
                }
            }
        };
        request.send();
    }
};