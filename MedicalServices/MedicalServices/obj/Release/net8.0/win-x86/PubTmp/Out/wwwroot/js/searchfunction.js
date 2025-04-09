//function search(...Parameters) {
//    let searchInput = document.querySelector(`${Parameters[0]}`);
//    let boxHeading = document.querySelectorAll(`${Parameters[1]}`);
//    let boxes = document.querySelectorAll(`${Parameters[2]}`);
//    searchInput.addEventListener("input", function () {
//        let found = false;
//        boxHeading.forEach((h, index) => {
//            found = h.innerHTML
//                .trim()
//                .toLowerCase()
//                .includes(`${searchInput.value.trim().toLowerCase()}`);
//            if (found) {
//                boxes[index].style.display = "block";
//                found = false;
//            } else if (searchInput.value == "") {
//                boxes.forEach((b) => (b.style.display = "block"));
//            } else {
//                boxes[index].style.display = "none";
//            }
//        });
//        fixBox();
//    }

//    );
//}

//function totalSearch() {
//    let searchContainer = document.querySelector(".searchitem")
//    let searchInput = document.querySelector(".searchitem input");
//    let containerBody = document.querySelector(".cards-container .container");
//    let branchIdElement = document.querySelector(".Branch-Id");
//    const firstcontentContainer = document.querySelector(".cards-container .container .boxes");
//    if (branchIdElement)
//        branchId = branchIdElement.value;
//    searchInput.addEventListener("input", function () {

//        if (searchInput.value === "") {
//            //alert("No Input");
//            BoxesClear();
//            //alert("No Input After Clear");
//            containerBody.appendChild(firstcontentContainer);
//            //alert("No Input After append firstcontentContainer");
//        }
//        else {
//            //alert("Input before ajaxCallSearch");
//            ajaxCallSearch();
//            //alert("Input after ajaxCallSearch");
//            //removeUncheck();
//        }
//    });

//    //function removeUncheck() {
//    //    let catigoryCheckBox = document.querySelector(".catigory input[type='checkbox']");
//    //    if (catigoryCheckBox) {
//    //        catigoryCheckBox.setAttribute("disabled", "disabled");
//    //    }
//    //}

//    function BoxesClear() {
//        let Boxes = document.querySelector(".cards-container .container .boxes");
//        if (Boxes) {
//            Boxes.remove();
//        }
//    }

//    function ajaxCallSearch() {
//        BoxesClear();
//        $.ajax({
//            url: `/MedicalService/SearchByNameGuset`,
//            data: { searchname: searchInput.value, branchid: branchId },
//            success: function (response) {


//                if (response) {
//                    const parser = new DOMParser();
//                    const doc = parser.parseFromString(response, 'text/html');
//                    const newBoxes = doc.querySelector('.boxes');
//                    if (newBoxes) {
//                        containerBody.appendChild(newBoxes);
//                    }
//                }
//            }
//        });
//    }
//}


//totalSearch();


function totalSearch() {
    let searchContainer = document.querySelector(".searchitem")
    let searchInput = document.querySelector(".searchitem input");
    let containerBody = document.querySelector(".cards-container .container");
    let branchIdElement = document.querySelector(".Branch-Id");
    const firstcontentContainer = document.querySelector(".cards-container .container .boxes");
    let branchId = branchIdElement ? branchIdElement.value : null;
    let currentRequest = null;

    searchInput.addEventListener("input", function () {
        if (currentRequest) {
            currentRequest.abort();
        }

        if (searchInput.value === "") {
            BoxesClear();
            returncheck();
            containerBody.appendChild(firstcontentContainer);
        } else {
            removeUncheck();
            ajaxCallSearch();
        }
    });

    function removeUncheck() {
        let catigoryCheckBoxes = document.querySelectorAll(".catigory input[type='checkbox']");
        catigoryCheckBoxes.forEach((cat) => {
            cat.setAttribute("disabled", "disabled");
        })
    }

    function returncheck() {
        let catigoryCheckBoxes = document.querySelectorAll(".catigory input[type='checkbox']");
        catigoryCheckBoxes.forEach((cat) => {
            cat.removeAttribute("disabled", "disabled");
        })
    }

    function BoxesClear() {
        let Boxes = document.querySelector(".cards-container .container .boxes");
        if (Boxes) {
            Boxes.remove();
        }
    }
    function ajaxCallSearch() {
        BoxesClear();
        currentRequest = $.ajax({
            url: `/MedicalService/SearchByNameGuset`,
            data: { searchname: searchInput.value, branchid: branchId },
            success: function (response) {
                if (response) {
                    const parser = new DOMParser();
                    const doc = parser.parseFromString(response, 'text/html');
                    const newBoxes = doc.querySelector('.boxes');
                    if (newBoxes) {
                        containerBody.appendChild(newBoxes);
                    }
                }
                currentRequest = null;
            },
        });
    }
}

totalSearch();