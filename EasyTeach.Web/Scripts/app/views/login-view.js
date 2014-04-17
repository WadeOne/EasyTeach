define([
  'views/base/view',
  'text!../templates/login.html'
], function(View, template) {
  'use strict';

  var LoginView = View.extend({
    // Automatically render after initialize
    container: '#content',
    id: 'site-container',
    template: template,
    autoRender: true
  });

  return LoginView;
});
