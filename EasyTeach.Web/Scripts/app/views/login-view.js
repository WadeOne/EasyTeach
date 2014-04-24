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
        template: _.template(template),
        autoRender: true,
        noWrap: true,
        events: {
            "submit #login-form": "userLogin"
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
        loginFail: function (model, errorData) {
            _.extend(model, {
                errorMessage: $.parseJSON(errorData.responseText).error_description
            });
            this.render();
        },
        render: function() {
            var html = this.template({
                errorModel: this.model,
            });

            this.$el.html(html);

            return this;
        }
    });
});
