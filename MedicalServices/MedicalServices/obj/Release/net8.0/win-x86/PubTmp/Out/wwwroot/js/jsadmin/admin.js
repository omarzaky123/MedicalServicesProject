let page = location.pathname;
responsiveAdmin();
dropDownAdmin();
if (!(page.includes("/Date"))) {
    trydropdown(".new .input-new", ".new-catigory-form");
}
let ele = document.querySelectorAll("table .control .delete");
deleteCheckFirst(ele);

if (page.includes("/Catigory/Insert")) {
    flyForm("Catigory");
}
if (page.includes("/MedicalService/Insert")) {
    flyForm("MedicalService");
}
if (page.includes("/Branch/Insert")) {
    flyForm("Branch");

}
if (!(page.includes("/Role/New")
    || page.includes("/Role/Index")
    || page.includes("/Branch/GetByIdAdmin/")
    || page.includes("/Workers")
    || page.includes("/Date")
    || page.includes("/AdminWelcome")
    || page.includes("/BranchGusetService/Index"))) {
    alertFormAdmin();
}
if (page.includes("/Branch/GetByIdAdmin/")) {
    calculateTotal();
}




