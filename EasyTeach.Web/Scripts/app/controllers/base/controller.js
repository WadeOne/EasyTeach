define([
    'chaplin',
    'views/site-view',
    'views/shared/menu',
    'views/logout-view',
    'models/shared/menu',
    'models/user-logout'
], function(Chaplin, SiteView, MenuView, LogoutView, Menu, UserLogout) {
    'use strict';

    var menu = new Menu();

    return Chaplin.Controller.extend({
      beforeAction: function () {
          this.reuse('site', SiteView);
          this.reuse('menu', MenuView, { model: menu });
          this.reuse('logout', LogoutView, {model: new UserLogout()});
      }
  });
});
