declare let dotvvm;

namespace DotvvmAcademy {
    export function setLoading(element: Element) {
        element.classList.remove("with-tooltip");
        element.classList.add("with-loading");
    }

    export function setSuccess(element: Element) {
        element.classList.remove("with-loading");
        window.setTimeout(() => element.classList.add("success"), 50);
    }

    export function setWithTooltip(element: Element) {
        element.classList.remove("with-loading");
        window.setTimeout(() => element.classList.add("with-tooltip"), 50);
    }

    export function redirect() {
        let viewModel = dotvvm.viewModels.root.viewModel;
        let language = viewModel.Language().Moniker();
        let lessonMoniker = viewModel.LessonMoniker();
        let nextStep = viewModel.Step().NextStep();
        window.location.pathname = `/${language}/${lessonMoniker}/${nextStep}`;
    }
}

dotvvm.events.beforePostback.subscribe(args => {
    // ensure this is the right postback
    let validationButton = document.querySelector("#validation-button");
    if (validationButton != args.sender) {
        return;
    }

    DotvvmAcademy.setLoading(validationButton);
})


dotvvm.events.afterPostback.subscribe(args => {
    // ensure this is the right postback
    let validationButton = document.querySelector("#validation-button");
    if (validationButton != args.sender) {
        return;
    }

    if (args.viewModel.CodeTask().IsCodeCorrect()) {
        DotvvmAcademy.setSuccess(args.sender);
        window.setTimeout(() => {
            DotvvmAcademy.redirect();
        }, 1500);
    } else {
        DotvvmAcademy.setWithTooltip(args.sender);
    }
});
