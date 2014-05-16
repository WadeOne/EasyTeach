define([
	'underscore',
	'views/base/view',
	'text!templates/set-password.html'
], function (_, View, template) {
    'use strict';

    return View.extend({
        container: '#content',
        className: 'row',
        template: _.template(template),
        autoRender: true,
        events: {
            "submit #set-password-form": "setPassword"
        },
        initialize: function () {
            this.listenTo(this.model, "sync", this.setPasswordSuccess);
            this.listenTo(this.model, "error", this.render);
        },
        setPassword: function () {
            var data = {
                //TODO : set data from other model
                userId: 3,
                resetPasswordToken: "99f0f561184ede9",
                newPassword: $("#set-password-form").find("input[name=password]").val()
            };
            this.model.save(data);
            return false;
        },
        setPasswordSuccess: function () {
            alert("success");
        }
    });
});