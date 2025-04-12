function alertForm() {
    let AppointmentDateID = document.getElementById("AppointmentDateID");
    let form = document.querySelector(".invoiceform");

    form.addEventListener("submit", function (e) {
        if (AppointmentDateID.value == '0' && AppointmentDateID.options.length == 1) {
            e.preventDefault();
            Swal.fire({
                title: "No Dates Available",
                text: "Sorry, no dates are available now. Please try later.",
                icon: "error"
            });
        }
        else if (AppointmentDateID.value == '0' && AppointmentDateID.options.length > 1) {
            e.preventDefault();
            Swal.fire({
                title: "Select a Date",
                text: "Please select your appointment date before submitting.",
                icon: "warning"
            });
        }
    });
}


