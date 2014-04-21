define([
        'controllers/base/controller',
        'views/login-view',
        'views/register-view',
        'views/reset-password-view'
    ], function(Controller, LoginView, RegisterView, ResetPasswordView) {
        var HomeController = Controller.extend({
            login: function () {
                this.view = new LoginView({region: 'main'});
            },
            register: function () {
                this.view = new RegisterView({region: 'main'});
            },
            resetPassword: function () {
                this.view = new ResetPasswordView({ region: 'main' });
            }
        });
    return HomeController;
    });