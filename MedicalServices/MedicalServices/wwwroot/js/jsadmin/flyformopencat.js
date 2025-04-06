function flyForm(action) {
    
    let btns = document.querySelectorAll("table .control .edit");
    
    btns.forEach((btn) => {
        btn.addEventListener("click", function () {
            let id = btn.getAttribute("id");
            let CurrentPage = btn.getAttribute("current-page");
            let CurrentSort = btn.getAttribute("current-sort")
            AjaxCallEdit(id, CurrentPage, CurrentSort);

        });
    })
    document.addEventListener('click', function (e) {
        let flyForm = document.querySelector(".fly-form");
        let appereForm = document.querySelector(".disapper-form");
        if (appereForm && appereForm.classList.contains("grid") &&
            !appereForm.contains(e.target) &&
            !Array.from(btns).some(btn => btn.contains(e.target))) {

            appereForm.classList.remove("grid");
            destroyTheCss();
            flyForm.innerHTML = ''; 
        }
    });

    function AjaxCallEdit(id,CurrentPage,CurrentSort) {
        let flyForm = document.querySelector(".fly-form");
  
        $.ajax({
            url: `/${action}/UpdateById`,
            data: { id: `${id}`, CurrentPage: `${CurrentPage}`, Sort: `${CurrentSort}`},
            success: function (response) {
                flyForm.innerHTML = response;
                let appereForm = document.querySelector(".disapper-form");
                appereForm.classList.toggle("grid");
                customTheCss();
                let inputs = document.querySelectorAll(".disapper-form input:not(submit)");
                let textAreas = document.querySelectorAll(".disapper-form textarea");
                let spanInput = document.querySelectorAll(".disapper-form .text span");
                let spanText = document.querySelectorAll(".disapper-form .text-area span");
                appereForm.addEventListener("submit", function (event) {
                    let isEmptyinput = false;
                    let isEmptyTextArea = false;
                    for (let i = 0; i < inputs.length - 2; i++) {
                        if (inputs[i].value == "") {
                            isEmptyinput = true;
                        }
                        else {
                            isEmptyinput = false;
                        }

                        if (isEmptyinput) {
                            spanInput[i].innerHTML = "This field is Requied";
                            event.preventDefault();
                        }
                    }

                    for (let i = 0; i < textAreas.length; i++) {
                        if (textAreas[i].value == "") {
                            isEmptyTextArea = true;
                        }
                        else {
                            isEmptyTextArea = false;
                        }

                        if (isEmptyTextArea) {
                            event.preventDefault();
                            spanText[i].innerHTML = "This field is Requied";
                        }
                    }

                })
            }
        });
    }
}
function customTheCss() {
    let interface = document.querySelector(".interface");
    let body = document.querySelector("body");
    interface.classList.add("set-height");
    body.classList.add("set-pos");
    let bodyBefore = document.createElement('div');
    bodyBefore.classList.add("body-overlay");
    bodyBefore.classList.add("active");
    body.appendChild(bodyBefore);
}
function destroyTheCss() {
    let interface = document.querySelector(".interface");
    let body = document.querySelector("body");
    interface.classList.remove("set-height");
    body.classList.remove("set-pos");
    let bodyBefore = document.querySelector('.body-overlay');
    if (bodyBefore) {
        bodyBefore.remove();
    }
}