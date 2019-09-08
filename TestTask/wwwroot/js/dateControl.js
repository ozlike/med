let dateSpan, datepicker, formDateInput;

document.addEventListener("DOMContentLoaded", function () {
    dateSpan = document.getElementById("dateSpan");
    datepicker = document.getElementById("datepicker");
    formDateInput = document.getElementById("formDate");

    datepicker.addEventListener("input", checkDate);
    
    document.getElementById("submitBtn").addEventListener("click", function (e) {
        if (!checkDate()) e.preventDefault();
    });
});

function checkDate() {
    let dateArray = datepicker.value.split('.').filter(x=>x.length > 0);
    if (dateArray.length != 3) return showInvalidDateMessage();
    var date = new Date(dateArray[2], parseInt(dateArray[1]) - 1, dateArray[0]);
    if (isNaN(date) || date > new Date() || new Date().getFullYear() - date.getFullYear() > 150) return showInvalidDateMessage();
    formDateInput.value = dateArray[2] + "-" + dateArray[1] + "-" + dateArray[0];
    dateSpan.innerHTML = "";
    return true;
}

function showInvalidDateMessage() {
    dateSpan.innerHTML = "Неверная дата";
    return false;
}

$(function () {
    $("#datepicker").datepicker({
        minDate: '-150Y',
        maxDate: 'today',
        dateFormat: 'dd.mm.yy',
        onSelect: checkDate
    });
});