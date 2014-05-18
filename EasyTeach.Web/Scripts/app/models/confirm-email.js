define([
    'underscore',
    'backbone',
    'models/base/model'
], function (_, Backbone, Model) {
    'use strict';

    return Model.extend({
        url: '/api/User/ConfirmEmail',
        constructor: function() {
            var args = Array.prototype.slice.call(arguments);
            var obj = args[0];
            var params = ["userId", "confirmEmailToken"];

            this.sync = function (method, model, options) {
                _.extend(options, {
                    async: false,
                    emulateJSON: true,
                    data: _.pick(obj, params)
                });

                return Backbone.sync.apply(this, arguments);
            };

            args[0] = _.omit(obj, params);
            Model.apply(this, args);
        },
        initialize: function() {
          this.save();
        }
    });
});