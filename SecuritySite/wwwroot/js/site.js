// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function viewPage(id) {
    fetch(`?handler=PageView&id=${id}`).then((response) => { return response.text(); }).then((result) => { document.getElementById("partial-container").innerHTML = result;});
};
let isOpen = false;
function SideBar() {

    if (isOpen) {
        document.getElementById("side-bar").classList.add("side-bar-hidden");
        document.getElementById("main-container").classList.add("main-container-full");
        document.getElementById("burger-button").classList.add("burger-button-black");
        document.getElementById("side-bar").classList.remove("side-bar");
        document.getElementById("main-container").classList.remove("main-container");
        document.getElementById("burger-button").classList.remove("burger-button");
        isOpen = false;
    }
    else {
        document.getElementById("side-bar").classList.remove("side-bar-hidden");
        document.getElementById("main-container").classList.remove("main-container-full");
        document.getElementById("burger-button").classList.remove("burger-button-black");
        document.getElementById("side-bar").classList.add("side-bar");
        document.getElementById("main-container").classList.add("main-container");
        document.getElementById("burger-button").classList.add("burger-button");
        isOpen = true;
    }

}
