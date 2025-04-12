function alertFormAdmin() {
    
    let selectBoxes = document.querySelectorAll("form .container .text select");
    let form = document.querySelector(".new-catigory-form");
    let Value = "";
    let Value02 = "";
    form.addEventListener("submit", function (e) {

        selectBoxes.forEach((selectBox) => {
            if (selectBox.value == '0' && selectBox.options.length > 1) {
                e.preventDefault();
                Value = selectBox.options[0].text.split("--")[1];
                Swal.fire({
                    title: `${Value}`,
                    text: "Please select From the List before submitting.",
                    icon: "warning"
                });
            }
            else if (selectBox.value == '0' && selectBox.options.length == 1) {
                e.preventDefault();
                Value02 = selectBox.options[0].text.split("--")[1];
                Swal.fire({
                    title: `No Available ${Value02.split(" ")[1]}`,
                    text: `Please Add First a ${Value02}`,
                    icon: "error"
                });
            }
        })
    });
}


