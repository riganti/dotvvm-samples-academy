declare let dotvvm;

dotvvm.events.init.subscribe(_ => {
    let buttonElement = document.querySelector("#validation-button");
    new DotvvmAcademy.ValidationButton(buttonElement);
})

namespace DotvvmAcademy {
    export class ValidationButton {
        element: Element;

        constructor(element: Element) {
            this.element = element;

            dotvvm.events.beforePostback.subscribe(args => {
                // ensure this is the right postback
                if (this.element != args.sender) {
                    return;
                }

                this.setLoading();
            })


            dotvvm.events.afterPostback.subscribe(args => {
                // ensure this is the right postback
                if (this.element != args.sender) {
                    return;
                }

                if (args.viewModel.CodeTask().IsCodeCorrect()) {
                    this.setSuccess();
                    window.setTimeout(() => {
                        this.redirect();
                    }, 1500);
                } else {
                    this.setFailed();
                    this.setWithTooltip();
                }
            });
        }

        setLoading() {
            this.element.classList.remove("with-tooltip");
            this.element.classList.add("with-loading");
        }

        setSuccess() {
            this.element.classList.remove("with-loading");
            this.element.classList.remove("failed");
            window.setTimeout(() => this.element.classList.add("success"), 50);
        }

        setFailed() {
            this.element.classList.remove("with-loading")
            this.element.classList.remove("success");
            window.setTimeout(() => this.element.classList.add("failed"), 50);
        }


        setWithTooltip() {
            this.element.classList.remove("with-loading");
            window.setTimeout(() => this.element.classList.add("with-tooltip"), 50);
        }

        redirect() {
            let viewModel = dotvvm.viewModels.root.viewModel;
            let language = viewModel.Language().Moniker();
            let lessonMoniker = viewModel.LessonMoniker();
            let nextStep = viewModel.Step().NextStep();
            window.location.pathname = `/${language}/${lessonMoniker}/${nextStep}`;
        }
    }
}