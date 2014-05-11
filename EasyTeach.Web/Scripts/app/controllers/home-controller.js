define([
	'controllers/base/controller',
	'views/login-view',
	'views/register-view',
	'views/reset-password-view',
	'views/confirm-email-view',
	'views/set-password',
	'views/add-homeworks-view',
	'models/user-login',
    'models/confirm-email',
    'models/set-password'
], function(Controller, LoginView, RegisterView, ResetPasswordView, ConfirmEmailView, SetPasswordView, AddHomeworksView, UserLogin, ConfirmEmail, SetPassword) {
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
		    this.view = new ConfirmEmailView({ region: 'main', data: params, model: new ConfirmEmail()});
		},
		setPassword: function () {
			this.view = new SetPasswordView({ region: 'main', model: new SetPassword()});
		},
		addHomeworks: function () {
			this.view = new AddHomeworksView({ region: 'main' });
		}
	});
});