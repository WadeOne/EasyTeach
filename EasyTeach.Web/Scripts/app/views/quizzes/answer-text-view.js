define([
	'underscore',
	'views/base/view'
], function(_, View) {
	'use strict';

	return View.extend({
		autoRender: true,
		/*container: '.answer-container',*/
		initialize: function(options) {
			debugger
			this.constructor.__super__.initialize.apply(this, arguments);
			this.template = options.template;
		},
		render: function() {
			this.constructor.__super__.render.apply(this, arguments);
			debugger
		}
	});
});