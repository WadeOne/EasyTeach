define([
	'underscore',
	'views/base/view',
	'views/quizzes/quiz-description-view',
	'views/quizzes/question-list-view',
	'text!templates/quizzes/edit-quiz.html'
], function(_, View, QuizDescriptionView, QuestionListView, template) {
	'use strict';

	return View.extend({
		container: '#content',
		className: 'row',
		template: template,
		autoRender: true,
		regions: {
			description: '#quiz-description',
			questionList: '#question-list'
		},
		render: function () {
			this.constructor.__super__.render.apply(this, arguments);
			this.subview('quizDescription', new QuizDescriptionView({region: 'description', model: this.model}));
			this.subview('questionList', new QuestionListView({region: 'questionList'}));
		}
	});
});
