// Toggle password visibility
const toggleBtns = document.querySelectorAll('.password-toggle');

toggleBtns.forEach(btn => {
    btn.addEventListener('click', function () {
        const input = this.previousElementSibling;
        if (input.type === 'password') {
            input.type = 'text';
            this.querySelector('span').textContent = '👁️‍🗨️';
        } else {
            input.type = 'password';
            this.querySelector('span').textContent = '👁️';
        }
    });
});

// Form validation
const form = document.querySelector('form');
form.addEventListener('submit', function (e) {
    e.preventDefault();

    // Get form values
    const fullname = document.getElementById('fullname').value;
    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;
    const confirmPassword = document.getElementById('confirm-password').value;

    // Simple validation
    if (password !== confirmPassword) {
        alert('Passwords do not match');
        return;
    }

    // Here you would typically send the form data to your server
    console.log('Form submitted', { fullname, email });

    // Simulate successful submission
    alert('Account created successfully! Redirecting to login...');
});