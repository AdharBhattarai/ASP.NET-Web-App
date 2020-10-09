// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

//Reference Taken from Professor Holmes from the appointment of cart system
$(".addToCart").click(function () {
    var id = $(this).data("id");
    $.ajax({
        url: "Menu/AddToCart"
        , data: { catId: id }
        , success: function (data) {
            if (data.success) {
                updateCart();
                //GetCart()
                //each loop ul ma li append
                location.reload();
            } else {
                alert("Please Add the quantity from box in the cart!");
            }
        }
        , error: function () {
            alert("ooops");
        }
    });
});


$(".proceed_to_checkout").click(function () {
    var id = $(this).data("id");
    console.log($(this).data)
    $.ajax({
        url: "Menu/ProceedToCheckout"
        , success: function (data) {
            console.log(data);
            if (data.success) {
                $("#cart").empty();
                location.reload();
            } else {
                alert("something went wrong in proceed to checkout.");
            }
        }
        , error: function () {
            alert("ooops");
        }
    });
});

$(".quantity_product").click(function () {
    var quantity = $(this).val();
    var id = $(this).data("id");
    console.log(quantity);
    console.log(id);
    $.ajax({
        url: "Menu/RefreshCart"
        , data: {
            catId: id, quantity: quantity,
        }
        , success: function (data) {
            console.log(data);
            if (data.success) {
                //$("#cart").empty();
                location.reload();
            } else {
                alert("something went wrong in proceed to checkout.");
            }
        }
        , error: function () {
            alert("ooops");
        }
    });
})

//Reference Taken from Professor Holmes from the appointment of cart system

function updateCart() {
    $("#cart").empty();
    $.ajax({
        url: "Menu/GetCart"
        , success: function (data) {
            //$('#cart').html(data);
            $.each(data, function (ndx, menuo) {
                var li = $("<li>").text(menuo.text);
                //var li1 = $("<li>").text(menuo.price);

                $("#cart").append(li);
                //$("#cart").append(li1);

            });
        }
        , error: function () {
            alert("ooops");

        }
    });
}


$(".btnSaveDraft").click(function () {
    //console.log('Changed');
    var menuName = $(".menuName").val();
    var menuPrice = $(".menuPrice").val();
    var menuDescription = $(".menuDescription").val();

    localStorage.setItem("menuName", menuName);
    localStorage.setItem("menuPrice", menuPrice);
    localStorage.setItem("menuDescription", menuDescription);

});

$(document).ready(function () {
    var menuName = localStorage.getItem("menuName");
    var menuPrice = localStorage.getItem("menuPrice");
    var menuDescription = localStorage.getItem("menuDescription");
    var menuCreateURL = "https://localhost:44344/Menu/Create";
    console.log($(location).attr("href"))
    if ($(location).attr("href") == menuCreateURL) {
        $(".menuName").val(menuName);
        $(".menuPrice").val(menuPrice);
        $(".menuDescription").val(menuDescription);
    }

});




// Nav Bar for small screen
var mainNav = document.getElementById('js-menu');
var navBarToggle = document.getElementById('js-navbar-toggle');

navBarToggle.addEventListener('click', function () {
    mainNav.classList.toggle('active');
});

// Carousel
//Reference Taken from https://www.w3schools.com/howto/tryit.asp?filename=tryhow_js_slideshow
var slideIndex = 1;
showSlides(slideIndex);

function plusSlides(n) {
    showSlides(slideIndex += n);
}

function currentSlide(n) {
    showSlides(slideIndex = n);
}

function showSlides(n) {
    var i;
    var slides = document.getElementsByClassName("mySlides");
    var dots = document.getElementsByClassName("dot");
    if (n > slides.length) { slideIndex = 1 }
    if (n < 1) { slideIndex = slides.length }
    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }
    for (i = 0; i < dots.length; i++) {
        dots[i].className = dots[i].className.replace(" active", "");
    }
    slides[slideIndex - 1].style.display = "block";
    dots[slideIndex - 1].className += " active";
}

//add and subtract



