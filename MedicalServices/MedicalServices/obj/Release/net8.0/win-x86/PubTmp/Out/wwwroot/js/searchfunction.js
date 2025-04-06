function search(...Parameters) {
    let searchInput = document.querySelector(`${Parameters[0]}`);
    let boxHeading = document.querySelectorAll(`${Parameters[1]}`);
    let boxes = document.querySelectorAll(`${Parameters[2]}`);
    searchInput.addEventListener("input", function () {
        let found = false;
        boxHeading.forEach((h, index) => {
            found = h.innerHTML
                .trim()
                .toLowerCase()
                .includes(`${searchInput.value.trim().toLowerCase()}`);
            if (found) {
                boxes[index].style.display = "block";
                found = false;
            } else if (searchInput.value == "") {
                boxes.forEach((b) => (b.style.display = "block"));
            } else {
                boxes[index].style.display = "none";
            }
        });
        fixBox();
    }
    
    );
}