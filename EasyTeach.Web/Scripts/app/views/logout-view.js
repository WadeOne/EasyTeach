define([
	'underscore',
	'views/base/view',
	'text!templates/logout.html',
	'lib/utils'
], function (_, View, template, utils) {
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
			this.listenTo(this.model, "sync", this.logoutSuccess);
			this.listenTo(this.model, "error", this.logoutFail);
		},
		logoutSuccess: function () {
			window.alert("logout success");
			utils.redirectTo("home#login");
		},
		logoutFail: function (model, errorData) {
			window.alert("logout fail");
		},
		userLogout: function () {
			this.model.logout();

			return false;
		}
	});
});
