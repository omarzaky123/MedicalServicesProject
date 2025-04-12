function totalSearch(controller) {
    let branchId = 0;
    let searchContainer = document.querySelector(".searchbox")
    let searchInput = document.querySelector(".searchbox input");
    let tableBody = document.querySelector(".board");
    let branchIdElement = document.querySelector("#branchId");
    const firstcontentContainer = tableBody.innerHTML;
    searchContainer.style.visibility = "visible";
    
    if (branchIdElement)
        branchId = branchIdElement.getAttribute("branch-id");
    searchInput.addEventListener("input", function () {
        if (searchInput.value === "") {
            tableBody.innerHTML = firstcontentContainer;
        } else {
            ajaxCallSearch();
        }
    });

    function ajaxCallSearch() {

        tableBody.innerHTML = "";
        
        $.ajax({
            url: `/${controller}/SearchByName`,
            data: { searchname: searchInput.value, branchid: branchId },
            success: function (response) {
                tableBody.innerHTML = response;
                let ele = document.querySelectorAll("table .control .delete");
                deleteCheckFirst(ele);
                flyForm(controller);
            }
        });
    }
}

if (location.pathname.includes("/BranchGusetService/Index")) {

    totalSearch("BranchGusetService");
}

if (location.pathname.includes("/Catigory/Insert")) {
    totalSearch("Catigory");
}
else if (location.pathname.includes("/MedicalService/Insert")) {
    totalSearch("MedicalService");
}
else if (location.pathname.includes("/Branch/Insert")) {
    totalSearch("Branch");
}
else if (location.pathname.includes("/Workers/GetAllDoctors")) {
    totalSearch("Doctor");
}
else if (location.pathname.includes("/Workers/GetAllAccountants")) {
    totalSearch("Accountant");
}
else if (location.pathname.includes("/Workers/GetAllAssastants")) {
    totalSearch("Assastant");
}
else if (location.pathname.includes("/Workers/GetAllAdmins")) {
    totalSearch("Admin");
}

