let langSwitcher = document.querySelector(".lang-switcher");
let langSwitcherList = document.querySelector(".lang-switcher_list");

langSwitcher.addEventListener("click", function () {
    langSwitcherList.classList.toggle("open");
});

document.addEventListener("click", function (e) {
    if (!(<HTMLElement>e.target).closest(".lang-switcher")) {
        langSwitcherList.classList.remove("open");
    }
});