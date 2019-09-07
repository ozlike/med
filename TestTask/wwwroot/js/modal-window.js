document.addEventListener("DOMContentLoaded", function () {
    let modalWindow = document.getElementsByClassName("modal-window-container")[0];
    document.getElementById("deleteGraft").addEventListener("click", function () {
        modalWindow.classList.remove("hide");
    });
    modalWindow.getElementsByClassName("no")[0].addEventListener("click", function () {
        modalWindow.classList.add("hide");
    });
    modalWindow.getElementsByClassName("yes")[0].addEventListener("click", function () {
        document.getElementById("deleteFormContainer").children[0].submit();
    });
});