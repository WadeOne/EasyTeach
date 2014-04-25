define([
  'views/base/view',
  'models/user',
  'text!../templates/register.html'
  ], function(View, User, template) {
    'use strict';

    var RegisterView = View.extend({
      container: '#content',
      id: 'site-container',
      template: _.template(template),
      autoRender: true,
      noWrap: true,
      events: {
        "submit #user-registration-form": "createUser"
      },
      initialize: function() {
        this.model = new User();

        this.listenTo(this.model, "sync", this.registerSuccess);
        this.listenTo(this.model, "error", this.registerFail);
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
        };

        this.model.save(userInfo);
        return false;
      },
      registerSuccess: function(){
        alert("success");        
      },
      registerFail: function(model, errorData){
        _.extend(model, {
          errorMessage: $.parseJSON(errorData.responseText).modelState.email
        });
        this.render();        
      },
      render: function() {
        var html = this.template({
          errorModel: this.model,
        });

        this.$el.html(html);

        return this;
      }
    });

return RegisterView;
});
