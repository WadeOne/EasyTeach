
define([
        'controllers/base/controller',
        'views/login-view',
        'views/register-view',
        'views/site-view'
    ], function(Controller, LoginView, RegisterView, SiteView) {
        var HomeController = Controller.extend({
            beforeAction: function () {
                this.reuse('site', SiteView);
                //this.reuse('login', LoginView);
            },
            /*renderView: function () {
                this.view = new SiteView();
            },*/
            login: function () {
                this.view = new LoginView({region: 'main'});
            }
        });
        return HomeController;
    });