define([
    'underscore',
    'backbone',
    'models/base/model'
], function (_, Backbone, Model) {
    'use strict';

    var makeSync = function(data) {
        return function (method, model, options) {
            _.extend(options, {
                async: false,
                emulateJSON: true,
                data: data
            });

            return Backbone.sync.apply(this, arguments);
        };
    };

    return Model.extend({
        url: '/api/User/ConfirmEmail',
        constructor: function() {
            var args = Array.prototype.slice.call(arguments);
            var obj = args[0];

            this.sync = makeSync(_.pick(obj, ["userId", "confirmEmailToken"]));

            args[0] = _.omit(obj, "confirmEmailToken");
            Model.apply(this, args);
        },
        initialize: function() {
          this.save();
        }
    });
});