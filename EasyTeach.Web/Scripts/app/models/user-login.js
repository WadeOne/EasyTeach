define([
  'models/base/model'
], function (Model) {
	'use strict';

	var UserLogin = Model.extend({
		url: '../Token',
		initialize: function () {
			$.ajaxPrefilter( function( options, originalOptions, jqXHR ) {
				options.contentType = 'application/xxx-form-urlencoded';
		  });
		}
	});

	return UserLogin;
});