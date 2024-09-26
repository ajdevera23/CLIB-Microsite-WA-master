$(function () {
    let myModal = $('#myModal')
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
    }

    // Attach event listeners to each accordion header button
    collapses.forEach(collapse => collapse.addEventListener('click', handleExpandToggle));

    $('[id^="btn_show_"]').on('click', function () {
        var docId = this.id.split('_')[2];
        //alert(docId)
        myModal.modal('show')
    });

    $('[id^="btn_upload_"]').click(function (e) {
        e.preventDefault(); // Prevent the default button action
        var docId = this.id.split('_')[2];
        $('#file_upload_' + docId).click(); // Simulate a click on the hidden file input
    });

    $('[id^="file_upload_"]').change(function () {
        var id = this.id.split('_')[2];
        var fileName = $(this).val().split('\\').pop(); // Get the file name
        $('#file_name_' + id).text(fileName);
    });

})