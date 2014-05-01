define([
    'underscore',
    'backbone',
    'models/base/model'
], function (_, Backbone, Model) {
    'use strict';

    return Model.extend({
        defaults: {
            errorMessage: ""
        }
    });
});