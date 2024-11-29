// Dark mode toggle functionality
const darkModeToggle = document.getElementById('darkModeToggle');
const body = document.body;
const sidebar = document.querySelector('.sidebar');
const header = document.querySelector('header');
const sidebarHeader = document.querySelector('.sidebar-header'); // Get the sidebar header
const navLinks = document.querySelectorAll('.nav-link');
const profileMenu = document.querySelector('.dropdown-menu');
const profileMenuItems = document.querySelectorAll('.dropdown-item'); // Select all dropdown items
const profileButton = document.querySelector('.btn-profile');

// Toggle dark mode
darkModeToggle.addEventListener('click', function () {
    // Toggle the dark mode class on the relevant elements
    body.classList.toggle('bg-dark');
    sidebar.classList.toggle('bg-dark');
    sidebarHeader.classList.toggle('bg-dark');
    header.classList.toggle('bg-dark');
    navLinks.forEach(link => link.classList.toggle('bg-dark'));
    profileMenu.classList.toggle('bg-dark');
    profileButton.classList.toggle('bg-dark');
    profileMenuItems.forEach(item => item.classList.toggle('bg-dark'));

    // Toggle icon between moon and sun
    const darkModeIcon = darkModeToggle.querySelector('i');
    if (darkModeIcon.classList.contains('bi-moon')) {
        darkModeIcon.classList.replace('bi-moon', 'bi-sun');
        localStorage.setItem('darkMode', 'true'); // Save dark mode state in localStorage
    } else {
        darkModeIcon.classList.replace('bi-sun', 'bi-moon');
        localStorage.setItem('darkMode', 'false'); // Save dark mode state in localStorage
    }
});

// Sidebar toggle functionality
const sidebarToggler = document.getElementById('sidebarToggler');
const headerContent = document.querySelector('.header-content');

// Function to handle sidebar toggle and persistence
sidebarToggler.addEventListener('click', function () {
    sidebar.classList.toggle('hidden');

    // Check if the sidebar is hidden
    if (sidebar.classList.contains('hidden')) {
        headerContent.style.marginLeft = '-180px'; // Move content left when sidebar is hidden
        localStorage.setItem('sidebarHidden', 'true'); // Save sidebar state in localStorage
    } else {
        headerContent.style.marginLeft = '0'; // Reset margin when sidebar is visible
        localStorage.setItem('sidebarHidden', 'false'); // Save sidebar state in localStorage
    }
});

// Dropdown animation handling (for the profile dropdown)
const profileDropdown = document.getElementById('profileDropdown');
const dropdownMenu = document.querySelector('.dropdown-menu');
profileDropdown.addEventListener('click', function () {
    dropdownMenu.classList.toggle('show');
});

// Initialize Bootstrap dropdown for toggling (needed for Bootstrap JS components)
const bsDropdown = new bootstrap.Dropdown(profileDropdown);

// On page load, restore dark mode and sidebar state
document.addEventListener('DOMContentLoaded', function () {
    // Temporarily remove transitions to prevent animations during page load
    sidebar.style.transition = 'none';
    headerContent.style.transition = 'none';

    // Restore dark mode from localStorage
    const darkModeState = localStorage.getItem('darkMode');
    if (darkModeState === 'true') {
        body.classList.add('bg-dark');
        sidebar.classList.add('bg-dark');
        sidebarHeader.classList.add('bg-dark');
        header.classList.add('bg-dark');
        navLinks.forEach(link => link.classList.add('bg-dark'));
        profileMenu.classList.add('bg-dark');
        profileButton.classList.add('bg-dark');
        profileMenuItems.forEach(item => item.classList.add('bg-dark'));

        const darkModeIcon = darkModeToggle.querySelector('i');
        darkModeIcon.classList.replace('bi-moon', 'bi-sun');
    }

    // Restore sidebar visibility from localStorage
    const sidebarState = localStorage.getItem('sidebarHidden');
    if (sidebarState === 'true') {
        sidebar.classList.add('hidden');
        headerContent.style.marginLeft = '-180px'; // Move content left when sidebar is hidden
    } else {
        sidebar.classList.remove('hidden');
        headerContent.style.marginLeft = '0'; // Reset margin when sidebar is visible
    }

    // Re-enable transitions after a short delay
    setTimeout(function () {
        sidebar.style.transition = 'all 0.3s ease';
        headerContent.style.transition = 'margin-left 0.3s ease';
    }, 50); // Add a small delay to ensure styles are applied before the transition
});
