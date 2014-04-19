define([
    'models/base/model'
], function(Model) {
    'use strict';

    return Model.extend({
        defaults: {
            route: "",
            name: "",
            active: false
        }
    });
});