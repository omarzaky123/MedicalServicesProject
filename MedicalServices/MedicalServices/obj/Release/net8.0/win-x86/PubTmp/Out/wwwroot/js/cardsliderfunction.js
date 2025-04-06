function TotalCardslider() {

    let boxes = document.querySelectorAll(".doctor .container .boxes .box");
    let right = document.querySelector(".doctor .container .items .clicks .right");
    let left = document.querySelector(".doctor .container .items .clicks .left");
    let counter = 0;
    let gap = 0;
    var leftBox = 0;
    let divided = 0;

    function cardSliderFun() {
        boxes = document.querySelectorAll(".doctor .container .boxes .box");
        right = document.querySelector(".doctor .container .items .clicks .right");
        left = document.querySelector(".doctor .container .items .clicks .left");
        counter = 0;
        gap = 0;
        leftBox = 0;
        divided = 0;
        if (boxes.length % 2 == 0) {
            divided = 4;
            leftBox = boxes.length / 2 - 2;
        } else {
            divided = 5;
            leftBox = Math.floor(boxes.length / 2) - 2;
        }
        boxes.forEach((b) => {
            b.style.minWidth = `calc((100% - 160px) / ${divided})`;
        });
        //click
        right.addEventListener("click", moveToRight);
        left.addEventListener("click", moveToLeft);

        //function
        function moveToRight() {
            clearInterval(automaticCardSlider);
            counter += 100;
            gap = gap + 40;
            boxes.forEach((b) => {
                b.style.transform = `translate(calc(${counter}% + ${gap}px))`;
            });
            leftBox--;
            if (leftBox == 0) {
                right.classList.add("disable");
            }
            left.classList.remove("disable");
        }

        function moveToLeft() {
            counter -= 100;
            gap = gap - 40;
            boxes.forEach((b) => {
                b.style.transform = `translate(calc(${counter}% + ${gap}px))`;
            });
            leftBox++;
            if (leftBox == boxes.length - divided) {
                left.classList.add("disable");
                clearInterval(automaticCardSlider);
            }
            right.classList.remove("disable");
        }
    }

    if (window.matchMedia("(min-width: 768px)").matches && boxes.length>5) {

        cardSliderFun();
    }

    let automaticCardSlider = setInterval(() => left.click(), 1000);

}