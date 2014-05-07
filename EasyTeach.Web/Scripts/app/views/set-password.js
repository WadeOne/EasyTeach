define([
	'underscore',
	'views/base/view',
	'text!templates/set-password.html'
	], function (_, View, template) {
		'use strict';

		return View.extend({
			container: '#content',
			className: 'row',
			template: _.template(template),
			autoRender: true,
		});
	});