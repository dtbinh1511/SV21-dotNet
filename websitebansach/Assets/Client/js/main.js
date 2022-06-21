const $ = document.querySelector.bind(document);
const $$ = document.querySelectorAll.bind(document);
//slider start
const pictures = $$(".slider-img");
const silderControls = $$(".slider-control-item");
var imgSliderWidth = pictures[0].clientWidth;
var countSilder = 0;

// slider auto
var autoSliderId = setInterval(function () {
    if (countSilder == pictures.length - 1) {
        picture.style.transform = `translateX(0)`;
        console.log(countSilder);
        countSilder = 0;
        silderControls[countSilder].classList.add("active");
        silderControls[pictures.length - 1].classList.remove("active");
    } else {
        countSilder++;
        picture.style.transform = `translateX(calc(${countSilder}* -${imgSliderWidth}px))`;
        silderControls[countSilder - 1].classList.remove("active");
        silderControls[countSilder].classList.add("active");
    }
}, 5000);

// slider right
const picture = $(".slider-list-picture");
$(".slider-arrow-right").onclick = function (e) {
    if (countSilder == pictures.length - 1) {
        countSilder = 0;
        picture.style.transform = `translateX(0)`;
        silderControls[countSilder].classList.add("active");
        silderControls[pictures.length - 1].classList.remove("active");
    } else {
        countSilder++;
        picture.style.transform = `translateX(calc(${countSilder}* -${imgSliderWidth}px))`;
        silderControls[countSilder - 1].classList.remove("active");
        silderControls[countSilder].classList.add("active");
    }
};

// slider left
$(".slider-arrow-left").onclick = function (e) {
    if (countSilder == 0) {
        countSilder = pictures.length - 1;
        silderControls[0].classList.remove("active");
        silderControls[countSilder].classList.add("active");
        picture.style.transform = `translateX(calc(${countSilder}* -${imgSliderWidth}px))`;
    } else {
        countSilder--;
        picture.style.transform = `translateX(calc(${countSilder}* -${imgSliderWidth}px))`;
        silderControls[countSilder + 1].classList.remove("active");
        silderControls[countSilder].classList.add("active");
    }
};
//slider end

// khai báo

const widthItemProduct = $(".product-item").clientWidth;
var numberBook = Math.round($(".product-list").clientWidth / widthItemProduct);

// sách mới start
var newBookCount = 0;

// right
$("#new-book .product-arrow-right").onclick = function () {
    if (newBookCount == $$("#new-book .product-item").length - numberBook) {
        $("#new-book .product-list").style.transform = `translateX(0)`;
        newBookCount = 0;
    } else {
        newBookCount++;
        $(
            "#new-book .product-list"
        ).style.transform = `translateX(calc(${newBookCount}*-${widthItemProduct}px))`;
    }
};

//left
$("#new-book .product-arrow-left").onclick = function () {
    if (newBookCount == 0) {
        newBookCount = $$("#new-book .product-item").length - numberBook;
        $(
            "#new-book .product-list"
        ).style.transform = `translateX(calc(${newBookCount}*-${widthItemProduct}px))`;
    } else {
        newBookCount--;
        $(
            "#new-book .product-list"
        ).style.transform = `translateX(calc(${newBookCount}*-${widthItemProduct}px))`;
    }
};
// sách mới end

// sắp phát hành start

var newPublished = 0;
// right
$("#published .product-arrow-right").onclick = function () {
    if (newPublished == $$("#published .product-item").length - numberBook) {
        $("#published .product-list").style.transform = `translateX(0)`;
        newPublished = 0;
    } else {
        newPublished++;
        $(
            "#published .product-list"
        ).style.transform = `translateX(calc(${newPublished}*-${widthItemProduct}px))`;
    }
};

//left
$("#published .product-arrow-left").onclick = function () {
    if (newPublished == 0) {
        newPublished = $$("#published .product-item").length - numberBook;
        $(
            "#published .product-list"
        ).style.transform = `translateX(calc(${newPublished}*-${widthItemProduct}px))`;
    } else {
        newPublished--;
        $(
            "#published .product-list"
        ).style.transform = `translateX(calc(${newPublished}*-${widthItemProduct}px))`;
    }
};
// sắp phát hành end

// sắp phát hành start

var newSeller = 0;
// right
$("#seller .product-arrow-right").onclick = function () {
    if (newSeller == $$("#seller .product-item").length - numberBook) {
        $("#seller .product-list").style.transform = `translateX(0)`;
        newSeller = 0;
    } else {
        newSeller++;
        $(
            "#seller .product-list"
        ).style.transform = `translateX(calc(${newSeller}*-${widthItemProduct}px))`;
    }
};

//left
$("#seller .product-arrow-left").onclick = function () {
    if (newSeller == 0) {
        newSeller = $$("#seller .product-item").length - numberBook;
        $(
            "#seller .product-list"
        ).style.transform = `translateX(calc(${newSeller}*-${widthItemProduct}px))`;
    } else {
        newSeller--;
        $(
            "#seller .product-list"
        ).style.transform = `translateX(calc(${newSeller}*-${widthItemProduct}px))`;
    }
};
// sắp phát hành end