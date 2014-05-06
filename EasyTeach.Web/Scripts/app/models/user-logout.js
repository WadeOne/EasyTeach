define([
  'models/base/model'
], function (Model) {
    'use strict';

    return Model.extend({
        url: '../api/User/Logout',
        initialize: function () {
            this.on("sync", this.successHandler);
        },
        successHandler: function () {
            this.publishEvent("!user:logout");
        },
        logout: function() {
        	var data = {};
        	this.save(data);
        }
    });
});
