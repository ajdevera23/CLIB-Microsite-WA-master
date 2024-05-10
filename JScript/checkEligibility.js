var btncheckeligibility = $('.checkeligibility');

btncheckeligibility.click(function () {
    Swal.fire({
        title: 'Please wait while we process your details...',
        showConfirmButton: false,
        allowOutsideClick: false,
        allowEscapeKey: false
    });
})