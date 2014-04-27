define([
    'underscore',
    'backbone',
    'models/base/model'
], function (_, Backbone, Model) {
    'use strict';

	return Model.extend({
        url: '/Token',
		defaults: {
            errorMessage: "",
            username: "",
            password: "",
            grant_type: 'password'
        },
        initialize: function() {
            this.on("sync", this.successHandler);
            this.on("error", this.errorHandler);
        },
        sync: function(method, model, options) {
            _.extend(options, {
                emulateJSON: true,
                data: model.omit('errorMessage')
            });

            return Backbone.sync.apply(this, arguments);
        },
        successHandler: function() {
            this.publishEvent("!user:login");
        },
        errorHandler: function (model, errorData) {
            this.set("errorMessage", errorData.responseJSON.error_description);
        }
	});
});