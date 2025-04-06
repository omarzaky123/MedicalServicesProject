function calculateTotal() {
    let servicePrices = document.querySelectorAll("#service-price");
    let sum = 0;
    servicePrices.forEach((s) => {
        sum += parseInt(s.innerHTML);
    })
    let board = document.querySelector("#orders .role .text");
    board.innerHTML = sum;
}

