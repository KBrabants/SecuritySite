// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function viewPage(id) {
    fetch(`?handler=PageView&id=${id}`).then((response) => { return response.text(); }).then((result) => { document.getElementById("partial-container").innerHTML = result;});
};
          
