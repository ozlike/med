document.addEventListener("DOMContentLoaded", function () {
    document.getElementById("submitBtn").addEventListener("click", function (e) {
        let date = new Date(document.getElementById("datepicker").value);
        let dateSpan = document.getElementById("dateSpan");
        if (isNaN(date) || date > new Date() || new Date().getFullYear() - date.getFullYear() > 150) {
            dateSpan.innerHTML = "Неверная дата";
            e.preventDefault();
        }
        else dateSpan.innerHTML = "";
    });
});

$(function () {
    $("#datepicker").datepicker({
        minDate: '-150Y',
        maxDate: 'today',
        dateFormat: 'dd.mm.yy'
    });
});