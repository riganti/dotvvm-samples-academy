const navBurger = document.getElementById('showNav') as HTMLButtonElement;
const navMenu = document.getElementById('navMenu') as HTMLDivElement;
const body = document.querySelector('body') as HTMLBodyElement;

navBurger.addEventListener('click', function () {
    navBurger.classList.toggle('open');
    navMenu.classList.toggle('open');
    body.classList.toggle('scroll-disabled');
});