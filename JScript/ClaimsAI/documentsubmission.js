﻿$(document).ready(function () {

    var myModal = $('#myModal');
    const previewContainer = document.getElementById("previewContainer");
    const filePreview = document.getElementById("filePreview");
    const pdfPreview = document.getElementById("pdfPreview");
    let selectedFile = null; // Store the selected file for later preview

    const collapses = document.querySelectorAll("[app-collapse-header-btn]");

    const handleExpandToggle = (event) => {
        const btnElement = event.currentTarget;
        const controlsId = btnElement.getAttribute('aria-controls');
        const contentEl = document.getElementById(controlsId);

        // Toggle the clicked accordion
        const isOpen = btnElement.getAttribute('aria-expanded') === 'true';
        btnElement.setAttribute('aria-expanded', `${!isOpen}`);

        if (isOpen) {
            contentEl.classList.remove('app-collapse-expanded');
        } else {
            contentEl.classList.add('app-collapse-expanded');
        }
    };

    // Attach event listeners to each accordion header button
    collapses.forEach(collapse => collapse.addEventListener('click', handleExpandToggle));


    // Trigger file upload when btn_upload is clicked
    $(document).on('click', '[id^="btn_upload_"]', function (e) {
        e.preventDefault();
        var docId = this.id.split('_')[2];
        $('#file_upload_' + docId).click();
    });

    // Handle file upload input change
    $(document).on('change', '[id^="file_upload_"]', function (event) {
        var id = this.id.split('_')[2]; // Extract the id from the input element
        var fileName = $(this).val().split('\\').pop(); // Get the file name
        var fileInput = $(this)[0];
        var file = fileInput.files[0];

        var maxSize = 10 * 1024 * 1024; // 10MB file size limit
        var allowedTypes = ['image/jpeg', 'image/png', 'image/gif', 'application/pdf']; // Allowed file types

        if (file) {
            // Check if the file type is allowed
            if (!allowedTypes.includes(file.type)) {
                Swal.fire({
                    title: 'Invalid file type',
                    text: 'Please select a JPG, PNG, GIF, or PDF.',
                    icon: 'error',
                    confirmButtonText: 'OK'
                });
                fileInput.value = ''; // Clear file input
                $('#file_name_' + id).text(''); // Clear displayed file name
                previewContainer.style.display = 'none'; // Hide preview container
                selectedFile = null; // Clear selected file
                return;
            }

            // Check if the file size exceeds the maximum limit
            if (file.size > maxSize) {
                Swal.fire({
                    title: 'File size exceeds 10MB',
                    text: 'Please select a smaller file.',
                    icon: 'error',
                    confirmButtonText: 'OK'
                });
                fileInput.value = ''; // Clear file input
                $('#file_name_' + id).text(''); // Clear displayed file name
                previewContainer.style.display = 'none'; // Hide preview container
                selectedFile = null; // Clear selected file
                return;
            }

            // If everything is okay, store the selected file for later preview
            $('#file_name_' + id).text(fileName);
            $('#validation_message_' + id).text('');
            $('#btn_show_' + id).attr({ disabled: false });
            $('#btn_download_' + id).attr({ disabled: true });
            $("#eye_icon_" + id).css("fill", "#00263E");
            $("#dl_" + id).css({
                "fill": "gray",
                "opacity": "0.5"
            });
            $("#path1_dl_" + id).css({
                "fill": "gray",
                "opacity": "0.5"
            });
            $("#path2_dl_" + id).css({
                "fill": "gray",
                "opacity": "0.5"
            });
            $("#path1_" + id).css("fill", "#00263E");
            $("#path2_" + id).css("fill", "#00263E");
            selectedFile = file; // Store the file for the modal preview

            // Optional: Show a success message
            Swal.fire({
                title: 'File selected',
                text: 'You have selected: ' + fileName,
                icon: 'success',
                confirmButtonText: 'OK'
            });
        } else {
            previewContainer.style.display = "none";
            selectedFile = null; // Clear selected file

            $('#file_name_' + id).text('');
            $('#btn_show_' + id).attr({ disabled: true });
            $("#meye_icon_" + id).css({
                "fill": "gray",
                "opacity": "0.5"
            });
            $("#path1_" + id).css({
                "fill": "gray",
                "opacity": "0.5"
            });
            $("#path2_" + id).css({
                "fill": "gray",
                "opacity": "0.5"
            });
        }
    });

    $(document).on('click', '#btn_Submit', function (e) {
        e.preventDefault();

        //var inputedCaptcha = $('#infoForm_captchaText').val();

        //if (!inputedCaptcha) {
        //    Swal.fire({
        //        title: 'Required',
        //        text: 'Captcha is required.',
        //        icon: 'warning',
        //        confirmButtonText: 'OK'
        //    });
        //    return;
        //}
        
        $(this).html('<span class="loading-spinner"></span> Submitting...').css({
            "display": "inline-flex;",
            "align-items": "center;",
            "justify-content": "center;",
            "position": "relative;"
        }).attr({ disabled: true });

        if (!validateRequiredDocuments()) {
            Swal.fire({
                title: 'Required',
                text: 'Please upload the required documents.',
                icon: 'warning',
                confirmButtonText: 'OK'
            });
            $(this).html('Submit').attr({ disabled: false });
            return;
        }

        $('#param_for_saving').val('submit');
        $('#enrollmentForm').submit();
    })

    function validateRequiredDocuments() {
        let toValidate = [];
        let message = "";

        if ($("[id^='doc_']").length <= 0) toValidate.push("Please add the required documents.");

        $("[id^='document_type_']").each(function (i, e) {
            var docId = this.id.split('_')[2];
            var fileInput = $('#file_upload_' + docId);

            if (this.value == 'Primary') {
                if (fileInput.val() == '' && $('#file_name_' + docId).text() == '') {
                    toValidate.push('This document is required.');
                    $('#validation_message_' + docId).text('This document is required.');
                }
            }
        })

        for (var i = 0; i < toValidate.length; i++) {
            if (i == 0) {
                message += toValidate[i];
            } else {
                message += ", " + toValidate[i];
            }
        }
        if (toValidate.length == 0) {
            return true;
        } else {
            return false;
        }
    }

    $(document).on('change', '#infoForm_dataPrivacy1Checkbox', function () {
        if ($(this).prop('checked'))
            $('#infoForm_captchaText').attr({ disabled: false })
        else
            $('#infoForm_captchaText').val('').attr({ disabled: true })
    })

    $(document).on('input', '#infoForm_captchaText', function () {
        var captchaInput = this.value;

        $.ajax({
            type: "POST",
            url: "DocumentSubmission.aspx/ValidateCaptcha",
            data: JSON.stringify({ captcha: captchaInput }),
            contentType: "application/json; charset=utf-8",
            dataType: 'JSON',
            headers: {
                "__RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val() // Include anti-forgery token
            },
            success: function (response) {
                // Handle success
            },
            error: function (error) {
                // Handle error
            }
        });

    })

    // Function to show the spinner overlay
    function showSpinner() {
        $(".overlay-page").show();
    }

    // Function to hide the spinner overlay
    function hideSpinner() {
        $(".overlay-page").hide();
    }

    // Show spinner on partial postback start and hide on postback end
    Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(function () {
        showSpinner();
    });

    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
        hideSpinner();
    });

});








