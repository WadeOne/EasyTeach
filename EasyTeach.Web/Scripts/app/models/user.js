define([
  'models/base/model'
], function(Model) {
  'use strict';

  var User = Model.extend({
    url: '../api/User/Register'
  });

  return User;
});