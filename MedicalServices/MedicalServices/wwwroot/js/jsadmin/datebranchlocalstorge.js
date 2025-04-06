let saveBtn = document.querySelector(".new .save");
let tableBody = document.getElementById("tableBody");


// Load existing data from localStorage
async function loadData() {
    for (let i = 0; i < localStorage.length; i++) {
        let key = localStorage.key(i);
        let branchname = await AjaxCallBranchName(key); // Await the result of the async function
        let datesForKey = JSON.parse(localStorage.getItem(key));
        for (const date of datesForKey) {
            let dateName = await AjaxCallDateName(date); // Await the result of the async function
            CreateElement(branchname, dateName);
        }
    }
}

// Call the async function to load data
loadData();

function CreateElement(branch, date) {
    const row = document.createElement("tr");

    // Create and append the 'Name' cell
    const nameCell = document.createElement("td");
    nameCell.className = "title";
    const nameDiv = document.createElement("div");
    nameDiv.className = "text";
    const nameHeader = document.createElement("h5");
    nameHeader.textContent = branch; // Use the passed branch value
    nameDiv.appendChild(nameHeader);
    nameCell.appendChild(nameDiv);
    row.appendChild(nameCell);

    // Create and append the 'Location' cell
    const locationCell = document.createElement("td");
    locationCell.className = "role";
    const locationText = document.createElement("p");
    locationText.className = "text";
    locationText.textContent = "Some Location"; // Replace with actual location if available
    locationCell.appendChild(locationText);
    row.appendChild(locationCell);

    // Create and append the 'Date' cell
    const dateCell = document.createElement("td");
    dateCell.className = "role";
    const dateText = document.createElement("p");
    dateText.className = "text";
    dateText.textContent = date; // Use the passed date value
    dateCell.appendChild(dateText);
    row.appendChild(dateCell);

    // Append the row to the table body
    tableBody.appendChild(row);
}

saveBtn.addEventListener("click", async function (e) {
    for (let i = 0; i < localStorage.length; i++) {
        let key = localStorage.key(i);
        let datesForKey = JSON.parse(localStorage.getItem(key));

        // Delete old dates for the branch
        await AjaxCallDeleteForBranch(key);

        // Add new dates for the branch
        for (const date of datesForKey) {
            await AjaxCallDatebranch(key, date);
        }
    }

    localStorage.clear();
    window.location = "/Date/GetDates";
});

function AjaxCallDatebranch(branch, date) {
    return new Promise((resolve, reject) => {
        $.ajax({
            url: '/Date/SaveAssignDatesForBranch',
            data: { branchid: branch, dateid: date },
            success: function (response) {
                resolve(response);
            },
            error: function (error) {
                reject(error);
            }
        });
    });
}

function AjaxCallDeleteForBranch(branch) {
    return new Promise((resolve, reject) => {
        $.ajax({
            url: '/Date/DeleteDatesForBranch',
            data: { branchid: branch },
            success: function (response) {
              
                resolve(response);
            },
            error: function (error) {
                reject(error);
            }
        });
    });
}

function AjaxCallBranchName(branch) {
    return new Promise((resolve, reject) => {
        $.ajax({
            url: '/Branch/BranchName',
            data: { id: branch },
            success: function (response) {
               
                resolve(response); // Resolve with the branch name
            },
            error: function (error) {
                reject(error);
            }
        });
    });
}

function AjaxCallDateName(date) {
    return new Promise((resolve, reject) => {
        $.ajax({
            url: '/Date/DateName',
            data: { id: date },
            success: function (response) {
               
                resolve(response); // Resolve with the date name
            },
            error: function (error) {
                reject(error);
            }
        });
    });
}