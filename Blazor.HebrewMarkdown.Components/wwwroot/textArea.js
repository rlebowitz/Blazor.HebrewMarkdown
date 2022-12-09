// This is a JavaScript module that is loaded on demand. It can export any number of
// functions, and may import other JavaScript modules if required.
// https://gomakethings.com/automatically-expand-a-textarea-as-the-user-types-using-vanilla-javascript/

export function setHeight(element) {
    element.style.height = 'inherit';
    // Get the computed styles for the element
    var computed = window.getComputedStyle(element);
    // Calculate the height
    var height = parseInt(computed.getPropertyValue('border-top-width'), 10)
        + parseInt(computed.getPropertyValue('padding-top'), 10)
        + element.scrollHeight
        + parseInt(computed.getPropertyValue('padding-bottom'), 10)
        + parseInt(computed.getPropertyValue('border-bottom-width'), 10);
    element.style.height = height + 'px';
    console.log('Height: ' + element.style.height);
}

export function initialize() {
    document.addEventListener('input', function (event) {
        if (event.target.tagName !== 'TEXTAREA') return;
        setHeight(event.target);
    }, false);
    console.log('Initialized!');
}





