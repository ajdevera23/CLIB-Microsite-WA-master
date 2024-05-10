function recaptcha_callback() {
    //document.getElementById('#submitBtn').disable = false;
    $('#infoForm_submitBtn').removeAttr('disabled');
    //alert("I went here")
    ////if (response !== '') {
    jQuery('#lblMessage').css('color', 'green').html('Success'); 
       
    //}  
}; 

const acc_btns = document.querySelectorAll(".accordion-header");
const acc_contents = document.querySelectorAll(".accordion-body");

acc_btns.forEach((btn) => {
    btn.addEventListener("click", (e) => {
        acc_contents.forEach((acc) => {
            if (
                e.target.nextElementSibling !== acc &&
                acc.classList.contains("active")
            ) {
               // acc.classList.remove("active");
               // acc_btns.forEach((btn) => btn.classList.remove("active"));
            }
        });

        const panel = btn.nextElementSibling;
        panel.classList.toggle("active");
        btn.classList.toggle("active");
       
    });
});

var buttonmicrosite = $('.button-microsite'),
    spinner = '<span class="spinner" disabled></span>';
buttonmicrosite.click(function () {
    if (!buttonmicrosite.hasClass('loading')) {
        buttonmicrosite.toggleClass('loading').html(spinner);
        document.getElementById("popup").classList.remove("popupactive");
        Swal.fire({
            title: 'Saving information, please wait...',
            showConfirmButton: false,
            allowOutsideClick: false,
            allowEscapeKey: false
        });
    }
    else {
        buttonmicrosite.toggleClass('loading').html("Load");
    }
})

var buttoniqreqr = $('.button-iqreqr'),
    spinner = '<span class="spinner" disabled></span>';
buttoniqreqr.click(function () {
    if (!buttoniqreqr.hasClass('loading')) {
        buttoniqreqr.toggleClass('loading').html(spinner);
        Swal.fire({
            title: 'Please wait. Your request is being processed',
            showConfirmButton: false,
            allowOutsideClick: false,
            allowEscapeKey: false
        });
    }
    else {
        buttoniqreqr.toggleClass('loading').html("Load");
    }
})


var buttonfinalstep = $('.btn-finalstep');
buttonfinalstep.click(function () {
    Swal.fire({
        title: 'Please wait. Your request is being processed',
        showConfirmButton: false,
        allowOutsideClick: false,
        allowEscapeKey: false
    });
})

var btnpaymentmethod = $('.btn-payment-method');
btnpaymentmethod.click(function () {
    Swal.fire({
        title: 'Please wait. Your request is being processed',
        showConfirmButton: false,
        allowOutsideClick: false,
        allowEscapeKey: false
    });
})

var goBackLoad = $('.goBackLoad');
goBackLoad.click(function () {
    Swal.fire({
        title: 'Loading... Please wait',
        showConfirmButton: false,
        allowOutsideClick: false,
        allowEscapeKey: false
    });
})


var goBackLoad = $('.btn-quantity');
goBackLoad.click(function () {
    Swal.fire({
        title: 'Calculation in progress. Please wait....',
        showConfirmButton: false,
        allowOutsideClick: false,
        allowEscapeKey: false
    });
})




// ===== Open Op Up ==== 
$(".btnSubmitWithPopUp").click(function () {
    document.getElementById("popup").classList.add("popupactive");
});

$(".goBack").click(function () {
    document.getElementById("overlay").classList.remove("overlayActive");
    document.getElementById("popup").classList.remove("popupactive");
    document.getElementById("overlay").classList.remove("overlay");
});


$(".goBackMicrosite").click(function () {
    document.getElementById("popup").classList.remove("popupactive");
    document.getElementById("overlay").classList.remove("overlay");
});




// // ===== Open Pop Up Display Summary ====
//$(".popup-step-two").click(function () {
//    document.getElementById("popup-display-summary").classList.add("popupactive");
//    document.getElementById("overlay-display-preview").classList.add("overlayActive");
//    document.getElementById("overlay-display-preview").style.display = "block";

//});
// // ===== Go Back Display Summary ====
//$(".goBack-DisplayPreview").click(function () {
//    document.getElementById("overlay").classList.add("overlayActive");
//    document.getElementById("popup").classList.add("popupactive");
//});

// ===== Scroll to Top ==== 
$(window).scroll(function () {
    if ($(this).scrollTop() >= 50) {        // If page is scrolled more than 50px
        $('#return-to-top').fadeIn(200);    // Fade in the arrow
    } else {
        $('#return-to-top').fadeOut(200);   // Else fade out the arrow
    }
});
$('#return-to-top').click(function () {      // When arrow is clicked
    $('body,html').animate({
        scrollTop: 0                       // Scroll to top of body
    }, 500);
});


