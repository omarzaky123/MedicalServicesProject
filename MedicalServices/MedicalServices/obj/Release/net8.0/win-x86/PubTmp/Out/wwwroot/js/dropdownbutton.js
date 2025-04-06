function dropDown(...parameter) {
    let dropDownButton = document.querySelector(`${parameter[0]}`);
    let arrow = document.querySelector(`${parameter[1]}`);
    let displayedItem = document.querySelector(`${parameter[2]}`);

    displayedItem.style.transform = "translateY(-10%)";
    displayedItem.style.zIndex = "-1";
    dropDownButton.addEventListener("click", function () {
        
        if (displayedItem.style.transform == "translateY(-10%)") {
            displayedItem.style.transform = "translateY(0%)";
            displayedItem.style.zIndex = "1";
            arrow.style.transform = "rotate(180deg)"; 
        }
        else {
            displayedItem.style.zIndex = "-1";
            displayedItem.style.transform = "translateY(-10%)";
            arrow.style.transform = "rotate(0deg)";
        }
    })




}