define([
  'views/base/view',
  'models/user-login',
  'text!../templates/login.html'
], function (View, UserLogin, template) {
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
    userLogin: function (event) {
        var form = $('#login-form'),
          userLoginData = {
              username: form.find('input[name=username]').val(),
              password: form.find('input[name=password]').val(),
              grant_type: 'password'
          },
        user = new UserLogin();
        user.save(
            {
                data: userLoginData,
                contentType: 'application/xxx-form-urlencoded'
            },
            {
            success: function () {
                console.log(success);
            },
            error: function (e) {
                console.log(e);
            }
        });
        return false;
    }    
  });

  return LoginView;
});
