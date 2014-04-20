define([
    'chaplin',
    'views/site-view',
    'views/shared/menu',
    'models/shared/menu'
], function(Chaplin, SiteView, MenuView, Menu) {
    'use strict';

    var menu = new Menu();

    return Chaplin.Controller.extend({
      beforeAction: function () {
          this.reuse('site', SiteView);
          this.reuse('menu', MenuView, {model: menu});
      }
  });
});
