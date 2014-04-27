define([
  'models/base/model'
], function (Model) {
    'use strict';

    var Logout = Model.extend({
        url: '../api/User/Logout'
    });

    return Logout;
});
