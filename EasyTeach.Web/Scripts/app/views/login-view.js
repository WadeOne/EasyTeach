define([
    'underscore',
    'views/base/view',
    'models/user-login',
    'text!templates/login.html',
    'utils/serialize',
    'lib/utils',
    'config/public-routes'
], function (_, View, UserLogin, template, serialize, utils, routes) {
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
            this.listenTo(this.model, "error", this.render);
        },
        userLogin: function() {
            var data = serialize.form(this.$('#login-form'));

            this.model.save(data, {
                error: function (model, response) {

                }
            });

            return false;
        },
        loginSuccess: function() {
            utils.redirectTo(routes.loginRedirect);
        }
    });
});