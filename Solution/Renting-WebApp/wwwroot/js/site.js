// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
<script>
    document.addEventListener('scroll', function() {
        const scrollPosition = window.scrollY;
    const parallaxContainer = document.querySelector('.parallax-container');

    // Create a reversed parallax effect where the background image moves
    const offset = -scrollPosition * 0.3; // Adjust multiplier to change speed and direction
    parallaxContainer.style.backgroundPositionY = `${offset}px`;
    });
</script>



