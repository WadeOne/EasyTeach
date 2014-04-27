define([
    'controllers/base/controller',
    'views/login-view',
    'views/register-view',
    'views/reset-password-view',
    'views/add-homeworks-view',
    'models/user-login'
], function(Controller, LoginView, RegisterView, ResetPasswordView, AddHomeworksView, UserLogin) {
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
        addHomeworks: function () {
            this.view = new AddHomeworksView({ region: 'main' });
        }
    });
});