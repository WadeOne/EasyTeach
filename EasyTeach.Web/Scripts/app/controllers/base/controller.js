define([
    'chaplin',
    'views/site-view',
    'views/shared/menu'
], function(Chaplin, SiteView, MenuView) {
  'use strict';

  var Controller = Chaplin.Controller.extend({
      beforeAction: function () {
          this.reuse('site', SiteView);
          this.reuse('menu', MenuView);
      }
  });

  return Controller;
});
