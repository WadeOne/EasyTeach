define([
  'models/base/model'
], function (Model) {
    'use strict';

    return Model.extend({
        defaults: {
            email: ""
        },
        url: function() {
            return '/api/User/ResetPassword?email=' + this.get("email");
        }
    });
});