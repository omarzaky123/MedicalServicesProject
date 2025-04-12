const { post } = require("jquery");

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

    //if click on the page it close the form
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
                appereForm.addEventListener("submit", function (e) {
                    e.preventDefault();
                    
                    confirmEdit(id, CurrentPage, CurrentSort);
                })
            }
        });
    }
    function confirmEdit(id, CurrentPage, CurrentSort) {
        // Get the form element
        var form = document.getElementById('formEdit');

        // Create FormData from the form (this will include the image)
        var formData = new FormData(form);

        // Add the additional parameters
        formData.append('id', id);
        formData.append('CurrentPage', CurrentPage);
        formData.append('Sort', CurrentSort);
        $.ajax({
            url: `/${action}/Update`,
            data: formData,
            type: 'POST',
            contentType: false, // Required for FormData
            processData: false, // Required for FormData
            success: function (result) {
                if (result.success) {
                    closeAfterFinsh();
                } else {
                    let form = document.querySelector(".disapper-form");
                    form.innerHTML = result;
                }
            },
            error: function (xhr, status) {
                alert("Error form here" + xhr + status);
            }
        });
    }

}

function closeAfterFinsh() {

    let flyForm = document.querySelector(".fly-form");
    let appereForm = document.querySelector(".disapper-form");

    if (appereForm && appereForm.classList.contains("grid")) {
        appereForm.classList.remove("grid");
        destroyTheCss();
        flyForm.innerHTML = '';
    }
    location.reload();

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