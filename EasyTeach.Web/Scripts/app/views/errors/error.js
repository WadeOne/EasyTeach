define([
    'underscore',
	'chaplin',
	'views/base/view',
	'text!templates/errors/error-page.html'
], function (_, Chaplin, View, template) {
	'use strict';

	return View.extend({
		container: '#content',
		template: _.template(template),
		autoRender: true
	});
});
