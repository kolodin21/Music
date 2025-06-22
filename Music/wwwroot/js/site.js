"use strict";

$.fn.setLoading = function (isLoading) {
    return this.each(function () {
        const $btn = $(this);
        if (isLoading) {
            $btn.addClass('button-loading');
            $btn.prop('disabled', true);
        } else {
            $btn.removeClass('button-loading');
            $btn.prop('disabled', false);
        }
    });
};