define([
  'views/base/view',
  'models/user',
  'text!../templates/register.html'
], function(View, User, template) {
  'use strict';

  var RegisterView = View.extend({
    container: '#content',
    id: 'site-container',
    template: template,
    autoRender: true,
    noWrap: true,
    events: {
        "submit #user-registration-form": "createUser"
    },
    createUser: function (ev) {
      var form = $(ev.currentTarget),
          userInfo = {
            firstName: form.find('input[name=firstName]').val(),
            lastName: form.find('input[name=lastName]').val(),
            group: {
              groupNumber: form.find('input[name=groupNumber]').val(),
              year: form.find('input[name=year]').val()
            },
            email: form.find('input[name=email]').val()
          },
          user = new User();
      user.save(userInfo, {
        success: function (user) {
          console.log(success);
        },
        error: function (e) {
          console.log(e);
        }
      });
      return false;
    }
  });

  return RegisterView;
});
