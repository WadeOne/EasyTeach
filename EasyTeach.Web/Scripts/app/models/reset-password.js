define([
  'models/base/model',
  'jquery',
  'chaplin',
  'underscore'
], function (Model, $) {
    'use strict';

    return Model.extend({
        defaults: {
            email: "",
        },
        sync: function (method, model) {
            var self = this;
            return $.ajax({
                url: '/api/User/ResetPassword?email=' + model.attributes.email,
                type: 'POST',
                data: model.attributes,
                dataType: 'json'
            }).success(function (data) {
                self.trigger("success", data);
            }).fail(function (errorData) {
                self.trigger("error", errorData);
            });
        }
    });
});