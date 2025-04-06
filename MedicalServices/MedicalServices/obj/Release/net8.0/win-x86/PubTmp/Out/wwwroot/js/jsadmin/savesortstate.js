let select = document.querySelector("select");
let links = document.querySelectorAll(".pag-item a");
let linksFromMenu = document.querySelectorAll(".hidden-menu li a");
let form = document.querySelector("form");
setTimeout(() => {
    let confirmDelete = document.querySelector(".swal2-confirm.swal2-styled.swal2-default-outline");
    
}, 500); 

select.addEventListener("change", function (e) {
    let selectedOption = select.options[select.selectedIndex].value;
    localStorage.setItem('CurrentOption', selectedOption);
});

function setSelectedOptionFromLocalStorage() {
    let storedOption = localStorage.getItem('CurrentOption');
    if (storedOption) {
        for (let i = 0; i < select.options.length; i++) {
            if (select.options[i].value === storedOption) {
                select.selectedIndex = i;
                break;
            }
        }
    }
}
linksFromMenu.forEach((link) => {
    link.addEventListener("click", function () {
        select.selectedIndex = 0;
        localStorage.removeItem('CurrentOption'); 
    });
});

if (form) {
    form.addEventListener("submit", function () {
        select.selectedIndex = 0;
        localStorage.removeItem('CurrentOption'); 
    })
}

window.onload = setSelectedOptionFromLocalStorage;