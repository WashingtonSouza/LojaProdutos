//button loading
//(function ($) {
//    $(document).ajaxComplete(function (event, xhr, settings) {
//        $("button[data-loading-text],a[data-loading-text],input[type=submit][data-loading-text],input[type=button][data-loading-text]").each(function () {
//            var btn = $(this);
//            btn.button('reset');
//        });

//        $('.chosen').chosen({ width: '100%' })
//    });

//    $('body').on('click', '[data-loading-text]', function () {
//        var button = $(this);
//        button.closest('form').on('submit', function () {
//            if (!$(this).valid()) {
//                button.button('reset');
//            }
//        });
//        button.button('loading');
//    });
//}(jQuery));

//ajax json response
(function ($) {
    $(document).ajaxComplete(function (event, xhr, settings) {
        var contentType = xhr.getResponseHeader("Content-Type");
        if (contentType !== null && contentType.indexOf("application/json") !== -1) {
            var data = $.parseJSON(xhr.responseText);
            if (data.type == "redirect" && data.url) {
                $('.modal').modal('hide');
                window.location = data.url;
            }
        }
    });
}(jQuery));

//modal show
(function ($) {
    $('.modal-footer').on('click', 'input[type=submit],button[type=submit]', function (event) {
        event.preventDefault();
        $(this).closest('.modal').find('.modal-body form').submit();
    });

    $('body').on('click', '[data-modal-target]', function () {
        $($(this).attr('data-modal-target')).modal('show');
    });
}(jQuery));

$.fn.showModal = function () {
    this.modal('show');
    return this;
}