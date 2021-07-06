const langSwitch = document.querySelector(".lang-switch");
const langSwitchButon = document.querySelector(".lang-switch__button") as HTMLButtonElement;
const langSwitchList = document.querySelector(".lang-switch__list") as HTMLDivElement;

function toggleLanguage() {
    langSwitch.classList.toggle("lang-switch--open");
}

document.addEventListener("click", function (e) {
    if (!(<HTMLElement>e.target).closest('.lang-switch')) {
        langSwitch.classList.remove("lang-switch--open");
    }
});