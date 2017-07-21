(function ($) {

    $('form').on('submit', customAjaxConfirm);

    $('form').each(function () {

        var $this = $(this);
        var element = $this.attr('data-ajax-confirm');

        if (element) {

            $this.removeAttr('data-ajax-confirm');
            $this.attr('data-modal-confirm', element);
        }
    });

    function customAjaxConfirm(e) {

        if (!$(this).valid()) {
            return false;
        }

        var element = $(this).attr('data-modal-confirm')

        if (!element) {
            return true;
        }

        e.preventDefault();
        e.stopImmediatePropagation();

        var obj = $.parseJSON(element);

        bootbox.confirm({
            title: obj.title,
            message: obj.message,
            callback: function (result) {

                if (result) {
                    var elements = handleComposite($(e.target));
                    $(e.target).unbind('submit', customAjaxConfirm).submit().bind('submit', customAjaxConfirm);
                    enableElements(elements);
                }
            }
        });

        return false;
    }
}(jQuery));

function handleComposite(form) {

    var elements = [];

    $(form).find('.form-control-composite:enabled').each(function (i, element) {

        var fields = $(element).attr('data-composite-fields');

        if (fields) {

            var keys = fields.split(',');
            var values = $(element).val().split(',');
            var parentName = $(element).attr('name');
            elements.push(parentName);

            while (values.length < keys.length)
                values.push('');

            $(keys).each(function (j, key) {

                var name = parentName + "." + key;
                var item = $(form).find("input:hidden[name='" + name + "']")[0];
                var value = values[j];

                if (item) {
                    $(item).val(value);
                }
                else {
                    $('<input>', { type: 'hidden', name: name, value: value }).appendTo($(form));
                }
            });

            $(element).prop('disabled', true);
        }
    });

    return elements;
}

function enableElements(elements) {

    $.each(elements, function (i, element) {
        $("[name='" + element + "']").prop('disabled', false);
    });
}

function showMessage(data) {

    bootbox.alert({
        title: data.title,
        message: data.message,
        callback: function () {
            if (data.redirectTo) {
                location.href = data.redirectTo;
                return false;
            }
        }
    });
}

function showConfirmDelete(btn, page) {

    var target = $(btn);

    bootbox.confirm({
        title: 'Excluir',
        message: 'Deseja realmente excluir o registro?',
        callback: function (result) {
            if (result) {
                $.ajax({
                    type: 'POST',
                    url: target.attr('href'),
                    dataType: 'json',
                    cache: false,
                    async: true,
                    error: function (xhr) {
                        showMessage(xhr.responseJSON);
                    },
                    success: function (json) {
                        showMessage(json);

                        if (json.success) {

                            var count = target.closest('tbody').find('tr').length;

                            if (count > 1 || $(page).text() == "1") {
                                $(page).click();
                            } else {
                                $(page).parent().prev().find('span').click();
                            }
                        }
                    }
                });
            }
        }
    });
}

function showConfirm(title, message) {

    var target = $(event.target);

    bootbox.confirm({
        title: title,
        message: message,
        callback: function (result) {
            if (result) {
                $.ajax({
                    type: 'POST',
                    url: target.attr('href'),
                    dataType: 'json',
                    cache: false,
                    async: true,
                    error: function (xhr) {
                        showMessage(xhr.responseJSON);
                    },
                    success: function (json) {
                        if (json.success && json.redirectTo) {
                            location.href = json.redirectTo;
                        }
                    }
                });
            }
        }
    });
}