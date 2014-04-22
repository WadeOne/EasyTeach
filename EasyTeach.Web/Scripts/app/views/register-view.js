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
          user = new User(),
          that = this;
      user.save(userInfo, {
        success: function (user) {
          console.log(success);
        },
        //TODO: move to the template, add styles
        error: function (model, response) {
          that.$el.before('<div class="error-message">'+ $.parseJSON(response.responseText).message +'</div>')
        }
      });
      return false;
    }
  });

  return RegisterView;
});
