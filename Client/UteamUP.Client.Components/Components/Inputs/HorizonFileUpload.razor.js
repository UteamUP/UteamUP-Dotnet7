export function openFileDialog(inputId) {
    var fileInput = document.getElementById(inputId);

    fileInput.value = "";
    fileInput.click();

    const event = new Event('change', {bubbles: true});
    fileInput.dispatchEvent(event);
}

export function setupDropZone(id, inputId) {
    var dropzone = document.getElementById(id);
    var fileInput = document.getElementById(inputId);

    dropzone.ondragover = dropzone.ondragenter = function (evt) {
        evt.preventDefault();
    };

    dropzone.ondrop = function (evt) {
        fileInput.files = evt.dataTransfer.files;

        const event = new Event('change', {bubbles: true});
        fileInput.dispatchEvent(event);

        evt.preventDefault();
    };
}