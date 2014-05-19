define([
	'underscore',
	'views/base/view',
	'views/quizzes/question-view',
	'text!templates/quizzes/question-list.html'
], function(_, View, QuestionView, template) {
	'use strict';

	return View.extend({
		template: template,
		container: '#question-wrap',
		autoRender: true,
		events: {
			"click #add-question": "addQuestionView"
		},
		addQuestionView: function () {
			new QuestionView();
		}
	});
});
