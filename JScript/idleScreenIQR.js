
var idleTimer;
var idleTime = 60 * 10 * 1000;
//var idleTime = 60 * 1000;
var countdownTimer;
var countdownTime = 60 * 1000; // 10 seconds in milliseconds

function resetIdleTimer() {
    clearTimeout(idleTimer);
    idleTimer = setTimeout(showIdleAlert, idleTime);
}

function showIdleAlert() {
    let timeLeft = countdownTime;

    Swal.fire({
        title: 'Session Expiration Warning',
        html: 'Your online session will expire in <span id="countdown"></span>.<br>Click "Continue" to keep working, or click "Close" to end your session now.',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Continue',
        cancelButtonText: 'Close',
        timer: countdownTime,
        timerProgressBar: true,
        onBeforeOpen: () => {
            // Update the countdown timer every second
            countdownTimer = setInterval(() => {
                document.getElementById('countdown').textContent = formatTimeLeft(timeLeft);
                timeLeft -= 1000;
            }, 1000);
        },
        onClose: () => {
            // Clear the countdown timer when the dialog is closed
            clearInterval(countdownTimer);
            // Reset the idle timer when the dialog is closed
            resetIdleTimer();
        }
    }).then((result) => {
        // User clicked "Continue" or closed the alert
        if (result.isConfirmed) {
            resetIdleTimer();
        } else {
            // User clicked "Close" or the dialog auto-closed due to the timer
            redirection();

        }
    });
}

// Function to handle redirection and session expiration warning
function redirection() {
    // Get the value of the query string parameter "PART"
    const urlParams = new URLSearchParams(window.location.search);
    let partParam = urlParams.get('PART');

    // If the PART parameter is not present in the query string, fallback to the session value
    if (!partParam) {
        partParam = sessionExpiration;
    }

    // Define the base URL
    let baseUrl = 'ProductRegistration.aspx';

    // Check if the PART parameter is not null or empty
    if (partParam) {
        // Construct the URL with the PART parameter
        baseUrl += `?PART=${partParam}`;
    }

    // Redirect to the constructed URL
    window.location.href = baseUrl;
}


function formatTimeLeft(timeLeft) {
    const seconds = Math.floor(timeLeft / 1000);
    return `${seconds} sec`;
}

document.addEventListener('mousemove', resetIdleTimer);
document.addEventListener('keypress', resetIdleTimer);
document.addEventListener('scroll', resetIdleTimer); 

// Initial setup
resetIdleTimer();