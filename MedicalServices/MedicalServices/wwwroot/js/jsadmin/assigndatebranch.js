let checkBoxesForDate = document.querySelectorAll(".board .date-check-box");
let checkBoxesForBranches = document.querySelectorAll(".board .branch-check-box");
let activeDates = [];
let activeBranches = [];
let contfirmBtn = document.querySelector(".new .add");
let saveBtn = document.querySelector(".new .save");
const tableBody = document.getElementById("tableBody");
let profileIcon = document.querySelector(".profile i");


checkBoxesForDate.forEach(checkbox => {
    checkbox.addEventListener("change", function (event) {
        if (event.target.checked) {
            activeDates.push(checkbox.value);
        } else {
            activeDates = activeDates.filter(date => date !== checkbox.value);
        }
    });
});

checkBoxesForBranches.forEach(checkbox => {
    checkbox.addEventListener("change", function (event) {
        if (event.target.checked) {
            activeBranches.push(checkbox.value);
        } else {
            activeBranches = activeBranches.filter(branch => branch !== checkbox.value);
        }
    });
});

contfirmBtn.addEventListener("click", function () {
    let datesForEachBranchLocal = [];
    activeBranches.forEach((branch) => {
        activeDates.forEach((date) => {
            datesForEachBranchLocal.push(date);
            localStorage.setItem(`${branch}`, JSON.stringify(datesForEachBranchLocal));
            ShowMsg();
            profileIcon.style.color = "red";
            window.scrollTo(0, 0);
        });
        datesForEachBranchLocal = [];
    });
    activeDates = [];
    activeBranches = [];
    checkBoxesForDate.forEach((cb) => {
        cb.checked = false;
    });
    checkBoxesForBranches.forEach((cb) => {
        cb.checked = false;
    });
});

function ShowMsg() {

        Swal.fire({
            title: "",
            icon: "success",
            text: 'the dates add to the local'
        }
        );
}











