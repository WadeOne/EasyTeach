define([
  'models/base/model',
  'jquery',
  'chaplin',
  'underscore'
], function (Model, $) {
	'use strict';

	return Model.extend({
		defaults: {
            username: "",
            password: "",
            grant_type: 'password'
        },
        sync: function(method, model) {
            var self = this;
            return $.ajax({
                url: '/Token',
                type: 'POST',
                contentType: 'application/x-www-form-urlencoded',
                data: model.attributes,
                dataType: 'json'
            }).success(function(data) {
                self.trigger("success", data);
            }).fail(function(errorData) {
                self.trigger("error", errorData);
            });
        }
	});
});