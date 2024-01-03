document.addEventListener('DOMContentLoaded', function () {
    const body = document.body;

    document.getElementById('sunIcon').addEventListener('click', function () {
        setTheme('light');
    });

    document.getElementById('moonIcon').addEventListener('click', function () {
        setTheme('dark');
    });

    const savedTheme = localStorage.getItem('theme');
    if (savedTheme) {
        setTheme(savedTheme);
    }
});

function setTheme(theme) {
    const body = document.body;

    if (theme === 'light') {
        body.classList.remove('dark-mode');
        localStorage.setItem('theme', 'light');
    } else if (theme === 'dark') {
        body.classList.add('dark-mode');
        localStorage.setItem('theme', 'dark');
    }
}
