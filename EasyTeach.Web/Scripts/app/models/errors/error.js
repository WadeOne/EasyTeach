define([
    'underscore',
    'backbone',
    'models/base/model'
], function (_, Backbone, Model) {
    'use strict';

    return Model.extend({
        url: '',
        defaults: {
        },
        sync: function (method, model, options) {
            _.extend(options, {
                emulateJSON: true,
                data: model.serialize()
            });

            return Backbone.sync.apply(this, arguments);
        }
    });
});