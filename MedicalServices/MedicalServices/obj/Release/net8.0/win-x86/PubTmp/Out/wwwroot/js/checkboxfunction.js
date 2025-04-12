function checkbox() {

    const firstBoxes = document.querySelector(".cards-container .container .boxes").cloneNode(true);
    let allCheckBoxes = document.querySelectorAll(".cards-container .container .searchitem .catigorys .catigory");
    let containerBox = document.querySelector(".cards-container .container .boxes");
    let BranchID = document.querySelector(".box .Branch-Id").value;
    let activeIds = [];
    AjaxCall();
    function AjaxCall() {

        allCheckBoxes.forEach((checkbox) => {
            checkbox.addEventListener("change", function (event) {
                if (event.target.checked) {
                    activeIds.push(event.target.id);
                    addToTheContainer();
                }
                else {
                    activeIds = activeIds.filter((ele, index) => ele != event.target.id)
                    addToTheContainer();
                }
            }
            );
        })
    }

    function addToTheContainer() {
        containerBox.innerHTML = "";
        activeIds.forEach((e) => {
            $.ajax({
                url: "/Catigory/RelatedServiceBranch",
                data: {
                    "BranchID": parseInt(BranchID),
                    "CatigoryId": parseInt(e)
                }
                , success: function (result) {
                    containerBox.innerHTML += result;
                    fixBox();
                }
            }
            );
        })



        if (activeIds.length == 0) {
            ////print all services in this branch
            //$.ajax({
            //    url: "/Branch/RelatedService",
            //    data: { "id": parseInt(BranchID) }
            //    , success: function (result) {
            //        containerBox.innerHTML += result;
            //        fixBox();
            //    }
            //}
            //);
            containerBox.appendChild(firstBoxes);
            fixBox();
        }

    }


}