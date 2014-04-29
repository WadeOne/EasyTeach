define([
  'models/base/model'
], function (Model) {
    'use strict';

    return Model.extend({
        url: '../api/User/Logout',
        logout: function() {
        	var data = {}
            this.save(data, {
            	error: function (model, response) {
            		
            	}
            });
        }
    });
});
