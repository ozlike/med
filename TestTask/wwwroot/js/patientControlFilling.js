document.addEventListener("DOMContentLoaded", function () {
    let sexInput = document.getElementById("sexInput");
    document.getElementById("sSelected").setAttribute("selected", "selected");

    /* Управление полем 'Пол' */
    document.getElementById("sexSelect").addEventListener("input", function (e) {
        sexInput.value = e.target.children[e.target.selectedIndex].getAttribute("key");
    });


    /* События перед отправкой формы */
    document.getElementById("submitBtn").addEventListener("click", function (e) {
        let sexSpan = document.getElementById("sexSpan");
        if (sexInput.value.length == 0) {
            sexSpan.innerHTML = "Не выбран пол";
            e.preventDefault();
        }
        else sexSpan.innerHTML = "";

        let date = new Date(document.getElementById("datepicker").value);
        let dateSpan = document.getElementById("dateSpan");
        if (isNaN(date) || date > new Date() || new Date().getFullYear() - date.getFullYear() > 150) {
            dateSpan.innerHTML = "Ошибка в дате рождения";
            e.preventDefault();
        }
        else dateSpan.innerHTML = "";
    });
});

$(function () {
    $("#datepicker").datepicker({
        minDate: '-150Y',
        maxDate: 'today'
    });
});