function ShowMsg(msg) {
    document.addEventListener('DOMContentLoaded', function () {
        Swal.fire({
            title: "",
            icon: "success",
            text: msg
        });
    });
}

if (location.pathname.includes("/File/Index")) {
    ShowMsg("The Email Is Sent Successfully")
}



