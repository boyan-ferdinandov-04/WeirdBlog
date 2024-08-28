$(document).ready(function () {
    const lightMode = $('#light-mode');
    const darkMode = $('#dark-mode');
    const toggleButton = $('#theme-toggle');

    if (localStorage.getItem('theme') === 'dark') {
        darkMode.prop('disabled', false);
        lightMode.prop('disabled', true);
        toggleButton.text('Switch to Light Mode');
    } else {
        darkMode.prop('disabled', true);
        lightMode.prop('disabled', false);
        toggleButton.text('Switch to Dark Mode');
    }

    toggleButton.click(function () {
        let newTheme = 'light';
        if (darkMode.prop('disabled')) {
            darkMode.prop('disabled', false);
            lightMode.prop('disabled', true);
            toggleButton.text('Switch to Light Mode');
            newTheme = 'dark';
        } else {
            darkMode.prop('disabled', true);
            lightMode.prop('disabled', false);
            toggleButton.text('Switch to Dark Mode');
        }
        localStorage.setItem('theme', newTheme);
    });
});
