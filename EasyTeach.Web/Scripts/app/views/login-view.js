define([
  'views/base/view',
  'text!../templates/login.html'
], function(View, template) {
  'use strict';

  var LoginView = View.extend({
    // Automatically render after initialize
    autorender: true,
    className: 'login',

    // Save the template string in a prototype property.
    // This is overwritten with the compiled template function.
    // In the end you might want to used precompiled templates.
    template: template
  });

  return LoginView;
});
