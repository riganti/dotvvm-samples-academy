ko.bindingHandlers["dotvvm-svg"] = {
    init: function (element: HTMLElement, valueAccessor: KnockoutObservable<string>) {
        valueAccessor();
    },
    update: function (element: HTMLElement, valueAccessor: KnockoutObservable<string>) {
        var url = valueAccessor();
        var request = new XMLHttpRequest();
        request.open("GET", url, true);
        request.onreadystatechange = function () {
            if (request.readyState === 4) {
                if (request.status === 200 || request.status === 0) {
                    var container = document.createElement("div");
                    container.innerHTML = request.response;
                    element.innerHTML = container.firstElementChild.innerHTML;
                    for (var property in container.firstElementChild) {
                        element[property] = container.firstElementChild[property];
                    }
                }
            }
        };
        request.send();
    }
};
