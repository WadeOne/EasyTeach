define([
    'underscore',
    'backbone',
    'models/base/model'
], function (_, Backbone, Model) {
    'use strict';

    return Model.extend({
        url: '../api/User/Register',
        defaults: {
            errorMessage: "",
            group: {}
        },
        sync: function (method, model, options) {
            _.extend(options, {
                emulateJSON: true,
                data: model.omit('errorMessage')
            });

            return Backbone.sync.apply(this, arguments);
        },
        modelErrors: {
            400: function (model, error) {
                this.set("errorMessage", error.message);
            }
        }
    });
});