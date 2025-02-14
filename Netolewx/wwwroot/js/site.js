document.addEventListener('DOMContentLoaded', function () {
    // Mobile menu toggle
    const mobileMenuBtn = document.querySelector('.mobile-menu-btn');
    const navLinks = document.querySelector('.nav-link');

    if (mobileMenuBtn) {
        mobileMenuBtn.addEventListener('click', function () {
            navLinks.classList.toggle('active');
            // Change icon between bars and times
            const icon = this.querySelector('i');
            icon.classList.toggle('fa-bars');
            icon.classList.toggle('fa-times');
        });
    }

    // Close mobile menu when clicking outside
    document.addEventListener('click', function (event) {
        const isClickInsideNav = event.target.closest('.nav-bar');
        if (!isClickInsideNav && navLinks.classList.contains('active')) {
            navLinks.classList.remove('active');
            const icon = mobileMenuBtn.querySelector('i');
            icon.classList.add('fa-bars');
            icon.classList.remove('fa-times');
        }
    });

    // Active link highlighting
    const currentPath = window.location.pathname;
    const navLinkElements = document.querySelectorAll('.nav-link a');

    navLinkElements.forEach(link => {
        if (link.getAttribute('href') === currentPath) {
            link.classList.add('active');
        }
    });

    // Smooth scroll to top
    const scrollToTop = () => {
        window.scrollTo({
            top: 0,
            behavior: 'smooth'
        });
    };

    // Add scroll to top button if page is long
    window.addEventListener('scroll', function () {
        if (window.scrollY > 500) {
            if (!document.querySelector('.scroll-top')) {
                const scrollBtn = document.createElement('button');
                scrollBtn.className = 'scroll-top';
                scrollBtn.innerHTML = '<i class="fas fa-arrow-up"></i>';
                document.body.appendChild(scrollBtn);
                scrollBtn.addEventListener('click', scrollToTop);
            }
        } else {
            const scrollBtn = document.querySelector('.scroll-top');
            if (scrollBtn) {
                scrollBtn.remove();
            }
        }
    });

    // Dropdown menu enhancement for mobile
    const dropdowns = document.querySelectorAll('.dropdown');

    dropdowns.forEach(dropdown => {
        const dropdownToggle = dropdown.querySelector('.dropdown-toggle');

        dropdownToggle.addEventListener('click', function (e) {
            if (window.innerWidth <= 768) {
                e.preventDefault();
                const dropdownMenu = this.nextElementSibling;
                dropdownMenu.style.display =
                    dropdownMenu.style.display === 'none' ? 'block' : 'none';
            }
        });
    });

    // Add scroll animation to reveal elements
    const revealOnScroll = () => {
        const elements = document.querySelectorAll('.reveal');
        elements.forEach(element => {
            const elementTop = element.getBoundingClientRect().top;
            const elementVisible = 150;

            if (elementTop < window.innerHeight - elementVisible) {
                element.classList.add('active');
            }
        });
    };

    window.addEventListener('scroll', revealOnScroll);
});