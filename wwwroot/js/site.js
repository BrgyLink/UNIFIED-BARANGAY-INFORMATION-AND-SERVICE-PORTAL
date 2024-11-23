// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


document.addEventListener('DOMContentLoaded', function () {
    // Get all dropdown elements
    var dropdowns = document.querySelectorAll('.dropdown');

    dropdowns.forEach(function (dropdown) {
        // Add event listener for when the dropdown is shown
        dropdown.addEventListener('show.bs.dropdown', function () {
            var menu = dropdown.querySelector('.dropdown-menu');
            menu.style.display = 'block'; // Ensure it's displayed
            menu.classList.remove('dropdown-out'); // Remove any previous "out" class if present
            menu.classList.add('dropdown-in'); // Add the "in" class for animation
        });

        // Add event listener for when the dropdown is hidden
        dropdown.addEventListener('hidden.bs.dropdown', function () {
            var menu = dropdown.querySelector('.dropdown-menu');
            menu.classList.remove('dropdown-in'); // Remove the "in" class after animation
            setTimeout(function () {
                menu.style.display = 'none'; // Hide the dropdown after animation completes
            }, 300); // Match this with the animation duration (0.3s)
        });
    });
});

// Add event listener to toggle the dropdown
document.querySelector('.navbar-nav .dropdown').addEventListener('click', function (event) {
    event.stopPropagation(); // Prevent closing of dropdown
    this.classList.toggle('show'); // Toggle the 'show' class to display or hide the dropdown
});

// Close the dropdown if clicked anywhere outside of it
document.addEventListener('click', function (event) {
    if (!event.target.closest('.navbar-nav .dropdown')) {
        document.querySelector('.navbar-nav .dropdown').classList.remove('show');
    }
});
