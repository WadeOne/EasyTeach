define([
	'underscore',
	'views/base/view',
	'text!templates/confirm-email.html',
    'lib/utils'
], function (_, View, template, utils) {
    'use strict';

    return View.extend({
        container: '#content',
        className: 'row',
        template: _.template(template),
        autoRender: true,
        events: {
            "click .easy-teach-btn": "setPassword"
        },
        setPassword: function () {
            utils.redirectTo(
                "home#setPassword",
                _.pick(this.model.toJSON(), ["userId", "resetPasswordToken"])
            );

            return false;
        }
    });
});