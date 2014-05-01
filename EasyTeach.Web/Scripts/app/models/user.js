define([
  'models/base/model'
], function(Model) {
  'use strict';

  var User = Model.extend({
    url: '../api/User/Register',
    defaults: {
            errorMessage: "",
        },
        initialize: function() {
            this.on("error", this.errorHandler);
        },
        sync: function(method, model, options) {
            _.extend(options, {
                emulateJSON: true,
                data: model.omit('errorMessage')
            });

            return Backbone.sync.apply(this, arguments);
        },
        errorHandler: function (model, errorData) {
            this.set("errorMessage", errorData.responseJSON.modelState.email);
        }
  });

  return User;
});