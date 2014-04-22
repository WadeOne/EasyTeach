define([
  'models/base/model',
  'underscore',
  'backbone'
], function (Model, _, Backbone) {
    'use strict';

    return Model.extend({
        defaults: {
            email: ""
        },
        sync: function (method, model, options) {
            _.extend(options, {
                url: '/api/User/ResetPassword?email=' + model.get("email")
            });

            return Backbone.sync.apply(this, arguments);
        }
    });
});