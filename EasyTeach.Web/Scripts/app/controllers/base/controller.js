define(['chaplin', 'views/login-view'], function(Chaplin, LoginView) {
  'use strict';

  var Controller = Chaplin.Controller.extend({
    // Place your application-specific controller features here.
    beforeAction: function() {
      this.reuse('login', LoginView);
    }
  });

  return Controller;
});
