define([
    'views/base/view',
    'text!templates/reset-password.html',
     'models/reset-password'
], function (View, template, ResetPasswordModel) {
    'use strict';

    return View.extend({
        container: '#content',
        template: template,
        autoRender: true,
        events: {
            "click #reset-btn": "resetPassword"
        },
        resetPassword: function () {
            var form = this.$('#reset-password-form');

            new ResetPasswordModel({
                email: form.find('input[name=email]').val()
            }).on('success', this.resetPasswordSuccess)
            .on('error', this.resetPasswordFail)
            .save();

            return false;
        },
        resetPasswordSuccess: function (data) {
            window.alert("success: " + data);
        },
        resetPasswordFail: function (model, errorData) {
            window.alert(errorData.statusText);
        }
    });
});
