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
        var docId = $(this).attr('data-value')
        //alert(docId)
        myModal.modal('show')
    });
})