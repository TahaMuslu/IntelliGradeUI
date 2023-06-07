// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function showLoader(text) {
    document.getElementById("js-preloader").classList.remove("loaded");
    if (text != undefined && text != null && text != "")
    document.getElementById("loader_text").innerText = text;
}

function hideLoader() {
    document.getElementById("js-preloader").classList.add("loaded");
}

function logOut() {
    console.log("logOut");
    document.cookie = "Token=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
    document.cookie = "Success=show;"
    document.cookie = "SuccessMessage=Ba%C5%9Far%C4%B1yla%20%C3%A7%C4%B1k%C4%B1%C5%9F%20yapt%C4%B1n%C4%B1z.;"
    window.location.href = "/Base";
}

toasts = [];
toasts = document.getElementsByClassName("toast");

setTimeout(function () {
for (var i = 0; i < toasts.length; i++) {
        toasts[i].classList.add("d-none");
    }
}, 4300);