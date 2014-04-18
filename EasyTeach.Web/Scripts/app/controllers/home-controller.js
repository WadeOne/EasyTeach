define([
        'controllers/base/controller',
        'views/login-view'
    ], function(Controller, LoginView) {
        var HomeController = Controller.extend({
            login: function () {
                this.view = new LoginView({region: 'main'});
            }
        });
    return HomeController;
    });