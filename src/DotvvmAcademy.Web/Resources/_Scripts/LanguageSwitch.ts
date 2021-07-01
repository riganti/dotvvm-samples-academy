﻿let langSwitch = document.querySelector(".lang-switch");
let langSwitchButon = document.querySelector(".lang-switch__button") as HTMLButtonElement;
let langSwitchList = document.querySelector(".lang-switch__list") as HTMLDivElement;

langSwitchButon.addEventListener("click", function () {
    langSwitch.classList.toggle("lang-switch--open");
});

document.addEventListener("click", function (e) {
    if (!(<HTMLElement>e.target).closest('.lang-switch')) {
        langSwitch.classList.remove("lang-switch--open");
    }
});