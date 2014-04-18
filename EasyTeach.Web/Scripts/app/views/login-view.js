define([
  'views/base/view',
  'text!../templates/login.html'
], function(View, template) {
  'use strict';

  var LoginView = View.extend({
    container: '#content',
    id: 'site-container',
    template: template,
    autoRender: true,
    noWrap: true,
    events: {
        "click #login-btn": "userLogin"
    },
    userLogin: function () {
        alert(2);
        return false;
    }    
  });

  return LoginView;
});
