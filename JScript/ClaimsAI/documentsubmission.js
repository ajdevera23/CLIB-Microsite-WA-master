$(document).ready(function () {

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

        $(this).html('<span class="loading-spinner"></span> Submitting...').css({
            "display": "inline-flex;",
            "align-items": "center;",
            "justify-content": "center;",
            "position": "relative;"
        }).attr({ disabled: true });

        if ($('#infoForm_natureofclaimDropdownlist').val() == 'Select') {
            Swal.fire({
                text: 'Please select Nature of Claim',
                confirmButtonText: 'OK'
            });
            $('#btn_Submit').html('Submit').attr({ disabled: false });
            return;
        }

        if (!validateRequiredDocuments()) {
            Swal.fire({
                text: 'Please select atleast one benefit and upload all the required documents.',
                confirmButtonText: 'OK'
            });
            $('#btn_Submit').html('Submit').attr({ disabled: false });
            return;
        }

        if (!$('#infoForm_dataPrivacy1Checkbox').prop('checked')) {
            Swal.fire({
                text: 'Please read and check our Data Privacy policy before proceeding.',
                confirmButtonText: 'OK'
            });
            $('#btn_Submit').html('Submit').attr({ disabled: false });
            return;
        }

        if (!$('#infoForm_captchaText').val()) {
            Swal.fire({
                text: 'Please answer Captcha Text!',
                confirmButtonText: 'OK'
            });
            $('#btn_Submit').html('Submit').attr({ disabled: false });
            return;
        }

        $('#param_for_saving').val('submit');
        $('#enrollmentForm').submit();
    })

    function validateRequiredDocuments() {
        var totalBenefits = 0, totalBenefitsChecked = 0;
        let toValidate = [];
        let message = "";

        if ($("[id^='doc_']").length <= 0) toValidate.push("Please add the required documents.");

        $("[id^='infoForm_rptBenefits_chkBenefit_']").each(function () {
            if (!$(this).prop('checked'))
                totalBenefits++;
            else
                totalBenefitsChecked++;
        })

        if (totalBenefitsChecked == 0)
            toValidate.push("Please select atleast one benefit.");

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

    $(document).on('click', '[id^="btn_download_"]', function () {
        var docId = this.id.split('_')[2];
        var refNo = $('#infoForm_referenceNumber').val();

        $.ajax({
            type: 'POST',
            url: 'DocumentsSubmission.aspx/DownloadDocument',
            data: JSON.stringify({ documentId: docId, referenceNo: refNo }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            beforeSend: function () {
                showSpinner();
            },
            success: function (response) {
                var parsedResponse = JSON.parse(response.d);

                if (parsedResponse.success) {
                    var base64Data = parsedResponse.base64File;
                    var fileName = parsedResponse.fileName;
                    var contentType = parsedResponse.contentType;

                    // Convert base64 to Blob
                    var byteCharacters = atob(base64Data);
                    var byteNumbers = new Array(byteCharacters.length);
                    for (var i = 0; i < byteCharacters.length; i++) {
                        byteNumbers[i] = byteCharacters.charCodeAt(i);
                    }
                    var byteArray = new Uint8Array(byteNumbers);
                    var blob = new Blob([byteArray], { type: contentType });

                    // Create a link element for download
                    var link = document.createElement('a');
                    link.href = window.URL.createObjectURL(blob);
                    link.download = fileName;
                    link.click(); // Programmatically click the link to trigger the download
                    hideSpinner();
                } else {
                    hideSpinner();
                    Swal.fire({
                        title: 'Download Interupted',
                        text: 'Please try again.',
                        confirmButtonText: 'OK'
                    });
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                hideSpinner();
                console.error("AJAX error: ", textStatus, errorThrown);
            }
        });
    })
});








