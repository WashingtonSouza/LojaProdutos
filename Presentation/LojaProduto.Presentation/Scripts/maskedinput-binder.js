$(function () {
    $('[mask]').each(function (e) {
        $(this).mask($(this).attr('mask'));
    });
});
