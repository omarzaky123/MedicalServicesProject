//HeaderResponsive
let icons = document.querySelector(".icons");
let mobileHeader = document.querySelector(".header .mobilenav");
let header = document.querySelector(".header");
icons.addEventListener("click", function() {
    header.classList.toggle("open");
    mobileHeader.classList.toggle("open");

});
