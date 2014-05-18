define([
    "underscore",
    "backbone",
    'models/base/model'
], function (_, Backbone, Model) {
    'use strict';

    return Model.extend({
        url: '/api/User/SetPassword',
        defaults: {
            errorMessage: ""
        },
        initialize: function() {
            this.on("invalid", this.setError);
        },
        validate: function(attrs) {
            this.set(attrs);
            if (attrs.newPassword !== attrs.confirmNewPassword) {
                return {message: "пароли должны совпадать"};
            }
        },
        setError: function (model, error) {
            this.set("errorMessage", error.message);
        },
        sync: function (method, model, options) {
            _.extend(options, {
                emulateJSON: true,
                data: model.omit(['errorMessage', 'confirmNewPassword'])
            });

            return Backbone.sync.apply(this, arguments);
        },
        modelErrors: {
            400: "setError"
        }
    });
});