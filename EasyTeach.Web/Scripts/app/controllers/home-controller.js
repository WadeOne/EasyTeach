
define([
        'controllers/base/controller',
        'views/login-view',
        'views/register-view',
        'views/site-view'
    ], function(Controller, LoginView, RegisterView, SiteView) {
        var HomeController = Controller.extend({
            beforeAction: function () {
                this.reuse('site', SiteView);
            },
            login: function () {
                this.view = new LoginView({region: 'main'});
            }
        });
        return HomeController;
    });