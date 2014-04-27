define([
    'chaplin',
    'views/site-view',
    'views/shared/menu',
    'views/logout-view',
    'models/shared/menu',
    'models/user-logout',
    'models/user/session',
    'config/public-routes'
], function(Chaplin, SiteView, MenuView, LogoutView, Menu, UserLogout, UserSession, routes) {
    'use strict';

    var menu = new Menu();
    var session = new UserSession();
    session.fetch();

    return Chaplin.Controller.extend({
        userSession: function () {
            return session.getAttributes();
        },
        beforeAction: function (params, route) {
            var isAuthenticated = this.userSession().isAuthenticated === true;

            this.reuse('site', SiteView);

            if (isAuthenticated) {
                this.reuse('menu', MenuView, { model: menu });
                this.reuse('logout', LogoutView, {model: new UserLogout()});

                if (route.name === routes.login) {
                    this.redirectTo(routes.loginRedirect);
                }
            } else if (routes.isPublic(route.name) === false) {
                this.redirectTo(routes.login);
            }
        }
  });
});
