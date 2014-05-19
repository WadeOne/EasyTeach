define([
	'underscore',
	'views/base/view'
], function(_, View) {
	'use strict';

	return View.extend({
		autoRender: true,
		container: '.answer-container',
		events: {
			'click .add-option-btn': 'addOption'
		},
		initialize: function(options) {
		     this.template = options.template;
		     
		     // No need for the next line, as Backbone does it automatically also:
		     // this.model = options.model;
		},
		addOption: function (ev) {
			this.$el.append(this.template);
		}
	});
});