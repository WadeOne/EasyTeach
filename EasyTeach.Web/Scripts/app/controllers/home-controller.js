define([
        'controllers/base/controller',
        'views/login-view',
        'views/register-view'
    ], function(Controller, LoginView, RegisterView) {
        var HomeController = Controller.extend({
            login: function () {
                this.view = new LoginView({region: 'main'});
            },
            register: function () {
                this.view = new RegisterView({region: 'main'});
            }
        });
    return HomeController;
    });