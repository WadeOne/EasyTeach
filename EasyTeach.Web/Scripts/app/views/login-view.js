define([
    'chaplin',
    'views/base/view',
    'models/user-login',
    'text!templates/login.html',
    'utils/serialize'
], function (Chaplin, View, UserLogin, template, serialize) {
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
        initialize: function() {
            this.model = new UserLogin();

            this.listenTo(this.model, "sync", this.loginSuccess);
            this.listenTo(this.model, "error", this.loginFail);
        },
        userLogin: function() {
            var data = serialize.form(this.$('#login-form'));

            this.model.save(data);

            return false;
        },
        loginSuccess: function() {
            Chaplin.utils.redirectTo("students#grades");
        },
        loginFail: function(model, errorData) {
            window.alert(errorData.statusText);
        }
    });
});
