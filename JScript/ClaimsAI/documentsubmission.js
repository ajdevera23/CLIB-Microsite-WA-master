﻿$(document).ready(function () {

    //window.onload = function () {
    //    $('#param_for_saving').val('');
    //}

    //$(`[name="PurchaseOrderDetails[${i}].VatInclusive"]`).prop("checked", item.VatInclusive).trigger("change");

    ////$('#infoForm_dataPrivacy1Checkbox').

    //$('#infoForm_dataPrivacy1Checkbox').on('change', function () {
    //    var checked = $(this).is(":checked");
    //    computeSubTotal();
    //})


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

    //// Show preview modal when btn_show is clicked
    //$('[id^="btn_show_"]').on('click', function () {
    //    var docId = this.id.split('_')[2];

    //    // Only show the modal if a file has been selected and validated
    //    if (selectedFile) {
    //        // Display the modal
    //        myModal.modal('show');
    //        previewContainer.style.display = "block";

    //        // Preview the previously selected file
    //        if (selectedFile.type.startsWith("image/")) {
    //            filePreview.style.display = "block";
    //            pdfPreview.style.display = "none";

    //            const reader = new FileReader();
    //            reader.onload = function (e) {
    //                filePreview.src = e.target.result;
    //            };
    //            reader.readAsDataURL(selectedFile);
    //        } else if (selectedFile.type === "application/pdf") {
    //            pdfPreview.style.display = "block";
    //            filePreview.style.display = "none";

    //            const fileURL = URL.createObjectURL(selectedFile);
    //            pdfPreview.src = fileURL;
    //        }

    //        // Set the docId in the hidden form input
    //        $('#docIdInput').val(docId);

    //        // Submit the form to trigger the server-side action
    //        //$('#enrollmentForm').submit();
    //    } else {
    //        Swal.fire({
    //            title: 'No file selected',
    //            text: 'Please select a file before previewing.',
    //            icon: 'warning',
    //            confirmButtonText: 'OK'
    //        });
    //    }
    //});


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

});

//var btncheckeligibility = $('.validate-btn');

//btncheckeligibility.click(function () {
//    Swal.fire({
//        title: 'Please wait while we process your details...',
//        showConfirmButton: false,
//        allowOutsideClick: false,
//        allowEscapeKey: false
//    });

//    // Set a timer for 2 seconds (2000 milliseconds)
//    setTimeout(function () {
//        // Code to execute after the delay
//        // For example, you can make an AJAX call or any other function
//        console.log('Processing completed.'); // Placeholder for your next action
//        Swal.close(); // Close the Swal alert if needed
//    }, 1000);
//});






