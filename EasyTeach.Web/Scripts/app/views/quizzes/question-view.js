define([
	'underscore',
	'views/base/view',
	'text!templates/quizzes/question.html'
], function(_, View, template) {
	'use strict';

	return View.extend({
		template: template,
		autoRender: true,
		container: '#question-list',
		render: function () {
			this.$el.append(template);
		}
	});
});
