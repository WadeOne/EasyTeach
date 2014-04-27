define([
    'underscore',
    'backbone',
    'models/base/model'
], function(_, Backbone, Model) {
    "use strict";

    return Model.extend({
        url: "/api/Claim/Get",
        defaults: {
            isAuthenticated: false,
            claims: []
        },
        initialize: function() {
            this.subscribeEvent('!user:login', this.fetch);
            this.on("error", this.errorHandler);
        },
        sync: function(method, model, options) {
            _.extend(options, {async: false});

            return Backbone.sync.apply(this, arguments);
        },
        errorHandler: function() {
            this.set(this.defaults);
        },
        parse: function(claims) {
            this.set({
                isAuthenticated: true,
                claims: claims
            });
        }
    });
});
