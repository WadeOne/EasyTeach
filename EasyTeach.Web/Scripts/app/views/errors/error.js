define([
	'chaplin',
	'views/base/view',
	'models/errors/error',
	'text!templates/errors/error-page.html'
], function (Chaplin, View, Error, template) {
	'use strict';

	return View.extend({
		container: '#content',
		id: 'site-container',
		template: _.template(template),
		autoRender: true,
		noWrap: true,
		render: function () {
			var errorData = $.parseJSON(window.localStorage.getItem('errorInfo')),
				error = new Error({errorMessage: $.parseJSON(errorData.responseText).message}),
				html = this.template({
					errorModel: error
				});

			this.$el.append(html);
			return this;
		}
	});
});
