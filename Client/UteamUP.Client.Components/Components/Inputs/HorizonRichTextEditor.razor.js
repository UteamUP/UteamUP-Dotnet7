export function openFileDialog(inputId) {
    var fileInput = document.getElementById(inputId);

    fileInput.value = "";
    fileInput.click();

    const event = new Event('change', {bubbles: true});
    fileInput.dispatchEvent(event);
}