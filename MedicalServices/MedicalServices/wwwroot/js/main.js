if (window.location.pathname === "/") {
    TotalCardslider();
    if (window.matchMedia("(min-width: 992px)").matches) {
        landingSlider();
    }  
}
////calling delete function
if (window.location.pathname.includes("/Guset/RelatedBGS/")) {
    let deleteElementsoutSide = document.querySelectorAll(".basket .container .delete a");
    deleteCheck(deleteElementsoutSide);
}
////calling alert form
else if (window.location.pathname.includes("/Welcome/Invoice")) {
    alertForm();
}
else if (window.location.pathname.includes("/Welcome/SeeMedicalServcie/")) {
    fixBox();
    let searchInput = document.querySelector(`.cards-container .container .searchitem .search`);
    searchInput.addEventListener("focus", function () {
        search(".cards-container .container .searchitem .search",
            ".cards-container .container .box .text h3",
            ".cards-container .container .box");
    })
        dropDown(".cards-container .container .searchitem .drop-button",
            ".cards-container .container .searchitem .drop-button .arrow",
            ".cards-container .container .searchitem .catigorys");
    checkbox();    
    
    }
















