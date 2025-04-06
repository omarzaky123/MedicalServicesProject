function landingSlider() {
    let landing = document.querySelector(".landing");
    let left = document.querySelector(".landing .controls .left");
    let right = document.querySelector(".landing .controls .right");
    let imagesUrls = ["../Images/Landing.png", "../Images/information.png"];
    let paragraph = document.querySelector(
        ".landing .container .text p:nth-of-type(2)"
    );
    let Firstparagraph = document.querySelector(
        ".landing .container .text p:nth-of-type(1)"
    );
    let arrOfParagraphtText = [
        "Welcome to our Medical Services Management System – a smart and efficient platform designed to simplify healthcare operations. Easily browse services, book appointments, and manage transactions, all in a secure and user-friendly environmen",
        "Your health, our priority! Our Medical Services Management System connects you with top-quality healthcare services, allowing you to explore treatments, select branches, and manage bookings effortlessly. Experience seamless and efficient medical care today!",
    ];
    let arrOfFirstParagraphtText = [
        "The best health care in the world",
        "Specialist doctor for every field",
    ];
    let currentImage = 0;
    left.addEventListener("click", function () {
        toLeft();
        clearInterval(automaticCall);
    });
    right.addEventListener("click", function () {
        toRight();
        clearInterval(automaticCall);
    });

    function toRight() {
        if (currentImage == imagesUrls.length - 1) {
            currentImage = 0;
        } else {
            currentImage++;
        }

        action();
    }
    function toLeft() {
        if (currentImage == 0) {
            currentImage = imagesUrls.length - 1;
        } else {
            currentImage--;
        }
        action();
    }
    function action() {
        if (currentImage < imagesUrls.length && currentImage > -1) {
            landing.classList.add("not-active");
            setTimeout(() => {
                landing.classList.remove("not-active");
                landing.style.backgroundImage = `url(${imagesUrls[currentImage]})`;
                paragraph.innerHTML = `${arrOfParagraphtText[currentImage]}`;
                Firstparagraph.innerHTML = `${arrOfFirstParagraphtText[currentImage]}`;
            }, 1000);
        }
    }

    let automaticCall = setInterval(toRight, 5000);
}
