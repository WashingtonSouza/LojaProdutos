window.AjaxUtils || (window.AjaxUtils = {});
window.AjaxUtils.singleton || (window.AjaxUtils.singleton = {});

window.AjaxUtils.loading = (function () {
    function loading(config) {
        this.config = config;

        $(document).ready(function (_this) {
            return function () {
                _this.config.divloading = $(_this.config.divloading);
                _this.bindEventStart();
                _this.bindEventStop();
            }
        }(this));
    }

    loading.prototype.bindEventStart = function () {
        $(document).unbind('ajaxStart').bind('ajaxStart', function (_this) {
            return function () {
                _this.showLoading();
            }
        }(this));
    }

    loading.prototype.bindEventStop = function () {
        $(document).unbind('ajaxStop').bind('ajaxStop', function (_this) {
            return function () {
                _this.hideLoading();
            }
        }(this));
    }

    loading.prototype.showLoading = function () {
        this.config.divloading.modal();
        this.bindEventStop();
    }

    loading.prototype.hideLoading = function () {
        this.config.divloading.modal('hide');
        this.bindEventStart();
    }

    return loading;
})();

window.AjaxUtils.singleton.Loading = new window.AjaxUtils.loading({ divloading: '#loading' });

$.ajaxSetup({
    statusCode: {
        401: function (response) {
            window.location.href = 'Home/AccessDenied';
        }
    }
});

$.validator.setDefaults({ ignore: ":hidden:not(select)" });