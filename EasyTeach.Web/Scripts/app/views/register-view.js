define([
    'underscore',
	'views/base/view',
	'text!../templates/register.html'
], function (_, View, template) {
    'use strict';

	return View.extend({
		container: '#content',
		className: 'row',
		template: _.template(template),
		autoRender: true,
		events: {
			"submit #user-registration-form": "createUser"
		},
		initialize: function() {
			this.listenTo(this.model, "sync", this.registerSuccess);
			this.listenTo(this.model, "error", this.render);
		},
		createUser: function (ev) {
			var form = this.$(ev.currentTarget),
                userInfo = {
                    firstName: form.find('input[name=firstName]').val(),
                    lastName: form.find('input[name=lastName]').val(),
                    group: {
                        groupNumber: form.find('input[name=groupNumber]').val(),
                        year: form.find('input[name=year]').val()
                    },
                    email: form.find('input[name=email]').val()
                };

			this.model.save(userInfo);
            return false;
		},
		registerSuccess: function() {
            this.model.set(this.model.defaults);
            this.render();

			window.alert("success");
		}
	});
});
