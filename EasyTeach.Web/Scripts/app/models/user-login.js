define([
  'models/base/model'
], function (Model) {
    'use strict';

    var UserLogin = Model.extend({
        url: '../Token'
    });

    return UserLogin;
});