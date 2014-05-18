define([
	'controllers/base/controller',
	'views/login-view',
	'views/register-view',
	'views/reset-password-view',
	'views/confirm-email-view',
	'views/set-password',
	'views/add-homeworks-view',
    'views/student-homeworks-view',
	'models/user-login',
    'models/user',
    'models/confirm-email',
    'models/set-password'
], function(Controller, LoginView, RegisterView, ResetPasswordView, ConfirmEmailView, SetPasswordView, AddHomeworksView, StudentHomeworksView, UserLogin, User, ConfirmEmail, SetPassword) {
	"use strict";

	return Controller.extend({
		login: function () {
			this.view = new LoginView({region: 'main', model: new UserLogin()});
		},
		register: function () {
			this.view = new RegisterView({region: 'main', model: new User()});
		},
		resetPassword: function () {
			this.view = new ResetPasswordView({ region: 'main' });
		},
		confirmEmail: function (params) {
            this.view = new ConfirmEmailView({ region: 'main', model: new ConfirmEmail(params)});
		},
		setPassword: function (params) {
			this.view = new SetPasswordView({ region: 'main', model: new SetPassword(params)});
		},
		addHomeworks: function () {
			this.view = new AddHomeworksView({ region: 'main' });
		},
		showHomeworks: function () {
            this.view = new StudentHomeworksView({ region: 'main' });
		}
	});
});