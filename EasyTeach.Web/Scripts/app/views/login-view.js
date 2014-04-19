define([
    'views/base/view',
    'models/user-login',
    'text!templates/login.html',
    'utils/serialize'
], function (View, UserLogin, template, serialize) {
    'use strict';

    return View.extend({
        container: '#content',
        id: 'site-container',
        template: template,
        autoRender: true,
        noWrap: true,
        events: {
            "click #login-btn": "userLogin"
        },
        userLogin: function() {
            var data = serialize.form(this.$('#login-form'));

            new UserLogin(data)
                .on('success', this.loginSuccess)
                .on('error', this.loginFail)
                .save();

            return false;
        },
        loginSuccess: function(data) {
            window.alert("success: " + data);
        },
        loginFail: function(errorData) {
            window.alert(errorData.statusText);
        }
    });
});
