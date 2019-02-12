declare let dotvvm;

dotvvm.events.init.subscribe(function () {
    dotvvm.postbackHandlers["academy-loading"] = function (handlerOptions) {
        return {
            execute: function (callback, options) {
                var handler = this;
                return new Promise(function (resolve, reject) {
                    handler.apply(options.sender);
                    callback().then(
                        v => {
                            handler.reset(options.sender);
                            resolve(v);
                        },
                        v => {
                            handler.reset(options.sender);
                            reject(v);
                        });
                });
            },
            apply: function (element: HTMLElement) {
                element.classList.remove(handlerOptions["removed-classes"]);
                window.setTimeout(() => element.classList.add(handlerOptions["added-classes"]), 50);
            },
            reset: function (element: HTMLElement) {
                element.classList.remove(handlerOptions["added-classes"]);
                window.setTimeout(() => element.classList.add(handlerOptions["removed-classes"]), 50);
            }
        }
    };
})