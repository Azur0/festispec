// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('.nav-toggle').on('click', function () {
    $('body').toggleClass('nav-open');
});

$('.inspection--title').on('click', function () {
    $(this).parent().toggleClass('inspection--open');
    $(this).parent().find('.inspection--details').slideToggle({duration: 200});
});

$('.inspection--instructions-toggle').on('click', function () {
    $(this).parent().toggleClass('open');
    $(this).parent().find('.inspection--instructions-text').slideToggle({ duration: 200 });
});