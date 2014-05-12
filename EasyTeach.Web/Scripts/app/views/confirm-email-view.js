define([
	'underscore',
	'views/base/view',
	'text!templates/confirm-email.html'
], function (_, View, template) {
    'use strict';

    return View.extend({
        container: '#content',
        className: 'row',
        template: _.template(template),
        autoRender: true,
        initialize: function (params) {
            var data = params.data;
            var userData = {
                userId: data.userId,
                confirmEmailToken: data.token
            };
            this.model.save(userData);
        }
    });
});