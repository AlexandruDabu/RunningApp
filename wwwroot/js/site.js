// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
//Login/Register-Success-Message
    document.addEventListener('DOMContentLoaded', function () {
        var successMessage = document.getElementById('message-success');
        if (successMessage) {
            setTimeout(function () {
                successMessage.classList.add('hidden');
                successMessage.addEventListener('transitionend', function () {
                    successMessage.remove();
                }, { once: true });
            }, 1500);
        }
    });

