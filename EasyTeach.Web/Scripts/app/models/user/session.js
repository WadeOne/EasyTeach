define([
    'underscore',
    'backbone',
    'models/base/model'
], function(_, Backbone, Model) {
    "use strict";

    var authenticationFails = function() {
        this.set(this.defaults);
    };

    return Model.extend({
        url: "/api/Claim",
        defaults: {
            isAuthenticated: false,
            claims: []
        },
        initialize: function() {
            this.subscribeEvent('!user:login', this.fetch);
            this.subscribeEvent('!user:logout', this.fetch);
        },
        sync: function(method, model, options) {
            _.extend(options, {async: false});

            return Backbone.sync.apply(this, arguments);
        },
        modelErrors: {
            401: authenticationFails,
            500: authenticationFails
        },
        parse: function(claims) {
            this.set({
                isAuthenticated: true,
                claims: claims
            });
        }
    });
});
