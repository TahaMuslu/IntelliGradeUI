// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function logOut() {
    console.log("logOut");
    document.cookie = "Token=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
    window.location.href = "/Login?register=false";
}

toasts = [];
toasts = document.getElementsByClassName("toast");

setTimeout(function () {
for (var i = 0; i < toasts.length; i++) {
        toasts[i].classList.add("d-none");
    }
}
, 4300);