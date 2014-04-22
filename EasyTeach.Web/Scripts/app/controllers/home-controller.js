define([
        'controllers/base/controller',
        'views/login-view',
        'views/register-view',
        'views/reset-password-view',
        'views/add-homeworks-view'
    ], function(Controller, LoginView, RegisterView, ResetPasswordView, AddHomeworksView) {
        var HomeController = Controller.extend({
            login: function () {
                this.view = new LoginView({region: 'main'});
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
    return HomeController;
    });