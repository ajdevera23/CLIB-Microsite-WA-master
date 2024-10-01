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

        var maxSize = 3 * 1024 * 1024; // 3MB file size limit
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
                    title: 'File size exceeds 3MB',
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
            $('#btn_show_' + id).attr({ disabled: false });
            $("#mata_" + id).css("fill", "#00263E");
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
        }
    });

    //$(document).on('click', function () {
    //    Swal.fire({
    //        title: 'You got it!',
    //        text: 'Eeyy',
    //        icon: 'info',
    //        showConfirmButton: false,
    //        allowOutsideClick: false,
    //        allowEscapeKey: false
    //    });
    //});

});

var btncheckeligibility = $('.validate-btn');

    btncheckeligibility.click(function () {
        Swal.fire({
            title: 'Please wait while we process your details...',
            showConfirmButton: false,
            allowOutsideClick: false,
            allowEscapeKey: false
        });

        // Set a timer for 2 seconds (2000 milliseconds)
        setTimeout(function () {
            // Code to execute after the delay
            // For example, you can make an AJAX call or any other function
            console.log('Processing completed.'); // Placeholder for your next action
            Swal.close(); // Close the Swal alert if needed
        }, 1000);
    });

});





