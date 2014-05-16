define([
	'underscore',
	'views/base/view',
	'text!templates/quizzes/quiz-description.html'
], function(_, View, template) {
	'use strict';

	return View.extend({
		template: template,
		autoRender: true
	});
});
