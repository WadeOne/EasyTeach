define([
	'underscore',
	'views/base/view'
], function(_, View) {
	'use strict';

	return View.extend({
		autoRender: true,
		/*container: '.answer-container',*/
		initialize: function(options) {
			this.constructor.__super__.initialize.apply(this, arguments);
			this.template = options.template;
		}
	});
});