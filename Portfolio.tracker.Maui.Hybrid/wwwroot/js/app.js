function hideLanguageDropdown(elementId) {
    document.getElementById(elementId).click();
}

function getElementValueById(id) {
    return document.getElementById(id).value;
};

let rowIndex = null;

function getTableRowNumberFromButtonClicked() {
    document.querySelectorAll('button').forEach(button => {
        button.addEventListener('click', function () {
            let row = this.closest('tr');
            rowIndex = (Array.from(row.parentNode.children).indexOf(row)).toString();
        });
    });

    return rowIndex;
}