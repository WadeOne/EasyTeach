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
		     if(options.multiple) {
		     	this.multiple = options.multiple
			debugger;
		     	console.log(this.$el.parent().find('.add-option-btn').show());
		     }
		     // No need for the next line, as Backbone does it automatically also:
		     // this.model = options.model;
		},
		addOption: function (ev) {
			this.$el.append(this.template);
		}
	});
});