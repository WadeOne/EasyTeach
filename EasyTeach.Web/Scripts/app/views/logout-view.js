define([
    'chaplin',
    'views/base/view',
    'models/user-logout',
    'text!templates/logout.html'
], function (Chaplin, View, UserLogout, template) {
    'use strict';

    return View.extend({
        container: '.logout-wrapper',
        template: _.template(template),
        autoRender: true,
        noWrap: true,
        events: {    
            "click #logout-btn": "userLogout"
        },
        initialize: function () {
            this.model = new UserLogout();

            this.listenTo(this.model, "sync", this.logoutSuccess);
            this.listenTo(this.model, "error", this.logoutFail);
        },
        logoutSuccess: function () {
            alert("logout success")
            Chaplin.utils.redirectTo("home#login");
        },
        logoutFail: function (model, errorData) {
            alert("logout fail");
        },
        userLogout: function () {
            this.model.save();
            return false;
        }
    });
});
