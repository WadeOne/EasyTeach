define([
	'underscore',
	'views/base/view',
	'text!templates/set-password.html',
    'utils/serialize',
    'lib/utils'
], function (_, View, template, serialize, utils) {
    'use strict';

    return View.extend({
        container: '#content',
        className: 'row',
        template: _.template(template),
        autoRender: true,
        initialize: function () {
            this.listenTo(this.model, "sync", this.setPasswordSuccess);
            this.listenTo(this.model, "error", this.render);
            this.listenTo(this.model, "invalid", this.render);
        },
        events: {
            "submit #set-password-form": "setPassword"
        },
        setPassword: function () {
            this.model.save(serialize.form(this.$("#set-password-form")));
            return false;
        },
        setPasswordSuccess: function () {
            utils.redirectTo("home#login");
        }
    });
});