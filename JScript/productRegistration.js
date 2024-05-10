function submitFunction(e) {
    var voucherCode = $("#voucherCode").val();
    if (voucherCode.length == 0) {
        e.preventDefault;
        alert("Please input voucher.");
    }
}


var btnSubmit = $('.btnSubmit');

btnSubmit.click(function () {
    Swal.fire({
        title: 'Please wait while we are processing your request...',
        showConfirmButton: false,
        allowOutsideClick: false,
        allowEscapeKey: false
    });
})