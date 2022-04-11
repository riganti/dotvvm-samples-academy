"use strict";
const navBurger = document.getElementById('showNav');
const navMenu = document.getElementById('navMenu');
const body = document.querySelector('body');
navBurger.addEventListener('click', function () {
    navBurger.classList.toggle('open');
    navMenu.classList.toggle('open');
    body.classList.toggle('scroll-disabled');
});
