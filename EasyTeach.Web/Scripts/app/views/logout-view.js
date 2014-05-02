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
		},
		logoutSuccess: function () {
			window.alert("logout success");
			utils.redirectTo("home#login");
		},
		userLogout: function () {
			this.model.logout();
			return false;
		}
	});
});
