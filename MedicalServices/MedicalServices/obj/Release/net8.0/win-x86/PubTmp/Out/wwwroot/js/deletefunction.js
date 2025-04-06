function deleteCheck(deleteElements) {
    deleteElements.forEach((e) => {
        e.addEventListener("click", function (event) {
            event.preventDefault(); // Prevent the default link behavior
            const href = this.href; // Store href in a variable to use in the callback

            Swal.fire({
                title: "Are you sure?",
                text: "You won't be able to revert this!",
                icon: "warning",
                showCancelButton: true,
                confirmButtonColor: "#3085d6",
                cancelButtonColor: "#d33",
                confirmButtonText: "Yes, delete it!"
            }).then((result) => {
                if (result.isConfirmed) {
                    window.location.href = href; // Use the stored href
                }
            });
        });
    });
}

