define([
	'underscore',
	'views/base/view',
	'text!templates/quizzes/edit-quiz.html'
], function(_, View, template) {
	'use strict';

	return View.extend({
		className: 'row',
		template: template,
		autoRender: true
	});
});
