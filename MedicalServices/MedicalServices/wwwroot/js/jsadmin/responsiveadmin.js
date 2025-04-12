function responsiveAdmin() {
    let menuBtn = document.querySelector(".interface .nav > i");
    let menu = document.querySelector(".menu");
    let interface = document.querySelector(".interface");
    menuBtn.addEventListener("click", function () {
        menu.classList.toggle("active");
        interface.classList.toggle("active");
    });
}