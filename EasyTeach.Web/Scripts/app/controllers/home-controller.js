define([
	'controllers/base/controller',
	'views/login-view',
	'views/register-view',
	'views/reset-password-view',
	'views/confirm-email-view',
	'views/set-password',
	'views/add-homeworks-view',
	'models/user-login'
], function(Controller, LoginView, RegisterView, ResetPasswordView, ConfirmEmailView, SetPasswordView, AddHomeworksView, UserLogin) {
	"use strict";

	return Controller.extend({
		login: function () {
			this.view = new LoginView({region: 'main', model: new UserLogin()});
		},
		register: function () {
			this.view = new RegisterView({region: 'main'});
		},
		resetPassword: function () {
			this.view = new ResetPasswordView({ region: 'main' });
		},
		confirmEmail: function (params) {
			window.console.log(params.token);
			this.view = new ConfirmEmailView({ region: 'main' });
		},
		setPassword: function () {
			this.view = new SetPasswordView({ region: 'main' });
		},
		addHomeworks: function () {
			this.view = new AddHomeworksView({ region: 'main' });
		}
	});
});