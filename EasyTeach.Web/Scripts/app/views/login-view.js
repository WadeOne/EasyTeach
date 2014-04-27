define([
    'underscore',
    'views/base/view',
    'models/user-login',
    'text!templates/login.html',
    'utils/serialize',
    'lib/utils'
], function (_, View, UserLogin, template, serialize, utils) {
    'use strict';

    return View.extend({
        container: '#content',
        className: 'row',
        template: _.template(template),
        autoRender: true,
        events: {
            "submit #login-form": "userLogin"
        },
        initialize: function() {
            this.listenTo(this.model, "sync", this.loginSuccess);
            this.listenTo(this.model, "error", this.loginFail);
        },
        userLogin: function() {
            var data = serialize.form(this.$('#login-form'));

            this.model.save(data);

            return false;
        },
        loginSuccess: function() {
            utils.redirectTo("students#grades");
        },
        loginFail: function (model, errorData) {
            _.extend(model, {
                errorMessage: errorData.responseJSON.error_description
            });
            this.render();
        },
        render: function() {
            var html = this.template({
                errorModel: this.model
            });

            this.$el.html(html);

            return this;
        }
    });
});
