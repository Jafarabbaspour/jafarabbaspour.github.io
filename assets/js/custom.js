
function navigateTo(section) {
    // Change the URL without reloading the page
    history.pushState(null, null, `/${section}`);
    
    // Scroll to the section
    const sectionElement = document.getElementById(section);
    if (sectionElement) {
        sectionElement.scrollIntoView({ behavior: 'smooth' });
    }
}
