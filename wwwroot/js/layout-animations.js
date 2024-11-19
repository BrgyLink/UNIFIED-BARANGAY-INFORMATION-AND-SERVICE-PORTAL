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

darkModeToggle.addEventListener('click', function () {
	body.classList.toggle('bg-dark');
	sidebar.classList.toggle('bg-dark');
	sidebarHeader.classList.toggle('bg-dark'); // Add this line to toggle dark mode for sidebar header
	header.classList.toggle('bg-dark');
	navLinks.forEach(link => link.classList.toggle('bg-dark'));
	profileMenu.classList.toggle('bg-dark');
	profileButton.classList.toggle('bg-dark');
	profileMenuItems.forEach(item => item.classList.toggle('bg-dark')); // Toggle dark mode for each dropdown item

	// Toggle icon between moon and sun
	const darkModeIcon = darkModeToggle.querySelector('i');
	if (darkModeIcon.classList.contains('bi-moon')) {
		darkModeIcon.classList.replace('bi-moon', 'bi-sun');
	} else {
		darkModeIcon.classList.replace('bi-sun', 'bi-moon');
	}
});


// Sidebar toggle functionality
const sidebarToggler = document.getElementById('sidebarToggler');

sidebarToggler.addEventListener('click', function () {
	sidebar.classList.toggle('hidden');
	const headerContent = document.querySelector('.header-content');

	// Check if the sidebar is hidden
	if (sidebar.classList.contains('hidden')) {
		headerContent.style.marginLeft = '-209px'; // Move content left when sidebar is hidden

	} else {
		headerContent.style.marginLeft = '0'; // Reset margin when sidebar is visible
	}
});

// Dropdown animation handling
const profileDropdown = document.getElementById('profileDropdown');
profileDropdown.addEventListener('click', function () {
	dropdownMenu.classList.toggle('show');
});
