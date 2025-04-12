function fixBox() {
    let AvaliableAboxes = document.querySelectorAll(".cards-container .container .box");
    let visibleBoxes = Array.from(AvaliableAboxes).filter(div => getComputedStyle(div).display === 'block'); 

    if (visibleBoxes.length === 1 || AvaliableAboxes.length == 1) {
        visibleBoxes[0].style.flex = "1";
        AvaliableAboxes[0].style.flex = "1";

    }
    else if (visibleBoxes.length === 2 || AvaliableAboxes.length == 2) {
        visibleBoxes[0].style.flex = "1";
        visibleBoxes[1].style.flex = "1";
        
    }
    else {

        visibleBoxes.forEach((e) => {
            e.style.flex = "0 1 auto";
        })
    }
}
