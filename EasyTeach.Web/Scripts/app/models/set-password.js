define([
    'models/base/model'
], function (Model) {
    'use strict';

    return Model.extend({
        url: '../api/User/SetPassword',
        defaults: {
            errorMessage: "",
        },
        sync: function (method, model, options) {
            _.extend(options, {
                emulateJSON: true,
                data: model.omit('errorMessage')
            });

            return Backbone.sync.apply(this, arguments);
        },
        modelErrors: {
            400: function (model, errorData) {
                var parsed = errorData.responseJSON;
                this.set("errorMessage", parsed.message);
            }
        }
    });
});